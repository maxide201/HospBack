using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Referral
    {
        public int Id { get; set; }
        public int CertificateId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string AnalyseType { get; set; }

        public virtual Certificate Certificate { get; set; }
    }
}
