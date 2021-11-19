using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using HospBack.Repositories;

namespace HospBack.Services
{
	public interface IDoctorService
	{
		List<Doctor> GetAllDoctors(dbContext ctx);
		void CreateDoctor(dbContext ctx, Doctor doctor);
		void EditDoctor(dbContext ctx, Doctor doctor);
		void DeleteDoctor(dbContext ctx, int id);
	}
	public class DoctorService : IDoctorService
	{
		IDoctorRepository _doctorRepository;
		public DoctorService(IDoctorRepository doctorRepository)
		{
			_doctorRepository = doctorRepository;
		}

		public void CreateDoctor(dbContext ctx, Doctor doctor)
		{
			isDataCorrect(doctor);

			var model = _doctorRepository.GetDoctorByUserId(ctx, doctor.UserId);
			if (model != null)
				throw new ModelAlreadyExistException();

			_doctorRepository.CreateDoctor(ctx, doctor);
		}

		public void DeleteDoctor(dbContext ctx, int id)
		{
			var model = _doctorRepository.GetDoctorById(ctx, id);
			if (model == null)
				throw new ModelNotExistException();

			//_doctorRepository.Delete(ctx, id);
		}

		public void EditDoctor(dbContext ctx, Doctor doctor)
		{
			isDataCorrect(doctor);

			var model = _doctorRepository.GetDoctorById(ctx, doctor.Id);
			if (model == null)
				throw new ModelNotExistException();

			_doctorRepository.UpdateDoctor(ctx, doctor);
		}

		public List<Doctor> GetAllDoctors(dbContext ctx)
		{
			return _doctorRepository.GetDoctors(ctx);
		}


		public bool isDataCorrect(Doctor doctor)
		{
			if (doctor?.Name != null && doctor?.Surname != null && doctor?.UserId != null && doctor?.HospitalId != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
