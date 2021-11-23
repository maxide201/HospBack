using HospBack.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Repositories
{
	public abstract class BaseRepository
	{
		//IConfiguration _configuration;
		//IServiceProvider _serviceProvider;
		//public BaseRepository(IConfiguration configuration, IServiceProvider serviceProvider)
		//{
		//	_configuration = configuration;
		//	_serviceProvider = serviceProvider;
		//}
		//protected dbContext GetDataContext()
		//{
		//	var contextFactory = _serviceProvider.GetService<IContextFactory>();
		//	return contextFactory.Get(_configuration.GetConnectionString("Database"));
		//}
	}
}
