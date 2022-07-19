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
		public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<Shoppinglist>().ToTable("Shoppinglists");
			modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItems");

			modelBuilder.Entity<Shoppinglist>()
						.Property(f => f.Id)
						.ValueGeneratedOnAdd();
			modelBuilder.Entity<ShoppingListItem>()
					.Property(f => f.Id)
					.ValueGeneratedOnAdd();

		}
	}
}
