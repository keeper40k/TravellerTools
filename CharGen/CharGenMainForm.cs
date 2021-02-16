﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen
{
    public partial class CharGenMainForm : Form
    {
        // Protected enums

        protected enum CreationProcessState
        {
            SELECT_SERVICE = 1,
            TERMS = 2,
            MUSTERING_OUT = 3,
            DEAD = 4
        }
        
        // constant string

        private static string NEW_CHAR_SURE = " Are you sure you want to create a New Character?\nThe current Character will be lost.";
        private static string NEW_CHAR_SURE_TITLE = "Create New Character";

        private static string RECOMMENDATIONS_LABEL = "Service Recommendations";
        private static string CHARACTER_HISTORY_LABEL = "Current Traveller Character";

        private static string ENLIST_LABEL = "{0} needs {1}+ on 2d6 to Enlist. You will get a +{2} modifier.";
        private static string ENLIST_SUCCESS = "{0} successfully enlisted in the {1}, with a total roll of {2}.";
        private static string ENLIST_FAIL = "{0} failed to enlisted in the {1}, with a total roll of {2}.";
        private static string DRAFT_RESULT = "{0} has been drafted into the {1}.";

        private static string TERM_TITLE = "Processing Term {0} ...";
        private static string TERM_BLOCK_TITLE = "Term {0} ...";
        private static string SURVIVAL_ROLL = "Survival: {0} rolled a {1} against a target of {2}+{3}.";
        private static string SURVIVAL_MODIFIED = " including a +2 bonus";
        private static string SURVIVED_THIS_TERM = "{0} survived.";
        private static string DIED_THIS_TERM = "{0} died.";
        private static string INJURED_THIS_TERM = "{0} was injured and left the {1} service.";
        private static string NOT_ELIGIBLE_FOR_COMMISSION_DUE_TO_DRAFT = "{0} was not eligible for a commission this term, due to being drafted.";
        private static string COMMISSION_ROLL = "Commission: {0} rolled a {1} against a target of {2}+{3}.";
        private static string COMMISSION_MODIFIED = " including a +1 bonus";
        private static string COMMISSION_THIS_TERM = "{0} was Commissioned as a {1}.";
        private static string COMMISSION_FAILED = "{0} was not Commissioned this term.";
        private static string PROMOTION_ROLL = "Promotion: {0} rolled a {1} against a target of {2}+{3}.";
        private static string PROMOTION_MODIFIED = " including a +1 bonus";
        private static string PROMOTION_THIS_TERM = "{0} was Promoted to a {1}.";
        private static string PROMOTION_FAILED = "{0} was not Promoted this term.";
        private static string MUST_REENLIST = "{0} is required to stay in the {1} Service for an additional term and may not leave at this time.";
        private static string REENLIST_ROLL = "Reenlistment: {0} rolled a {1} against a target of {2}.";
        private static string REENLIST_FAILED = "The {0} Service no longer needs the services of {1}. They may not enlist for another term.";

        // Protected member variables
        protected CharGenSettings Settings = null;
        protected TravellerServices Services = null;
        protected TravellerService Service = null;
        protected TravellerCharacter Character = null;
        protected bool ForceReenlistment = false;

        public CharGenMainForm()
        {
            InitializeComponent();
            Settings = new CharGenSettings();
            Settings.LoadSettings();
            Services = new TravellerServices();
            Services.LoadSettings();
            Service = new TravellerService();
            Character = new TravellerCharacter();
            CurrentState = CreationProcessState.SELECT_SERVICE;
            InitialiseServiceSelectionBoxes();
            RefreshCharacterDisplay();
            UpdateInputBoxes();
        }

        // Protected Methods

        protected void ResetState()
        {
            Service = new TravellerService();
            Character = new TravellerCharacter();
            CurrentState = CreationProcessState.SELECT_SERVICE;

            autoCreate.Enabled = true;
            nameBox.Enabled = true;
            ageNumberBox.Enabled = true;
            enlistButton.Enabled = true;
            serviceBox.Enabled = false;
            termRollButton.Enabled = false;

            enlistmentChoiceBox.SelectedIndex = 0;
            enlistTargetLabel.Text = string.Empty;
        }

        // Some of the input boxes won't change content, so they only need initialising once.
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

        protected void RefreshCharacterDisplay()
        {
            characterDisplay.Text = string.Empty;
            // This makes the history data scroll to the end
            characterDisplay.Focus();
            characterDisplay.AppendText( Character.ShortStringFormat() );

            characterHistory.Text = string.Empty;
            // Show Recommendations for Services if in SELECT_SERVICE state
            if (CurrentState == CreationProcessState.SELECT_SERVICE)
            {
                characterHistoryLabel.Text = RECOMMENDATIONS_LABEL;
                // Do need to scroll to the end here.
                characterHistory.Text = Services.RecommendText(Character);
            }
            // Otherwise show the character history
            else
            {
                characterHistoryLabel.Text = CHARACTER_HISTORY_LABEL;
                // This makes the history data scroll to the end
                characterHistory.Focus();
                characterHistory.AppendText(Character.CharacterHistory());
            }            
        }

        protected void UpdateInputBoxes()
        {
            // Roll Button
            autoCreate.Enabled = Settings.AllowReroll;
            autoCreateLabel.Enabled = Settings.AllowReroll;

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
            if( Character.Rank != string.Empty )
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

        protected void UpdateTermBoxes()
        {
            switch( CurrentState )
            {
                case CreationProcessState.SELECT_SERVICE:
                {
                        enlistmentChoiceLabel.Visible = true;
                        enlistmentChoiceBox.Visible = true;
                        enlistTargetLabel.Visible = true;
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
                        string termTitle = String.Format(TERM_TITLE, Character.TermsOfService + 1);
                        termTitleLabel.Text = termTitle;
                        termTitleLabel.Visible = true;
                        termRollButton.Enabled = true;
                        termRollButton.Visible = true;
                        enlistButton.Enabled = false;
                        musterOutButton.Visible = Character.TermsOfService > 0;
                        musterOutButton.Enabled = !ForceReenlistment;
                        break;
                }
                case CreationProcessState.DEAD:
                {
                    enlistmentChoiceLabel.Visible = false;
                    enlistmentChoiceBox.Visible = false;
                    enlistTargetLabel.Visible = false;
                    autoCreate.Enabled = false;
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
                case CreationProcessState.MUSTERING_OUT:
                {
                    enlistmentChoiceLabel.Visible = false;
                    enlistmentChoiceBox.Visible = false;
                    enlistTargetLabel.Visible = false;
                    termTitleLabel.Visible = true;
                    termRollButton.Enabled = false;
                    termRollButton.Visible = true;
                    enlistButton.Enabled = false;
                    musterOutButton.Visible = true;
                    musterOutButton.Enabled = true;
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        protected void ExecuteDraft()
        {
            Character.Drafted = true;
            if (enlistmentChoiceBox.SelectedItem is TravellerService)
            {
                Character.FailedService = (enlistmentChoiceBox.SelectedItem as TravellerService).Name;
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

        protected void ProcessTerm()
        {
            ForceReenlistment = false;
            decimal termStartingRank = Character.RankNumber;

            decimal currentTerm = Character.TermsOfService + 1;
            string textUpdate = string.Format(TERM_BLOCK_TITLE, currentTerm) + "\n";

            // Survive
            decimal survivalTarget = Service.Survival.Target;
            decimal survivalRoll = DiceTools.RollDice(2, 6);
            bool hasSurvivalModifier = Service.SurvivalPlusTwo.Pass(Character);
            if (hasSurvivalModifier)
            {
                survivalRoll += 2;
            }
            bool survived = survivalRoll >= survivalTarget;
            string modifierText = hasSurvivalModifier ? SURVIVAL_MODIFIED : string.Empty;
            textUpdate += string.Format(SURVIVAL_ROLL, Character.Name, survivalRoll, survivalTarget, modifierText) + "\n";
            if (survived)
            {
                textUpdate += string.Format(SURVIVED_THIS_TERM, Character.Name) + "\n";

                // Aging Crisis is handled in TravellerCharacter

                 // Increment Term
                Character.TermsOfService += 1;

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
                            modifierText = commissionBonus ? COMMISSION_MODIFIED : string.Empty;
                            textUpdate += string.Format(COMMISSION_ROLL, Character.Name, commissionRoll, commissionTarget, modifierText) + "\n";
                            if (commissionRoll >= commissionTarget)
                            {
                                Character.RankNumber += 1;
                                Character.Rank = Service.RankName((int)Character.RankNumber - 1);
                                // Reset, termStarting Rank, as we've just run auto-skills again
                                termStartingRank = Character.RankNumber;
                                AddAutomaticSkills();
                            }

                            if (Character.Commissioned)
                            {
                                textUpdate += string.Format(COMMISSION_THIS_TERM, Character.Name, Character.Rank) + "\n";
                            }
                            else
                            {
                                textUpdate += string.Format(COMMISSION_FAILED, Character.Name) + "\n";
                            }
                        }
                        else
                        {
                            textUpdate += string.Format(NOT_ELIGIBLE_FOR_COMMISSION_DUE_TO_DRAFT, Character.Name) + "\n";
                        }
                    }

                    // Promotion is not available until a character is Commissioned, but it can happen in the same term as a successful Commission
                    // Also, a character cannot be promoted if they have reached the maximum rank in that service
                    if( Character.Commissioned && ( Character.RankNumber < Service.Ranks.Count ) )
                    {
                        decimal promotionTarget = Service.Promotion.Target;
                        decimal promotionRoll = DiceTools.RollDice(2, 6);
                        bool promotionBonus = Service.PromotionPlusOne.Pass(Character);
                        if (promotionBonus)
                        {
                            promotionRoll += 1;
                        }
                        modifierText = promotionBonus ? PROMOTION_MODIFIED : string.Empty;
                        textUpdate += string.Format(PROMOTION_ROLL, Character.Name, promotionRoll, promotionTarget, modifierText) + "\n";
                        if (promotionRoll >= promotionTarget)
                        {
                            Character.RankNumber += 1;
                            Character.Rank = Service.RankName((int)Character.RankNumber - 1);
                            textUpdate += string.Format(PROMOTION_THIS_TERM, Character.Name, Character.Rank) + "\n";
                        }
                        else
                        {
                            textUpdate += string.Format(PROMOTION_FAILED, Character.Name) + "\n";
                        }
                    }

                }

                // Skills and Training

                // Automatic Skills if RankNumber has increased
                if (termStartingRank != Character.RankNumber)
                {
                    AddAutomaticSkills();
                }

                // TO DO

                // Reenlistment

                decimal reenlistmentRoll = DiceTools.RollDice(2, 6);
                // A reenlistment roll of 12 (two sixes on two d6) means the character must stay in the service.
                if ( reenlistmentRoll == 12 )
                {
                    textUpdate += string.Format(MUST_REENLIST, Character.Name, Service.Name) + "\n";
                    ForceReenlistment = true;
                }
                else
                {
                    bool reenlistSuccess = reenlistmentRoll >= Service.Reenlist.Target;
                    textUpdate += string.Format(REENLIST_ROLL, Character.Name, reenlistmentRoll, Service.Reenlist.Target) + "\n";
                    if( !reenlistSuccess )
                    {
                        textUpdate += string.Format(REENLIST_FAILED, Service.Name, Character.Name) + "\n";
                        CurrentState = CreationProcessState.MUSTERING_OUT;
                    }

                    // Retirement
                    // Mustering Out
                }

            }
            else if (Settings.AllowCharacterSurvival)
            {
                textUpdate += string.Format(INJURED_THIS_TERM, Character.Name, Service.Name) + "\n";
                Character.Age += 2;
                Character.InjuredDuringCreation = true;
                CurrentState = CreationProcessState.MUSTERING_OUT;
            }
            else
            {
                textUpdate += string.Format(DIED_THIS_TERM, Character.Name) + "\n";
                CurrentState = CreationProcessState.DEAD;
            }

            Character.CreationHistory += textUpdate + "\n";

            // Age
            Character.Age += 4;
            // Aging could kill the character ...
            if( Character.IsDead )
            {
                CurrentState = CreationProcessState.DEAD;
            }

            // Automatically run another term, if it has been rolled and the character hasn't died due to aging
            if ( !Character.IsDead && ForceReenlistment )
            {
                ProcessTerm();
            }
        }

        protected void AddAutomaticSkills()
        {
            List<TravellerSkillModifier> autoSkills = Service.AutomaticSkillsAtRank((int)Character.RankNumber);
            foreach (TravellerSkillModifier autoSkill in autoSkills)
            {
                Character.AddSkill(autoSkill);
            }
        }

        // Protected Properaties
        protected CreationProcessState CurrentState;

        // Events

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;
            if( Settings.PromptOnNewChar )
            {
                result = MessageBox.Show(NEW_CHAR_SURE, NEW_CHAR_SURE_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            
            if( result == DialogResult.Yes )
            {
                autoCreate.Enabled = true;
                ResetState();
                RefreshCharacterDisplay();
                UpdateInputBoxes();
            }
        }

        private void autoCreate_Click(object sender, EventArgs e)
        {
            Character.Reinitialise();
            Character.RollRandomCharacteristics();
            UpdateInputBoxes();
            RefreshCharacterDisplay();
            if (!Settings.AllowReroll)
            {
                autoCreate.Enabled = false;
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            Character.Name = nameBox.Text;
            RefreshCharacterDisplay();
        }

        private void titleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            titleBox_TextChanged(sender, e);
        }

        private void titleBox_TextChanged(object sender, EventArgs e)
        {
            Character.Title = titleBox.Text;
            RefreshCharacterDisplay();
        }

        private void useTitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Character.UseTitle = useTitleCheckBox.Checked;
            RefreshCharacterDisplay();
        }

        private void useRankCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Character.UseRank = useRankCheckBox.Checked;
            RefreshCharacterDisplay();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings settingsForm = new GeneralSettings( Settings );
            settingsForm.ShowDialog();
            Settings = settingsForm.Settings;
            UpdateInputBoxes();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ageNumberBox_ValueChanged(object sender, EventArgs e)
        {
            Character.Age = ageNumberBox.Value;
            RefreshCharacterDisplay();
        }

        private void serviceBox_TextChanged(object sender, EventArgs e)
        {
            Character.Service = serviceBox.Text;
            RefreshCharacterDisplay();
        }


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
                string enlistmentResultText = String.Format(ENLIST_SUCCESS, Character.Name, Service.Name, result);
                Character.CreationHistory += enlistmentResultText += "\n";
            }
            else
            {
                string enlistmentResultText = String.Format(ENLIST_FAIL, Character.Name, Service.Name, result);
                Character.CreationHistory += enlistmentResultText += "\n";
                // What happens in the draft ...
                ExecuteDraft();
            }

            if (Character.Drafted)
            {
                string draftResultText = String.Format(DRAFT_RESULT, Character.Name, Character.Service);
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

        private void termRollButton_Click(object sender, EventArgs e)
        {
            ProcessTerm();
            UpdateInputBoxes();
            RefreshCharacterDisplay();
        }

        private void enlistmentChoiceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // There is a string in the list, if this is chosen, then the
            // SelctedService should revert to null
            if (enlistmentChoiceBox.SelectedItem is TravellerService)
            {
                Service = enlistmentChoiceBox.SelectedItem as TravellerService;
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
                string labelText = String.Format(ENLIST_LABEL, Character.Name, target, bonus);
                enlistTargetLabel.Text = labelText;
                enlistButton.Enabled = true;
            }
            else
            {
                Service = null;
                enlistButton.Enabled = false;
            }

            UpdateInputBoxes();
        }
    }
}
