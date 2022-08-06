using Microsoft.AspNetCore.Identity;
using ShoppinglistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Data
{
	public static class DbInitializer
	{
		public static void Initialize(UserDbContext context)
		{
			context.Database.EnsureCreated();

			if (context.Users.Any())
			{
				return;   // seeds have been done
			}

			var users = new IdentityUser[]
			{
				new IdentityUser{UserName="Matti",Email="matti.laukaala@example.com"},
				new IdentityUser{UserName="Pekka",Email="pekka.parttinen@example.com"},
				new IdentityUser{UserName="Hanna",Email="hanna.pollikka@example.com"},
				new IdentityUser{UserName="Laura",Email="laura.bosch@example.com"},
				};

			foreach (IdentityUser u in users)
			{
				

				context.Users.Add(u);
			}
			context.SaveChanges();

		}
	}
}
