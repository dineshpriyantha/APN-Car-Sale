using APN_Car_Sale.Models;
using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APN_Car_Sale.Controllers
{
    //[RoutePrefix("APNCar")]
    public class AdsController : Controller
    {
        private string baseUrl = "http://localhost:1134/";

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //[Route("ads")]
        public async Task<ActionResult> Vehicle()
        {
            IEnumerable<APN_Vehicle> vehicle = await GetVehicle();
            IEnumerable<APN_Category> category = await GetCategory();
            IEnumerable<APN_SubCategory> subcategory = await GetSubCategory();

            ViewModel vModel = new ViewModel();
            vModel.vehicles = vehicle;
            vModel.categories = category;
            vModel.subcategories = subcategory;

            return View(vModel);
        }

        public ActionResult PostAd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostAd(APN_Vehicle vehicle)
        {
            IEnumerable<APN_Files> File = SaveFileDetails(vehicle.files, vehicle.Cid, vehicle.Subid);
            vehicle.File = File;
            vehicle.files = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/APN_Vehicle"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PostAsJsonAsync<APN_Vehicle>("APN_Vehicle", vehicle);
                resTask.Wait();

                var result = resTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("SubCategoryIndex");
                }

                return View(vehicle);
            }
        }

        public ActionResult ad()
        {
            return View();
        }

        public List<APN_Files> SaveFileDetails(IEnumerable<HttpPostedFileBase> files, int Cid, int Sid)
        {
            List<APN_Files> fileLst = new List<APN_Files>();

            foreach (var item in files)
            {
                fileLst.Add(new APN_Files
                {
                    Name = item.FileName,
                    ContentType = item.ContentType,
                    ImageBytes = ConvertToBytes(item),
                    Cid = Cid,
                    Sid = Sid
                });

            }
            return fileLst;
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }

        public PartialViewResult LeftSideBar()
        {
            return PartialView();
        }

        private async Task<IEnumerable<APN_Category>> GetCategory()
        {
            IEnumerable<APN_Category> categorys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/Category"));

                var responseTask = client.GetAsync("Category");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APN_Category>>();
                    readTask.Wait();

                    categorys = readTask.Result;
                }
                else
                {
                    categorys = Enumerable.Empty<APN_Category>();
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            return categorys;
        }

        private async Task<IEnumerable<APN_SubCategory>> GetSubCategory()
        {
            IEnumerable<APN_SubCategory> categorys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/GetSubCategory"));

                var responseTask = client.GetAsync("SubCategoryAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APN_SubCategory>>();
                    readTask.Wait();

                    categorys = readTask.Result;
                }
                else
                {
                    categorys = Enumerable.Empty<APN_SubCategory>();
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            return categorys;
        }

        private async Task<IEnumerable<APN_Vehicle>> GetVehicle()
        {
            IEnumerable<APN_Vehicle> vehicle = null;

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
            }
            return vehicle;
        }

        public JsonResult GetSubCategoryByCategoryId(int cid)
        {
            IEnumerable<APN_SubCategory> apnCategory = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/SubCategoryAPI"));
                var responseTask = client.GetAsync("SubCategoryAPI?cid=" + cid.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APN_SubCategory>>();
                    readTask.Wait();
                    apnCategory = readTask.Result;
                }
                else
                {
                    apnCategory = null;
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            return Json(apnCategory, JsonRequestBehavior.AllowGet);
        }
    }
}
