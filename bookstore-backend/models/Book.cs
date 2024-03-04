namespace bookstore_backend.models
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public byte[]? image { get; set; }
        public string? Description { get; set; } // Optional book description
        public int CategoryId { get; set; } // Foreign key referencing Category (assuming a Category model exists)
        public Category? Category { get; set; } // Navigation property for category
        public decimal Price { get; set; }
        public decimal? AverageRating { get; set; } // Average rating from all reviews
        public List<Review>? Reviews { get; set; } // Navigation property for reviews
    }

}
