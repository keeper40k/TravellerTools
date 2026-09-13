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
    /// <summary>Presents a modal choice among a skill's specialisations.</summary>
    /// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
    public partial class SelectSkillSpecialisationForm : Form
    {
        private static string SELECTION_LABEL = "Choose Skill Specialisation for {0}";

        // Public constructors

        /// <summary>Creates a specialisation chooser with its first item selected.</summary>
        /// <param name="skillName">The parent skill name shown in the prompt.</param>
        /// <param name="list">The non-null, non-empty list of choices retained by reference in the control.</param>
        /// <remarks>This constructor selects index zero and does not support an empty list.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The list is empty.</exception>
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
        /// <summary>Displays the selected specialisation's summary, or clears it when nothing is selected.</summary>
        protected void UpdateBoxes()
        {
            if (skillSpecialisationsBox.SelectedItem is TravellerSkill )
            {
                specialisationSummaryBox.Text = ((TravellerSkill)skillSpecialisationsBox.SelectedItem).Summary;
            }
            else
            {
                specialisationSummaryBox.Text = string.Empty;
            }
        }

        // public properties

        /// <summary>The current selected skill by reference, or null when the control has no skill selected.</summary>
        public TravellerSkill? SelectedSkill;

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
