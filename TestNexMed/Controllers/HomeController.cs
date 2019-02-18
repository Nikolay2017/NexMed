using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
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
        private HttpClient client;
        private HttpResponseMessage response;
        private HttpContent content;

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

        /// <summary>
        /// Страница "главная"
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = await UserManager.FindByIdAsync(userId);
            var dataRootObject = await GetDataApiServices(currentUser.Sity);
            ViewBag.Message = dataRootObject != null ?
                $"Температура в твоем городе({currentUser.Sity}) = {dataRootObject.data.First().temp.ToString(CultureInfo.InvariantCulture)}" : "Нет связи с погодным сервером!";

            return View();
        }

        /// <summary>
        /// Получение погодных данных с погодного сервера по названию города
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Temperature(ModelSityWeather model)
        {
            var dataRootObject = await GetDataApiServices(model.SityName);
            var temp = dataRootObject != null ?
                $"Температура в городе {model.SityName} = {dataRootObject.data.First().temp.ToString(CultureInfo.InvariantCulture)}" : "Нет связи с погодным сервером!";

            ModelWeather.SeviceData seviceData = new ModelWeather.SeviceData { NameSity = model.SityName, NameService = "weatherbit", Temperature = dataRootObject.data.First().temp, DateWeather = DateTime.Now };
            await SaveWeather(seviceData);

            return RedirectToAction("About", "Home", new { temp });
        }

        /// <summary>
        /// Добавление полученных данных с погодного сервера в базу 
        /// </summary>
        /// <param name="seviceData"></param>
        private async Task SaveWeather(ModelWeather.SeviceData seviceData)
        {
            using (WeatherContext weatherContext = new WeatherContext())
            {
                weatherContext.SeviceDatas.Add(seviceData);
                await weatherContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Вытягивания данных о погоде с сервера
        /// </summary>
        /// <param name="sity"></param>
        /// <returns></returns>
        public async Task<ModelWeather.RootObject> GetDataApiServices(string sity)
        {
            client = new HttpClient();
            ModelWeather.RootObject dataRootObject = null;
            try
            {
                response = await client.GetAsync(
                    $"https://api.weatherbit.io/v2.0/current?city={sity}&key=58c2500edd1b4308aa4bf5063a7fcb03");
                content = response.Content;
                string result = await content.ReadAsStringAsync();
                dataRootObject = JToken.Parse(result).ToObject<ModelWeather.RootObject>(); //json2charp.com

            }
            catch (Exception)
            {
                // ignored
            }

            return dataRootObject;
        }

        /// <summary>
        /// Страница "о погоде"
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public ActionResult About(string temp)
        {
            ViewBag.Message = "на " + DateTime.Now.Date.ToShortDateString();
            ViewBag.Temperature = temp;
            return View();
        }

        /// <summary>
        /// Страница архив
        /// </summary>
        /// <returns></returns>
        public ActionResult Arxive()
        {
            ViewBag.Message = "-----------------------";
            return View();
        }

    }
}