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
    public class NonAlcoholicDrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NonAlcoholicDrinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NonAlcoholicDrinks
        public async Task<IActionResult> Index()
        {
            return View(await _context.NonAlcoholicDrink.ToListAsync());
        }

        public async Task<IActionResult> NonAlcoholicDrink()
        {
            return View(await _context.NonAlcoholicDrink.ToListAsync());
        }

        // GET: NonAlcoholicDrinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NonAlcoholicDrinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] NonAlcoholicDrink nonAlcoholicDrink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonAlcoholicDrink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nonAlcoholicDrink);
        }

        // GET: NonAlcoholicDrinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonAlcoholicDrink = await _context.NonAlcoholicDrink.FindAsync(id);
            if (nonAlcoholicDrink == null)
            {
                return NotFound();
            }
            return View(nonAlcoholicDrink);
        }

        // POST: NonAlcoholicDrinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] NonAlcoholicDrink nonAlcoholicDrink)
        {
            if (id != nonAlcoholicDrink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonAlcoholicDrink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonAlcoholicDrinkExists(nonAlcoholicDrink.Id))
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
            return View(nonAlcoholicDrink);
        }

        // GET: NonAlcoholicDrinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonAlcoholicDrink = await _context.NonAlcoholicDrink
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nonAlcoholicDrink == null)
            {
                return NotFound();
            }

            return View(nonAlcoholicDrink);
        }

        // POST: NonAlcoholicDrinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonAlcoholicDrink = await _context.NonAlcoholicDrink.FindAsync(id);
            if (nonAlcoholicDrink != null)
            {
                _context.NonAlcoholicDrink.Remove(nonAlcoholicDrink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonAlcoholicDrinkExists(int id)
        {
            return _context.NonAlcoholicDrink.Any(e => e.Id == id);
        }

        // GET: NonAlcoholicDrinks/Hide/5
        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonAlcoholicDrink = await _context.NonAlcoholicDrink.FirstOrDefaultAsync(m => m.Id == id);
            if (nonAlcoholicDrink == null)
            {
                return NotFound();
            }

            return View(nonAlcoholicDrink);
        }

        // POST: NonAlcoholicDrinks/Hide/5
        [HttpPost]
        [ActionName("Hide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var nonAlcoholicDrink = await _context.NonAlcoholicDrink.FindAsync(id);
            if (nonAlcoholicDrink == null)
            {
                return NotFound();
            }

            // Označení produktu jako skrytého
            nonAlcoholicDrink.IsHidden = true;

            // Aktualizace produktu v databázi
            _context.Update(nonAlcoholicDrink);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }

        // GET: NonAlcoholicDrinks/Unhide/5
        public async Task<IActionResult> Unhide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonAlcoholicDrink = await _context.NonAlcoholicDrink.FirstOrDefaultAsync(m => m.Id == id);
            if (nonAlcoholicDrink == null)
            {
                return NotFound();
            }

            return View(nonAlcoholicDrink);
        }

        // POST: AlcoholicDrinks/Unhide/5
        [HttpPost, ActionName("Unhide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnhideConfirmed(int id)
        {
            var nonAlcoholicDrink = await _context.NonAlcoholicDrink.FindAsync(id);
            if (nonAlcoholicDrink == null)
            {
                return NotFound();
            }

            // Označení produktu jako ne-skrytého
            nonAlcoholicDrink.IsHidden = false;

            // Aktualizace produktu v databázi
            _context.Update(nonAlcoholicDrink);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }
    }
}
