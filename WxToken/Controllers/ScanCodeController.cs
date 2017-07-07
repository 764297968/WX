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

            string accesstoken=WxHelper.GetWXAccessToken(WxConfig.AppId, WxConfig.Secret);
            if(accesstoken!="err")
            {
                string jsapi = "kgt8ON7yVITDhtdwci0qefm28dEjHL9zp4vUDrJ-ZMGCqZkoeXHz_M2BtKuwCKVLthE9vH5Hn6dc9Ju89FleiA";
                string noncestr =OperateHelper. GenerateNonceStr();
                string timestamp = OperateHelper.Timestamp();
                string url = Request.Url.AbsoluteUri.ToString();
                string str = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi, noncestr, timestamp, url);
                string sign = OperateHelper.SHA1(str).ToLower();
                WxModel wx = new WxModel()
                {
                    appId = WxConfig.AppId,
                    nonceStr = noncestr,
                    timestamp = timestamp,
                    signature = sign,
                };
                return View(wx);
            }
            else
            {
                return Content("err");
            }
            
        }
       
    }
}