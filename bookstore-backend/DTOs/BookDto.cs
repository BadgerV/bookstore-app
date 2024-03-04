namespace bookstore_backend.DTOs
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public byte[]? image { get; set; }

        public int CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
