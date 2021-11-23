using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using HospBack.Repositories;

namespace HospBack.Services
{
	public interface IHospitalService
	{
		List<Hospital> GetAllHospitals(dbContext ctx);
		Hospital GetHospital(dbContext ctx, int id);
		void CreateHospital(dbContext ctx, Hospital hospital);
		void EditHospital(dbContext ctx, Hospital hospital);
		void DeleteHospital(dbContext ctx, int id);
	}
	public class HospitalService : IHospitalService
	{
		public void CreateHospital(dbContext ctx, Hospital hospital)
		{
			isDataCorrect(hospital);

			var model = ctx.Hospitals.Where(x => x.Name == hospital.Name).FirstOrDefault();
			if (model != null)
				throw new ModelAlreadyExistException();

			ctx.Hospitals.Add(hospital);
		}

		public void DeleteHospital(dbContext ctx, int id)
		{
			var model = ctx.Hospitals.Find(id);
			if (model == null)
				throw new ModelNotExistException();

			ctx.Hospitals.Remove(model);
		}

		public void EditHospital(dbContext ctx, Hospital hospital)
		{
			isDataCorrect(hospital);

			var model = ctx.Hospitals.Find(hospital.Id);
			if (model == null)
				throw new ModelNotExistException();

			model.Address = hospital.Address;
			model.Name = hospital.Name;
			model.PhoneNumber = hospital.PhoneNumber;
		}

		public List<Hospital> GetAllHospitals(dbContext ctx)
		{
			return ctx.Hospitals.ToList();
		}

		public Hospital GetHospital(dbContext ctx, int id)
		{
			return ctx.Hospitals.Find(id);
		}


		public bool isDataCorrect(Hospital hospital)
		{
			if (hospital?.Name != null && hospital?.Address != null && hospital?.PhoneNumber != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
