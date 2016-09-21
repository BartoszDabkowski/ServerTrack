using ServerTracker.Persistence;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ServerTracker
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Create Server and populate LoadRecords in memory
            System.Web.HttpRuntime.Cache["InMemoryContext"] = new InMemoryContext();
            var inMemoryContext = (InMemoryContext)System.Web.HttpRuntime.Cache["InMemoryContext"];
            inMemoryContext.LoadRecordsByServer.AddServer(Global.Utility.PopulatedServer);
            inMemoryContext.LoadRecordsByServer.InitializeServersLoadRecordsRandom(Global.Utility.PopulatedServer, 0.1, 1.5);
            inMemoryContext.LoadRecordsByServer.AddServer(Global.Utility.NewServer);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
