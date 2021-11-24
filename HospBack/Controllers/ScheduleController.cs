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
    public class ScheduleController : ControllerBase
    {
        IDoctorService _doctorService;
        IScheduleService _scheduleService;
        public ScheduleController(IConfiguration configuration, IDoctorService doctorService,
                                  IScheduleService scheduleService) : base(configuration)
        {
            _doctorService = doctorService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult EditPage()
        {
            using (var ctx = CreateDataContext())
            {
                var doctors = _doctorService.GetAllDoctors(ctx);
                return View(doctors);
            }
        }


        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult EditSchedulePage([FromRoute]int id)
        {
            using (var ctx = CreateDataContext())
            {
                var schedule = _scheduleService.GetScheduleByDoctorId(ctx, id);
                var doctor = _doctorService.GetDoctor(ctx, id);

                var scheduleView = new AddSchedulePageModelView();
                scheduleView.Schedules = schedule;
                scheduleView.Doctor = doctor.ToViewModel();

                return View(scheduleView);
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateSchedule(ScheduleViewModel schedule)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    var model = schedule.ToDataModel();
                    _scheduleService.CreateSchedule(ctx, model);
                }
                catch(IncorrectDataException)
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
                return Redirect("/schedule/edit/" + schedule.DoctorId);
            }
        }

        [HttpGet]
        [Route("update/{id}")]
        public IActionResult UpdateSchedulePage([FromRoute] int id)
        {
            using (var ctx = CreateDataContext())
            {
                var schedule = _scheduleService.GetSchedule(ctx, id);
                return View(schedule);
            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateSchedule([FromForm] Schedule schedule)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _scheduleService.EditSchedule(ctx, schedule);
                }
                catch (IncorrectDataException)
                {
                    TempData["status"] = "incorrect";
                }
                catch (ModelNotExistException)
                {
                    TempData["status"] = "notexist";
                }
                catch(VisitExistException)
				{
                    TempData["status"] = "visitexist";
                }
                catch (Exception)
                {
                    TempData["status"] = "unknow";
                }
                ctx.SaveChanges();
                return Redirect("/schedule/update/" + schedule.Id);
            }
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteSchedule([FromForm]int id, [FromForm] int DoctorId)
        {
            using (var ctx = CreateDataContext())
            {
                try
                {
                    _scheduleService.DeleteSchedule(ctx, id);
                }
                catch (VisitExistException)
                {
                    TempData["status"] = "visitexist";
                }
                catch (Exception)
                {
                    TempData["status"] = "unknow";
                }
                ctx.SaveChanges();

                return Redirect("/schedule/edit/" + DoctorId);
            }
        }


    }
}