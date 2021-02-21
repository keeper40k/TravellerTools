using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TravellerTools.TravellerData
{
    public class TravellerGearStorehouse
    {
        // private const strings

        private const string GEAR_JSON_FILE = "gear.json";

        // static Constructors

        static TravellerGearStorehouse()
        {
            Gear = new List<TravellerGear>();
            LoadGear();
        }

        // static Protected Methods

        static protected void LoadGear()
        {
            Gear.Clear();
            string json = File.ReadAllText(GEAR_JSON_FILE);

            using (JsonDocument document = JsonDocument.Parse(json))
            {
                JsonElement root = document.RootElement;
                foreach (JsonElement o in root.EnumerateArray())
                {
                    string rawText = o.GetProperty("ClassType").ToString();
                    if (rawText == "TravellerGear")
                    {
                        Gear.Add(JsonSerializer.Deserialize<TravellerGear>(o.GetRawText()));
                    }
                }
            }
        }

        // static Public Methods

        public static TravellerGear GetGear( string Name, string weaponType )
        {
            // TO DO - this should work better than this!
            TravellerGear result = null;
            foreach( TravellerGear gear in Gear )
            {
                if( gear.Name == Name )
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
