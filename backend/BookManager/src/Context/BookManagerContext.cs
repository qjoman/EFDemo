using Microsoft.EntityFrameworkCore;

public class BookManagerContext : DbContext
{

    // System entities
    public DbSet<User> User {get; set;}
    public DbSet<AuditLog> AuditLogs {get;set;}

    // Book related entities
    public DbSet<Author> Authors {get;set;}
    public DbSet<Book> Books {get;set;}
    public DbSet<BookPrice> BookPrices {get;set;}
    public DbSet<Category> Categories {get;set;}
    
    // Order related entities
    public DbSet<Order> Orders {get;set;}
    public DbSet<OrderItem> OrderItems {get;set;}

    public BookManagerContext(DbContextOptions<BookManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API is not required on this project
    }
}