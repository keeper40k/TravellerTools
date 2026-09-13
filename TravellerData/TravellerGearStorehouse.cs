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

        private const string GearJsonFile = "gear.json";

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
