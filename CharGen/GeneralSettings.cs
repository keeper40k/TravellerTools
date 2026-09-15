using System;
using System.Windows.Forms;


namespace TravellerTools.CharGen;

/// <summary>Edits character-generation preferences and saves them when closed through the close button.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
public partial class GeneralSettings : Form
{
    // Constructor

    /// <summary>Creates an editor for supplied preferences.</summary>
    /// <param name="settings">The preferences edited by reference. Legacy null input creates defaults and attempts to load settings.json.</param>
    public GeneralSettings(CharGenSettings settings)
    {
        InitializeComponent();
        if (settings == null)
        {
            Settings = new();
            Settings.LoadSettings();
        }
        else
        {
            Settings = settings;
        }

        promptOnNewCheckBox.Checked = Settings.PromptOnNewChar;
        allowAgeEditingBox.Checked = Settings.AllowAgeEditing;
        allowCharacterSurvivalBox.Checked = Settings.AllowCharacterSurvival;
    }

    // Public Methods

    // Persists the shared preferences before dismissing the editor.
    private void closeButton_Click(object sender, EventArgs e)
    {
        Settings.SaveSettings();
        Close();
    }

    // Public Properties

    /// <summary>The preferences edited by the dialog; changes affect the supplied object directly.</summary>
    public CharGenSettings Settings = null!;

    // Updates whether starting a new character prompts before replacing the current one.
    private void promptOnNewCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.PromptOnNewChar = promptOnNewCheckBox.Checked;
    }

    // Updates whether the character's age can be edited manually.
    private void allowAgeEditingBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AllowAgeEditing = allowAgeEditingBox.Checked;
    }

    // Updates whether a failed survival roll can end service with an injury.
    private void allowCharacterSurvival_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AllowCharacterSurvival = allowCharacterSurvivalBox.Checked;
    }
}
