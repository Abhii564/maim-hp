using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class ApplicationLink
    {
        public decimal ApplicationId { get; set; }
        public decimal? ProductId { get; set; }
        public int Id { get; set; }

        public Application Application { get; set; }
        public Product Product { get; set; }
    }
}
