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
		void CreatePatient(dbContext ctx, Patient patient);
		void EditPatient(dbContext ctx, Patient patient);
		void DeletePatient(dbContext ctx, int id);
	}
	public class PatientService : IPatientService
	{
		IPatientRepository _patientRepository;
		public PatientService(IPatientRepository patientRepository)
		{
			_patientRepository = patientRepository;
		}

		public void CreatePatient(dbContext ctx, Patient patient)
		{
			isDataCorrect(patient);

			var model = _patientRepository.GetPatientByPhoneNumber(ctx, patient.PhoneNumber);
			if (model != null)
				throw new ModelAlreadyExistException();

			_patientRepository.CreatePatient(ctx, patient);
		}

		public void DeletePatient(dbContext ctx, int id)
		{
			var model = _patientRepository.GetPatientById(ctx, id);
			if (model == null)
				throw new ModelNotExistException();

			//_patientRepository.Delete(ctx, id);
		}

		public void EditPatient(dbContext ctx, Patient patient)
		{
			isDataCorrect(patient);

			var model = _patientRepository.GetPatientById(ctx, patient.Id);
			if (model == null)
				throw new ModelNotExistException();

			_patientRepository.UpdatePatient(ctx, patient);
		}

		public List<Patient> GetAllPatients(dbContext ctx)
		{
			return _patientRepository.GetPatients(ctx);
		}


		public bool isDataCorrect(Patient patient)
		{
			if (patient?.PhoneNumber != null && patient?.Name != null && patient?.Surname != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
