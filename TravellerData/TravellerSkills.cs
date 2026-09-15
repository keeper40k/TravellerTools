using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TravellerTools.TravellerData;

/// <summary>Provides shared skill definitions loaded from skills.json in the current working directory.</summary>
/// <remarks>The file is loaded once during type initialization. File and JSON failures surface through TypeInitializationException. Lookups return mutable shared definitions, not copies.</remarks>
public class TravellerSkills
{
    // Shared definitions use the current working directory at first access.
    private const string SkillsFileName = "skills.json";

    /// <summary>Loads the shared definitions once for this type.</summary>
    static TravellerSkills()
    {
        string json = File.ReadAllText(SkillsFileName);
        Skills = JsonSerializer.Deserialize<List<TravellerSkill>>(json) ?? new();
    }

    /// <summary>Searches a skill list depth-first, visiting children only when HasSpecialisations is true.</summary>
    /// <param name="name">The non-null name compared with String.CompareTo.</param>
    /// <param name="skills">The non-null list of definitions to search.</param>
    /// <returns>The first matching definition by reference, or null.</returns>
    protected static TravellerSkill? InternalMatch(string name, List<TravellerSkill> skills)
    {
        TravellerSkill? result = null;
        foreach (TravellerSkill skill in skills)
        {
            if (name.CompareTo(skill.Name) == 0)
            {
                result = skill;
                break;
            }
            if (skill.HasSpecialisations)
            {
                result = InternalMatch(name, skill.Specialisations);
                if (result != null)
                {
                    break;
                }
            }
        }
        return result;
    }

    /// <summary>Finds a named skill or nested specialisation in the shared definitions.</summary>
    /// <param name="name">The non-null skill name compared with String.CompareTo.</param>
    /// <returns>The first matching shared definition, or null.</returns>
    /// <remarks>Copy the returned skill before making character-specific changes.</remarks>
    /// <exception cref="TypeInitializationException">The initial skills.json load failed.</exception>
    public static TravellerSkill? MatchSkill(string name)
    {
        return InternalMatch(name, Skills);
    }

    /// <summary>Stores the mutable definitions shared by all lookups.</summary>
    private static List<TravellerSkill> Skills { get; set; } = new();
}
