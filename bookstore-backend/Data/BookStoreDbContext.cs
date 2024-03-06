using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using bookstore_backend.models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace bookstore_backend.Data
{

    public class BookStoreDbContext : DbContext
    {
        // Constructor to inject configuration options
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        //Defining DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>().
                HasOne<User>(r => r.User).WithMany(x => x.Reviews).HasForeignKey(r => r.UserId).IsRequired();

            modelBuilder.Entity<Review>().
                HasOne<Book>(r => r.Book).WithMany().HasForeignKey(r => r.BookId).IsRequired();
        }
    }
}
