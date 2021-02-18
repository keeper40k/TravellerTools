using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerTools.Fundamentals;

namespace TravellerTools.TravellerData
{
    public class TravellerCharacter
    {
        // Static Strings

        private static string SIR = "Sir";
        private static string LADY = "Lady";
        private static string KNIGHT = "Knight";
        private static string KNIGHTESS = "Knightess";
        private static string DAME = "Dame";
        private static string BARON = "Baron";
        private static string BARONET = "Baronet";
        private static string BARONESS = "Baroness";
        private static string MARQUIS = "Marquis";
        private static string MARQUESA = "Marquesa";
        private static string MARCHIONESS = "Marchioness";
        private static string COUNT = "Count";
        private static string VISCOUNT = "Viscount";
        private static string COUNTESS = "Countess";
        private static string DUKE = "Duke";
        private static string DUCHESS = "Duchess";
        private static string ARCHDUKE = "Archduke";
        private static string ARCHDUCHESS = "Archduchess";
        private static string PRINCE = "Prince";
        private static string PRINCESS = "Princess";
        private static string EMPEROR = "Emperor";
        private static string EMPERORESS = "Emperoress";

        private static string FAILED_AGING_CRISIS = "{0} suffered an Aging Crisis and passed away at the age of {1}.";
        private static string PASSED_AGING_CRISIS = "{0} passed through Aging Crisis at the age of {1} and aged a further {2} months due to Slow Drug recovery.";

        private static string ATT_SKILL_LOSS = "{0} lost {1} {2}\n";
        private static string ATT_SKILL_GAIN = "{0} gained {1} {2}\n";

        // Constructors

        public TravellerCharacter()
        {
            // Temporary Name
            Name = "Bob";
            Reinitialise();
            RollRandomCharacteristics();
        }

        // Protected Methods

        protected void ProcessAging(decimal start, decimal end)
        {
            // Doesn't handle negative aging at this time!
            if( end <= start)
            {
                return;
            }

            for( decimal i = start+1; i <= end; i++ )
            {
                // i mod 4 remainder 2 gives us aging years of 34, 28, 42, etc.
                if ( (i > 33) && (i % 4 == 2) )
                {
                    if( i < 50 )
                    {
                        Phase1AgeCheck(i);
                    }
                    else if( i < 66 )
                    {
                        Phase2AgeCheck(i);
                    }
                    else
                    {
                        Phase3AgeCheck(i);
                    }
                }
            }
        }

