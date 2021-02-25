using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerStarshipBenefit : TravellerGear
    {
        // Private Const Strings

        private const string MORTGAGE_STRING = " ({0} year mortgage remaining)";

        // Public Constructor

        public TravellerStarshipBenefit()
        {
            MortgageDuration = 40;
        }

        // Public Override Methods

        public override string DisplayString()
        {
            string result = Name;
            if( Name != "Scout Ship" )
            {
                result += string.Format(MORTGAGE_STRING, MortgageDuration);
            }
            return result;
        }

        // Public Properties

        public int MortgageDuration { get; set; }
    }
}
