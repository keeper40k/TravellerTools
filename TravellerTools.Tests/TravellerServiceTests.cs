using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.TravellerData;

namespace TravellerTools.Tests;

/// <summary>Verifies career table displays and rank-based awards without file access or random rolls.</summary>
[TestClass]
[TestCategory("Unit")]
public class TravellerServiceTests
{
    /// <summary>Checks that each skill table displays a signed characteristic adjustment exactly once.</summary>
    /// <param name="level">The characteristic adjustment, including integer boundaries.</param>
    /// <param name="expected">The expected signed text in the invariant culture.</param>
    [TestMethod]
    [DataRow(-1, "-1")]
    [DataRow(0, "+0")]
    [DataRow(1, "+1")]
    [DataRow(int.MinValue, "-2147483648")]
    [DataRow(int.MaxValue, "+2147483647")]
    public void SkillTablesFormatSignedAdjustments(int level, string expected)
    {
        CultureInfo previousCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            TravellerService service = new();
            List<KeyValuePair<int, TravellerSkillModifier>> rows = new()
            {
                new(6, new("Pilot", 1, true, false)),
                new(2, new("STR", level, false, true))
            };
            service.PersonalDevelopmentTable = rows;
            service.ServiceSkillsTable = rows;
            service.AdvancedEducationTable = rows;
            service.AdvancedEducationTable2 = rows;

            string expectedTable = $"6    Pilot\n2    {expected} STR";
            Assert.AreEqual(expectedTable, service.PersonalDevelopmentTableText());
            Assert.AreEqual(expectedTable, service.ServiceSkillsTableText());
            Assert.AreEqual(expectedTable, service.AdvancedEducationTableText());
            Assert.AreEqual(expectedTable, service.AdvancedEducationTable2Text());
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }

    /// <summary>Checks that negative numbers retain the current culture's negative sign.</summary>
    [TestMethod]
    public void CharacteristicAdjustmentUsesCurrentCultureNegativeSign()
    {
        CultureInfo previousCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NegativeSign = "~";
            CultureInfo.CurrentCulture = culture;
            TravellerService service = new();
            service.PersonalDevelopmentTable.Add(new(1, new("END", -2, false, true)));

            Assert.AreEqual("1    ~2 END", service.PersonalDevelopmentTableText());
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }

    /// <summary>Preserves the legacy rule that null adjustments are omitted without blank rows.</summary>
    [TestMethod]
    public void SkillTablesSkipNullAdjustments()
    {
        TravellerService service = new();
        service.ServiceSkillsTable.Add(new(1, null!));
        Assert.AreEqual(string.Empty, service.ServiceSkillsTableText());
        service.ServiceSkillsTable.Add(new(2, new("Pilot", 1, true, false)));
        service.ServiceSkillsTable.Add(new(3, null!));

        Assert.AreEqual("2    Pilot", service.ServiceSkillsTableText());
    }

    /// <summary>Checks list order and literal line-feed separators for cash and non-cash awards.</summary>
    [TestMethod]
    public void BenefitTablesPreserveOrderWithoutTrailingLineFeed()
    {
        TravellerService service = new();
        service.CashTable.Add(new(6, 10000));
        service.CashTable.Add(new(1, 1000));
        service.BenefitsTable.Add(new(4, new() { Name = "Weapon" }));
        service.BenefitsTable.Add(new(2, new() { Name = "Passage" }));

        Assert.AreEqual("6    Cr10000\n1    Cr1000", service.CashTableText());
        Assert.AreEqual("4    Weapon\n2    Passage", service.BenefitsTableText());
    }

    /// <summary>Checks both ends of the rank-name list and the argument named by validation.</summary>
    /// <param name="rankIndex">An index outside the single available rank.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(1)]
    public void RankNameRejectsOutOfRangeIndices(int rankIndex)
    {
        TravellerService service = new();
        service.Ranks.Add("Ensign");

        ArgumentOutOfRangeException exception = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => service.RankName(rankIndex));

        Assert.AreEqual("rankIndex", exception.ParamName);
        Assert.AreEqual("Ensign", service.RankName(0));
    }

    /// <summary>Checks that an empty rank list has no valid index.</summary>
    [TestMethod]
    public void EmptyServiceHasNoRanks()
    {
        TravellerService service = new();

        Assert.IsFalse(service.UsesRanks);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => service.RankName(0));
    }

    /// <summary>Checks that every matching award is returned in order in an independent list.</summary>
    [TestMethod]
    public void AutomaticSkillsPreserveAllMatchingAwardsByReference()
    {
        TravellerService service = new();
        TravellerSkillModifier pilot = new("Pilot", 1, true, false);
        TravellerSkillModifier navigation = new("Navigation", 1, true, false);
        service.AutomaticSkills.Add(new(2, pilot));
        service.AutomaticSkills.Add(new(1, new("Gun Combat", 1, true, false)));
        service.AutomaticSkills.Add(new(2, navigation));

        List<TravellerSkillModifier> awards = service.AutomaticSkillsAtRank(2);

        Assert.AreEqual(2, awards.Count);
        Assert.AreSame(pilot, awards[0]);
        Assert.AreSame(navigation, awards[1]);
        awards.Clear();
        Assert.AreEqual(2, service.AutomaticSkillsAtRank(2).Count);
        Assert.AreEqual(0, service.AutomaticSkillsAtRank(-1).Count);
    }
}
