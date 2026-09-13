using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerSkillModifier
    {
        // Public Constructor

        public TravellerSkillModifier()
        {
            Name = string.Empty;
            Level = 0;
            IsAttribute = false;
            IsSkill = true;
        }

        // Assumes only one of isSkill or isAttribute is set to true.
        // Skill will be set by default if both or neither are set.
        public TravellerSkillModifier(string name, int level, bool isSkill, bool isAttribute)
        {
            Name = name;
            Level = level;
            IsAttribute = isAttribute;
            IsSkill = isSkill || (isSkill == false && isAttribute == false);
        }

        // Protected member variables backing properties

        protected bool isSkill;
        protected bool isAttribute;

        // Public Properties

        public string Name { get; set;  }
        public int Level { get; set; }
        // Only one of IsSkill or IsAttribute should be set and one should be true
        public bool IsSkill
        {
            get
            {
                return isSkill;
            }
            set
            {
                isSkill = value;
                isAttribute = !value;
            }
        }
        // Only one of IsSkill or IsAttribute should be set and one should be true
        public bool IsAttribute
        {
            get
            {
                return isAttribute;
            }
            set
            {
                isAttribute = value;
                isSkill = !value;
            }
        }
    }
}
