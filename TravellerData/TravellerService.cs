using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
{
    /// <summary>Stores mutable career targets, ranks, skill tables, and mustering-out benefits.</summary>
    /// <remarks>Collection keys are roll totals or rank indices as appropriate. Collections are not automatically validated for completeness or duplicate keys.</remarks>
    public class TravellerService
    {
        // private constant strings

        private const string SkillRowFormat = "{0}    {1}\n";
        private const string AttributeRowFormat = "{0}    {1}{2} {3}\n";
        private const string BenefitRowFormat = "{0}    {1}\n";
        private const string CashRowFormat = "{0}    Cr{1}\n";

        // Constructor
        /// <summary>Creates an unnamed service with zero targets, empty tables, and retirement pay enabled.</summary>
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
            PersonalDevelopmentTable = new List<KeyValuePair<int, TravellerSkillModifier>>();
            ServiceSkillsTable = new List<KeyValuePair<int, TravellerSkillModifier>>();
            AdvancedEducationTable = new List<KeyValuePair<int, TravellerSkillModifier>>();
            AdvancedEducationTable2 = new List<KeyValuePair<int, TravellerSkillModifier>>();
            CashTable = new List<KeyValuePair<int, decimal>>();
            BenefitsTable = new List<KeyValuePair<int, TravellerMusteringOutBenefit>>();
        }

        // Protected Methods

        /// <summary>Formats a skill or characteristic-adjustment table for display.</summary>
        /// <param name="table">The non-null list of roll totals and adjustments.</param>
        /// <returns>Rows in list order separated by line feeds, without a final line feed; empty when no rows are formatted.</returns>
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
                        result += string.Format(SkillRowFormat, row.Key, thisSkill.Name);
                    }
                    else if (thisSkill.IsAttribute)
                    {
                        string modifier = "+";
                        if (thisSkill.Level < 0)
                        {
                            modifier = "-";
                        }
                        result += string.Format(AttributeRowFormat, row.Key, modifier, thisSkill.Level, thisSkill.Name);
                    }
                }
            }
            // Remove the last \n character
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        // Public Methods

        /// <summary>Formats the service for selection lists.</summary>
        /// <returns>The service name.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>Gets whether the rank-name list contains any entries.</summary>
        public bool UsesRanks
        {
            get
            {
                return Ranks.Count > 0;
            }
        }

        /// <summary>Returns the displayed rank name at a zero-based index.</summary>
        /// <param name="rankIndex">An index from zero through Ranks.Count minus one.</param>
        /// <returns>The rank name.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The index is negative or outside the rank list.</exception>
        public string RankName(int rankIndex)
        {
            if(rankIndex < 0 || rankIndex >= Ranks.Count )
            {
                throw new ArgumentOutOfRangeException("rankIndex", "Parameter rank must be within the range of available ranks.");
            }
            return Ranks[rankIndex];
        }

        /// <summary>Collects the automatic adjustments keyed to a rank.</summary>
        /// <param name="rankIndex">The rank key to match; a missing key is allowed.</param>
        /// <returns>A new list containing references to every matching adjustment, or an empty list.</returns>
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

        /// <summary>Formats the personal development table for display.</summary>
        /// <returns>Rows in list order separated by line feeds, without a trailing line feed; empty for an empty table.</returns>
        public string PersonalDevelopmentTableText()
        {
            return TableText( PersonalDevelopmentTable );
        }

        /// <summary>Formats the service skills table for display.</summary>
        /// <returns>Rows in list order separated by line feeds, without a trailing line feed; empty for an empty table.</returns>
        public string ServiceSkillsTableText()
        {
            return TableText( ServiceSkillsTable );
        }

        /// <summary>Formats the advanced education table for display.</summary>
        /// <returns>Rows in list order separated by line feeds, without a trailing line feed; empty for an empty table.</returns>
        public string AdvancedEducationTableText()
        {
            return TableText( AdvancedEducationTable );
        }

        /// <summary>Formats the second advanced education table for display.</summary>
        /// <returns>Rows in list order separated by line feeds, without a trailing line feed; empty for an empty table.</returns>
        public string AdvancedEducationTable2Text()
        {
            return TableText( AdvancedEducationTable2 );
        }

        /// <summary>Formats the mustering-out cash table for display.</summary>
        /// <returns>Rows in list order separated by line feeds, without a trailing line feed; empty for an empty table.</returns>
        public string CashTableText()
        {
            string result = string.Empty;
            foreach( KeyValuePair<int, decimal> item in CashTable )
            {
                result += string.Format(CashRowFormat, item.Key, item.Value);
            }
            // Remove the last \n character
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        /// <summary>Formats the mustering-out benefits table for display.</summary>
        /// <returns>Rows in list order separated by line feeds, without a trailing line feed; empty for an empty table.</returns>
        public string BenefitsTableText()
        {
            string result = string.Empty;
            foreach ( KeyValuePair<int, TravellerMusteringOutBenefit> item in BenefitsTable )
            {
                result += string.Format(BenefitRowFormat, item.Key, item.Value.Name);
            }
            // Remove the last \n character
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        // Public Properties

        /// <summary>Gets or sets the service name.</summary>
        public String Name { get; set; }
        /// <summary>Gets or sets the unmodified enlistment target.</summary>
        public TravellerRollTarget Enlistment { get; set; }
        /// <summary>Gets or sets the characteristic threshold granting DM+1 to enlistment.</summary>
        public TravellerCharacteristicRollTarget EnlistmentPlusOne { get; set; }
        /// <summary>Gets or sets the characteristic threshold granting DM+2 to enlistment.</summary>
        public TravellerCharacteristicRollTarget EnlistmentPlusTwo { get; set; }
        /// <summary>Gets or sets the unmodified term-survival target.</summary>
        public TravellerRollTarget Survival { get; set; }
        /// <summary>Gets or sets the characteristic threshold granting DM+2 to survival.</summary>
        public TravellerCharacteristicRollTarget SurvivalPlusTwo { get; set; }
        /// <summary>Gets or sets the unmodified commission target.</summary>
        public TravellerRollTarget Commission { get; set; }
        /// <summary>Gets or sets the characteristic threshold granting DM+1 to commission.</summary>
        public TravellerCharacteristicRollTarget CommissionPlusOne { get; set; }
        /// <summary>Gets or sets the unmodified promotion target.</summary>
        public TravellerRollTarget Promotion { get; set; }
        /// <summary>Gets or sets the characteristic threshold granting DM+1 to promotion.</summary>
        public TravellerCharacteristicRollTarget PromotionPlusOne { get; set; }
        /// <summary>Gets or sets the reenlistment target.</summary>
        public TravellerRollTarget Reenlist { get; set; }
        /// <summary>Gets or sets the die result that assigns this service in the draft.</summary>
        public decimal DraftNumber { get; set; }
        /// <summary>Gets or sets the mutable rank-name list indexed from zero.</summary>
        public List<string> Ranks { get; set; }
        /// <summary>Gets or sets rank-indexed skill or characteristic adjustments; multiple entries may share a rank.</summary>
        public List<KeyValuePair<int, TravellerSkillModifier>> AutomaticSkills { get; set; }
        /// <summary>Gets or sets the number of skill selections granted in the first term.</summary>
        public decimal SkillsFirstTerm { get; set; }
        /// <summary>Gets or sets the number of skill selections granted per later term.</summary>
        public decimal SkillsPerTerm { get; set; }
        /// <summary>Gets or sets the personal-development outcomes keyed by die result.</summary>
        public List<KeyValuePair<int, TravellerSkillModifier>> PersonalDevelopmentTable { get; set; }
        /// <summary>Gets or sets the service-skill outcomes keyed by die result.</summary>
        public List<KeyValuePair<int, TravellerSkillModifier>> ServiceSkillsTable { get; set; }
        /// <summary>Gets or sets the first advanced-education table keyed by die result.</summary>
        public List<KeyValuePair<int, TravellerSkillModifier>> AdvancedEducationTable { get; set; }
        /// <summary>Gets or sets the second advanced-education table keyed by die result.</summary>
        public List<KeyValuePair<int, TravellerSkillModifier>> AdvancedEducationTable2 { get; set; }
        /// <summary>Gets or sets the mustering-out credit awards keyed by die result.</summary>
        public List<KeyValuePair<int, decimal>> CashTable { get; set; }
        /// <summary>Gets or sets the non-cash mustering-out outcomes keyed by die result.</summary>
        public List<KeyValuePair<int, TravellerMusteringOutBenefit>> BenefitsTable { get; set; }
        /// <summary>Gets or sets whether the service grants retirement pay; defaults to true.</summary>
        public bool HasRetirementPay { get; set; }
    }
}
