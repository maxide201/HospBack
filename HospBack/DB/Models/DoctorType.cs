using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class DoctorType
    {
        public DoctorType()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
