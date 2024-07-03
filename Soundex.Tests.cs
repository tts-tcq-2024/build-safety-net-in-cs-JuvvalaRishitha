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
    public void GenerateSoundex_ValidName_ReturnsCorrectSoundex()
    {
        // Arrange
        string input = "Jack";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // J -> J, a -> 0, c -> 2, k -> 2
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
    public void GenerateSoundex_NumbersInString_ReturnsCorrectSoundex()
    {
        // Arrange
        string input = "Jack123";

        // Act
        string result = Soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("J252", result); // Numbers should be ignored
    }

    [Fact]
    public void InitializeTheSoundex_ValidName_ReturnsInitializedSoundex()
    {
        // Arrange
        string input = "John";

        // Act
        var result = Soundex.InitializeTheSoundex(input);

        // Assert
        Assert.Equal("J", result.ToString()); // Only the first character should be included
    }

    [Fact]
    public void AppendingSoundexCharacters_ProcessesCharactersCorrectly()
    {
        // Arrange
        var soundexBuilder = new StringBuilder("J");
        char prevCode = 'J';

        // Act
        Soundex.AppendingSoundexCharacters("John", soundexBuilder, prevCode);

        // Assert
        Assert.Equal("J530", soundexBuilder.ToString()); // J -> J, o -> 0, h -> 0, n -> 5
    }

    [Fact]
    public void Characters_AppendsCorrectCode()
    {
        // Arrange
        var soundexBuilder = new StringBuilder("J");
        char prevCode = 'J';

        // Act
        Soundex.Characters('a', soundexBuilder, prevCode);

        // Assert
        Assert.Equal("J0", soundexBuilder.ToString()); // Should append 0 for 'a'
    }

    [Fact]
    public void AppendCode_ReturnsTrueForDifferentCode()
    {
        // Arrange
        char code = '1';
        char prevCode = '0';

        // Act
        bool result = Soundex.AppendCode(code, prevCode);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AppendCode_ReturnsFalseForSameCode()
    {
        // Arrange
        char code = '1';
        char prevCode = '1';

        // Act
        bool result = Soundex.AppendCode(code, prevCode);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void SoundexCode_AppendsZerosToMatchMaxLength()
    {
        // Arrange
        var soundexBuilder = new StringBuilder("J");

        // Act
        Soundex.SoundexCode(soundexBuilder);

        // Assert
        Assert.Equal("J000", soundexBuilder.ToString());
    }

    [Fact]
    public void GetSoundexCode_ValidCharacter_ReturnsCorrectCode()
    {
        // Arrange
        char character = 'B';

        // Act
        char result = Soundex.GetSoundexCode(character);

        // Assert
        Assert.Equal('1', result);
    }

    [Fact]
    public void GetSoundexCode_UnknownCharacter_ReturnsZero()
    {
        // Arrange
        char character = 'X';

        // Act
        char result = Soundex.GetSoundexCode(character);

        // Assert
        Assert.Equal('2', result);
    }

    [Fact]
    public void GenerateSoundex_CaseInsensitivity_ReturnsSameCode()
    {
        // Arrange
        string inputLowercase = "jack";
        string inputUppercase = "JACK";

        // Act
        string resultLowercase = Soundex.GenerateSoundex(inputLowercase);
        string resultUppercase = Soundex.GenerateSoundex(inputUppercase);

        // Assert
        Assert.Equal(resultLowercase, resultUppercase);
    }

    [Fact]
    public void GenerateSoundex_SingleLetterWithDifferentCases_ReturnsPaddedCode()
    {
        // Arrange
        string inputLowercase = "a";
        string inputUppercase = "A";

        // Act
        string resultLowercase = Soundex.GenerateSoundex(inputLowercase);
        string resultUppercase = Soundex.GenerateSoundex(inputUppercase);

        // Assert
        Assert.Equal(resultLowercase, resultUppercase);
        Assert.Equal("A000", resultLowercase);
    }
   [Theory]
    [InlineData('B', '1')]
    [InlineData('F', '1')]
    [InlineData('P', '1')]
    [InlineData('V', '1')]
    [InlineData('C', '2')]
    [InlineData('G', '2')]
    [InlineData('J', '2')]
    [InlineData('K', '2')]
    [InlineData('Q', '2')]
    [InlineData('S', '2')]
    [InlineData('X', '2')]
    [InlineData('Z', '2')]
    [InlineData('D', '3')]
    [InlineData('T', '3')]
    [InlineData('L', '4')]
    [InlineData('M', '5')]
    [InlineData('N', '5')]
    [InlineData('R', '6')]
    [InlineData('A', '0')]
    [InlineData('E', '0')]
    [InlineData('I', '0')]
    [InlineData('O', '0')]
    [InlineData('U', '0')]
    [InlineData('H', '0')]
    [InlineData('W', '0')]
    [InlineData('Y', '0')]
    [InlineData(' ', '0')] // Testing a space character
    [InlineData('1', '0')] // Testing a digit
    public void GetSoundexCode_ValidAndInvalidCharacters_ReturnsExpectedCode(char input, char expected)
    {
        // Act
        char result = Soundex.GetSoundexCode(input);

        // Assert
        Assert.Equal(expected, result);
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
