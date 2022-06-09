using Microsoft.EntityFrameworkCore;
using ShoppinglistApp.Models;

namespace ShoppinglistApp.Data
{
  public class ShoppingListContext : DbContext
  {
    public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Shoppinglist> Shoppinglists { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().ToTable("User");
      modelBuilder.Entity<Shoppinglist>().ToTable("Shoppinglist");
      modelBuilder.Entity<Shoppinglist>()
                .Property(b => b.ListID)
                .ValueGeneratedOnAdd();
    }
  }
}
