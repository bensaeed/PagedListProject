using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAppTestPagedList
{
    public class MvcApplication : System.Web.HttpApplication
    {
        GetVisitLog objGetVisitLog = new GetVisitLog();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest()
        {
objGetVisitLog.StartOperation();
        }
        protected void Application_End()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            


        }
    }

}
