using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Indexzor.Common;

namespace Indexzor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Search", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            if(ConfigurationManager.AppSettings["BuildIndexOnStartup"] == "true")
            {
                DocumentIndexBuilder indexBuilder = new DocumentIndexBuilder(Server.MapPath(ConfigurationManager.AppSettings["IndexDirectory"]), ConfigurationManager.AppSettings["ContentDirectory"]);
                indexBuilder.Build();
            }
        }
    }
}