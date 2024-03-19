using bookstore_backend.models;
using System.Runtime.CompilerServices;

namespace bookstore_backend.DTOs
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public List<int>? CategoriesId { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateBookDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<int>? CategoriesId { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateBookResponseDto
    {
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public List<string>? Categories { get; set; }
        public decimal Price { get; set; }
    }
}

//anither frivolous comment abeg