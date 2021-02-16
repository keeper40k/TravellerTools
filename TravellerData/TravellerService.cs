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
            Name = string.Empty;
            Enlistment = new TravellerRollTarget(0);
            EnlistmentPlusOne = new TravellerCharacteristicRollTarget(string.Empty, 0);
            EnlistmentPlusTwo = new TravellerCharacteristicRollTarget(string.Empty, 0);
            Survival = new TravellerRollTarget(0);
            SurvivalPlusTwo = new TravellerCharacteristicRollTarget(string.Empty, 0);
            Commission = new TravellerRollTarget(0);
            CommissionPlusOne = new TravellerCharacteristicRollTarget(string.Empty, 0);
            Promotion = new TravellerRollTarget(0);
            PromotionPlusOne = new TravellerCharacteristicRollTarget(string.Empty, 0);
            Reenlist = new TravellerRollTarget(0);
            DraftNumber = 0;
            Ranks = new List<string>();
            AutomaticSkills = new List<KeyValuePair<int, TravellerSkillModifier>>();
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

        public List<TravellerSkillModifier> AutomaticSkillsAtRank (int rankIndex)
        {
            List<TravellerSkillModifier> results = new List<TravellerSkillModifier>();

            foreach(KeyValuePair<int, TravellerSkillModifier> item in AutomaticSkills )
            {
                if( item.Key == rankIndex)
                {
                    results.Add(item.Value);
                }
            }

            return results;
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
        public List<KeyValuePair<int, TravellerSkillModifier>> AutomaticSkills { get; set; }
    }
}
