using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Visit
    {
        public Visit()
        {
            Certificates = new HashSet<Certificate>();
        }

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string UserId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}
