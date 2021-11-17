using HospBack.DB;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IConfiguration configuration, UserManager<User> userManager,
                                    RoleManager<IdentityRole> roleManager) : base(configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("registry")]
        public IActionResult Registry()
        {
            using(var ctx = CreateDataContext())
			{
                var registrars = ctx.Registrars.ToList();
                return View(registrars);
            }
        }

        [HttpPost]
        [Route("registry/create")]
        public IActionResult CreateRegsitrar([FromForm] RegistrarViewModel registrar)
        {
            if (!isDataCorrect(registrar))
                return Redirect("../");

            using (var ctx = CreateDataContext())
            {
                var isExist = ctx.Users.Any(x => x.Email == registrar.Email);
                if(isExist)
                    return Redirect("../");

				
                var user = new User();
                user.Email = registrar.Email;
                var password = new PasswordHasher<User>();
                user.PasswordHash = password.HashPassword(user, "12345");
                ctx.Users.Add(user);
                _userManager.AddToRoleAsync(user, "registrar");

                var model = registrar.ToDataModel();
                model.UserId = user.Id;
                ctx.Registrars.Add(model);
                ctx.SaveChanges();

                return Redirect("../");
            }
        }

        private bool isDataCorrect(RegistrarViewModel registrar)
        {
            if (registrar?.Name != null && registrar?.Surname != null)
                return true;
            return false;
        }

    }
}
