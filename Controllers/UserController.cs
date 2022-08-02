using System;
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
			UserListView list = new UserListView();
			list.UserList = await _userContext.Users.ToListAsync();

			return View(list);
		}
		// GET: Users
		[Authorize]
		public async Task<IActionResult> Friends()
		{
			//only users friends
			UserListView list = new UserListView();
			list.UserList = await _userContext.Users.ToListAsync();

			return View(list);
		}

		// GET: All users
		[Authorize]
		public async Task<IActionResult> FindFriends()
		{
			UserListView list = new UserListView();
			list.UserList = await _userContext.Users.ToListAsync();

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
			var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name) ;

			foreach (string s in nameArr)
			{
				var friendRequest = new FriendRequestModel();
				friendRequest.SenderEmail = curUser.Email;
				friendRequest.TargetEmail = s;
				_userContext.FriendRequests.Add(friendRequest);
			}
			await _userContext.SaveChangesAsync();
			
			return View();
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
