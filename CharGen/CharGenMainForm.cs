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

        private static string TERM_TITLE = "Processing Term {0} ...";
        private static string TERM_BLOCK_TITLE = "Term {0} ...";
        private static string SURVIVAL_ROLL = "Survival: {0} rolled a {1} against a target of {2}+{3}.";
        private static string SURVIVAL_MODIFIED = " including a +2 bonus";
        private static string SURVIVED_THIS_TERM = "{0} survived.";
        private static string DIED_THIS_TERM = "{0} died.";
        private static string INJURED_THIS_TERM = "{0} was injured and left the {1} service.";

        // Protected member variables
        protected CharGenSettings m_settings = null;
        protected CharGenDataBlock m_data = null;

        public CharGenMainForm()
        {
            InitializeComponent();
            m_settings = new CharGenSettings();
            m_settings.LoadSettings();
            m_data = new CharGenDataBlock();
            CurrentState = CreationProcessState.SELECT_SERVICE;
            RefreshCharacterDisplay();
            UpdateInputBoxes();
        }

        // Protected Methods

        protected void ResetState()
        {
            m_data = new CharGenDataBlock();
            CurrentState = CreationProcessState.SELECT_SERVICE;

            autoCreate.Enabled = true;
            nameBox.Enabled = true;
            ageNumberBox.Enabled = true;
            enlistButton.Enabled = true;
            serviceBox.Enabled = false;
            termRollButton.Enabled = false;
        }

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

            // Term Boxes
            UpdateTermBoxes();
        }

        protected void UpdateTermBoxes()
        {
            switch( CurrentState )
            {
                case CreationProcessState.SELECT_SERVICE:
                {
                        termTitleLabel.Visible = false;
                        termRollButton.Visible = false;
                        break;
                }
                case CreationProcessState.TERMS:
                {
                        string termTitle = String.Format(TERM_TITLE, m_data.Character.TermsOfService + 1);
                        termTitleLabel.Text = termTitle;
                        termTitleLabel.Visible = true;
                        termRollButton.Enabled = true;
                        termRollButton.Visible = true;
                        break;
                }
                case CreationProcessState.DEAD:
                {
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
                    break;
                }
                case CreationProcessState.MUSTERING_OUT:
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        protected void ProcessTerm()
        {
            decimal currentTerm = m_data.Character.TermsOfService + 1;
            string textUpdate = string.Format(TERM_BLOCK_TITLE, currentTerm) + "\n";

            // Survive
            decimal survivalTarget = m_data.Service.Survival.Target;
            decimal survivalRoll = DiceTools.RollDice(2, 6);
            bool hasSurvivalModifier = m_data.Service.SurvivalPlusTwo.Pass(m_data.Character);
            if (hasSurvivalModifier)
            {
                survivalRoll += 2;
            }
            bool survived = survivalRoll >= survivalTarget;
            string modifierText = hasSurvivalModifier ? SURVIVAL_MODIFIED : string.Empty;
            textUpdate += string.Format(SURVIVAL_ROLL, m_data.Character.Name, survivalRoll, survivalTarget, modifierText) + "\n";
            if (survived)
            {
                textUpdate += string.Format(SURVIVED_THIS_TERM, m_data.Character.Name) + "\n";

                // Age
                m_data.Character.Age += 4;

                // Aging Crisis

                // TO DO

                // Increment Term
                m_data.Character.TermsOfService += 1;

                // Commission
                // Promotion
                // Skills and Training
                // Reenlistment
                // Retirement
                // Mustering Out
            }
            else if (m_settings.AllowCharacterSurvival)
            {
                textUpdate += string.Format(INJURED_THIS_TERM, m_data.Character.Name, m_data.Service.Name) + "\n";
                m_data.Character.Age += 2;
                m_data.Character.InjuredDuringCreation = true;
                CurrentState = CreationProcessState.MUSTERING_OUT;
            }
            else
            {
                textUpdate += string.Format(DIED_THIS_TERM, m_data.Character.Name) + "\n";
                CurrentState = CreationProcessState.DEAD;
            }

            m_data.Character.CreationHistory += textUpdate + "\n";
        }

        // Protected Properaties
        protected CreationProcessState CurrentState;

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
                autoCreate.Enabled = true;
                ResetState();
                RefreshCharacterDisplay();
                UpdateInputBoxes();
            }
        }

        private void autoCreate_Click(object sender, EventArgs e)
        {
            if(m_data != null)
            {
                m_data.Character.Reinitialise();
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
                m_data.Service = enlistForm.SelectedService;
                CurrentState = CreationProcessState.TERMS;
                UpdateInputBoxes();
                RefreshCharacterDisplay();
            }
        }

        private void termRollButton_Click(object sender, EventArgs e)
        {
            ProcessTerm();
            UpdateInputBoxes();
            RefreshCharacterDisplay();
        }
    }
}
