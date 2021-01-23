using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.CharGen
{
    public class TravellerCharacteristicRollTarget : TravellerRollTarget
    {
        // Constructor
        public TravellerCharacteristicRollTarget(string stat, decimal target )
        {
            Stat = stat;
            Target = target;
        }

        // Properties

        public string Stat;
    }
}
