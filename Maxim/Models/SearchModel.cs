using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maximFinal.Models
{
    public partial class SearchModel
    {
        public string productDescription { get; set; }
        public string productName { get; set; }
        public string brandName { get; set; }
        public string brandDescription { get; set; }
        public string productThumbnail { get; set; }

        public decimal productId { get; set; }

        public string categoryName { get; set; }
        public decimal categoryId { get; set; }

    }

}
