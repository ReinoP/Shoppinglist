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

		public ShoppinglistController(ShoppingListContext context, UserManager<IdentityUser> manager)
		{
			_manager = manager;
			_context = context;
		}

		// GET: Shoppinglist
		[Authorize]
		public async Task<IActionResult> Index()
		{
			return View(await _context.Shoppinglists.ToListAsync());
		}

		// GET: Shoppinglist/Details/5
		[Authorize]
		public async Task<IActionResult> Details(Guid? id)
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

		// GET: Shoppinglist/Create
		[Authorize]
		public IActionResult Create()
		{
			return View(new Shoppinglist());
		}

		// POST: Shoppinglist/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create(string listName, string shoppinglist)
		{

			var arr = JsonConvert.DeserializeObject<string[]>(shoppinglist);
			Guid id = Guid.NewGuid();
			foreach (string s in arr)
			{
				var sli = new ShoppingListItem();
				sli.ItemName = s;
				sli.ListId = id;
				_context.ShoppingListItems.Add(sli);
			}

			var sl = new Shoppinglist();
			sl.ListName = listName;
			sl.ListID = id;
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


		// GET: Shoppinglist/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(Guid? id)
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

		// POST: Shoppinglist/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(string listName, string listId, string shoppinglist)
		{
			var arr = JsonConvert.DeserializeObject<string[]>(shoppinglist);
			var sl = await _context.Shoppinglists
						.FirstOrDefaultAsync(s => s.ListID == new Guid(listId));
			sl.ListName = listName;
			//this is most likely a bad way of saving/modifying/editing shopping list items...
			List<ShoppingListItem> items = await _context.ShoppingListItems.Where(i => i.ListId == new Guid(listId)).ToListAsync();
			foreach (ShoppingListItem item in items)
			{
				_context.ShoppingListItems.Remove(item);
			}

			foreach (string s in arr)
			{
				var sli = new ShoppingListItem();
				sli.ItemName = s;
				sli.ListId = new Guid(listId);
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
		public async Task<IActionResult> Delete(Guid? id)
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

		// POST: Shoppinglist/Delete/5
		[HttpPost, ActionName("Delete")]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var shoppinglist = await _context.Shoppinglists.FindAsync(id);
			_context.Shoppinglists.Remove(shoppinglist);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[Authorize]
		private bool ShoppinglistExists(Guid id)
		{
			return _context.Shoppinglists.Any(e => e.ListID == id);
		}
	}
}
