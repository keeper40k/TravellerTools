using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies character rolls consume the supplied deterministic source.</summary>
[TestClass]
[TestCategory("Unit")]
public class TravellerCharacterRandomnessTests
{
    // Expose existing aging operations without advancing through unrelated earlier phases.
    private sealed class AgingCharacter : TravellerCharacter
    {
        /// <summary>Initializes characteristics from the same source used by aging.</summary>
        public AgingCharacter(IRandomSource source) : base(source)
        {
        }

        /// <summary>Runs the aging check immediately preceding the supplied age.</summary>
        public void CheckAge(int age) => ProcessAging(age - 1, age);

        /// <summary>Resolves an existing zero characteristic without another aging loss.</summary>
        public void CheckCrisis() => AgingCrisis(66);
    }

    /// <summary>Verifies constructor rolls map to characteristics in the documented order.</summary>
    [TestMethod]
    public void ConstructorConsumesSixOrderedTwoDieRolls()
    {
        FixedRandomSource source = new(1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6);
        TravellerCharacter character = new(source);

        Assert.AreEqual("2468AC", character.UPP);
        Assert.AreEqual(18m, character.Age);
        Assert.AreEqual("Bob", character.Name);
        Assert.IsTrue(character.UseTitle);
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Verifies null sources fail at the public constructor boundary.</summary>
    [TestMethod]
    public void ConstructorRejectsNullSource()
    {
        Assert.AreEqual("randomSource",
            Assert.ThrowsException<ArgumentNullException>(() => new TravellerCharacter(null!)).ParamName);
    }

    /// <summary>Verifies reset consumes nothing and rerolls continue the retained sequence.</summary>
    [TestMethod]
    public void ReinitialisePreservesSourcePositionForRerolls()
    {
        FixedRandomSource source = new(Enumerable.Repeat(6, 12).Concat(Enumerable.Repeat(1, 12)).ToArray());
        TravellerCharacter character = new(source);
        character.Reinitialise();

        Assert.AreEqual("CCCCCC", character.UPP);
        Assert.AreEqual(12, source.RemainingCount);
        character.RollRandomCharacteristics();

        Assert.AreEqual("222222", character.UPP);
        Assert.IsFalse(character.UseTitle);
        Assert.AreEqual(0, source.RemainingCount);
    }

    /// <summary>Verifies the Age setter uses the injected source only at eligible ages.</summary>
    [TestMethod]
    public void AgeSetterRollsAtThirtyFourButNotAdjacentOrEarlierAges()
    {
        FixedRandomSource source = new(Enumerable.Repeat(6, 12).Concat(new[] { 3, 4, 3, 3, 3, 4 }).ToArray());
        TravellerCharacter character = new(source);
        character.Age = 33;
        Assert.AreEqual(6, source.RemainingCount);

        character.Age = 34;
        Assert.AreEqual("BBBCCC", character.UPP);
        Assert.AreEqual(0, source.RemainingCount);
        character.Age = 35;
        character.Age = 34;
        Assert.AreEqual(34m, character.Age);
        Assert.IsFalse(character.IsDead);
    }

    /// <summary>Verifies exact save thresholds and losses at each aging phase boundary.</summary>
    /// <param name="age">An eligible age in the selected phase.</param>
    /// <param name="passes">Whether rolls meet each save exactly or fall one short.</param>
    [TestMethod]
    [DataRow(34, true)]
    [DataRow(34, false)]
    [DataRow(50, true)]
    [DataRow(50, false)]
    [DataRow(66, true)]
    [DataRow(66, false)]
    public void AgingPhasesUseExpectedSaveThresholds(int age, bool passes)
    {
        int[] thresholds = age switch
        {
            34 => new[] { 8, 7, 8 },
            50 => new[] { 9, 8, 9 },
            _ => new[] { 9, 9, 9, 9 }
        };
        int[] agingRolls = thresholds.SelectMany(target => new[] { 4, target - 4 - (passes ? 0 : 1) }).ToArray();
        FixedRandomSource source = new(Enumerable.Repeat(6, 12).Concat(agingRolls).ToArray());
        AgingCharacter character = new(source);

        character.CheckAge(age);

        int physical = passes ? 12 : age == 66 ? 10 : 11;
        Assert.AreEqual(physical, character.STR);
        Assert.AreEqual(physical, character.DEX);
        Assert.AreEqual(physical, character.END);
        Assert.AreEqual(!passes && age == 66 ? 11 : 12, character.INT);
        Assert.AreEqual(12, character.EDU);
        Assert.AreEqual(12, character.SOC);
        Assert.AreEqual(0, source.RemainingCount);
        Assert.IsFalse(character.IsDead);
        if (passes)
        {
            Assert.AreEqual(string.Empty, character.CreationHistory);
        }
        else
        {
            StringAssert.Contains(character.CreationHistory, $"Bob lost {12 - physical} STR\n");
        }
    }

    /// <summary>Verifies all crisis saves and recovery-month rolls use the retained source.</summary>
    /// <param name="characteristic">The zero characteristic requiring a save.</param>
    /// <param name="survives">Whether the save meets eight or falls one short.</param>
    [TestMethod]
    [DataRow("STR", true)]
    [DataRow("STR", false)]
    [DataRow("DEX", true)]
    [DataRow("DEX", false)]
    [DataRow("END", true)]
    [DataRow("END", false)]
    [DataRow("INT", true)]
    [DataRow("INT", false)]
    public void AgingCrisesUseSourceForSurvivalAndRecovery(string characteristic, bool survives)
    {
        int[] crisisRolls = survives ? new[] { 4, 4, 5 } : new[] { 3, 4 };
        FixedRandomSource source = new(Enumerable.Repeat(6, 12).Concat(crisisRolls).ToArray());
        AgingCharacter character = new(source);
        switch (characteristic)
        {
            case "STR": character.STR = 0; break;
            case "DEX": character.DEX = 0; break;
            case "END": character.END = 0; break;
            case "INT": character.INT = 0; break;
        }

        character.CheckCrisis();

        int result = characteristic switch
        {
            "STR" => character.STR,
            "DEX" => character.DEX,
            "END" => character.END,
            _ => character.INT
        };
        Assert.AreEqual(survives ? 1 : 0, result);
        Assert.AreEqual(!survives, character.IsDead);
        Assert.AreEqual(0, source.RemainingCount);
        StringAssert.Contains(character.CreationHistory, survives ? "5 months" : "passed away at the age of 66");
        Assert.AreEqual(survives ? 66m + 5m / 12m : 66m, character.Age);
    }
}
