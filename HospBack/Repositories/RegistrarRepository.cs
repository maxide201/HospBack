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
	public interface IRegistrarRepository
	{
		List<Registrar> GetRegistrars(dbContext ctx);
		void CreateRegistrar(dbContext ctx, Registrar registrar);
		void UpdateRegistrar(dbContext ctx, Registrar registrar);
		Registrar GetRegistrarById(dbContext ctx, int id);
		Registrar GetRegistrarByUserId(dbContext ctx, string userId);
	}
	public class RegistrarRepository : IRegistrarRepository
	{
		public List<Registrar> GetRegistrars(dbContext ctx) =>
			ctx.Registrars.ToList();

		public void CreateRegistrar(dbContext ctx, Registrar registrar) =>
			ctx.Registrars.Add(registrar);

		public void UpdateRegistrar(dbContext ctx, Registrar registrar) =>
			ctx.Registrars.Update(registrar);

		public Registrar GetRegistrarById(dbContext ctx, int id) =>
			ctx.Registrars.Include(x => x.User)
			.Where(x => x.Id == id)
			.FirstOrDefault();

		public Registrar GetRegistrarByUserId(dbContext ctx, string userId) =>
			ctx.Registrars.Where(x => x.UserId == userId).FirstOrDefault();

	}
}