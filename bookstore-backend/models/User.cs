using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace bookstore_backend.models
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? PasswordHash { get; set; } // Hashed password for security
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }  // Optional separate billing address
        public bool IsAdmin { get; set; } // Flag to indicate admin privileges

        // Additional optional properties based on requirements:
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; } // Birthday (consider privacy implications)
        public List<Order>? Orders { get; set; } // Navigation property for user's orders
        public List<Review>? Reviews { get; set; } // Navigation property for user's reviews

        public User()
        {
            IsAdmin = false;
        }
    }
}
