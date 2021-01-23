using System;
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

        // Properties

        public TravellerRollTarget Enlistment { get; set; }
        public TravellerCharacteristicRollTarget EnlistmentPlusOne { get; set; }
        public TravellerCharacteristicRollTarget EnlistmentPlusTwo { get; set; }
        public TravellerRollTarget Survial { get; set; }
        public TravellerCharacteristicRollTarget SurvivalPlusTwo { get; set; }
        public TravellerRollTarget Commission { get; set; }
        public TravellerCharacteristicRollTarget CommissionPlusOne { get; set; }
        public TravellerRollTarget Promotion { get; set; }
        public TravellerCharacteristicRollTarget PromotionPlusOne { get; set; }
        public TravellerRollTarget Reenlist { get; set; }

    }
}
