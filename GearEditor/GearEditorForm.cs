using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TravellerTools.TravellerData;

namespace GearEditor;

/// <summary>Edits gear definitions and loads or saves their JSON representation.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread. Loading appends supported gear entries, skips unknown ClassType values, and may leave partial additions if a later entry fails. Saving replaces the selected file and preserves properties of the three supported gear types. File and JSON failures propagate.</remarks>
public partial class GearEditorForm : Form
{
    // JSON delimiters, data-file names, and editor labels.

    private const string JsonArrayStart = "[";
    private const string JsonArrayEnd = "]";
    private const string Comma = ",";

    private const string NewGear = "New Gear";

    private const string GearTypesFileName = "gearTypes.json";

    private const string PayValueLabel = "Amount";
    private const string NormalValueLabel = "Value        Cr";

    // Public Constructors

    /// <summary>Initializes the gear editor, its empty inventory, and gear-type choices.</summary>
    public GearEditorForm()
    {
        Gear = new();

        InitializeComponent();
        LoadGearTypes();
        UpdateBoxes();
    }

    // Protected methods

    /// <summary>Refreshes controls for the selected gear subtype and available list operations.</summary>
    protected void UpdateBoxes()
    {
        int oldIndex = gearBox.SelectedIndex;

        gearBox.Items.Clear();
        foreach (TravellerGear gear in Gear)
        {
            gearBox.Items.Add(gear);
        }

        if (oldIndex < gearBox.Items.Count)
        {
            suppressReselection = true;
            gearBox.SelectedIndex = oldIndex;
            suppressReselection = false;
        }
        else if (oldIndex >= gearBox.Items.Count && gearBox.Items.Count > 0)
        {
            suppressReselection = true;
            gearBox.SelectedIndex = gearBox.Items.Count - 1;
            suppressReselection = false;
        }
        else if (gearBox.Items.Count > 0)
        {
            suppressReselection = true;
            gearBox.SelectedIndex = 0;
            suppressReselection = false;
        }

        if (gearBox.Items.Count > 0)
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

        if (gearBox.SelectedItem != null)
        {
            TravellerGear? gear = gearBox.SelectedItem as TravellerGear;

            if (gear == null)
            {
                return;
            }

            gearTypeBox.Enabled = true;
            nameBox.Enabled = true;
            descriptionBox.Enabled = true;
            techLevelBox.Enabled = true;
            weightBox.Enabled = true;
            valueBox.Enabled = true;

            gearTypeBox.Text = gear.GearType;
            nameBox.Text = gear.Name;
            descriptionBox.Text = gear.Description;
            techLevelBox.Value = gear.TechLevel;
            weightBox.Value = gear.Weight;
            valueBox.Value = gear.Value;
        }
        else
        {
            gearTypeBox.Enabled = false;
            nameBox.Enabled = false;
            descriptionBox.Enabled = false;
            techLevelBox.Enabled = false;
            weightBox.Enabled = false;
            valueBox.Enabled = false;

            gearTypeBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            descriptionBox.Text = string.Empty;
            techLevelBox.Value = 0;
            weightBox.Value = 0;
            valueBox.Value = 0;
        }

        if (SelectedGear != null)
        {
            switch (SelectedGear.ClassType)
            {
                case "TravellerRetirementPay":
                    {
                        techLevelBox.Enabled = false;
                        weightBox.Enabled = false;
                        valueBox.Enabled = true;
                        valueLabel.Text = PayValueLabel;
                        break;
                    }
                case "TravellerStarshipBenefit":
                    {
                        techLevelBox.Enabled = false;
                        weightBox.Enabled = false;
                        valueBox.Enabled = false;
                        valueLabel.Text = NormalValueLabel;
                        break;
                    }
                default:
                    {
                        techLevelBox.Enabled = true;
                        weightBox.Enabled = true;
                        valueBox.Enabled = true;
                        valueLabel.Text = NormalValueLabel;
                        break;
                    }
            }
        }
    }

    /// <summary>Populates category choices from gearTypes.json in the current working directory.</summary>
    /// <remarks>The file is required; missing-file, read, and JSON errors propagate. Existing choices are cleared before reading. JSON null gives no choices; null entries are not supported by the control.</remarks>
    protected void LoadGearTypes()
    {
        gearTypeBox.Items.Clear();
        string json = File.ReadAllText(GearTypesFileName);
        List<string> types = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        foreach (string type in types)
        {
            gearTypeBox.Items.Add(type);
        }
    }


    // Public Properties

    /// <summary>Gets or sets the mutable list of gear definitions being edited.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<TravellerGear> Gear { get; set; }

    /// <summary>Gets or sets the item currently displayed by the editor, or null for no selection.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TravellerGear? SelectedGear { get; set; }

    // private events

    // Appends supported gear subtypes from the selected JSON file to the current inventory.
    private void loadButton_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openDialog = new();
        openDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
        openDialog.FilterIndex = 2;
        openDialog.Multiselect = false;

        if (openDialog.ShowDialog() == DialogResult.OK)
        {
            AppendGear(openDialog.FileName, Gear);
        }

