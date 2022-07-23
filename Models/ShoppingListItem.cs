using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class ShoppingListItem
	{
		[Key]
		public int Id { get; set; }
		public string ListId { get; set; }

		public string ItemName { get; set; }

		public List<ShoppingListItem> items { get; set; }

	}
}
