namespace TravellerTools.TravellerData;

/// <summary>Stores a mutable item or benefit in a character's inventory.</summary>
/// <remarks>Numeric properties are stored without range validation. Display formatting uses the current culture.</remarks>
public class TravellerGear
{
    // private const strings

    private const string CountPrefix = "{0}x ";

    // Public Constructors

    /// <summary>Creates one unnamed item with zero value, weight, and technology level.</summary>
    public TravellerGear()
    {
        GearType = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        Count = 1;
        Value = 0;
        Weight = 0;
        TechLevel = 0;
    }

    // Public override methods
    /// <summary>Formats the inventory item's name with a quantity prefix when Count exceeds one.</summary>
    /// <returns>The name, prefixed with the count and 'x ' when applicable.</returns>
    public override string ToString()
    {
        string result = string.Empty;
        if (Count > 1)
        {
            result += string.Format(CountPrefix, Count);
        }
        result += Name;

        return result;
    }

    // Public Methods

    /// <summary>Formats the item for character and inventory displays.</summary>
    /// <returns>The result of ToString; derived benefits may include additional details.</returns>
    public virtual string DisplayString()
    {
        return ToString();
    }

    // Public Properties

    /// <summary>Gets the runtime type name used to identify gear subclasses in JSON.</summary>
    /// <remarks>The setter intentionally ignores input; it exists so the property participates in serialization.</remarks>
    public string ClassType
    {
        get
        {
            return GetType().Name;
        }
        set
        {
            // Do nothing, except ensure it is serialised by having both get and set.
        }
    }
    /// <summary>Gets or sets the category used to group gear and match weapon skills.</summary>
    public string GearType { get; set; }
    /// <summary>Gets or sets the item name used for display and inventory matching.</summary>
    public string Name { get; set; }
    /// <summary>Gets or sets the descriptive rules text for the item.</summary>
    public string Description { get; set; }

    /// <summary>Gets or sets the quantity represented by this inventory entry; defaults to one.</summary>
    public decimal Count { get; set; }

    /// <summary>Gets or sets the item's credit value.</summary>
    public int Value { get; set; }
    // Weight is measured in grams
    /// <summary>Gets or sets the item's weight in grams.</summary>
    public int Weight { get; set; }
    /// <summary>Gets or sets the technology level associated with the item.</summary>
    public decimal TechLevel { get; set; }
}
