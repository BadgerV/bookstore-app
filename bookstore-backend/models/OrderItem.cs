namespace bookstore_backend.models
{
    public class OrderItem
    {
        public int Id { get; set; } // Primary key for the order item
        public int OrderId { get; set; } // Foreign key referencing Order
        public Order? Order { get; set; } // Navigation property for order
        public int BookId { get; set; } // Foreign key referencing Book (assuming a Book model exists)
        public Book? Book { get; set; } // Navigation property for book
        public int Quantity { get; set; } // Number of units of this book in the order
        public decimal Price { get; set; } // Price per unit of the book at the time of the order
    }

}
