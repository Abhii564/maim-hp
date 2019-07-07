using System;
using System.Collections.Generic;

namespace maximFinal.Models
{
    public partial class MiscAttachment
    {
        public decimal PId { get; set; }
        public decimal Id { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public string Attachment { get; set; }

        public Brand IdNavigation { get; set; }
    }
}
