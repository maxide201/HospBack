using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class AspNetUserToken
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual AspNetUser1 User { get; set; }
    }
}
