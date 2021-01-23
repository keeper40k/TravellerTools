using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.CharGen
{
    public class TravellerServices
    {
        List<TravellerService> m_services;

        // Constructor
        public TravellerServices()
        {
            m_services = new List<TravellerService>();
        }

        public List<TravellerService> Services
        {
            get
            {
                return m_services;
            }
            // Unsure right now if I need a setter or a loader.  I am tending towards a loader ...
        }
    }
}
