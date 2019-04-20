using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebAppTestPagedList.Helper;

namespace WebAppTestPagedList
{
    public class MvcApplication : System.Web.HttpApplication
    {
        GetVisitLog objGetVisitLog = new GetVisitLog();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            LogSetting.FirstRequest = 0;
        }
        protected void Application_BeginRequest()
        {
            if (LogSetting.FirstRequest==0)
            {
                LogSetting.FirstRequest = 1;
                objGetVisitLog.StartOperation();
            }

        }
        protected void Application_End()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

}
