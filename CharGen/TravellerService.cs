﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.CharGen
{
    public class TravellerService
    {

        // Constructor
        public TravellerService()
        {
        }

        // Public Methods
        public override string ToString()
        {
            return Name;
        }

        // Public Properties

        public String Name { get; set; }
        public TravellerRollTarget Enlistment { get; set; }
        public TravellerCharacteristicRollTarget EnlistmentPlusOne { get; set; }
        public TravellerCharacteristicRollTarget EnlistmentPlusTwo { get; set; }
        public TravellerRollTarget Survival { get; set; }
        public TravellerCharacteristicRollTarget SurvivalPlusTwo { get; set; }
        public TravellerRollTarget Commission { get; set; }
        public TravellerCharacteristicRollTarget CommissionPlusOne { get; set; }
        public TravellerRollTarget Promotion { get; set; }
        public TravellerCharacteristicRollTarget PromotionPlusOne { get; set; }
        public TravellerRollTarget Reenlist { get; set; }

        public decimal DraftNumber { get; set; }

    }
}
