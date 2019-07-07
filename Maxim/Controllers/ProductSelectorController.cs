using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using maximFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace maximFinal.Controllers
{
    public class ProductSelectorController : Controller
    {
        private readonly MaximContext _context = new MaximContext();
        public async Task<IActionResult> Index()
        {
            //var products =  _context.Product.Include(p => p.Id);
            //var brands = _context.Brand.Include(b => b.Id);
            //var industries = _context.Industry.Include(i => i.Id);

            //var maximContext = _context.Product.Include(p => p.Brand);
            var view = new ProductSelectorModel();

            view.Brands = await _context.Brand.ToListAsync();
            view.Products = await _context.Product.ToListAsync();
            view.Categories = await _context.Category.ToListAsync();
            view.Industries = await _context.Industry.ToListAsync();
            view.Standards = await _context.Standard.ToListAsync();
            view.Applications = await _context.Application.ToListAsync();

            return View(view);

        }

    }
}