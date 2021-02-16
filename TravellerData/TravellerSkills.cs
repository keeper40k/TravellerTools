using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TravellerTools.TravellerData
{
    public class TravellerSkills
    {
        // String constants

        private const string SKILLS_FILENAME = "skills.json";

        // Public Static Constructors

        static TravellerSkills()
        {
            string json = File.ReadAllText(SKILLS_FILENAME);
            Skills = JsonSerializer.Deserialize<List<TravellerSkill>>(json);
        }

        // ProtectedMethods

        protected static TravellerSkill InternalMatch(string name, List<TravellerSkill> skills)
        {
            TravellerSkill result = null;
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
                    if( result != null )
                    {
                        break;
                    }
                }
            }
            return result;
        }

        // Public methods

        public static TravellerSkill MatchSkill(string name)
        {
            return InternalMatch(name, Skills);
        }

        // Public static Properties

        static List<TravellerSkill> Skills { get; set; }
    }
}
