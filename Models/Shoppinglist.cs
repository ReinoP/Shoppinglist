using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class Shoppinglist
	{
		[Key]
		public int Id { get; set; }
		public string UserID { get; set; }

		public string ListID { get; set; }
		public string ListName { get; set; }

	}
}
