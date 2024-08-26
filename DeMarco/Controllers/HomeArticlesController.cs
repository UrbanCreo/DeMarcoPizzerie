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
    public class HomeArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeArticle.ToListAsync());
        }

        // GET: HomeArticles
        public async Task<IActionResult> Home()
        {
            return View(await _context.HomeArticle.ToListAsync());
        }

        // GET: HomeArticles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] HomeArticle homeArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeArticle);
        }

        // GET: HomeArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeArticle = await _context.HomeArticle.FindAsync(id);
            if (homeArticle == null)
            {
                return NotFound();
            }
            return View(homeArticle);
        }

        // POST: HomeArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] HomeArticle homeArticle)
        {
            if (id != homeArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeArticleExists(homeArticle.Id))
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
            return View(homeArticle);
        }

        // GET: HomeArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeArticle = await _context.HomeArticle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeArticle == null)
            {
                return NotFound();
            }

            return View(homeArticle);
        }

        // POST: HomeArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeArticle = await _context.HomeArticle.FindAsync(id);
            if (homeArticle != null)
            {
                _context.HomeArticle.Remove(homeArticle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeArticleExists(int id)
        {
            return _context.HomeArticle.Any(e => e.Id == id);
        }
    }
}
