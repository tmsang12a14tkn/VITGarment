using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Garment.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}