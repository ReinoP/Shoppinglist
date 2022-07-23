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
				new IdentityUser{UserName="Matti",Email="matti@joku.com"},
				new IdentityUser{UserName="Pekka",Email="Parttinen"},
				new IdentityUser{UserName="Hanna",Email="Kollikka"},
				new IdentityUser{UserName="Laura",Email="Bosch"},
				};

			foreach (IdentityUser u in users)
			{
				

				context.Users.Add(u);
			}
			context.SaveChanges();

		}
	}
}
