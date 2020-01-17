using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APN_Car_Sale.Controllers
{
    public class APN_UserController : ApiController
    {
        private IUserRepository users;

        public APN_UserController(IUserRepository _users)
        {
            this.users = _users;
        }

        // GET: api/APN_User
        public HttpResponseMessage Get()
        {
            IEnumerable<APN_User> userList = users.GetAllUsers();
            return Request.CreateResponse(HttpStatusCode.OK, userList);
        }

        // GET: api/APN_User/5
        public HttpResponseMessage Get(int id)
        {
            var user = users.GetUser(id);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id " + id + " not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
        }

        // POST: api/APN_User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/APN_User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/APN_User/5
        public void Delete(int id)
        {
            Console.WriteLine("Deleted");
        }
    }
}
