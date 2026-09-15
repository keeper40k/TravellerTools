using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen;

/// <summary>Hosts character creation, service terms, and mustering-out workflows.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
public partial class CharGenMainForm : Form, ISkillSpecialisationCollection
{
    // Protected enums

    /// <summary>Tracks which actions are available during character creation.</summary>
    protected enum CreationProcessState
    {
        /// <summary>The character must enlist or enter the draft.</summary>
        SELECT_SERVICE = 1,
        /// <summary>The character can resolve another service term.</summary>
        TERMS = 2,
        /// <summary>The character must leave service and muster out.</summary>
        MUST_RETIRE = 3,
        /// <summary>Mustering out has completed.</summary>
        MUSTERED_OUT = 4,
        /// <summary>The character died during creation.</summary>
        DEAD = 5
    }

    // Prompts and history formats shared by the character-creation workflow.

    private const string NewCharacterConfirmation = " Are you sure you want to create a New Character?\nThe current Character will be lost.";
    private const string NewCharacterConfirmationTitle = "Create New Character";

    private const string RecommendationsLabel = "Service Recommendations";
    private const string CharacterHistoryLabel = "Traveller Creation History";

    private const string EnlistLabel = "{0} needs {1}+ on 2d6 to Enlist. You will get a +{2} modifier.";
    private const string EnlistSuccess = "{0} successfully enlisted in the {1}, with a total roll of {2}.";
    private const string EnlistFail = "{0} failed to enlisted in the {1}, with a total roll of {2}.";
    private const string DraftResult = "{0} has been drafted into the {1}.";

    private const string TermTitle = "Processing Term {0} ...";
    private const string TermBlockTitle = "Term {0} ...";
    private const string SurvivalRoll = "Survival: {0} rolled a {1} against a target of {2}+{3}.";
    private const string SurvivalModified = " including a +2 bonus";
    private const string SurvivedThisTerm = "{0} survived.";
    private const string DiedThisTerm = "{0} died.";
    private const string InjuredThisTerm = "{0} was injured and left the {1} service.";
    private const string NotEligibleForCommissionDueToDraft = "{0} was not eligible for a commission this term, due to being drafted.";
    private const string CommissionRoll = "Commission: {0} rolled a {1} against a target of {2}+{3}.";
    private const string CommissionModified = " including a +1 bonus";
    private const string CommissionThisTerm = "{0} was Commissioned as a {1}.";
    private const string CommissionFailed = "{0} was not Commissioned this term.";
    private const string PromotionRoll = "Promotion: {0} rolled a {1} against a target of {2}+{3}.";
    private const string PromotionModified = " including a +1 bonus";
    private const string PromotionThisTerm = "{0} was Promoted to a {1}.";
    private const string PromotionFailed = "{0} was not Promoted this term.";
    private const string MustReenlist = "{0} is required to stay in the {1} Service for an additional term and may not leave at this time.";
    private const string ReenlistRoll = "Reenlistment: {0} rolled a {1} against a target of {2}.";
    private const string ReenlistFailed = "The {0} Service no longer needs the services of {1}. They may not enlist for another term.";
    private const string EnoughTerms = "{0} must now retire after serving {1} terms.";

    // Protected member variables
    /// <summary>The preferences controlling the current creation session.</summary>
    protected CharGenSettings Settings = null!;
    /// <summary>The available career definitions.</summary>
    protected TravellerServices Services = null!;
    /// <summary>The career selected for the current character.</summary>
    protected TravellerService Service = null!;
    /// <summary>The character being created and edited.</summary>
    protected TravellerCharacter Character = null!;
    /// <summary>Whether the current term result requires another term.</summary>
    protected bool ForceReenlistment = false;

