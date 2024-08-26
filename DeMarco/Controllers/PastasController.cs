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
    public class PastasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PastasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pastas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pasta.ToListAsync());
        }

        public async Task<IActionResult> Pasta()
        {
            return View(await _context.Pasta.ToListAsync());
        }

        // GET: Pastas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pastas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] Pasta pasta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pasta);
        }

        // GET: Pastas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta.FindAsync(id);
            if (pasta == null)
            {
                return NotFound();
            }
            return View(pasta);
        }

        // POST: Pastas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] Pasta pasta)
        {
            if (id != pasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PastaExists(pasta.Id))
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
            return View(pasta);
        }

        // GET: Pastas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasta == null)
            {
                return NotFound();
            }

            return View(pasta);
        }

        // POST: Pastas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasta = await _context.Pasta.FindAsync(id);
            if (pasta != null)
            {
                _context.Pasta.Remove(pasta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PastaExists(int id)
        {
            return _context.Pasta.Any(e => e.Id == id);
        }

        // GET: Pastas/Hide/5
        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza.FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pastas/Hide/5
        [HttpPost]
        [ActionName("Hide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            // Označení produktu jako skrytého
            pizza.IsHidden = true;

            // Aktualizace produktu v databázi
            _context.Update(pizza);
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

            var pizza = await _context.Pizza.FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pastas/Unhide/5
        [HttpPost, ActionName("Unhide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnhideConfirmed(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            // Označení produktu jako ne-skrytého
            pizza.IsHidden = false;

            // Aktualizace produktu v databázi
            _context.Update(pizza);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }
    }
}