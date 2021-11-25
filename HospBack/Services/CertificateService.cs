using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.DB;
using HospBack.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HospBack.Services
{
	public interface ICertificateService
	{
		List<Certificate> GetAllCertificates(dbContext ctx);
		Certificate GetCertificate(dbContext ctx, int id);
		Certificate GetCertificateByVisitId(dbContext ctx, int visitid);
		void CreateOrEditCertificate(dbContext ctx, Certificate certificate);
	}

	public class CertificateService : ICertificateService
	{
		public void CreateOrEditCertificate(dbContext ctx, Certificate certificate)
		{
			isDataCorrect(certificate);

			var model = ctx.Certificates.Where(x => x.VisitId == certificate.VisitId).FirstOrDefault();
			if (model != null)
			{
				model.Description = certificate.Description;
			}
			else
			{
				ctx.Certificates.Add(certificate);
			}			
		}


		public List<Certificate> GetAllCertificates(dbContext ctx)
		{
			return ctx.Certificates.ToList();
		}

		public Certificate GetCertificate(dbContext ctx, int id)
		{
			return ctx.Certificates.Find(id);
		}

		public Certificate GetCertificateByVisitId(dbContext ctx, int visitid)
		{
			return ctx.Certificates.Where(x => x.VisitId == visitid)
				.Include(x => x.Visit)
				.FirstOrDefault();
		}


		public bool isDataCorrect(Certificate certificate)
		{
			if (certificate != null)
				return true;
			throw new IncorrectDataException();
		}
	}
}
