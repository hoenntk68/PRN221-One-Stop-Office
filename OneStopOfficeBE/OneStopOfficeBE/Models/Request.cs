using System;
using System.Collections.Generic;

namespace OneStopOfficeBE.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public string? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string? Reason { get; set; }
        public string? Attachment { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User? User { get; set; }
    }
}
