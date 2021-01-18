using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TravellerTools.CharGen
{
    public partial class GeneralSettings : Form
    {
        public GeneralSettings()
        {
            InitializeComponent();
            LoadSettings();
        }

        // Protected Methods

        protected void LoadSettings()
        {

        }

        protected void SaveSettings()
        {
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

    }
}
