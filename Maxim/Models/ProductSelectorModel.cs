using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maximFinal.Models
{
    public partial class ProductSelectorModel 
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Industry> Industries { get; set; }

        public IEnumerable<Brand> Brands { get; set; }

        public IEnumerable<Standard> Standards { get; set; }

        public IEnumerable<Application> Applications { get; set; }

    }

}
