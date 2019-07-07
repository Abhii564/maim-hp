using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class StandardParent
    {
        public StandardParent()
        {
            Standard = new HashSet<Standard>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailTemp { get; set; }
        public string Keywords { get; set; }

        public ICollection<Standard> Standard { get; set; }
    }
}
