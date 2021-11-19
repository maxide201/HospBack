using HospBack.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Repositories
{
	public interface IDoctorRepository
	{
		List<Doctor> GetDoctors(dbContext ctx);
		void CreateDoctor(dbContext ctx, Doctor doctor);
		void UpdateDoctor(dbContext ctx, Doctor doctor);
		Doctor GetDoctorById(dbContext ctx, int id);
		Doctor GetDoctorByUserId(dbContext ctx, string userId);
	}
	public class DoctorRepository : IDoctorRepository
	{
		public List<Doctor> GetDoctors(dbContext ctx) =>
			ctx.Doctors.ToList();

		public void CreateDoctor(dbContext ctx, Doctor doctor) =>
			ctx.Doctors.Add(doctor);

		public void UpdateDoctor(dbContext ctx, Doctor doctor) =>
			ctx.Doctors.Update(doctor);

		public Doctor GetDoctorById(dbContext ctx, int id) =>
			ctx.Doctors.Include(x => x.User)
					   .Where(x => x.Id == id)
					   .FirstOrDefault();

		public Doctor GetDoctorByUserId(dbContext ctx, string userId) =>
			ctx.Doctors.Where(x => x.UserId == userId).FirstOrDefault();

	}
}