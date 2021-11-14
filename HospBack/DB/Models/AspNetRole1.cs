using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class AspNetRole1
    {
        public AspNetRole1()
        {
            AspNetRoleClaim1s = new HashSet<AspNetRoleClaim1>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaim1> AspNetRoleClaim1s { get; set; }
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
