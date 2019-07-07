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
    public class StandardParentsController : Controller
    {
        private readonly MaximContext _context;

        public StandardParentsController(MaximContext context)
        {
            _context = context;
        }

        // GET: StandardParents
        public async Task<IActionResult> Index()
        {
            return View(await _context.StandardParent.ToListAsync());
        }

        // GET: StandardParents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardParent = await _context.StandardParent
                .Include(a => a.Standard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardParent == null)
            {
                return NotFound();
            }

            return View(standardParent);
        }

        // GET: StandardParents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StandardParents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ThumbnailTemp,Keywords")] StandardParent standardParent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standardParent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(standardParent);
        }

        // GET: StandardParents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardParent = await _context.StandardParent.FindAsync(id);
            if (standardParent == null)
            {
                return NotFound();
            }
            return View(standardParent);
        }

        // POST: StandardParents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Description,ThumbnailTemp,Keywords")] StandardParent standardParent)
        {
            if (id != standardParent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standardParent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardParentExists(standardParent.Id))
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
            return View(standardParent);
        }

        // GET: StandardParents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardParent = await _context.StandardParent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardParent == null)
            {
                return NotFound();
            }

            return View(standardParent);
        }

        // POST: StandardParents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var standardParent = await _context.StandardParent.FindAsync(id);
            _context.StandardParent.Remove(standardParent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandardParentExists(decimal id)
        {
            return _context.StandardParent.Any(e => e.Id == id);
        }
    }
}
