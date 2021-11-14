using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class AspNetUser1
    {
        public AspNetUser1()
        {
            Analyses = new HashSet<Analysis>();
            AspNetUserClaim1s = new HashSet<AspNetUserClaim1>();
            AspNetUserLogin1s = new HashSet<AspNetUserLogin1>();
            AspNetUserRole1s = new HashSet<AspNetUserRole1>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Doctors = new HashSet<Doctor>();
            Visits = new HashSet<Visit>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<Analysis> Analyses { get; set; }
        public virtual ICollection<AspNetUserClaim1> AspNetUserClaim1s { get; set; }
        public virtual ICollection<AspNetUserLogin1> AspNetUserLogin1s { get; set; }
        public virtual ICollection<AspNetUserRole1> AspNetUserRole1s { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
