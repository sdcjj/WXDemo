using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace weixinDemo
{
    public class Helper
    {
        /// <summary>
        /// 分析微信端发来的数据并转换成自己的WeiXinMessage类型
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public static WeiXinMessage convertToMessage(string postStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);
            XmlElement rootElement = doc.DocumentElement;

            XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

            WeiXinMessage requestXML = new WeiXinMessage();
            requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
            requestXML.MsgType = MsgType.InnerText;

            if (requestXML.MsgType == "text")
            {
                requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                requestXML.MsgId = rootElement.SelectSingleNode("MsgId").InnerText;
            }
            else if (requestXML.MsgType == "location")
            {
                requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
            }
            else if (requestXML.MsgType == "image")
            {
                requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
            }
            else if (requestXML.MsgType == "event")
            {
                requestXML.Wxevent = rootElement.SelectSingleNode("Event").InnerText;
                requestXML.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
            }
            return requestXML;
        }


        /// <summary>
        /// datetime转换为Unix时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        //一条消息实例:
        //<xml><ToUserName><![CDATA[gh_ed67f0a66c6f]]></ToUserName>
        //<FromUserName><![CDATA[ox5V9juv7f-7PB7WGgS8Dyblpr4c]]></FromUserName>
        //<CreateTime>1389689382</CreateTime>
        //<MsgType><![CDATA[event]]></MsgType>
        //<Event><![CDATA[CLICK]]></Event>
        //<EventKey><![CDATA[dididetail]]></EventKey>
        //</xml>

        /// <summary>
        /// 获取返回是数据
        /// </summary>
        /// <param name="date">用户发送过来的消息</param>
        /// <returns></returns>
        public static string getMessgae(string date)
        {
            if (string.IsNullOrEmpty(date)) return "";
            string result = "";
            if (date.ToUpper() == "XXX")
            {
                //这里根据date参数获取提供的数据
            }
            else if (date.ToUpper() == "XXX")
            {
                //这里根据date参数获取提供的数据
            }
            else
            {
                //这里根据date参数获取提供的数据
            }


            if (string.IsNullOrEmpty(result))
            {
                return "没有查询到您要的数据，请检查格式！";
            }
            else
            {
                return result;
            }
        }
    }
}