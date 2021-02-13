using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerSkill
    {
        // Public Constructors
        public TravellerSkill()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        // Public Methods

        public override string ToString()
        {
            return Name;
        }

        // Public Properties

        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Referee { get; set; }
        public decimal Level { get; set; }
    }
}
