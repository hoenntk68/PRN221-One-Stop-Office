using System;
using System.Collections.Generic;

namespace OneStopOfficeBE.Models
{
    public partial class staff
    {
        public staff()
        {
            Categories = new HashSet<Category>();
        }

        public int StaffId { get; set; }
        public string UserId { get; set; } = null!;
        public bool IsSuperAdmin { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
    }
}
