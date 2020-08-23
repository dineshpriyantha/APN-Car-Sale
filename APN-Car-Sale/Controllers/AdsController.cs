using APN_Car_Sale.Models;
using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using Newtonsoft.Json;
using PagedList;
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
        public async Task<ActionResult> Vehicle(string sortOrder, string currentFilter, string searchString, int? page, int? PageSize, int? sid)
        {
            IEnumerable<APN_Vehicle> vehicle = await GetVehicle();
            //IEnumerable<APN_Category> category = await GetCategory();
            //IEnumerable<APN_SubCategory> subcategory = await GetSubCategory();

            //ViewModel vModel = new ViewModel();
            //vModel.vehicles = vehicle;
            //vModel.categories = category;
            //vModel.subcategories = subcategory;

            // Not sure here
            if (searchString == null)
            {
                searchString = currentFilter;
            }

            // Pass filtering string to view in order to maintain filtering when paging
            ViewBag.CurrentFilter = searchString;

            var vehicles = from u in vehicle select u;

            // filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(u => (String.Equals(u.Brand, searchString, StringComparison.OrdinalIgnoreCase))
                                          || (String.Equals(u.Model, searchString, StringComparison.OrdinalIgnoreCase)));
            }

            // filter by cid
            if (sid > 0)
            {
                vehicles = vehicles.Where(u => u.Subid == sid);
            }


            // Populate DropDownList
            ViewBag.PageSize = new List<SelectListItem>() {
                new SelectListItem { Text = "10", Value = "10", Selected = true },
                new SelectListItem { Text = "25", Value = "25" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" }
            };

            int pageNumber = (page ?? 1);
            int pageSize = (PageSize ?? 20);
            ViewBag.psize = pageSize;

            return View(vehicles.ToPagedList(pageNumber, pageSize));
        }

        public async Task<JsonResult> getAllads(string txtSearch, int? page, int? sid, int? cid)
        {
            IEnumerable<APN_Vehicle> vehicle = await GetVehicle();

            var data = (from s in vehicle select s);
            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => (String.Equals(s.Brand, txtSearch, StringComparison.OrdinalIgnoreCase))
                                        || (String.Equals(s.Model, txtSearch, StringComparison.OrdinalIgnoreCase)));
            }

            if (cid > 0)
            {
                data = data.Where(x => x.Cid == cid);
            }

            // reset static parameter when sid null
            if (sid == null)
            {
                CommonProperty.CommonProperty.subId = 0;
            }

            // filter by sid
            if (sid > 0)
            {
                CommonProperty.CommonProperty.subId = sid;
            }



            if (CommonProperty.CommonProperty.subId > 0)
                data = data.Where(u => u.Subid == CommonProperty.CommonProperty.subId);

            if (page == null)
            {
                page = 0;
            }
            int pageSize = 20;
            int start = (int)(page - 1) * pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = data.OrderByDescending(x => x.Id).Skip(start).Take(pageSize);
            List<APN_Vehicle> nList = new List<APN_Vehicle>();
            nList = dataPost.ToList();

            return Json(new
            {
                data = nList,
                pageCurrent = page,
                numSize = numSize
            }, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public ActionResult PostAd()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostAd(CommonViewModel model, int Cid, int Subid)
        {
            HttpResponseMessage result = null;
            if (model.ModelName == "Vehicle")
            {
                model.Vehicle.File = SaveFileDetails(model.files, Cid, Subid);
                model.Vehicle.Cid = Cid;
                model.Vehicle.Subid = Subid;
                model.Vehicle.Email = User.Identity.Name;

                result = PastDataVehicle(model.Vehicle);
            }
            else if (model.ModelName == "Book")
            {
                result = PastDataBook(model.Vehicle);
            }

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("PostAd");
            }
            return View();
        }


        public HttpResponseMessage PastDataBook(APN_Vehicle vehicle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(CommonProperty.CommonProperty.baseUrl, "api/APN_Vehicle"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PostAsJsonAsync<APN_Vehicle>("APN_Vehicle", vehicle);
                resTask.Wait();

                var result = resTask.Result;
                return result;
            }
        }


        public HttpResponseMessage PastDataVehicle(APN_Vehicle vehicle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(CommonProperty.CommonProperty.baseUrl, "api/APN_Vehicle"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resTask = client.PostAsJsonAsync<APN_Vehicle>("APN_Vehicle", vehicle);
                resTask.Wait();

                var result = resTask.Result;

                return result;
            }
        }

        public ActionResult ad()
        {
            return View();
        }

        public List<APN_Files> SaveFileDetails(IEnumerable<HttpPostedFileBase> files, int Cid, int Sid)
        {
            List<APN_Files> fileLst = new List<APN_Files>();

            if (files != null)
            {
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

        public PartialViewResult VehiclePartialView()
        {
            return PartialView();
        }

        public PartialViewResult BookPartialView()
        {
            return PartialView();
        }

        public PartialViewResult OtherView()
        {
            return PartialView();
        }
    }
}
