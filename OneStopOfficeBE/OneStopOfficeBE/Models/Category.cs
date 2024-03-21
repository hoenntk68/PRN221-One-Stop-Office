using System;
using System.Collections.Generic;

namespace OneStopOfficeBE.Models
{
    public partial class Category
    {
        public Category()
        {
            Requests = new HashSet<Request>();
            Users = new HashSet<User>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
