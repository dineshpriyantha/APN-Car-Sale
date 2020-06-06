using APN_Car_Sale.Models;
using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace APN_Car_Sale.Controllers
{
    public class AccountController : Controller
    {
        private string baseUrl = "http://localhost:1134/";

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(APN_User user)
        {
            if (IsValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return RedirectToAction("Vehicle", "Ads");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(user);
        }

        /// <summary>
        /// Check valid User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValid(string email, string password)
        {
            IEnumerable<APN_User> user = null;
            bool IsValid = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/APN_User"));
                var responseTask = client.GetAsync("APN_User");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APN_User>>();
                    readTask.Wait();

                    user = readTask.Result;
                    var crypto = new SimpleCrypto.PBKDF2();
                    var logUser = user.FirstOrDefault(u => u.Email == email);
                    if (logUser != null)
                    {
                        if (logUser.Password == crypto.Compute(password, logUser.PasswordSalt))
                        {
                            IsValid = true;
                        }
                    }
                }
            }
            return IsValid;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(APN_User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APN_User newUser = new APN_User();
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrypPass = crypto.Compute(user.Password);
                    newUser.Email = user.Email;
                    newUser.Password = encrypPass;
                    newUser.PasswordSalt = crypto.Salt;
                    newUser.FirstName = user.FirstName;
                    newUser.UserType = "User";
                    newUser.CreatedDate = DateTime.Now;
                    newUser.IsActive = true;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(String.Concat(baseUrl, "api/APN_User"));
                        var resposeTask = client.PostAsJsonAsync<APN_User>("APN_User", newUser);
                        resposeTask.Wait();
                        if (resposeTask.Result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}