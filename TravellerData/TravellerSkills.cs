using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TravellerTools.TravellerData;

/// <summary>Provides shared skill definitions loaded from skills.json in the current working directory.</summary>
/// <remarks>
/// The file is loaded once during type initialization, relative to the working directory at that time.
/// JSON null produces an empty catalogue. Missing files, read failures, and invalid JSON surface through
/// <see cref="TypeInitializationException"/>; initialization is not retried for this loaded type.
/// File changes are not reloaded. Lookups return mutable shared definitions, not copies.
/// Definitions are not validated: traversed lists must be non-null, contain no null entries,
/// and form a finite, acyclic specialisation tree. Concurrent mutation is not supported.
/// </remarks>
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
    /// <param name="name">The non-null name compared case-sensitively using the current culture; no trimming is performed.</param>
    /// <param name="skills">The non-null list of definitions to search.</param>
    /// <returns>The first matching definition by reference, or null when no match exists.</returns>
    /// <remarks>Visits each parent before its children, in list order, so an earlier subtree can match before a later top-level entry. Empty names are allowed. Lists and entries must be non-null and traversed branches must be acyclic.</remarks>
    /// <exception cref="NullReferenceException">A traversed list or entry is null, or name is null when an entry is examined. A null name with an empty list returns null.</exception>
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
    /// <param name="name">The non-null skill name compared case-sensitively using the current culture; no trimming is performed.</param>
    /// <returns>The first matching shared definition, or null.</returns>
    /// <remarks>
    /// Searches parents before children in list order, descending only when HasSpecialisations is true.
    /// Duplicate names return the first match. An empty catalogue or unknown name returns null.
    /// Copy the returned skill before making character-specific changes; mutations otherwise affect future lookups.
    /// Null arguments are not validated: a null name returns null for an empty catalogue but throws
    /// <see cref="NullReferenceException"/> when an entry is examined.
    /// </remarks>
    /// <exception cref="NullReferenceException">The name is null when an entry is examined, or a traversed definition contains a null entry or list.</exception>
    /// <exception cref="TypeInitializationException">The initial skills.json load failed.</exception>
    public static TravellerSkill? MatchSkill(string name)
    {
        return InternalMatch(name, Skills);
    }

    /// <summary>Stores the mutable definitions shared by all lookups.</summary>
    private static List<TravellerSkill> Skills { get; set; } = new();
}
