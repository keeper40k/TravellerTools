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
    public partial class EnlistServiceForm : Form
    {

        // Constructor 

        public EnlistServiceForm( TravellerCharacter character )
        {
            Character = character;
            InitializeComponent();
            RefreshCharacterDisplay();
        }

        // Protected Methods
        protected void RefreshCharacterDisplay()
        {
            characterDisplay.Text = Character.ShortStringFormat();
        }
        
        // Public Properties

        public TravellerCharacter Character;

    }
}
