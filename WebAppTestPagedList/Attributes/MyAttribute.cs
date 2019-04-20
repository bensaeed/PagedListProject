using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using WebAppTestPagedList.Database;
namespace WebAppTestPagedList.Attributes
{
    public class MyAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //db_TestEntities _db = new db_TestEntities();
            //var WebsiteState = _db.tbl_WebsiteState.FirstOrDefault();
            //if (WebsiteState.State == 0)
            //{

            //} 
            SetNewValue();
            var strName = ConfigurationManager.AppSettings["DownWebSite"];
            if (strName == "True")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "DownWeb"
                }));
            }
        }
        public void SetNewValue()
        {
            Configuration AppConfigSettings = WebConfigurationManager.OpenWebConfiguration("~");
            AppConfigSettings.AppSettings.Settings["DownWebSite"].Value = "20";
            var strName = ConfigurationManager.AppSettings["DownWebSite"];
            if (strName == "True")
            {
                AppConfigSettings.AppSettings.Settings["DownWebSite"].Value = "False";
            }
            else
            {
                AppConfigSettings.AppSettings.Settings["DownWebSite"].Value = "True";
            }

            AppConfigSettings.Save();
        }


    }
}