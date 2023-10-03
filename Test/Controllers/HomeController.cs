using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.ServiceReference;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Test.Models.LoginResponse
            {
                IsError= false,
                Message = string.Empty,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string loginName, string loginPassword)
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(loginPassword))
            {
                new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Login or password can not be empty");
            }

            var client = new ICUTechClient();
            var response = await client.LoginAsync(loginName, loginPassword, "127.0.0.1");

            dynamic json = JsonConvert.DeserializeObject(response.@return);

            if(json.ResultCode < 0)
            {
                return View(new Test.Models.LoginResponse
                {
                    IsError = true,
                    Message = response.@return
                });
            }

            var model = new Test.Models.LoginResponse
            {
                IsError= false,
                Message = response.@return
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}