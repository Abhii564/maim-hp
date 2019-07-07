using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class Standard
    {
        public Standard()
        {
            StandardLink = new HashSet<StandardLink>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailTemp { get; set; }
        public decimal ParentId { get; set; }
        public string Keywords { get; set; }

        public StandardParent Parent { get; set; }
        public ICollection<StandardLink> StandardLink { get; set; }
    }
}
