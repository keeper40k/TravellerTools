using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerTools.CharGen;

namespace TravellerTools.Tests;

[TestClass]
public class CharGenTests
{
    [TestMethod]
    public void CharacterGenerationSettingsHaveExpectedDefaults()
    {
        CharGenSettings settings = new();

        Assert.IsTrue(settings.PromptOnNewChar);
        Assert.IsFalse(settings.AllowAgeEditing);
        Assert.IsFalse(settings.AllowCharacterSurvival);
    }

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
