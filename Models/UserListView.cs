using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class UserListView
	{
		public List<IdentityUser> userList{ get; set; }

	}
}
