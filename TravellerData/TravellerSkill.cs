using System.Collections.Generic;

namespace TravellerTools.TravellerData;

/// <summary>Stores a mutable skill definition, level, and nested specialisations.</summary>
public class TravellerSkill
{
    // Public Constructors
    /// <summary>Creates a level-zero skill with empty text and no specialisations.</summary>
    public TravellerSkill()
    {
        InitialiseData();
    }

    /// <summary>Copies a skill and recursively copies its specialisation tree.</summary>
    /// <param name="source">The non-null skill to copy; its specialisation tree must be finite and contain no null entries.</param>
    /// <remarks>Specialisation lists and skill objects are copied rather than shared.</remarks>
    public TravellerSkill(TravellerSkill source)
    {
        InitialiseData();
        Name = source.Name;
        Summary = source.Summary;
        Description = source.Description;
        Referee = source.Referee;
        HasSpecialisations = source.HasSpecialisations;
        Level = source.Level;
        foreach (TravellerSkill specialisation in source.Specialisations)
        {
            Specialisations.Add(new TravellerSkill(specialisation));
        }

    }

    // Protected Methods

    /// <summary>Clears descriptive text and replaces the specialisation list with an empty list.</summary>
    /// <remarks>This reset clears HasSpecialisations but does not reset Level.</remarks>
    protected void InitialiseData()
    {
        Name = string.Empty;
        Summary = string.Empty;
        Description = string.Empty;
        Referee = string.Empty;

        HasSpecialisations = false;
        Specialisations = new();
    }

    // Public Methods

    /// <summary>Formats the skill for selection lists.</summary>
    /// <returns>The skill name, without a level suffix.</returns>
    public override string ToString()
    {
        return Name;
    }

    // Public Properties

    /// <summary>Gets or sets the name used for display and skill matching.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Gets or sets the short description shown during skill selection.</summary>
    public string Summary { get; set; } = string.Empty;
    /// <summary>Gets or sets the full player-facing skill description.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Gets or sets the skill's referee guidance.</summary>
    public string Referee { get; set; } = string.Empty;
    /// <summary>Gets or sets the character's level in this skill.</summary>
    public decimal Level { get; set; }
    /// <summary>Gets or sets whether consumers should inspect the Specialisations list.</summary>
    public bool HasSpecialisations { get; set; }
    /// <summary>Gets or sets the mutable child-skill list; keep it non-null and free of cycles.</summary>
    public List<TravellerSkill> Specialisations { get; set; } = new();
}
