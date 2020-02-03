using Garment.Web.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiContrib.Formatting.Jsonp;

namespace Garment.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Configure();
            //var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //jsonFormatter.SerializerSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;
            GlobalConfiguration.Configuration.BindParameter(typeof(DateTime), new HttpDateTimeBinder());
            GlobalConfiguration.Configuration.BindParameter(typeof(DateTime?), new HttpDateTimeBinder());

            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableDateTimeBinder());

            //jsonFormatter.SerializerSettings = new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};
            //var config = GlobalConfiguration.Configuration;
            //config.Formatters.Insert(0, new JsonpMediaTypeFormatter(jsonFormatter));
            GlobalConfiguration.Configure(config =>
            {
                //config.MapHttpAttributeRoutes();
                //var jsonFormatter = config.Formatters.JsonFormatter;
                //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                config.AddJsonpFormatter();
            });
        }
        void Application_BeginRequest(object sender, EventArgs e)
        {
            string strUserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
            if (strUserAgent != null)
            {
                if (!Request.Url.AbsoluteUri.Contains(@"/api/")
                    && Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") || strUserAgent.Contains("ipad") ||
                    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    strUserAgent.Contains("palm"))
                {
                    Response.Redirect("http://m." + Request.Url.Authority);
                }
            }
        }

    }
}
