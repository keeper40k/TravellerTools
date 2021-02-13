using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
{
    public class TravellerRollTarget
    {
        // Constructor
        public TravellerRollTarget( decimal target )
        {
            Target = target;
        }

        // Properties
        public decimal Target { get; set; }

    }
}
