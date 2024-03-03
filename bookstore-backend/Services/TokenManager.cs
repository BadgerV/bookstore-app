using bookstore_backend.models;
using bookstore_backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bookstore_backend.Services
{
    public class TokenManager : ITokenManager
    {
        public SymmetricSecurityKey GetSigninKey()
        {
            var secret = Encoding.ASCII.GetBytes("this_is_a_very_strong_key_sovidinjso;diifh");
            return new SymmetricSecurityKey(secret);
        }

        public string GenerateJWTToken (User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(1000),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(GetSigninKey(), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
