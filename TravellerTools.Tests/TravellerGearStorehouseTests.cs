using System.Reflection;
using System.Runtime.Loader;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies gear loading, matching, and reload failures without sharing static state.</summary>
[TestClass]
[TestCategory("Unit")]
[DoNotParallelize]
public class TravellerGearStorehouseTests
{
    // Isolate working-directory changes and restore them after every case.
    private string previousDirectory = string.Empty;
    private string settingsDirectory = string.Empty;

    // Separate assemblies keep initialization failures and reloads local to each test.
    private sealed class GearCatalogue : IDisposable
    {
        private readonly AssemblyLoadContext context = new(null, isCollectible: true);
        private readonly Func<string, string, object?> lookup;
        private readonly Action reload;

        /// <summary>Loads a fresh copy of the real implementation without initializing settings yet.</summary>
        public GearCatalogue()
        {
            Assembly assembly = context.LoadFromAssemblyPath(typeof(TravellerGearStorehouse).Assembly.Location);
            Type type = assembly.GetType(typeof(TravellerGearStorehouse).FullName!, throwOnError: true)!;
            // Isolated gear types have distinct runtime identities; object preserves identity
            // across this boundary, while delegates preserve the original exception types.
            lookup = type.GetMethod(nameof(TravellerGearStorehouse.GetGear))!.CreateDelegate<Func<string, string, object?>>();
            reload = type.GetMethod("LoadGear", BindingFlags.Static | BindingFlags.NonPublic)!.CreateDelegate<Action>();
        }

        /// <summary>Invokes the real public lookup.</summary>
        public object? Get(string name, string weaponType = "") => lookup(name, weaponType);

        /// <summary>Invokes the protected reload operation available to derived consumers.</summary>
        public void Reload() => reload();

        /// <summary>Requests unloading after test references are released.</summary>
        public void Dispose() => context.Unload();
    }

    /// <summary>Creates temporary settings independent of the repository catalogue.</summary>
    [TestInitialize]
    public void CreateSettingsDirectory()
    {
        previousDirectory = Directory.GetCurrentDirectory();
        settingsDirectory = Directory.CreateTempSubdirectory("TravellerGearTests-").FullName;
        Directory.SetCurrentDirectory(settingsDirectory);
    }

    /// <summary>Restores the process directory and removes only this test's temporary files.</summary>
    [TestCleanup]
    public void RemoveSettingsDirectory()
    {
        Directory.SetCurrentDirectory(previousDirectory);
        Directory.Delete(settingsDirectory, recursive: true);
    }

    /// <summary>Verifies all supported subtypes retain their common and specific JSON properties.</summary>
    [TestMethod]
    public void SupportedTypesPreserveTheirData()
    {
        File.WriteAllText("gear.json", """
            [
              {"ClassType":"TravellerGear","Name":"Rifle","GearType":"Gun","Count":2,"Value":200,"Weight":3000,"TechLevel":5,"Description":"Longarm"},
              {"ClassType":"TravellerRetirementPay","Amount":6000},
              {"ClassType":"TravellerStarshipBenefit","Name":"Free Trader","MortgageDuration":27}
            ]
            """);
        using GearCatalogue catalogue = new();

        object? rifle = catalogue.Get("Rifle");
        Assert.IsNotNull(rifle);
        Assert.AreEqual(nameof(TravellerGear), rifle.GetType().Name);
        JsonElement gear = Data(rifle);
        Assert.AreEqual(2m, gear.GetProperty("Count").GetDecimal());
        Assert.AreEqual(200, gear.GetProperty("Value").GetInt32());
        Assert.AreEqual(3000, gear.GetProperty("Weight").GetInt32());
        Assert.AreEqual(5m, gear.GetProperty("TechLevel").GetDecimal());
        Assert.AreEqual("Longarm", gear.GetProperty("Description").GetString());
        object? retirement = catalogue.Get("Retirement Pay");
        Assert.IsNotNull(retirement);
        Assert.AreEqual(nameof(TravellerRetirementPay), retirement.GetType().Name);
        Assert.AreEqual(6000m, Data(retirement).GetProperty("Amount").GetDecimal());
        object? ship = catalogue.Get("Free Trader");
        Assert.IsNotNull(ship);
        Assert.AreEqual(nameof(TravellerStarshipBenefit), ship.GetType().Name);
        Assert.AreEqual(27, Data(ship).GetProperty("MortgageDuration").GetInt32());
    }

