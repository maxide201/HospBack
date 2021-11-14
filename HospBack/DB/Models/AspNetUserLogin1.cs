using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class AspNetUserLogin1
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser1 User { get; set; }
    }
}
