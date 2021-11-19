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
		void CreateRegistrar(dbContext ctx, Registrar registrar);
		void EditRegistrar(dbContext ctx, Registrar registrar);
		void DeleteRegistrar(dbContext ctx, int id);
	}
	public class RegistrarService : IRegistrarService
	{
		IRegistrarRepository _registrarRepository;
		public RegistrarService(IRegistrarRepository registrarRepository)
		{
			_registrarRepository = registrarRepository;
		}

		public void CreateRegistrar(dbContext ctx, Registrar registrar)
		{
			isDataCorrect(registrar);

			var model = _registrarRepository.GetRegistrarByUserId(ctx, registrar.UserId);
			if (model != null)
				throw new ModelAlreadyExistException();

			_registrarRepository.CreateRegistrar(ctx, registrar);
		}

		public void DeleteRegistrar(dbContext ctx, int id)
		{
			var model = _registrarRepository.GetRegistrarById(ctx, id);
			if (model == null)
				throw new ModelNotExistException();

			//_registrarRepository.Delete(ctx, id);
		}

		public void EditRegistrar(dbContext ctx, Registrar registrar)
		{
			isDataCorrect(registrar);

			var model = _registrarRepository.GetRegistrarById(ctx, registrar.Id);
			if (model == null)
				throw new ModelNotExistException();

			_registrarRepository.UpdateRegistrar(ctx, registrar);
		}

		public List<Registrar> GetAllRegistrars(dbContext ctx)
		{
			return _registrarRepository.GetRegistrars(ctx);
		}


		public bool isDataCorrect(Registrar registrar)
		{
			if (registrar?.Name != null && registrar?.Surname != null && registrar?.UserId != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
