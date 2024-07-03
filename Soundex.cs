using System;
using System.Text;

public class Soundex
{
    public static string GenerateSoundex(string name)
{
    if(string.IsNullOrEmpty(name))
    {
        return string.Empty;
    }
    StringBuilder soundex = InitializeTheSoundex(name);
    char prevCode = GetSoundexCode(name[0]);

    AppendingSoundexCharacters(name, soundex, prevCode);
    GenerateSoundex(soundex);
    return soundex.ToString();
}

private StringBuilder InitializeTheSoundex(string name)
{
    StringBuilder soundex = new StringBuilder();
    soundex.Append(char.ToUpper(name[0]));
    return soundex;
}

private void AppendingSoundexCharacters(string name, StringBuilder soundex, char prevCode)
{
    for (int i = 1; i < name.Length && soundex.Length < 4; i++)
    {
        Characters(name[i], soundex, prevCode);
    }
}
private void Characters(char character, StringBuilder soundex, char prevCode)
{
    char code = GetSoundexCode(character);
    if (AppendCode(code, prevCode))
    {
        soundex.Append(code);
        prevCode = code;
    }
}
private bool AppendCode(char code, char prevCode) => code != 0 && code != prevCode;
    
private void GenerateSoundex(StringBuilder soundex)
{
    while (soundex.Length < 4)
    {
        soundex.Append(0);
    }
}

    private char GetSoundexCode(char character)
    {
        character = char.ToUpper(character);
        return character switch
        {
            'B' or 'F' or 'P' or 'V' => '1',
            'C' or 'G' or 'J' or 'K' or 'Q' or 'S' or 'X' or 'Z' => '2',
            'D' or 'T' => '3',
            'L' => '4',
            'M' or 'N' => '5',
            'R' => '6',
            _ => 0
        };
    }
}
