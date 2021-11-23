using HospBack.DB.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HospBack.ViewModels;
using HospBack.DB;

namespace HospBack.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "registrar")]
    public class RegistryController : ControllerBase
    {
        public RegistryController(IConfiguration configuration) : base(configuration) { }

   //     [HttpPost]
   //     [Route("Create")]
   //     public IActionResult CreatePatient([FromForm]PatientViewModel patient)
   //     {
   //         if (!isDataCorrect(patient))
   //             return View("CreatePage");

   //         using(var ctx = CreateDataContext())
			//{
   //             var model = patient.ToDataModel();

   //             ctx.Patients.Add(model);
   //             ctx.SaveChanges();

   //             return Redirect("CreatePage");
   //         }
   //     }

        [HttpGet]
        public IActionResult Index()
		{
            return View();
		}


        private bool isDataCorrect(PatientViewModel patient)
        {
            if (patient?.PhoneNumber != null && patient?.Name != null && patient?.Surname != null)
                return true;
            return false;
        }
    }
}
