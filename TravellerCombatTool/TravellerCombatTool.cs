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
    /// <summary>Displays a selected terrain height-map image; 3D rendering is currently disabled.</summary>
    /// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
    public partial class TravellerCombatTool : Form
    {
        // Public Constructors

        /// <summary>Initializes the terrain-image interface without starting a 3D renderer.</summary>
        public TravellerCombatTool()
        {
            InitializeComponent();

            // TODO: Restore 3D portal initialization with a supported rendering dependency.
        }

        // Protected Methods

        /// <summary>Loads the terrain image when the selected file exists, otherwise clears the preview.</summary>
        /// <remarks>Image-loading errors propagate. A loaded image may keep its source file locked until disposed.</remarks>
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

        // TODO: Restore the 3D portal property with a supported rendering dependency.
        // public _3DPortal Portal { get; set; }

        /// <summary>Gets or sets the height-map image path used when the preview is refreshed.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TerrainFilename { get; set; } = string.Empty;

        // Private Event Handlers

        // TODO: Restore the Open 3D action with a supported rendering dependency.
        // private void open3DButton_Click(object sender, EventArgs e)
        // {
        //     if (Portal == null)
        //     {
        //         Portal = new _3DPortal();
        //         if (Portal != null)
        //         {
        //             Portal.Run();
        //         }
        //     }
        // }

        // TODO: Restore the Close 3D action with a supported rendering dependency.
        // private void close3DButton_Click(object sender, EventArgs e)
        // {
        //     if (Portal != null)
        //     {
        //         Portal.Exit();
        //         Portal.Dispose();
        //         Portal = null;
        //     }
        // }

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
