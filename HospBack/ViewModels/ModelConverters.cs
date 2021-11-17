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
			model.Name = registrar.Name;
			model.Surname = registrar.Surname;

			return model;
		}

		
	}
}
