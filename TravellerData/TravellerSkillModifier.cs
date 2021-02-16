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
        public TravellerSkillModifier( string name, int level, bool isSkill, bool isAttrubute )
        {
            Name = name;
            Level = level;
            IsAttribute = isAttrubute;
            IsSkill = isSkill || (isSkill == false && isAttrubute == false);
        }

        // Protected member variables backing properties

        protected bool m_isSkill;
        protected bool m_isAttribute;

        // Public Properties

        public string Name { get; set;  }
        public int Level { get; set; }
        // Only one of IsSkill or IsAttribute should be set and one should be true
        public bool IsSkill
        {
            get
            {
                return m_isSkill;
            }
            set
            {
                m_isSkill = value;
                m_isAttribute = !value;
            }
        }
        // Only one of IsSkill or IsAttribute should be set and one should be true
        public bool IsAttribute
        {
            get
            {
                return m_isAttribute;
            }
            set
            {
                m_isAttribute = value;
                m_isSkill = !value;
            }
        }
    }
}
