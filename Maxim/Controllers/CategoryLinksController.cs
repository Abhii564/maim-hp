using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maximFinal.Models;

namespace maximFinal.Controllers
{
    public class CategoryLinksController : Controller
    {
        private readonly MaximContext _context;

        public CategoryLinksController(MaximContext context)
        {
            _context = context;
        }

        // GET: CategoryLinks
        public async Task<IActionResult> Index()
        {
            var maximContext = _context.CategoryLink.Include(c => c.Category).Include(c => c.Product);
            return View(await maximContext.ToListAsync());
        }

        // GET: CategoryLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryLink = await _context.CategoryLink
                .Include(c => c.Category)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryLink == null)
            {
                return NotFound();
            }

            return View(categoryLink);
        }

        // GET: CategoryLinks/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: CategoryLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CategoryId")] CategoryLink categoryLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", categoryLink.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", categoryLink.ProductId);
            return View(categoryLink);
        }

        // GET: CategoryLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryLink = await _context.CategoryLink.FindAsync(id);
            if (categoryLink == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", categoryLink.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", categoryLink.ProductId);
            return View(categoryLink);
        }

        // POST: CategoryLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,CategoryId")] CategoryLink categoryLink)
        {
            if (id != categoryLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryLinkExists(categoryLink.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", categoryLink.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", categoryLink.ProductId);
            return View(categoryLink);
        }

        // GET: CategoryLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryLink = await _context.CategoryLink
                .Include(c => c.Category)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryLink == null)
            {
                return NotFound();
            }

            return View(categoryLink);
        }

        // POST: CategoryLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryLink = await _context.CategoryLink.FindAsync(id);
            _context.CategoryLink.Remove(categoryLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryLinkExists(int id)
        {
            return _context.CategoryLink.Any(e => e.Id == id);
        }
    }
}
