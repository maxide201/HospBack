using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using HospBack.Repositories;

namespace HospBack.Services
{
	public interface IRegistrarService
	{
		List<Registrar> GetAllRegistrars(dbContext ctx);
		Registrar GetRegistrar(dbContext ctx, int id);
		void CreateRegistrar(dbContext ctx, Registrar registrar);
		void EditRegistrar(dbContext ctx, Registrar registrar);
		void DeleteRegistrar(dbContext ctx, int id);
	}
	public class RegistrarService : IRegistrarService
	{
		public void CreateRegistrar(dbContext ctx, Registrar registrar)
		{
			isDataCorrect(registrar);

			var model = ctx.Registrars.Where(x => x.UserId == registrar.UserId).FirstOrDefault();
			if (model != null)
				throw new ModelAlreadyExistException();

			ctx.Registrars.Add(registrar);
		}

		public void DeleteRegistrar(dbContext ctx, int id)
		{
			var model = ctx.Registrars.Find(id);
			if (model == null)
				throw new ModelNotExistException();

			ctx.Registrars.Remove(model);
		}

		public void EditRegistrar(dbContext ctx, Registrar registrar)
		{
			isDataCorrect(registrar);

			var model = ctx.Registrars.Find(registrar.Id);
			if (model == null)
				throw new ModelNotExistException();

			model.Name = registrar.Name;
			model.Surname = registrar.Surname;
		}

		public List<Registrar> GetAllRegistrars(dbContext ctx)
		{
			return ctx.Registrars.ToList();
		}

		public Registrar GetRegistrar(dbContext ctx, int id)
		{
			return ctx.Registrars.Find(id);
		}

		public bool isDataCorrect(Registrar registrar)
		{
			if (registrar?.Name != null && registrar?.Surname != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
