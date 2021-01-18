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
            // Temporary Initialisaton
            Name = "Bob";
            Title = string.Empty;
            UseTitle = true;
            Rank = string.Empty;
            UseRank = true;
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
                    titles.Add( KNIGHT );
                    titles.Add( KNIGHTESS );
                    titles.Add( DAME );
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

        // Override Methods
        public override string ToString()
        {
            string result = "";
            if ( (Title != string.Empty) && UseTitle )
            {
                result += Title + " ";
            }
            if( (Rank != string.Empty) && UseRank )
            {
                result += Rank + " ";
            }
            result += Name + "\t" + UPP;
            return result;
        }

        // Property Backers complex properties
        private int m_SOC;

        // Properties
        public string Title;
        public bool UseTitle;
        public string Rank;
        public bool UseRank;
        public string Name;
        public int STR;
        public int DEX;
        public int END;
        public int INT;
        public int EDU;
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
    }
}
