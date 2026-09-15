using System;

namespace TravellerTools.Fundamentals;

/// <summary>
/// Provides thread-safe dice randomness using <see cref="Random.Shared"/>.
/// </summary>
/// <remarks>Instances may be used concurrently. This source does not provide a reproducible seeded sequence.</remarks>
public sealed class SystemRandomSource : IRandomSource
{
    // Sharing the platform generator keeps all instances safe for concurrent callers.
    private readonly Random random;

    /// <summary>
    /// Initializes a source backed by the shared, thread-safe random number generator.
    /// </summary>
    public SystemRandomSource()
    {
        random = Random.Shared;
    }

    /// <summary>
    /// Returns a random value in the requested half-open interval.
    /// </summary>
    /// <param name="minimumValue">The inclusive lower bound.</param>
    /// <param name="maximumValue">The exclusive upper bound, which must be at least <paramref name="minimumValue"/>.</param>
    /// <returns>A value in the requested interval, or <paramref name="minimumValue"/> when the bounds are equal.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumValue"/> exceeds <paramref name="maximumValue"/>.</exception>
    /// <remarks>This method may be called concurrently from multiple threads.</remarks>
    public int Next(int minimumValue, int maximumValue)
    {
        return random.Next(minimumValue, maximumValue);
    }
}
