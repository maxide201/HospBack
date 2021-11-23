using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.ViewModels
{
	public class DoctorViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public int DoctorType { get; set; }
		public int HospitalId { get; set; }
	}
}
