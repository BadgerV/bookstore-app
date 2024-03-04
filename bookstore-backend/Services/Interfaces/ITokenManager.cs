using bookstore_backend.models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace bookstore_backend.Services.Interfaces
{
    public interface ITokenManager
    {
        SymmetricSecurityKey GetSigninKey();
        string GenerateJWTToken(User user);

        ClaimsPrincipal ValidateToken(string token);
    }
}
