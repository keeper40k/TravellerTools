using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.CharGen;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies character awards never share mutable data with definitions or other characters.</summary>
[TestClass]
[TestCategory("Unit")]
public class BenefitAwardsTests
{
    /// <summary>Repeated awards increase one character's level without resetting it or changing other owners.</summary>
    [TestMethod]
    public void SkillAwardsBelongToEachCharacter()
    {
        TravellerSkill definition = new()
        {
            Name = "Rifle", Level = 4, Summary = "Summary", Description = "Description", Referee = "Referee"
        };
        TravellerCharacter first = new();
        TravellerCharacter second = new();

        first.AddSkill(BenefitAwards.CreateSkill(definition));
        second.AddSkill(BenefitAwards.CreateSkill(definition));
        first.AddSkill(BenefitAwards.CreateSkill(definition));
        first.AddSkill(BenefitAwards.CreateSkill(definition));

        Assert.AreEqual(3m, first.Skills[0].Level);
        Assert.AreEqual(1m, second.Skills[0].Level);
        Assert.AreEqual(4m, definition.Level);
        Assert.AreNotSame(definition, first.Skills[0]);
        Assert.AreNotSame(first.Skills[0], second.Skills[0]);
        Assert.AreEqual(definition.Name, first.Skills[0].Name);
        Assert.AreEqual(definition.Summary, first.Skills[0].Summary);
        Assert.AreEqual(definition.Description, first.Skills[0].Description);
        Assert.AreEqual(definition.Referee, first.Skills[0].Referee);
    }

    /// <summary>Copies remain independent through multiple levels of specialisations.</summary>
    [TestMethod]
    public void SkillAwardsCopyNestedSpecialisations()
    {
        TravellerSkill leaf = new() { Name = "Leaf", Level = 2 };
        TravellerSkill child = new() { Name = "Child", HasSpecialisations = true };
        child.Specialisations.Add(leaf);
        TravellerSkill definition = new() { Name = "Parent", HasSpecialisations = true };
        definition.Specialisations.Add(child);

        TravellerSkill first = BenefitAwards.CreateSkill(definition);
        TravellerSkill second = BenefitAwards.CreateSkill(definition);
        first.Specialisations[0].Specialisations[0].Level = 9;
        first.Specialisations.Add(new() { Name = "Extra" });

        Assert.IsTrue(first.HasSpecialisations);
        Assert.AreEqual(2m, leaf.Level);
        Assert.AreEqual(2m, second.Specialisations[0].Specialisations[0].Level);
        Assert.AreEqual(1, definition.Specialisations.Count);
        Assert.AreEqual(1, second.Specialisations.Count);
    }

    /// <summary>Copies common gear fields and keeps quantity changes local to the awarded inventory.</summary>
    /// <param name="kind">The supported catalogue gear type.</param>
    [TestMethod]
    [DataRow("gear")]
    [DataRow("ship")]
    [DataRow("retirement")]
    public void GearAwardsPreserveDataAndKeepCharactersIndependent(string kind)
    {
        TravellerGear definition = kind switch
        {
            "ship" => new TravellerStarshipBenefit { MortgageDuration = 27 },
            "retirement" => new TravellerRetirementPay { Amount = 6000 },
            _ => new TravellerGear()
        };
        definition.Name = "Award";
        definition.GearType = "Category";
        definition.Description = "Description";
        definition.Count = 2;
        definition.Value = 1200;
        definition.Weight = 750;
        definition.TechLevel = 9;

        TravellerCharacter first = new();
        TravellerCharacter second = new();
        TravellerGear firstAward = BenefitAwards.CreateGear(definition);
        TravellerGear secondAward = BenefitAwards.CreateGear(definition);
        first.AddGear(firstAward);
        second.AddGear(secondAward);

        Assert.AreNotSame(definition, firstAward);
        Assert.AreNotSame(firstAward, secondAward);
        Assert.AreEqual(definition.GetType(), firstAward.GetType());
        Assert.AreEqual(definition.ClassType, firstAward.ClassType);
        Assert.AreEqual(definition.Name, firstAward.Name);
        Assert.AreEqual(definition.GearType, firstAward.GearType);
        Assert.AreEqual(definition.Description, firstAward.Description);
        Assert.AreEqual(definition.Count, firstAward.Count);
        Assert.AreEqual(definition.Value, firstAward.Value);
        Assert.AreEqual(definition.Weight, firstAward.Weight);
        Assert.AreEqual(definition.TechLevel, firstAward.TechLevel);

        first.AddGear(BenefitAwards.CreateGear(definition));
        Assert.AreEqual(4m, first.Gear[0].Count);
        Assert.AreEqual(2m, second.Gear[0].Count);
        Assert.AreEqual(2m, definition.Count);

        if (firstAward is TravellerStarshipBenefit ship)
        {
            Assert.AreEqual(27, ship.MortgageDuration);
            ship.MortgageDuration -= 10;
            Assert.AreEqual(27, ((TravellerStarshipBenefit)secondAward).MortgageDuration);
            Assert.AreEqual(27, ((TravellerStarshipBenefit)definition).MortgageDuration);
        }
        if (firstAward is TravellerRetirementPay retirement)
        {
            Assert.AreEqual(6000m, retirement.Amount);
            retirement.Amount = 8000;
            Assert.AreEqual(6000m, ((TravellerRetirementPay)secondAward).Amount);
            Assert.AreEqual(6000m, ((TravellerRetirementPay)definition).Amount);
        }
    }

    // Future catalogue subtypes need an explicit copy policy to avoid silently losing their data.
    private sealed class CustomGear : TravellerGear
    {
    }

    /// <summary>Unsupported subtypes fail explicitly instead of becoming base gear with lost data.</summary>
    [TestMethod]
    public void UnknownGearSubtypeIsRejected()
    {
        Assert.ThrowsException<NotSupportedException>(() => BenefitAwards.CreateGear(new CustomGear()));
    }
}
