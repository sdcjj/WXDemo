using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

namespace weixinDemo
{
    public partial class _default : System.Web.UI.Page
    {
        const string token = "123456789";//在网站里填写的Token

        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = string.Empty;
            #region 数据都是post过来的  其他的非法都屏蔽
            if (Request.HttpMethod.ToLower() == "post")
            {
                #region 获取真实数据流转换成UTF-8字符串
                Stream stream = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[stream.Length];
                stream.Read(b, 0, (int)stream.Length);
                postStr = System.Text.Encoding.UTF8.GetString(b);
                #endregion


                #region 分析字符串
                if (!string.IsNullOrEmpty(postStr))
                {
                    //分析数据并转换成WeiXinMessage对象
                    WeiXinMessage message = Helper.convertToMessage(postStr);
                    //回复消息
                    responseMsg(message);
                } 
                #endregion
            } 
            #endregion
        }

        /// <summary>
        /// 回复消息(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private void responseMsg(WeiXinMessage requestXML)
        {
            string resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" 
                        + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + Helper.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            try
            {
                if (requestXML.MsgType == "text")
                {
                    int count = 0;
                    if (requestXML.Content.Trim() == "一个图")//用户发送"一个图"时返回一个图片消息
                    {
                        count = 1;
                        resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                        resxml += "<item><Title><![CDATA[这里是标题]]></Title><Description><![CDATA[图片描述]]></Description><PicUrl><![CDATA[图片地址]]></PicUrl><Url><![CDATA[点击图片后跳转的地址]]></Url></item>";//URL是点击之后跳转去那里
                        resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                    }
                    else if (requestXML.Content.Trim() == "多图")//用户发送"多个图"时返回一个图片消息
                    {
                        count = 10;//假设是10个图
                        resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                        for (int i = 0; i < count; i++)
                        {
                            resxml += "<item><Title><![CDATA[这里是标题" + (i + 1) + "]]></Title><Description><![CDATA[图片描述]]></Description><PicUrl><![CDATA[图片地址]]></PicUrl><Url><![CDATA[点击图片后跳转的地址]]></Url></item>";//URL是点击之后跳转去那里
                        }
                        resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                    }
                    else//用户发送其他文本数据时
                    {
                        resxml += string.Format("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content><FuncFlag>0</FuncFlag></xml>", Helper.getMessgae(requestXML.Content));
                    }
                }
                else if (requestXML.MsgType == "location")//用户发送一个地理位置
                {
                    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发过来的是地理消息]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else if (requestXML.MsgType == "image")//用户发送一个图片
                {
                    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发一张图片]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else if (requestXML.MsgType == "voice")//用户发送语音消息
                {
                    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发一张图片]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else if (requestXML.MsgType == "link")//用户发送链接
                {
                    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发一张图片]]></Content><FuncFlag>0</FuncFlag></xml>";
                }                    
                else if (requestXML.MsgType == "event")//用户点击菜单按钮时
                {
                    string detal = "";
                    if (requestXML.EventKey == "XXX")
                    {
                        detal = "欢迎关注XXX";

                    }
                    else if (requestXML.EventKey == "XXX")//
                    {
                        detal = "EventKey对应的提示";
                    }

                    //if (requestXML.Wxevent == "unsubscribe")
                    //{
                    //    //取消关注
                    //}
                    //else
                    //{
                    //    //新增关注
                    //    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[谢谢你关注我。]]></Content><FuncFlag>0</FuncFlag></xml>";
                    //}

                    resxml += string.Format("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content><FuncFlag>0</FuncFlag></xml>", detal);
                }
            }
            catch (Exception ex)
            {
                //这里进行异常处理
            }
            Response.Write(resxml);
            Response.End();
        }

        
    }
}