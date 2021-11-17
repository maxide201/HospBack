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
using Microsoft.AspNetCore.Identity;

namespace HospBack.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;

        public AccountController(IConfiguration configuration, SignInManager<User> signInManager) : base(configuration)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromForm] AccountViewModel data)
		{
			if (!isDataCorrect(data))
				return View("Login");

            var result = await _signInManager.PasswordSignInAsync(data.Login, data.Password, true, false);

            if (!result.Succeeded)
                return NotFound(); //error

            if (User.IsInRole("doctor"))
                return View("");

            if (User.IsInRole("registrar"))
                return View("registry");

            if (User.IsInRole("admin"))
                return View("admin");

            return View(); //error
        }

		[HttpGet]
		[Route("Login")]
		public IActionResult Login()
		{
            return View();
		}


        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("login");
        }

        private bool isDataCorrect(AccountViewModel data)
        {
            if (data?.Login != null && data?.Password != null)
                return true;
            return false;
        }
    }
}
