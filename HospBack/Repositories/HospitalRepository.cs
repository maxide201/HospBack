using HospBack.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Repositories
{
	public interface IHospitalRepository
	{
		List<Hospital> GetHospitals(dbContext ctx);
		void CreateHospital(dbContext ctx, Hospital hospital);
		void UpdateHospital(dbContext ctx, Hospital hospital);
		Hospital GetHospitalById(dbContext ctx, int id);
		Hospital GetHospitalByName(dbContext ctx, string name);
	}
	public class HospitalRepository : IHospitalRepository
	{ 
		public List<Hospital> GetHospitals(dbContext ctx) => 
			ctx.Hospitals.ToList();

		public void CreateHospital(dbContext ctx, Hospital hospital) =>
			ctx.Hospitals.Add(hospital);

		public void UpdateHospital(dbContext ctx, Hospital hospital) =>
			ctx.Hospitals.Update(hospital);

		public Hospital GetHospitalById(dbContext ctx, int id) =>
			ctx.Hospitals.Where(x => x.Id == id).FirstOrDefault();

		public Hospital GetHospitalByName(dbContext ctx, string name) =>
			ctx.Hospitals.Where(x => x.Name == name).FirstOrDefault();

	}
}
