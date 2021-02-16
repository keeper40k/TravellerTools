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
    public partial class MusteringOutDialog : Form, ISkillSpecialisationCollection
    {
        // static constant strings
        private string ROLLS_REMAINING_LABEL = "Mustering Out Rolls Remaining ... {0}";
        private string CASH_ROLLS_MADE_LABEL = "{0} Cash Rolls Made";
        private string BENEFIT_ROLLS_MADE_LABEL = "{0} Benefits Rolls Made";

        // protected members

        protected TravellerService m_service;
        protected TravellerCharacter m_character;
        protected decimal m_rollsCount;
        protected decimal m_cashRollsCount;
        protected decimal m_benefitRollsCount;

        // Public Constructors

        public MusteringOutDialog(TravellerService service, TravellerCharacter character, decimal rollsCount)
        {
            m_service = service;
            m_character = character;
            m_rollsCount = rollsCount;
            m_cashRollsCount = 0;
            m_benefitRollsCount = 0;

            InitializeComponent();
            UpdateButtons();
        }

        protected void UpdateButtons()
        {
            if (m_rollsCount == 0)
            {
                // If nothing left to do, close the form
                Close();
            }
            else
            {
                rollsRemainingLabel.Text = string.Format(ROLLS_REMAINING_LABEL, m_rollsCount);
                benefitsRollLabel.Text = string.Format(BENEFIT_ROLLS_MADE_LABEL, m_benefitRollsCount);
                cashRollsLabel.Text = string.Format(CASH_ROLLS_MADE_LABEL, m_cashRollsCount);
                bonusToBenefitsLabel.Visible = m_character.RankNumber > 4;
                bonusToCashLabel.Visible = m_character.HasSkill("Gambling");

                characterDisplay.Text = m_character.ShortStringFormat();
                benefitsTableButton.Text = m_service.BenefitsTableText();
                cashTableButton.Text = m_service.CashTableText();

                cashTableButton.Enabled = m_cashRollsCount < 3;

            }
        }

        protected void ProcessBenefitSelection(List<KeyValuePair<int, TravellerMusteringOutBenefit>> table)
        {
            int roll = DiceTools.RollOneDie(6);
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
                }
                else if( rolledBenefit.IsGear )
                {
                    TravellerGear gear = BenefitGearLookup(rolledBenefit.Name);
                    m_character.Gear.Add( gear );
                }
                m_rollsCount--;
                m_benefitRollsCount++;
            }
            UpdateButtons();
        }

        protected void ProcessCashSelection(List<KeyValuePair<int, decimal>> table)
        {
            int roll = DiceTools.RollOneDie(6);
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
            TravellerGear gear = new TravellerGear();
            gear.Name = name;
            return gear;
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
    }
}