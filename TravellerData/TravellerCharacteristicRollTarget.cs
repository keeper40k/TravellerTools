using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
{
    /// <summary>Tests a named character characteristic against a threshold.</summary>
    public class TravellerCharacteristicRollTarget
    {
        // Constructor
        /// <summary>Stores a characteristic code and minimum threshold.</summary>
        /// <param name="stat">The case-sensitive code STR, DEX, END, INT, EDU, or SOC; other codes never pass.</param>
        /// <param name="target">The minimum characteristic value needed to pass.</param>
        public TravellerCharacteristicRollTarget(string stat, decimal target)
        {
            Stat = stat;
            Target = target;
        }

        // Public Methods

        /// <summary>Compares the selected characteristic with the threshold.</summary>
        /// <param name="character">The character to inspect; must be non-null for a recognized characteristic.</param>
        /// <returns>True if the characteristic is at least Target; false for a lower value or an unrecognized code.</returns>
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

        /// <summary>Gets or sets the case-sensitive characteristic code: STR, DEX, END, INT, EDU, or SOC.</summary>
        public string Stat { get; set; }
        /// <summary>Gets or sets the minimum characteristic value needed to pass.</summary>
        public decimal Target { get; set; }
    }
}
