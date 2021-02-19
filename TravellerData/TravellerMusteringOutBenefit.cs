using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerMusteringOutBenefit
    {
        // Public Constructor

        public TravellerMusteringOutBenefit()
        {
            Name = string.Empty;
            IsAtt = false;
            IsGear = false;
        }

        // Public override methods

        public override string ToString()
        {
            return Name;
        }

        // Public Properties

        public string Name { get; set; }
        public bool IsAtt { get; set; }
        public bool IsGear { get; set; }
    }
}
