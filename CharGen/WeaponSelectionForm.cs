using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen;

/// <summary>Offers a weapon benefit or a related skill benefit based on the character's inventory.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
public partial class WeaponSelectionForm : Form
{
    // Display prompt and the suffix connecting gear categories to combat skills.

    private const string ChoiceLabel = "You have rolled {0}. Please choose on option:";

    private const string CombatExtension = " Combat";

    // Protected Memebers

    /// <summary>The combat skill whose specialisations define the weapon choices.</summary>
    protected TravellerSkill weapon;
    /// <summary>The character inventory used to determine eligibility for a skill benefit.</summary>
    protected List<TravellerGear> currentGear;

    // Public Constructors

    /// <summary>Initializes a chooser for a weapon or related skill benefit.</summary>
    /// <param name="weapon">The non-null combat skill containing available weapon specialisations.</param>
    /// <param name="gear">The non-null character inventory, retained by reference.</param>
    public WeaponSelectionForm(TravellerSkill weapon, List<TravellerGear> gear)
    {
        this.weapon = weapon;
        IsWeaponSelected = true;
        currentGear = gear;

        SelectedGear = null;
        SelectedSkill = null;

        InitializeComponent();
        UpdateBoxes();
    }

    // Protected methods

    /// <summary>Rebuilds benefit choices from weapon specialisations or the matching inventory.</summary>
    protected void UpdateBoxes()
    {
        promptLabel.Text = string.Format(ChoiceLabel, weapon.Name);

        selectButton.Enabled = choicesBox.SelectedItem != null;

        // Can only do the choices box content, after using its info above
        choicesBox.Items.Clear();
        if (IsWeaponSelected)
        {
            foreach (TravellerSkill skill in weapon.Specialisations)
            {
                choicesBox.Items.Add(skill);
            }
        }
        else
        {
            foreach (TravellerGear gear in currentGear)
            {
                string gearToSkill = gear.GearType + CombatExtension;
                if (gearToSkill == weapon.Name)
                {
                    choicesBox.Items.Add(gear);
                }

            }
        }

        if (currentGear.Count == 0 || !GearContainsWeaponType())
        {
            IsWeaponSelected = true;
            skillChoiceBox.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label3.Enabled = false;
        }
        UpdateCheckBoxes();
    }

    /// <summary>Synchronizes the two exclusive benefit modes without re-entering their change handlers.</summary>
    protected void UpdateCheckBoxes()
    {
        suppressCheckChange = true;
        weaponChoiceBox.Checked = IsWeaponSelected;
        skillChoiceBox.Checked = !IsWeaponSelected;
        suppressCheckChange = false;
    }

    /// <summary>Checks whether the inventory contains gear matching the combat skill category.</summary>
    /// <returns>True when a GearType plus ' Combat' exactly matches the weapon skill name.</returns>
    protected bool GearContainsWeaponType()
    {
        bool found = false;
        foreach (TravellerGear gear in currentGear)
        {
            string gearToSkill = gear.GearType + CombatExtension;
            if (gearToSkill == weapon.Name)
            {
                found = true;
                // Once we've found one, that is enough to know
                break;
            }

        }
        return found;
    }

    // Public properties

    /// <summary>Gets or sets whether the chooser awards gear rather than a skill.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsWeaponSelected { get; set; }
    /// <summary>Gets or sets the resolved gear benefit, or null when no gear was resolved.</summary>
    /// <remarks>Selection copies the chosen definition or inventory entry into an independent award.</remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TravellerGear? SelectedGear { get; set; }
    /// <summary>Gets or sets the resolved skill benefit, or null when no skill was resolved.</summary>
    /// <remarks>Selection creates an independent level-one award without modifying the shared definition.</remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TravellerSkill? SelectedSkill { get; set; }

    // Private events

    // Keep programmatic mode changes from triggering a second refresh.
    private bool suppressCheckChange = false;

    // Switches to inventory awards unless controls are being synchronized internally.
    private void weaponChoiceBox_CheckedChanged(object sender, EventArgs e)
    {
        if (!suppressCheckChange)
        {
            IsWeaponSelected = true;
            UpdateBoxes();
        }
    }

    // Switches to skill awards unless controls are being synchronized internally.
    private void skillChoiceBox_CheckedChanged(object sender, EventArgs e)
    {
        if (!suppressCheckChange)
        {
            IsWeaponSelected = false;
            UpdateBoxes();
        }
    }

    // Assumes that choicesBox.SelectedItem isn't null.
    private void selectButton_Click(object sender, EventArgs e)
    {
        if (IsWeaponSelected)
        {
            TravellerSkill? selection = choicesBox.SelectedItem as TravellerSkill;
            if (selection != null)
            {
                SelectedGear = TravellerGearStorehouse.GetGear(selection.Name, weapon.Name);
            }
            else // let's try it as gear instead!
            {
                SelectedGear = choicesBox.SelectedItem as TravellerGear;
            }
            if (SelectedGear != null)
            {
                SelectedGear = BenefitAwards.CreateGear(SelectedGear);
            }
        }
        // So, a skill instead!
        else
        {
            TravellerGear? selected = choicesBox.SelectedItem as TravellerGear;
            if (selected != null)
            {
                SelectedSkill = TravellerSkills.MatchSkill(selected.Name);
            }
            else // let's try it as a skill instead!
            {
                SelectedSkill = choicesBox.SelectedItem as TravellerSkill;
            }
            if (SelectedSkill != null)
            {
                SelectedSkill = BenefitAwards.CreateSkill(SelectedSkill);
            }
        }
        Close();
    }

    // Guard selection restoration while the choices list is rebuilt.
    private bool preventSelectionLoop = false;
    /// <summary>Preserves the selected benefit while rebuilding choices, guarding against recursive selection events.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void choicesBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // The control contains either gear or skill definitions; retain its original item identity.
        object? selection = choicesBox.SelectedItem;
        if (choicesBox.Items.Contains(selection) && !preventSelectionLoop)
        {
            UpdateBoxes();
            preventSelectionLoop = true;
            choicesBox.SelectedItem = selection;
            preventSelectionLoop = false;
        }
    }
}
