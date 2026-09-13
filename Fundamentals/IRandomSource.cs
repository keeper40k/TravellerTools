namespace TravellerTools.Fundamentals;

/// <summary>
/// Supplies random integer values for dice operations.
/// </summary>
public interface IRandomSource
{
    /// <summary>
    /// Returns a value in the half-open interval from <paramref name="minimumValue"/> to <paramref name="maximumValue"/>.
    /// </summary>
    int Next(int minimumValue, int maximumValue);
}
