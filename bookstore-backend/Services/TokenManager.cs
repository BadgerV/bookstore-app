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

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Securely retrieve the signing key (not shown here)
            var signingKey = GetSigninKey();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                // Consider a reasonable clock skew tolerance (e.g., TimeSpan.FromMinutes(5))
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            // Validate the token and return the claims principal
            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }


    }
}
