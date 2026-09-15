using System;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen;

// Award objects belong to a character; catalogue definitions and existing inventory do not.
internal static class BenefitAwards
{
    // Copy the entire specialisation tree before assigning the awarded level.
    internal static TravellerSkill CreateSkill(TravellerSkill definition)
    {
        return new TravellerSkill(definition) { Level = 1 };
    }

    // Preserve benefit-specific data and reject unknown subtypes rather than losing their fields.
    internal static TravellerGear CreateGear(TravellerGear definition)
    {
        TravellerGear award = definition switch
        {
            TravellerStarshipBenefit ship when ship.GetType() == typeof(TravellerStarshipBenefit)
                => new TravellerStarshipBenefit { MortgageDuration = ship.MortgageDuration },
            TravellerRetirementPay retirement when retirement.GetType() == typeof(TravellerRetirementPay)
                => new TravellerRetirementPay { Amount = retirement.Amount },
            _ when definition.GetType() == typeof(TravellerGear) => new TravellerGear(),
            _ => throw new NotSupportedException("The gear benefit type does not have an award-copy implementation.")
        };

        award.Name = definition.Name;
        award.GearType = definition.GearType;
        award.Description = definition.Description;
        award.Count = definition.Count;
        award.Value = definition.Value;
        award.Weight = definition.Weight;
        award.TechLevel = definition.TechLevel;
        return award;
    }
}
