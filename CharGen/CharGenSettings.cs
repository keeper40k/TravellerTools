using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravellerTools.CharGen
{
    /// <summary>Stores and persists character-generation preferences.</summary>
    public class CharGenSettings
    {

        // static strings
        private static string SETTINGS_FILE = "settings.json";

        // Constructor

        /// <summary>Creates preferences with confirmation enabled and age editing and survival overrides disabled.</summary>
        public CharGenSettings()
        {
            SetDefaults();
        }

        // Public Methods
        /// <summary>Loads preferences from settings.json in the current working directory.</summary>
        /// <remarks>A missing file or JSON null leaves current preferences unchanged; file-system and JSON errors propagate.</remarks>
        /// <exception cref="System.IO.IOException">The file cannot be read.</exception>
        /// <exception cref="System.Text.Json.JsonException">The contents cannot be deserialized.</exception>
        public void LoadSettings()
        {
            if (File.Exists(SETTINGS_FILE))
            {
                string json = File.ReadAllText(SETTINGS_FILE);
                CharGenSettings? settings = JsonSerializer.Deserialize<CharGenSettings>(json);
                if (settings != null)
                {
                    Duplicate(settings);
                }
            }
        }

        /// <summary>Writes preferences to settings.json in the current working directory, replacing any existing file.</summary>
        /// <remarks>File-system and serialization errors propagate.</remarks>
        /// <exception cref="System.IO.IOException">The file cannot be written.</exception>
        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(SETTINGS_FILE, json);
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
        protected void Duplicate( CharGenSettings settings )
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
}
