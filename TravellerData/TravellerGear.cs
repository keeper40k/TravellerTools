using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerGear
    {
        // private const strings

        private const string COUNT_PREFIX = "{0}x ";

        // Public Constructors

        public TravellerGear()
        {
            Name = string.Empty;
            Description = string.Empty;
            Count = 1;
            WeaponType = string.Empty;
        }

        // Public override methods
        public override string ToString()
        {
            string result = string.Empty;
            if( Count > 1 )
            {
                result += string.Format(COUNT_PREFIX, Count);
            }
            result += Name;

            return result;
        }

        // Public Properties

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Count { get; set; }

        public string WeaponType { get; set; }
    }
}
