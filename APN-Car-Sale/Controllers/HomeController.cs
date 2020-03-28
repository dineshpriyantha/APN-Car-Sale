using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APN_Car_Sale.Controllers
{
    [RoutePrefix("APNCar")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("ads")]
        public async Task<ActionResult> Vehicle()
        {
            string baseUrl = "http://localhost:1134/";
            List<APN_Vehicle> vehicle = new List<APN_Vehicle>();

            using (var client = new HttpClient())
            {
                // passing service base url
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();

                // define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // sending request to find web api REST service resource
                HttpResponseMessage res = await client.GetAsync("api/APN_Vehicle");

                // check the response is sucessful or not with is sent using HttpClient  
                if (res.IsSuccessStatusCode)
                {
                    // storing th =e response details recieved from wen api
                    var vehicles = res.Content.ReadAsStringAsync().Result;

                    vehicle = JsonConvert.DeserializeObject<List<APN_Vehicle>>(vehicles);
                }
                return View(vehicle);
            }

        }
    }
}
