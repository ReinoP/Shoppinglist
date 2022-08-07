using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
	public class ManageFriendsView
	{
		public List<IdentityUser> UserList{ get; set; }
		public List<FriendRequestModel> FriendRequests { get; set; }
		public List<FriendModel> MyFriendsList { get; set; }

	}
}
