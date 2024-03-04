using bookstore_backend.DTOs;
using bookstore_backend.models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace bookstore_backend.Utilities
{
    public static class UtilityClasses
    {
        public class LoginRequest
        {
            public string? Email { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        public static bool ValidatePassword(string plainPassword, string hashedPassword)
        {
            return BCryptNet.Verify(plainPassword, hashedPassword);
        }

        public static string HashPassword(string password)
        {
            return BCryptNet.HashPassword(password);
        }

        public static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ShippingAddress = user.ShippingAddress,
            };
        }
    }
}
