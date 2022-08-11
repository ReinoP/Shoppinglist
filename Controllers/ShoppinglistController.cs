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
using Newtonsoft.Json.Linq;
using ShoppinglistApp.Data;
using ShoppinglistApp.Models;

namespace ShoppinglistApp.Controllers
{
	public class ShoppinglistController : Controller
	{
		private readonly ShoppingListContext _context;
		private readonly UserManager<IdentityUser> _manager;
		private readonly UserDbContext _userContext;


		public ShoppinglistController(ShoppingListContext context, UserManager<IdentityUser> manager, UserDbContext ucontext)
		{
			_manager = manager;
			_context = context;
			_userContext = ucontext;

		}


		[Authorize]
		public async Task<IActionResult> Index()
		{
			var curUser = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

			var userId = _manager.GetUserId(User);
			var list = await _context.Shoppinglists.Where(u => u.ListID == userId).ToListAsync();

			var friendsList = await _userContext.FriendList.Where(f => f.FriendEmail == curUser.Email).ToListAsync();

			ViewBag.FriendList = friendsList;

			return View(list);
		}


		[Authorize]
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var shoppinglist = await _context.Shoppinglists
					.FirstOrDefaultAsync(m => m.ListID == id);
			List<ShoppingListItem> items = await _context.ShoppingListItems.Where(m => m.ListId == id).ToListAsync();
			var list = new ShoppingListViewModel();
			list.Items = items;
			list.ListID = shoppinglist.ListID;
			list.ListName = shoppinglist.ListName;
			list.UserID = shoppinglist.UserID;
			if (shoppinglist == null)
			{
				return NotFound();
			}

			return View(list);
		}


		[Authorize]
		public IActionResult Create()
		{
			return View(new Shoppinglist());
		}


		[HttpPost]
	
		[Authorize]
		public async Task<IActionResult> Create(string listName, string shoppinglist)
		{

			var arr = JsonConvert.DeserializeObject<string[]>(shoppinglist);
			var userId = _manager.GetUserId(User);
			;
			foreach (string s in arr)
			{
				var sli = new ShoppingListItem();
				sli.ItemName = s;
				sli.ListId = userId;
				_context.ShoppingListItems.Add(sli);
			}

			var sl = new Shoppinglist();
			sl.ListName = listName;
			sl.ListID = userId;
			sl.UserID = User.Identity.Name;


			//check if user has an existing list of shoppinglists, if so, add, if not, create.
			//TODO should i remove User-class and just use IdentityUser? where to save info about existing lists etc?

			if (ModelState.IsValid)
			{
				_context.Shoppinglists.Add(sl);

				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}



		[Authorize]
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var shoppinglist = await _context.Shoppinglists
					.FirstOrDefaultAsync(m => m.ListID == id);
			List<ShoppingListItem> items = await _context.ShoppingListItems.Where(m => m.ListId == id).ToListAsync();
			var list = new ShoppingListViewModel();
			list.Items = items;
			list.ListID = shoppinglist.ListID;
			list.ListName = shoppinglist.ListName;
			list.UserID = shoppinglist.UserID;
			if (shoppinglist == null)
			{
				return NotFound();
			}

			return View(list);
		}


		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(string listName, string listId, string shoppinglist)
		{
			var arr = JsonConvert.DeserializeObject<string[]>(shoppinglist);
			var sl = await _context.Shoppinglists
						.FirstOrDefaultAsync(s => s.ListID == listId);
			sl.ListName = listName;
			//this is most likely a bad way of saving/modifying/editing shopping list items...
			List<ShoppingListItem> items = await _context.ShoppingListItems.Where(i => i.ListId == listId).ToListAsync();
			foreach (ShoppingListItem item in items)
			{
				_context.ShoppingListItems.Remove(item);
			}

			foreach (string s in arr)
			{
				var sli = new ShoppingListItem();
				sli.ItemName = s;
				sli.ListId = listId;
				_context.ShoppingListItems.Add(sli);
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(sl);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ShoppinglistExists(sl.ListID))
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
			return View(shoppinglist);
		}

		// GET: Shoppinglist/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var shoppinglist = await _context.Shoppinglists.FirstOrDefaultAsync(m => m.ListID == id);
			if (shoppinglist == null)
			{
				return NotFound();
			}

			return View(shoppinglist);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> ShareList(string listId, string listName, string targetEmail)
		{
			Debug.WriteLine("list name " + listName);
			var curUser = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
			try
			{
				var shareList = new SharedListModel();
				shareList.FriendEmail = targetEmail;
				shareList.ListID = listId;
				shareList.ListName = listName;
				shareList.UserEmail = curUser.Email;

				_context.SharedLists.Add(shareList);
				await _context.SaveChangesAsync();
			}
			catch(Exception e)
			{
				return Json(false);
			}

			return Json(true);
		}

		[Authorize]
		public async Task<IActionResult> SharedLists()
		{
			var curUser = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
			var shoppinglists = await _context.SharedLists.Where(u=> u.FriendEmail == curUser.Email).ToListAsync();

			return View(shoppinglists);
		}

		// POST: Shoppinglist/Delete/5
		[HttpPost, ActionName("Delete")]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var shoppinglist = await _context.Shoppinglists.FindAsync(id);
			_context.Shoppinglists.Remove(shoppinglist);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[Authorize]
		private bool ShoppinglistExists(string id)
		{
			return _context.Shoppinglists.Any(e => e.ListID == id);
		}
	}
}
