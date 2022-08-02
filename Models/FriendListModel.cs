using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class FriendListModel
	{
		[Key]
		public int Id { get; set; }
		public string UserEmail { get; set; }

		public IdentityUser Friend { get; set; }

	}
}
