using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WxToken.Common;
using WxToken.Models;

namespace WxToken.Controllers
{
    public class ScanCodeController : Controller
    {
        // GET: ScanCode
        public ActionResult Index()
        {

            string appid = ConfigurationManager.AppSettings["appid"];
            string secret = ConfigurationManager.AppSettings["secret"];
            string accesstoken=WxHelper.GetWXAccessToken(appid,secret);
            string jsapi = "kgt8ON7yVITDhtdwci0qefm28dEjHL9zp4vUDrJ-ZMGCqZkoeXHz_M2BtKuwCKVLthE9vH5Hn6dc9Ju89FleiA";
            string noncestr = GenerateNonceStr();
            string timestamp = Timestamp();
            string url = Request.Url.AbsoluteUri.ToString();
            string str = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}",jsapi, noncestr,timestamp,url);
            string str1 = "jsapi_ticket=kgt8ON7yVITDhtdwci0qefm28dEjHL9zp4vUDrJ-ZMHfZo6LQ5KwrVMrUyDASs_m0a6T0ejYPaKi8vAVOI1FOg&noncestr=ac2c4129d87147a8b458a4008df740db&timestamp=1499336214&url=http://1x687f9296.iok.la/ScanCode/Index";
            string sign= FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();
            WxModel wx = new WxModel() {
                appId = appid,
                nonceStr= noncestr,
                timestamp= timestamp,
                signature=sign,
            };
            return View(wx);
        }
        public string Timestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}