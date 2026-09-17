using System.Globalization;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies skill catalogue loading and lookup without sharing static state between cases.</summary>
[TestClass]
[TestCategory("Unit")]
[DoNotParallelize]
public class TravellerSkillsTests
{
    // The loader uses process-wide working-directory state on its first lookup.
    private string previousDirectory = string.Empty;
    private string settingsDirectory = string.Empty;

    // A separate assembly gives each case a fresh static constructor, including failure cases.
    private sealed class SkillCatalogue : IDisposable
    {
        private readonly AssemblyLoadContext context = new(null, isCollectible: true);
        private readonly Func<string, object?> match;

        /// <summary>Loads the real catalogue implementation without initializing its settings yet.</summary>
        public SkillCatalogue()
        {
            Assembly assembly = context.LoadFromAssemblyPath(typeof(TravellerSkills).Assembly.Location);
            Type type = assembly.GetType(typeof(TravellerSkills).FullName!, throwOnError: true)!;
            // The isolated TravellerSkill has a different runtime type identity. Covariance
            // exposes it as object while preserving reference identity and original exceptions.
            match = type.GetMethod(nameof(TravellerSkills.MatchSkill))!.CreateDelegate<Func<string, object?>>();
        }

        /// <summary>Invokes the actual public lookup in this isolated catalogue.</summary>
        public object? Match(string name) => match(name);

        /// <summary>Requests unloading after the test releases its catalogue references.</summary>
        public void Dispose() => context.Unload();
    }

    /// <summary>Creates a directory containing only this test's settings.</summary>
    [TestInitialize]
    public void CreateSettingsDirectory()
    {
        previousDirectory = Directory.GetCurrentDirectory();
        settingsDirectory = Directory.CreateTempSubdirectory("TravellerSkillsTests-").FullName;
        Directory.SetCurrentDirectory(settingsDirectory);
    }

    /// <summary>Restores the process directory and removes this test's settings.</summary>
    [TestCleanup]
    public void RemoveSettingsDirectory()
    {
        Directory.SetCurrentDirectory(previousDirectory);
        Directory.Delete(settingsDirectory, recursive: true);
    }

    /// <summary>Verifies JSON null and empty arrays both produce an empty catalogue.</summary>
    /// <param name="json">An empty catalogue representation.</param>
    [TestMethod]
    [DataRow("null")]
    [DataRow("[]")]
    public void EmptyCatalogueReturnsNull(string json)
    {
        File.WriteAllText("skills.json", json);
        using SkillCatalogue catalogue = new();

        Assert.IsNull(catalogue.Match("Pilot"));
        Assert.IsNull(catalogue.Match(string.Empty));
        Assert.IsNull(catalogue.Match(null!));
    }

    /// <summary>Verifies missing settings fail initialization and a later file does not retry that type.</summary>
    [TestMethod]
    public void MissingFileFailureIsCached()
    {
        using SkillCatalogue catalogue = new();

        TypeInitializationException first = Assert.ThrowsException<TypeInitializationException>(() => catalogue.Match("Pilot"));
        Assert.IsInstanceOfType<FileNotFoundException>(first.InnerException);
        File.WriteAllText("skills.json", """[{"Name":"Pilot"}]""");

        TypeInitializationException second = Assert.ThrowsException<TypeInitializationException>(() => catalogue.Match("Pilot"));
        Assert.IsInstanceOfType<FileNotFoundException>(second.InnerException);
        using SkillCatalogue freshCatalogue = new();
        Assert.IsNotNull(freshCatalogue.Match("Pilot"));
    }

    /// <summary>Verifies invalid JSON and incompatible shapes are wrapped as initialization failures.</summary>
    /// <param name="json">Malformed or incompatible settings.</param>
    [TestMethod]
    [DataRow("{")]
    [DataRow("{}")]
    [DataRow("[{\"Level\":\"invalid\"}]")]
    public void InvalidJsonFailsInitialization(string json)
    {
        File.WriteAllText("skills.json", json);
        using SkillCatalogue catalogue = new();

        TypeInitializationException failure = Assert.ThrowsException<TypeInitializationException>(() => catalogue.Match("Pilot"));

        Assert.IsInstanceOfType<JsonException>(failure.InnerException);
    }

    /// <summary>Verifies first use determines the directory and later file changes do not reload definitions.</summary>
    [TestMethod]
    public void CatalogueLoadsOnceFromDirectoryAtFirstLookup()
    {
        using SkillCatalogue catalogue = new();
        string firstUseDirectory = Directory.CreateDirectory(Path.Combine(settingsDirectory, "first-use")).FullName;
        Directory.SetCurrentDirectory(firstUseDirectory);
        File.WriteAllText("skills.json", """[{"Name":"Pilot"}]""");

        object? pilot = catalogue.Match("Pilot");
        Assert.IsNotNull(pilot);
        File.WriteAllText("skills.json", "{");
        Directory.SetCurrentDirectory(settingsDirectory);

        Assert.AreSame(pilot, catalogue.Match("Pilot"));
        Assert.IsNull(catalogue.Match("Unknown"));
    }

