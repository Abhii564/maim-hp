using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class CategoryLink
    {
        public int Id { get; set; }
        public decimal ProductId { get; set; }
        public decimal CategoryId { get; set; }

        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
