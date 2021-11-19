using HospBack.DB.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospBack.Services;
using Microsoft.Extensions.DependencyInjection;
using HospBack.DB;

namespace HospBack.Controllers
{
    [Route("[controller]")]
    public class HospitalController : ControllerBase
    {
        IHospitalService _hospitalService;
        public HospitalController(IConfiguration configuration, IHospitalService hospitalService) : base(configuration) 
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        [Route("hospitals")]
        public IActionResult GetAllHospitals()
        {
            using (var ctx = CreateDataContext())
			{
                var hospitals = _hospitalService.GetAllHospitals(ctx);
                return View(hospitals);
            }
        }
    }
}
