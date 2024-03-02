namespace bookstore_backend.models
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; } // Optional category description
        public List<Book>? Books { get; set; } // Navigation property for books
    }

}
