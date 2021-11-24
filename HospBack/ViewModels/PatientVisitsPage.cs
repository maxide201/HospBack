using HospBack.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.ViewModels
{
	public class PatientVisitsPage
	{
		public PatientViewModel Patient { get; set; }
		public List<Visit> Visits { get; set; }
	}
}
