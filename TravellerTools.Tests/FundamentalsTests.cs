using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;

namespace TravellerTools.Tests;

[TestClass]
public class FundamentalsTests
{
    private sealed class FixedRandomSource : IRandomSource
    {
        private readonly Queue<int> values;

        public FixedRandomSource(params int[] values)
        {
            this.values = new Queue<int>(values);
        }

        public int Next(int minimumValue, int maximumValue)
        {
            int value = values.Dequeue();
            Assert.IsTrue(value >= minimumValue && value < maximumValue);
            return value;
        }
    }

    [TestMethod]
    public void RollOneDieReturnsValuesWithinRequestedRange()
    {
        for (int i = 0; i < 100; i++)
        {
            int result = DiceTools.RollOneDie(6);

            Assert.IsTrue(result >= 1 && result <= 6);
        }
    }

    [TestMethod]
    public void RollOneDieRejectsFewerThanTwoSides()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => DiceTools.RollOneDie(1));
    }

    [TestMethod]
    public void RollDiceReturnsValuesWithinRequestedRange()
    {
        for (int i = 0; i < 100; i++)
        {
            int result = DiceTools.RollDice(3, 6);

            Assert.IsTrue(result >= 3 && result <= 18);
        }
    }

    [TestMethod]
    public void RollDiceRejectsZeroDice()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => DiceTools.RollDice(0, 6));
    }

    [TestMethod]
    public void RollDiceRejectsFewerThanTwoSides()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => DiceTools.RollDice(1, 1));
    }

    [TestMethod]
    public void RollDiceUsesSuppliedRandomSource()
    {
        int result = DiceTools.RollDice(3, 6, new FixedRandomSource(1, 2, 6));

        Assert.AreEqual(9, result);
    }

    [TestMethod]
    public void RollManyDiceUsesEachGroupCountAndSides()
    {
        List<Dice> handful = new() { new Dice(2, 6), new Dice(1, 4) };

        int result = DiceTools.RollManyDice(handful, new FixedRandomSource(1, 2, 3));

        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void RollManyDiceRejectsInvalidGroups()
    {
        Dice invalidDice = new(1, 6);
        invalidDice.Count = 0;

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            DiceTools.RollManyDice(new[] { invalidDice }, new FixedRandomSource()));
    }

    [TestMethod]
    public void DiceWithInvalidSidesDefaultsToSixSidedSingleDie()
    {
        Dice dice = new(1);

        Assert.AreEqual(6, dice.Sides);
        Assert.AreEqual(1, dice.Count);
    }

    [TestMethod]
    public void DiceWithInvalidCountDefaultsToOneDie()
    {
        Dice dice = new(0, 8);

        Assert.AreEqual(8, dice.Sides);
        Assert.AreEqual(1, dice.Count);
    }

    [TestMethod]
    public void SingleTableRowMatchesOnlyItsNumber()
    {
        TableRowSingle row = new(7, "seven", "seven");

        Assert.IsTrue(row.Matches(7));
        Assert.IsFalse(row.Matches(6));
        CollectionAssert.AreEqual(new[] { 7 }, row.FullRange());
    }

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

    [TestMethod]
    public void InvalidRangeTableRowIsRejectedWhenUsed()
    {
        TableRowRange row = new(4, 2, "invalid", "invalid");

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => row.Matches(3));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => row.FullRange());
    }

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

    [TestMethod]
    public void TableRejectsRowsWithDuplicateUid()
    {
        RPGTable table = new();

        Assert.IsTrue(table.AddRow(new TableRowSingle(1, "first", "same")));
        Assert.IsFalse(table.AddRow(new TableRowSingle(2, "second", "same")));
    }

    [TestMethod]
    public void TableRecognisesUniqueContiguousRanges()
    {
        RPGTable table = new();
        table.AddRow(new TableRowRange(1, 3, "one to three", "first"));
        table.AddRow(new TableRowRange(4, 6, "four to six", "second"));

        Assert.IsTrue(table.IsUniqueAndContiguous());
    }

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