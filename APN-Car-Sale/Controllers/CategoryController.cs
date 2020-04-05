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
    public class CategoryController : ApiController
    {
        private IRepository<APN_Category, int> categorys;

        public CategoryController(IRepository<APN_Category, int> categorys)
        {
            this.categorys = categorys;
        }

        public HttpResponseMessage Get()
        {
            IEnumerable<APN_Category> categoryList = categorys.GetAllData();
            return Request.CreateResponse(HttpStatusCode.OK, categoryList);
        }
        // GET: api/APN_Category/5
        public HttpResponseMessage Get(int id)
        {
            var user = categorys.GetUniqueData(id);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id " + id + " not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
        }

        // POST: api/APN_Category
        public HttpResponseMessage Post([FromBody]APN_Category category)
        {
            try
            {
                categorys.SaveData(category);
                return Request.CreateResponse(HttpStatusCode.Created, category.name);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/APN_Category/5
        public HttpResponseMessage Put(int id, [FromBody]APN_Category category)
        {
            try
            {
                var entity = categorys.GetUniqueData(id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "category with id " + id + " not found to update");
                }
                else
                {
                    categorys.UpdateRecord(id, category);
                    return Request.CreateResponse(HttpStatusCode.OK, "category with id " + id + " update successfully..");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/APN_Category/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = categorys.GetUniqueData(id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "category id " + id + " not found to delete");
                }
                else
                {
                    categorys.DeleteRecord(id);
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
