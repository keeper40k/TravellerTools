using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.CharGen
{
    public class TravellerCharacteristicRollTarget
    {
        // Constructor
        public TravellerCharacteristicRollTarget(string stat, decimal target )
        {
            Stat = stat;
            Target = target;
        }

        // Public Methods

        public bool Pass( TravellerCharacter character )
        {
            bool result = false;

            switch (Stat)
            {
                case "STR":
                {
                    if( character.STR >= Target )
                    {
                        result = true;
                    }
                    break;
                }
                case "DEX":
                {
                    if (character.DEX >= Target)
                    {
                        result = true;
                    }
                    break;
                }
                case "END":
                {
                    if (character.END >= Target)
                    {
                        result = true;
                    }
                    break;
                }
                case "INT":
                {
                    if (character.INT >= Target)
                    {
                        result = true;
                    }
                    break;
                }
                case "EDU":
                {
                    if (character.EDU >= Target)
                    {
                        result = true;
                    }
                    break;
                }
                case "SOC":
                {
                    if (character.SOC >= Target)
                    {
                        result = true;
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }

            return result;
        }

        // Properties

        public string Stat { get; set; }
        public decimal Target { get; set; }
    }
}
