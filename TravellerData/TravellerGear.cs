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
            Value = 0;
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

        public string Type
        {
            get
            {
                return this.GetType().Name;
            }
            set
            {
                // Do nothing, except ensure it is serialised by having both get and set.
            }
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Count { get; set; }

        public string WeaponType { get; set; }

        public int Value { get; set; }
    }
}
