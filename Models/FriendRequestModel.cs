using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class FriendRequestModel
	{
		[Key]
		public int Id { get; set; }
		public string SenderEmail { get; set; }

		public string TargetEmail { get; set; }

	}
}
