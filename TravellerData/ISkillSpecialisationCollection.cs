using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
{
    /// <summary>Provides a caller-defined choice when a skill has alternative specialisations.</summary>
    public interface ISkillSpecialisationCollection
    {
        /// <summary>Chooses a specialisation from the supplied alternatives.</summary>
        /// <param name="skillName">The parent skill name used to describe the choice.</param>
        /// <param name="list">The available specialisations; implementations may require a non-empty list.</param>
        /// <returns>The chosen skill, or null when no choice is made.</returns>
        /// <remarks>Implementations may display a modal dialog. Returned skills can themselves have specialisations; callers decide how to resolve further choices.</remarks>
        TravellerSkill? SelectSpecialisation(string skillName, List<TravellerSkill> list);
    }
}
