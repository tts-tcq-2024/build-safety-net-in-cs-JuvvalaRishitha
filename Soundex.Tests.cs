using Xunit;
using System;
using Moq;

public class SoundexTests
{
    private readonly Mock<ISoundexService> _mockSoundexService;
    private readonly Soundex _soundex;

    public SoundexTests()
    {
        _mockSoundexService = new Mock<ISoundexService>();
        _soundex = new Soundex(_mockSoundexService.Object);
    }

    [Theory]
    [InlineData("Robert", 'R', "R163")]
    [InlineData("Rupert", 'R', "R163")]
    [InlineData("Rubin", 'R', "R150")]
    [InlineData("Ashcraft", 'A', "A261")]
    [InlineData("Ashcroft", 'A', "A261")]
    [InlineData("Tymczak", 'T', "T522")]
    [InlineData("Pfister", 'P', "P236")]
    [InlineData("", ' ', "")]
    [InlineData(null, ' ', "")]
    public void GenerateSoundex_ValidAndInvalidInputs_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

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
    [InlineData("B", 'B', "B000")]
    [InlineData("Bo", 'B', "B000")]
    [InlineData("Bob", 'B', "B100")]
    public void GenerateSoundex_ShortNames_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("A", 'A', "A000")]
    [InlineData("E", 'E', "E000")]
    [InlineData("I", 'I', "I000")]
    [InlineData("O", 'O', "O000")]
    [InlineData("U", 'U', "U000")]
    [InlineData("H", 'H', "H000")]
    [InlineData("W", 'W', "W000")]
    [InlineData("Y", 'Y', "Y000")]
    public void GenerateSoundex_SingleVowel_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("BFPV", 'B', "B110")]
    [InlineData("CGJKQSXZ", 'C', "C220")]
    [InlineData("DT", 'D', "D300")]
    [InlineData("L", 'L', "L400")]
    [InlineData("MN", 'M', "M500")]
    [InlineData("R", 'R', "R600")]
    public void GenerateSoundex_SameSoundexGroupLetters_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("BCDL", 'B', "B234")]
    [InlineData("BFGHJKL", 'B', "B245")]
    public void GenerateSoundex_MixedLetters_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Bobbbyyyyyy", 'B', "B110")]
    [InlineData("Ashcrofttttttt", 'A', "A261")]
    public void GenerateSoundex_LongNames_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("O'Malley", 'O', "O540")]
    [InlineData("D'Angelo", 'D', "D524")]
    [InlineData("McDonald", 'M', "M235")]
    public void GenerateSoundex_NamesWithApostrophes_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Washington", 'W', "W252")]
    [InlineData("Lee", 'L', "L000")]
    [InlineData("Gutierrez", 'G', "G362")]
    [InlineData("Pfister", 'P', "P236")]
    [InlineData("Jackson", 'J', "J250")]
    [InlineData("Tymczak", 'T', "T522")]
    [InlineData("Van", 'V', "V500")]
    public void GenerateSoundex_VariousNames_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Robert ", 'R', "R163")]
    [InlineData("  Rupert", 'R', "R163")]
    [InlineData("  Rubin  ", 'R', "R150")]
    public void GenerateSoundex_NamesWithLeadingTrailingSpaces_ReturnsExpectedResults(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input.Trim());

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Smith", 'S', "S530")]
    [InlineData("Smythe", 'S', "S530")]
    public void GenerateSoundex_EqualSoundexDifferentNames_ReturnsSameSoundexCode(string input, char firstCharCode, string expected)
    {
        // Arrange
        _mockSoundexService.Setup(s => s.GetSoundexCode(It.IsAny<char>())).Returns((char c) =>
        {
            return c switch
            {
                'B' or 'F' or 'P' or 'V' => '1',
                'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
                'D' or 'T' => '3',
                'L' => '4',
                'M' or 'N' => '5',
                'R' => '6',
                _ => '0'
            };
        });

        if (!string.IsNullOrEmpty(input))
        {
            _mockSoundexService.Setup(s => s.GetSoundexCode(input[0])).Returns(_mockSoundexService.Object.GetSoundexCode(input[0]));
        }

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal(expected, result);
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
        string input = "Tymczak";

        // Act
        string result = _soundex.GenerateSoundex(input);

        // Assert
        Assert.Equal("T522", result);
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
