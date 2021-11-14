using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class HospitalDoctorType
    {
        public int HospitalId { get; set; }
        public int DoctorType { get; set; }

        public virtual DoctorType DoctorTypeNavigation { get; set; }
        public virtual Hospital Hospital { get; set; }
    }
}
