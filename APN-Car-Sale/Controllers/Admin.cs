using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APNCarSaleDataService.Models;
using APN_Car_Sale;
using APNCarSaleDataService.Interfaces;

namespace APN_Car_Sale.Controllers
{
    public class Admin : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IRepository<APN_Category, int> categorys;

        public Admin(IRepository<APN_Category, int> categorys)
        {
            this.categorys = categorys;
        }

        // GET: api/Admin
        public HttpResponseMessage GetAPN_Category()
        {
            IEnumerable<APN_Category> categoryList = categorys.GetAllData();
            return Request.CreateResponse(HttpStatusCode.OK, categoryList);
        }

        // GET: api/Admin/5
        [ResponseType(typeof(APN_Category))]
        public async Task<IHttpActionResult> GetAPN_Category(int id)
        {
            APN_Category aPN_Category = await db.APN_Category.FindAsync(id);
            if (aPN_Category == null)
            {
                return NotFound();
            }

            return Ok(aPN_Category);
        }

        // PUT: api/Admin/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAPN_Category(int id, APN_Category aPN_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aPN_Category.id)
            {
                return BadRequest();
            }

            db.Entry(aPN_Category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APN_CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Admin
        [ResponseType(typeof(APN_Category))]
        public async Task<IHttpActionResult> PostAPN_Category(APN_Category aPN_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.APN_Category.Add(aPN_Category);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = aPN_Category.id }, aPN_Category);
        }

        // DELETE: api/Admin/5
        [ResponseType(typeof(APN_Category))]
        public async Task<IHttpActionResult> DeleteAPN_Category(int id)
        {
            APN_Category aPN_Category = await db.APN_Category.FindAsync(id);
            if (aPN_Category == null)
            {
                return NotFound();
            }

            db.APN_Category.Remove(aPN_Category);
            await db.SaveChangesAsync();

            return Ok(aPN_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool APN_CategoryExists(int id)
        {
            return db.APN_Category.Count(e => e.id == id) > 0;
        }
    }
}