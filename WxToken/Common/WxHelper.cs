using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WxToken.Common
{
    public static class WxHelper
    {
        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        /// <param name="appid">APPIDy</param>
        /// <param name="secret">凭证</param>
        /// <returns></returns>
        public  static string GetWXAccessToken(string appid, string secret)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
           
            string result = "";
            string access_token = "";
            try
            {
                if (cache.Get(appid) == null)
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
                else
                {
                    return cache.Get(appid).ToString();
                }

            }
            catch (Exception ex)
            {
                //ErrorLogHelper.Error(ex);
                //BizLogHelper.InfoMessage("获取微信accesstoken", "appid:" + appid + ";secret:" + secret + ";返回值:" + result, null);
                return "err";
            }

        }
        public static string GetWXJsapi_Ticket(string accessToken)
        {
            string result = "";
            string access_token = "";
            try
            {
                if (CacheHelper.Get(accessToken) == null)
                {
                    System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(string.Format(WxUrl.Jsapi_TicketUrl, appid, secret).ToString());
                    System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    result = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                    Newtonsoft.Json.Linq.JObject remark = Newtonsoft.Json.Linq.JObject.Parse(result);

                    access_token = remark.GetValue("access_token").ToString();
                    CacheHelper.Add(accessToken, access_token, TimeSpan.FromSeconds(7000));  
                    return access_token;
                }
                else
                {
                    return CacheHelper.Get(accessToken).ToString();
                }

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