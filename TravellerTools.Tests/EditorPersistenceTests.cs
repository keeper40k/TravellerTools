using System.Reflection;
using System.Text.Json;
using GearEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkillEditor;
using TravellerTools.CharGen;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies settings and editor file persistence without opening modal dialogs.</summary>
[TestClass]
[TestCategory("Unit")]
[DoNotParallelize]
public class EditorPersistenceTests
{
    // Typed delegates exercise the exact private operations used by the dialog handlers.
    private static readonly Func<string, List<TravellerSkill>> ReadSkills =
        Method<Func<string, List<TravellerSkill>>>(typeof(SkillEditorForm), "ReadSkills");
    private static readonly Action<string, List<TravellerSkill>> WriteSkills =
        Method<Action<string, List<TravellerSkill>>>(typeof(SkillEditorForm), "WriteSkills");
    private static readonly Action<string, List<TravellerGear>> AppendGear =
        Method<Action<string, List<TravellerGear>>>(typeof(GearEditorForm), "AppendGear");
    private static readonly Action<string, List<TravellerGear>> WriteGear =
        Method<Action<string, List<TravellerGear>>>(typeof(GearEditorForm), "WriteGear");

    // Settings use the process working directory; tests restore it before removing their files.
    private string previousDirectory = string.Empty;
    private string temporaryDirectory = string.Empty;

    /// <summary>Creates an isolated directory for settings and editor exports.</summary>
    [TestInitialize]
    public void PrepareDirectory()
    {
        previousDirectory = Directory.GetCurrentDirectory();
        temporaryDirectory = Directory.CreateTempSubdirectory("EditorPersistenceTests-").FullName;
        Directory.SetCurrentDirectory(temporaryDirectory);
    }

    /// <summary>Restores process state and removes this test's files.</summary>
    [TestCleanup]
    public void RestoreDirectory()
    {
        Directory.SetCurrentDirectory(previousDirectory);
        Directory.Delete(temporaryDirectory, recursive: true);
    }

    /// <summary>Verifies absent settings and JSON null leave all existing preferences intact.</summary>
    /// <param name="json">JSON content, or null for a missing file.</param>
    [TestMethod]
    [DataRow(null)]
    [DataRow("null")]
    public void MissingOrNullSettingsPreservePreferences(string? json)
    {
        if (json != null)
        {
            File.WriteAllText("settings.json", json);
        }
        CharGenSettings settings = ChangedSettings();

        settings.LoadSettings();

        AssertChangedSettings(settings);
    }

    /// <summary>Verifies omitted fields use constructor defaults, while unknown or differently cased fields are ignored.</summary>
    /// <param name="json">A partial settings object.</param>
    /// <param name="allowAge">The expected explicit or default age preference.</param>
    [TestMethod]
    [DataRow("{}", false)]
    [DataRow("{\"AllowAgeEditing\":true}", true)]
    [DataRow("{\"allowAgeEditing\":true,\"Unknown\":true}", false)]
    public void SettingsObjectsReplaceFlagsUsingDefaults(string json, bool allowAge)
    {
        File.WriteAllText("settings.json", json);
        CharGenSettings settings = ChangedSettings();

        settings.LoadSettings();

        Assert.IsTrue(settings.PromptOnNewChar);
        Assert.AreEqual(allowAge, settings.AllowAgeEditing);
        Assert.IsFalse(settings.AllowCharacterSurvival);
    }

    /// <summary>Verifies invalid settings fail before any preference is copied.</summary>
    /// <param name="json">Malformed JSON, an incompatible root, or an invalid preference value.</param>
    [TestMethod]
    [DataRow("{")]
    [DataRow("[]")]
    [DataRow("{\"PromptOnNewChar\":true,\"AllowAgeEditing\":\"invalid\"}")]
    public void InvalidSettingsPreservePreferences(string json)
    {
        File.WriteAllText("settings.json", json);
        CharGenSettings settings = ChangedSettings();

        AssertJsonFailure(settings.LoadSettings);

        AssertChangedSettings(settings);
    }

    /// <summary>Verifies settings overwrite existing content and subsequent loads replace earlier flags.</summary>
    [TestMethod]
    public void SettingsSaveOverwritesAndRepeatedLoadReplaces()
    {
        File.WriteAllText("settings.json", "obsolete file content");
        CharGenSettings saved = ChangedSettings();
        saved.SaveSettings();
        CharGenSettings loaded = new();
        loaded.LoadSettings();
        AssertChangedSettings(loaded);

        saved.SetDefaults();
        saved.SaveSettings();
        loaded.LoadSettings();

        Assert.IsTrue(loaded.PromptOnNewChar);
        Assert.IsFalse(loaded.AllowAgeEditing);
        Assert.IsFalse(loaded.AllowCharacterSurvival);
    }

