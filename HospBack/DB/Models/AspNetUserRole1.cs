using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class AspNetUserRole1
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser1 User { get; set; }
    }
}
