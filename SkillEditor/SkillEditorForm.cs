using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravellerTools.TravellerData;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkillEditor
{
    public partial class SkillEditorForm : Form
    {
        // static strings
        protected static string NEW_SKILL = "New Skill";

        // Form Data

        protected List<TravellerSkill> Skills = null;
        protected TravellerSkill CurrentSkill = null;

        // Public Constructors

        public SkillEditorForm()
        {
            InitializeComponent();
            Skills = new List<TravellerSkill>();
            UpdateBoxes();
        }

        // Protected Methods

        protected void UpdateBoxes()
        {
            skillsBox.Items.Clear();
            int index = -1;
            for( int i = 0; i < Skills.Count; i++ )
            {
                skillsBox.Items.Add(Skills[i]);
                if( Skills[i] == CurrentSkill )
                {
                    index = i;
                }
            }

            if ( Skills.Count > 0 )
            {
                if( CurrentSkill == null )
                {
                    CurrentSkill = Skills[0];
                }

                skillNameBox.Enabled = true;
                skillSummaryBox.Enabled = true;
                skillDescriptionBox.Enabled = true;
                skillRefereeBox.Enabled = true;

                skillNameBox.Text = CurrentSkill.Name;
                skillSummaryBox.Text = CurrentSkill.Summary;
                skillDescriptionBox.Text = CurrentSkill.Description;
                skillRefereeBox.Text = CurrentSkill.Referee;

                if (index != -1)
                {
                    m_suppressSkillSelectionUpdate = true;
                    skillsBox.SelectedIndex = index;
                    m_suppressSkillSelectionUpdate = false;
                }
            }
            else
            {
                skillNameBox.Enabled = false;
                skillSummaryBox.Enabled = false;
                skillDescriptionBox.Enabled = false;
                skillRefereeBox.Enabled = false;
            }

            if ( Skills.Count > 0 )
            {
                skillsBox.Enabled = true;
                removeSkillButton.Enabled = true;
            }
            else
            {
                skillsBox.Enabled = false;
                removeSkillButton.Enabled = false;
            }
        }

        // Private Events

        private void addSkillButton_Click(object sender, EventArgs e)
        {
            TravellerSkill newSkill = new TravellerSkill();
            newSkill.Name = NEW_SKILL;
            Skills.Add( newSkill );
            CurrentSkill = newSkill;
            UpdateBoxes();
        }

        private void removeSkillButton_Click(object sender, EventArgs e)
        {
            Skills.Remove( CurrentSkill );
            CurrentSkill = null;
            UpdateBoxes();
        }

        private bool m_suppressSkillSelectionUpdate = false;

        private void skillsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillsBox.SelectedItem is TravellerSkill && !m_suppressSkillSelectionUpdate )
            {
                CurrentSkill = (skillsBox.SelectedItem as TravellerSkill);
                UpdateBoxes();
            }
        }

        private void skillNameBox_TextChanged(object sender, EventArgs e)
        {
            CurrentSkill.Name = skillNameBox.Text;
            UpdateBoxes();
        }

        private void skillSummaryBox_TextChanged(object sender, EventArgs e)
        {
            CurrentSkill.Summary = skillSummaryBox.Text;
        }

        private void skillDescriptionBox_TextChanged(object sender, EventArgs e)
        {
            CurrentSkill.Description = skillDescriptionBox.Text;
        }

        private void skillRefereeBox_TextChanged(object sender, EventArgs e)
        {
            CurrentSkill.Referee = skillRefereeBox.Text;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(openDialog.FileName);
                Skills = JsonSerializer.Deserialize<List<TravellerSkill>>(json);
            }

            UpdateBoxes();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            saveDialog.FilterIndex = 2;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string json = JsonSerializer.Serialize(Skills);
                File.WriteAllText(saveDialog.FileName, json);
            }
        }
    }
}
