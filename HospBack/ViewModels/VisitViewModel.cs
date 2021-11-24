using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.ViewModels
{
	public class VisitViewModel
	{
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }


        public PatientViewModel Patient { get; set; }
        public DoctorViewModel Doctor { get; set; }
    }
}
