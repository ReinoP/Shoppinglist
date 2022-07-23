using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class ShoppingListViewModel
	{
		public string UserID { get; set; }
		public string ListID { get; set; }
		public string ListName { get; set; }
		public List<ShoppingListItem> Items { get; set; }
	}
}
