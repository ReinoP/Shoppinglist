using ShoppinglistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Data
{
	public static class DbInitializer
	{
		public static void Initialize(ShoppingListContext context)
		{
			context.Database.EnsureCreated();

			if (context.Users.Any())
			{
				return;   // seeds have been done
			}

			var users = new User[]
			{
				new User{FirstName="Matti",LastName="Meikäläinen",RegisterDate=DateTime.Parse("2004-09-14")},
				new User{FirstName="Pekka",LastName="Parttinen",RegisterDate=DateTime.Parse("2012-12-07")},
				new User{FirstName="Hanna",LastName="Kollikka",RegisterDate=DateTime.Parse("2010-02-05")},
				new User{FirstName="Laura",LastName="Bosch",RegisterDate=DateTime.Parse("2001-04-06")},
				};

			foreach (User u in users)
			{
				var sl = new Shoppinglist();
				sl.ListName = u.FirstName + u.LastName + "List";
				//var listItems = new List<ShoppingListItem>();
				//for (int i = 0; i < 5; i++)
				//{
				//  var listItem = new ShoppingListItem("item " + i);
				//  listItems.Add(listItem);
				//}
				//sl.Items = listItems;
				//sl.UserID = u.ID;
				//sl.ListID = u.ID;

				context.Users.Add(u);
			}
			context.SaveChanges();

		}
	}
}