    /// <summary>Verifies unknown class values are skipped without blocking later valid entries.</summary>
    /// <param name="classType">A JSON value that does not identify a supported class.</param>
    [TestMethod]
    [DataRow("\"FutureGear\"")]
    [DataRow("\"travellergear\"")]
    [DataRow("null")]
    [DataRow("42")]
    public void UnknownClassTypesAreSkipped(string classType)
    {
        File.WriteAllText("gear.json",
            "[{\"ClassType\":" + classType + ",\"Name\":\"Skipped\"},{\"ClassType\":\"TravellerGear\",\"Name\":\"Kept\"}]");
        using GearCatalogue catalogue = new();

        Assert.IsNull(catalogue.Get("Skipped"));
        Assert.IsNotNull(catalogue.Get("Kept"));
    }

    /// <summary>Verifies exact matching, optional filtering, and duplicate selection in file order.</summary>
    [TestMethod]
    public void MatchingIsExactAndReturnsFirstApplicableEntry()
    {
        File.WriteAllText("gear.json", """
            [
              {"ClassType":"TravellerGear","Name":"Weapon","GearType":"Blade","Description":"first"},
              {"ClassType":"TravellerGear","Name":"Weapon","GearType":"Gun","Description":"second"},
              {"ClassType":"TravellerGear","Name":"Weapon","GearType":"Gun","Description":"third"},
              {"ClassType":"TravellerGear","Name":"Caf\u00e9"}
            ]
            """);
        using GearCatalogue catalogue = new();

        object? first = catalogue.Get("Weapon");
        Assert.AreEqual("first", Data(first).GetProperty("Description").GetString());
        Assert.AreSame(first, catalogue.Get("Weapon", null!));
        Assert.AreEqual("second", Data(catalogue.Get("Weapon", "Gun Combat")).GetProperty("Description").GetString());
        Assert.IsNull(catalogue.Get("weapon"));
        Assert.IsNull(catalogue.Get(" Weapon "));
        Assert.IsNull(catalogue.Get("Weapon", "gun Combat"));
        Assert.IsNull(catalogue.Get("Weapon", "Gun"));
        Assert.IsNull(catalogue.Get("Weapon", " "));
        Assert.IsNull(catalogue.Get("Unknown"));
        Assert.IsNull(catalogue.Get(null!));
        Assert.IsNull(catalogue.Get("Cafe\u0301"), "Gear names retain ordinal rather than linguistic matching.");
    }

    /// <summary>Verifies default and explicit null names are retained without argument validation.</summary>
    [TestMethod]
    public void MissingPropertiesUseDefaultsAndNullNamesCanMatch()
    {
        File.WriteAllText("gear.json", """
            [{"ClassType":"TravellerGear"},{"ClassType":"TravellerGear","Name":null}]
            """);
        using GearCatalogue catalogue = new();

        Assert.AreEqual(1m, Data(catalogue.Get(string.Empty)).GetProperty("Count").GetDecimal());
        Assert.IsNotNull(catalogue.Get(null!));
    }

    /// <summary>Verifies returned objects are mutable shared definitions rather than copies.</summary>
    [TestMethod]
    public void ReturnedGearIsShared()
    {
        File.WriteAllText("gear.json", """[{"ClassType":"TravellerGear","Name":"Rifle"}]""");
        using GearCatalogue catalogue = new();
        object? rifle = catalogue.Get("Rifle");
        Assert.IsNotNull(rifle);
        rifle.GetType().GetProperty(nameof(TravellerGear.Name))!.SetValue(rifle, "Renamed");

        Assert.AreSame(rifle, catalogue.Get("Renamed"));
        Assert.IsNull(catalogue.Get("Rifle"));
    }

    /// <summary>Verifies initial loading uses the first-access directory and ignores later file changes.</summary>
    [TestMethod]
    public void SettingsLoadOnceAtFirstAccess()
    {
        using GearCatalogue catalogue = new();
        Directory.SetCurrentDirectory(Directory.CreateDirectory(Path.Combine(settingsDirectory, "first-use")).FullName);
        File.WriteAllText("gear.json", """[{"ClassType":"TravellerGear","Name":"Rifle"}]""");
        object? rifle = catalogue.Get("Rifle");
        Assert.IsNotNull(rifle);
        File.WriteAllText("gear.json", "{");
        Directory.SetCurrentDirectory(settingsDirectory);

        Assert.AreSame(rifle, catalogue.Get("Rifle"));
    }

    /// <summary>Verifies missing initial settings permanently fail that loaded type.</summary>
    [TestMethod]
    public void MissingInitialFileFailureIsCached()
    {
        using GearCatalogue catalogue = new();
        TypeInitializationException first = Assert.ThrowsException<TypeInitializationException>(() => catalogue.Get("Rifle"));
        Assert.IsInstanceOfType<FileNotFoundException>(first.InnerException);
        File.WriteAllText("gear.json", "[]");

        Assert.ThrowsException<TypeInitializationException>(() => catalogue.Get("Rifle"));
        using GearCatalogue fresh = new();
        Assert.IsNull(fresh.Get("Rifle"));
    }

