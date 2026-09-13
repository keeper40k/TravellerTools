using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TravellerTools.TravellerData
{
    /// <summary>Provides shared gear definitions loaded from gear.json in the current working directory.</summary>
    /// <remarks>The initial load occurs once during type initialization. Lookups return mutable shared objects. The loader recognizes TravellerGear, TravellerRetirementPay, and TravellerStarshipBenefit ClassType values and skips other types.</remarks>
    public class TravellerGearStorehouse
    {
        // private const strings

        private const string GearJsonFile = "gear.json";

        // static Constructors

        static TravellerGearStorehouse()
        {
            Gear = new List<TravellerGear>();
            LoadGear();
        }

        // static Protected Methods

        /// <summary>Clears and reloads gear definitions from gear.json in the current working directory.</summary>
        /// <remarks>Each JSON entry must provide ClassType. Read or parse failures propagate and may leave the collection empty or partially populated.</remarks>
        /// <exception cref="System.IO.IOException">The file cannot be read.</exception>
        /// <exception cref="System.Text.Json.JsonException">The JSON cannot be parsed or converted.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">An entry has no ClassType property.</exception>
        static protected void LoadGear()
        {
            Gear.Clear();
            string json = File.ReadAllText(GearJsonFile);

            using (JsonDocument document = JsonDocument.Parse(json))
            {
                JsonElement root = document.RootElement;
                foreach (JsonElement o in root.EnumerateArray())
                {
                    string rawText = o.GetProperty("ClassType").ToString();
                    if (rawText == "TravellerGear")
                    {
                        TravellerGear? gear = JsonSerializer.Deserialize<TravellerGear>(o.GetRawText());
                        if (gear != null)
                        {
                            Gear.Add(gear);
                        }
                    }
                    else if (rawText == "TravellerRetirementPay")
                    {
                        TravellerRetirementPay? retirementPay = JsonSerializer.Deserialize<TravellerRetirementPay>(o.GetRawText());
                        if (retirementPay != null)
                        {
                            Gear.Add(retirementPay);
                        }
                    }
                    else if (rawText == "TravellerStarshipBenefit")
                    {
                        TravellerStarshipBenefit? starshipBenefit = JsonSerializer.Deserialize<TravellerStarshipBenefit>(o.GetRawText());
                        if (starshipBenefit != null)
                        {
                            Gear.Add(starshipBenefit);
                        }
                    }
                }
            }
        }

        // static Public Methods

        /// <summary>Finds the first gear definition matching a name and optional weapon category.</summary>
        /// <param name="Name">The exact, case-sensitive gear name.</param>
        /// <param name="weaponType">The required GearType followed by ' Combat'; an empty or null string disables this filter.</param>
        /// <returns>The matching shared gear object, or null.</returns>
        /// <remarks>No copy is made.</remarks>
        /// <exception cref="TypeInitializationException">The initial gear.json load failed.</exception>
        public static TravellerGear? GetGear( string Name, string weaponType )
        {
            TravellerGear? result = null;
            foreach( TravellerGear gear in Gear )
            {
                bool matchesName = gear.Name == Name;
                bool matchesWeaponType = string.IsNullOrEmpty(weaponType) || gear.GearType + " Combat" == weaponType;
                if( matchesName && matchesWeaponType )
                {
                    result = gear;
                    break;
                }
            }
            return result;
        }

        // static properties

        static List<TravellerGear> Gear { get; set; }
    }
}
