namespace TravellerTools.TravellerData;

/// <summary>Stores a mutable target number for a Traveller roll.</summary>
public class TravellerRollTarget
{
    // Constructor
    /// <summary>Stores a roll target without range validation.</summary>
    /// <param name="target">The target number against which a caller will compare a roll.</param>
    public TravellerRollTarget(decimal target)
    {
        Target = target;
    }

    // Properties
    /// <summary>Gets or sets the target number; this type does not perform the roll.</summary>
    public decimal Target { get; set; }

}
