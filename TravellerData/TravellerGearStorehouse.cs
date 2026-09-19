using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TravellerTools.TravellerData;

/// <summary>Provides shared gear definitions loaded from gear.json in the current working directory.</summary>
/// <remarks>
/// Initial loading uses gear.json in the working directory at first access. Initialization failures
/// are wrapped in TypeInitializationException and are not retried for that loaded type.
/// File changes are not automatically reloaded. Lookups return mutable shared objects, not copies.
/// The loader recognizes exactly TravellerGear, TravellerRetirementPay, and TravellerStarshipBenefit
/// ClassType values and skips unrecognized values. Concurrent mutation or reloading is not supported.
/// </remarks>
public class TravellerGearStorehouse
{
    // Shared definitions use the current working directory at first access.
    private const string GearFileName = "gear.json";

    /// <summary>Loads the shared definitions once for this type.</summary>
    static TravellerGearStorehouse()
    {
        Gear = new();
        LoadGear();
    }

    /// <summary>Clears and reloads gear definitions from gear.json in the current working directory.</summary>
    /// <remarks>The root must be an array of objects, each providing ClassType. An empty array clears the catalogue; JSON null is invalid. The old catalogue is cleared before the file is read. Read or parse failures leave it empty; conversion or entry-shape failures retain only earlier successfully loaded entries. Unknown ClassType values are skipped. Other properties use the selected gear type's defaults and deserialization rules; no additional validation is performed. A later explicit reload can recover after a failed reload, provided initial type initialization succeeded.</remarks>
    /// <exception cref="System.UnauthorizedAccessException">Access to the gear file is denied.</exception>
    /// <exception cref="System.IO.IOException">The file cannot be read.</exception>
    /// <exception cref="System.Text.Json.JsonException">The JSON cannot be parsed or converted.</exception>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">An entry has no ClassType property.</exception>
    /// <exception cref="InvalidOperationException">The JSON root is not an array or an entry is not an object.</exception>
    protected static void LoadGear()
    {
        Gear.Clear();
        string json = File.ReadAllText(GearFileName);

        using (JsonDocument document = JsonDocument.Parse(json))
        {
            JsonElement root = document.RootElement;
            foreach (JsonElement entry in root.EnumerateArray())
            {
                string classType = entry.GetProperty("ClassType").ToString();
                if (classType == "TravellerGear")
                {
                    TravellerGear? gear = JsonSerializer.Deserialize<TravellerGear>(entry.GetRawText());
                    if (gear != null)
                    {
                        Gear.Add(gear);
                    }
                }
                else if (classType == "TravellerRetirementPay")
                {
                    TravellerRetirementPay? retirementPay = JsonSerializer.Deserialize<TravellerRetirementPay>(entry.GetRawText());
                    if (retirementPay != null)
                    {
                        Gear.Add(retirementPay);
                    }
                }
                else if (classType == "TravellerStarshipBenefit")
                {
                    TravellerStarshipBenefit? starshipBenefit = JsonSerializer.Deserialize<TravellerStarshipBenefit>(entry.GetRawText());
                    if (starshipBenefit != null)
                    {
                        Gear.Add(starshipBenefit);
                    }
                }
            }
        }
    }

    /// <summary>Finds the first gear definition matching a name and optional weapon category.</summary>
    /// <param name="Name">The exact, case-sensitive gear name, compared ordinally without trimming. Empty names can match unnamed definitions; a null name matches only a definition whose Name is null.</param>
    /// <param name="weaponType">The exact GearType followed by ' Combat', compared ordinally without trimming; an empty or null string disables this filter.</param>
    /// <returns>The matching shared gear object, or null.</returns>
    /// <remarks>Returns the first matching entry in file order, including when names are duplicated. Unknown names return null. Arguments are not validated. No copy is made, so mutations affect subsequent lookups.</remarks>
    /// <exception cref="TypeInitializationException">The initial gear.json load failed.</exception>
    // Retain Name for compatibility with callers using named arguments.
    public static TravellerGear? GetGear(string Name, string weaponType)
    {
        TravellerGear? result = null;
        foreach (TravellerGear gear in Gear)
        {
            bool matchesName = gear.Name == Name;
            bool matchesWeaponType = string.IsNullOrEmpty(weaponType) || gear.GearType + " Combat" == weaponType;
            if (matchesName && matchesWeaponType)
            {
                result = gear;
                break;
            }
        }
        return result;
    }

    /// <summary>Stores the mutable definitions shared by all lookups.</summary>
    private static List<TravellerGear> Gear { get; set; }
}
