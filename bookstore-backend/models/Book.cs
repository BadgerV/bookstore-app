namespace bookstore_backend.models;

public class Book : BaseEntity
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public byte[]? Image { get; set; }
    public string? Description { get; set; }
    public List<string> Categories { get; set; } = [];
    public decimal Price { get; set; }
    public decimal? AverageRating { get; set; }
}