    /// <summary>Initializes the character-creation interface and its settings and career data.</summary>
    public CharGenMainForm()
    {
        InitializeComponent();
        Settings = new();
        Settings.LoadSettings();
        Services = new();
        Services.LoadSettings();
        Service = new();
        Character = new();
        CurrentState = CreationProcessState.SELECT_SERVICE;
        InitialiseServiceSelectionBoxes();
        RefreshCharacterDisplay();
        UpdateInputBoxes();
    }

    // Protected Methods

    /// <summary>Resets creation-session state and refreshes the controls for a new character.</summary>
    protected void ResetState()
    {
        Service = new();
        Character = new();
        CurrentState = CreationProcessState.SELECT_SERVICE;

        nameBox.Enabled = true;
        ageNumberBox.Enabled = true;
        enlistButton.Enabled = true;
        serviceBox.Enabled = false;
        termRollButton.Enabled = false;

        enlistmentChoiceBox.SelectedIndex = 0;
        enlistTargetLabel.Text = string.Empty;
    }

    // Some of the input boxes won't change content, so they only need initialising once.
    /// <summary>Populates the career-selection controls from the available services.</summary>
    protected void InitialiseServiceSelectionBoxes()
    {
        enlistmentChoiceBox.Items.Clear();
        enlistmentChoiceBox.Items.Add(" -- Choose a Service --");
        foreach (TravellerService service in Services.Services)
        {
            enlistmentChoiceBox.Items.Add(service);
        }
        enlistmentChoiceBox.SelectedItem = enlistmentChoiceBox.Items[0];
        enlistTargetLabel.Text = string.Empty;
        enlistButton.Enabled = false;
    }

    /// <summary>Refreshes character details and history shown in the interface.</summary>
    protected void RefreshCharacterDisplay()
    {
        characterDisplay.Text = Character.ShortStringFormat();

        characterHistory.Text = string.Empty;
        // Show Recommendations for Services if in SELECT_SERVICE state
        if (CurrentState == CreationProcessState.SELECT_SERVICE)
        {
            characterHistoryLabel.Text = RecommendationsLabel;
            // Do need to scroll to the end here.
            characterHistory.Text = Services.RecommendText(Character);
        }
        // Otherwise show the character history
        else
        {
            characterHistoryLabel.Text = CharacterHistoryLabel;
            // This makes the history data scroll to the end
            characterHistory.Focus();
            characterHistory.AppendText(Character.CharacterHistory());
        }
    }

    /// <summary>Synchronizes editable character controls with the current model.</summary>
    protected void UpdateInputBoxes()
    {
        // Title Combo and Use Title
        useTitleCheckBox.Checked = Character.UseTitle;
        if (Character.SOC >= 11)
        {
            titleBox.Items.Clear();
            titleBox.Items.AddRange(Character.AvailableTitles().ToArray());
            titleBox.SelectedIndex = 0;
            titleBox.Enabled = true;
            useTitleCheckBox.Enabled = true;
        }
        else
        {
            titleBox.Text = string.Empty;
            titleBox.Items.Clear();
            titleBox.Enabled = false;
            useTitleCheckBox.Enabled = false;
        }
        // Rank Box and Use Rank
        rankBox.Text = Character.Rank;
        if (Character.Rank != string.Empty)
        {
            useRankCheckBox.Enabled = true;
            useRankCheckBox.Checked = Character.UseRank;
        }
        else
        {
            useRankCheckBox.Enabled = false;
        }
        // Name Box
        nameBox.Text = Character.Name;
        // Age Box
        ageNumberBox.Value = Character.Age;
        ageNumberBox.Enabled = Settings.AllowAgeEditing;

        // Service Box
        if (Character.Service != string.Empty)
        {
            serviceBox.Text = Character.Service;
        }

        // Term Boxes
        UpdateTermBoxes();
    }

