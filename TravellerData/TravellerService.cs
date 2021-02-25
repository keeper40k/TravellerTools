using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
{
    public class TravellerService
    {
        // private constant strings

        private const string SKILL_ROW = "{0}    {1}\n";
        private const string ATT_ROW = "{0}    {1}{2} {3}\n";
        private const string BENEFIT_ROW = "{0}    {1}\n";
        private const string CASH_ROW = "{0}    Cr{1}\n";

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
            SkillsPerTerm = 0;
            HasRetirementPay = true;
            CashTable = new List<KeyValuePair<int, decimal>>();
            BenefitsTable = new List<KeyValuePair<int, TravellerMusteringOutBenefit>>();
        }

        // Protected Methods

        protected string TableText( List<KeyValuePair<int, TravellerSkillModifier>> table )
        {
            string result = string.Empty;
            foreach (KeyValuePair<int, TravellerSkillModifier> row in table)
            {
                TravellerSkillModifier thisSkill = (row.Value as TravellerSkillModifier);
                if (thisSkill != null)
                {
                    if (thisSkill.IsSkill)
                    {
                        result += string.Format(SKILL_ROW, row.Key, thisSkill.Name);
                    }
                    else if (thisSkill.IsAttribute)
                    {
                        string modifier = "+";
                        if (thisSkill.Level < 0)
                        {
                            modifier = "-";
                        }
                        result += string.Format(ATT_ROW, row.Key, modifier, thisSkill.Level, thisSkill.Name);
                    }
                }
            }
            // Remove the last \n character
            result = result.Substring(0, result.Length - 1);
            return result;
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

        public string PersonalDevelopmentTableText()
        {
            return TableText( PersonalDevelopmentTable );
        }

        public string ServiceSkillsTableText()
        {
            return TableText( ServiceSkillsTable );
        }

        public string AdvancedEducationTableText()
        {
            return TableText( AdvancedEducationTable );
        }

        public string AdvancedEducationTable2Text()
        {
            return TableText( AdvancedEducationTable2 );
        }

        public string CashTableText()
        {
            string result = string.Empty;
            foreach( KeyValuePair<int, decimal> item in CashTable )
            {
                result += string.Format(CASH_ROW, item.Key, item.Value);
            }
            // Remove the last \n character
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public string BenefitsTableText()
        {
            string result = string.Empty;
            foreach ( KeyValuePair<int, TravellerMusteringOutBenefit> item in BenefitsTable )
            {
                result += string.Format(BENEFIT_ROW, item.Key, item.Value.Name);
            }
            // Remove the last \n character
            result = result.Substring(0, result.Length - 1);
            return result;
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
        public decimal SkillsFirstTerm { get; set; }
        public decimal SkillsPerTerm { get; set; }
        public List<KeyValuePair<int, TravellerSkillModifier>> PersonalDevelopmentTable { get; set; }
        public List<KeyValuePair<int, TravellerSkillModifier>> ServiceSkillsTable { get; set; }
        public List<KeyValuePair<int, TravellerSkillModifier>> AdvancedEducationTable { get; set; }
        public List<KeyValuePair<int, TravellerSkillModifier>> AdvancedEducationTable2 { get; set; }
        public List<KeyValuePair<int, decimal>> CashTable { get; set; }
        public List<KeyValuePair<int, TravellerMusteringOutBenefit>> BenefitsTable { get; set; }
        public bool HasRetirementPay { get; set; }
    }
}
