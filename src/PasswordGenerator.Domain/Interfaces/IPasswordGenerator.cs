using PasswordGenerator.Domain.Entities;

namespace PasswordGenerator.Domain.Interfaces;

public interface IPasswordGenerator
{
    string Generate(PasswordOptions options);
}