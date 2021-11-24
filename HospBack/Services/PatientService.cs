using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using HospBack.Repositories;

namespace HospBack.Services
{
	public interface IPatientService
	{
		List<Patient> GetAllPatients(dbContext ctx);
		Patient GetPatient(dbContext ctx, int id);
		void CreatePatient(dbContext ctx, Patient patient);
		void EditPatient(dbContext ctx, Patient patient);
		void DeletePatient(dbContext ctx, int id);
	}
	public class PatientService : IPatientService
	{
		public void CreatePatient(dbContext ctx, Patient patient)
		{
			isDataCorrect(patient);

			var model = ctx.Patients.Where(x => x.PhoneNumber == patient.PhoneNumber).FirstOrDefault();
			if (model != null)
				throw new ModelAlreadyExistException();

			ctx.Patients.Add(patient);
		}

		public void DeletePatient(dbContext ctx, int id)
		{
			var model = ctx.Patients.Find(id);
			if (model == null)
				throw new ModelNotExistException();

			ctx.Patients.Remove(model);
		}

		public void EditPatient(dbContext ctx, Patient patient)
		{
			isDataCorrect(patient);

			var model = ctx.Patients.Find(patient.Id);
			if (model == null)
				throw new ModelNotExistException();

			model.Name = patient.Name;
			model.Surname = patient.Surname;
		}

		public List<Patient> GetAllPatients(dbContext ctx)
		{
			return ctx.Patients.ToList();
		}

		public Patient GetPatient(dbContext ctx, int id)
		{
			return ctx.Patients.Find(id);
		}


		public bool isDataCorrect(Patient patient)
		{
			if (patient?.PhoneNumber != null && patient?.Name != null && patient?.Surname != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
