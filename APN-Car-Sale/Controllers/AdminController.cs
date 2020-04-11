using APN_Car_Sale.Filters;
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
    public class AdminController : Controller
    {
        private string baseUrl = "http://localhost:1134/";

        // GET: Admin        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(APN_Category category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/APN_Category"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PostAsJsonAsync<APN_Category>("Category", category);
                resTask.Wait();

                var result = resTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Update(APN_Category category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/APN_Category"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PutAsJsonAsync<APN_Category>("Category?id=" + category.id.ToString(), category);


                //var responseTask = client.GetAsync("Category?id=" + id.ToString());
                resTask.Wait();

                var result = resTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(category);
            }
        }

        public JsonResult GetCategory()
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
            return Json(categorys, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoryById(int id)
        {
            APN_Category apnCategory = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/Category"));
                var responseTask = client.GetAsync("Category?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<APN_Category>();
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

        public JsonResult DeleteCategoryRecord(int id)
        {
            bool result = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/Category"));
                var responseTask = client.DeleteAsync("Category?id=" + id.ToString());
                responseTask.Wait();

                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var readTask = res.Content.ReadAsAsync<APN_Category>();
                    readTask.Wait();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}