    /// <summary>Refreshes career controls and their availability for the current creation stage.</summary>
    protected void UpdateTermBoxes()
    {
        switch (CurrentState)
        {
            case CreationProcessState.SELECT_SERVICE:
                {
                    enlistmentChoiceLabel.Visible = true;
                    enlistmentChoiceBox.Visible = true;
                    enlistTargetLabel.Visible = true;
                    enlistButton.Visible = true;
                    serviceLabel.Visible = false;
                    serviceBox.Visible = false;
                    termTitleLabel.Visible = false;
                    termRollButton.Visible = false;
                    musterOutButton.Visible = false;
                    break;
                }
            case CreationProcessState.TERMS:
                {
                    enlistmentChoiceLabel.Visible = false;
                    enlistmentChoiceBox.Visible = false;
                    enlistTargetLabel.Visible = false;
                    serviceLabel.Visible = true;
                    serviceBox.Visible = true;
                    string termTitle = string.Format(TermTitle, Character.TermsOfService + 1);
                    termTitleLabel.Text = termTitle;
                    termTitleLabel.Visible = true;
                    termRollButton.Enabled = true;
                    termRollButton.Visible = true;
                    enlistLabel.Visible = false;
                    enlistButton.Visible = false;
                    musterOutButton.Visible = Character.TermsOfService > 0;
                    musterOutButton.Enabled = !ForceReenlistment;
                    break;
                }
            case CreationProcessState.MUST_RETIRE:
                {
                    enlistmentChoiceLabel.Visible = false;
                    enlistmentChoiceBox.Visible = false;
                    enlistTargetLabel.Visible = false;
                    termTitleLabel.Visible = false;
                    termRollButton.Enabled = false;
                    termRollButton.Visible = true;
                    enlistButton.Enabled = false;
                    musterOutButton.Visible = true;
                    musterOutButton.Enabled = true;
                    break;
                }
            case CreationProcessState.MUSTERED_OUT:
                {
                    enlistmentChoiceLabel.Visible = false;
                    enlistmentChoiceBox.Visible = false;
                    enlistTargetLabel.Visible = false;
                    termTitleLabel.Visible = false;
                    termRollButton.Enabled = false;
                    termRollButton.Visible = true;
                    enlistButton.Enabled = false;
                    musterOutButton.Visible = true;
                    musterOutButton.Enabled = false;
                    break;
                }
            case CreationProcessState.DEAD:
                {
                    enlistmentChoiceLabel.Visible = false;
                    enlistmentChoiceBox.Visible = false;
                    enlistTargetLabel.Visible = false;
                    titleBox.Enabled = false;
                    useTitleCheckBox.Enabled = false;
                    rankBox.Enabled = false;
                    useRankCheckBox.Enabled = false;
                    nameBox.Enabled = false;
                    ageNumberBox.Enabled = false;
                    enlistButton.Enabled = false;
                    serviceBox.Enabled = false;
                    termRollButton.Enabled = false;
                    musterOutButton.Visible = false;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    /// <summary>Rolls the draft and assigns the character to the matching service.</summary>
    protected void ExecuteDraft()
    {
        Character.Drafted = true;
        if (enlistmentChoiceBox.SelectedItem is TravellerService)
        {
            Character.FailedService = ((TravellerService)enlistmentChoiceBox.SelectedItem).Name;
        }

        decimal draftResult = DiceTools.RollOneDie(6);
        for (int i = 0; i < Services.Services.Count; i++)
        {
            if (Services.Services[i].DraftNumber == draftResult)
            {
                Character.Service = Services.Services[i].Name;
                enlistmentChoiceBox.SelectedItem = Services.Services[i];
                break;
            }
        }
    }

    /// <summary>Resolves survival, commission, promotion, skills, and reenlistment for a service term.</summary>
    protected void ProcessTerm()
    {
        ForceReenlistment = false;
        decimal termStartingRank = Character.RankNumber;

        decimal currentTerm = Character.TermsOfService + 1;
        Character.CreationHistory += string.Format(TermBlockTitle, currentTerm) + "\n";
        string textUpdate = string.Empty;

        // Survive
        decimal survivalTarget = Service.Survival.Target;
        decimal survivalRoll = DiceTools.RollDice(2, 6);
        bool hasSurvivalModifier = Service.SurvivalPlusTwo.Pass(Character);
        if (hasSurvivalModifier)
        {
            survivalRoll += 2;
        }
        bool survived = survivalRoll >= survivalTarget;
        string modifierText = hasSurvivalModifier ? SurvivalModified : string.Empty;
        textUpdate += string.Format(SurvivalRoll, Character.Name, survivalRoll, survivalTarget, modifierText) + "\n";
        if (survived)
        {
            textUpdate += string.Format(SurvivedThisTerm, Character.Name) + "\n";

            // Aging Crisis is handled in TravellerCharacter

            // Increment Term
            Character.TermsOfService += 1;

            bool commissionedThisTerm = false;
            bool promotedThisTerm = false;

            // Commissions and Promotions are not available in all Services
            if (Service.UsesRanks)
            {
                // Commissioning shouldn't be done if the character has already been Commissioned!
                if (!Character.Commissioned)
                {
                    //  Characters cannot be commissioned in their first term of service if they were drafted
                    if (!Character.Drafted || Character.TermsOfService != 1)
                    {
                        decimal commissionTarget = Service.Commission.Target;
                        decimal commissionRoll = DiceTools.RollDice(2, 6);
                        bool commissionBonus = Service.CommissionPlusOne.Pass(Character);
                        if (commissionBonus)
                        {
                            commissionRoll += 1;
                        }
                        modifierText = commissionBonus ? CommissionModified : string.Empty;
                        textUpdate += string.Format(CommissionRoll, Character.Name, commissionRoll, commissionTarget, modifierText) + "\n";
                        if (commissionRoll >= commissionTarget)
                        {
                            Character.RankNumber += 1;
                            Character.Rank = Service.RankName((int)Character.RankNumber - 1);
                            // Reset, termStarting Rank, as we've just run auto-skills again
                            termStartingRank = Character.RankNumber;
                            commissionedThisTerm = true;
                            AddAutomaticSkills();
                        }

                        if (Character.Commissioned)
                        {
                            textUpdate += string.Format(CommissionThisTerm, Character.Name, Character.Rank) + "\n";
                        }
                        else
                        {
                            textUpdate += string.Format(CommissionFailed, Character.Name) + "\n";
                        }
                    }
                    else
                    {
                        textUpdate += string.Format(NotEligibleForCommissionDueToDraft, Character.Name) + "\n";
                    }
                }

                // Promotion is not available until a character is Commissioned, but it can happen in the same term as a successful Commission
                // Also, a character cannot be promoted if they have reached the maximum rank in that service
                if (Character.Commissioned && (Character.RankNumber < Service.Ranks.Count))
                {
                    decimal promotionTarget = Service.Promotion.Target;
                    decimal promotionRoll = DiceTools.RollDice(2, 6);
                    bool promotionBonus = Service.PromotionPlusOne.Pass(Character);
                    if (promotionBonus)
                    {
                        promotionRoll += 1;
                    }
                    modifierText = promotionBonus ? PromotionModified : string.Empty;
                    textUpdate += string.Format(PromotionRoll, Character.Name, promotionRoll, promotionTarget, modifierText) + "\n";
                    if (promotionRoll >= promotionTarget)
                    {
                        Character.RankNumber += 1;
                        Character.Rank = Service.RankName((int)Character.RankNumber - 1);
                        promotedThisTerm = true;
                        textUpdate += string.Format(PromotionThisTerm, Character.Name, Character.Rank) + "\n";
                    }
                    else
                    {
                        textUpdate += string.Format(PromotionFailed, Character.Name) + "\n";
                    }
                }

            }

            // Skills and Training

            // Automatic Skills if RankNumber has increased
            if (termStartingRank != Character.RankNumber)
            {
                AddAutomaticSkills();
            }

            // TermSkill Eligibility
            decimal skillCount = 0;
            if (Character.TermsOfService == 1)
            {
                skillCount += Service.SkillsFirstTerm;
            }
            else
            {
                skillCount += Service.SkillsPerTerm;
            }

            skillCount += commissionedThisTerm ? 1 : 0;
            skillCount += promotedThisTerm ? 1 : 0;

            if (skillCount > 0)
            {
                using SkillSelectionDialog skillsDialog = new(Service, Character, skillCount);
                skillsDialog.ShowDialog();
            }

            // Reenlistment

            decimal reenlistmentRoll = DiceTools.RollDice(2, 6);
            // A reenlistment roll of 12 (two sixes on two d6) means the character must stay in the service.
            if (reenlistmentRoll == 12)
            {
                textUpdate += string.Format(MustReenlist, Character.Name, Service.Name) + "\n";
                ForceReenlistment = true;
            }
            else
            {
                bool reenlistSuccess = reenlistmentRoll >= Service.Reenlist.Target;
                textUpdate += string.Format(ReenlistRoll, Character.Name, reenlistmentRoll, Service.Reenlist.Target) + "\n";
                if (!reenlistSuccess)
                {
                    textUpdate += string.Format(ReenlistFailed, Service.Name, Character.Name) + "\n";
                    CurrentState = CreationProcessState.MUST_RETIRE;
                }

            }

            // Age
            Character.Age += 4;
            // Aging could kill the character ...
            if (Character.IsDead)
            {
                CurrentState = CreationProcessState.DEAD;
            }
        }
        else if (Settings.AllowCharacterSurvival)
        {
            textUpdate += string.Format(InjuredThisTerm, Character.Name, Service.Name) + "\n";
            Character.Age += 2;
            Character.InjuredDuringCreation = true;
            CurrentState = CreationProcessState.MUSTERED_OUT;
        }
        else
        {
            CurrentState = CreationProcessState.DEAD;
        }

        if (CurrentState == CreationProcessState.DEAD)
        {
            textUpdate += string.Format(DiedThisTerm, Character.Name) + "\n";
        }

        Character.CreationHistory += textUpdate + "\n";

        // Only allow 7 terms, unless forced reenlistment happens
        if (Character.TermsOfService > 6 && CurrentState == CreationProcessState.TERMS && !ForceReenlistment)
        {
            Character.CreationHistory += string.Format(EnoughTerms, Character.Name, Character.TermsOfService) + "\n";
            CurrentState = CreationProcessState.MUST_RETIRE;
        }

        // Automatically run another term, if it has been rolled and the character hasn't died due to aging
        if (!Character.IsDead && ForceReenlistment)
        {
            ProcessTerm();
        }
    }

    /// <summary>Applies all automatic skill adjustments for the character's current rank.</summary>
    protected void AddAutomaticSkills()
    {
        List<TravellerSkillModifier> autoSkills = Service.AutomaticSkillsAtRank((int)Character.RankNumber);
        foreach (TravellerSkillModifier autoSkill in autoSkills)
        {
            Character.AddSkill(autoSkill, this);
        }
    }

    /// <summary>Writes the current character as JSON, replacing an existing file.</summary>
    /// <param name="filename">The destination path.</param>
    /// <remarks>File-system errors propagate.</remarks>
    /// <exception cref="System.IO.IOException">The destination cannot be written.</exception>
    protected void SaveJsonCharacter(string filename)
    {
        string json = JsonSerializer.Serialize(Character);
        File.WriteAllText(filename, json);
    }

    /// <summary>Writes the current character as display text, replacing an existing file.</summary>
    /// <param name="filename">The destination path.</param>
    /// <remarks>File-system errors propagate.</remarks>
    /// <exception cref="System.IO.IOException">The destination cannot be written.</exception>
    protected void SaveTextCharacter(string filename)
    {
        string text = Character.ToString();
        File.WriteAllText(filename, text);
    }

    // Implementation of ISkillSpecialisationCollection

    /// <summary>Displays a modal choice of specialisations.</summary>
    /// <param name="skillName">The parent skill name shown in the prompt.</param>
    /// <param name="list">The non-null, non-empty list of available choices.</param>
    /// <returns>The initially selected child, or null if the dialog has no selected skill.</returns>
    /// <remarks>A selected child with further specialisations opens another dialog, but this method returns the original child rather than the deeper result. Some implementations retain a non-nullable return annotation for compatibility.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The choice list is empty, so the dialog cannot select its first item.</exception>
    public TravellerSkill SelectSpecialisation(string skillName, List<TravellerSkill> list)
    {
        using SelectSkillSpecialisationForm form = new(skillName, list);
        form.ShowDialog();
        TravellerSkill? selectedSkill = form.SelectedSkill;
        if (selectedSkill != null && selectedSkill.HasSpecialisations)
        {
            SelectSpecialisation(selectedSkill.Name, selectedSkill.Specialisations);
        }

        return selectedSkill!;
    }

    // Protected Properaties
    /// <summary>The current creation stage used to enable UI actions.</summary>
    protected CreationProcessState CurrentState;

    // Event Handlers

    // Starts a fresh creation session after any configured confirmation.
    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DialogResult result = DialogResult.Yes;
        if (Settings.PromptOnNewChar)
        {
            result = MessageBox.Show(NewCharacterConfirmation, NewCharacterConfirmationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        }

        if (result == DialogResult.Yes)
        {
            ResetState();
            RefreshCharacterDisplay();
            UpdateInputBoxes();
        }
    }

    // Resets career gains and rerolls characteristics before refreshing the character view.
    private void autoCreate_Click(object sender, EventArgs e)
    {
        Character.Reinitialise();
        Character.RollRandomCharacteristics();
        UpdateInputBoxes();
        RefreshCharacterDisplay();
    }

    // Updates the character identity used by the summary and history display.
    private void nameBox_TextChanged(object sender, EventArgs e)
    {
        Character.Name = nameBox.Text;
        RefreshCharacterDisplay();
    }

    // Uses the same update path for a listed title and a manually entered title.
    private void titleBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        titleBox_TextChanged(sender, e);
    }

    // Stores the chosen noble title and refreshes the character summary.
    private void titleBox_TextChanged(object sender, EventArgs e)
    {
        Character.Title = titleBox.Text;
        RefreshCharacterDisplay();
    }

    // Controls whether the chosen title appears in the character summary.
    private void useTitleCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Character.UseTitle = useTitleCheckBox.Checked;
        RefreshCharacterDisplay();
    }

    // Controls whether the service rank appears in the character summary.
    private void useRankCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Character.UseRank = useRankCheckBox.Checked;
        RefreshCharacterDisplay();
    }

