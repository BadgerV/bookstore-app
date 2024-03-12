using bookstore_backend.models;

namespace bookstore_backend.DTOs
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public byte[]? image { get; set; }
        public List<int>? CategoriesId { get; set; } // Navigation property for category
        public decimal Price { get; set; }
    }

    public class CreateBookDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte[]? image { get; set; }
        public List<int>? CategoriesId { get; set; } // Navigation property for category
        public decimal Price { get; set; }
    }
}

//just a frivolous comment
//anither frivolous comment sha
