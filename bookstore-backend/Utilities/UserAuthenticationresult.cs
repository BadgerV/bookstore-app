using bookstore_backend.DTOs;
using bookstore_backend.models;

namespace bookstore_backend.Utilities
{
    public class UserAuthenticationresult
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; } = default!;
        public bool Success { get; set; } = default!;
        public string? Message { get; set; } = default!;

    }
}
