using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class Industry
    {
        public Industry()
        {
            IndustryLink = new HashSet<IndustryLink>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailTemp { get; set; }
        public string Keywords { get; set; }

        public ICollection<IndustryLink> IndustryLink { get; set; }
    }
}
