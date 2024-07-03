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
    Soundex(soundex);
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
        char code = GetSoundexCode(name[i]);
        if (code != '0' && code != prevCode)
        {
            soundex.Append(code);
            prevCode = code;
        }
    }
}

private void Soundex(StringBuilder soundex)
{
    while (soundex.Length < 4)
    {
        soundex.Append(0);
    }
}

    private static char GetSoundexCode(char c)
    {
        c = char.ToUpper(c);
        switch (c)
        {
            case 'B':
            case 'F':
            case 'P':
            case 'V':
                return '1';
            case 'C':
            case 'G':
            case 'J':
            case 'K':
            case 'Q':
            case 'S':
            case 'X':
            case 'Z':
                return '2';
            case 'D':
            case 'T':
                return '3';
            case 'L':
                return '4';
            case 'M':
            case 'N':
                return '5';
            case 'R':
                return '6';
            default:
                return '0'; // For A, E, I, O, U, H, W, Y
        }
    }
}
