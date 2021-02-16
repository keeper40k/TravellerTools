using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravellerTools.CharGen
{
    public class CharGenSettings
    {

        // static strings
        private static string SETTINGS_FILE = "settings.json";

        // Constructor

        public CharGenSettings()
        {
            SetDefaults();
        }

        // Public Methods
        public void LoadSettings()
        {
            if (File.Exists(SETTINGS_FILE))
            {
                string json = File.ReadAllText(SETTINGS_FILE);
                CharGenSettings settings = JsonSerializer.Deserialize<CharGenSettings>(json);
                Duplicate( settings );
            }
        }

        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(SETTINGS_FILE, json);
        }

        // Protected Methods

        public void SetDefaults()
        {
            PromptOnNewChar = true;
            AllowAgeEditing = false;
            AllowCharacterSurvival = false;
        }

        protected void Duplicate( CharGenSettings settings )
        {
            PromptOnNewChar = settings.PromptOnNewChar;
            AllowAgeEditing = settings.AllowAgeEditing;
            AllowCharacterSurvival = settings.AllowCharacterSurvival;
        }

        // Public Properties

        public bool PromptOnNewChar { get; set; }
        public bool AllowAgeEditing { get; set; }
        public bool AllowCharacterSurvival { get; set; }
    }
}