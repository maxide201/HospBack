using HospBack.DB.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.ViewModels;
using HospBack.Services;
using HospBack.DB;
using HospBack.Exceptions;

namespace HospBack.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin,registrar")]
    public class RegistryController : ControllerBase
    {
        IRegistrarService _registrarService;
        IPatientService _patientService;
        public RegistryController(IConfiguration configuration, IRegistrarService registrarService,
                                  IPatientService patientService) : base(configuration)
        {
            _registrarService = registrarService;
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("patient/create")]
        public IActionResult CreatePatient()
		{
            return View();
		}

        [HttpPost]
        [Route("patient/create")]
        public IActionResult CreatePatient(PatientViewModel patient)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = patient.ToDataModel();
                    _patientService.CreatePatient(ctx, model);
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

                return Redirect("/registry/patient/create");
            }
        }
    }
}