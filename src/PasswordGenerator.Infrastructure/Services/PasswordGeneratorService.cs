using System.Security.Cryptography;
using System.Text;
using PasswordGenerator.Domain.Entities;
using PasswordGenerator.Domain.Interfaces;

namespace PasswordGenerator.Infrastructure.Services;

public class PasswordGeneratorService : IPasswordGenerator
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Special = "!@#$%^&*()-_=+[]{}|;:,.<>?";

    public string Generate(PasswordOptions options)
    {
        ValidateOptions(options);
        
        var characterPools = BuildCharacterPools(options);
        var allCharacters = string.Concat(characterPools);
        
        var passwordChars = new List<char>();

        foreach (var pool in characterPools)
        {
            passwordChars.Add(GetRandomChar(allCharacters));
        }

        while (passwordChars.Count < options.Length)
        {
            passwordChars.Add(GetRandomChar(allCharacters));
        }

        Shuffle(passwordChars);

        return new string(passwordChars.ToArray());
    }

    private void ValidateOptions(PasswordOptions options)
    {
        if (options.Length < 4)
            throw new ArgumentException("Password length must be at least 4");

        if (!options.UseLowercase &&
            !options.UseUppercase &&
            !options.UseDigits &&
            !options.UseSpecialCharacters)
        {
            throw new ArgumentException("At least one character set must be selected");
        }
    }

    private static List<string> BuildCharacterPools(PasswordOptions options)
    {
        var pools = new List<string>();
        
        if (options.UseLowercase)
            pools.Add(Lowercase);
        
        if (options.UseUppercase)
            pools.Add(Uppercase);
        
        if (options.UseDigits)
            pools.Add(Digits);
        
        if (options.UseSpecialCharacters)
            pools.Add(Special);

        return pools;
    }

    private static char GetRandomChar(string source)
    {
        var index = RandomNumberGenerator.GetInt32(source.Length);
        return source[index];
    }

    private static void Shuffle(List<char> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j =  RandomNumberGenerator.GetInt32(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}

