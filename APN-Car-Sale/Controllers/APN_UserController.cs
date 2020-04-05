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
        private IRepository<APN_User, int> users;

        public APN_UserController(IRepository<APN_User, int> _users)
        {
            this.users = _users;
        }

        // GET: api/APN_User
        public HttpResponseMessage Get()
        {
            IEnumerable<APN_User> userList = users.GetAllData();
            return Request.CreateResponse(HttpStatusCode.OK, userList);
        }

        // GET: api/APN_User/5
        public HttpResponseMessage Get(int id)
        {
            var user = users.GetUniqueData(id);
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
        public HttpResponseMessage Post([FromBody]APN_User user)
        {
            try
            {
                users.SaveData(user);
                return Request.CreateResponse(HttpStatusCode.Created, user.name);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/APN_User/5
        public HttpResponseMessage Put(int id, [FromBody]APN_User user)
        {
            try
            {
                var entity = users.GetUniqueData(id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id " + id + " not found to update");
                }
                else
                {
                    users.UpdateRecord(id, user);
                    return Request.CreateResponse(HttpStatusCode.OK, "User with id " + id + " update successfully..");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/APN_User/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = users.GetUniqueData(id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User id " + id + " not found to delete");
                }
                else
                {
                    users.DeleteRecord(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
