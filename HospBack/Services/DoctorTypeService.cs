using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;

namespace HospBack.Services
{
	public interface IDoctorTypeService
	{
		List<DoctorType> GetAllDoctorTypes(dbContext ctx);
		DoctorType GetDoctorType(dbContext ctx, int id);
		void CreateDoctorType(dbContext ctx, DoctorType doctorType);
		void EditDoctorType(dbContext ctx, DoctorType doctorType);
		void DeleteDoctorType(dbContext ctx, int id);
	}
	public class DoctorTypeService : IDoctorTypeService
	{
		public void CreateDoctorType(dbContext ctx, DoctorType doctorType)
		{
			isDataCorrect(doctorType);

			var model = ctx.DoctorTypes.Where(x => x.Type == doctorType.Type).FirstOrDefault();
			if (model != null)
				throw new ModelAlreadyExistException();

			ctx.DoctorTypes.Add(doctorType);
		}

		public void DeleteDoctorType(dbContext ctx, int id)
		{
			var model = ctx.DoctorTypes.Find(id);
			if (model == null)
				throw new ModelNotExistException();

			ctx.DoctorTypes.Remove(model);
		}

		public void EditDoctorType(dbContext ctx, DoctorType doctorType)
		{
			isDataCorrect(doctorType);

			var model = ctx.DoctorTypes.Find(doctorType.Id);
			if (model == null)
				throw new ModelNotExistException();

			model.Type = doctorType.Type;
		}

		public List<DoctorType> GetAllDoctorTypes(dbContext ctx)
		{
			return ctx.DoctorTypes.ToList();
		}

		public DoctorType GetDoctorType(dbContext ctx, int id)
		{
			return ctx.DoctorTypes.Find(id);
		}


		public bool isDataCorrect(DoctorType doctorType)
		{
			if (doctorType?.Type != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
