using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bidia.Models.BidiaModels;

namespace Bidia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BidiaDB>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
