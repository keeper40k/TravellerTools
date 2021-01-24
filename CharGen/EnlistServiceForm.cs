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

        private static string ENLIST_LABEL = "You need {0}+ on 2d6 to Enlist. You will get a +{1} modifier.";
        private static string ENLIST_SUCCESS = "You successfully enlisted in the {0}, with a total roll of {1}.";
        private static string ENLIST_FAIL = "You failted to enlisted in the {0}, with a total roll of {1}.";

        // Constructor 

        public EnlistServiceForm( TravellerCharacter character )
        {
            Character = character;
            Services = new TravellerServices();
            Services.LoadSettings();
            SelectedService = null;
            InitializeComponent();
            InitialiseInputBoxes();
            UpdateInputBoxes();
            RefreshCharacterDisplay();
        }

        // Protected Methods
        protected void RefreshCharacterDisplay()
        {
            characterDisplay.Text = Character.ShortStringFormat();
            serviceRecommendationsBox.Text = Services.RecommendText( Character );
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

            // OK Button
            // Should be disabled until a Service has been selected.
            // Enable happens in methods enlistButton_Click()
            okButton.Enabled = false;
        }

        protected void UpdateInputBoxes()
        {
        }

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
                string labelText = String.Format(ENLIST_LABEL, target, bonus);
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
                string labelText = String.Format(ENLIST_SUCCESS, SelectedService.Name, result);
                enlistmentResultLabel.Text = labelText;
                okButton.Enabled = true;
            }
            else
            {
                string labelText = String.Format(ENLIST_FAIL, SelectedService.Name, result);
                enlistmentResultLabel.Text = labelText;
                // What happens in the draft ...
            }
        }
    }
}
