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

        protected CharGenDataBlock data = null;

        public CharGenMainForm()
        {
            InitializeComponent();
            data = new CharGenDataBlock();
            RefreshCharacterDisplay();
            UpdateInputBoxes();
        }

        // Protected Methods

        protected void RefreshCharacterDisplay()
        {
            characterDisplay.Text = data.Character.ToString();
        }

        protected void UpdateInputBoxes()
        {
            // Title Combo and Use Title
            useTitleCheckBox.Checked = data.Character.UseTitle;
            if (data.Character.SOC >= 11)
            {
                titleBox.Items.Clear();
                titleBox.Items.AddRange(data.Character.AvailableTitles().ToArray());
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
            rankBox.Text = data.Character.Rank;
            if( data.Character.Rank != string.Empty )
            {
                useRankCheckBox.Enabled = true;
                useRankCheckBox.Checked = data.Character.UseRank;
            }
            else
            {
                useRankCheckBox.Enabled = false;
            }
            // Name Box
            nameBox.Text = data.Character.Name;
        }

        // Events

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show( NEW_CHAR_SURE, NEW_CHAR_SURE_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2 );
            if( result == DialogResult.Yes )
            {
                data = new CharGenDataBlock();
                autoCreate.Enabled = true;
            }
        }

        private void autoCreate_Click(object sender, EventArgs e)
        {
            if(data != null)
            {
                data.Character.RollRandomCharacteristics();
                UpdateInputBoxes();
                RefreshCharacterDisplay();
                autoCreate.Enabled = false;
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            data.Character.Name = nameBox.Text;
            RefreshCharacterDisplay();
        }

        private void titleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            titleBox_TextChanged(sender, e);
        }

        private void titleBox_TextChanged(object sender, EventArgs e)
        {
            data.Character.Title = titleBox.Text;
            RefreshCharacterDisplay();
        }

        private void useTitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            data.Character.UseTitle = useTitleCheckBox.Checked;
            RefreshCharacterDisplay();
        }

        private void useRankCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            data.Character.UseRank = useRankCheckBox.Checked;
            RefreshCharacterDisplay();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings settingsForm = new GeneralSettings();
            settingsForm.ShowDialog();
        }
    }
}