    /// <summary>Edits shared preferences in a modal dialog and refreshes controls affected by those preferences.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using GeneralSettings settingsForm = new(Settings);
        settingsForm.ShowDialog();
        Settings = settingsForm.Settings;
        UpdateInputBoxes();
    }

    // Closes the application's main window.
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    /// <summary>Applies manual age changes through the model's aging setter, then refreshes any resulting characteristic or history changes.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void ageNumberBox_ValueChanged(object sender, EventArgs e)
    {
        Character.Age = ageNumberBox.Value;
        RefreshCharacterDisplay();
    }

    // Stores the displayed service name in the character record.
    private void serviceBox_TextChanged(object sender, EventArgs e)
    {
        Character.Service = serviceBox.Text;
        RefreshCharacterDisplay();
    }


    /// <summary>Resolves enlistment or a fallback draft, awards rank-zero skills, and automatically processes the first term.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void enlistButton_Click(object sender, EventArgs e)
    {
        decimal target = Service.Enlistment.Target;
        decimal bonus = 0;
        if (Service.EnlistmentPlusTwo.Pass(Character))
        {
            bonus += 2;
        }
        if (Service.EnlistmentPlusOne.Pass(Character))
        {
            bonus += 1;
        }

        decimal roll = DiceTools.RollDice(2, 6);
        decimal result = roll + bonus;
        bool enlist = (result >= Service.Enlistment.Target);
        if (enlist)
        {
            string enlistmentResultText = string.Format(EnlistSuccess, Character.Name, Service.Name, result);
            Character.CreationHistory += enlistmentResultText += "\n";
        }
        else
        {
            string enlistmentResultText = string.Format(EnlistFail, Character.Name, Service.Name, result);
            Character.CreationHistory += enlistmentResultText += "\n";
            // What happens in the draft ...
            ExecuteDraft();
        }

        if (Character.Drafted)
        {
            string draftResultText = string.Format(DraftResult, Character.Name, Character.Service);
            Character.CreationHistory += draftResultText + "\n";
        }
        Character.CreationHistory += "\n";

        serviceBox.Text = Service.Name;

        // Automatic Skills at Rank 0
        AddAutomaticSkills();

        CurrentState = CreationProcessState.TERMS;
        // Run the first term automatically
        ProcessTerm();

        UpdateInputBoxes();
        RefreshCharacterDisplay();

    }

    // Resolves the next service term and refreshes all affected controls.
    private void termRollButton_Click(object sender, EventArgs e)
    {
        ProcessTerm();
        UpdateInputBoxes();
        RefreshCharacterDisplay();
    }

    /// <summary>Displays characteristic-based enlistment modifiers, disabling enlistment for the placeholder choice.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void enlistmentChoiceBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // There is a string in the list, if this is chosen, then the
        // SelctedService should revert to null
        if (enlistmentChoiceBox.SelectedItem is TravellerService)
        {
            Service = (TravellerService)enlistmentChoiceBox.SelectedItem;
            decimal target = Service.Enlistment.Target;
            decimal bonus = 0;
            if (Service.EnlistmentPlusTwo.Pass(Character))
            {
                bonus += 2;
            }
            if (Service.EnlistmentPlusOne.Pass(Character))
            {
                bonus += 1;
            }
            string labelText = string.Format(EnlistLabel, Character.Name, target, bonus);
            enlistTargetLabel.Text = labelText;
            enlistButton.Enabled = true;
        }
        else
        {
            Service = null!;
            enlistButton.Enabled = false;
        }

        UpdateInputBoxes();
    }

    /// <summary>Computes term and rank-based benefit rolls, resolves awards, and adds eligible retirement pay.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void musterOutButton_Click(object sender, EventArgs e)
    {
        decimal rolls = Character.TermsOfService;
        if (Character.RankNumber > 0)
        {
            rolls++;
        }
        if (Character.RankNumber > 2)
        {
            rolls++;
        }
        if (Character.RankNumber > 4)
        {
            rolls++;
        }
        using MusteringOutDialog form = new(Service, Character, rolls);
        form.ShowDialog();

        if (Service.HasRetirementPay && Character.TermsOfService > 4)
        {
            TravellerRetirementPay retirementPay = new();
            // Cr4000 for 5 Terms, Cr6000 for 6 Terms, Cr8000 for 7 Terms, +Cr2000 per term above.
            retirementPay.Amount = 2000 * (Character.TermsOfService - 3);
            // Intentionally not using AddGear() methods for this
            Character.Gear.Add(retirementPay);
        }

        CurrentState = CreationProcessState.MUSTERED_OUT;
        UpdateInputBoxes();
        RefreshCharacterDisplay();
    }

    /// <summary>Prompts for an export path and chooses JSON or text output from its filename extension.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveDialog = new();
        saveDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*";
        saveDialog.FilterIndex = 1;
        saveDialog.FileName = Character.Name;

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            if (saveDialog.FileName.EndsWith(".json"))
            {
                SaveJsonCharacter(saveDialog.FileName);
            }
            else if (saveDialog.FileName.EndsWith(".txt"))
            {
                SaveTextCharacter(saveDialog.FileName);
            }
        }
    }
}
