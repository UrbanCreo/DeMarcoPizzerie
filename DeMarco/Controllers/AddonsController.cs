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
    public class AddonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Addons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Addon.ToListAsync());
        }

        public async Task<IActionResult> Addon()
        {
            return View(await _context.Addon.ToListAsync());
        }


        // GET: Addons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,IsHidden")] Addon addon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addon);
        }

        // GET: Addons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addon = await _context.Addon.FindAsync(id);
            if (addon == null)
            {
                return NotFound();
            }
            return View(addon);
        }

        // POST: Addons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,IsHidden")] Addon addon)
        {
            if (id != addon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddonExists(addon.Id))
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
            return View(addon);
        }

        // GET: Addons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addon = await _context.Addon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addon == null)
            {
                return NotFound();
            }

            return View(addon);
        }

        // POST: Addons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addon = await _context.Addon.FindAsync(id);
            if (addon != null)
            {
                _context.Addon.Remove(addon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddonExists(int id)
        {
            return _context.Addon.Any(e => e.Id == id);
        }

        // GET: Pastas/Hide/5
        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addon = await _context.Addon.FirstOrDefaultAsync(m => m.Id == id);
            if (addon == null)
            {
                return NotFound();
            }

            return View(addon);
        }

        // POST: Pastas/Hide/5
        [HttpPost]
        [ActionName("Hide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var addon = await _context.Addon.FindAsync(id);
            if (addon == null)
            {
                return NotFound();
            }

            // Označení produktu jako skrytého
            addon.IsHidden = true;

            // Aktualizace produktu v databázi
            _context.Update(addon);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }

        // GET: Pastas/Unhide/5
        public async Task<IActionResult> Unhide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addon = await _context.Addon.FirstOrDefaultAsync(m => m.Id == id);
            if (addon == null)
            {
                return NotFound();
            }

            return View(addon);
        }

        // POST: Pastas/Unhide/5
        [HttpPost, ActionName("Unhide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnhideConfirmed(int id)
        {
            var addon = await _context.Addon.FindAsync(id);
            if (addon == null)
            {
                return NotFound();
            }

            // Označení produktu jako ne-skrytého
            addon.IsHidden = false;

            // Aktualizace produktu v databázi
            _context.Update(addon);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }
    }
}