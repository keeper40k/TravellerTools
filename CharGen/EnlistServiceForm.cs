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
    public partial class EnlistServiceForm : Form
    {
        // Static Strings

        private static string ENLIST_LABEL = "{0} needs {1}+ on 2d6 to Enlist. You will get a +{2} modifier.";
        private static string ENLIST_SUCCESS = "{0} successfully enlisted in the {1}, with a total roll of {2}.";
        private static string ENLIST_FAIL = "{0} failed to enlisted in the {1}, with a total roll of {2}.";
        private static string DRAFT_RESULT = "{0} has been drafted into the {1}.";

        private static string SERVICE_ENTERED = "Service Entered:";

        // Constructor 

        public EnlistServiceForm( TravellerCharacter character )
        {
            Character = character;
            Services = new TravellerServices();
            Services.LoadSettings();
            SelectedService = null;
            TermsHistoryText = string.Empty;
            InitializeComponent();
            InitialiseInputBoxes();
            UpdateInputBoxes();
            RefreshCharacterDisplay();
        }

        // Protected Methods
        protected void RefreshCharacterDisplay()
        {
            characterDisplay.Text = Character.ShortStringFormat();
            recommendationsBox.Text = Services.RecommendText(Character);
        }

        // Some of the input boxes won't change content, so they only need initialising once.
        protected void InitialiseInputBoxes()
        {
            enlistmentChoiceBox.Items.Clear();
            enlistmentChoiceBox.Items.Add( " -- Choose a Service --" );
            foreach (TravellerService service in Services.Services)
            {
                enlistmentChoiceBox.Items.Add(service);
            }
            enlistmentChoiceBox.SelectedItem = enlistmentChoiceBox.Items[0];
            enlistTargetLabel.Text = string.Empty;
            enlistButton.Enabled = false;
            enlistmentResultLabel.Text = string.Empty;
            draftResultLabel.Text = string.Empty;

            // OK Button
            // Should be disabled until character creation is complete
            okButton.Enabled = false;

        }

        protected void UpdateInputBoxes()
        {
        }

        protected void ExecuteDraft()
        {
            Character.Drafted = true;
            if (enlistmentChoiceBox.SelectedItem is TravellerService)
            {
                Character.FailedService = (enlistmentChoiceBox.SelectedItem as TravellerService).Name;
            }

            decimal draftResult = DiceTools.RollOneDie(6);
            for( int i = 0; i < Services.Services.Count; i++ )
            {
                if (Services.Services[i].DraftNumber == draftResult)
                {
                    Character.Service = Services.Services[i].Name;
                    enlistmentChoiceBox.SelectedItem = Services.Services[i];
                    break;
                }
            }
            if (Character.Service != String.Empty)
            {
                string resultString = String.Format(DRAFT_RESULT, Character.Name, Character.Service);
                draftResultLabel.Text = resultString;
            }
        }

        // Protected Properties
        protected string TermsHistoryText;

        // Public Properties

        public TravellerServices Services;
        public TravellerCharacter Character;
        public TravellerService SelectedService;

        // Private Event Handlers
        private void enlistmentChoiceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // There is a string in the list, if this is chosen, then the
            // SelctedService should revert to null
            if (enlistmentChoiceBox.SelectedItem is TravellerService)
            {
                SelectedService = enlistmentChoiceBox.SelectedItem as TravellerService;
                decimal target = SelectedService.Enlistment.Target;
                decimal bonus = 0;
                if ( SelectedService.EnlistmentPlusTwo.Pass( Character ) )
                {
                    bonus += 2;
                }
                if ( SelectedService.EnlistmentPlusOne.Pass( Character ) )
                {
                    bonus += 1;
                }
                string labelText = String.Format(ENLIST_LABEL, Character.Name, target, bonus);
                enlistTargetLabel.Text = labelText;
                enlistButton.Enabled = true;
            }
            else
            {
                SelectedService = null;
                enlistButton.Enabled = false;
            }

            UpdateInputBoxes();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Update the Character with all of the choices made
            if (SelectedService is TravellerService)
            {
                Character.Service = SelectedService.ToString();
            }
            this.Close();
        }

        private void enlistButton_Click(object sender, EventArgs e)
        {
            decimal target = SelectedService.Enlistment.Target;
            decimal bonus = 0;
            if (SelectedService.EnlistmentPlusTwo.Pass(Character))
            {
                bonus += 2;
            }
            if (SelectedService.EnlistmentPlusOne.Pass(Character))
            {
                bonus += 1;
            }

            decimal roll = DiceTools.RollDice(2, 6);
            decimal result = roll + bonus;
            bool enlist = (result >= SelectedService.Enlistment.Target);
            if (enlist)
            {
                string labelText = String.Format(ENLIST_SUCCESS, Character.Name, SelectedService.Name, result);
                enlistmentResultLabel.Text = labelText;
            }
            else
            {
                string labelText = String.Format(ENLIST_FAIL, Character.Name, SelectedService.Name, result);
                enlistmentResultLabel.Text = labelText;
                // What happens in the draft ...
                ExecuteDraft();
            }

            Character.CreationHistory += enlistmentResultLabel.Text + "\n";
            if( Character.Drafted )
            {
                Character.CreationHistory += draftResultLabel.Text + "\n";
            }
            Character.CreationHistory += "\n";

            enlistmentChoiceLabel.Text = SERVICE_ENTERED;
            enlistmentChoiceBox.Enabled = false;
            enlistButton.Enabled = false;
            okButton.Enabled = true;
        }
    }
}
