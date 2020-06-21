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
    public class APN_FileAPIController : ApiController
    {
        private IRepository<APN_Files, int> file;

        public APN_FileAPIController(IRepository<APN_Files, int> _file)
        {
            this.file = _file;
        }

        public HttpResponseMessage Get()
        {
            IEnumerable<APN_Files> bookList = file.GetAllData();
            return Request.CreateResponse(HttpStatusCode.OK, bookList);
        }
    }
}
