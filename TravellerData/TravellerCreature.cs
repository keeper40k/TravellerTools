namespace TravellerTools.TravellerData;

/// <summary>Stores creature encounter statistics and a feeding-behaviour classification.</summary>
/// <remarks>Fields are mutable and are not range-validated.</remarks>
public class TravellerCreature
{
    // Local Public Enums

    /// <summary>Identifies encounter behaviours, with numeric bands used for feeding-group classification.</summary>
    public enum CreatureType
    {
        /// <summary>No creature behaviour has been assigned.</summary>
        Undefined = -1,
        /// <summary>A herbivore classified as a filter feeder.</summary>
        Filter = 1,
        /// <summary>A herbivore classified as an intermittent feeder.</summary>
        Intermittent = 2,
        /// <summary>A herbivore classified as a grazer.</summary>
        Grazer = 3,
        /// <summary>An omnivore classified as a gatherer.</summary>
        Gatherer = 104,
        /// <summary>An omnivore classified as a hunter.</summary>
        Hunter = 105,
        /// <summary>An omnivore classified as an eater.</summary>
        Eater = 106,
        /// <summary>A carnivore classified as a pouncer.</summary>
        Pouncer = 207,
        /// <summary>A carnivore classified as a chaser.</summary>
        Chaser = 208,
        /// <summary>A carnivore classified as a trapper.</summary>
        Trapper = 209,
        /// <summary>A carnivore classified as a siren.</summary>
        Siren = 210,
        /// <summary>A carnivore classified as a killer.</summary>
        Killer = 211,
        /// <summary>A scavenger classified as a hijacker.</summary>
        Hijacker = 312,
        /// <summary>A scavenger classified as an intimidator.</summary>
        Intimidator = 313,
        /// <summary>A scavenger classified as a carrion eater.</summary>
        Carrion_Eater = 314,
        /// <summary>A scavenger classified as a reducer.</summary>
        Reducer = 315
    }

    // Preserve the protected names and mutable bounds for existing derived types.

    /// <summary>The inclusive lower numeric bound used to classify herbivore behaviour.</summary>
    protected int HERBIVORE_MIN = 1;
    /// <summary>The inclusive upper numeric bound used to classify herbivore behaviour.</summary>
    protected int HERBIVORE_MAX = 99;
    /// <summary>The inclusive lower numeric bound used to classify omnivore behaviour.</summary>
    protected int OMNIVORE_MIN = 101;
    /// <summary>The inclusive upper numeric bound used to classify omnivore behaviour.</summary>
    protected int OMNIVORE_MAX = 199;
    /// <summary>The inclusive lower numeric bound used to classify carnivore behaviour.</summary>
    protected int CARNIVORE_MIN = 201;
    /// <summary>The inclusive upper numeric bound used to classify carnivore behaviour.</summary>
    protected int CARNIVORE_MAX = 299;
    /// <summary>The inclusive lower numeric bound used to classify scavenger behaviour.</summary>
    protected int SCAVENGER_MIN = 301;
    /// <summary>The inclusive upper numeric bound used to classify scavenger behaviour.</summary>
    protected int SCAVENGER_MAX = 399;

    /// <summary>The display label for the undefined classification.</summary>
    protected const string CREATURE_TYPE_Undefined = "Undefined";
    /// <summary>The display label for the filter classification.</summary>
    protected const string CREATURE_TYPE_Filter = "Filter";
    /// <summary>The display label for the intermittent classification.</summary>
    protected const string CREATURE_TYPE_Intermittent = "Intermittent";
    /// <summary>The display label for the grazer classification.</summary>
    protected const string CREATURE_TYPE_Grazer = "Grazer";
    /// <summary>The display label for the gatherer classification.</summary>
    protected const string CREATURE_TYPE_Gatherer = "Gatherer";
    /// <summary>The display label for the hunter classification.</summary>
    protected const string CREATURE_TYPE_Hunter = "Hunter";
    /// <summary>The display label for the eater classification.</summary>
    protected const string CREATURE_TYPE_Eater = "Eater";
    /// <summary>The display label for the pouncer classification.</summary>
    protected const string CREATURE_TYPE_Pouncer = "Pouncer";
    /// <summary>The display label for the chaser classification.</summary>
    protected const string CREATURE_TYPE_Chaser = "Chaser";
    /// <summary>The display label for the trapper classification.</summary>
    protected const string CREATURE_TYPE_Trapper = "Trapper";
    /// <summary>The display label for the siren classification.</summary>
    protected const string CREATURE_TYPE_Siren = "Siren";
    /// <summary>The display label for the killer classification.</summary>
    protected const string CREATURE_TYPE_Killer = "Killer";
    /// <summary>The display label for the hijacker classification.</summary>
    protected const string CREATURE_TYPE_Hijacker = "Hijacker";
    /// <summary>The display label for the intimidator classification.</summary>
    protected const string CREATURE_TYPE_Intimidator = "Intimidator";
    /// <summary>The display label for the carrion eater classification.</summary>
    protected const string CREATURE_TYPE_Carrion_Eater = "Carrion Eater";
    /// <summary>The display label for the reducer classification.</summary>
    protected const string CREATURE_TYPE_Reducer = "Reducer";

