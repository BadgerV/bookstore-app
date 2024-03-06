namespace bookstore_backend.DTOs
{
    public class ReviewDto
    {
        public string? UserUsername { get; set; } // Foreign key referencing User
        public string? BookName { get; set; } // Foreign key referencing Book (assuming a Book model exists)
        public int Rating { get; set; } // Rating out of 5 (or adapt based on your rating system)
        public string? Comment { get; set; } // Optional review text

        public ReviewDto()
        {
            Rating = 0; // Initialize rating to 0 by default
        }
    }

    public class PostReviewDto
    {
        public int Rating { get; set; } // Rating out of 5 (or adapt based on your rating system)
        public string? Comment { get; set; } // Optional review text

        public PostReviewDto()
        {
            Rating = 0; // Initialize rating to 0 by default
        }
    }
}
