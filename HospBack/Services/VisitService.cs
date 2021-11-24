using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using HospBack.DB;
using HospBack.Exceptions;
using HospBack.Repositories;

namespace HospBack.Services
{
	public interface IVisitService
	{
		List<Visit> GetAllVisits(dbContext ctx);
		Visit GetVisit(dbContext ctx, int id);
		void CreateVisit(dbContext ctx, Visit visit);
		void DeleteVisit(dbContext ctx, int id);
		List<Visit> GetVisitsByPatientId(dbContext ctx, int id);
		List<Visit> GetVisitsByVisitDay(dbContext ctx, DateTime day);
		public Visit GetVisitByTimeAndDoctorId(dbContext ctx, DateTime time, int doctorId);
	}
	public class VisitService : IVisitService
	{
		public void CreateVisit(dbContext ctx, Visit visit)
		{
			isDataCorrect(visit);

			var model = ctx.Visits.Where(x => x.VisitDate == visit.VisitDate).FirstOrDefault();
			if (model != null)
				throw new ModelAlreadyExistException();

			visit.Status = "created";

			ctx.Visits.Add(visit);
		}

		public void DeleteVisit(dbContext ctx, int id)
		{
			var model = ctx.Visits.Find(id);
			if (model == null)
				throw new ModelNotExistException();
			if (model.VisitDate < DateTime.Now)
				throw new Exception();

			ctx.Visits.Remove(model);
		}

		public List<Visit> GetAllVisits(dbContext ctx)
		{
			return ctx.Visits.ToList();
		}

		public List<Visit> GetVisitsByPatientId(dbContext ctx, int id)
		{
			return ctx.Visits.Where(x => x.PatientId == id).ToList();
		}
		public List<Visit> GetVisitsByVisitDay(dbContext ctx, DateTime day)
		{
			return ctx.Visits.Where(x => x.VisitDate.Day == day.Day).ToList();
		}

		public Visit GetVisitByTimeAndDoctorId(dbContext ctx, DateTime time, int doctorId)
		{
			return ctx.Visits.Where(x => x.DoctorId == doctorId && x.VisitDate == time).FirstOrDefault();
		}

		public Visit GetVisit(dbContext ctx, int id)
		{
			return ctx.Visits.Where(x => x.Id == id)
				.Include(x => x.Doctor).ThenInclude(x => x.DoctorTypeNavigation)
				.FirstOrDefault();
		}


		public bool isDataCorrect(Visit visit)
		{
			if (visit != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
