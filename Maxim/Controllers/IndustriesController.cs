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
    public class IndustriesController : Controller
    {
        private readonly MaximContext _context;

        public IndustriesController(MaximContext context)
        {
            _context = context;
        }

        // GET: Industries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Industry.ToListAsync());
        }

        // GET: Industries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industry = await _context.Industry
                .Include(a => a.IndustryLink) 
                .FirstOrDefaultAsync(m => m.Id == id);
            if (industry == null)
            {
                return NotFound();
            }

            //return View(industry);

            IndustryModel industrymodel = new IndustryModel();
            industrymodel.Id = industry.Id;
            industrymodel.Name = industry.Name;
            industrymodel.ThumbnailInd = industry.ThumbnailTemp;
            industrymodel.Description = industry.Description;
            industrymodel.Keywords = industry.Keywords;
            industrymodel.Product = new HashSet<Product>(from i in _context.Industry
                                                         join il in _context.IndustryLink on i.Id equals il.IndustryId
                                                         join p in _context.Product on il.ProductId equals p.Id
                                                         where i.Id == id
                                                         select il.Product).Take(16);

            return View(industrymodel);

        }

        // GET: Industries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Industries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ThumbnailTemp")] Industry industry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(industry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(industry);
        }

        // GET: Industries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industry = await _context.Industry.FindAsync(id);
            if (industry == null)
            {
                return NotFound();
            }
            return View(industry);
        }

        // POST: Industries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Description,ThumbnailTemp")] Industry industry)
        {
            if (id != industry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(industry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndustryExists(industry.Id))
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
            return View(industry);
        }

        // GET: Industries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industry = await _context.Industry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (industry == null)
            {
                return NotFound();
            }

            return View(industry);
        }

        // POST: Industries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var industry = await _context.Industry.FindAsync(id);
            _context.Industry.Remove(industry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndustryExists(decimal id)
        {
            return _context.Industry.Any(e => e.Id == id);
        }
    }
}
