using HospBack.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Repositories
{
	public interface IDoctorTypeRepository
	{
		List<DoctorType> GetDoctorTypes(dbContext ctx);
		void CreateDoctorType(dbContext ctx, DoctorType doctorType);
		void UpdateDoctorType(dbContext ctx, DoctorType doctorType);
		DoctorType GetDoctorTypeById(dbContext ctx, int id);
		DoctorType GetDoctorTypeByType(dbContext ctx, string type);
	}
	public class DoctorTypeRepository : IDoctorTypeRepository
	{
		public List<DoctorType> GetDoctorTypes(dbContext ctx) =>
			ctx.DoctorTypes.ToList();

		public void CreateDoctorType(dbContext ctx, DoctorType doctorType) =>
			ctx.DoctorTypes.Add(doctorType);

		public void UpdateDoctorType(dbContext ctx, DoctorType doctorType) =>
			ctx.DoctorTypes.Update(doctorType);

		public DoctorType GetDoctorTypeById(dbContext ctx, int id) =>
			ctx.DoctorTypes.Where(x => x.Id == id).FirstOrDefault();

		public DoctorType GetDoctorTypeByType(dbContext ctx, string type) =>
			ctx.DoctorTypes.Where(x => x.Type == type).FirstOrDefault();

		

	}
}