using HospBack.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Repositories
{
	public interface IPatientRepository
	{
		List<Patient> GetPatients(dbContext ctx);
		void CreatePatient(dbContext ctx, Patient patient);
		void UpdatePatient(dbContext ctx, Patient patient);
		Patient GetPatientById(dbContext ctx, int id);
		Patient GetPatientByPhoneNumber(dbContext ctx, int id);
	}
	public class PatientRepository : IPatientRepository
	{
		public List<Patient> GetPatients(dbContext ctx) =>
			ctx.Patients.ToList();

		public void CreatePatient(dbContext ctx, Patient patient) =>
			ctx.Patients.Add(patient);

		public void UpdatePatient(dbContext ctx, Patient patient) =>
			ctx.Patients.Update(patient);

		public Patient GetPatientById(dbContext ctx, int id) =>
			ctx.Patients.Where(x => x.Id == id).FirstOrDefault();

		public Patient GetPatientByPhoneNumber(dbContext ctx, int id) =>
			ctx.Patients.Where(x => x.Id == id).FirstOrDefault();

	}
}