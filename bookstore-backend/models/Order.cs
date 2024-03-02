namespace bookstore_backend.models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; } // Foreign key referencing User
        public User? User { get; set; } // Navigation property for user
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ShippingAddress { get; set; } // Can be different from user's address
        public string? Status { get; set; } // Order status (e.g., "Placed", "Shipped", "Delivered", "Cancelled")
        public List<OrderItem> OrderItems { get; set; } // Navigation property for order items

        public Order()
        {
            OrderItems = new List<OrderItem>(); // Initialize empty list for order items
        }
    }

}


