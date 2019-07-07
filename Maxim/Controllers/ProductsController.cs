using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maximFinal.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace maximFinal.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MaximContext _context;

        public ProductsController(MaximContext context)
        {
            _context = context;
        }

        // GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var maximContext = _context.Product.Include(p => p.Brand);
        //    return View(await maximContext.ToListAsync());
        //}

        // GET: Products/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                                        

            var product = await _context.Product
              .Include(p => p.Brand)
              .Include(p => p.Attachment) //changed to get attachments 
              .Include(p => p.CategoryLink) 
              .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                {
                    return NotFound();
                }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Features,Specification,TempThumbnail,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Id", product.BrandId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Id", product.BrandId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Description,Features,Specification,TempThumbnail,BrandId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Id", product.BrandId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(decimal id)
        {
            return _context.Product.Any(e => e.Id == id);
        }


        //search functionality
        //public ViewResult Index(string searchString, int? pageNumber)
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            //var product = from p in _context.Product
            //              join b in _context.Brand on p.BrandId equals b.Id
            //              orderby p.BrandId
            //              select new SearchModel //search Model class is defined in model
            //              {
            //                  productDescription = p.Description,
            //                  productName = p.Name,
            //                  brandName = b.Name,
            //                  brandDescription = b.Description,
            //                  productThumbnail = p.TempThumbnail,
            //                  productId = p.Id
            //              };
            if (searchString != null)
            {
                pageNumber = 1;
            }

            var product = from p in _context.Product
                          join b in _context.Brand on p.BrandId equals b.Id
                          join cl in _context.CategoryLink on p.Id equals cl.ProductId
                          join c in _context.Category on cl.CategoryId equals c.Id
                          
                          select new SearchModel //search Model class is defined in model
                          {
                              productDescription = p.Description,
                              productName = p.Name,
                              brandName = b.Name,
                              brandDescription = b.Description,
                              productThumbnail = p.TempThumbnail,
                              productId = p.Id,
                              categoryId = c.Id,
                              categoryName = c.Name
                          };


            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.productDescription.Contains(searchString)
                                     || p.productName.Contains(searchString)
                                     || p.brandName.Contains(searchString)
                                     || p.brandDescription.Contains(searchString)); 
            }
            ViewBag.Filter = searchString;

            //get cateogries for filtering
            IDictionary<string, string> catResults = new Dictionary<string, string>();
            IDictionary<string, string> brandResults = new Dictionary<string, string>();
            

            List<string> brands = new List<string>();
            List<string> catResultList = new List<string>();
          


            //get the brands for filtering
            decimal prevProductId = 0; 
            foreach (var b in product)
            {
                if (prevProductId != b.productId)
                {
                    brands.Add(b.brandName);
                    prevProductId = b.productId;
                    
             
                }
                
                catResultList.Add(b.categoryName);

                
            }

            foreach (var brand in brands.Distinct())
            {
                brandResults.Add(brand, brands.Count(i => i.Equals(brand, StringComparison.InvariantCultureIgnoreCase)).ToString());
            }

            foreach (var cat in catResultList.Distinct())
            {
                catResults.Add(cat, catResultList.Count(i => i.Equals(cat, StringComparison.InvariantCultureIgnoreCase)).ToString());

            }

            product = product.GroupBy(o => new
            {
                o.productId

            }).Select(g => g.FirstOrDefault());
                      
            int pageSize = 10;
            //return View(product.ToList());
            return View(await PaginatedList<Product>.CreateAsync(product.AsNoTracking(), pageNumber ?? 1, pageSize,
                brandResults,
                catResults
                ));
        }


    }
}
