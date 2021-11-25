using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using HospBack.DB;
using HospBack.Exceptions;

namespace HospBack.Services
{
	public interface IDoctorService
	{
		List<Doctor> GetAllDoctors(dbContext ctx);
		Doctor GetDoctor(dbContext ctx, int id);
		Doctor GetDoctorByUserId(dbContext ctx, string id);
		List<Doctor> GetDoctorsByHospitalIdAndType(dbContext ctx, int hospitalId, int doctorType);
		void CreateDoctor(dbContext ctx, Doctor doctor);
		void EditDoctor(dbContext ctx, Doctor doctor);
		void DeleteDoctor(dbContext ctx, int id);
	}
	public class DoctorService : IDoctorService
	{
		public void CreateDoctor(dbContext ctx, Doctor doctor)
		{
			isDataCorrect(doctor);

			ctx.Doctors.Add(doctor);
		}

		public void DeleteDoctor(dbContext ctx, int id)
		{
			var model = ctx.Doctors.Find(id);
			if (model == null)
				throw new ModelNotExistException();

			ctx.Doctors.Remove(model);
		}

		public void EditDoctor(dbContext ctx, Doctor doctor)
		{
			isDataCorrect(doctor);

			var model = ctx.Doctors.Find(doctor.Id);
			if (model == null)
				throw new ModelNotExistException();

			model.Name = doctor.Name;
			model.Surname = doctor.Surname;
			model.HospitalId = doctor.HospitalId;
			model.DoctorType = doctor.DoctorType;
			
		}

		public List<Doctor> GetAllDoctors(dbContext ctx)
		{
			return ctx.Doctors.ToList();
		}

		public Doctor GetDoctor(dbContext ctx, int id)
		{
			return ctx.Doctors.Find(id);
		}

		public Doctor GetDoctorByUserId(dbContext ctx, string id)
		{
			return ctx.Doctors.Where(x => x.UserId == id).FirstOrDefault();
		}
		public List<Doctor> GetDoctorsByHospitalIdAndType(dbContext ctx, int hospitalId, int doctorType)
		{
			return ctx.Doctors.Where(x => x.HospitalId == hospitalId && x.DoctorType == doctorType).ToList();
		}

		public bool isDataCorrect(Doctor doctor)
		{
			if (doctor?.Name != null && doctor?.Surname != null && doctor?.HospitalId != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
