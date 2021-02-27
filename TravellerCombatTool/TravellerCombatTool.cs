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

namespace TravellerTools.TravellerCombatTool
{
    public partial class TravellerCombatTool : Form
    {
        // Public Constructors

        public TravellerCombatTool()
        {
            InitializeComponent();

            Portal = null;
        }

        // Protected Methods

        protected void UpdateBoxes()
        {
            if( File.Exists( TerrainFilename ) )
            {
                Image map = Image.FromFile(TerrainFilename);
                if (map != null)
                {
                    heightMapPictureBox.Image = map;
                }
            }
            else
            {
                heightMapPictureBox.Image = null;
            }
        }

        // Public Properties

        public _3DPortal Portal { get; set; }

        public string TerrainFilename { get; set; }

        // Private Event Handlers

        private void open3DButton_Click(object sender, EventArgs e)
        {
            if (Portal == null)
            {
                Portal = new _3DPortal();
                if (Portal != null)
                {
                    Portal.Run();
                }
            }
        }

        private void close3DButton_Click(object sender, EventArgs e)
        {
            if (Portal != null)
            {
                Portal.Exit();
                Portal.Dispose();
                Portal = null;
            }
        }

        private void selectHeightMapButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openDialog.FilterIndex = 1;
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilename = openDialog.FileName;
                // Does this need more checks than this?
                if (File.Exists(selectedFilename))
                {
                    TerrainFilename = selectedFilename;
                }
            }

            UpdateBoxes();
        }
    }
}
