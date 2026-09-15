namespace TravellerTools.TravellerData;

/// <summary>Describes an adjustment to either a skill level or a named characteristic.</summary>
public class TravellerSkillModifier
{
    // Public Constructor

    /// <summary>Creates an unnamed, zero-level skill adjustment.</summary>
    public TravellerSkillModifier()
    {
        Name = string.Empty;
        Level = 0;
        IsAttribute = false;
        IsSkill = true;
    }

    // Assumes only one of isSkill or isAttribute is set to true.
    // Skill will be set by default if both or neither are set.
    /// <summary>Creates an adjustment with mutually exclusive skill and characteristic modes.</summary>
    /// <param name="name">The skill name or characteristic code.</param>
    /// <param name="level">The amount to add; negative values represent decreases.</param>
    /// <param name="isSkill">Whether to use skill mode.</param>
    /// <param name="isAttribute">Whether to use characteristic mode when skill mode is false.</param>
    /// <remarks>Skill mode wins when both flags are true and is the default when both are false.</remarks>
    public TravellerSkillModifier(string name, int level, bool isSkill, bool isAttribute)
    {
        Name = name;
        Level = level;
        IsAttribute = isAttribute;
        IsSkill = isSkill || (isSkill == false && isAttribute == false);
    }

    // Protected member variables backing properties

    /// <summary>Backs skill mode; derived types should preserve its opposition to isAttribute.</summary>
    protected bool isSkill;
    /// <summary>Backs characteristic mode; derived types should preserve its opposition to isSkill.</summary>
    protected bool isAttribute;

    // Public Properties

    /// <summary>Gets or sets the skill name or characteristic code affected by the adjustment.</summary>
    public string Name { get; set; }
    /// <summary>Gets or sets the signed amount added to the skill or characteristic.</summary>
    public int Level { get; set; }
    // Only one of IsSkill or IsAttribute should be set and one should be true
    /// <summary>Gets or sets skill mode; assigning it sets IsAttribute to the opposite value.</summary>
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
    /// <summary>Gets or sets characteristic mode; assigning it sets IsSkill to the opposite value.</summary>
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
