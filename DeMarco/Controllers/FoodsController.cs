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
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Food.ToListAsync());
        }

        public async Task<IActionResult> Food()
        {
            return View(await _context.Food.ToListAsync());
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price,IsHidden")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
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
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food != null)
            {
                _context.Food.Remove(food);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.Id == id);
        }

        // GET: Foods/Hide/5
        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Hide/5
        [HttpPost]
        [ActionName("Hide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            // Označení produktu jako skrytého
            food.IsHidden = true;

            // Aktualizace produktu v databázi
            _context.Update(food);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }

        // GET: Foods/Unhide/5
        public async Task<IActionResult> Unhide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Unhide/5
        [HttpPost, ActionName("Unhide")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnhideConfirmed(int id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            // Označení produktu jako ne-skrytého
            food.IsHidden = false;

            // Aktualizace produktu v databázi
            _context.Update(food);
            await _context.SaveChangesAsync();

            // Přesměrování zpět na předchozí stránku
            return RedirectToAction("Index");
        }
    }
}