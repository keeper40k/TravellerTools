using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

[TestClass]
public class SmokeTests
{
    [TestMethod]
    public void RollOneDieReturnsValueWithinRequestedRange()
    {
        int result = DiceTools.RollOneDie(6);

        Assert.IsTrue(result >= 1 && result <= 6);
    }

    [TestMethod]
    public void TravellerRollTargetStoresTarget()
    {
        TravellerRollTarget target = new(8.5m);

        Assert.AreEqual(8.5m, target.Target);
    }
}