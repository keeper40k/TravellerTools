using System;

namespace TravellerTools.Fundamentals;

/// <summary>
/// Provides dice randomness using <see cref="Random"/>.
/// </summary>
public sealed class SystemRandomSource : IRandomSource
{
    private readonly Random random;

    /// <summary>
    /// Initializes a source with a time-based seed.
    /// </summary>
    public SystemRandomSource()
    {
        random = new Random();
    }

    /// <summary>
    /// Returns a random value in the requested half-open interval.
    /// </summary>
    public int Next(int minimumValue, int maximumValue)
    {
        return random.Next(minimumValue, maximumValue);
    }
}