using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies nested choices and selector cleanup during character skill awards.</summary>
[TestClass]
[TestCategory("Unit")]
[DoNotParallelize]
public class TravellerCharacterSpecialisationTests
{
    // Restore both process-wide loading context and shared definitions after every test.
    private string previousDirectory = string.Empty;
    private TravellerSkill? root;
    private List<TravellerSkill>? originalChoices;
    private TravellerSkill branch = new();
    private TravellerSkill leaf = new();

    // Allows each test to supply choices or fail without opening a dialog.
    private sealed class SkillSelector : ISkillSpecialisationCollection
    {
        private readonly Func<string, List<TravellerSkill>, TravellerSkill?> select;

        /// <summary>Stores the test's selection behaviour.</summary>
        public SkillSelector(Func<string, List<TravellerSkill>, TravellerSkill?> select)
        {
            this.select = select;
        }

        /// <summary>Delegates a single choice to the test.</summary>
        public TravellerSkill? SelectSpecialisation(string skillName, List<TravellerSkill> list) => select(skillName, list);
    }

    /// <summary>Temporarily supplies a two-level specialisation tree through an existing shared category.</summary>
    [TestInitialize]
    public void PrepareNestedDefinitions()
    {
        previousDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory, "..", "..", "..", "..", "CharGen", "JSON")));
        root = TravellerSkills.MatchSkill("Blade Combat");
        Assert.IsNotNull(root);
        originalChoices = root.Specialisations;
        leaf = new() { Name = "Test leaf", Level = 7 };
        branch = new() { Name = "Test branch", HasSpecialisations = true };
        branch.Specialisations.Add(leaf);
        root.Specialisations = new() { branch };
    }

    /// <summary>Restores the original definitions so other tests observe the repository catalogue.</summary>
    [TestCleanup]
    public void RestoreDefinitions()
    {
        if (root != null && originalChoices != null)
        {
            root.Specialisations = originalChoices;
        }
        Directory.SetCurrentDirectory(previousDirectory);
    }

    /// <summary>Verifies one request per level, leaf awards, and cleanup on successful completion.</summary>
    /// <param name="existingLevel">Zero for a new skill, otherwise its existing level.</param>
    [TestMethod]
    [DataRow(0)]
    [DataRow(2)]
    public void NestedSelectionAwardsLeafExactlyOnce(int existingLevel)
    {
        TravellerCharacter character = CreateCharacter();
        if (existingLevel > 0)
        {
            character.Skills.Add(new() { Name = leaf.Name, Level = existingLevel });
        }
        List<string> requests = new();
        SkillSelector selector = new((name, choices) =>
        {
            requests.Add(name);
            Assert.IsNotNull(character.SpecialisationSelectionCallback);
            return choices[0];
        });

        character.AddSkill(new("Blade Combat", 1, true, false), selector);

        CollectionAssert.AreEqual(new[] { "Blade Combat", branch.Name }, requests);
        Assert.AreEqual(1, character.Skills.Count);
        Assert.AreEqual(leaf.Name, character.Skills[0].Name);
        Assert.AreEqual(existingLevel + 1m, character.Skills[0].Level);
        Assert.AreNotSame(leaf, character.Skills[0]);
        Assert.AreEqual(7m, leaf.Level);
        Assert.AreEqual("Earlier event\nAlex gained 1 Test leaf\n", character.CreationHistory);
        Assert.IsNull(character.SpecialisationSelectionCallback);
    }

    /// <summary>Verifies cancellation and selector exceptions release the callback without awarding a skill.</summary>
    /// <param name="depth">The zero-based choice at which selection stops.</param>
    /// <param name="throws">Whether the selector throws instead of cancelling.</param>
    [TestMethod]
    [DataRow(0, false)]
    [DataRow(1, false)]
    [DataRow(0, true)]
    [DataRow(1, true)]
    public void InterruptedSelectionClearsCallbackAndPreservesAwards(int depth, bool throws)
    {
        TravellerCharacter character = CreateCharacter();
        TravellerSkill existing = new() { Name = leaf.Name, Level = 2 };
        character.Skills.Add(existing);
        int calls = 0;
        InvalidOperationException failure = new("Selection failed.");
        SkillSelector selector = new((name, choices) =>
        {
            if (calls++ == depth)
            {
                if (throws)
                {
                    throw failure;
                }
                return null;
            }
            return choices[0];
        });

        if (throws)
        {
            Assert.AreSame(failure, Assert.ThrowsException<InvalidOperationException>(
                () => character.AddSkill(new("Blade Combat", 1, true, false), selector)));
        }
        else
        {
            character.AddSkill(new("Blade Combat", 1, true, false), selector);
        }

        Assert.AreEqual(depth + 1, calls);
        Assert.AreEqual(1, character.Skills.Count);
        Assert.AreSame(existing, character.Skills[0]);
        Assert.AreEqual(2m, existing.Level);
        Assert.AreEqual("Earlier event\n", character.CreationHistory);
        Assert.AreEqual(7m, leaf.Level);
        Assert.IsNull(character.SpecialisationSelectionCallback);
    }

    /// <summary>Verifies characteristic awards do not invoke the selector and still release it.</summary>
    [TestMethod]
    public void CharacteristicAwardDoesNotRequestSpecialisation()
    {
        TravellerCharacter character = CreateCharacter();
        SkillSelector selector = new((name, choices) => throw new AssertFailedException("Unexpected selection."));

        character.AddSkill(new("STR", 1, false, true), selector);

        Assert.AreEqual(7, character.STR);
        Assert.IsNull(character.SpecialisationSelectionCallback);
    }

    /// <summary>Creates a predictable character without consuming system randomness.</summary>
    private static TravellerCharacter CreateCharacter()
    {
        return new(new FixedRandomSource(Enumerable.Repeat(3, 12).ToArray()))
        {
            Name = "Alex",
            CreationHistory = "Earlier event\n"
        };
    }
}
