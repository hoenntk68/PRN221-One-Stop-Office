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
        public string? ProcessNote { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Status { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User? User { get; set; }
    }
}
