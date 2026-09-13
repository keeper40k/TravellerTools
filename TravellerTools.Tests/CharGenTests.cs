using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.CharGen;

namespace TravellerTools.Tests;

/// <summary>Verifies character-generation preferences and JSON persistence.</summary>
[TestClass]
public class CharGenTests
{
    /// <summary>Verifies confirmation is enabled while optional age editing and survival overrides are disabled.</summary>
    [TestMethod]
    public void CharacterGenerationSettingsHaveExpectedDefaults()
    {
        CharGenSettings settings = new();

        Assert.IsTrue(settings.PromptOnNewChar);
        Assert.IsFalse(settings.AllowAgeEditing);
        Assert.IsFalse(settings.AllowCharacterSurvival);
    }

    /// <summary>Verifies settings survive a save and load in an isolated working directory.</summary>
    [TestMethod]
    public void CharacterGenerationSettingsRoundTripThroughJson()
    {
        string previousDirectory = Directory.GetCurrentDirectory();
        string temporaryDirectory = Path.Combine(Path.GetTempPath(), "TravellerToolsTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temporaryDirectory);

        try
        {
            Directory.SetCurrentDirectory(temporaryDirectory);
            CharGenSettings saved = new()
            {
                PromptOnNewChar = false,
                AllowAgeEditing = true,
                AllowCharacterSurvival = true
            };
            saved.SaveSettings();

            CharGenSettings loaded = new();
            loaded.SetDefaults();
            loaded.LoadSettings();

            Assert.IsFalse(loaded.PromptOnNewChar);
            Assert.IsTrue(loaded.AllowAgeEditing);
            Assert.IsTrue(loaded.AllowCharacterSurvival);
        }
        finally
        {
            Directory.SetCurrentDirectory(previousDirectory);
            Directory.Delete(temporaryDirectory, recursive: true);
        }
    }
}
