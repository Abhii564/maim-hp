using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class Attachment
    {
        public decimal Id { get; set; }
        public decimal? ProductId { get; set; }
        public string Type { get; set; }
        public string ThumbnailTemp { get; set; }

        public Product Product { get; set; }
    }
}
