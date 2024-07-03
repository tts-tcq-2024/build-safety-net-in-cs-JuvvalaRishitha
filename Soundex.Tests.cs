using Xunit;
using System;

public class SoundexTests
{
  
     [Fact]
    public void GenerateSoundex_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        string input = "";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GenerateSoundex_SingleCharacter_ReturnsPaddedCode()
    {
        // Arrange
        string input = "A";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("A000", result);
    }

    [Fact]
    public void GenerateSoundex_CharacterNotInService_ReturnsDefaultCode()
    {
        // Arrange
        string input = "1"; // Assuming 1 is not mapped

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("1000", result);
    }

    [Fact]
    public void GenerateSoundex_SomeCharactersInService_ReturnsEncodedSoundex()
    {
        // Arrange
        string input = "Jack";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Assuming J -> J, a -> 0, c -> 2, k -> 2
    }

    [Fact]
    public void GenerateSoundex_CharactersWithSameCode_AppliesRulesCorrectly()
    {
        // Arrange
        string input = "Jacket";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J232", result); // Assuming J -> J, a -> 0, c -> 2, k -> 2, e -> 0, t -> 3
    }

    [Fact]
    public void GenerateSoundex_HandlesSpecialCharacters_ReturnsEncodedSoundex()
    {
        // Arrange
        string input = "Jack@Home";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Special characters should be ignored
    }

    [Fact]
    public void GenerateSoundex_LongString_ReturnsTruncatedCode()
    {
        // Arrange
        string input = "JackandJill";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Only the first 4 significant characters
    }

    [Fact]
    public void GenerateSoundex_MixedCharacterTypes_ReturnsCorrectSoundex()
    {
        // Arrange
        string input = "Jack123";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Numbers should be ignored
    }

    [Fact]
    public void InitializeSoundex_ValidName_ReturnsInitializedSoundex()
    {
        // Arrange
        string input = "John";

        // Act
        var result = Soundex.InitializeTheSoundex(input);

        // Assert
        Assert.Equal("J", result.ToString());
    }

    [Fact]
    public void AppendSoundexCharacters_ProcessesCharactersCorrectly()
    {
        // Arrange
        var soundexBuilder = new StringBuilder("J");
        char previousCode = 'J';

        // Act
        Soundex.AppendingSoundexCharacters("John", soundexBuilder, ref previousCode);

        // Assert
        Assert.Equal("J525", soundexBuilder.ToString());
    }

    [Fact]
    public void Soundex_AppendsZerosToMatchMaxLength()
    {
        // Arrange
        var soundexBuilder = new StringBuilder("J");

        // Act
        Soundex.SoundexCode(soundexBuilder);

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
