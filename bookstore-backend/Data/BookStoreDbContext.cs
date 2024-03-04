using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using bookstore_backend.models;

namespace bookstore_backend.Data
{
    public class BookStoreDbContext : DbContext
    {
        // Constructor to inject configuration options
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        //Defining DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }


        // Override OnConfiguring to configure the connection string
        //just a frivolous comment
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=127.0.0.1 port=5432 dbname=postgres user=postgres password=xxxxxxx sslmode=prefer connect_timeout=10"); // Specify your PostgreSQL connection string
        }

    }
}
