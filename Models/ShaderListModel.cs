using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class SharedListModel
	{
		[Key]
		public int Id { get; set; }
		public string UserEmail { get; set; }

		public string FriendEmail { get; set; }
		public string ListName { get; set; }

		public string ListID { get; set; }

	}
}
