using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // GET: Shoppinglists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shoppinglists.ToListAsync());
        }

        // GET: Shoppinglists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppinglist = await _context.Shoppinglists
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (shoppinglist == null)
            {
                return NotFound();
            }

            return View(shoppinglist);
        }

        // GET: Shoppinglists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoppinglists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,ListID")] Shoppinglist shoppinglist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppinglist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppinglist);
        }

        // GET: Shoppinglists/Edit/5
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

        // POST: Shoppinglists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,ListID")] Shoppinglist shoppinglist)
        {
            if (id != shoppinglist.UserID)
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
                    if (!ShoppinglistExists(shoppinglist.UserID))
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

        // GET: Shoppinglists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppinglist = await _context.Shoppinglists
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (shoppinglist == null)
            {
                return NotFound();
            }

            return View(shoppinglist);
        }

        // POST: Shoppinglists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppinglist = await _context.Shoppinglists.FindAsync(id);
            _context.Shoppinglists.Remove(shoppinglist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppinglistExists(int id)
        {
            return _context.Shoppinglists.Any(e => e.UserID == id);
        }
    }
}
