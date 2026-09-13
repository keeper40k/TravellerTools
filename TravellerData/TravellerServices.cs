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
    /// <summary>Stores a mutable collection of career definitions and persists it as services.json.</summary>
    public class TravellerServices
    {

        // static strings
        private static string SERVICES_FILE = "services.json";
        

        // Private members

        private List<TravellerService> services;

        // Constructor
        /// <summary>Creates an empty career collection.</summary>
        public TravellerServices()
        {
            services = new List<TravellerService>();
        }

        // Public Methods
        /// <summary>Appends career definitions loaded from services.json in the current working directory.</summary>
        /// <remarks>A missing file or JSON null leaves the current collection unchanged. Repeated loads append duplicates rather than replacing existing entries.</remarks>
        /// <exception cref="System.IO.IOException">The existing file cannot be read.</exception>
        /// <exception cref="System.Text.Json.JsonException">The file cannot be deserialized.</exception>
        public void LoadSettings()
        {
            if (File.Exists(SERVICES_FILE))
            {
                string json = File.ReadAllText(SERVICES_FILE);
                TravellerServices? loadedServices = JsonSerializer.Deserialize<TravellerServices>(json);
                if (loadedServices != null)
                {
                    Duplicate(loadedServices);
                }
            }
        }

        /// <summary>Writes this collection to services.json in the current working directory, replacing an existing file.</summary>
        /// <remarks>File-system and serialization errors propagate.</remarks>
        /// <exception cref="System.IO.IOException">The file cannot be written.</exception>
        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(SERVICES_FILE, json);
        }

        /// <summary>Lists services offering characteristic-based bonuses to the supplied character.</summary>
        /// <param name="character">The non-null character whose characteristics are checked.</param>
        /// <returns>Service names, applicable die modifiers, and targets separated by line feeds; empty when no bonuses apply.</returns>
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

        /// <summary>Appends the source collection's service objects by reference.</summary>
        /// <param name="services">The non-null collection to copy from.</param>
        /// <remarks>The destination is not cleared and individual services are not cloned.</remarks>
        protected void Duplicate(TravellerServices services)
        {
            TravellerService[] sourceServices = new TravellerService[services.Services.Count];
            services.Services.CopyTo(sourceServices);
            foreach (TravellerService service in sourceServices)
            {
                this.services.Add(service);
            }
        }

        // Public Properties

        /// <summary>Gets or sets the mutable career list; consumers require a non-null list.</summary>
        public List<TravellerService> Services
        {
            get
            {
                return services;
            }
            // Unsure right now if I need a setter or a loader.  I am tending towards a loader ...
            set
            {
                services = value;
            }
        }
    }
}
