using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTestPagedList.Database;
namespace WebAppTestPagedList.Attributes
{
    public class MyAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            db_TestEntities _db = new db_TestEntities();
            var WebsiteState = _db.tbl_WebsiteState.FirstOrDefault();

            if (WebsiteState.State == 0)
                filterContext.Result = new RedirectResult(string.Format("~/Home/DownWeb", filterContext.HttpContext.Request.Url.AbsolutePath));
        }


    }
}