using Microsoft.EntityFrameworkCore;
using BookstoreAPI.Models;

namespace BookstoreAPI;

public class BookstoreContext : DbContext
{
    public BookstoreContext(DbContextOptions<BookstoreContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(b => b.BookID);
        });
    }
}