    // Constructors

    /// <summary>Creates a creature with an undefined behaviour classification.</summary>
    public TravellerCreature()
    {
        Type = CreatureType.Undefined;
    }

    // Public Methods

    /// <summary>Checks whether the type falls within the configured herbivore numeric band.</summary>
    /// <returns>True when Type is within the inclusive classification bounds; otherwise false.</returns>
    public bool IsHerbivore()
    {
        return ((int)Type >= HERBIVORE_MIN) && ((int)Type <= HERBIVORE_MAX);
    }

    /// <summary>Checks whether the type falls within the configured omnivore numeric band.</summary>
    /// <returns>True when Type is within the inclusive classification bounds; otherwise false.</returns>
    public bool IsOmnivore()
    {
        return ((int)Type >= OMNIVORE_MIN) && ((int)Type <= OMNIVORE_MAX);
    }

    /// <summary>Checks whether the type falls within the configured carnivore numeric band.</summary>
    /// <returns>True when Type is within the inclusive classification bounds; otherwise false.</returns>
    public bool IsCarnivore()
    {
        return ((int)Type >= CARNIVORE_MIN) && ((int)Type <= CARNIVORE_MAX);
    }

    /// <summary>Checks whether the type falls within the configured scavenger numeric band.</summary>
    /// <returns>True when Type is within the inclusive classification bounds; otherwise false.</returns>
    public bool IsScavenger()
    {
        return ((int)Type >= SCAVENGER_MIN) && ((int)Type <= SCAVENGER_MAX);
    }

    // public Static methods

    /// <summary>Converts a creature behaviour to its display label.</summary>
    /// <param name="type">The behaviour to name.</param>
    /// <returns>The label, including 'Undefined' for that enum value, or an empty string for unrecognized numeric values.</returns>
    public static string NameOfType(CreatureType type)
    {
        return type switch
        {
            CreatureType.Undefined => CREATURE_TYPE_Undefined,
            CreatureType.Filter => CREATURE_TYPE_Filter,
            CreatureType.Intermittent => CREATURE_TYPE_Intermittent,
            CreatureType.Grazer => CREATURE_TYPE_Grazer,
            CreatureType.Gatherer => CREATURE_TYPE_Gatherer,
            CreatureType.Hunter => CREATURE_TYPE_Hunter,
            CreatureType.Eater => CREATURE_TYPE_Eater,
            CreatureType.Pouncer => CREATURE_TYPE_Pouncer,
            CreatureType.Chaser => CREATURE_TYPE_Chaser,
            CreatureType.Trapper => CREATURE_TYPE_Trapper,
            CreatureType.Siren => CREATURE_TYPE_Siren,
            CreatureType.Killer => CREATURE_TYPE_Killer,
            CreatureType.Hijacker => CREATURE_TYPE_Hijacker,
            CreatureType.Intimidator => CREATURE_TYPE_Intimidator,
            CreatureType.Carrion_Eater => CREATURE_TYPE_Carrion_Eater,
            CreatureType.Reducer => CREATURE_TYPE_Reducer,
            _ => string.Empty
        };
    }

    // Public Properties

    /// <summary>The encounter behaviour used for feeding-group classification.</summary>
    public CreatureType Type;

    /// <summary>Gets the display label of the current behaviour, or an empty string for an unrecognized value.</summary>
    public string TypeName
    {
        get
        {
            return TravellerCreature.NameOfType(Type);
        }
    }

    // Creature weight is given in kg
    /// <summary>The creature's weight in kilograms.</summary>
    public int Weight;

    // HitsToUnconscious and TotalHits are the capacity of the creature to take damage
    /// <summary>The damage capacity before unconsciousness.</summary>
    public int HitsToUnconscious;
    /// <summary>The creature's total damage capacity.</summary>
    public int TotalHits;

    /// <summary>The armour description used in encounter displays.</summary>
    public string Armour = string.Empty;

    /// <summary>The creature's outgoing damage rating.</summary>
    public int Wounds;
    /// <summary>The description of the creature's weapons.</summary>
    public string Weapons = string.Empty;

    /// <summary>The encounter-table rating for willingness to attack.</summary>
    public decimal AttackPredisposition;
    /// <summary>The encounter-table rating for willingness to flee.</summary>
    public decimal FleeDisposition;
    /// <summary>The creature's encounter movement rating.</summary>
    public decimal Speed;
}
