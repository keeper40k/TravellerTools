namespace TravellerTools.TravellerData;

/// <summary>Tests a named character characteristic against a threshold.</summary>
public class TravellerCharacteristicRollTarget
{
    // Constructor
    /// <summary>Stores a characteristic code and minimum threshold.</summary>
    /// <param name="stat">The case-sensitive code STR, DEX, END, INT, EDU, or SOC; other codes never pass.</param>
    /// <param name="target">The minimum characteristic value needed to pass.</param>
    public TravellerCharacteristicRollTarget(string stat, decimal target)
    {
        Stat = stat;
        Target = target;
    }

    // Public Methods

    /// <summary>Compares the selected characteristic with the threshold.</summary>
    /// <param name="character">The character to inspect; must be non-null for a recognized characteristic.</param>
    /// <returns>True if the characteristic is at least Target; false for a lower value or an unrecognized code.</returns>
    public bool Pass(TravellerCharacter character)
    {
        // Unknown codes retain the legacy false result without inspecting the character.
        return Stat switch
        {
            "STR" => character.STR >= Target,
            "DEX" => character.DEX >= Target,
            "END" => character.END >= Target,
            "INT" => character.INT >= Target,
            "EDU" => character.EDU >= Target,
            "SOC" => character.SOC >= Target,
            _ => false
        };
    }

    // Properties

    /// <summary>Gets or sets the case-sensitive characteristic code: STR, DEX, END, INT, EDU, or SOC.</summary>
    public string Stat { get; set; }
    /// <summary>Gets or sets the minimum characteristic value needed to pass.</summary>
    public decimal Target { get; set; }
}
