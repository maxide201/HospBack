using HospBack.DB;
using HospBack.DB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public IConfiguration Configuration { get; }
        public string AuthorizedUserId => User.GetUserId();

        public ControllerBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected dbContext CreateDataContext()
        {
            return new dbContext(Configuration.GetConnectionString("Database"));
        }
    }
}
