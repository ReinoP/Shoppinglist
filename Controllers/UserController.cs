﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppinglistApp.Data;
using ShoppinglistApp.Models;

namespace ShoppinglistApp.Controllers
{
	public class UserController : Controller
	{
		private readonly ShoppingListContext _context;
		private readonly UserDbContext _userContext;


		public UserController(ShoppingListContext context, UserDbContext ucontext)
		{	
			_context = context;
			_userContext = ucontext;
		}

		// GET: Users
		[Authorize]
		public async Task<IActionResult> Index()
		{
			//ManageFriendsView list = new ManageFriendsView();
			//list.UserList = await _userContext.Users.ToListAsync();
			//list.FriendRequests = await _userContext.FriendRequests.ToListAsync();

			//return View(list);
			return View();
		}


		// GET: All users
		[Authorize]
		public async Task<IActionResult> ManageFriends()
		{
			ManageFriendsView list = new ManageFriendsView();
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);

			//var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			try
			{
				list.FriendRequests = await _userContext.FriendRequests.ToListAsync();
				list.UserList = await _userContext.Users.ToListAsync();
				list.MyFriendsList = await _userContext.FriendList.Where(f => f.FriendEmail == curUser.Email).ToListAsync();
			}
			catch(Exception e)
			{
				Debug.WriteLine(e);
			}
			return View(list);
		}
		[Authorize]
		// GET: Users/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userContext.Users.FirstOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}
		public IActionResult AddFriends()
		{
			return View();
		}

		// POST: Users/AddFriends
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddFriends(string friendlist)
		{
			var nameArr = JsonConvert.DeserializeObject<string[]>(friendlist);
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			try
			{
				foreach (string s in nameArr)
				{
					var friendRequest = new FriendRequestModel();
					friendRequest.SenderEmail = curUser.Email;
					friendRequest.TargetEmail = s;
					_userContext.FriendRequests.Add(friendRequest);
				}
				await _userContext.SaveChangesAsync();
			}
			//TODO maybe let user know why it fails?
			catch(Exception e)
			{
				return Json(false);
			}
			return Json(true); 
		}

		//public IActionResult AcceptRequest()
		//{
		//	return View();
		//}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AcceptRequest(string friendEmail)
		{
		
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name) ;
			//var newFriend = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == friendEmail);
			Debug.WriteLine("acceptrequst " + friendEmail);
			var newFriend = new FriendModel();
			newFriend.UserEmail = curUser.Email;
			newFriend.FriendEmail = friendEmail;

			_userContext.FriendList.Add(newFriend);
		
			await _userContext.SaveChangesAsync();

			return Redirect("~/User/ManageFriends");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> DeclineRequest(string friendEmail)
		{

			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			//var newFriend = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == friendEmail);
			Debug.WriteLine("decline " + friendEmail);

			var req = await _userContext.FriendRequests.FirstOrDefaultAsync(m => m.TargetEmail == User.Identity.Name);
			_userContext.FriendRequests.Remove(req);

			await _userContext.SaveChangesAsync();

			return Redirect("~/User/ManageFriends");
		}

		// GET: Users/Create
		[Authorize]
		public IActionResult Create()
		{
			return View();
		}

		// POST: Users/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(IdentityUser user)
		{
			if (ModelState.IsValid)
			{
				_userContext.Users.Add(user);
				await _userContext.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(user);
		}

		// GET: Users/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userContext.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(string id, IdentityUser user)
		{
			if (id != user.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_userContext.Users.Update(user);
					await _userContext.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserExists(user.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(user);
		}

		// GET: Users/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userContext.Users.FirstOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var user = await _userContext.Users.FindAsync(id);
			_userContext.Users.Remove(user);
			await _userContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserExists(string id)
		{
			return _userContext.Users.Any(e => e.Id == id);
		}
	}
}
