using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.CharGen
{
    public class CharGenDataBlock
    {

        // Constructor
        public CharGenDataBlock()
        {
            Character = new TravellerCharacter();
            Service = null;
        }

        // Properties
        public TravellerCharacter Character { get; set; }
        public TravellerService Service { get; set; }
}
}