        UpdateBoxes();
    }

    /// <summary>Serializes each gear item using its runtime benefit subtype so subtype-specific properties survive export.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void saveButton_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveDialog = new();
        saveDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
        saveDialog.FilterIndex = 2;

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            WriteGear(saveDialog.FileName, Gear);
        }
    }

    /// <summary>Appends supported entries in file order without clearing the supplied list.</summary>
    /// <remarks>Requires an array of objects with ClassType. Unknown types are skipped. Read and parse failures leave the list unchanged; later entry failures retain earlier additions. JSON null is invalid.</remarks>
    private static void AppendGear(string fileName, List<TravellerGear> gearItems)
    {
        string json = File.ReadAllText(fileName);

        using (JsonDocument document = JsonDocument.Parse(json))
        {
            JsonElement root = document.RootElement;
            foreach (JsonElement entry in root.EnumerateArray())
            {
                string classType = entry.GetProperty("ClassType").ToString();
                if (classType == "TravellerGear")
                {
                    TravellerGear? gear = JsonSerializer.Deserialize<TravellerGear>(entry.GetRawText());
                    if (gear != null)
                    {
                        gearItems.Add(gear);
                    }
                }
                else if (classType == "TravellerRetirementPay")
                {
                    TravellerRetirementPay? retirementPay = JsonSerializer.Deserialize<TravellerRetirementPay>(entry.GetRawText());
                    if (retirementPay != null)
                    {
                        gearItems.Add(retirementPay);
                    }
                }
                else if (classType == "TravellerStarshipBenefit")
                {
                    TravellerStarshipBenefit? starshipBenefit = JsonSerializer.Deserialize<TravellerStarshipBenefit>(entry.GetRawText());
                    if (starshipBenefit != null)
                    {
                        gearItems.Add(starshipBenefit);
                    }
                }
            }
        }
    }

    /// <summary>Replaces a file with the current gear list, preserving the three supported types' properties.</summary>
    /// <remarks>Serialization and write failures propagate. No additional validation or transactional write is performed.</remarks>
    private static void WriteGear(string fileName, List<TravellerGear> gearItems)
    {
        string json = JsonArrayStart;
        for (int i = 0; i < gearItems.Count; i++)
        {
            if (gearItems[i] is TravellerRetirementPay)
            {
                TravellerRetirementPay? gear = gearItems[i] as TravellerRetirementPay;
                json += JsonSerializer.Serialize(gear);
            }
            else if (gearItems[i] is TravellerStarshipBenefit)
            {
                TravellerStarshipBenefit? gear = gearItems[i] as TravellerStarshipBenefit;
                json += JsonSerializer.Serialize(gear);
            }
            else if (gearItems[i] is TravellerGear)
            {
                TravellerGear? gear = gearItems[i] as TravellerGear;
                json += JsonSerializer.Serialize(gear);
            }

            if (i != gearItems.Count - 1)
            {
                json += Comma;
            }
        }
        json += JsonArrayEnd;
        File.WriteAllText(fileName, json);
    }

    // Moves the selected item earlier in the inventory's saved order.
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

    // Moves the selected item later in the inventory's saved order.
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

    // Avoid refreshing the editor recursively during programmatic reselection.
    private bool suppressReselection = false;
    /// <summary>Updates the active gear item while avoiding a recursive refresh during programmatic reselection.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void gearBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedGear = gearBox.SelectedItem as TravellerGear;
        if (!suppressReselection)
        {
            UpdateBoxes();
        }
    }

    // Adds an unnamed inventory definition and refreshes the available choices.
    private void addGearButton_Click(object sender, EventArgs e)
    {
        TravellerGear newGear = new();
        newGear.Name = NewGear;
        Gear.Add(newGear);
        UpdateBoxes();
    }

    // Removes the selected inventory definition and refreshes the editor.
    private void removeGearButton_Click(object sender, EventArgs e)
    {
        TravellerGear? selectedGear = gearBox.SelectedItem as TravellerGear;
        if (selectedGear != null)
        {
            Gear.Remove(selectedGear);
        }
        UpdateBoxes();
    }

    // Stores the category used for gear grouping and combat-skill matching.
    private void gearTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectedGear != null)
        {
            SelectedGear.GearType = gearTypeBox.Text;
        }
    }

    // Refreshes inventory labels after the selected item's name changes.
    private void nameBox_TextChanged(object sender, EventArgs e)
    {
        if (SelectedGear != null)
        {
            SelectedGear.Name = nameBox.Text;
        }
        UpdateBoxes();
    }

    // Stores rules text on the active inventory definition.
    private void descriptionBox_TextChanged(object sender, EventArgs e)
    {
        if (SelectedGear != null)
        {
            SelectedGear.Description = descriptionBox.Text;
        }
    }

    // Retains the editor's legacy conversion of technology level to a whole number.
    private void techLevelBox_ValueChanged(object sender, EventArgs e)
    {
        if (SelectedGear != null)
        {
            SelectedGear.TechLevel = (int)techLevelBox.Value;
        }
    }

    // Stores the entered weight as an integer number of grams.
    private void weightBox_ValueChanged(object sender, EventArgs e)
    {
        if (SelectedGear != null)
        {
            SelectedGear.Weight = (int)weightBox.Value;
        }
    }

    // Stores the entered item value as an integer number of credits.
    private void valueBox_ValueChanged(object sender, EventArgs e)
    {
        if (SelectedGear != null)
        {
            SelectedGear.Value = (int)valueBox.Value;
        }
    }
}
