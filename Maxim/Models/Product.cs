using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class Product
    {
        public Product()
        {
            ApplicationLink = new HashSet<ApplicationLink>();
            Attachment = new HashSet<Attachment>();
            CategoryLink = new HashSet<CategoryLink>();
            IndustryLink = new HashSet<IndustryLink>();
            StandardLink = new HashSet<StandardLink>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string Specification { get; set; }
        public string TempThumbnail { get; set; }
        public decimal BrandId { get; set; }
        public bool Featured { get; set; }

        public Brand Brand { get; set; }
        public ICollection<ApplicationLink> ApplicationLink { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
        public ICollection<CategoryLink> CategoryLink { get; set; }
        public ICollection<IndustryLink> IndustryLink { get; set; }
        public ICollection<StandardLink> StandardLink { get; set; }
    }
}
