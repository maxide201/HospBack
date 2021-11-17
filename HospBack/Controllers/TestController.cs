using HospBack.DB;
using HospBack.DB.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.Controllers
{
    
    public class TestController : ControllerBase
	{
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestController(IConfiguration configuration, UserManager<User> userManager,
                                    RoleManager<IdentityRole> roleManager) : base(configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public JsonResult Index()
        {
            return Message("OK");
        }

        [HttpGet]
        [Authorize]
        public JsonResult IsAuthorized()
        {
            return Message($"{User.Identity.Name} {User.GetUserId()}");
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> Role()
        {
            using(var ctx = CreateDataContext())
			{
                var user = _userManager.Users.Where(x => x.Id == AuthorizedUserId).FirstOrDefault();
                var a = await _userManager.GetRolesAsync(user);
                return new JsonResult(a);
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> DbConnection()
        {
            try
            {
                using (var ctx = CreateDataContext())
                {
                    var connected = await ctx.Database.CanConnectAsync();
                    var a = ctx.Hospitals.FirstOrDefault(); // test
                    return Message(connected ? "Connected" : "Not connected");
                }
            }
            catch (Exception e)
            {
                return Message("Exeption thrown");
            }
        }

        [HttpGet]
        public async Task<JsonResult> Init()
		{

            string[] roles = new string[] { "admin", "doctor", "registrar" };

            foreach (string role in roles)
            {
                var isExist = await _roleManager.RoleExistsAsync(role);
                if (!isExist)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
            var user = new User
            {
                UserName = "admin"
            };

            var admin = await _userManager.FindByNameAsync(user.UserName);
            if (admin == null)
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                await _userManager.CreateAsync(user);
                var result = await _userManager.AddToRoleAsync(user, "admin");

            }

            return new JsonResult("success!");
        }

        #region Private methods

        private static JsonResult Message(string message)
        {
            return new JsonResult($"{DateTime.Now}: {message}");
        }

        #endregion Private methods
    }
}
