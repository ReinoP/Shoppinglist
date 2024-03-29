﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
		public IActionResult Index()
		{
			return View();
		}


		// GET: All users
		[Authorize]
		public async Task<IActionResult> ManageFriends()
		{
			ManageFriendsView list = new ManageFriendsView();
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);

			try
			{
				list.FriendRequests = await _userContext.FriendRequests.Where(f => f.TargetEmail == curUser.Email).ToListAsync();
				list.UserList = await _userContext.Users.ToListAsync();
				list.MyFriendsList =  await _userContext.FriendList.Where(f => f.FriendEmail == curUser.Email).ToListAsync();
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

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> RemoveFriend(string friendEmail)
		{
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			try
			{
				var friend = await _userContext.FriendList.FirstOrDefaultAsync(f => f.UserEmail == curUser.Email && f.FriendEmail == friendEmail);
				_userContext.FriendList.Remove(friend);
				await _userContext.SaveChangesAsync();
				//we remove both friends, as when one is created it creates 2 friendModels, friend1 -> friend2, and friend2->friend1
				var revereFriend = await _userContext.FriendList.FirstOrDefaultAsync(f => f.FriendEmail == curUser.Email && f.UserEmail == friendEmail);
				_userContext.FriendList.Remove(revereFriend);
				await _userContext.SaveChangesAsync();

				//remove any lists that have been shared between them.
				var list = await _context.SharedLists.Where(l => ( l.UserEmail == friendEmail && l.FriendEmail == curUser.Email) || (l.FriendEmail == friendEmail && l.UserEmail == curUser.Email)).ToListAsync();
				list.ForEach((list) =>
				{
					_context.SharedLists.Remove(list);
				});
				await _context.SaveChangesAsync();
			}
			//TODO maybe let user know why it fails?
			catch (Exception e)
			{
				return Json(false);
			}
			return Json(true);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AcceptRequest(string friendEmail)
		{
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			var newFriend = new FriendModel();
			newFriend.UserEmail = curUser.Email;
			newFriend.FriendEmail = friendEmail;
			_userContext.FriendList.Add(newFriend);
			//bad way to deal with this probably, but we need to create friend model both ways.
			var reverseFriend = new FriendModel();
			reverseFriend.FriendEmail = curUser.Email;
			reverseFriend.UserEmail = friendEmail;
			_userContext.FriendList.Add(reverseFriend);

			var fRequest= await _userContext.FriendRequests.FirstOrDefaultAsync(r => r.SenderEmail == friendEmail);
			_userContext.FriendRequests.Remove(fRequest);

			await _userContext.SaveChangesAsync();

			var requests = Int32.Parse(HttpContext.Session.GetString("FriendRequests"));
			if (requests > 0)
			{
				requests -= 1;
			}
			if(requests > 0)
			{
				HttpContext.Session.SetString("FriendRequests", requests.ToString());
			}
			else
			{
				HttpContext.Session.Remove("FriendRequests");
			}

			return RedirectToAction(nameof(ManageFriends));
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> DeclineRequest(string friendEmail)
		{
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			var req = await _userContext.FriendRequests.FirstOrDefaultAsync(m => m.TargetEmail == User.Identity.Name);
			_userContext.FriendRequests.Remove(req);

			await _userContext.SaveChangesAsync();

			var requests = Int32.Parse(HttpContext.Session.GetString("FriendRequests"));
			if (requests > 0)
			{
				requests -= 1;
			}
			if (requests > 0)
			{
				HttpContext.Session.SetString("FriendRequests", requests.ToString());
			}
			else
			{
				HttpContext.Session.Remove("FriendRequests");
			}
			return RedirectToAction(nameof(ManageFriends));

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
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
			//Check if user has authorization to delete other users
			//TODO implement roles?

			//var user = await _userContext.Users.FindAsync(id);
			//_userContext.Users.Remove(user);
			//await _userContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserExists(string id)
		{
			return _userContext.Users.Any(e => e.Id == id);
		}
	}
}
