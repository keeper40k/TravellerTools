using System.IO;
using System.Text.Json;

namespace TravellerTools.CharGen;

/// <summary>Stores and persists character-generation preferences.</summary>
public class CharGenSettings
{

    // Preferences are resolved against the caller's current working directory.
    private const string SettingsFileName = "settings.json";

    // Constructor

    /// <summary>Creates preferences with confirmation enabled and age editing and survival overrides disabled.</summary>
    public CharGenSettings()
    {
        SetDefaults();
    }

    // Public Methods
    /// <summary>Loads preferences from settings.json in the current working directory.</summary>
    /// <remarks>A missing file or JSON null leaves current preferences unchanged. A JSON object replaces all three flags; omitted fields use constructor defaults rather than existing values. Property names are case-sensitive and unknown properties are ignored. Read or deserialization failures propagate before any flags are copied.</remarks>
    /// <exception cref="System.UnauthorizedAccessException">Access to the settings file is denied.</exception>
    /// <exception cref="System.IO.IOException">The file cannot be read.</exception>
    /// <exception cref="System.Text.Json.JsonException">The contents cannot be deserialized.</exception>
    public void LoadSettings()
    {
        if (File.Exists(SettingsFileName))
        {
            string json = File.ReadAllText(SettingsFileName);
            CharGenSettings? settings = JsonSerializer.Deserialize<CharGenSettings>(json);
            if (settings != null)
            {
                Duplicate(settings);
            }
        }
    }

    /// <summary>Writes preferences to settings.json in the current working directory, replacing any existing file.</summary>
    /// <remarks>Uses settings.json in the current working directory. File-system and serialization errors propagate; the write is not transactional.</remarks>
    /// <exception cref="System.UnauthorizedAccessException">Access to the settings file is denied.</exception>
    /// <exception cref="System.IO.IOException">The file cannot be written.</exception>
    public void SaveSettings()
    {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(SettingsFileName, json);
    }

    // Protected Methods

    /// <summary>Enables new-character confirmation and disables age editing and survival overrides.</summary>
    public void SetDefaults()
    {
        PromptOnNewChar = true;
        AllowAgeEditing = false;
        AllowCharacterSurvival = false;
    }

    /// <summary>Copies the three preference flags from another settings object.</summary>
    /// <param name="settings">The non-null preferences to copy.</param>
    protected void Duplicate(CharGenSettings settings)
    {
        PromptOnNewChar = settings.PromptOnNewChar;
        AllowAgeEditing = settings.AllowAgeEditing;
        AllowCharacterSurvival = settings.AllowCharacterSurvival;
    }

    // Public Properties

    /// <summary>Gets or sets whether creating a new character requires confirmation.</summary>
    public bool PromptOnNewChar { get; set; }
    /// <summary>Gets or sets whether the age control permits manual editing.</summary>
    public bool AllowAgeEditing { get; set; }
    /// <summary>Gets or sets whether a failed survival roll may cause injury and discharge instead of death.</summary>
    public bool AllowCharacterSurvival { get; set; }
}
