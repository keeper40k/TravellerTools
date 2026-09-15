using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies service persistence and recommendation contracts independently of repository settings.</summary>
[TestClass]
[TestCategory("Unit")]
[DoNotParallelize]
public class TravellerServicesTests
{
    // The legacy loader resolves its file against process-wide state.
    private string previousDirectory = string.Empty;
    private string settingsDirectory = string.Empty;

    /// <summary>Isolates each test from real settings files.</summary>
    [TestInitialize]
    public void CreateSettingsDirectory()
    {
        previousDirectory = Directory.GetCurrentDirectory();
        settingsDirectory = Directory.CreateTempSubdirectory("TravellerServicesTests-").FullName;
        Directory.SetCurrentDirectory(settingsDirectory);
    }

    /// <summary>Restores the working directory and removes only this test's temporary settings.</summary>
    [TestCleanup]
    public void RemoveSettingsDirectory()
    {
        Directory.SetCurrentDirectory(previousDirectory);
        Directory.Delete(settingsDirectory, recursive: true);
    }

    /// <summary>Verifies absent or empty settings preserve existing entries and the list instance.</summary>
    /// <param name="json">Settings content, or null to leave the file absent.</param>
    [TestMethod]
    [DataRow(null)]
    [DataRow("null")]
    [DataRow("{}")]
    [DataRow("{\"Services\":[]}")]
    public void EmptySettingsLeaveExistingServicesUnchanged(string? json)
    {
        if (json != null)
        {
            File.WriteAllText("services.json", json);
        }
        TravellerService existing = new() { Name = "Navy" };
        List<TravellerService> entries = new() { existing };
        TravellerServices services = new() { Services = entries };

        services.LoadSettings();

        Assert.AreSame(entries, services.Services);
        Assert.AreEqual(1, entries.Count);
        Assert.AreSame(existing, entries[0]);
    }

    /// <summary>Verifies invalid JSON propagates without changing the existing collection.</summary>
    /// <param name="json">Malformed JSON or a value incompatible with the service collection.</param>
    [TestMethod]
    [DataRow("{")]
    [DataRow("[]")]
    public void InvalidSettingsThrowWithoutAppending(string json)
    {
        File.WriteAllText("services.json", json);
        TravellerService existing = new() { Name = "Navy" };
        TravellerServices services = new();
        services.Services.Add(existing);

        Assert.ThrowsException<JsonException>(() => services.LoadSettings());

        Assert.AreEqual(1, services.Services.Count);
        Assert.AreSame(existing, services.Services[0]);
    }

    /// <summary>Verifies saving replaces the file and repeated loads append complete service definitions.</summary>
    [TestMethod]
    public void SaveAndRepeatedLoadsPreserveDataAndAppendEntries()
    {
        File.WriteAllText("services.json", "obsolete settings");
        TravellerService navy = new() { Name = "Navy", Enlistment = new(8) };
        navy.Ranks.Add("Ensign");
        TravellerServices source = new();
        source.Services.Add(navy);

        source.SaveSettings();

        using JsonDocument document = JsonDocument.Parse(File.ReadAllText("services.json"));
        Assert.AreEqual("Navy", document.RootElement.GetProperty("Services")[0].GetProperty("Name").GetString());
        TravellerService existing = new() { Name = "Scout" };
        TravellerServices loaded = new();
        loaded.Services.Add(existing);
        loaded.LoadSettings();
        loaded.LoadSettings();

        Assert.AreEqual(3, loaded.Services.Count);
        Assert.AreSame(existing, loaded.Services[0]);
        for (int index = 1; index < loaded.Services.Count; index++)
        {
            Assert.AreEqual("Navy", loaded.Services[index].Name);
            Assert.AreEqual(8m, loaded.Services[index].Enlistment.Target);
            CollectionAssert.AreEqual(new[] { "Ensign" }, loaded.Services[index].Ranks);
        }
        Assert.AreNotSame(loaded.Services[1], loaded.Services[2]);
    }

    /// <summary>Verifies all qualifying bonuses retain their order and literal line-feed separators.</summary>
    [TestMethod]
    public void RecommendationsIncludeEveryPassingBonusInOrder()
    {
        TravellerService navy = new()
        {
            Name = "Navy",
            Enlistment = new(8),
            Survival = new(5),
            Commission = new(10),
            Promotion = new(8),
            EnlistmentPlusOne = new("STR", 8),
            EnlistmentPlusTwo = new("STR", 8),
            SurvivalPlusTwo = new("STR", 8),
            CommissionPlusOne = new("STR", 8),
            PromotionPlusOne = new("STR", 8)
        };
        TravellerServices services = new();
        services.Services.Add(navy);
        services.Services.Add(new() { Name = "No bonuses" });

        string result = services.RecommendText(new() { STR = 8 });

        Assert.AreEqual("Navy\nDM+2 Enlistment 8\nDM+1 Enlistment 8\nDM+2 Survival 5\nDM+1 Commission 10\nDM+1 Promotion 8\n\n", result);
    }

    /// <summary>Verifies empty collections and failed bonus thresholds produce no recommendation.</summary>
    [TestMethod]
    public void RecommendationsAreEmptyWithoutPassingBonuses()
    {
        TravellerServices services = new();
        TravellerCharacter character = new() { STR = 7 };
        Assert.AreEqual(string.Empty, services.RecommendText(character));
        services.Services.Add(new() { Name = "Navy", EnlistmentPlusOne = new("STR", 8) });

        Assert.AreEqual(string.Empty, services.RecommendText(character));
    }
}
