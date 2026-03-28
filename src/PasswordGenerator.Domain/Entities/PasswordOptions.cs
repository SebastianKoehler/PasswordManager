namespace PasswordGenerator.Domain.Entities;

public class PasswordOptions
{
    public int Length { get; set; } = 12;
    public bool UseUppercase { get; set; } = true;
    public bool UseLowercase { get; set; } = true;
    public bool UseDigits { get; set; } = true;
    public bool UseSpecialCharacters { get; set; } = true;
}