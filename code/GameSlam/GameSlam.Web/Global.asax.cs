using GameSlam.Infrastructure.Repositories;
using System;
using System.Collections.Generic;      
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GameSlam.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // store the default database values.
            


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<ApplicationDbContext, Infrastructure.Migrations.Configuration>());
            
            /*
            //System.Data.Entity.Database.SetInitializer<ApplicationDbContext>(new GameSlamContextInitializer());
            using (var g = new ApplicationDbContext())
            {
                g.Database.CommandTimeout = 1440;
                //g.Database.Connection.ConnectionTimeout = 1440;
                g.Database.Initialize(true);
            }
            */

        }
    }
}
