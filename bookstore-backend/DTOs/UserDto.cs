namespace bookstore_backend.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShippingAddress { get; set; }
        public bool? IsAuthor { get; set; }
    }
}
