using Xunit;
using System;

public class SoundexTests
{
  
     [Fact]
    public void GenerateSoundex_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GenerateSoundex_SingleCharacter_ReturnsPaddedCode()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "A";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("A000", result);
    }

    [Fact]
    public void GenerateSoundex_CharacterNotInService_ReturnsDefaultCode()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "1"; // Assuming 1 is not mapped

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("1000", result);
    }

    [Fact]
    public void GenerateSoundex_SomeCharactersInService_ReturnsEncodedSoundex()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "Jack";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Assuming J -> J, a -> 0, c -> 2, k -> 2
    }

    [Fact]
    public void GenerateSoundex_CharactersWithSameCode_AppliesRulesCorrectly()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "Jacket";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J232", result); // Assuming J -> J, a -> 0, c -> 2, k -> 2, e -> 0, t -> 3
    }

    [Fact]
    public void GenerateSoundex_HandlesSpecialCharacters_ReturnsEncodedSoundex()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "Jack@Home";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Special characters should be ignored
    }

    [Fact]
    public void GenerateSoundex_LongString_ReturnsTruncatedCode()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "JackandJill";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Only the first 4 significant characters
    }

    [Fact]
    public void GenerateSoundex_MixedCharacterTypes_ReturnsCorrectSoundex()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "Jack123";

        // Act
        string result = soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Numbers should be ignored
    }

    [Fact]
    public void IsInvalidInput_EmptyString_ReturnsTrue()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "";

        // Act
        bool result = soundex.IsInvalidInput(input);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsInvalidInput_NullString_ReturnsTrue()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = null;

        // Act
        bool result = soundex.IsInvalidInput(input);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InitializeSoundex_ValidName_ReturnsInitializedSoundex()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        string input = "John";

        // Act
        var result = soundex.InitializeSoundex(input);

        // Assert
        Assert.Equal("J", result.ToString());
    }

    [Fact]
    public void AppendSoundexCharacters_ProcessesCharactersCorrectly()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        var soundexBuilder = new StringBuilder("J");
        char previousCode = 'J';

        // Act
        soundex.AppendSoundexCharacters("John", soundexBuilder, ref previousCode);

        // Assert
        Assert.Equal("J525", soundexBuilder.ToString());
    }

    [Fact]
    public void PadSoundex_AppendsZerosToMatchMaxLength()
    {
        // Arrange
        var soundexService = new SimpleSoundexService();
        var soundex = new Soundex(soundexService);
        var soundexBuilder = new StringBuilder("J");

        // Act
        soundex.PadSoundex(soundexBuilder);

        // Assert
        Assert.Equal("J000", soundexBuilder.ToString());
    }
    [Fact]
    public void HandlesEmptyString()
    {
        Assert.Equal(string.Empty, Soundex.GenerateSoundex(""));
    }

    [Fact]
    public void HandlesSingleCharacter()
    {
        Assert.Equal("A000", Soundex.GenerateSoundex("A"));
    }

   
}
