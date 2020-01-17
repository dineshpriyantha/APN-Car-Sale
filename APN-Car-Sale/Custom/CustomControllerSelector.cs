using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace APN_Car_Sale.Custom
{
    /// <summary>
    /// use for find custom controller with vesrion
    /// </summary>
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        HttpConfiguration __config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            __config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            var contollerName = routeData.Values["controller"].ToString();
            string versionNumber = "1";
            var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            if (versionQueryString["v"] != null)
            {
                versionNumber = versionQueryString["v"];
            }

            if (versionNumber == "1")
            {
                contollerName = contollerName + "V1";
            }
            else
            {
                contollerName = contollerName + "V2s";
            }

            HttpControllerDescriptor controllerDescription;
            if (controllers.TryGetValue(contollerName, out controllerDescription))
            {
                return controllerDescription;
            }
            return null;
        }
    }
}