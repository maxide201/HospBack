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
			model.Name = patient.Name;
			model.Surname = patient.Surname;
			model.PhoneNumber = patient.PhoneNumber;
			model.Email = patient.Email;

			return model;
		}

		#endregion

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

	}
}
