using System;
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
    public partial class SelectSkillSpecialisationForm : Form
    {
        private static string SELECTION_LABEL = "Choose Skill Specialisation for {0}";

        // Public constructors

        public SelectSkillSpecialisationForm( string skillName, List<TravellerSkill> list )
        {
            InitializeComponent();

            chooseLabel.Text = string.Format(SELECTION_LABEL, skillName);
            skillSpecialisationsBox.Items.Clear();
            foreach (TravellerSkill skill in list)
            {
                skillSpecialisationsBox.Items.Add(skill);
            }
            skillSpecialisationsBox.SelectedIndex = 0;
            if( skillSpecialisationsBox.SelectedItem is TravellerSkill )
            {
                SelectedSkill = skillSpecialisationsBox.SelectedItem as TravellerSkill;
            }
            else
            {
                SelectedSkill = null;
            }
            UpdateBoxes();
        }

        // Protected methods
        protected void UpdateBoxes()
        {
            if (skillSpecialisationsBox.SelectedItem is TravellerSkill )
            {
                specialisationSummaryBox.Text = (skillSpecialisationsBox.SelectedItem as TravellerSkill).Summary;
            }
            else
            {
                specialisationSummaryBox.Text = string.Empty;
            }
        }

        // public properties

        public TravellerSkill SelectedSkill;

        // Private events handlers

        private void skillSpecialisationsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillSpecialisationsBox.SelectedItem is TravellerSkill)
            {
                SelectedSkill = skillSpecialisationsBox.SelectedItem as TravellerSkill;
            }
            else
            {
                SelectedSkill = null;
            }
            
            UpdateBoxes();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
