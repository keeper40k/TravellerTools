using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerRetirementPay : TravellerGear
    {

        // private const strings

        private const string RETIREMENT_PAY_NAME = "Retirement Pay";
        private const string TOSTRING_SEPARATOR = ": Cr";

        // Public constructors

        public TravellerRetirementPay()
        {
            Name = RETIREMENT_PAY_NAME;
            Amount = 0;
        }

        // Public Override Methods

        public override string ToString()
        {
            return Name;
        }

        public override string DisplayString()
        {
            return Name + TOSTRING_SEPARATOR + Amount;
        }


        // Public Properties

        public decimal Amount { get; set; }
    }
}
