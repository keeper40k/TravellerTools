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
            GearType = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Count = 1;
            Value = 0;
            Weight = 0;
            TechLevel = 0;
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

        public string ClassType
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
        public string GearType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Count { get; set; }

        public int Value { get; set; }
        // Weight is measured in grams
        public int Weight { get; set; }
        public decimal TechLevel { get; set; }
    }
}
