using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Hospital
    {
        public Hospital()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
