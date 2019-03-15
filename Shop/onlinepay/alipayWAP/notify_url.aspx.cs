using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Xml;
using Shop.Model;
using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace Com.Alipay.wap
{

    /// <summary>
    /// 功能：服务器异步通知页面
    /// 版本：3.3
    /// 日期：2012-07-10
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// ///////////////////页面功能说明///////////////////
    /// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
    /// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
    /// 该页面调试工具请使用写文本函数logResult。
    /// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
    /// </summary>
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> sPara = GetRequestPost();
           
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sPara["notify_data"]);
                
            }
            catch(Exception)
            {
                Response.Write("系统错误");
                SystemLog.Add( "alipayWAP-付款异步通知失败-XML解析异常");
                Response.End();
            }
            //商户订单号
            string out_trade_no = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText; ;
            SystemLog.Add("out_trade_no___" + out_trade_no);
            Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + out_trade_no + "'}");
            if (order == null)
            {
                Response.Write("系统错误");
                SystemLog.Add(out_trade_no + "-alipayWAP-付款异步通知失败-无此订单");
                Response.End();
                return;
            }
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify(order);
                bool verifyResult = aliNotify.VerifyNotify(sPara, Request.Form["sign"]);
                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //解密（如果是RSA签名需要解密，如果是MD5签名则下面一行清注释掉）
                    sPara = aliNotify.Decrypt(sPara);

                    //XML解析notify_data数据
                    try
                    {

                        xmlDoc.LoadXml(sPara["notify_data"]);
                        //商户订单号
                        //string out_trade_no = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText;
                        //支付宝交易号
                        string trade_no = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText;
                        //交易状态
                        string trade_status = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText;

                        if (trade_status == "TRADE_FINISHED")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            //注意：
                            //该种交易状态只在两种情况下出现
                            //1、开通了普通即时到账，买家付款成功后。
                            //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。
                            // Log.Add(out_trade_no + "等待发货");
                            Order.OnlinePaySuccess("alipayWAP", out_trade_no);
                            Response.Write("success");  //请不要修改或删除
                        }
                        else if (trade_status == "TRADE_SUCCESS")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            //注意：
                            //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。

                            Order.OnlinePaySuccess("alipayWAP", out_trade_no);
                            Response.Write("success");  //请不要修改或删除
                        }
                        else
                        {
                            SystemLog.Add(out_trade_no + "-alipayWAP-付款异步通知失败-" + trade_status + "");
                            Response.Write(trade_status);
                        }

                    }
                    catch (Exception exc)
                    {
                        SystemLog.Add(out_trade_no + "-alipayWAP-付款异步通知失败-" + exc.ToString() + "");
                        Response.Write(exc.ToString());
                    }



                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    SystemLog.Add(out_trade_no + "-alipayWAP-付款异步通知失败-fail");
                    Response.Write("fail");
                }
            }
            else
            {
                SystemLog.Add(out_trade_no + "-alipayWAP-付款异步通知失败-无通知参数");
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
       
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}