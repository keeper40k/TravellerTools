using System;
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
    /// <summary>Edits character-generation preferences and saves them when closed through the close button.</summary>
    /// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
    public partial class GeneralSettings : Form
    {
        // Constructor

        /// <summary>Creates an editor for supplied preferences.</summary>
        /// <param name="settings">The preferences edited by reference. Legacy null input creates defaults and attempts to load settings.json.</param>
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

        /// <summary>The preferences edited by the dialog; changes affect the supplied object directly.</summary>
        public CharGenSettings Settings = null!;

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
