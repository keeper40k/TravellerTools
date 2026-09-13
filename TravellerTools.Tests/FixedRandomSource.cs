using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.Fundamentals;

namespace TravellerTools.Tests;

/// <summary>Supplies a finite sequence and fails on unexpected random draws.</summary>
internal sealed class FixedRandomSource : IRandomSource
{
    private readonly Queue<int> values;

    /// <summary>Stores the values in the order they should be consumed.</summary>
    public FixedRandomSource(params int[] values)
    {
        this.values = new Queue<int>(values);
    }

    /// <summary>Gets the number of values still available.</summary>
    public int RemainingCount => values.Count;

    /// <summary>Returns the next value after checking the requested bounds.</summary>
    public int Next(int minimumValue, int maximumValue)
    {
        Assert.IsTrue(values.Count > 0, "Unexpected random draw.");
        int value = values.Dequeue();
        Assert.IsTrue(value >= minimumValue && value < maximumValue);
        return value;
    }
}
