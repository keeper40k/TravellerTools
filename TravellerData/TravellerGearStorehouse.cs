using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerGearStorehouse
    {

        // static constructors

        static TravellerGearStorehouse()
        {
            Gear = new List<TravellerGear>();
        }

        // static methods

        public static TravellerGear GetGear( string Name, string weaponType )
        {
            // TO DO - this should work better than this!
            TravellerGear result = new TravellerGear();
            result.Name = Name;
            result.WeaponType = weaponType;
            return result;
        }

        // static properties

        static List<TravellerGear> Gear { get; set; }
    }
}
