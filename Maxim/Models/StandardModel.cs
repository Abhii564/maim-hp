using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maximFinal.Models
{
    public class StandardModel
    {
        public StandardModel()
        {
            Product = new HashSet<Product>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailTemp { get; set; }
        public decimal ParentId { get; set; }
        //public string Keywords { get; set; }

        //public StandardParent Parent { get; set; }
        public IEnumerable<Product> Product { get; set; }
    }
}
