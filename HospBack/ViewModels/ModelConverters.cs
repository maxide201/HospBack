using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospBack.DB;

namespace HospBack.ViewModels
{
    public static class ModelConverters
    {
		#region Patient

		public static Patient ToDataModel(this PatientViewModel patient)
		{
			if (patient == null)
				return null;

			var model = new Patient();
			model.Id = patient.Id;
			model.Name = patient.Name;
			model.Surname = patient.Surname;
			model.PhoneNumber = patient.PhoneNumber;
			model.Email = patient.Email;

			return model;
		}
		public static PatientViewModel ToViewModel(this Patient patient)
		{
			if (patient == null)
				return null;

			var model = new PatientViewModel();
			model.Id = patient.Id;
			model.Name = patient.Name;
			model.Surname = patient.Surname;
			model.PhoneNumber = patient.PhoneNumber;
			model.Email = patient.Email;

			return model;
		}

		#endregion

		#region Registrar
		public static Registrar ToDataModel(this RegistrarViewModel registrar)
		{
			if (registrar == null)
				return null;

			var model = new Registrar();
			model.Id = registrar.Id;
			model.Name = registrar.Name;
			model.Surname = registrar.Surname;

			return model;
		}

		public static RegistrarViewModel ToViewModel(this Registrar registrar, string email)
		{
			if (registrar == null)
				return null;

			var model = new RegistrarViewModel();
			model.Id = registrar.Id;
			model.Name = registrar.Name;
			model.Surname = registrar.Surname;
			model.Email = email;

			return model;
		}
		#endregion

		#region Hospital
		public static Hospital ToDataModel(this HospitalViewModel hospital)
		{
			if (hospital == null)
				return null;

			var model = new Hospital();
			model.Id = hospital.Id;
			model.Name = hospital.Name;
			model.PhoneNumber = hospital.PhoneNumber;
			model.Address = hospital.Address;

			return model;
		}

		public static HospitalViewModel ToViewModel(this Hospital hospital)
		{
			if (hospital == null)
				return null;

			var model = new HospitalViewModel();
			model.Id = hospital.Id;
			model.Name = hospital.Name;
			model.PhoneNumber = hospital.PhoneNumber;
			model.Address = hospital.Address;

			return model;
		}
		#endregion

		#region DoctorType
		public static DoctorType ToDataModel(this DoctorTypeViewModel doctorType)
		{
			if (doctorType == null)
				return null;

			var model = new DoctorType();
			model.Id = doctorType.Id;
			model.Type = doctorType.Type;

			return model;
		}

		public static DoctorTypeViewModel ToViewModel(this DoctorType doctorType)
		{
			if (doctorType == null)
				return null;

			var model = new DoctorTypeViewModel();
			model.Id = doctorType.Id;
			model.Type = doctorType.Type;

			return model;
		}
		#endregion

		#region Doctor
		public static Doctor ToDataModel(this DoctorViewModel doctor)
		{
			if (doctor == null)
				return null;

			var model = new Doctor();
			model.Id = doctor.Id;
			model.Name = doctor.Name;
			model.Surname = doctor.Surname;
			model.DoctorType = Int32.Parse(doctor.DoctorType);
			model.HospitalId = Int32.Parse(doctor.HospitalId);

			return model;
		}

		public static DoctorViewModel ToViewModel(this Doctor doctor, string email = null)
		{
			if (doctor == null)
				return null;

			var model = new DoctorViewModel();
			model.Id = doctor.Id;
			model.Email = email;
			model.Name = doctor.Name;
			model.Surname = doctor.Surname;
			model.DoctorType = doctor.DoctorType.ToString();
			model.HospitalId = doctor.HospitalId.ToString();

			return model;
		}
		#endregion

		public static Schedule ToDataModel(this ScheduleViewModel schedule)
		{
			if (schedule == null)
				return null;

			var model = new Schedule();
			model.Id = schedule.Id;
			model.DoctorId = schedule.DoctorId;
			model.Day = schedule.Day;
			model.StartTime = schedule.StartTime;
			model.EndTime = schedule.EndTime;
			model.VisitTime = schedule.VisitTime;

			return model;
		}
	}
}
