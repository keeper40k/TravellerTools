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
        protected TravellerSkill CurrentSpecialisationSkill = null;

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
                skillHasSpecialisationsBox.Enabled = true;
                skillSummaryBox.Enabled = true;
                skillDescriptionBox.Enabled = true;
                skillRefereeBox.Enabled = true;

                skillNameBox.Text = CurrentSkill.Name;
                skillHasSpecialisationsBox.Checked = CurrentSkill.HasSpecialisations;
                skillSummaryBox.Text = CurrentSkill.Summary;
                skillDescriptionBox.Text = CurrentSkill.Description;
                skillRefereeBox.Text = CurrentSkill.Referee;

                if (index != -1)
                {
                    m_suppressSkillSelectionUpdate = true;
                    skillsBox.SelectedIndex = index;
                    m_suppressSkillSelectionUpdate = false;
                }

                skillsBox.Enabled = true;
                removeSkillButton.Enabled = true;
                skillUpButton.Enabled = skillsBox.SelectedIndex != 0;
                skillDownButton.Enabled = skillsBox.SelectedIndex != Skills.Count - 1;
            }
            else
            {
                skillNameBox.Enabled = false;
                skillHasSpecialisationsBox.Enabled = false;
                skillSummaryBox.Enabled = false;
                skillDescriptionBox.Enabled = false;
                skillRefereeBox.Enabled = false;

                skillsBox.Enabled = false;
                removeSkillButton.Enabled = false;
                skillUpButton.Enabled = false;
                skillDownButton.Enabled = false;
            }

            specialisationSkillsBox.Items.Clear();
            if ( CurrentSkill != null && CurrentSkill.HasSpecialisations )
            {
                index = -1;
                for (int i = 0; i < CurrentSkill.Specialisations.Count; i++)
                {
                    specialisationSkillsBox.Items.Add(CurrentSkill.Specialisations[i]);
                    if (CurrentSkill.Specialisations[i] == CurrentSpecialisationSkill)
                    {
                        index = i;
                    }
                }

                if (CurrentSkill.Specialisations.Count > 0)
                {
                    if (CurrentSpecialisationSkill == null)
                    {
                        CurrentSpecialisationSkill = CurrentSkill.Specialisations[0];
                    }
                    if (index != -1)
                    {
                        m_suppressSpecialistSkillSelectionUpdate = true;
                        specialisationSkillsBox.SelectedIndex = index;
                        m_suppressSpecialistSkillSelectionUpdate = false;
                    }

                    specialisationSkillsBox.Enabled = true;
                    specialisationSkillNameBox.Enabled = true;
                    specialisationSkillSummaryBox.Enabled = true;
                    specialisationSkillDescriptionBox.Enabled = true;
                    specialisationSkillRefereeBox.Enabled = true;

                    removeSpecialistSkillButton.Enabled = true;
                    specialistSkillUpButton.Enabled = specialisationSkillsBox.SelectedIndex != 0;
                    specialistSkillDownButton.Enabled = specialisationSkillsBox.SelectedIndex != CurrentSkill.Specialisations.Count - 1;
                }
                addSpecialistSkillButton.Enabled = true;

                if (CurrentSpecialisationSkill != null)
                {
                    specialisationSkillNameBox.Text = CurrentSpecialisationSkill.Name;
                    specialisationSkillSummaryBox.Text = CurrentSpecialisationSkill.Summary;
                    specialisationSkillDescriptionBox.Text = CurrentSpecialisationSkill.Description;
                    specialisationSkillRefereeBox.Text = CurrentSpecialisationSkill.Referee;
                }
                else
                {
                    specialisationSkillNameBox.Text = string.Empty;
                    specialisationSkillSummaryBox.Text = string.Empty;
                    specialisationSkillDescriptionBox.Text = string.Empty;
                    specialisationSkillRefereeBox.Text = string.Empty;
                }
            }
            else
            {
                specialisationSkillsBox.Enabled = false;
                specialisationSkillNameBox.Enabled = false;
                specialisationSkillSummaryBox.Enabled = false;
                specialisationSkillDescriptionBox.Enabled = false;
                specialisationSkillRefereeBox.Enabled = false;

                addSpecialistSkillButton.Enabled = false;
                removeSpecialistSkillButton.Enabled = false;
                specialistSkillUpButton.Enabled = false;
                specialistSkillDownButton.Enabled = false;

                specialisationSkillNameBox.Text = string.Empty;
                specialisationSkillSummaryBox.Text = string.Empty;
                specialisationSkillDescriptionBox.Text = string.Empty;
                specialisationSkillRefereeBox.Text = string.Empty;
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
                if( ! CurrentSkill.HasSpecialisations )
                {
                    CurrentSpecialisationSkill = null;
                }
                else if( CurrentSkill.Specialisations.Count > 0 )
                {
                    CurrentSpecialisationSkill = CurrentSkill.Specialisations[0];
                }
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

        private void skillUpButton_Click(object sender, EventArgs e)
        {
            int currentIndex = skillsBox.SelectedIndex;
            // Don't do anything if this is index 0 (top item)
            if (currentIndex > 0)
            {
                Skills.Reverse(currentIndex - 1, 2);
                UpdateBoxes();
            }
        }

        private void skillDownButton_Click(object sender, EventArgs e)
        {
            int currentIndex = skillsBox.SelectedIndex;
            // Don't do anything if this is the last item
            if (currentIndex < Skills.Count-1)
            {
                Skills.Reverse(currentIndex, 2);
                UpdateBoxes();
            }
        }

        private void hasSpecialisationsBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentSkill.HasSpecialisations = skillHasSpecialisationsBox.Checked;
            UpdateBoxes();
        }

        private void addSpecialistSkillButton_Click(object sender, EventArgs e)
        {
            TravellerSkill newSkill = new TravellerSkill();
            newSkill.Name = NEW_SKILL;
            CurrentSkill.Specialisations.Add(newSkill);
            CurrentSpecialisationSkill = newSkill;
            UpdateBoxes();
        }

        private void removeSpecialistSkillButton_Click(object sender, EventArgs e)
        {
            CurrentSkill.Specialisations.Remove(CurrentSpecialisationSkill);
            CurrentSpecialisationSkill = null;
            UpdateBoxes();
        }

        private void specialistSkillUpButton_Click(object sender, EventArgs e)
        {
            int currentIndex = specialisationSkillsBox.SelectedIndex;
            // Don't do anything if this is index 0 (top item)
            if (currentIndex > 0)
            {
                CurrentSkill.Specialisations.Reverse(currentIndex - 1, 2);
                UpdateBoxes();
            }
        }

        private void specialistSkillDownButton_Click(object sender, EventArgs e)
        {
            int currentIndex = specialisationSkillsBox.SelectedIndex;
            // Don't do anything if this is the last item
            if (currentIndex < CurrentSkill.Specialisations.Count - 1)
            {
                CurrentSkill.Specialisations.Reverse(currentIndex, 2);
                UpdateBoxes();
            }
        }

        private bool m_suppressSpecialistSkillSelectionUpdate = false;

        private void specialisationSkillsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (specialisationSkillsBox.SelectedItem is TravellerSkill && !m_suppressSpecialistSkillSelectionUpdate)
            {
                CurrentSpecialisationSkill = (specialisationSkillsBox.SelectedItem as TravellerSkill);
                UpdateBoxes();
                specialisationSkillsBox.Focus();
            }
        }

        private void specialisationSkillNameBox_TextChanged(object sender, EventArgs e)
        {
            // Don't update when we're switching away from specialisation
            TravellerSkill targetSkill = skillsBox.SelectedItem as TravellerSkill;
            if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
            {
                CurrentSpecialisationSkill.Name = specialisationSkillNameBox.Text;
                UpdateBoxes();
            }
        }

        private void specialisationSkillSummaryBox_TextChanged(object sender, EventArgs e)
        {
            // Don't update when we're switching away from specialisation
            TravellerSkill targetSkill = skillsBox.SelectedItem as TravellerSkill;
            if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
            {
                CurrentSpecialisationSkill.Summary = specialisationSkillSummaryBox.Text;
            }
        }

        private void specialisationSkillDescriptionBox_TextChanged(object sender, EventArgs e)
        {
            // Don't update when we're switching away from specialisation
            TravellerSkill targetSkill = skillsBox.SelectedItem as TravellerSkill;
            if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
            {
                CurrentSpecialisationSkill.Description = specialisationSkillDescriptionBox.Text;
            }
        }

        private void specialisationSkillRefereeBox_TextChanged(object sender, EventArgs e)
        {
            // Don't update when we're switching away from specialisation
            TravellerSkill targetSkill = skillsBox.SelectedItem as TravellerSkill;
            if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
            {
                CurrentSpecialisationSkill.Referee = specialisationSkillRefereeBox.Text;
            }
        }
    }
}
