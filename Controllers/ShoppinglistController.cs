using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

    public ShoppinglistController(ShoppingListContext context)
    {
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
      return View();
    }

    // POST: Shoppinglist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    //[ValidateAntiForgeryToken]
    [Authorize]
    public async Task<JsonResult> Create(string shoppinglist)
    {
      //TODO user id added to shoppinglist here or earlier
      Debug.WriteLine("IN CONTROLLER " + shoppinglist);
      //var sl = JsonConvert.DeserializeObject<>(shoppinglist);


   //   sl.ListName = sl.ListName;
			//if (ModelState.IsValid)
			//{
			//	_context.Add(shoppinglist);
			//	await _context.SaveChangesAsync();
			//	return RedirectToAction(nameof(Index));
			//}
			return Json(shoppinglist);
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
