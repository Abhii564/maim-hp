using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class Application
    {
        public Application()
        {
            ApplicationLink = new HashSet<ApplicationLink>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }

        public ICollection<ApplicationLink> ApplicationLink { get; set; }
    }
}
