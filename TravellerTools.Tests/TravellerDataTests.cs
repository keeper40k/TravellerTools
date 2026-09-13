using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies Traveller data models, display formats, and repository JSON integration.</summary>
[TestClass]
public class TravellerDataTests
{
    /// <summary>Chooses a named skill without displaying a dialog.</summary>
    private sealed class NamedSkillSelection : ISkillSpecialisationCollection
    {
        private readonly string selectedName;

        /// <summary>Stores the exact child skill to select.</summary>
        public NamedSkillSelection(string selectedName)
        {
            this.selectedName = selectedName;
        }

        /// <summary>Gets how many specialisation choices were requested.</summary>
        public int SelectionCount { get; private set; }

        /// <summary>Returns the requested definition from the supplied choices.</summary>
        public TravellerSkill? SelectSpecialisation(string skillName, List<TravellerSkill> list)
        {
            SelectionCount++;
            return list.Single(skill => skill.Name == selectedName);
        }
    }

    /// <summary>Verifies history names the awarded skill for both new and existing entries.</summary>
    /// <param name="awardName">The skill category or unspecialised skill awarded.</param>
    /// <param name="resolvedName">The specific skill expected in the character and history.</param>
    /// <param name="initialLevel">Zero for a new entry; otherwise the existing skill level.</param>
    [TestMethod]
    [DataRow("Blade Combat", "Cutlass", 0)]
    [DataRow("Blade Combat", "Cutlass", 1)]
    [DataRow("Gun Combat", "Rifle", 0)]
    [DataRow("Gun Combat", "Rifle", 1)]
    [DataRow("Vehicle", "ATV", 0)]
    [DataRow("Vehicle", "ATV", 1)]
    [DataRow("Engineering", "Engineering", 0)]
    [DataRow("Engineering", "Engineering", 1)]
    [DataRow("Pilot", "Pilot", 0)]
    [DataRow("Pilot", "Pilot", 1)]
    public void SkillGainHistoryRecordsResolvedSkill(string awardName, string resolvedName, int initialLevel)
    {
        string previousDirectory = Directory.GetCurrentDirectory();
        string dataDirectory = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..", "CharGen", "JSON"));

        try
        {
            // Shared definitions load relative to the working directory on first use.
            Directory.SetCurrentDirectory(dataDirectory);
            TravellerCharacter character = new() { Name = "Alex", CreationHistory = "Earlier event\n" };
            if (initialLevel > 0)
            {
                character.Skills.Add(new TravellerSkill { Name = resolvedName, Level = initialLevel });
            }
            NamedSkillSelection selection = new(resolvedName);

            character.AddSkill(new TravellerSkillModifier(awardName, 1, true, false), selection);

            Assert.AreEqual(1, character.Skills.Count);
            Assert.AreEqual(resolvedName, character.Skills[0].Name);
            Assert.AreEqual((decimal)initialLevel + 1, character.Skills[0].Level);
            Assert.AreEqual($"Earlier event\nAlex gained 1 {resolvedName}\n", character.CreationHistory);
            Assert.AreEqual(awardName == resolvedName ? 0 : 1, selection.SelectionCount);
        }
        finally
        {
            Directory.SetCurrentDirectory(previousDirectory);
        }
    }

    /// <summary>Verifies inventory defaults and quantity-aware display formatting.</summary>
    [TestMethod]
    public void TravellerGearDefaultsAndFormatsAsItsName()
    {
        TravellerGear gear = new();

        Assert.AreEqual(string.Empty, gear.Name);
        Assert.AreEqual(1m, gear.Count);
        Assert.AreEqual(string.Empty, gear.ToString());

        gear.Name = "Rations";
        Assert.AreEqual("Rations", gear.DisplayString());
        gear.Count = 2;
        Assert.AreEqual("2x Rations", gear.ToString());
    }

    /// <summary>Verifies retirement pay displays its default name and credit amount.</summary>
    [TestMethod]
    public void RetirementPayUsesFixedNameAndDisplayFormat()
    {
        TravellerRetirementPay benefit = new() { Amount = 5000 };

        Assert.AreEqual("Retirement Pay", benefit.ToString());
        Assert.AreEqual("Retirement Pay: Cr5000", benefit.DisplayString());
    }

    /// <summary>Verifies mortgage text is omitted for a Scout Ship.</summary>
    [TestMethod]
    public void StarshipBenefitUsesMortgageTextExceptForScoutShip()
    {
        TravellerStarshipBenefit benefit = new() { Name = "Free Trader", MortgageDuration = 12 };

        Assert.AreEqual("Free Trader (12 year mortgage remaining)", benefit.DisplayString());

        benefit.Name = "Scout Ship";
        Assert.AreEqual("Scout Ship", benefit.DisplayString());
    }

