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
    public class APN_VehicleController : ApiController
    {
        private IRepository<APN_Vehicle, int> vehicles;

        public APN_VehicleController(IRepository<APN_Vehicle, int> vehicle)
        {
            this.vehicles = vehicle;
        }

        /// <summary>
        /// GET: api/APN_Vehicle
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            IEnumerable<APN_Vehicle> vehicleList = vehicles.GetAllData();
            return Request.CreateResponse(HttpStatusCode.OK, vehicleList);
        }
        // POST: api/APN_Vehicle
        public HttpResponseMessage Post([FromBody]APN_Vehicle vehicle)
        {
            try
            {
                vehicles.SaveData(vehicle);
                return Request.CreateResponse(HttpStatusCode.Created, vehicle.Brand);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
