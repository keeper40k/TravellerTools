using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerTools.Fundamentals;

namespace TravellerTools.CharGen
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

        // Constructors

        public TravellerCharacter()
        {
            // Temporary Name
            Name = "Bob";
            Reinitialise();
            RollRandomCharacteristics();
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

            CreationHistory = string.Empty;
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
            result += Name + "\t" + UPP + "\tAge " + Age;
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

        // Property Backers complex properties

        private int m_SOC;

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

        public decimal Age { get; set; }
        public string Service { get; set; }
        public bool Drafted { get; set; }
        public string FailedService { get; set; }
        public decimal TermsOfService { get; set; }
        public bool InjuredDuringCreation { get; set; }
        public decimal RankNumber { get; set; }

        public string CreationHistory { get; set; }
    }
}
