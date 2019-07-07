using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class StandardLink
    {
        public int Id { get; set; }
        public decimal StandardId { get; set; }
        public decimal? ProductId { get; set; }

        public Product Product { get; set; }
        public Standard Standard { get; set; }
    }
}
