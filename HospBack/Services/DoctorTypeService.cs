using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using HospBack.Repositories;

namespace HospBack.Services
{
	public interface IDoctorTypeService
	{
		List<DoctorType> GetAllDoctorTypes(dbContext ctx);
		void CreateDoctorType(dbContext ctx, DoctorType doctorType);
		void EditDoctorType(dbContext ctx, DoctorType doctorType);
		void DeleteDoctorType(dbContext ctx, int id);
	}
	public class DoctorTypeService : IDoctorTypeService
	{
		IDoctorTypeRepository _doctorTypeRepository;
		public DoctorTypeService(IDoctorTypeRepository doctorTypeRepository)
		{
			_doctorTypeRepository = doctorTypeRepository;
		}

		public void CreateDoctorType(dbContext ctx, DoctorType doctorType)
		{
			isDataCorrect(doctorType);

			var model = _doctorTypeRepository.GetDoctorTypeByType(ctx, doctorType.Type);
			if (model != null)
				throw new ModelAlreadyExistException();

			_doctorTypeRepository.CreateDoctorType(ctx, doctorType);
		}

		public void DeleteDoctorType(dbContext ctx, int id)
		{
			var model = _doctorTypeRepository.GetDoctorTypeById(ctx, id);
			if (model == null)
				throw new ModelNotExistException();

			//_doctorTypeRepository.Delete(ctx, id);
		}

		public void EditDoctorType(dbContext ctx, DoctorType doctorType)
		{
			isDataCorrect(doctorType);

			var model = _doctorTypeRepository.GetDoctorTypeById(ctx, doctorType.Id);
			if (model == null)
				throw new ModelNotExistException();

			_doctorTypeRepository.UpdateDoctorType(ctx, doctorType);
		}

		public List<DoctorType> GetAllDoctorTypes(dbContext ctx)
		{
			return _doctorTypeRepository.GetDoctorTypes(ctx);
		}


		public bool isDataCorrect(DoctorType doctorType)
		{
			if (doctorType?.Type != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
