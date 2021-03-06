﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen
{
    public partial class WeaponSelectionForm : Form
    {
        // private constant strings

        private const string CHOICE_LABEL = "You have rolled {0}. Please choose on option:";

        private const string COMBAT_EXTENSION = " Combat";

        // Protected Memebers

        protected TravellerSkill m_weapon = null;
        protected List<TravellerGear> m_currentGear;

        // Public Constructors

        public WeaponSelectionForm( TravellerSkill weapon, List<TravellerGear> gear )
        {
            m_weapon = weapon;
            IsWeaponSelected = true;
            m_currentGear = gear;

            SelectedGear = null;
            SelectedSkill = null;

            InitializeComponent();
            UpdateBoxes();
        }

        // Protected methods

        protected void UpdateBoxes()
        {
            promptLabel.Text = string.Format(CHOICE_LABEL, m_weapon.Name);

            selectButton.Enabled = choicesBox.SelectedItem != null;

            // Can only do the choices box content, after using its info above
            choicesBox.Items.Clear();
            if (IsWeaponSelected)
            {
                foreach (TravellerSkill skill in m_weapon.Specialisations)
                {
                    choicesBox.Items.Add(skill);
                }
            }
            else
            {
                foreach( TravellerGear gear in m_currentGear )
                {
                    string gearToSkill = gear.GearType + COMBAT_EXTENSION;
                    if (gearToSkill == m_weapon.Name)
                    {
                        choicesBox.Items.Add(gear);
                    }

                }
            }

            if (m_currentGear.Count == 0 || ! GearContainsWeaponType() )
            {
                IsWeaponSelected = true;
                skillChoiceBox.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
            }
            UpdateCheckBoxes();
        }

        protected void UpdateCheckBoxes()
        {
            m_suppressCheckChange = true;
            weaponChoiceBox.Checked = IsWeaponSelected;
            skillChoiceBox.Checked = !IsWeaponSelected;
            m_suppressCheckChange = false;
        }

        protected bool GearContainsWeaponType()
        {
            bool found = false;
            foreach (TravellerGear gear in m_currentGear)
            {
                string gearToSkill = gear.GearType + COMBAT_EXTENSION;
                if (gearToSkill == m_weapon.Name)
                {
                    found = true;
                    // Once we've found one, that is enough to know
                    break;
                }

            }
            return found;
        }

        // Public properties

        public bool IsWeaponSelected { get; set; }
        public TravellerGear SelectedGear { get; set; }
        public TravellerSkill SelectedSkill { get; set; }

        // Private events

        private bool m_suppressCheckChange = false;

        private void weaponChoiceBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_suppressCheckChange)
            {
                IsWeaponSelected = true;
                UpdateBoxes();
            }
        }

        private void skillChoiceBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_suppressCheckChange)
            {
                IsWeaponSelected = false;
                UpdateBoxes();
            }
        }

        // Assumes that choicesBox.SelectedItem isn't null.
        private void selectButton_Click(object sender, EventArgs e)
        {
            if(IsWeaponSelected)
            {
                TravellerSkill selection = choicesBox.SelectedItem as TravellerSkill;
                if (selection != null)
                {
                    SelectedGear = TravellerGearStorehouse.GetGear(selection.Name, m_weapon.Name);
                }
                else // let's try it as gear instead!
                {
                    SelectedGear = choicesBox.SelectedItem as TravellerGear;
                }
            }
            // So, a skill instead!
            else
            {
                TravellerGear selected = choicesBox.SelectedItem as TravellerGear;
                if (selected != null) 
                {
                    SelectedSkill = TravellerSkills.MatchSkill(selected.Name);
                }
                else // let's try it as a skill instead!
                {
                    SelectedSkill = choicesBox.SelectedItem as TravellerSkill;
                }
                if (selected != null)
                {
                    SelectedSkill.Level = 1;
                }
            }
            Close();
        }

        bool m_preventSelectionLoop = false;
        private void choicesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selection = choicesBox.SelectedItem;
            if( choicesBox.Items.Contains( selection ) && !m_preventSelectionLoop )
            {
                UpdateBoxes();
                m_preventSelectionLoop = true;
                choicesBox.SelectedItem = selection;
                m_preventSelectionLoop = false;
            }
        }
    }
}