    /// <summary>Verifies parent-first, depth-first traversal and first-match handling of duplicate names.</summary>
    [TestMethod]
    public void LookupUsesParentFirstDepthFirstListOrder()
    {
        File.WriteAllText("skills.json", """
            [
              {"Name":"Parent","HasSpecialisations":true,"Specialisations":[
                {"Name":"Parent","Summary":"child duplicate"},
                {"Name":"Branch","HasSpecialisations":true,"Specialisations":[
                  {"Name":"Target","Summary":"nested first"}
                ]}
              ]},
              {"Name":"Target","Summary":"later top-level"},
              {"Name":"Parent","Summary":"later duplicate"}
            ]
            """);
        using SkillCatalogue catalogue = new();

        Assert.AreEqual(string.Empty, Summary(catalogue.Match("Parent")));
        Assert.AreEqual("nested first", Summary(catalogue.Match("Target")));
        Assert.IsNull(catalogue.Match("Missing"));
    }

    /// <summary>Verifies the specialisation flag controls descent even when child definitions exist.</summary>
    [TestMethod]
    public void DisabledSpecialisationsAreNotSearched()
    {
        File.WriteAllText("skills.json", """
            [{"Name":"Parent","HasSpecialisations":false,"Specialisations":[{"Name":"Hidden"}]}]
            """);
        using SkillCatalogue catalogue = new();

        Assert.IsNotNull(catalogue.Match("Parent"));
        Assert.IsNull(catalogue.Match("Hidden"));
    }

    /// <summary>Verifies lookups expose the same mutable definition rather than making copies.</summary>
    [TestMethod]
    public void ReturnedDefinitionsAreShared()
    {
        File.WriteAllText("skills.json", """[{"Name":"Pilot"}]""");
        using SkillCatalogue catalogue = new();
        object? pilot = catalogue.Match("Pilot");
        Assert.IsNotNull(pilot);

        // Reflection is required because this object's assembly is deliberately isolated.
        pilot.GetType().GetProperty(nameof(TravellerSkill.Summary))!.SetValue(pilot, "Changed");

        Assert.AreSame(pilot, catalogue.Match("Pilot"));
        Assert.AreEqual("Changed", Summary(catalogue.Match("Pilot")));
    }

    /// <summary>Verifies matching is case-sensitive, does not trim, and permits empty names.</summary>
    [TestMethod]
    public void LookupPreservesNameSpellingAndWhitespace()
    {
        File.WriteAllText("skills.json", """[{"Name":"Pilot"},{"Name":""}]""");
        using SkillCatalogue catalogue = new();

        Assert.IsNotNull(catalogue.Match("Pilot"));
        Assert.IsNotNull(catalogue.Match(string.Empty));
        Assert.IsNull(catalogue.Match("pilot"));
        Assert.IsNull(catalogue.Match(" Pilot "));
        Assert.ThrowsException<NullReferenceException>(() => catalogue.Match(null!));
    }

    /// <summary>Verifies lookup retains linguistic rather than ordinal name comparison.</summary>
    [TestMethod]
    public void LookupUsesCurrentCultureComparison()
    {
        CultureInfo previousCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            File.WriteAllText("skills.json", """[{"Name":"Caf\u00e9"}]""");
            using SkillCatalogue catalogue = new();

            Assert.IsNotNull(catalogue.Match("Cafe\u0301"));
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }

    /// <summary>Verifies structurally invalid definitions are not validated during loading.</summary>
    /// <param name="json">Definitions containing a traversed null entry or null child list.</param>
    [TestMethod]
    [DataRow("[null]")]
    [DataRow("[{\"Name\":\"Parent\",\"HasSpecialisations\":true,\"Specialisations\":null}]")]
    public void InvalidDefinitionsFailDuringTraversal(string json)
    {
        File.WriteAllText("skills.json", json);
        using SkillCatalogue catalogue = new();

        Assert.ThrowsException<NullReferenceException>(() => catalogue.Match("Missing"));
    }

    /// <summary>Reads descriptive data across the isolated assembly boundary.</summary>
    private static string? Summary(object? skill)
    {
        Assert.IsNotNull(skill);
        return JsonSerializer.SerializeToElement(skill, skill.GetType()).GetProperty("Summary").GetString();
    }
}
