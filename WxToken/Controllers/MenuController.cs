using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace WxToken.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            string appid = ConfigurationManager.AppSettings["appid"];
            string secret = ConfigurationManager.AppSettings["secret"];
            string accessToken = "dvUqhkbuLLxZVOf5ng1-4KQ__0FykRbkJANxuXSZSzSkDAQMZPWA-IrfeEd4GZidDcN1tkgsQgr71Zj0bTBPo7NYjYSqdIUw6ps5AD75DFGedwUbrEpbpUHXJPmgcZRsARGdACAOFU";// GetWXAccessToken(appid, secret);

            string url = " https://api.weixin.qq.com/cgi-bin/menu/create?access_token="+ accessToken;
            string postDataStr = "{\"button\":[{\"type\":\"click\",\"name\":\"今日歌曲\",\"key\":\"V1001_TODAY_MUSIC\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"view\",\"name\":\"扫一扫\",\"url\":\"http://1x687f9296.iok.la/ScanCode/Index\"},{\"type\":\"view\",\"name\":\"视频\",\"url\":\"http://v.qq.com/\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}]}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = payload.Length;

            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            //var retString = request.GetResponse() as HttpWebResponse;
            System.IO.Stream stream = request.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
           string retString = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return Content(retString.ToString());
        }
        /// <summary>
         /// 获取微信AccessToken
         /// </summary>
         /// <param name="appid">APPIDy</param>
         /// <param name="secret">凭证</param>
         /// <returns></returns>
        public string GetWXAccessToken(string appid, string secret)
        {
            System.Web.Caching.Cache cache = new System.Web.Caching.Cache();
            string result = "";
            string access_token = "";
            try
            {
                //if (cache.Get(appid) == null)
                {
                    System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret).ToString());
                    System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    result = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                    Newtonsoft.Json.Linq.JObject remark = Newtonsoft.Json.Linq.JObject.Parse(result);
                   
                    access_token = remark.GetValue("access_token").ToString();
                    cache.Insert(appid, access_token);//, TimeSpan.FromSeconds(7000)
                    return access_token;
                }
                //else
                //{
                //    return cache.Get(appid).ToString();
                //}

            }
            catch (Exception ex)
            {
                //ErrorLogHelper.Error(ex);
                //BizLogHelper.InfoMessage("获取微信accesstoken", "appid:" + appid + ";secret:" + secret + ";返回值:" + result, null);
                return "err";
            }

        }
    }
    

}