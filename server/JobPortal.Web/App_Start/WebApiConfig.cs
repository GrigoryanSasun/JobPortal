using JobPortal.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JobPortal.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes(new CommonPrefixProvider("api"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
