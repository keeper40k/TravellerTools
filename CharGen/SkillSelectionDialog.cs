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
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen
{
    public partial class SkillSelectionDialog : Form, ISkillSpecialisationCollection
    {
        // static constant strings
        private string SKILL_LABEL = "Skill Selections Remaining ... {0}";

        // protected members

        protected TravellerService service;
        protected TravellerCharacter character;
        protected decimal skillCount;

        // Public Constructors

        public SkillSelectionDialog( TravellerService service, TravellerCharacter character, decimal skillCount )
        {
            this.service = service;
            this.character = character;
            this.skillCount = skillCount;
            InitializeComponent();
            UpdateButtons();
        }

        protected void UpdateButtons()
        {
            if (skillCount == 0)
            {
                // If nothing left to do, close the form
                Close();
            }
            else
            {
                skillsRemainingLabel.Text = string.Format(SKILL_LABEL, skillCount);
                characterDisplay.Text = character.ShortStringFormat();
                skillTable1Button.Text = service.PersonalDevelopmentTableText();
                skillTable2Button.Text = service.ServiceSkillsTableText();
                skillTable3Button.Text = service.AdvancedEducationTableText();
                skillTable4Button.Text = service.AdvancedEducationTable2Text();

                // Only enabled the 2nd Advanced Education Table, if the character's education is 8 or more
                skillTable4Button.Enabled = character.EDU > 7;
            }
        }

        protected void ProcessSkillSelection(List<KeyValuePair<int, TravellerSkillModifier>> table)
        {
            int roll = DiceTools.RollOneDie(6);
            TravellerSkillModifier? rolledSkill = null;
            foreach( KeyValuePair<int, TravellerSkillModifier> item in table )
            {
                if( roll == item.Key )
                {
                    rolledSkill = item.Value;
                    break;
                }
            }
            if( rolledSkill != null )
            {
                character.AddSkill(rolledSkill, this);
                skillCount--;
            }
            UpdateButtons();
        }

        // Implementation of ISkillSpecialisationCollection

        public TravellerSkill SelectSpecialisation(string skillName, List<TravellerSkill> list)
        {
            SelectSkillSpecialisationForm form = new SelectSkillSpecialisationForm(skillName, list);
            form.ShowDialog();
            TravellerSkill? selectedSkill = form.SelectedSkill;
            if( selectedSkill != null && selectedSkill.HasSpecialisations )
            {
                SelectSpecialisation(selectedSkill.Name, selectedSkill.Specialisations);
            }

            return selectedSkill!;
        }

        // Private Event Handlers

        private void skillTable1Button_Click(object sender, EventArgs e)
        {
            ProcessSkillSelection( service.PersonalDevelopmentTable );
        }

        private void skillTable2Button_Click(object sender, EventArgs e)
        {
            ProcessSkillSelection( service.ServiceSkillsTable );
        }

        private void skillTable3Button_Click(object sender, EventArgs e)
        {
            ProcessSkillSelection( service.AdvancedEducationTable );
        }

        private void skillTable4Button_Click(object sender, EventArgs e)
        {
            ProcessSkillSelection( service.AdvancedEducationTable2 );
        }
    }
}
