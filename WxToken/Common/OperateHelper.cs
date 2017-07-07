using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;

namespace WxToken.Common
{
    public static class OperateHelper
    {
        public static string Post(string url,string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = payload.Length;

            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.IO.Stream stream = request.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string retString = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return retString;
        }
        public static string Get(string url)
        {
            System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(url);
            System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return result;
        }
        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        public static string Timestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public static string SHA1(string str)
        {
            return  FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");

        }
    }
}