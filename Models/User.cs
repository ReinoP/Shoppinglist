using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public DateTime RegisterDate { get; set; }

		public ICollection<Shoppinglist> Shoppinglists { get; set; }
	}
}