    /// <summary>Verifies new benefit flags and name-only display.</summary>
    [TestMethod]
    public void MusteringOutBenefitDefaultsAndFormatsAsItsName()
    {
        TravellerMusteringOutBenefit benefit = new();

        Assert.AreEqual(string.Empty, benefit.Name);
        Assert.IsFalse(benefit.IsAtt);
        Assert.IsFalse(benefit.IsGear);

        benefit.Name = "Weapon";
        Assert.AreEqual("Weapon", benefit.ToString());
    }

    /// <summary>Verifies adjustment mode setters keep skill and characteristic flags opposite.</summary>
    [TestMethod]
    public void SkillModifierDefaultsToSkillAndPropertiesRemainExclusive()
    {
        TravellerSkillModifier modifier = new();

        Assert.IsTrue(modifier.IsSkill);
        Assert.IsFalse(modifier.IsAttribute);

        modifier.IsAttribute = true;
        Assert.IsTrue(modifier.IsAttribute);
        Assert.IsFalse(modifier.IsSkill);

        modifier.IsSkill = true;
        Assert.IsTrue(modifier.IsSkill);
        Assert.IsFalse(modifier.IsAttribute);
    }

    /// <summary>Verifies neither constructor flag selects skill mode by default.</summary>
    [TestMethod]
    public void SkillModifierConstructorDefaultsToSkillWhenNeitherFlagIsSet()
    {
        TravellerSkillModifier modifier = new("Pilot", 1, false, false);

        Assert.AreEqual("Pilot", modifier.Name);
        Assert.AreEqual(1, modifier.Level);
        Assert.IsTrue(modifier.IsSkill);
        Assert.IsFalse(modifier.IsAttribute);
    }

    /// <summary>Verifies representative behaviours fall into their feeding groups.</summary>
    [TestMethod]
    public void TravellerCreatureClassifiesCreatureTypes()
    {
        TravellerCreature creature = new();

        Assert.IsFalse(creature.IsHerbivore());
        creature.Type = TravellerCreature.CreatureType.Grazer;
        Assert.IsTrue(creature.IsHerbivore());
        creature.Type = TravellerCreature.CreatureType.Hunter;
        Assert.IsTrue(creature.IsOmnivore());
        creature.Type = TravellerCreature.CreatureType.Pouncer;
        Assert.IsTrue(creature.IsCarnivore());
        creature.Type = TravellerCreature.CreatureType.Hijacker;
        Assert.IsTrue(creature.IsScavenger());
    }

    /// <summary>Verifies labels for undefined, carrion-eater, and siren behaviours.</summary>
    [TestMethod]
    public void TravellerCreatureProvidesNamesForTypes()
    {
        Assert.AreEqual("Undefined", TravellerCreature.NameOfType(TravellerCreature.CreatureType.Undefined));
        Assert.AreEqual("Carrion Eater", TravellerCreature.NameOfType(TravellerCreature.CreatureType.Carrion_Eater));

        TravellerCreature creature = new() { Type = TravellerCreature.CreatureType.Siren };
        Assert.AreEqual("Siren", creature.TypeName);
    }

    /// <summary>Verifies initial collections and ordered extended-hexadecimal characteristic output.</summary>
    [TestMethod]
    public void CharacterInitialisesCollectionsAndFormatsCharacteristics()
    {
        TravellerCharacter character = new();
        character.STR = 10;
        character.DEX = 11;
        character.END = 12;
        character.INT = 13;
        character.EDU = 14;
        character.SOC = 15;

        Assert.AreEqual("ABCDEF", character.UPP);
        Assert.IsNotNull(character.Skills);
        Assert.IsNotNull(character.Gear);
        Assert.AreEqual(18m, character.Age);
    }

    /// <summary>Verifies title choices for noble and non-noble social standing.</summary>
    [TestMethod]
    public void CharacterTitlesDependOnSocialStanding()
    {
        TravellerCharacter character = new();

        character.SOC = 11;
        CollectionAssert.AreEqual(new[] { "Knight", "Knightess", "Dame", "Sir", "Lady" }, character.AvailableTitles());

        character.SOC = 10;
        CollectionAssert.AreEqual(new[] { string.Empty }, character.AvailableTitles());
    }

    /// <summary>Verifies repeated skill names combine levels into one entry.</summary>
    [TestMethod]
    public void CharacterAddsAndCombinesSkillsByName()
    {
        TravellerCharacter character = new();

        character.AddSkill(new TravellerSkill { Name = "Pilot", Level = 1 });
        character.AddSkill(new TravellerSkill { Name = "Pilot", Level = 2 });

        Assert.AreEqual(1, character.Skills.Count);
        Assert.AreEqual(3m, character.Skills[0].Level);
        Assert.IsTrue(character.HasSkill("Pilot"));
    }

