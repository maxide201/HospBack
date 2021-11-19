using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using HospBack.DB;
using HospBack.Services;
using HospBack.Repositories;

namespace HospBack
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

        public void ConfigureServices(IServiceCollection services)
        {
            // база данных
            services.AddDbContext<dbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Database"), b => b.MigrationsAssembly("HospBack")));

            // работа контроллеров
            services
                .AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            // паттерн mvc
            services.AddMvc();

            //  Identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequiredLength = 7;
            })
                .AddEntityFrameworkStores<dbContext>();

            // репозитории моделей
            AddModelRepositories(services);

            // сервисы моделей
            AddModelServices(services);

        }

        public void AddModelServices(IServiceCollection services )
		{
            services.AddSingleton<IHospitalService, HospitalService>();
            services.AddSingleton<IDoctorService, DoctorService>();
            services.AddSingleton<IDoctorTypeService, DoctorTypeService>();
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IRegistrarService, RegistrarService>();
        }
        public void AddModelRepositories(IServiceCollection services)
        {
            services.AddSingleton<IHospitalRepository, HospitalRepository>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();
            services.AddSingleton<IDoctorTypeRepository, DoctorTypeRepository>();
            services.AddSingleton<IPatientRepository, PatientRepository>();
            services.AddSingleton<IRegistrarRepository, RegistrarRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
