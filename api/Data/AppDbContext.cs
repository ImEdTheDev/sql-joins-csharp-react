using Microsoft.EntityFrameworkCore;
using PurchasesApi.Models;

namespace PurchasesApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Purchase> Purchases => Set<Purchase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.Property(u => u.Id).ValueGeneratedNever();
            user.Property(u => u.Name).HasMaxLength(50);
            user.HasMany(u => u.Purchases)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        });

        modelBuilder.Entity<Purchase>(purchase =>
        {
            purchase.Property(p => p.Id).ValueGeneratedNever();
            purchase.Property(p => p.Product).HasMaxLength(50);
            purchase.Property(p => p.Amount).HasPrecision(10, 2);
        });

        // Same data as the video: Sophia (3) and Olivia (5) have no purchases.
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Emma" },
            new User { Id = 2, Name = "James" },
            new User { Id = 3, Name = "Sophia" },
            new User { Id = 4, Name = "Liam" },
            new User { Id = 5, Name = "Olivia" });

        modelBuilder.Entity<Purchase>().HasData(
            new Purchase { Id = 101, UserId = 2, Product = "Keyboard", Amount = 79.99m },
            new Purchase { Id = 102, UserId = 1, Product = "Mouse", Amount = 24.99m },
            new Purchase { Id = 103, UserId = 2, Product = "Monitor", Amount = 249.99m },
            new Purchase { Id = 104, UserId = 4, Product = "Headphones", Amount = 59.99m });
    }
}
