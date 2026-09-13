using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;

namespace TravellerTools.Tests;

/// <summary>Verifies dice and roll-table contracts with deterministic inputs.</summary>
[TestClass]
[TestCategory("Unit")]
public class FundamentalsTests
{

    /// <summary>Verifies supported die endpoints, including percentile 100.</summary>
    [TestMethod]
    [DataRow(2, 1)]
    [DataRow(2, 2)]
    [DataRow(100, 100)]
    [DataRow(int.MaxValue - 1, int.MaxValue - 1)]
    public void DieSupportsBoundaryValues(int sides, int value)
    {
        Assert.AreEqual(value, DiceTools.RollOneDie(sides, new FixedRandomSource(value)));
    }

    /// <summary>Rejects unsupported side counts before requesting randomness.</summary>
    [TestMethod]
    [DataRow(int.MinValue)]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(int.MaxValue)]
    public void DiceRejectUnsupportedSides(int sides)
    {
        FixedRandomSource source = new();
        Assert.AreEqual("sides", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => DiceTools.RollOneDie(sides, source)).ParamName);
        Assert.AreEqual("sides", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => DiceTools.RollDice(1, sides, source)).ParamName);
        Dice group = new(6) { Sides = sides };
        Assert.AreEqual("handful", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => DiceTools.RollManyDice(new[] { new Dice(6), group }, source)).ParamName);
    }

    /// <summary>Rejects invalid counts even when preceded by a valid group.</summary>
    [TestMethod]
    [DataRow(int.MinValue)]
    [DataRow(-1)]
    [DataRow(0)]
    public void DiceRejectInvalidCountsBeforeRolling(int count)
    {
        FixedRandomSource source = new();
        Assert.AreEqual("number", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => DiceTools.RollDice(count, 6, source)).ParamName);
        Dice group = new(6) { Count = count };
        Assert.AreEqual("handful", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => DiceTools.RollManyDice(new[] { new Dice(6), group }, source)).ParamName);
    }

    /// <summary>Deliberately passes null to verify runtime argument contracts.</summary>
    [TestMethod]
    public void DiceRejectNullArguments()
    {
        Assert.AreEqual("source", Assert.ThrowsException<ArgumentNullException>(
            () => DiceTools.RollOneDie(6, null!)).ParamName);
        Assert.AreEqual("source", Assert.ThrowsException<ArgumentNullException>(
            () => DiceTools.RollDice(2, 6, null!)).ParamName);
        Assert.AreEqual("source", Assert.ThrowsException<ArgumentNullException>(
            () => DiceTools.RollManyDice(Array.Empty<Dice>(), null!)).ParamName);
        Assert.AreEqual("handful", Assert.ThrowsException<ArgumentNullException>(
            () => DiceTools.RollManyDice(null!, new FixedRandomSource())).ParamName);
        Assert.AreEqual("handful", Assert.ThrowsException<ArgumentException>(
            () => DiceTools.RollManyDice(new[] { new Dice(6), null! }, new FixedRandomSource())).ParamName);
    }

    /// <summary>An empty handful has zero total and consumes no randomness.</summary>
    [TestMethod]
    public void EmptyHandfulReturnsZero()
    {
        Assert.AreEqual(0, DiceTools.RollManyDice(Array.Empty<Dice>(), new FixedRandomSource()));
    }

    /// <summary>Allows a representable total even if other possible outcomes could overflow.</summary>
    [TestMethod]
    public void DiceAllowMaximumRepresentableTotal()
    {
        Assert.AreEqual(int.MaxValue, DiceTools.RollDice(
            2, int.MaxValue - 1, new FixedRandomSource(int.MaxValue - 1, 1)));
        Assert.AreEqual(int.MaxValue, DiceTools.RollManyDice(
            new[] { new Dice(int.MaxValue - 1), new Dice(2) },
            new FixedRandomSource(int.MaxValue - 1, 1)));
    }

    /// <summary>Detects overflow within a group, between groups, and with a maximum count.</summary>
    [TestMethod]
    public void DiceThrowWhenRolledTotalOverflows()
    {
        Assert.ThrowsException<OverflowException>(() => DiceTools.RollDice(
            2, int.MaxValue - 1, new FixedRandomSource(int.MaxValue - 1, 2)));
        Assert.ThrowsException<OverflowException>(() => DiceTools.RollManyDice(
            new[] { new Dice(2, int.MaxValue - 1) },
            new FixedRandomSource(int.MaxValue - 1, 2)));
        Assert.ThrowsException<OverflowException>(() => DiceTools.RollManyDice(
            new[] { new Dice(int.MaxValue - 1), new Dice(2) },
            new FixedRandomSource(int.MaxValue - 1, 2)));
        Assert.ThrowsException<OverflowException>(() => DiceTools.RollDice(
            int.MaxValue, int.MaxValue - 1, new FixedRandomSource(int.MaxValue - 1, 2)));
    }

    /// <summary>Enumerates short ranges at both integer boundaries without counter wraparound.</summary>
    [TestMethod]
    [DataRow(int.MaxValue, int.MaxValue)]
    [DataRow(int.MaxValue - 1, int.MaxValue)]
    [DataRow(int.MinValue, int.MinValue + 1)]
    public void TableRangeSupportsIntegerEndpoints(int start, int end)
    {
        TableRowRange row = new(start, end, "boundary", "boundary");
        List<int> values = row.FullRange();
        Assert.AreEqual((int)((long)end - start + 1), values.Count);
        Assert.AreEqual(start, values[0]);
        Assert.AreEqual(end, values[^1]);
        Assert.IsTrue(row.Matches(start));
        Assert.IsTrue(row.Matches(end));
        RPGTable table = new();
        table.AddRow(row);
        Assert.IsTrue(table.IsUniqueAndContiguous());
    }

    /// <summary>Rejects a range that cannot be represented by the list return type.</summary>
    [TestMethod]
    public void TableRangeRejectsUnrepresentableLength()
    {
        TableRowRange row = new(int.MinValue, int.MaxValue, "wide", "wide");
        Assert.AreEqual("End", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => row.FullRange()).ParamName);
    }

    /// <summary>Retains delayed range validation while identifying the invalid bound.</summary>
    [TestMethod]
    public void InvalidTableRangeIdentifiesStart()
    {
        TableRowRange row = new(2, 1, "invalid", "invalid");
        Assert.AreEqual("Start", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => row.Matches(1)).ParamName);
        Assert.AreEqual("Start", Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => row.FullRange()).ParamName);
    }

    /// <summary>Null rejection leaves both empty and populated tables usable.</summary>
    [TestMethod]
    public void TableRejectsNullWithoutChangingItsRows()
    {
        RPGTable table = new();
        // Deliberately violate the non-null API contract.
        Assert.AreEqual("row", Assert.ThrowsException<ArgumentNullException>(
            () => table.AddRow(null!)).ParamName);
        Assert.IsNull(table.RollOnTable(1));
        TableRowSingle row = new(1, "one", "one");
        Assert.IsTrue(table.AddRow(row));
        Assert.AreEqual("row", Assert.ThrowsException<ArgumentNullException>(
            () => table.AddRow(null!)).ParamName);
        Assert.AreSame(row, table.RollOnTable(1));
        Assert.IsTrue(table.IsUniqueAndContiguous());
    }
    /// <summary>Verifies both endpoints without relying on random sampling.</summary>
    [TestMethod]
    public void RollOneDieReturnsValuesWithinRequestedRange()
    {
        Assert.AreEqual(1, DiceTools.RollOneDie(6, new FixedRandomSource(1)));
        Assert.AreEqual(6, DiceTools.RollOneDie(6, new FixedRandomSource(6)));
    }

    /// <summary>Verifies a one-sided die is rejected by the default overload.</summary>
    [TestMethod]
    public void RollOneDieRejectsFewerThanTwoSides()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => DiceTools.RollOneDie(1));
    }

    /// <summary>Verifies minimum and maximum totals for three six-sided dice.</summary>
    [TestMethod]
    public void RollDiceReturnsValuesWithinRequestedRange()
    {
        Assert.AreEqual(3, DiceTools.RollDice(3, 6, new FixedRandomSource(1, 1, 1)));
        Assert.AreEqual(18, DiceTools.RollDice(3, 6, new FixedRandomSource(6, 6, 6)));
    }

    /// <summary>Verifies the default dice overload rejects an empty group.</summary>
    [TestMethod]
    public void RollDiceRejectsZeroDice()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => DiceTools.RollDice(0, 6));
    }

    /// <summary>Verifies the default group roller rejects an unsupported side count.</summary>
    [TestMethod]
    public void RollDiceRejectsFewerThanTwoSides()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => DiceTools.RollDice(1, 1));
    }

    /// <summary>Verifies supplied individual outcomes are summed in order.</summary>
    [TestMethod]
    public void RollDiceUsesSuppliedRandomSource()
    {
        int result = DiceTools.RollDice(3, 6, new FixedRandomSource(1, 2, 6));

        Assert.AreEqual(9, result);
    }

    /// <summary>Verifies mixed die types consume the expected outcomes and produce one total.</summary>
    [TestMethod]
    public void RollManyDiceUsesEachGroupCountAndSides()
    {
        List<Dice> handful = new() { new Dice(2, 6), new Dice(1, 4) };

        int result = DiceTools.RollManyDice(handful, new FixedRandomSource(1, 2, 3));

        Assert.AreEqual(6, result);
    }

    /// <summary>Verifies mutation to an invalid group count is detected before rolling.</summary>
    [TestMethod]
    public void RollManyDiceRejectsInvalidGroups()
    {
        Dice invalidDice = new(1, 6);
        invalidDice.Count = 0;

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            DiceTools.RollManyDice(new[] { invalidDice }, new FixedRandomSource()));
    }

    /// <summary>Verifies the single-die constructor preserves its six-sided fallback.</summary>
    [TestMethod]
    public void DiceWithInvalidSidesDefaultsToSixSidedSingleDie()
    {
        Dice dice = new(1);

        Assert.AreEqual(6, dice.Sides);
        Assert.AreEqual(1, dice.Count);
    }

    /// <summary>Verifies an invalid count falls back to one while preserving valid sides.</summary>
    [TestMethod]
    public void DiceWithInvalidCountDefaultsToOneDie()
    {
        Dice dice = new(0, 8);

        Assert.AreEqual(8, dice.Sides);
        Assert.AreEqual(1, dice.Count);
    }

    /// <summary>Verifies exact matching and single-value enumeration.</summary>
    [TestMethod]
    public void SingleTableRowMatchesOnlyItsNumber()
    {
        TableRowSingle row = new(7, "seven", "seven");

        Assert.IsTrue(row.Matches(7));
        Assert.IsFalse(row.Matches(6));
        CollectionAssert.AreEqual(new[] { 7 }, row.FullRange());
    }

    /// <summary>Verifies both range endpoints match and adjacent values do not.</summary>
    [TestMethod]
    public void RangeTableRowMatchesInclusiveRange()
    {
        TableRowRange row = new(2, 4, "two to four", "range");

        Assert.IsFalse(row.Matches(1));
        Assert.IsTrue(row.Matches(2));
        Assert.IsTrue(row.Matches(3));
        Assert.IsTrue(row.Matches(4));
        Assert.IsFalse(row.Matches(5));
        CollectionAssert.AreEqual(new[] { 2, 3, 4 }, row.FullRange());
    }

    /// <summary>Verifies reversed bounds fail during matching and enumeration.</summary>
    [TestMethod]
    public void InvalidRangeTableRowIsRejectedWhenUsed()
    {
        TableRowRange row = new(4, 2, "invalid", "invalid");

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => row.Matches(3));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => row.FullRange());
    }

    /// <summary>Verifies insertion order resolves overlaps and unmatched totals return null.</summary>
    [TestMethod]
    public void TableReturnsTheFirstMatchingRow()
    {
        RPGTable table = new();
        TableRow first = new TableRowRange(1, 3, "first", "first");
        TableRow second = new TableRowSingle(3, "second", "second");
        Assert.IsTrue(table.AddRow(first));
        Assert.IsTrue(table.AddRow(second));

        Assert.AreSame(first, table.RollOnTable(3));
        Assert.IsNull(table.RollOnTable(4));
    }

    /// <summary>Verifies duplicate identifiers prevent insertion.</summary>
    [TestMethod]
    public void TableRejectsRowsWithDuplicateUid()
    {
        RPGTable table = new();

        Assert.IsTrue(table.AddRow(new TableRowSingle(1, "first", "same")));
        Assert.IsFalse(table.AddRow(new TableRowSingle(2, "second", "same")));
    }

    /// <summary>Verifies adjacent non-overlapping ranges form a continuous table.</summary>
    [TestMethod]
    public void TableRecognisesUniqueContiguousRanges()
    {
        RPGTable table = new();
        table.AddRow(new TableRowRange(1, 3, "one to three", "first"));
        table.AddRow(new TableRowRange(4, 6, "four to six", "second"));

        Assert.IsTrue(table.IsUniqueAndContiguous());
    }

    /// <summary>Verifies missing and duplicated roll values fail table validation.</summary>
    [TestMethod]
    public void TableRejectsGapsAndOverlaps()
    {
        RPGTable gap = new();
        gap.AddRow(new TableRowRange(1, 3, "one to three", "first"));
        gap.AddRow(new TableRowSingle(5, "five", "second"));

        RPGTable overlap = new();
        overlap.AddRow(new TableRowRange(1, 3, "one to three", "first"));
        overlap.AddRow(new TableRowRange(3, 5, "three to five", "second"));

        Assert.IsFalse(gap.IsUniqueAndContiguous());
        Assert.IsFalse(overlap.IsUniqueAndContiguous());
    }
}
