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
		void CreateHospital(dbContext ctx, Hospital hospital);
		void EditHospital(dbContext ctx, Hospital hospital);
		void DeleteHospital(dbContext ctx, int id);
	}
	public class HospitalService : IHospitalService
	{
		IHospitalRepository _hospitalRepository;
		public HospitalService(IHospitalRepository hospitalRepository)
		{
			_hospitalRepository = hospitalRepository;
		}

		public void CreateHospital(dbContext ctx, Hospital hospital)
		{
			isDataCorrect(hospital);

			var model = _hospitalRepository.GetHospitalByName(ctx, hospital.Name);
			if (model != null)
				throw new ModelAlreadyExistException();

			_hospitalRepository.CreateHospital(ctx, hospital);
		}

		public void DeleteHospital(dbContext ctx, int id)
		{
			var model = _hospitalRepository.GetHospitalById(ctx, id);
			if (model == null)
				throw new ModelNotExistException();

			//_hospitalRepository.Delete(ctx, id);
		}

		public void EditHospital(dbContext ctx, Hospital hospital)
		{
			isDataCorrect(hospital);

			var model = _hospitalRepository.GetHospitalById(ctx, hospital.Id);
			if (model == null)
				throw new ModelNotExistException();

			_hospitalRepository.UpdateHospital(ctx, hospital);
		}

		public List<Hospital> GetAllHospitals(dbContext ctx)
		{
			return _hospitalRepository.GetHospitals(ctx);
		}


		public bool isDataCorrect(Hospital hospital)
		{
			if (hospital?.Name != null && hospital?.Address != null && hospital?.PhoneNumber != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