    /// <summary>Verifies skill null and empty arrays yield a new empty replacement list.</summary>
    /// <param name="json">An empty skill-tree representation.</param>
    [TestMethod]
    [DataRow("null")]
    [DataRow("[]")]
    public void EmptySkillsReplaceExistingList(string json)
    {
        File.WriteAllText("skills.json", json);
        List<TravellerSkill> original = new() { new() { Name = "Old" } };
        List<TravellerSkill> skills = original;

        skills = ReadSkills("skills.json");

        Assert.AreEqual(0, skills.Count);
        Assert.AreNotSame(original, skills);
        Assert.AreEqual(1, original.Count);
    }

    /// <summary>Verifies missing or invalid skill files cannot replace an existing list.</summary>
    /// <param name="json">Invalid content, or null for a missing file.</param>
    [TestMethod]
    [DataRow(null)]
    [DataRow("{")]
    [DataRow("{}")]
    public void FailedSkillLoadPreservesExistingList(string? json)
    {
        if (json != null)
        {
            File.WriteAllText("skills.json", json);
        }
        List<TravellerSkill> original = new() { new() { Name = "Old" } };
        List<TravellerSkill> skills = original;
        if (json == null)
        {
            Assert.ThrowsException<FileNotFoundException>(() => skills = ReadSkills("skills.json"));
        }
        else
        {
            AssertJsonFailure(() => skills = ReadSkills("skills.json"));
        }

        Assert.AreSame(original, skills);
        Assert.AreEqual("Old", skills[0].Name);
    }

    /// <summary>Verifies skill export preserves nested data, replaces the file, and reloads independently.</summary>
    [TestMethod]
    public void SkillTreeRoundTripPreservesFieldsAndOrder()
    {
        List<TravellerSkill> source = new()
        {
            new()
            {
                Name = "Parent", HasSpecialisations = true, Level = 2,
                Summary = "Summary", Description = "Description", Referee = "Referee",
                Specialisations = new() { new() { Name = "Child", Level = 3 } }
            },
            new() { Name = "Second" }
        };
        File.WriteAllText("skills.json", "obsolete");
        WriteSkills("skills.json", source);

        List<TravellerSkill> loaded = ReadSkills("skills.json");
        List<TravellerSkill> secondLoad = ReadSkills("skills.json");

        Assert.AreEqual(2, loaded.Count);
        Assert.AreEqual(JsonSerializer.Serialize(source), JsonSerializer.Serialize(loaded));
        Assert.AreNotSame(loaded, secondLoad);
        Assert.AreNotSame(source[0].Specialisations[0], loaded[0].Specialisations[0]);
        WriteSkills("skills.json", new());
        Assert.AreEqual(0, ReadSkills("skills.json").Count);
    }

    /// <summary>Verifies omitted skill properties retain model defaults.</summary>
    [TestMethod]
    public void SkillLoadUsesModelDefaults()
    {
        File.WriteAllText("skills.json", "[{}]");

        TravellerSkill skill = ReadSkills("skills.json")[0];

        Assert.AreEqual(string.Empty, skill.Name);
        Assert.AreEqual(0m, skill.Level);
        Assert.IsFalse(skill.HasSpecialisations);
        Assert.AreEqual(0, skill.Specialisations.Count);
    }

    /// <summary>Verifies gear loading appends repeatedly, keeps old entries, and skips unknown types.</summary>
    [TestMethod]
    public void GearLoadsAppendAndSkipUnknownTypes()
    {
        File.WriteAllText("gear.json", """
            [{"ClassType":"FutureGear","Name":"Skipped"},{"ClassType":"TravellerGear","Name":"New"}]
            """);
        TravellerGear old = new() { Name = "Old" };
        List<TravellerGear> gear = new() { old };

        AppendGear("gear.json", gear);
        AppendGear("gear.json", gear);

        Assert.AreEqual(3, gear.Count);
        Assert.AreSame(old, gear[0]);
        Assert.AreEqual("New", gear[1].Name);
        Assert.AreEqual(1m, gear[1].Count);
        Assert.AreNotSame(gear[1], gear[2]);
        File.WriteAllText("gear.json", "[]");
        AppendGear("gear.json", gear);
        Assert.AreEqual(3, gear.Count);
    }

