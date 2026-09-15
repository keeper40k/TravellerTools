namespace TravellerTools.TravellerData;

/// <summary>Represents a retirement-pay benefit in a character's inventory.</summary>
public class TravellerRetirementPay : TravellerGear
{

    // private const strings

    private const string RetirementPayName = "Retirement Pay";
    private const string ToStringSeparator = ": Cr";

    // Public constructors

    /// <summary>Creates a benefit named Retirement Pay with an amount of zero.</summary>
    public TravellerRetirementPay()
    {
        Name = RetirementPayName;
        Amount = 0;
    }

    // Public Override Methods

    /// <summary>Formats the benefit's name without its payment amount.</summary>
    /// <returns>The current Name.</returns>
    public override string ToString()
    {
        return Name;
    }

    /// <summary>Formats the benefit name and credit amount.</summary>
    /// <returns>The name followed by ': Cr' and Amount using the current culture.</returns>
    public override string DisplayString()
    {
        return Name + ToStringSeparator + Amount;
    }


    // Public Properties

    /// <summary>Gets or sets the retirement payment amount in credits.</summary>
    public decimal Amount { get; set; }
}
