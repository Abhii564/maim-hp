using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class IndustryLink
    {
        public int Id { get; set; }
        public decimal IndustryId { get; set; }
        public decimal? ProductId { get; set; }

        public Industry Industry { get; set; }
        public Product Product { get; set; }
    }
}
