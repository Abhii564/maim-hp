using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maximFinal.Models
{
    public partial class IndustryModel
    {
        public IndustryModel()
        {
            Product = new HashSet<Product>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        //public string ProductName { get; set; }
        public string Description { get; set; }
        public string ThumbnailInd { get; set; }

        //public string ThumbnailPro { get; set; }
        public string Keywords { get; set; }

        
        public IEnumerable<Product> Product { get; set; }
       

    }
}
