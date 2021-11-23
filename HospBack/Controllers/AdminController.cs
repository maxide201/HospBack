using HospBack.DB;
using HospBack.Services;
using HospBack.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HospBack.Controllers
{
	[Route("[controller]")]
    [Authorize(Roles = "admin")]
	public class AdminController : ControllerBase
	{
        private readonly UserManager<User> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IDoctorTypeService _doctorTypeService;
        private readonly IHospitalService _hospitalService;
        private readonly IRegistrarService _registrarService;
        private readonly IPatientService _patientService;

        public AdminController(IConfiguration configuration, UserManager<User> userManager,
                               IDoctorService doctorService, IHospitalService hospitalService,
                               IDoctorTypeService doctorTypeService, IPatientService patientService,
                               IRegistrarService registrarService) : base(configuration)
        {
            _userManager = userManager;
            _doctorService = doctorService;
            _doctorTypeService = doctorTypeService;
            _hospitalService = hospitalService;
            _registrarService = registrarService;
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		#region Registy
		[HttpGet]
        [Route("registry")]
        public IActionResult Registry()
        {
            using(var ctx = CreateDataContext())
			{
                var registrars = _registrarService.GetAllRegistrars(ctx);
                return View(registrars);
            }
        }

        [HttpGet]
        [Route("registry/registrar")]
        public IActionResult Registrar(int id)
        {
            using (var ctx = CreateDataContext())
            {
                var registrar = _registrarService.GetRegistrar(ctx, id);
                if (registrar == null)
                    return NotFound();

                var user = ctx.Users.Where(x => x.Id == registrar.UserId).FirstOrDefault();
                var viewModel = registrar.ToViewModel(user.Email);

                return View(viewModel);
            }
        }

        [HttpPost]
        [Route("registry/create")]
        public IActionResult CreateRegsitrar([FromForm] RegistrarViewModel registrar)
        {

            using (var ctx = CreateDataContext())
            {
                try
                {
                    var isExist = ctx.Users.Any(x => x.Email == registrar.Email);
                    if (isExist)
                        throw new ModelAlreadyExistException();

                    var user = new User();
                    user.Email = registrar.Email;
                    var password = new PasswordHasher<User>();
                    user.PasswordHash = password.HashPassword(user, "12345");
                    ctx.Users.Add(user);
                    _userManager.AddToRoleAsync(user, "registrar");

                    var model = registrar.ToDataModel();
                    model.UserId = user.Id;
                    _registrarService.CreateRegistrar(ctx, model);
                    TempData["status"] = "ok";
                }
                catch(IncorrectDataException)
				{
                    TempData["status"] = "incorrect";
                }
                catch(ModelAlreadyExistException)
				{
                    TempData["status"] = "exist";
                }
                catch(Exception)
				{
                    TempData["status"] = "unknow";
                }
                ctx.SaveChanges();

                return Redirect("/admin/registry");
            }
        }

        [HttpPost]
        [Route("registry/update")]
        public IActionResult UpdateRegsitrar([FromForm] RegistrarViewModel registrar)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = registrar.ToDataModel();
                    _registrarService.EditRegistrar(ctx, model);
                    TempData["status"] = "ok";
                }
                catch (IncorrectDataException)
                {
                    TempData["status"] = "incorrect";
                }
                catch (ModelNotExistException)
                {
                    TempData["status"] = "notexist";
                }
                catch (Exception e)
                {
                    TempData["status"] = "unknow";
                }
                ctx.SaveChanges();

                return Redirect("/admin/registry/registrar?id="+registrar.Id);
            }
        }

        [HttpPost]
        [Route("registry/delete")]
        public IActionResult DeleteRegsitrar(int id)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _registrarService.DeleteRegistrar(ctx, id);
                }
                catch (Exception)
                {
                }
                ctx.SaveChanges();

                return Redirect("/admin/registry");
            }
        }
		#endregion

		#region Hospital
		[HttpGet]
        [Route("hospitals")]
        public IActionResult Hospitals()
        {
            using (var ctx = CreateDataContext())
            {
                var hospitals = ctx.Hospitals.ToList();
                return View(hospitals);
            }
        }

        [HttpGet]
        [Route("hospitals/hospital")]
        public IActionResult Hospital(int id)
        {
            using (var ctx = CreateDataContext())
            {
                var registrar = _registrarService.GetRegistrar(ctx, id);
                if (registrar == null)
                    return NotFound();

                var user = ctx.Users.Where(x => x.Id == registrar.UserId).FirstOrDefault();
                var viewModel = registrar.ToViewModel(user.Email);

                return View(viewModel);
            }
        }

        [HttpPost]
        [Route("hospitals/create")]
        public IActionResult CreateHospital([FromForm] RegistrarViewModel registrar)
        {

            using (var ctx = CreateDataContext())
            {
                try
                {
                    var isExist = ctx.Users.Any(x => x.Email == registrar.Email);
                    if (isExist)
                        throw new ModelAlreadyExistException();

                    var user = new User();
                    user.Email = registrar.Email;
                    var password = new PasswordHasher<User>();
                    user.PasswordHash = password.HashPassword(user, "12345");
                    ctx.Users.Add(user);
                    _userManager.AddToRoleAsync(user, "registrar");

                    var model = registrar.ToDataModel();
                    model.UserId = user.Id;
                    _registrarService.CreateRegistrar(ctx, model);
                    TempData["status"] = "ok";
                }
                catch (IncorrectDataException)
                {
                    TempData["status"] = "incorrect";
                }
                catch (ModelAlreadyExistException)
                {
                    TempData["status"] = "exist";
                }
                catch (Exception)
                {
                    TempData["status"] = "unknow";
                }
                ctx.SaveChanges();

                return Redirect("/admin/registry");
            }
        }

        [HttpPost]
        [Route("hospitals/update")]
        public IActionResult UpdateHospital([FromForm] RegistrarViewModel registrar)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = registrar.ToDataModel();
                    _registrarService.EditRegistrar(ctx, model);
                    TempData["status"] = "ok";
                }
                catch (IncorrectDataException)
                {
                    TempData["status"] = "incorrect";
                }
                catch (ModelNotExistException)
                {
                    TempData["status"] = "notexist";
                }
                catch (Exception)
                {
                    TempData["status"] = "unknow";
                }
                ctx.SaveChanges();

                return Redirect("/admin/registry/registrar?id=" + registrar.Id);
            }
        }

        [HttpPost]
        [Route("hospitals/delete")]
        public IActionResult DeleteHospital(int id)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _registrarService.DeleteRegistrar(ctx, id);
                }
                catch (Exception)
                {
                }
                ctx.SaveChanges();

                return Redirect("/admin/registry");
            }
        }
		#endregion


		[HttpPost]
        [Route("hospitals/create")]
        public IActionResult CreateHospital([FromForm] HospitalViewModel hospital)
        {
            //if (!isDataCorrect(hospiatl))
                //return Redirect("/");

            using (var ctx = CreateDataContext())
            {
                var isExist = ctx.Users.Any(x => x.Email == hospital.Name);
                if (isExist)
                    return Redirect("/admin/hospitals");

                var model = hospital.ToDataModel();
                ctx.Hospitals.Add(model);
                ctx.SaveChanges();

                return Redirect("../");
            }
        }
       
    }
}
