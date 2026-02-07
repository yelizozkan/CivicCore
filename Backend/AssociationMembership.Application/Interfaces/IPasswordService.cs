using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Common.Results;

namespace AssociationMembership.Application.Services
{
    public interface IPasswordService
    {
        Result<string> HashPassword(string password);
        Result<bool> VerifyPassword(string password, string hashedPassword);
        Result<string> GenerateRandomPassword(int length = 12);
        Result<bool> ValidatePasswordStrength(string password);
    }
} 
