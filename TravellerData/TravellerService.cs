using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
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

        public bool UsesRanks
        {
            get
            {
                return Ranks.Count > 0;
            }
        }

        public string RankName(int rankIndex)
        {
            if(rankIndex < 0 || rankIndex >= Ranks.Count )
            {
                throw new ArgumentOutOfRangeException("rankIndex", "Parameter rank must be within the range of available ranks.");
            }
            return Ranks[rankIndex];
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
        public List<string> Ranks { get; set; }

    }
}
