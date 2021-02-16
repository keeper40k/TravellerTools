using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerGear
    {
        // Public Constructors

        public TravellerGear()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
