using HospBack.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.ViewModels
{
	public class AddSchedulePageModelView
	{
		public DoctorViewModel Doctor { get; set; }
		public List<Schedule> Schedules { get; set; }
	}
}
