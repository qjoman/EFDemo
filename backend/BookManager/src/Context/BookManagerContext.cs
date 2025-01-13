using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class BookManagerContext : DbContext
{

    // System entities
    public DbSet<User> User { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    // Book related entities
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookPrice> BookPrices { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookCategory> BookCategories {get;set; }

    // Order related entities
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public BookManagerContext(DbContextOptions<BookManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Book)
            .WithMany()
            .HasForeignKey(oi => oi.BookId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        Seed.Populate(modelBuilder);
    }
    
    public override int SaveChanges()
    {
        UpdateTimestamps();
        TrackChanges();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        TrackChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            if (entry.State == EntityState.Added && entry.Entity.CreatedAt == default)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }

    private void TrackChanges()
    {
        var auditEntries = new List<AuditLog>();
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                string action = entry.State.ToString();
                if (entry.State == EntityState.Modified && !entry.Entity.IsActive)
                {
                    action = "Disabled";
                }
                var auditLog = new AuditLog
                {
                    EntityId = entry.Entity.Id,
                    EntityName = entry.Entity.GetType().Name,
                    Action = action,
                    Timestamp = DateTime.UtcNow,
                    UserId = null
                };
                auditEntries.Add(auditLog);
            }
        }

        if (auditEntries.Any())
        {
            AuditLogs.AddRange(auditEntries);
        }
    }

}