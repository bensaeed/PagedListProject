using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTestPagedList.Database;
using WebAppTestPagedList.Models;
namespace WebAppTestPagedList.Attributes
{
    public class VisiteLogAttribute : ActionFilterAttribute
    {
        db_TestEntities _db = new db_TestEntities();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            var newItem = new tbl_Action
            {
                ActionTime = TimeNow(),
                DateSystem = TodayDate(),
                Browser = ClientBrowser(),
                Device = ClientDeviceType(),
                IP_Address = ClientIPaddress(),
                HostName = ClientHostName(),
                Country = GetLocationIPINFO(ClientIPaddress())
            };
            _db.tbl_Action.Add(newItem);
            _db.SaveChanges();
        }
        public static string TimeNow()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        public static string TodayDate()
        {
            var persianDateTimeNow = DateTime.Now;
            //persianDateTimeNow.EnglishNumber = true;
            return persianDateTimeNow.ToString("yyyy/MM/dd");
        }
        private string ClientIPaddress()
        {
            //var ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            string ipAddress = "";
            ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }
        private string ClientDeviceType()
        {
            string DeviceType = "";
            DeviceType = HttpContext.Current.Request.UserAgent;

            if (string.IsNullOrEmpty(DeviceType))
            {
                DeviceType = "";
            }
            return DeviceType;
        }
        private string ClientBrowser()
        {
            string BrowserName = "";
            HttpRequest req = HttpContext.Current.Request;
            BrowserName = req.Browser.Browser;
            if (string.IsNullOrEmpty(BrowserName))
            {
                BrowserName = "";
            }
            return BrowserName;
        }
        private string ClientHostName()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            if (string.IsNullOrEmpty(strHostName))
            {
                strHostName = "";
            }
            return strHostName;
        }

        private string GetLocationIPINFO(string ipaddress)
        {
            //var url = "http://freegeoip.net/json/" + ClientIPaddress();
            //var request = System.Net.WebRequest.Create(url);
            string strdata="";
                try
            {
                IPDataIPAPI ipInfo = new IPDataIPAPI();
                string strResponse = new WebClient().DownloadString("http://ipinfo.io/" + ipaddress);
                //if (strResponse == null || strResponse == "") return "";
                ipInfo = JsonConvert.DeserializeObject<IPDataIPAPI>(strResponse);
                //if (ipInfo == null || ipInfo.ip == null || ipInfo.ip == "") return "";
                //else return ipInfo.city + "; " + ipInfo.region + "; " + ipInfo.country + "; " + ipInfo.postal;
                strdata = ipInfo.@as + " ** " + ipInfo.city + " ** " + ipInfo.country + " ** " + ipInfo.countryCode + " ** " + ipInfo.isp + " ** " +
                      ipInfo.lat + " ** " + ipInfo.lon + " ** " + ipInfo.org + " ** " +
                      ipInfo.query + " ** " + ipInfo.region + " ** " + ipInfo.regionName + " ** " +
                      ipInfo.status + " ** " + ipInfo.timezone + " ** " +
                      ipInfo.zip + " ** ";
            }
            catch (Exception)
            {
                // return "";
            }
            return strdata;
        }
    }
}