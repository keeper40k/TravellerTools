namespace TravellerTools.TravellerData;

/// <summary>Represents a starship award with its remaining mortgage duration.</summary>
public class TravellerStarshipBenefit : TravellerGear
{
    // Private Const Strings

    private const string MortgageFormat = " ({0} year mortgage remaining)";

    // Public Constructor

    /// <summary>Creates an unnamed starship benefit with a forty-year mortgage.</summary>
    public TravellerStarshipBenefit()
    {
        MortgageDuration = 40;
    }

    // Public Override Methods

    /// <summary>Formats the ship name and remaining mortgage.</summary>
    /// <returns>The name alone for exactly 'Scout Ship'; otherwise the name and mortgage duration in years.</returns>
    public override string DisplayString()
    {
        string result = Name;
        if (Name != "Scout Ship")
        {
            result += string.Format(MortgageFormat, MortgageDuration);
        }
        return result;
    }

    // Public Properties

    /// <summary>Gets or sets the remaining mortgage duration in years; defaults to forty.</summary>
    public int MortgageDuration { get; set; }
}
