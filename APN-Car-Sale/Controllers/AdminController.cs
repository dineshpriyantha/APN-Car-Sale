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
    //[Authorize]
    public class AdminController : Controller
    {
        private string baseUrl = "http://localhost:1134/";

        [Authorize]
        // GET: Admin        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public ActionResult AdminLeftPanel()
        {
            return PartialView();
        }

        /////////////////// Sub Category //////////////////////

        public ActionResult SubCategoryIndex()
        {
            return View();
        }

        public JsonResult GetSubCategory()
        {
            IEnumerable<APN_SubCategory> categorys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/SubCategoryAPI"));

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
            return Json(categorys, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateSubCategory(APN_SubCategory category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/SubCategoryAPI"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PostAsJsonAsync<APN_SubCategory>("SubCategoryAPI", category);
                resTask.Wait();

                var result = resTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("SubCategoryIndex");
                }

                return View(category);
            }
        }

        [Authorize]
        public ActionResult UpdateSubCategory(APN_SubCategory category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/SubCategoryAPI"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PutAsJsonAsync<APN_SubCategory>("SubCategoryAPI?id=" + category.SId.ToString(), category);


                //var responseTask = client.GetAsync("Category?id=" + id.ToString());
                resTask.Wait();

                var result = resTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("SubCategoryIndex");
                }

                return View(category);
            }
        }

        public JsonResult GetSubCategoryById(int id)
        {
            APN_SubCategory apnCategory = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/SubCategoryAPI"));
                var responseTask = client.GetAsync("SubCategoryAPI?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<APN_SubCategory>();
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
        [Authorize]
        public JsonResult DeleteSubCategoryRecord(int id)
        {
            bool result = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/SubCategoryAPI"));
                var responseTask = client.DeleteAsync("SubCategoryAPI?id=" + id.ToString());
                responseTask.Wait();

                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var readTask = res.Content.ReadAsAsync<APN_SubCategory>();
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