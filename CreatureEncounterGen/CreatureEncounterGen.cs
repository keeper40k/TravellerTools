using System;
using System.Windows.Forms;

namespace CreatureEncounterGen;

/// <summary>Hosts the creature encounter generator's Windows Forms interface.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
public partial class CreatureEncounterGen : Form
{
    /// <summary>Initializes the encounter-generator form and its controls.</summary>
    public CreatureEncounterGen()
    {
        InitializeComponent();
    }

    /// <summary>Receives the form-load event; encounter initialization is not implemented here.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void CreatureEncounterGen_Load(object sender, EventArgs e)
    {

    }
}