    /// <summary>Verifies initial JSON and entry-shape errors retain their wrapped exception types.</summary>
    /// <param name="json">Invalid gear data.</param>
    /// <param name="errorType">The original failure wrapped by type initialization.</param>
    [TestMethod]
    [DataRow("{", typeof(JsonException))]
    [DataRow("null", typeof(InvalidOperationException))]
    [DataRow("{}", typeof(InvalidOperationException))]
    [DataRow("[null]", typeof(InvalidOperationException))]
    [DataRow("[{}]", typeof(KeyNotFoundException))]
    [DataRow("[{\"ClassType\":\"TravellerGear\",\"Count\":\"invalid\"}]", typeof(JsonException))]
    public void InitialLoadWrapsDataErrors(string json, Type errorType)
    {
        File.WriteAllText("gear.json", json);
        using GearCatalogue catalogue = new();

        TypeInitializationException failure = Assert.ThrowsException<TypeInitializationException>(() => catalogue.Get("Rifle"));

        Assert.IsNotNull(failure.InnerException);
        Assert.IsInstanceOfType(failure.InnerException, errorType);
    }

    /// <summary>Verifies a failed replacement preserves old objects, exposes no partial entries, and permits retry.</summary>
    /// <param name="invalidEntry">A later entry that makes the replacement invalid.</param>
    /// <param name="errorType">The expected exception family.</param>
    [TestMethod]
    [DataRow("{}", typeof(KeyNotFoundException))]
    [DataRow("null", typeof(InvalidOperationException))]
    [DataRow("{\"ClassType\":\"TravellerGear\",\"Count\":\"invalid\"}", typeof(JsonException))]
    public void FailedReloadPreservesCatalogueAndCanBeRetried(string invalidEntry, Type errorType)
    {
        File.WriteAllText("gear.json", """[{"ClassType":"TravellerGear","Name":"Old","Count":2}]""");
        using GearCatalogue catalogue = new();
        object? oldGear = catalogue.Get("Old");
        Assert.IsNotNull(oldGear);
        File.WriteAllText("gear.json",
            "[{\"ClassType\":\"TravellerGear\",\"Name\":\"Earlier\"}," + invalidEntry +
            ",{\"ClassType\":\"TravellerGear\",\"Name\":\"Later\"}]");

        Exception? failure = null;
        try
        {
            catalogue.Reload();
        }
        catch (Exception exception)
        {
            // Preserve parser subclasses when checking the documented exception family.
            failure = exception;
        }
        Assert.IsNotNull(failure);
        Assert.IsInstanceOfType(failure, errorType);
        Assert.AreSame(oldGear, catalogue.Get("Old"));
        Assert.AreEqual(2m, Data(oldGear).GetProperty("Count").GetDecimal());
        Assert.IsNull(catalogue.Get("Earlier"));
        Assert.IsNull(catalogue.Get("Later"));

        File.WriteAllText("gear.json", """[{"ClassType":"TravellerGear","Name":"Recovered"}]""");
        catalogue.Reload();
        Assert.IsNotNull(catalogue.Get("Recovered"));
        Assert.IsNull(catalogue.Get("Old"));
        Assert.IsNull(catalogue.Get("Earlier"));
        Assert.AreEqual("Old", Data(oldGear).GetProperty("Name").GetString());
        File.WriteAllText("gear.json", "[]");
        catalogue.Reload();
        Assert.IsNull(catalogue.Get("Recovered"));
    }

    /// <summary>Verifies read and parse failures preserve the old catalogue and its object identities.</summary>
    /// <param name="missing">Whether to remove the file rather than provide malformed JSON.</param>
    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void FailedReadOrParsePreservesCatalogue(bool missing)
    {
        File.WriteAllText("gear.json", """[{"ClassType":"TravellerGear","Name":"Old"}]""");
        using GearCatalogue catalogue = new();
        object? oldGear = catalogue.Get("Old");
        Assert.IsNotNull(oldGear);
        if (missing)
        {
            File.Delete("gear.json");
            Assert.ThrowsException<FileNotFoundException>(() => catalogue.Reload());
        }
        else
        {
            File.WriteAllText("gear.json", "{");
            try
            {
                catalogue.Reload();
                Assert.Fail("Malformed JSON should fail the reload.");
            }
            catch (JsonException)
            {
                // JsonDocument may throw a more specific JsonReaderException.
            }
        }

        Assert.AreSame(oldGear, catalogue.Get("Old"));
    }

    /// <summary>Reads gear properties across the deliberately isolated assembly boundary.</summary>
    private static JsonElement Data(object? gear)
    {
        Assert.IsNotNull(gear);
        return JsonSerializer.SerializeToElement(gear, gear.GetType());
    }
}
