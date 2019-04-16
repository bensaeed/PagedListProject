using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebAppTestPagedList.Database;
using WebAppTestPagedList.Models;

namespace WebAppTestPagedList
{
    public class GetVisitLog
    {
        db_TestEntities _db = new db_TestEntities();
        public void StartOperation()
        {
            string CurrentClientIP = ClientIPaddress();
            //GetLocationIPINFO(CurrentClientIP);
            GetLocationIPINFOFrom_IpApi(CurrentClientIP);
            var newItem = new tbl_Action
            {
                ActionTime = TimeNow(),
                DateSystem = TodayDate(),
                Browser = ClientBrowser(),
                Device = ClientDeviceType(),
                IP_Address = CurrentClientIP,
                HostName = ClientHostName(),
                Country = NewInfoIF.country,
                asn = NewInfoIF.@as,
                city = NewInfoIF.city,
                countryCode = NewInfoIF.countryCode,
                isp = NewInfoIF.isp,
                lat = Convert.ToString(NewInfoIF.lat),
                lon = Convert.ToString(NewInfoIF.lon),
                org = NewInfoIF.org,
                query = NewInfoIF.query,
                region = NewInfoIF.region,
                regionName = NewInfoIF.regionName,
                Status = NewInfoIF.status,
                timezone = NewInfoIF.timezone,
                zip = NewInfoIF.zip,
                district = NewInfoIF.district,
                mobile = NewInfoIF.mobile == true ? "1" : "0",
                proxy = NewInfoIF.proxy == true ? "1" : "0",
                reverse = NewInfoIF.reverse
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

            //var context = ((HttpApplication)source).Context;
            //ipAddress = context.Request.UserHostAddress;
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
        InfoIP NewInfoIF;
        private void GetLocationIPINFOFrom_IpApi(string ipaddress)
        {
            NewInfoIF = new InfoIP();
            try
            {
                string url = "http://ip-api.com/json/" + ipaddress + "?fields=status,message,country,countryCode,region,regionName,city,district,zip,lat,lon,timezone,isp,org,as,reverse,mobile,proxy,query";
                string strResponse = new WebClient().DownloadString(url);
                NewInfoIF = JsonConvert.DeserializeObject<InfoIP>(strResponse);
            }
            catch (Exception)
            {
            }

        }
        
    }
}