    /// <summary>Verifies initial read, parse, and root-shape failures leave the current inventory intact.</summary>
    /// <param name="json">Invalid content, or null for a missing file.</param>
    [TestMethod]
    [DataRow(null)]
    [DataRow("{")]
    [DataRow("null")]
    [DataRow("{}")]
    public void FailedGearLoadPreservesExistingEntries(string? json)
    {
        if (json != null)
        {
            File.WriteAllText("gear.json", json);
        }
        TravellerGear old = new() { Name = "Old" };
        List<TravellerGear> gear = new() { old };
        if (json == null)
        {
            Assert.ThrowsException<FileNotFoundException>(() => AppendGear("gear.json", gear));
        }
        else if (json == "{")
        {
            AssertJsonFailure(() => AppendGear("gear.json", gear));
        }
        else
        {
            Assert.ThrowsException<InvalidOperationException>(() => AppendGear("gear.json", gear));
        }

        Assert.AreEqual(1, gear.Count);
        Assert.AreSame(old, gear[0]);
    }

    /// <summary>Verifies a later invalid entry retains both the old inventory and earlier additions.</summary>
    [TestMethod]
    public void FailedGearEntryRetainsPartialAppend()
    {
        File.WriteAllText("gear.json", """
            [{"ClassType":"TravellerGear","Name":"Earlier"},{},{"ClassType":"TravellerGear","Name":"Later"}]
            """);
        List<TravellerGear> gear = new() { new() { Name = "Old" } };

        Assert.ThrowsException<KeyNotFoundException>(() => AppendGear("gear.json", gear));

        CollectionAssert.AreEqual(new[] { "Old", "Earlier" }, gear.Select(item => item.Name).ToArray());
    }

    /// <summary>Verifies gear export overwrites files and preserves supported subtype-specific fields.</summary>
    [TestMethod]
    public void GearRoundTripPreservesSupportedSubtypes()
    {
        List<TravellerGear> source = new()
        {
            new() { Name = "Rifle", GearType = "Gun", Count = 2, Value = 500, Weight = 3000, TechLevel = 5 },
            new TravellerRetirementPay { Amount = 6000 },
            new TravellerStarshipBenefit { Name = "Free Trader", MortgageDuration = 27 }
        };
        File.WriteAllText("gear.json", "obsolete");
        WriteGear("gear.json", source);
        List<TravellerGear> loaded = new();

        AppendGear("gear.json", loaded);

        Assert.AreEqual(3, loaded.Count);
        for (int index = 0; index < source.Count; index++)
        {
            Assert.AreEqual(source[index].GetType(), loaded[index].GetType());
            Assert.AreEqual(JsonSerializer.Serialize(source[index], source[index].GetType()),
                JsonSerializer.Serialize(loaded[index], loaded[index].GetType()));
        }
        Assert.AreEqual(6000m, ((TravellerRetirementPay)loaded[1]).Amount);
        Assert.AreEqual(27, ((TravellerStarshipBenefit)loaded[2]).MortgageDuration);
        WriteGear("gear.json", new());
        Assert.AreEqual("[]", File.ReadAllText("gear.json"));
    }

    /// <summary>Verifies existing output files remain intact when exclusive access prevents saving.</summary>
    /// <param name="editor">The persistence operation under test.</param>
    [TestMethod]
    [DataRow("settings")]
    [DataRow("skills")]
    [DataRow("gear")]
    public void SaveFailuresPropagate(string editor)
    {
        string path = editor + ".json";
        File.WriteAllText(path, "original");
        using (FileStream locked = new(path, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            Action save = editor switch
            {
                "settings" => () => new CharGenSettings().SaveSettings(),
                "skills" => () => WriteSkills(path, new()),
                _ => () => WriteGear(path, new())
            };
            Assert.ThrowsException<IOException>(save);
        }
        Assert.AreEqual("original", File.ReadAllText(path));
    }

    /// <summary>Binds a private persistence method without constructing a form or wrapping its exceptions.</summary>
    private static T Method<T>(Type type, string name) where T : Delegate
    {
        return type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)!.CreateDelegate<T>();
    }

    /// <summary>Accepts JsonException and its more specific parser subclasses.</summary>
    private static void AssertJsonFailure(Action action)
    {
        try
        {
            action();
            Assert.Fail("Expected invalid JSON to fail.");
        }
        catch (JsonException)
        {
            // Parser and serializer failures both satisfy the persistence contract.
        }
    }

    /// <summary>Creates preferences that differ from every constructor default.</summary>
    private static CharGenSettings ChangedSettings() =>
        new() { PromptOnNewChar = false, AllowAgeEditing = true, AllowCharacterSurvival = true };

    /// <summary>Checks that all non-default preferences survived a no-op or failed load.</summary>
    private static void AssertChangedSettings(CharGenSettings settings)
    {
        Assert.IsFalse(settings.PromptOnNewChar);
        Assert.IsTrue(settings.AllowAgeEditing);
        Assert.IsTrue(settings.AllowCharacterSurvival);
    }
}
