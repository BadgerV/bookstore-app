using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bookstore_backend.models
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; } // Foreign key referencing User

        [ForeignKey("UserId")]
        public User? User { get; set; } // Navigation property for user
        public int BookId { get; set; } // Foreign key referencing Book (assuming a Book model exists)
        [ForeignKey("BookId")]
        public Book? Book { get; set; } 
        public int Rating { get; set; } // Rating out of 5 (or adapt based on your rating system)
        public string? Comment { get; set; } // Optional review text

        public Review()
        {
            Rating = 0; // Initialize rating to 0 by default
        }
    }

}
