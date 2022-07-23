using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppinglistApp.Models;

namespace ShoppinglistApp.Data
{
	public class ShoppingListContext : DbContext
	{
		public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options)
		{
		}

		public DbSet<IdentityUser> Users { get; set; }
		public DbSet<Shoppinglist> Shoppinglists { get; set; }
		public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");
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
