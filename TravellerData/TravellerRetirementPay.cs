using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerRetirementPay : TravellerGear
    {

        // private const strings

        private const string RetirementPayName = "Retirement Pay";
        private const string ToStringSeparator = ": Cr";

        // Public constructors

        public TravellerRetirementPay()
        {
            Name = RetirementPayName;
            Amount = 0;
        }

        // Public Override Methods

        public override string ToString()
        {
            return Name;
        }

        public override string DisplayString()
        {
            return Name + ToStringSeparator + Amount;
        }


        // Public Properties

        public decimal Amount { get; set; }
    }
}
