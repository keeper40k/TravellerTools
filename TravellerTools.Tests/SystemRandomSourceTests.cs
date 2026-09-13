using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;

namespace TravellerTools.Tests;

/// <summary>Checks the default random source's boundary and concurrent-call contracts.</summary>
[TestClass]
[TestCategory("Unit")]
public class SystemRandomSourceTests
{
    /// <summary>Uses single-value intervals so concurrent results are deterministic.</summary>
    [TestMethod]
    public void OneSourceSupportsConcurrentCalls()
    {
        SystemRandomSource source = new();
        int[] results = new int[256];

        Parallel.For(0, results.Length, index =>
        {
            results[index] = source.Next(index, index + 1);
        });

        for (int index = 0; index < results.Length; index++)
        {
            Assert.AreEqual(index, results[index]);
        }
    }

    /// <summary>Equal bounds return that bound, including integer endpoints.</summary>
    [TestMethod]
    [DataRow(int.MinValue)]
    [DataRow(0)]
    [DataRow(int.MaxValue)]
    public void EqualBoundsReturnTheBound(int bound)
    {
        SystemRandomSource source = new();

        Assert.AreEqual(bound, source.Next(bound, bound));
    }

    /// <summary>Reversed bounds retain the underlying Random validation contract.</summary>
    [TestMethod]
    public void ReversedBoundsAreRejected()
    {
        SystemRandomSource source = new();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => source.Next(2, 1));
    }
}