        // STR, DEX and INT save on (8+, 7+, 8+)
        protected void Phase1AgeCheck(decimal currentAge)
        {
            int strRoll = DiceTools.RollDice(2, 6);
            if( strRoll < 8 )
            {
                STR = STR - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "STR");
            }
            int dexRoll = DiceTools.RollDice(2, 6);
            if (dexRoll < 7)
            {
                DEX = DEX - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "DEX");
            }
            int endRoll = DiceTools.RollDice(2, 6);
            if (endRoll < 8)
            {
                END = END - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "END");
            }
            AgingCrisis(currentAge);
        }

        // STR, DEX and INT save on (9+, 8+, 9+)
        protected void Phase2AgeCheck(decimal currentAge)
        {
            int strRoll = DiceTools.RollDice(2, 6);
            if (strRoll < 9)
            {
                STR = STR - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "STR");
            }
            int dexRoll = DiceTools.RollDice(2, 6);
            if (dexRoll < 8)
            {
                DEX = DEX - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "DEX");
            }
            int endRoll = DiceTools.RollDice(2, 6);
            if (endRoll < 9)
            {
                END = END - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "END");
            }
            AgingCrisis(currentAge);
        }

        // STR, DEX, INT and INT save on (9+, 9+, 9+. 9+)
        protected void Phase3AgeCheck(decimal currentAge)
        {
            int strRoll = DiceTools.RollDice(2, 6);
            if (strRoll < 9)
            {
                STR = STR - 2;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 2, "STR");
            }
            int dexRoll = DiceTools.RollDice(2, 6);
            if (dexRoll < 9)
            {
                DEX = DEX - 2;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 2, "DEX");
            }
            int endRoll = DiceTools.RollDice(2, 6);
            if (endRoll < 9)
            {
                END = END - 2;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 2, "END");
            }
            int intRoll = DiceTools.RollDice(2, 6);
            if (intRoll < 9)
            {
                INT = INT - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "INT");
            }
            AgingCrisis(currentAge);
        }

        protected void AgingCrisis(decimal currentAge)
        {
            int slowDrugAging = 0;
            if( STR == 0 )
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if( saveRoll < 8 )
                {
                    IsDead = true;
                }
                else
                {
                    STR = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (DEX == 0)
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if (saveRoll < 8)
                {
                    IsDead = true;
                }
                else
                {
                    DEX = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (END == 0)
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if (saveRoll < 8)
                {
                    IsDead = true;
                }
                else
                {
                    END = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (INT == 0)
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if (saveRoll < 8)
                {
                    IsDead = true;
                }
                else
                {
                    INT = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (IsDead)
            {
                CreationHistory += string.Format(FAILED_AGING_CRISIS, Name, currentAge) + "\n";
            }
            else if (slowDrugAging != 0)
            {
                CreationHistory += string.Format(PASSED_AGING_CRISIS, Name, currentAge, slowDrugAging) + "\n";
            }
        }

        // Public Methods

        public string EHexCharacteristic(int value)
        {
            string result = "";
            if (value <= 0)
            {
                result = "0";
            }
            else if (value > 0 && value <= 9)
            {
                result = value.ToString();
            }
            else if (value <= 17) //17 is H and then we skip I
            {
                result += (char)(value + 55);
            }
            else if (value <= 22) //22 is N then we skip O
            {
                result += (char)(value + 56);
            }
            else if (value <= 33) //33 is the maximum.  Everything above resolves to 33.
            {
                result += (char)(value + 57);
            }
            else  //33 is the maximum.  Everything above resolves to 33 or Z.
            {
                result = "Z";
            }

            return result;
        }

        public void Reinitialise()
        {
            Age = 18;
            Title = string.Empty;
            UseTitle = true;
            Rank = string.Empty;
            UseRank = true;
            Service = string.Empty;
            Drafted = false;
            FailedService = string.Empty;
            TermsOfService = 0;
            InjuredDuringCreation = false;
            RankNumber = 0;
            Skills = new List<TravellerSkill>();

            CreationHistory = string.Empty;

            IsDead = false;

            Cash = 0;
            Gear = new List<TravellerGear>();
        }

        public void RollRandomCharacteristics()
        {
            STR = DiceTools.RollDice(2, 6);
            DEX = DiceTools.RollDice(2, 6);
            END = DiceTools.RollDice(2, 6);
            INT = DiceTools.RollDice(2, 6);
            EDU = DiceTools.RollDice(2, 6);
            SOC = DiceTools.RollDice(2, 6);
        }

        public List<string> AvailableTitles()
        {
            List<string> titles = new List<string>();
            switch (SOC)
            {
                case 11:
                {
                    titles.Add(KNIGHT);
                    titles.Add(KNIGHTESS);
                    titles.Add(DAME);
                    titles.Add(SIR);
                    titles.Add(LADY);
                    break;
                }
                case 12:
                {
                    titles.Add(BARON);
                    titles.Add(BARONET);
                    titles.Add(BARONESS);
                    break;
                }
                case 13:
                    {
                    titles.Add(MARQUIS);
                    titles.Add(MARQUESA);
                    titles.Add(MARCHIONESS);
                    break;
                }
                case 14:
                {
                    titles.Add(COUNT);
                    titles.Add(VISCOUNT);
                    titles.Add(COUNTESS);
                    break;
                }
                case 15:
                {
                    titles.Add(DUKE);
                    titles.Add(DUCHESS);
                    break;
                }
                case 16:
                {
                    titles.Add(ARCHDUKE);
                    titles.Add(ARCHDUCHESS);
                    titles.Add(PRINCE);
                    titles.Add(PRINCESS);
                    break;
                }
                case 17:
                {
                    titles.Add(EMPEROR);
                    titles.Add(EMPERORESS);
                    break;
                }
                default:
                {
                    titles.Add(string.Empty);
                    break;
                }
            }
            return titles;
        }

        public void AddSkill( TravellerSkillModifier newSkill, ISkillSpecialisationCollection callback )
        {
            SpecialisationSelectionCallback = callback;

            // TO DO
            if( newSkill.IsAttribute )
            {
                switch( newSkill.Name )
                {
                    case "STR":
                    {
                        STR += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "DEX":
                    {
                        DEX += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "END":
                    {
                        END += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "INT":
                    {
                        INT += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "EDU":
                    {
                        EDU += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "SOC":
                    {
                        SOC += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    default:
                    {
                        // Do nothing
                        break;
                    }
                }
            }
            if( newSkill.IsSkill )
            {
                TravellerSkill fullSkill = TravellerSkills.MatchSkill(newSkill.Name);
                if (fullSkill != null)
                {
                    // Resolve Specialisation. While loop for nesting
                    while( fullSkill.HasSpecialisations )
                    {
                        fullSkill = ChooseSpecialisation(fullSkill.Name, fullSkill.Specialisations);
                    }

                    bool found = false;
                    foreach (TravellerSkill existingSkill in Skills)
                    {
                        if( existingSkill.Name == fullSkill.Name )
                        {
                            found = true;
                            existingSkill.Level += newSkill.Level;
                            CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                            break;
                        }
                    }

                    if (!found )
                    {
                        TravellerSkill localSkill = new TravellerSkill(fullSkill);
                        localSkill.Level = newSkill.Level;
                        Skills.Add( localSkill );
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                    }
                }
            }

            SpecialisationSelectionCallback = null;
        }

        // Should only be used when the newSkill does not have further specialisation
        public void AddSkill(TravellerSkill newSkill)
        {
            if (newSkill != null)
            {
                bool found = false;
                foreach (TravellerSkill existingSkill in Skills)
                {
                    if (existingSkill.Name == newSkill.Name)
                    {
                        found = true;
                        existingSkill.Level += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                }

                if (!found)
                {
                    Skills.Add(newSkill);
                    CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                }
            }
        }

        public void AddGear( TravellerGear gear )
        {
            bool found = false;
            foreach( TravellerGear item in Gear )
            {
                if( gear.Name == item.Name )
                {
                    found = true;
                    item.Count += gear.Count;
                }
            }
            if( !found )
            {
                Gear.Add(gear);
            }
        }

        // Public Override Methods

        public override string ToString()
        {
            return ShortStringFormat() + "\n" + CharacterHistory();
        }

        // Public Methods

        public string ShortStringFormat()
        {
            string result = "";
            if ((Title != string.Empty) && UseTitle)
            {
                result += Title + " ";
            }
            if ((Rank != string.Empty) && UseRank)
            {
                result += Rank + " ";
            }
            result += Name + "\t" + UPP + "\tAge " + Age + "\tCr" + Cash + "\n";

            for (int i = 0; i < Skills.Count; i++)
            {
                result += Skills[i].Name + "-" + Skills[i].Level;
                if (i < Skills.Count - 1)
                {
                    result += ", ";
                }
            }

            if (Gear.Count > 0)
            {
                result += "\n";
            }

            for (int i = 0; i < Gear.Count; i++)
            {
                result += Gear[i];
                if (i < Gear.Count - 1)
                {
                    result += ", ";
                }
            }
            return result;
        }

        public string CharacterHistory()
        {
            string result = string.Empty;
            if (Service != string.Empty)
            {
                result += "Service: " + Service;
            }
            if (CreationHistory != string.Empty)
            {
                result += "\n\n" + CreationHistory;
            }
            return result;
        }

        public bool HasSkill( string name )
        {
            bool result = false;
            foreach( TravellerSkill skill in Skills )
            {
                if( name == skill.Name )
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // Property Backers complex properties

        private int m_SOC;
        private decimal m_age;

        // Public Properties

        public string Title { get; set; }
        // Doesn't need to be serialised
        public bool UseTitle; 
        public string Rank { get; set; }
        public bool UseRank { get; set; }
        public string Name { get; set; }
        public int STR { get; set; }
        public int DEX { get; set; }
        public int END { get; set; }
        public int INT { get; set; }
        public int EDU { get; set; }
        public int SOC
        {
            get
            {
                return m_SOC;
            }
            set
            {
                m_SOC = value;
                if (value >= 11)
                {
                    UseTitle = true;
                }
                else
                {
                    UseTitle = false;
                }
            }
        }

        // Doesn't need to be serialised
        public string UPP
        {
            get
            {
                string upp = EHexCharacteristic(STR) +
                             EHexCharacteristic(DEX) +
                             EHexCharacteristic(END) +
                             EHexCharacteristic(INT) +
                             EHexCharacteristic(EDU) +
                             EHexCharacteristic(SOC);
                return upp;
            }
        }

        public bool Commissioned
        {
            get
            {
                return RankNumber > 0;
            }
        }

        public decimal Age
        {
            get
            {
                return m_age;
            }
            set
            {
                ProcessAging(m_age, value);
                m_age = value;
            }
        }
        public string Service { get; set; }
        public bool Drafted { get; set; }
        public string FailedService { get; set; }
        public decimal TermsOfService { get; set; }
        public bool InjuredDuringCreation { get; set; }
        public decimal RankNumber { get; set; }
        public List<TravellerSkill> Skills { get; set; }

        public string CreationHistory { get; set; }

        public bool IsDead { get; set; }

        public int Cash { get; set; }
        public List<TravellerGear> Gear { get; set; }

        // Event Management

        // Not using { get; set; } here as this data is not for serialisation. Should only be one at once.
        public ISkillSpecialisationCollection SpecialisationSelectionCallback;

        protected TravellerSkill ChooseSpecialisation( string skillName, List<TravellerSkill> list )
        {
            return SpecialisationSelectionCallback.SelectSpecialisation(skillName, list);
        }
    }
}
