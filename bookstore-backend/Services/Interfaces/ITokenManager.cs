using bookstore_backend.models;
using Microsoft.IdentityModel.Tokens;

namespace bookstore_backend.Services.Interfaces
{
    public interface ITokenManager
    {
        SymmetricSecurityKey GetSigninKey();
        string GenerateJWTToken(User user);
    }
}
