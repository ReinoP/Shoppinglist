using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppinglistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Data
{
	public class UserDbContext : IdentityDbContext<IdentityUser>
	{
		public UserDbContext(DbContextOptions<UserDbContext> options)
				: base(options)
		{
		}

		public DbSet<FriendRequestModel> FriendRequests { get; set; }
		public DbSet<FriendModel> FriendList { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<FriendRequestModel>().ToTable("FriendRequests");
			modelBuilder.Entity<FriendRequestModel>()
				.Property(f => f.Id)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<FriendRequestModel>().HasIndex(e => e.TargetEmail).IsUnique();

			modelBuilder.Entity<FriendModel>().ToTable("FriendList");
			modelBuilder.Entity<FriendModel>()
				.Property(f => f.Id)
				.ValueGeneratedOnAdd();

		}
	}
}
