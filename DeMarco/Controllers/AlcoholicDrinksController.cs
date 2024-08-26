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
    public class AlcoholicDrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlcoholicDrinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlcoholicDrinks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AlcoholicDrink.ToListAsync());
        }

        public async Task<IActionResult> AlcoholicDrink()
        {
            return View(await _context.AlcoholicDrink.ToListAsync());
        }

        // GET: AlcoholicDrinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlcoholicDrinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] AlcoholicDrink alcoholicDrink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alcoholicDrink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alcoholicDrink);
        }

        // GET: AlcoholicDrinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alcoholicDrink = await _context.AlcoholicDrink.FindAsync(id);
            if (alcoholicDrink == null)
            {
                return NotFound();
            }
            return View(alcoholicDrink);
        }

        // POST: AlcoholicDrinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] AlcoholicDrink alcoholicDrink)
        {
            if (id != alcoholicDrink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alcoholicDrink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlcoholicDrinkExists(alcoholicDrink.Id))
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
            return View(alcoholicDrink);
        }

        // GET: AlcoholicDrinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alcoholicDrink = await _context.AlcoholicDrink
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alcoholicDrink == null)
            {
                return NotFound();
            }

            return View(alcoholicDrink);
        }

        // POST: AlcoholicDrinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alcoholicDrink = await _context.AlcoholicDrink.FindAsync(id);
            if (alcoholicDrink != null)
            {
                _context.AlcoholicDrink.Remove(alcoholicDrink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlcoholicDrinkExists(int id)
        {
            return _context.AlcoholicDrink.Any(e => e.Id == id);
        }

        // GET: AlcoholicDrinks/Hide/5
        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alcoholicDrink = await _context.AlcoholicDrink.FirstOrDefaultAsync(m => m.Id == id);
            if (alcoholicDrink == null)
            {
                return NotFound();
            }

            return View(alcoholicDrink);
        }

        // POST: AlcoholicDrinks/Hide/5
        [HttpPost]
        [ActionName("Hide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var alcoholicDrink = await _context.AlcoholicDrink.FindAsync(id);
            if (alcoholicDrink == null)
            {
                return NotFound();
            }

            // Označení produktu jako skrytého
            alcoholicDrink.IsHidden = true;

            // Aktualizace produktu v databázi
            _context.Update(alcoholicDrink);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }

        // GET: AlcoholicDrinks/Unhide/5
        public async Task<IActionResult> Unhide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alcoholicDrink = await _context.AlcoholicDrink.FirstOrDefaultAsync(m => m.Id == id);
            if (alcoholicDrink == null)
            {
                return NotFound();
            }

            return View(alcoholicDrink);
        }

        // POST: AlcoholicDrinks/Unhide/5
        [HttpPost, ActionName("Unhide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnhideConfirmed(int id)
        {
            var alcoholicDrink = await _context.AlcoholicDrink.FindAsync(id);
            if (alcoholicDrink == null)
            {
                return NotFound();
            }

            // Označení produktu jako ne-skrytého
            alcoholicDrink.IsHidden = false;

            // Aktualizace produktu v databázi
            _context.Update(alcoholicDrink);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }
    }
}