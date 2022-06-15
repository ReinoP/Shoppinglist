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
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var shoppinglist = await _context.Shoppinglists
          .FirstOrDefaultAsync(m => m.ListID == id);
      if (shoppinglist == null)
      {
        return NotFound();
      }

      return View(shoppinglist);
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
      var slList = new List<ShoppingListItem>();
      foreach (string s in arr)
      {
        var sli = new ShoppingListItem(s);
        slList.Add(sli);
      }
      var sl = new Shoppinglist();
      sl.ListName = listName;
      sl.Items = slList;
      //check if user has an existing list of shoppinglists, if so, add, if not, create.
      //TODO should i remove User-class and just use IdentityUser? where to save info about existing lists etc?

      if (ModelState.IsValid)
			{
				_context.Add(sl);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
    }


    // GET: Shoppinglist/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var shoppinglist = await _context.Shoppinglists.FindAsync(id);
      if (shoppinglist == null)
      {
        return NotFound();
      }
      return View(shoppinglist);
    }

    // POST: Shoppinglist/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(int id, [Bind("ListID,ListName")] Shoppinglist shoppinglist)
    {
      if (id != shoppinglist.ListID)
      {
          return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(shoppinglist);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ShoppinglistExists(shoppinglist.ListID))
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
    public async Task<IActionResult> Delete(int? id)
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
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var shoppinglist = await _context.Shoppinglists.FindAsync(id);
      _context.Shoppinglists.Remove(shoppinglist);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    [Authorize]
    private bool ShoppinglistExists(int id)
    {
      return _context.Shoppinglists.Any(e => e.ListID == id);
    }
  }
}
