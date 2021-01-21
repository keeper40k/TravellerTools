using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravellerTools.CharGen
{
    public class CharGenSettings
    {

        // Constructor

        public CharGenSettings()
        {
            SetDefaults();
        }

        // Public Methods
        public void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                string json = File.ReadAllText("settings.json");
                CharGenSettings settings = JsonSerializer.Deserialize<CharGenSettings>(json);
                Duplicate( settings );
            }
        }

        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText("settings.json", json);
        }

        // Protected Methods

        public void SetDefaults()
        {
            PromptOnNewChar = true;
            AllowReroll = false;
        }

        protected void Duplicate( CharGenSettings settings )
        {
            PromptOnNewChar = settings.PromptOnNewChar;
            AllowReroll = settings.AllowReroll;
        }

        // Public Properties

        public bool PromptOnNewChar { get; set; }
        public bool AllowReroll { get; set; }
    }
}