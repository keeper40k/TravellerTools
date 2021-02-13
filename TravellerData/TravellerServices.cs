using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravellerTools.TravellerData
{
    public class TravellerServices
    {

        // static strings
        private static string SERVICES_FILE = "services.json";
        

        // Private members

        private List<TravellerService> m_services;

        // Constructor
        public TravellerServices()
        {
            m_services = new List<TravellerService>();
        }

        // Public Methods
        public void LoadSettings()
        {
            if (File.Exists(SERVICES_FILE))
            {
                string json = File.ReadAllText(SERVICES_FILE);
                TravellerServices services = JsonSerializer.Deserialize<TravellerServices>(json);
                Duplicate(services);
            }
        }

        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(SERVICES_FILE, json);
        }

        public string RecommendText(TravellerCharacter character)
        {
            string result = string.Empty;
            foreach (TravellerService service in Services)
            {
                // Find out if there are any bonuses
                bool EnlistBonusOne = service.EnlistmentPlusOne.Pass(character);
                bool EnlistBonusTwo = service.EnlistmentPlusTwo.Pass(character);
                bool SurvivalBonus = service.SurvivalPlusTwo.Pass(character);
                bool CommissionBonus = service.CommissionPlusOne.Pass(character);
                bool PromotionBonus = service.PromotionPlusOne.Pass(character);

                if (EnlistBonusOne || EnlistBonusTwo || SurvivalBonus || CommissionBonus || PromotionBonus)
                {
                    result += service.Name + "\n";
                    if (EnlistBonusTwo)
                    {
                        result += "DM+2 Enlistment " + service.Enlistment.Target + "\n";
                    }
                    if (EnlistBonusOne)
                    {
                        result += "DM+1 Enlistment " + service.Enlistment.Target + "\n";
                    }
                    if (SurvivalBonus)
                    {
                        result += "DM+2 Survival " + service.Survival.Target + "\n";
                    }
                    if (CommissionBonus)
                    {
                        result += "DM+1 Commission " + service.Commission.Target + "\n";
                    }
                    if (PromotionBonus)
                    {
                        result += "DM+1 Promotion " + service.Promotion.Target + "\n";
                    }
                    result += "\n";
                }

            }
            return result;
        }

        // Protected Methods

        protected void Duplicate(TravellerServices services)
        {
            TravellerService[] sourceServices = new TravellerService[services.Services.Count];
            services.Services.CopyTo(sourceServices);
            foreach (TravellerService service in sourceServices)
            {
                m_services.Add(service);
            }
        }

        // Public Properties

        public List<TravellerService> Services
        {
            get
            {
                return m_services;
            }
            // Unsure right now if I need a setter or a loader.  I am tending towards a loader ...
            set
            {
                m_services = value;
            }
        }
    }
}
