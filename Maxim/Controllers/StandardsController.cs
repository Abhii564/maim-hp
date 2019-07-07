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
    public class StandardsController : Controller
    {
        private readonly MaximContext _context;

        public StandardsController(MaximContext context)
        {
            _context = context;
        }

        // GET: Standards
        public async Task<IActionResult> Index()
        {
            var maximContext = _context.Standard.Include(s => s.Parent);
            return View(await maximContext.ToListAsync());
        }

        // GET: Standards/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = await _context.Standard
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            //if (standard == null)
            //{
            //    return NotFound();
            //}

            //return View(standard);

            StandardModel standardModel = new StandardModel();
            standardModel.Id = standard.Id;
            standardModel.Description = standard.Description;
            standardModel.Name = standard.Name;
            standardModel.ParentId = standard.ParentId;
            standardModel.ThumbnailTemp = standard.ThumbnailTemp;
            standardModel.Product = new HashSet<Product>(from s in _context.Standard
                                                         join sl in _context.StandardLink on s.Id equals sl.StandardId
                                                         join p in _context.Product on sl.ProductId equals p.Id
                                                         where s.Id == id
                                                         select sl.Product).Take(16);


            return View(standardModel);
        }

        // GET: Standards/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.StandardParent, "Id", "Id");
            return View();
        }

        // POST: Standards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ThumbnailTemp,ParentId")] Standard standard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.StandardParent, "Id", "Id", standard.ParentId);
            return View(standard);
        }

        // GET: Standards/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = await _context.Standard.FindAsync(id);
            if (standard == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.StandardParent, "Id", "Id", standard.ParentId);
            return View(standard);
        }

        // POST: Standards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Description,ThumbnailTemp,ParentId")] Standard standard)
        {
            if (id != standard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardExists(standard.Id))
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
            ViewData["ParentId"] = new SelectList(_context.StandardParent, "Id", "Id", standard.ParentId);
            return View(standard);
        }

        // GET: Standards/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = await _context.Standard
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standard == null)
            {
                return NotFound();
            }

            return View(standard);
        }

        // POST: Standards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var standard = await _context.Standard.FindAsync(id);
            _context.Standard.Remove(standard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandardExists(decimal id)
        {
            return _context.Standard.Any(e => e.Id == id);
        }
    }
}
