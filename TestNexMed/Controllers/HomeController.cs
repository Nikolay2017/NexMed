using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestNexMed.Models;

namespace TestNexMed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "на "+DateTime.Now.Date.ToShortDateString();
            Task t = new Task(RequestWether);
            t.Start();
            
            return View();
        }

        private async void RequestWether()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("https://api.weatherbit.io/v2.0/current?city=Raleigh,NC&key=58c2500edd1b4308aa4bf5063a7fcb03"))
            using (HttpContent content = response.Content)
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await content.ReadAsStringAsync();

                }
            }
        }

        public ActionResult Arxive()
        {
            ViewBag.Message = "-----------------------";

            return View();
        }
    }

}