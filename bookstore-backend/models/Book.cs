namespace bookstore_backend.models
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public byte[]? image { get; set; }
        public string? Description { get; set; } // Optional book description
        public List<string> Categories { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public decimal? AverageRating { get; set; } // Average rating from all reviews
    }
}