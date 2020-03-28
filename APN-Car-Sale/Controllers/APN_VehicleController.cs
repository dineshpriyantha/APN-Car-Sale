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
        private IRepository<APN_Vehicle, int> vehicle;

        public APN_VehicleController(IRepository<APN_Vehicle, int> vehicle)
        {
            this.vehicle = vehicle;
        }

        /// <summary>
        /// GET: api/APN_Vehicle
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            IEnumerable<APN_Vehicle> vehicleList = vehicle.GetAllData();
            return Request.CreateResponse(HttpStatusCode.OK, vehicleList);
        }

    }
}
