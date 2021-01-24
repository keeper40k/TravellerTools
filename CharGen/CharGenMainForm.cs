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
    public partial class CharGenMainForm : Form
    {
        // constant string

        private static string NEW_CHAR_SURE = " Are you sure you want to create a New Character?\nThe current Character will be lost.";
        private static string NEW_CHAR_SURE_TITLE = "Create New Character";

        // Protected member variables
        protected CharGenSettings m_settings = null;
        protected CharGenDataBlock m_data = null;

        public CharGenMainForm()
        {
            InitializeComponent();
            m_settings = new CharGenSettings();
            m_settings.LoadSettings();
            m_data = new CharGenDataBlock();
            RefreshCharacterDisplay();
            UpdateInputBoxes();
        }

        // Protected Methods

        protected void RefreshCharacterDisplay()
        {
            characterDisplay.Text = m_data.Character.ToString();
        }

        protected void UpdateInputBoxes()
        {
            // Roll Button
            autoCreate.Enabled = m_settings.AllowReroll;
            autoCreateLabel.Enabled = m_settings.AllowReroll;

            // Title Combo and Use Title
            useTitleCheckBox.Checked = m_data.Character.UseTitle;
            if (m_data.Character.SOC >= 11)
            {
                titleBox.Items.Clear();
                titleBox.Items.AddRange(m_data.Character.AvailableTitles().ToArray());
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
            rankBox.Text = m_data.Character.Rank;
            if( m_data.Character.Rank != string.Empty )
            {
                useRankCheckBox.Enabled = true;
                useRankCheckBox.Checked = m_data.Character.UseRank;
            }
            else
            {
                useRankCheckBox.Enabled = false;
            }
            // Name Box
            nameBox.Text = m_data.Character.Name;
            // Age Box
            ageNumberBox.Value = m_data.Character.Age;
            ageNumberBox.Enabled = m_settings.AllowAgeEditing;

            // Service Box
            if (m_data.Character.Service != string.Empty)
            {
                serviceBox.Text = m_data.Character.Service;
            }
        }

        // Events

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;
            if( m_settings.PromptOnNewChar )
            {
                result = MessageBox.Show(NEW_CHAR_SURE, NEW_CHAR_SURE_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            
            if( result == DialogResult.Yes )
            {
                m_data = new CharGenDataBlock();
                autoCreate.Enabled = true;
                RefreshCharacterDisplay();
                UpdateInputBoxes();
            }
        }

        private void autoCreate_Click(object sender, EventArgs e)
        {
            if(m_data != null)
            {
                m_data.Character.RollRandomCharacteristics();
                UpdateInputBoxes();
                RefreshCharacterDisplay();
                if (!m_settings.AllowReroll)
                {
                    autoCreate.Enabled = false;
                }
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            m_data.Character.Name = nameBox.Text;
            RefreshCharacterDisplay();
        }

        private void titleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            titleBox_TextChanged(sender, e);
        }

        private void titleBox_TextChanged(object sender, EventArgs e)
        {
            m_data.Character.Title = titleBox.Text;
            RefreshCharacterDisplay();
        }

        private void useTitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_data.Character.UseTitle = useTitleCheckBox.Checked;
            RefreshCharacterDisplay();
        }

        private void useRankCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_data.Character.UseRank = useRankCheckBox.Checked;
            RefreshCharacterDisplay();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings settingsForm = new GeneralSettings( m_settings );
            settingsForm.ShowDialog();
            m_settings = settingsForm.Settings;
            UpdateInputBoxes();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ageNumberBox_ValueChanged(object sender, EventArgs e)
        {
            m_data.Character.Age = ageNumberBox.Value;
            RefreshCharacterDisplay();
        }

        private void serviceBox_TextChanged(object sender, EventArgs e)
        {
            m_data.Character.Service = serviceBox.Text;
            RefreshCharacterDisplay();
        }

        private void enlistButton_Click(object sender, EventArgs e)
        {
            EnlistServiceForm enlistForm = new EnlistServiceForm( m_data.Character );
            DialogResult result = enlistForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_data.Character = enlistForm.Character;
                UpdateInputBoxes();
                RefreshCharacterDisplay();
            }
        }
    }
}
