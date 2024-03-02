﻿namespace bookstore_backend.models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt {get; set;}

        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
