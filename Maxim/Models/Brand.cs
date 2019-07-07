using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class Brand
    {
        public Brand()
        {
            MiscAttachment = new HashSet<MiscAttachment>();
            Product = new HashSet<Product>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailTemp { get; set; }
        public string Keywords { get; set; }

        public ICollection<MiscAttachment> MiscAttachment { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
