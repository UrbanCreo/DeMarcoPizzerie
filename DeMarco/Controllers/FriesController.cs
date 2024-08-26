using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeMarco.Data;
using DeMarco.Models;

namespace DeMarco.Controllers
{
    public class FriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fries.ToListAsync());
        }

        public async Task<IActionResult> Fries()
        {
            return View(await _context.Fries.ToListAsync());
        }

        // GET: Fries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] Fries fries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fries);
        }

        // GET: Fries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fries = await _context.Fries.FindAsync(id);
            if (fries == null)
            {
                return NotFound();
            }
            return View(fries);
        }

        // POST: Fries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] Fries fries)
        {
            if (id != fries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriesExists(fries.Id))
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
            return View(fries);
        }

        // GET: Fries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fries = await _context.Fries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fries == null)
            {
                return NotFound();
            }

            return View(fries);
        }

        // POST: Fries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fries = await _context.Fries.FindAsync(id);
            if (fries != null)
            {
                _context.Fries.Remove(fries);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriesExists(int id)
        {
            return _context.Fries.Any(e => e.Id == id);
        }

        // GET: Fries/Hide/5
        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fries = await _context.Fries.FirstOrDefaultAsync(m => m.Id == id);
            if (fries == null)
            {
                return NotFound();
            }

            return View(fries);
        }

        // POST: Fries/Hide/5
        [HttpPost]
        [ActionName("Hide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var fries = await _context.Fries.FindAsync(id);
            if (fries == null)
            {
                return NotFound();
            }

            // Označení produktu jako skrytého
            fries.IsHidden = true;

            // Aktualizace produktu v databázi
            _context.Update(fries);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }

        // GET: Fries/Unhide/5
        public async Task<IActionResult> Unhide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fries = await _context.Fries.FirstOrDefaultAsync(m => m.Id == id);
            if (fries == null)
            {
                return NotFound();
            }

            return View(fries);
        }

        // POST: Fries/Unhide/5
        [HttpPost, ActionName("Unhide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnhideConfirmed(int id)
        {
            var fries = await _context.Fries.FindAsync(id);
            if (fries == null)
            {
                return NotFound();
            }

            // Označení produktu jako ne-skrytého
            fries.IsHidden = false;

            // Aktualizace produktu v databázi
            _context.Update(fries);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }
    }
}