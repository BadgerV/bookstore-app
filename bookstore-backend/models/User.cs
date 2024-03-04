using System.ComponentModel.DataAnnotations;



namespace bookstore_backend.models
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage ="Username is required")]
        [MaxLength(25, ErrorMessage = "Username cannot be longer than 25 characters")]
        [MinLength(3, ErrorMessage = "Username too short")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } // Hashed password for security

        [Required(ErrorMessage = "Please provide a first name")]
        [MaxLength(25, ErrorMessage = "First name cannot be more than 25 characters")]
        [MinLength(3, ErrorMessage = "First name cannot be shorter then 3 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(25, ErrorMessage = "Last name cannot be more than 25 characters")]
        [MinLength(3, ErrorMessage = "Last name cannot be less than 3 characters")]
        public string LastName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }  // Optional separate billing address
        public bool IsAdmin { get; set; } // Flag to indicate admin 

        public bool IsAuthor { get; set; } // Flag to indicate author 

        // Additional optional properties based on requirements:
        public string? PhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; } // Birthday (consider privacy implications)
        public List<Order>? Orders { get; set; } // Navigation property for user's orders
        public List<Review>? Reviews { get; set; } // Navigation property for user's reviews

        public User()
        {
            IsAdmin = false;
        }
    }
}
