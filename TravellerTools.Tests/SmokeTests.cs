using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

[TestClass]
public class SmokeTests
{
    /// <summary>Resolves a representative 2d6 encounter through a complete table.</summary>
    [TestMethod]
    [TestCategory("Functional")]
    public void DiceRollResolvesAnEncounterTable()
    {
        RPGTable table = new();
        table.AddRow(new TableRowRange(2, 5, "Quiet journey", "quiet"));
        TableRowRange encounter = new(6, 8, "Merchant convoy", "merchants");
        table.AddRow(encounter);
        table.AddRow(new TableRowRange(9, 12, "Patrol", "patrol"));
        FixedRandomSource source = new(3, 4);

        Assert.IsTrue(table.IsUniqueAndContiguous());
        int result = DiceTools.RollDice(2, 6, source);

        Assert.AreEqual(7, result);
        Assert.AreSame(encounter, table.RollOnTable(result));
        Assert.AreEqual(0, source.RemainingCount);
    }

    [TestMethod]
    public void TravellerRollTargetStoresTarget()
    {
        TravellerRollTarget target = new(8.5m);

        Assert.AreEqual(8.5m, target.Target);
    }
}
