using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeMarco.Data;
using DeMarco.Models;
using Microsoft.AspNetCore.Authorization;

namespace DeMarco.Controllers
{
    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pizza.ToListAsync());
        }

        public async Task<IActionResult> Pizza()
        {
            return View(await _context.Pizza.ToListAsync());
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price")] Pizza pizza, [FromForm] IFormFile file)
        {
            // if image file is not loaded, remove from Modelstate
            if (file == null)
            {
                ModelState.Remove("file");
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {   // if image is loaded, it will be converted
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        pizza.ImageByte = binaryReader.ReadBytes((int)file.Length);
                        pizza.SaveImageAndMapPath();
                    }
                }

                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pizza == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberItem,ImagePath,Title,Weight,Description,Price")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //only Title, Describtion and Context will be updated
                    var existingPizza = await _context.Pizza.FindAsync(id);
                    if (existingPizza == null)
                    {
                        return NotFound();
                    }
                    existingPizza.NumberItem = pizza.NumberItem;
                    existingPizza.Title = pizza.Title;
                    existingPizza.Weight = pizza.Weight;
                    existingPizza.Description = pizza.Description;
                    existingPizza.Price = pizza.Price;
                    _context.Update(existingPizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
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
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pizza == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pizza == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pizza'  is null.");
            }
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizza.Remove(pizza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.Id == id);
        }

        // GET: Pizzas/Hide/5
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

        // POST: Pizzas/Hide/5
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

        // GET: Pizzas/Unhide/5
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

        // POST: Pizzas/Unhide/5
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