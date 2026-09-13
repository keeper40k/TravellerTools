namespace TravellerTools.Fundamentals;

/// <summary>Supplies random integer values for dice operations.</summary>
/// <remarks>Implementations may be deterministic or seeded. Thread safety and sequence reproducibility are implementation-specific.</remarks>
public interface IRandomSource
{
    /// <summary>Returns an integer in the requested half-open interval.</summary>
    /// <param name="minimumValue">The inclusive lower bound.</param>
    /// <param name="maximumValue">The exclusive upper bound; must be at least minimumValue.</param>
    /// <returns>A value at least minimumValue and below maximumValue, or minimumValue when the bounds are equal.</returns>
    /// <remarks>Implementations must respect the requested bounds; dice operations rely on this contract.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException">The lower bound exceeds the upper bound.</exception>
    int Next(int minimumValue, int maximumValue);
}
