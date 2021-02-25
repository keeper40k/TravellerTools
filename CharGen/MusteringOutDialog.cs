using System;
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
    public partial class MusteringOutDialog : Form, ISkillSpecialisationCollection
    {
        // static constant strings
        private const string ROLLS_REMAINING_LABEL = "Mustering Out Rolls Remaining ... {0}";
        private const string CASH_ROLLS_MADE_LABEL = "{0} Cash Rolls Made";
        private const string BENEFIT_ROLLS_MADE_LABEL = "{0} Benefits Rolls Made";

        private const string ROLL_RESULT_GEAR = "Roll {0} : {1}\n";
        private const string ROLL_RESULT_CASH = "Roll {0} : Cr{1}\n";

        private const string GUN_NAME = "Gun";
        private const string BLADE_NAME = "Blade";
        private const string FREE_TRADER_NAME = "Free Trader";

        private const string GEAR_TO_SKILL_SUFFIX = " Combat";

        // protected members

        protected TravellerService m_service;
        protected TravellerCharacter m_character;
        protected decimal m_rollsCount;
        protected decimal m_cashRollsCount;
        protected decimal m_benefitRollsCount;

        protected bool m_gamblingBonus;

        // Public Constructors

        public MusteringOutDialog(TravellerService service, TravellerCharacter character, decimal rollsCount)
        {
            m_service = service;
            m_character = character;
            m_rollsCount = rollsCount;
            m_cashRollsCount = 0;
            m_benefitRollsCount = 0;

            TravellerSkill gamblingSkill = null;
            foreach (TravellerSkill skill in m_character.Skills)
            {
                if (skill.Name == "Gambling")
                {
                    gamblingSkill = skill;
                    break;
                }
            }
            if (gamblingSkill != null && gamblingSkill.Level > 0)
            {
                m_gamblingBonus = true;
            }
            else
            {
                m_gamblingBonus = false;
            }

            InitializeComponent();

            resultsBox.Text = string.Empty;

            UpdateButtons();
        }

        protected void UpdateButtons()
        {
            if (m_rollsCount == 0)
            {
                benefitsTableButton.Enabled = false;
                cashTableButton.Enabled = false;

                characterDisplay.Text = m_character.ShortStringFormat();

                closeButton.Enabled = true;
            }
            else
            {
                rollsRemainingLabel.Text = string.Format(ROLLS_REMAINING_LABEL, m_rollsCount);
                benefitsRollLabel.Text = string.Format(BENEFIT_ROLLS_MADE_LABEL, m_benefitRollsCount);
                cashRollsLabel.Text = string.Format(CASH_ROLLS_MADE_LABEL, m_cashRollsCount);
                bonusToBenefitsLabel.Visible = m_character.RankNumber > 4;
                
                characterDisplay.Text = m_character.ShortStringFormat();
                benefitsTableButton.Text = m_service.BenefitsTableText();
                cashTableButton.Text = m_service.CashTableText();

                cashTableButton.Enabled = m_cashRollsCount < 3;

                closeButton.Enabled = false;
            }

            bonusToCashLabel.Visible = m_gamblingBonus;
        }

        protected void ProcessBenefitSelection(List<KeyValuePair<int, TravellerMusteringOutBenefit>> table)
        {
            object resultForReporting = null;

            int roll = DiceTools.RollOneDie(6);
            if( m_character.RankNumber > 4 )
            {
                roll++;
            }
            TravellerMusteringOutBenefit rolledBenefit = null;
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
                    TravellerSkillModifier att = BenefitAttLookup(rolledBenefit.Name);
                    m_character.AddSkill(att, this);
                    resultForReporting = att;
                }
                else if( rolledBenefit.IsGear )
                {
                    if(rolledBenefit.Name == GUN_NAME || rolledBenefit.Name == BLADE_NAME )
                    {
                        TravellerSkill weaponSkill = TravellerSkills.MatchSkill(rolledBenefit.Name + GEAR_TO_SKILL_SUFFIX);
                        WeaponSelectionForm form = new WeaponSelectionForm(weaponSkill, m_character.Gear);
                        form.ShowDialog();
                        if (form.IsWeaponSelected)
                        {
                            m_character.AddGear(form.SelectedGear);
                            resultForReporting = form.SelectedGear;
                        }
                        else
                        {
                            m_character.AddSkill(form.SelectedSkill);
                            resultForReporting = form.SelectedSkill;
                        }
                    }
                    else
                    {
                        bool found = false;
                        TravellerGear gear = BenefitGearLookup(rolledBenefit.Name);
                        if( gear is TravellerStarshipBenefit && gear.Name == FREE_TRADER_NAME )
                        {
                            TravellerStarshipBenefit starship = gear as TravellerStarshipBenefit;
                            foreach( TravellerGear charGear in m_character.Gear )
                            {
                                if( charGear is TravellerStarshipBenefit && charGear.Name == FREE_TRADER_NAME )
                                {
                                    found = true;
                                    TravellerStarshipBenefit charStarship = charGear as TravellerStarshipBenefit;
                                    charStarship.MortgageDuration -= 10;
                                    break;
                                }
                            }
                        }
                        if (!found)
                        {
                            m_character.AddGear(gear);
                            resultForReporting = gear;
                        }
                    }
                }
                m_rollsCount--;
                m_benefitRollsCount++;
            }
            string benefitString = rolledBenefit == null ? string.Empty : rolledBenefit.ToString();
            resultsBox.Text += string.Format( ROLL_RESULT_GEAR, m_cashRollsCount+m_benefitRollsCount, benefitString );

            UpdateButtons();
        }

        protected void ProcessCashSelection(List<KeyValuePair<int, decimal>> table)
        {
            int roll = DiceTools.RollOneDie(6);
            if( m_gamblingBonus )
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

            m_character.Cash += cashValue;
            m_rollsCount--;
            m_cashRollsCount++;

            resultsBox.Text += string.Format(ROLL_RESULT_CASH, m_cashRollsCount + m_benefitRollsCount, cashValue);

            UpdateButtons();
        }

        protected TravellerSkillModifier BenefitAttLookup( string name )
        {
            TravellerSkillModifier skill = new TravellerSkillModifier();
            switch( name )
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

        protected TravellerGear BenefitGearLookup( string name )
        {
            return TravellerGearStorehouse.GetGear(name, string.Empty);
        }

        // Implementation of ISkillSpecialisationCollection

        public TravellerSkill SelectSpecialisation(string skillName, List<TravellerSkill> list)
        {
            SelectSkillSpecialisationForm form = new SelectSkillSpecialisationForm(skillName, list);
            form.ShowDialog();
            TravellerSkill selectedSkill = form.SelectedSkill;
            if (selectedSkill != null && selectedSkill.HasSpecialisations)
            {
                SelectSpecialisation(selectedSkill.Name, selectedSkill.Specialisations);
            }

            return selectedSkill;
        }

        // Private Event Handlers

        private void benefitsTableButton_Click(object sender, EventArgs e)
        {
            ProcessBenefitSelection( m_service.BenefitsTable );
    }

    private void cashTableButton_Click(object sender, EventArgs e)
        {
            ProcessCashSelection( m_service.CashTable );
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}