using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Doctor
    {
        public Doctor()
        {
            Schedules = new HashSet<Schedule>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public int HospitalId { get; set; }
        public int DoctorType { get; set; }

        public virtual DoctorType DoctorTypeNavigation { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual AspNetUser1 User { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
