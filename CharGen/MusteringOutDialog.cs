using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen;

/// <summary>Resolves a character's remaining cash and non-cash mustering-out selections.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
public partial class MusteringOutDialog : Form, ISkillSpecialisationCollection
{
    // Display formats and the exact benefit names used by the service data.
    private const string RollsRemainingLabel = "Mustering Out Rolls Remaining ... {0}";
    private const string CashRollsMadeLabel = "{0} Cash Rolls Made";
    private const string BenefitRollsMadeLabel = "{0} Benefits Rolls Made";

    private const string RollResultGear = "Roll {0} : {1}\n";
    private const string RollResultCash = "Roll {0} : Cr{1}\n";

    private const string GunName = "Gun";
    private const string BladeName = "Blade";
    private const string FreeTraderName = "Free Trader";

    private const string GearToSkillSuffix = " Combat";

    // protected members

    /// <summary>The service whose tables supply the available outcomes.</summary>
    protected TravellerService service;
    /// <summary>The character mutated when a selection is resolved.</summary>
    protected TravellerCharacter character;
    /// <summary>The number of mustering-out rolls initially available.</summary>
    protected decimal rollsCount;
    /// <summary>The number of cash-table rolls already taken.</summary>
    protected decimal cashRollsCount;
    /// <summary>The number of non-cash benefit rolls already taken.</summary>
    protected decimal benefitRollsCount;

    /// <summary>Whether the character qualifies for the Gambling skill cash modifier.</summary>
    protected bool gamblingBonus;

    // Public Constructors

    /// <summary>Initializes mustering-out choices for a character leaving service.</summary>
    /// <param name="service">The non-null service supplying cash and benefit tables.</param>
    /// <param name="character">The non-null character to receive the awards.</param>
    /// <param name="rollsCount">The available mustering-out roll allowance.</param>
    public MusteringOutDialog(TravellerService service, TravellerCharacter character, decimal rollsCount)
    {
        this.service = service;
        this.character = character;
        this.rollsCount = rollsCount;
        cashRollsCount = 0;
        benefitRollsCount = 0;

        TravellerSkill? gamblingSkill = null;
        foreach (TravellerSkill skill in this.character.Skills)
        {
            if (skill.Name == "Gambling")
            {
                gamblingSkill = skill;
                break;
            }
        }
        if (gamblingSkill != null && gamblingSkill.Level > 0)
        {
            gamblingBonus = true;
        }
        else
        {
            gamblingBonus = false;
        }

        InitializeComponent();

        resultsBox.Text = string.Empty;

        UpdateButtons();
    }

    /// <summary>Refreshes remaining-roll labels, table choices, and cash-roll availability.</summary>
    protected void UpdateButtons()
    {
        if (rollsCount == 0)
        {
            benefitsTableButton.Enabled = false;
            cashTableButton.Enabled = false;

            characterDisplay.Text = character.ShortStringFormat();

            closeButton.Enabled = true;
        }
        else
        {
            rollsRemainingLabel.Text = string.Format(RollsRemainingLabel, rollsCount);
            benefitsRollLabel.Text = string.Format(BenefitRollsMadeLabel, benefitRollsCount);
            cashRollsLabel.Text = string.Format(CashRollsMadeLabel, cashRollsCount);
            bonusToBenefitsLabel.Visible = character.RankNumber > 4;

            characterDisplay.Text = character.ShortStringFormat();
            benefitsTableButton.Text = service.BenefitsTableText();
            cashTableButton.Text = service.CashTableText();

            cashTableButton.Enabled = cashRollsCount < 3;

            closeButton.Enabled = false;
        }

        bonusToCashLabel.Visible = gamblingBonus;
    }

    /// <summary>Rolls a non-cash benefit, applies it to the character, and refreshes the remaining choices.</summary>
    /// <param name="table">The non-null list mapping die results to benefits.</param>
    protected void ProcessBenefitSelection(List<KeyValuePair<int, TravellerMusteringOutBenefit>> table)
    {
        int roll = DiceTools.RollOneDie(6);
        if (character.RankNumber > 4)
        {
            roll++;
        }
        TravellerMusteringOutBenefit? rolledBenefit = null;
        foreach (KeyValuePair<int, TravellerMusteringOutBenefit> item in table)
        {
            if (roll == item.Key)
            {
                rolledBenefit = item.Value;
                break;
            }
        }
        if (rolledBenefit != null)
        {
            if (rolledBenefit.IsAtt)
            {
                TravellerSkillModifier adjustment = BenefitAttLookup(rolledBenefit.Name);
                character.AddSkill(adjustment, this);
            }
            else if (rolledBenefit.IsGear)
            {
                if (rolledBenefit.Name == GunName || rolledBenefit.Name == BladeName)
                {
                    TravellerSkill? weaponSkill = TravellerSkills.MatchSkill(rolledBenefit.Name + GearToSkillSuffix);
                    if (weaponSkill == null)
                    {
                        return;
                    }
                    using WeaponSelectionForm form = new(weaponSkill, character.Gear);
                    form.ShowDialog();
                    if (form.IsWeaponSelected)
                    {
                        if (form.SelectedGear != null)
                        {
                            character.AddGear(form.SelectedGear);
                        }
                    }
                    else
                    {
                        if (form.SelectedSkill != null)
                        {
                            character.AddSkill(form.SelectedSkill);
                        }
                    }
                }
                else
                {
                    bool found = false;
                    TravellerGear? gear = BenefitGearLookup(rolledBenefit.Name);
                    if (gear is TravellerStarshipBenefit && gear.Name == FreeTraderName)
                    {
                        foreach (TravellerGear charGear in character.Gear)
                        {
                            if (charGear is TravellerStarshipBenefit && charGear.Name == FreeTraderName)
                            {
                                found = true;
                                ((TravellerStarshipBenefit)charGear).MortgageDuration -= 10;
                                break;
                            }
                        }
                    }
                    if (!found)
                    {
                        if (gear != null)
                        {
                            character.AddGear(gear);
                        }
                    }
                }
            }
            rollsCount--;
            benefitRollsCount++;
        }
        string benefitString = rolledBenefit == null ? string.Empty : rolledBenefit.ToString();
        resultsBox.Text += string.Format(RollResultGear, cashRollsCount + benefitRollsCount, benefitString);

        UpdateButtons();
    }

