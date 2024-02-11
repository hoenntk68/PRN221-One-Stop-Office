using System;
using System.Collections.Generic;

namespace OneStopOfficeBE.Models
{
    public partial class User
    {
        public User()
        {
            Requests = new HashSet<Request>();
            staff = new HashSet<staff>();
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

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
