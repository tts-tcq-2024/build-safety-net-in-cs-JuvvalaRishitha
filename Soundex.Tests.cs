using Xunit;
using System;


public class SoundexTests
{
    private readonly Soundex _soundex;

    public SoundexTests()
    {
        _soundex = new Soundex();
    }

    [Theory]
    [InlineData("Robert", "R163")]
    [InlineData("Rupert", "R163")]
    [InlineData("Rubin", "R150")]
    [InlineData("Ashcraft", "A261")]
    [InlineData("Ashcroft", "A261")]
    [InlineData("Tymczak", "T522")]
    [InlineData("Pfister", "P236")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void GenerateSoundex_ValidAndInvalidInputs_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GenerateSoundex_EmptyInput_ReturnsEmptyString()
    {
        // Arrange
        string input = "";

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GenerateSoundex_NullInput_ReturnsEmptyString()
    {
        // Arrange
        string input = null;

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("B", "B000")]
    [InlineData("Bo", "B000")]
    [InlineData("Bob", "B100")]
    public void GenerateSoundex_ShortNames_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("A", "A000")]
    [InlineData("E", "E000")]
    [InlineData("I", "I000")]
    [InlineData("O", "O000")]
    [InlineData("U", "U000")]
    [InlineData("H", "H000")]
    [InlineData("W", "W000")]
    [InlineData("Y", "Y000")]
    public void GenerateSoundex_SingleVowel_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("BFPV", "B110")]
    [InlineData("CGJKQSXZ", "C220")]
    [InlineData("DT", "D300")]
    [InlineData("L", "L400")]
    [InlineData("MN", "M500")]
    [InlineData("R", "R600")]
    public void GenerateSoundex_SameSoundexGroupLetters_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("BCDL", "B234")]
    [InlineData("BFGHJKL", "B245")]
    public void GenerateSoundex_MixedLetters_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Bobbbyyyyyy", "B110")]
    [InlineData("Ashcrofttttttt", "A261")]
    public void GenerateSoundex_LongNames_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("O'Malley", "O540")]
    [InlineData("D'Angelo", "D524")]
    [InlineData("McDonald", "M235")]
    public void GenerateSoundex_NamesWithApostrophes_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Washington", "W252")]
    [InlineData("Lee", "L000")]
    [InlineData("Gutierrez", "G362")]
    [InlineData("Pfister", "P236")]
    [InlineData("Jackson", "J250")]
    [InlineData("Tymczak", "T522")]
    [InlineData("Van", "V500")]
    public void GenerateSoundex_VariousNames_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Robert ", "R163")]
    [InlineData("  Rupert", "R163")]
    [InlineData("  Rubin  ", "R150")]
    public void GenerateSoundex_NamesWithLeadingTrailingSpaces_ReturnsExpectedResults(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input.Trim());

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Smith", "S530")]
    [InlineData("Smythe", "S530")]
    public void GenerateSoundex_EqualSoundexDifferentNames_ReturnsSameSoundexCode(string input, string expected)
    {
        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Robert", "R163", "Rupert", "R163")]
    [InlineData("Rubin", "R150", "Ruben", "R150")]
    public void GenerateSoundex_CompareNamesWithSameSoundex(string name1, string expected1, string name2, string expected2)
    {
        // Act
        string result1 = _soundex.GenerateSoundex(name1);
        string result2 = _soundex.GenerateSoundex(name2);

        // Assert
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void GenerateSoundex_HandlesMixedCaseNames()
    {
        // Arrange
        string input = "rObErT";

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("R163", result);
    }

    [Fact]
    public void GenerateSoundex_HandlesRepeatingCharacters()
    {
        // Arrange
        string input = "Bbbbbb";

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("B000", result);
    }

    [Fact]
    public void GenerateSoundex_NamesWithNonAlphabetCharacters_ReturnsExpectedResults()
    {
        // Arrange
        string input = "R0b3rt!";

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("R163", result);
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
