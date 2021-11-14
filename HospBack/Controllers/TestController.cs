using HospBack.DB.Helpers;
using Microsoft.AspNetCore.Authorization;
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
        public TestController(IConfiguration configuration) : base(configuration) { }

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
            catch (Exception)
            {
                return Message("Exeption thrown");
            }
        }

        #region Private methods

        private static JsonResult Message(string message)
        {
            return new JsonResult($"{DateTime.Now}: {message}");
        }

        #endregion Private methods
    }
}
