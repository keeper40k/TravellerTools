﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TravellerTools.CharGen
{
    public partial class GeneralSettings : Form
    {
        // Constructor

        public GeneralSettings( CharGenSettings settings )
        {
            InitializeComponent();
            if (settings == null)
            {
                Settings = new CharGenSettings();
                Settings.LoadSettings();
            }
            else
            {
                Settings = settings;
            }

            promptOnNewCheckBox.Checked = Settings.PromptOnNewChar;
            allowAgeEditingBox.Checked = Settings.AllowAgeEditing;
            allowCharacterSurvivalBox.Checked = Settings.AllowCharacterSurvival;
        }

        // Public Methods

        private void closeButton_Click(object sender, EventArgs e)
        {
            Settings.SaveSettings();
            this.Close();
        }

        // Public Properties

        public CharGenSettings Settings = null;

        private void promptOnNewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.PromptOnNewChar = promptOnNewCheckBox.Checked;
        }

        private void allowAgeEditingBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AllowAgeEditing = allowAgeEditingBox.Checked;
        }

        private void allowCharacterSurvival_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AllowCharacterSurvival = allowCharacterSurvivalBox.Checked;
        }
    }
}
