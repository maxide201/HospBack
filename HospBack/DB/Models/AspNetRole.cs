using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            AspNetUserRole1s = new HashSet<AspNetUserRole1>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual ICollection<AspNetUserRole1> AspNetUserRole1s { get; set; }
    }
}
