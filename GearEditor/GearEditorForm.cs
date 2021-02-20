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

namespace GearEditor
{
    public partial class GearEditorForm : Form
    {
        // private const strings

        private const string JSON_ARRAY_START = "[";
        private const string JSON_ARRAY_END = "]";
        private const string COMMA = ",";

        private const string NEW_GEAR = "New Gear";

        // Public Constructors

        public GearEditorForm()
        {
            Gear = new List<TravellerGear>();

            InitializeComponent();
            UpdateBoxes();
        }

        // Protected methods

        protected void UpdateBoxes()
        {
            int oldIndex = gearBox.SelectedIndex;

            gearBox.Items.Clear();
            foreach( TravellerGear gear in Gear )
            {
                gearBox.Items.Add(gear);
            }

            if (oldIndex < gearBox.Items.Count)
            {
                m_suppressReselection = true;
                gearBox.SelectedIndex = oldIndex;
                m_suppressReselection = false;
            }
            else if (oldIndex >= gearBox.Items.Count && gearBox.Items.Count > 0)
            {
                m_suppressReselection = true;
                gearBox.SelectedIndex = gearBox.Items.Count - 1;
                m_suppressReselection = false;
            }
            else if (gearBox.Items.Count > 0)
            {
                m_suppressReselection = true;
                gearBox.SelectedIndex = 0;
                m_suppressReselection = false;
            }

            if ( gearBox.Items.Count > 0 )
            {
                gearUpButton.Enabled = gearBox.SelectedIndex != 0;
                gearDownButton.Enabled = gearBox.SelectedIndex != gearBox.Items.Count - 1;
                removeGearButton.Enabled = true;
            }
            else
            {
                gearUpButton.Enabled = false;
                gearDownButton.Enabled = false;
                removeGearButton.Enabled = false;
            }
        }


        // Public Properties

        public List<TravellerGear> Gear { get; set; }

        // private events

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(openDialog.FileName);

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;
                    foreach( JsonElement o in root.EnumerateArray( ) )
                    {
                        string rawText = o.GetProperty("Type").ToString();
                        if (rawText == "TravellerGear" )
                        {
                            Gear.Add(JsonSerializer.Deserialize<TravellerGear>(o.GetRawText()));
                        }
                    }
                }
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
                string json = JSON_ARRAY_START;
                for( int i = 0; i < Gear.Count; i++ )
                {
                    if (Gear[i] is TravellerGear)
                    {
                        TravellerGear gear = Gear[i] as TravellerGear;
                        json += JsonSerializer.Serialize(gear);
                    }

                    if( i != Gear.Count-1 )
                    {
                        json += COMMA;
                    }
                }
                json += JSON_ARRAY_END;
                File.WriteAllText(saveDialog.FileName, json);
            }
        }

        private void gearUpButton_Click(object sender, EventArgs e)
        {
            int currentIndex = gearBox.SelectedIndex;
            // Don't do anything if this is index 0 (top item)
            if (currentIndex > 0)
            {
                Gear.Reverse(currentIndex - 1, 2);
                UpdateBoxes();
                gearBox.SelectedIndex = currentIndex - 1;
            }
        }

        private void gearDownButton_Click(object sender, EventArgs e)
        {
            int currentIndex = gearBox.SelectedIndex;
            // Don't do anything if this is the last item
            if (currentIndex < Gear.Count - 1)
            {
                Gear.Reverse(currentIndex, 2);
                UpdateBoxes();
                gearBox.SelectedIndex = currentIndex + 1;
            }
        }

        bool m_suppressReselection = false;
        private void gearBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_suppressReselection)
            {
                UpdateBoxes();
            }
        }

        private void addGearButton_Click(object sender, EventArgs e)
        {
            TravellerGear newGear = new TravellerGear();
            newGear.Name = NEW_GEAR;
            Gear.Add(newGear);
            UpdateBoxes();
        }

        private void removeGearButton_Click(object sender, EventArgs e)
        {
            Gear.Remove(gearBox.SelectedItem as TravellerGear);
            UpdateBoxes();
        }
    }
}
