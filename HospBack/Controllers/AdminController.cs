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
using Microsoft.AspNetCore.Mvc.Rendering;

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
                var hospital = _hospitalService.GetHospital(ctx, id);
                if (hospital == null)
                    return NotFound();

                var viewModel = hospital.ToViewModel();
                return View(viewModel);
            }
        }

        [HttpPost]
        [Route("hospitals/create")]
        public IActionResult CreateHospital([FromForm] HospitalViewModel hospital)
        {

            using (var ctx = CreateDataContext())
            {
                try
                {
                    var isExist = ctx.Hospitals.Any(x => x.Name == hospital.Name);
                    if (isExist)
                        throw new ModelAlreadyExistException();

                    var model = hospital.ToDataModel();
                    _hospitalService.CreateHospital(ctx, model);
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

                return Redirect("/admin/hospitals");
            }
        }

        [HttpPost]
        [Route("hospitals/update")]
        public IActionResult UpdateHospital([FromForm] HospitalViewModel hospital)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = hospital.ToDataModel();
                    _hospitalService.EditHospital(ctx, model);
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

                return Redirect("/admin/hospitals/hospital?id=" + hospital.Id);
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
                    _hospitalService.DeleteHospital(ctx, id);
                }
                catch (Exception)
                {
                }
                ctx.SaveChanges();

                return Redirect("/admin/hospitals");
            }
        }
        #endregion

        #region DoctorType
        [HttpGet]
        [Route("doctorTypes")]
        public IActionResult DoctorTypes()
        {
            using (var ctx = CreateDataContext())
            {
                var doctorTypes = ctx.DoctorTypes.ToList();
                return View(doctorTypes);
            }
        }

        [HttpGet]
        [Route("doctorTypes/doctorType")]
        public IActionResult DoctorType(int id)
        {
            using (var ctx = CreateDataContext())
            {
                var doctorType = _doctorTypeService.GetDoctorType(ctx, id);
                if (doctorType == null)
                    return NotFound();

                var viewModel = doctorType.ToViewModel();
                return View(viewModel);
            }
        }

        [HttpPost]
        [Route("doctorTypes/create")]
        public IActionResult CreateDoctorType([FromForm] DoctorTypeViewModel doctorType)
        {

            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = doctorType.ToDataModel();
                    _doctorTypeService.CreateDoctorType(ctx, model);
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

                return Redirect("/admin/doctorTypes");
            }
        }

        [HttpPost]
        [Route("doctorTypes/update")]
        public IActionResult UpdateDoctorType([FromForm] DoctorTypeViewModel doctorType)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = doctorType.ToDataModel();
                    _doctorTypeService.EditDoctorType(ctx, model);
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

                return Redirect("/admin/doctorTypes/doctorType?id=" + doctorType.Id);
            }
        }

        [HttpPost]
        [Route("doctorTypes/delete")]
        public IActionResult DeleteDoctorType(int id)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _doctorTypeService.DeleteDoctorType(ctx, id);
                }
                catch (Exception)
                {
                }
                ctx.SaveChanges();

                return Redirect("/admin/doctorTypes");
            }
        }
        #endregion

        #region Doctor
        [HttpGet]
        [Route("doctors")]
        public IActionResult Doctors()
        {
            using (var ctx = CreateDataContext())
            {
                var doctors = _doctorService.GetAllDoctors(ctx);
                var types = _doctorTypeService.GetAllDoctorTypes(ctx);
                var hospitals = _hospitalService.GetAllHospitals(ctx);

                var doctorTypes = new List<SelectListItem>();
                foreach (var type in types)
                    doctorTypes.Add(new SelectListItem(type.Type, type.Id.ToString()));

                var hopitalIds = new List<SelectListItem>();
                foreach (var hospital in hospitals)
                    hopitalIds.Add(new SelectListItem(hospital.Name, hospital.Id.ToString()));

                ViewBag.DoctorTypes = doctorTypes;
                ViewBag.Hospitals = hopitalIds;
                return View(doctors);
            }
        }

        [HttpGet]
        [Route("doctors/doctor")]
        public IActionResult Doctor(int id)
        {
            using (var ctx = CreateDataContext())
            {
                var doctor = _doctorService.GetDoctor(ctx, id);
                if (doctor == null)
                    return NotFound();

                var user = ctx.Users.Where(x => x.Id == doctor.UserId).FirstOrDefault();
                var viewModel = doctor.ToViewModel(user.Email);

                var types = _doctorTypeService.GetAllDoctorTypes(ctx);
                var hospitals = _hospitalService.GetAllHospitals(ctx);

                var doctorTypes = new List<SelectListItem>();
                foreach (var type in types)
                    doctorTypes.Add(new SelectListItem(type.Type, type.Id.ToString()));

                var hopitalIds = new List<SelectListItem>();
                foreach (var hospital in hospitals)
                    hopitalIds.Add(new SelectListItem(hospital.Name, hospital.Id.ToString()));

                ViewBag.DoctorTypes = doctorTypes;
                ViewBag.Hospitals = hopitalIds;

                return View(viewModel);
            }
        }

        [HttpPost]
        [Route("doctors/create")]
        public IActionResult CreateDoctor([FromForm] DoctorViewModel doctor)
        {

            using (var ctx = CreateDataContext())
            {
                try
                {
                    var isExist = ctx.Users.Any(x => x.Email == doctor.Email);
                    if (isExist)
                        throw new ModelAlreadyExistException();

                    var user = new User();
                    user.Email = doctor.Email;
                    var password = new PasswordHasher<User>();
                    user.PasswordHash = password.HashPassword(user, "12345");
                    ctx.Users.Add(user);
                    _userManager.AddToRoleAsync(user, "doctor");

                    var model = doctor.ToDataModel();
                    model.UserId = user.Id;
                    _doctorService.CreateDoctor(ctx, model);
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

                return Redirect("/admin/doctors");
            }
        }

        [HttpPost]
        [Route("doctors/update")]
        public IActionResult UpdateDoctor([FromForm] DoctorViewModel doctor)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = doctor.ToDataModel();
                    _doctorService.EditDoctor(ctx, model);
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

                return Redirect("/admin/doctors/doctor?id=" + doctor.Id);
            }
        }

        [HttpPost]
        [Route("doctors/delete")]
        public IActionResult DeleteDoctor(int id)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _doctorService.DeleteDoctor(ctx, id);
                }
                catch (Exception)
                {
                }
                ctx.SaveChanges();

                return Redirect("/admin/doctors");
            }
        }
        #endregion
    }
}
