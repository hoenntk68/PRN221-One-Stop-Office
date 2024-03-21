using System;
using System.Collections.Generic;

namespace OneStopOfficeBE.Models{
    public partial class User
    {
        public User()
        {
            RequestAssignedToNavigations = new HashSet<Request>();
            RequestUsers = new HashSet<Request>();
            Categories = new HashSet<Category>();
        }

        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public bool? IsTokenValid { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public virtual ICollection<Request> RequestAssignedToNavigations { get; set; }
        public virtual ICollection<Request> RequestUsers { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
