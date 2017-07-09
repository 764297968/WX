using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WxToken.Common
{
    public class WeiXinXML
    {

        public static string CreateTextMsg(XmlDocument xmlDoc, string content)
        {
            string strTpl = string.Format(@"<xml>
                <ToUserName><![CDATA[{0}]]></ToUserName>
                <FromUserName><![CDATA[{1}]]></FromUserName>
                <CreateTime>{2}</CreateTime>
                <MsgType><![CDATA[text]]></MsgType>
                <Content><![CDATA[{3}]]></Content>
                </xml>", GetFromXML(xmlDoc, "FromUserName"), GetFromXML(xmlDoc, "ToUserName"),
                       DateTime2Int(DateTime.Now), content);

            return strTpl;
        }

        public static int DateTime2Int(DateTime dt)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(dt - startTime).TotalSeconds;
        }

        public static string GetFromXML(XmlDocument xmlDoc, string name)
        {
            XmlNode node = xmlDoc.SelectSingleNode("xml/" + name);
            if (node != null && node.ChildNodes.Count > 0)
            {
                return node.ChildNodes[0].Value;
            }
            return "";
        }
        /// <summary>  
        /// 图文消息主体  
        /// </summary>  
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>  
                            <ToUserName><![CDATA[{0}]]></ToUserName>  
                            <FromUserName><![CDATA[{1}]]></FromUserName>  
                            <CreateTime>{2}</CreateTime>  
                            <MsgType><![CDATA[news]]></MsgType>  
                            <ArticleCount>{3}</ArticleCount>  
                            <Articles>  
                            {4}  
                            </Articles>  
                            </xml> ";
            }
        }
        /// <summary>  
        /// 图文消息项  
        /// </summary>  
        public static string Message_News_Item
        {
            get
            {
                return @"<item>  
                            <Title><![CDATA[{0}]]></Title>   
                            <Description><![CDATA[{1}]]></Description>  
                            <PicUrl><![CDATA[{2}]]></PicUrl>  
                            <Url><![CDATA[{3}]]></Url>  
                            </item>";
            }
        }
    }
}