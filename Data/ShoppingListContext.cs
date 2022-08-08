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

		public DbSet<Shoppinglist> Shoppinglists { get; set; }
		public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
		public DbSet<SharedListModel> SharedLists { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Shoppinglist>().ToTable("Shoppinglists");
			modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItems");
			modelBuilder.Entity<SharedListModel>().ToTable("SharedLists");


			modelBuilder.Entity<Shoppinglist>()
						.Property(f => f.Id)
						.ValueGeneratedOnAdd();
			modelBuilder.Entity<ShoppingListItem>()
						.Property(f => f.Id)
						.ValueGeneratedOnAdd();
			modelBuilder.Entity<SharedListModel>()
						.Property(f => f.Id)
						.ValueGeneratedOnAdd();

		}
	}
}
