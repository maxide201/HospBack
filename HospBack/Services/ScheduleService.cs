using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HospBack.Services
{
	public interface IScheduleService
	{
		List<Schedule> GetAllSchedules(dbContext ctx);
		Schedule GetSchedule(dbContext ctx, int id);
		void CreateSchedule(dbContext ctx, Schedule schedule);
		void EditSchedule(dbContext ctx, Schedule schedule);
		void DeleteSchedule(dbContext ctx, int id);
		List<Schedule> GetScheduleByDoctorId(dbContext ctx, int id);
	}
	public class ScheduleService : IScheduleService
	{
		public void CreateSchedule(dbContext ctx, Schedule schedule)
		{
			isDataCorrect(schedule);

			var model = ctx.Schedules.Where(x => x.DoctorId == schedule.DoctorId && x.Day == schedule.Day).FirstOrDefault();
			if (model != null)
				throw new ModelAlreadyExistException();

			ctx.Schedules.Add(schedule);
		}

		public void DeleteSchedule(dbContext ctx, int id)
		{
			var model = ctx.Schedules.Find(id);
			if (model == null)
				throw new ModelNotExistException();

			var isAnyVisits = ctx.Visits.Any(x => x.VisitDate.Date == model.Day.Date);
			if (isAnyVisits)
				throw new VisitExistException();

			ctx.Schedules.Remove(model);
		}

		public void EditSchedule(dbContext ctx, Schedule schedule)
		{
			isDataCorrect(schedule);

			var model = ctx.Schedules.Find(schedule.Id);
			if (model == null)
				throw new ModelNotExistException();

			var isAnyVisits =  ctx.Visits.Any(x => x.VisitDate.Date == schedule.Day.Date);
			if (isAnyVisits)
				throw new VisitExistException();

			model.StartTime = schedule.StartTime;
			model.EndTime = schedule.EndTime;
			model.VisitTime = schedule.VisitTime;
		}

		public List<Schedule> GetAllSchedules(dbContext ctx)
		{
			return ctx.Schedules.ToList();
		}

		public Schedule GetSchedule(dbContext ctx, int id)
		{
			return ctx.Schedules.Find(id);
		}

		public List<Schedule> GetScheduleByDoctorId(dbContext ctx, int id)
		{
			return ctx.Schedules.Where(x => x.DoctorId == id)
				.OrderBy(x => x.Day)
				.ToList();
		}


		public bool isDataCorrect(Schedule schedule)
		{
			if((schedule?.StartTime >= schedule?.EndTime) || 
				(schedule.EndTime - schedule.StartTime) < new TimeSpan(0, schedule.VisitTime,0) || 
				schedule.VisitTime <= 0)
				throw new IncorrectDataException();

			var diff = (schedule.EndTime - schedule.StartTime).Value.TotalMinutes;
			if(diff%schedule.VisitTime != 0)
				throw new IncorrectDataException();

			return true;
		}
	}
}
