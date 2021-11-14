using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Certificate
    {
        public Certificate()
        {
            Referrals = new HashSet<Referral>();
        }

        public int Id { get; set; }
        public int VisitId { get; set; }
        public string Description { get; set; }

        public virtual Visit Visit { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