    /// <summary>Rolls a cash award, updates the character's cash, and records the selection.</summary>
    /// <param name="table">The non-null list mapping die results to credit awards.</param>
    protected void ProcessCashSelection(List<KeyValuePair<int, decimal>> table)
    {
        int roll = DiceTools.RollOneDie(6);
        if (gamblingBonus)
        {
            roll++;
        }
        int cashValue = 0;
        foreach (KeyValuePair<int, decimal> item in table)
        {
            if (roll == item.Key)
            {
                cashValue = (int)item.Value;
                break;
            }
        }

        character.Cash += cashValue;
        rollsCount--;
        cashRollsCount++;

        resultsBox.Text += string.Format(RollResultCash, cashRollsCount + benefitRollsCount, cashValue);

        UpdateButtons();
    }

    /// <summary>Translates supported characteristic benefit labels into adjustments.</summary>
    /// <param name="name">A label such as '+1 INT'; recognized increases are +1 or +2 INT, EDU, and SOC.</param>
    /// <returns>The corresponding characteristic adjustment, or an unnamed zero-level skill adjustment for an unknown label.</returns>
    protected TravellerSkillModifier BenefitAttLookup(string name)
    {
        TravellerSkillModifier skill = new();
        switch (name)
        {
            case "+1 INT":
                {
                    skill.Name = "INT";
                    skill.Level = 1;
                    skill.IsAttribute = true;
                    skill.IsSkill = false;
                    break;
                }
            case "+2 INT":
                {
                    skill.Name = "INT";
                    skill.Level = 2;
                    skill.IsAttribute = true;
                    skill.IsSkill = false;
                    break;
                }
            case "+1 EDU":
                {
                    skill.Name = "EDU";
                    skill.Level = 1;
                    skill.IsAttribute = true;
                    skill.IsSkill = false;
                    break;
                }
            case "+2 EDU":
                {
                    skill.Name = "EDU";
                    skill.Level = 2;
                    skill.IsAttribute = true;
                    skill.IsSkill = false;
                    break;
                }
            case "+1 SOC":
                {
                    skill.Name = "SOC";
                    skill.Level = 1;
                    skill.IsAttribute = true;
                    skill.IsSkill = false;
                    break;
                }
            case "+2 SOC":
                {
                    skill.Name = "SOC";
                    skill.Level = 2;
                    skill.IsAttribute = true;
                    skill.IsSkill = false;
                    break;
                }
            default:
                {
                    // Shouldn't find us here, so let's return just the emptry TravellerSkillModifier
                    break;
                }
        }
        return skill;
    }

    /// <summary>Looks up a named gear benefit without filtering by weapon type.</summary>
    /// <param name="name">The exact gear name.</param>
    /// <returns>The shared gear definition, or null.</returns>
    protected TravellerGear? BenefitGearLookup(string name)
    {
        return TravellerGearStorehouse.GetGear(name, string.Empty);
    }

    // Implementation of ISkillSpecialisationCollection

    /// <summary>Displays a modal choice of specialisations.</summary>
    /// <param name="skillName">The parent skill name shown in the prompt.</param>
    /// <param name="list">The non-null, non-empty list of available choices.</param>
    /// <returns>The initially selected child, or null if the dialog has no selected skill.</returns>
    /// <remarks>A selected child with further specialisations opens another dialog, but this method returns the original child rather than the deeper result. Some implementations retain a non-nullable return annotation for compatibility.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The choice list is empty, so the dialog cannot select its first item.</exception>
    public TravellerSkill? SelectSpecialisation(string skillName, List<TravellerSkill> list)
    {
        using SelectSkillSpecialisationForm form = new(skillName, list);
        form.ShowDialog();
        TravellerSkill? selectedSkill = form.SelectedSkill;
        if (selectedSkill != null && selectedSkill.HasSpecialisations)
        {
            SelectSpecialisation(selectedSkill.Name, selectedSkill.Specialisations);
        }

        return selectedSkill;
    }

    // Private Event Handlers

    // Resolves one of the remaining non-cash benefit rolls.
    private void benefitsTableButton_Click(object sender, EventArgs e)
    {
        ProcessBenefitSelection(service.BenefitsTable);
    }

    // Resolves one of the available cash-table rolls.
    private void cashTableButton_Click(object sender, EventArgs e)
    {
        ProcessCashSelection(service.CashTable);
    }

    // Returns control to character creation after the awards have been resolved.
    private void closeButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
