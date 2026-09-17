using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies chronological aging, crisis recovery, and termination at death.</summary>
[TestClass]
[TestCategory("Unit")]
public class TravellerCharacterAgingTests
{
    // Protected operations allow each test to start at a specific aging phase.
    private sealed class AgingCharacter : TravellerCharacter
    {
        /// <summary>Constructs the character from a finite sequence of dice results.</summary>
        public AgingCharacter(IRandomSource source) : base(source) { }

        /// <summary>Advances a chosen interval without simulating earlier career terms.</summary>
        public void Advance(decimal start, decimal end) => ProcessAging(start, end);

        /// <summary>Resolves a crisis at a specified chronological age.</summary>
        public void ResolveCrisis(decimal currentAge) => AgingCrisis(currentAge);
    }

    /// <summary>Verifies fractional ages cross the age-34 boundary exactly once.</summary>
    [TestMethod]
    public void FractionalAgeAdvancementChecksCrossedBoundary()
    {
        FixedRandomSource source = Source(4, 4, 3, 4, 4, 4);
        TravellerCharacter character = new(source);
        character.Age = 33.5m;

        character.Age = 34.5m;

        Assert.AreEqual(34.5m, character.Age);
        Assert.AreEqual("CCCCCC", character.UPP);
        Assert.AreEqual(0, source.RemainingCount);
        character.Age = 34.75m;
        Assert.AreEqual(34.75m, character.Age);
    }

    /// <summary>Verifies a phase-three loss from one to minus one triggers a crisis.</summary>
    /// <param name="characteristic">The physical characteristic reduced by two.</param>
    [TestMethod]
    [DataRow("STR")]
    [DataRow("DEX")]
    [DataRow("END")]
    public void NegativePhysicalCharacteristicTriggersCrisis(string characteristic)
    {
        FixedRandomSource source = Source(4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 6);
        AgingCharacter character = new(source);
        switch (characteristic)
        {
            case "STR": character.STR = 1; break;
            case "DEX": character.DEX = 1; break;
            case "END": character.END = 1; break;
        }

        character.Advance(65, 66);

        int restored = characteristic switch
        {
            "STR" => character.STR,
            "DEX" => character.DEX,
            _ => character.END
        };
        Assert.AreEqual(1, restored);
        Assert.IsFalse(character.IsDead);
        Assert.AreEqual(66.5m, character.Age);
        StringAssert.Contains(character.CreationHistory, $"Bob lost 2 {characteristic}\n");
        StringAssert.Contains(character.CreationHistory, "6 months");
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Verifies recovery is added on top of the complete requested normal interval.</summary>
    [TestMethod]
    public void RecoveryIsNotOverwrittenByRequestedFinalAge()
    {
        FixedRandomSource source = Source(3, 4, 3, 4, 4, 4, 4, 4, 6);
        TravellerCharacter character = new(source) { STR = 1 };
        character.Age = 33.5m;

        character.Age = 34.5m;

        Assert.AreEqual(35m, character.Age);
        Assert.AreEqual(1, character.STR);
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Verifies recovery crossing age 38 consumes no aging rolls; subsequent normal time checks age 42.</summary>
    [TestMethod]
    public void RecoveryCrossesThresholdWithoutAnExtraCheck()
    {
        FixedRandomSource source = Source(4, 4, 6, 4, 4, 3, 4, 4, 4);
        AgingCharacter character = new(source) { STR = 0 };

        character.ResolveCrisis(37.75m);

        Assert.AreEqual(38.25m, character.Age);
        Assert.AreEqual(6, source.RemainingCount);
        character.Age = 41.75m;
        Assert.AreEqual(6, source.RemainingCount);
        character.Age = 42.25m;
        Assert.AreEqual(42.25m, character.Age);
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Verifies multiple successful recoveries accumulate whole months before conversion to years.</summary>
    [TestMethod]
    public void MultipleRecoveriesAddAllElapsedMonths()
    {
        FixedRandomSource source = Source(4, 4, 5, 4, 4, 4, 4, 4, 3);
        AgingCharacter character = new(source) { STR = 0, DEX = -1, END = 0 };

        character.ResolveCrisis(34);

        Assert.AreEqual(35m, character.Age);
        Assert.AreEqual(1, character.STR);
        Assert.AreEqual(1, character.DEX);
        Assert.AreEqual(1, character.END);
        StringAssert.Contains(character.CreationHistory, "12 months");
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Verifies the first failed crisis save stops later saves and normal aging.</summary>
    [TestMethod]
    public void DeathStopsAtFailedCheckInsteadOfRequestedAge()
    {
        FixedRandomSource source = Source(3, 4, 3, 3, 3, 4, 3, 4);
        TravellerCharacter character = new(source) { STR = 1, DEX = 1, END = 1 };

        character.Age = 50;

        Assert.IsTrue(character.IsDead);
        Assert.AreEqual(34m, character.Age);
        Assert.AreEqual(0, character.DEX);
        Assert.AreEqual(0, character.END);
        Assert.AreEqual(0, source.RemainingCount);
        StringAssert.Contains(character.CreationHistory, "passed away at the age of 34");
        string history = character.CreationHistory;
        character.Age = 80;
        character.Age = 18;
        Assert.AreEqual(34m, character.Age);
        Assert.AreEqual(history, character.CreationHistory);

        character.Reinitialise();
        Assert.IsFalse(character.IsDead);
        Assert.AreEqual(18m, character.Age);
        character.Age = 22;
        Assert.AreEqual(22m, character.Age);
    }

    /// <summary>Verifies death after an earlier recovery includes that elapsed time and stops remaining saves.</summary>
    [TestMethod]
    public void DeathAfterRecoveryRecordsActualChronologicalAge()
    {
        FixedRandomSource source = Source(4, 4, 6, 3, 4);
        AgingCharacter character = new(source) { STR = 0, DEX = 0, END = 0 };

        character.ResolveCrisis(66);

        Assert.IsTrue(character.IsDead);
        Assert.AreEqual(66.5m, character.Age);
        Assert.AreEqual(1, character.STR);
        Assert.AreEqual(0, character.DEX);
        Assert.AreEqual(0, character.END);
        StringAssert.Contains(character.CreationHistory, "6 months");
        StringAssert.Contains(character.CreationHistory, $"passed away at the age of {character.Age}");
        Assert.AreEqual(0, source.RemainingCount);
        string history = character.CreationHistory;
        character.ResolveCrisis(70);
        character.Advance(66.5m, 80);
        Assert.AreEqual(66.5m, character.Age);
        Assert.AreEqual(history, character.CreationHistory);
    }

    /// <summary>Verifies a long normal interval processes successive boundaries in order.</summary>
    [TestMethod]
    public void NormalAdvancementCrossesMultipleBoundaries()
    {
        FixedRandomSource source = Source(Enumerable.Repeat(6, 12).ToArray());
        TravellerCharacter character = new(source);
        character.Age = 33.5m;

        character.Age = 38.5m;

        Assert.AreEqual(38.5m, character.Age);
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Supplies twelve constructor dice followed by the exact expected aging dice.</summary>
    private static FixedRandomSource Source(params int[] agingRolls)
    {
        return new(Enumerable.Repeat(6, 12).Concat(agingRolls).ToArray());
    }
}