    /// <summary>Verifies repeated gear names combine quantities into one entry.</summary>
    [TestMethod]
    public void CharacterAddsAndCombinesGearByName()
    {
        TravellerCharacter character = new();

        character.AddGear(new TravellerGear { Name = "Rations", Count = 2 });
        character.AddGear(new TravellerGear { Name = "Rations", Count = 3 });

        Assert.AreEqual(1, character.Gear.Count);
        Assert.AreEqual(5m, character.Gear[0].Count);
    }

    /// <summary>Verifies the legacy null-gear exception contract.</summary>
    [TestMethod]
    public void CharacterRejectsNullGear()
    {
        TravellerCharacter character = new();

        // Deliberately violate the non-null contract to verify runtime validation.
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => character.AddGear(null!));
    }

    /// <summary>Verifies threshold comparison and rejection of an unknown characteristic code.</summary>
    [TestMethod]
    public void CharacteristicRollTargetPassesAgainstSelectedCharacteristic()
    {
        TravellerCharacter character = new() { STR = 8, DEX = 3 };

        Assert.IsTrue(new TravellerCharacteristicRollTarget("STR", 8).Pass(character));
        Assert.IsFalse(new TravellerCharacteristicRollTarget("DEX", 4).Pass(character));
        Assert.IsFalse(new TravellerCharacteristicRollTarget("UNKNOWN", 0).Pass(character));
    }

    /// <summary>Verifies reset clears career gains and restores age eighteen.</summary>
    [TestMethod]
    public void CharacterReinitialiseRestoresCreationState()
    {
        TravellerCharacter character = new();
        character.Name = "Alex";
        character.Cash = 100;
        character.AddSkill(new TravellerSkill { Name = "Pilot", Level = 1 });

        character.Reinitialise();

        Assert.AreEqual(string.Empty, character.Title);
        Assert.AreEqual(0, character.Cash);
        Assert.AreEqual(0, character.Skills.Count);
        Assert.AreEqual(18m, character.Age);
    }

    /// <summary>Verifies extended-hexadecimal boundaries, skipped letters, and saturation at Z.</summary>
    [TestMethod]
    public void CharacterEHexFormattingUsesTravellerDigitsAndCapsAtZ()
    {
        TravellerCharacter character = new();

        Assert.AreEqual("0", character.EHexCharacteristic(0));
        Assert.AreEqual("9", character.EHexCharacteristic(9));
        Assert.AreEqual("A", character.EHexCharacteristic(10));
        Assert.AreEqual("H", character.EHexCharacteristic(17));
        Assert.AreEqual("N", character.EHexCharacteristic(22));
        Assert.AreEqual("P", character.EHexCharacteristic(23));
        Assert.AreEqual("Z", character.EHexCharacteristic(33));
        Assert.AreEqual("Z", character.EHexCharacteristic(99));
    }

    /// <summary>Verifies a characteristic adjustment changes the value and adds a history entry.</summary>
    [TestMethod]
    public void CharacterAddsAttributeSkillAndRecordsHistory()
    {
        TravellerCharacter character = new() { Name = "Alex", STR = 7 };

        // Attribute increases do not use the specialisation callback.
        character.AddSkill(new TravellerSkillModifier("STR", 2, false, true), null!);

        Assert.AreEqual(9, character.STR);
        StringAssert.Contains(character.CreationHistory, "Alex gained 2 STR");
    }

    /// <summary>Verifies displayed table keys, adjustments, and credit amounts.</summary>
    [TestMethod]
    public void ServiceFormatsSkillAndCashTables()
    {
        TravellerService service = new();
        service.PersonalDevelopmentTable.Add(new KeyValuePair<int, TravellerSkillModifier>(1, new TravellerSkillModifier("STR", 1, false, true)));
        service.CashTable.Add(new KeyValuePair<int, decimal>(6, 10000));

        Assert.AreEqual("1    +1 STR", service.PersonalDevelopmentTableText());
        Assert.AreEqual("6    Cr10000", service.CashTableText());
    }

    /// <summary>Verifies every new service table formats as empty text.</summary>
    [TestMethod]
    public void NewServiceHasSafeEmptyTables()
    {
        TravellerService service = new();

        Assert.AreEqual(string.Empty, service.PersonalDevelopmentTableText());
        Assert.AreEqual(string.Empty, service.ServiceSkillsTableText());
        Assert.AreEqual(string.Empty, service.AdvancedEducationTableText());
        Assert.AreEqual(string.Empty, service.AdvancedEducationTable2Text());
        Assert.AreEqual(string.Empty, service.CashTableText());
        Assert.AreEqual(string.Empty, service.BenefitsTableText());
    }

    /// <summary>Verifies rank-index lookup, bounds validation, and automatic adjustments.</summary>
    [TestMethod]
    public void ServiceRankAndAutomaticSkillLookupUseRankIndex()
    {
        TravellerService service = new();
        service.Ranks.Add("Ensign");
        TravellerSkillModifier modifier = new("Pilot", 1, true, false);
        service.AutomaticSkills.Add(new KeyValuePair<int, TravellerSkillModifier>(0, modifier));

        Assert.IsTrue(service.UsesRanks);
        Assert.AreEqual("Ensign", service.RankName(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => service.RankName(1));
        CollectionAssert.AreEqual(new[] { modifier }, service.AutomaticSkillsAtRank(0));
    }

    /// <summary>Verifies a qualifying characteristic produces a service recommendation.</summary>
    [TestMethod]
    public void ServiceRecommendationsIncludePassingCharacteristicBonuses()
    {
        TravellerService service = new() { Name = "Navy" };
        service.Enlistment = new TravellerRollTarget(8);
        service.EnlistmentPlusOne = new TravellerCharacteristicRollTarget("STR", 8);
        TravellerServices services = new();
        services.Services.Add(service);
        TravellerCharacter character = new() { STR = 8 };

        Assert.AreEqual("Navy\nDM+1 Enlistment 8\n\n", services.RecommendText(character));
    }

    /// <summary>Verifies copying preserves skill data while cloning child definitions.</summary>
    [TestMethod]
    public void SkillCopyIncludesNestedSpecialisations()
    {
        TravellerSkill source = new() { Name = "Pilot", HasSpecialisations = true, Level = 2 };
        source.Specialisations.Add(new TravellerSkill { Name = "Pilot (Small Craft)", Level = 1 });

        TravellerSkill copy = new(source);

        Assert.AreEqual(2m, copy.Level);
        Assert.AreEqual(1, copy.Specialisations.Count);
        Assert.AreEqual("Pilot (Small Craft)", copy.Specialisations[0].Name);
        Assert.AreNotSame(source.Specialisations[0], copy.Specialisations[0]);
    }

    /// <summary>Verifies repository gear lookup respects the optional combat category.</summary>
    [TestMethod]
    public void GearStorehouseFiltersWeaponGearByWeaponType()
    {
        string previousDirectory = Directory.GetCurrentDirectory();
        string gearDirectory = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..", "CharGen", "JSON"));

        try
        {
            Directory.SetCurrentDirectory(gearDirectory);

            TravellerGear? rifle = TravellerGearStorehouse.GetGear("Rifle", "Gun Combat");
            TravellerGear? wrongType = TravellerGearStorehouse.GetGear("Rifle", "Blade Combat");
            TravellerGear? nameOnly = TravellerGearStorehouse.GetGear("Rifle", string.Empty);

            Assert.IsNotNull(rifle);
            Assert.IsNull(wrongType);
            Assert.IsNotNull(nameOnly);
        }
        finally
        {
            Directory.SetCurrentDirectory(previousDirectory);
        }
    }

    /// <summary>Verifies service definitions and ranks load from the repository data.</summary>
    [TestMethod]
    public void ServicesLoadFromRepositoryJson()
    {
        string previousDirectory = Directory.GetCurrentDirectory();
        string dataDirectory = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..", "CharGen", "JSON"));

        try
        {
            Directory.SetCurrentDirectory(dataDirectory);
            TravellerServices services = new();

            services.LoadSettings();

            Assert.IsTrue(services.Services.Count > 0);
            Assert.AreEqual("Navy", services.Services[0].Name);
            Assert.IsTrue(services.Services[0].Ranks.Count > 0);
        }
        finally
        {
            Directory.SetCurrentDirectory(previousDirectory);
        }
    }

    /// <summary>Verifies shared skill definitions include their nested choices.</summary>
    [TestMethod]
    public void SkillsLoadFromRepositoryJsonWithSpecialisations()
    {
        string previousDirectory = Directory.GetCurrentDirectory();
        string dataDirectory = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..", "CharGen", "JSON"));

        try
        {
            Directory.SetCurrentDirectory(dataDirectory);

            TravellerSkill? skill = TravellerSkills.MatchSkill("Blade Combat");

            Assert.IsNotNull(skill);
            Assert.IsTrue(skill.HasSpecialisations);
            Assert.IsTrue(skill.Specialisations.Count > 0);
        }
        finally
        {
            Directory.SetCurrentDirectory(previousDirectory);
        }
    }
}
