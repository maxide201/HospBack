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
    public class VisitController : ControllerBase
    {
        IDoctorService _doctorService;
        IScheduleService _scheduleService;
        IPatientService _patientService;
        IVisitService _visitService;
        IDoctorTypeService _doctorTypeService;
        IHospitalService _hospitalService;
        public VisitController(IConfiguration configuration, IDoctorService doctorService,
                                  IScheduleService scheduleService, IPatientService patientService,
                                  IVisitService visitService, IDoctorTypeService doctorTypeService,
                                  IHospitalService hospitalService) : base(configuration)
        {
            _doctorService = doctorService;
            _scheduleService = scheduleService;
            _patientService = patientService;
            _visitService = visitService;
            _doctorTypeService = doctorTypeService;
            _hospitalService = hospitalService;
        }

        [HttpGet]
        [Route("manage")]
        public IActionResult PatientsPage()
        {
            using (var ctx = CreateDataContext())
            {
                var patients = _patientService.GetAllPatients(ctx);
                return View(patients);
            }
        }

        [HttpGet]
        [Route("manage/{id}")]
        public IActionResult PatientPage([FromRoute] int id)
        {
            using (var ctx = CreateDataContext())
            {
                var patient = _patientService.GetPatient(ctx, id);
                var visits = _visitService.GetVisitsByPatientId(ctx, id);

                var viewModel = new PatientVisitsPage();
                viewModel.Patient = patient.ToViewModel();
                viewModel.Visits = visits;

                return View(viewModel);
            }
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult VisitPage([FromRoute] int id)
        {
            using (var ctx = CreateDataContext())
            {
                var visit = _visitService.GetVisit(ctx, id);
                return View(visit);
            }
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteVisit([FromForm] int id)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _visitService.DeleteVisit(ctx, id);
                }
                catch(Exception) { }
                ctx.SaveChanges();
                return Redirect("../");
            }
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreatePage([FromQuery] int patientId, [FromQuery] int doctorType,
                                         [FromQuery] int hospitalId, [FromQuery] int doctorId,
                                         [FromQuery] int scheduleId)
        {
            using (var ctx = CreateDataContext())
            {
                if (patientId == 0)
                {
                    return Redirect("visit/manage");
                }
                if (doctorType == 0)
				{
                    ViewBag.Status = "type";
                    ViewBag.Elements = _doctorTypeService.GetAllDoctorTypes(ctx);
                    return View();
                }
                if(hospitalId == 0)
				{
                    ViewBag.Status = "hospital";
                    ViewBag.Elements = _hospitalService.GetHospitalByDoctorType(ctx, doctorType);
                    return View();
                }
                if (doctorId == 0)
                {
                    ViewBag.Status = "doctor";
                    ViewBag.Elements = _doctorService.GetDoctorsByHospitalIdAndType(ctx, hospitalId, doctorType);
                    return View();
                }
                if (scheduleId == 0)
                {
                    ViewBag.Status = "schedule";
                    ViewBag.Elements = _scheduleService.GetScheduleByDoctorId(ctx, doctorId).FindAll(x => x.Day >= DateTime.Now.Date && x.Day <= DateTime.Now.Add(new TimeSpan(7, 0, 0, 0)).Date);
                    return View();
                }
                ViewBag.Status = "time";

                var schedule = _scheduleService.GetSchedule(ctx, scheduleId);
                var times = new List<DateTime>();
                for(var start = schedule.StartTime; start <= schedule.EndTime; start += new TimeSpan(0, schedule.VisitTime, 0))
				{
                    var dateTime = schedule.Day.Add(new TimeSpan(start.Value.Hour, start.Value.Minute, 0));
                    if (_visitService.GetVisitByTimeAndDoctorId(ctx, dateTime, doctorId) == null)
                        times.Add(dateTime);
				}
                ViewBag.Times = times;
                return View();
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateVisit(VisitViewModel visit)
		{
            using(var ctx = CreateDataContext())
			{
                try
                {
                    var model = visit.ToDataModel();
                    _visitService.CreateVisit(ctx, model);
                }
                catch(ModelAlreadyExistException)
				{
                    return BadRequest();
				}
                catch (Exception)
                {
                    return BadRequest();
                }

                ctx.SaveChanges();
                return Redirect("/registry");
            }
            
		}


    }
}