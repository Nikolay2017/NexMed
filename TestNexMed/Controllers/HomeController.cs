using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using TestNexMed.Models;

namespace TestNexMed.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Message = "";
            var userId = User.Identity.GetUserId();
            var currentUser = await UserManager.FindByIdAsync(userId);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();

            response = await client.GetAsync(
                $"https://api.weatherbit.io/v2.0/current?city={currentUser.Sity}&key=58c2500edd1b4308aa4bf5063a7fcb03");
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                string result = await content.ReadAsStringAsync();
                ModelWeather.RootObject dataRootObject = JToken.Parse(result).ToObject<ModelWeather.RootObject>(); //json2charp.com
                ViewBag.Message =
                    $"Температура твоего города({currentUser.Sity}) = {dataRootObject.data.First().temp.ToString(CultureInfo.InvariantCulture)}";
            }
            else
            {
                
            }

            
           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "на "+DateTime.Now.Date.ToShortDateString();
            //Task t = new Task(RequestWether);
            //t.Start();
            return View();
        }

        public ActionResult Arxive()
        {
            ViewBag.Message = "-----------------------";
            return View();
        }
    }

}