using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TravellerTools.TravellerData;

/// <summary>Stores a mutable collection of career definitions and persists it as services.json.</summary>
public class TravellerServices
{
    // Settings are resolved relative to the current working directory.
    private const string ServicesFileName = "services.json";

    // Retain the supplied list so existing consumers can share mutations.
    private List<TravellerService> services;

    /// <summary>Creates an empty career collection.</summary>
    public TravellerServices()
    {
        services = new();
    }

    /// <summary>Appends career definitions loaded from services.json in the current working directory.</summary>
    /// <remarks>A missing file or JSON null leaves the current collection unchanged. Repeated loads append duplicates rather than replacing existing entries.</remarks>
    /// <exception cref="System.UnauthorizedAccessException">Access to the settings file is denied.</exception>
    /// <exception cref="System.IO.IOException">The existing file cannot be read.</exception>
    /// <exception cref="System.Text.Json.JsonException">The file cannot be deserialized.</exception>
    public void LoadSettings()
    {
        if (File.Exists(ServicesFileName))
        {
            string json = File.ReadAllText(ServicesFileName);
            TravellerServices? loadedServices = JsonSerializer.Deserialize<TravellerServices>(json);
            if (loadedServices != null)
            {
                Duplicate(loadedServices);
            }
        }
    }

    /// <summary>Writes this collection to services.json in the current working directory, replacing an existing file.</summary>
    /// <remarks>File-system and serialization errors propagate.</remarks>
    /// <exception cref="System.UnauthorizedAccessException">Access to the settings file is denied.</exception>
    /// <exception cref="System.IO.IOException">The file cannot be written.</exception>
    public void SaveSettings()
    {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(ServicesFileName, json);
    }

    /// <summary>Lists services offering characteristic-based bonuses to the supplied character.</summary>
    /// <param name="character">The non-null character whose characteristics are checked.</param>
    /// <returns>Service names, applicable die modifiers, and targets separated by line feeds; empty when no bonuses apply.</returns>
    public string RecommendText(TravellerCharacter character)
    {
        string result = string.Empty;
        foreach (TravellerService service in Services)
        {
            bool enlistBonusOne = service.EnlistmentPlusOne.Pass(character);
            bool enlistBonusTwo = service.EnlistmentPlusTwo.Pass(character);
            bool survivalBonus = service.SurvivalPlusTwo.Pass(character);
            bool commissionBonus = service.CommissionPlusOne.Pass(character);
            bool promotionBonus = service.PromotionPlusOne.Pass(character);

            if (enlistBonusOne || enlistBonusTwo || survivalBonus || commissionBonus || promotionBonus)
            {
                result += service.Name + "\n";
                if (enlistBonusTwo)
                {
                    result += "DM+2 Enlistment " + service.Enlistment.Target + "\n";
                }
                if (enlistBonusOne)
                {
                    result += "DM+1 Enlistment " + service.Enlistment.Target + "\n";
                }
                if (survivalBonus)
                {
                    result += "DM+2 Survival " + service.Survival.Target + "\n";
                }
                if (commissionBonus)
                {
                    result += "DM+1 Commission " + service.Commission.Target + "\n";
                }
                if (promotionBonus)
                {
                    result += "DM+1 Promotion " + service.Promotion.Target + "\n";
                }
                result += "\n";
            }

        }
        return result;
    }

    /// <summary>Appends the source collection's service objects by reference.</summary>
    /// <param name="services">The non-null collection to copy from.</param>
    /// <remarks>The destination is not cleared and individual services are not cloned. Copying a collection to itself appends its existing entries once.</remarks>
    protected void Duplicate(TravellerServices services)
    {
        // Snapshot first so copying this collection to itself also appends safely.
        TravellerService[] sourceServices = new TravellerService[services.Services.Count];
        services.Services.CopyTo(sourceServices);
        foreach (TravellerService service in sourceServices)
        {
            this.services.Add(service);
        }
    }

    /// <summary>Gets or sets the mutable career list; consumers require a non-null list.</summary>
    /// <value>The list used directly by this collection; assigning a list does not copy or validate it.</value>
    public List<TravellerService> Services
    {
        get
        {
            return services;
        }
        set
        {
            services = value;
        }
    }
}
