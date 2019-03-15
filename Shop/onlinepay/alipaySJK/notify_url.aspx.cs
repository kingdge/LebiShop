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
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace Com.Alipay.SJK
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
    /// 
    /// 如何判断该笔交易是通过即时到帐方式付款还是通过担保交易方式付款？
    /// 
    /// 担保交易的交易状态变化顺序是：等待买家付款→买家已付款，等待卖家发货→卖家已发货，等待买家收货→买家已收货，交易完成
    /// 即时到帐的交易状态变化顺序是：等待买家付款→交易完成
    /// 
    /// 每当收到支付宝发来通知中，就可以获取到这笔交易的交易状态，并且商户需要利用商户订单号查询商户网站的订单数据，
    /// 得到这笔订单在商户网站中的状态是什么，把商户网站中的订单状态与从支付宝通知中获取到的状态来做对比。
    /// 如果商户网站中目前的状态是等待买家付款，而从支付宝通知获取来的状态是买家已付款，等待卖家发货，那么这笔交易买家是用担保交易方式付款的
    ///  如果商户网站中目前的状态是等待买家付款，而从支付宝通知获取来的状态是交易完成，那么这笔交易买家是用即时到帐方式付款的
    /// </summary>
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();
            //商户订单号
            string out_trade_no = RequestTool.RequestSafeString("out_trade_no");
            Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + out_trade_no + "'}");
            if (order == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify(order);
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表


                    //支付宝交易号

                    string trade_no = RequestTool.RequestSafeString("trade_no");

                    //交易状态
                    string trade_status = RequestTool.RequestSafeString("trade_status");
                    Order.OnlinePaySuccess("alipayWAP", out_trade_no);

                    if (Request.QueryString["trade_status"] == "WAIT_BUYER_PAY")
                    {//该判断表示买家已在支付宝交易管理中产生了交易记录，但没有付款

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        Response.Write("success");  //请不要修改或删除
                    }
                    else if (Request.QueryString["trade_status"] == "WAIT_SELLER_SEND_GOODS")
                    {//该判断示买家已在支付宝交易管理中产生了交易记录且付款成功，但卖家没有发货

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        Log.Add(out_trade_no + "等待发货");
                        //Order.PaySuccess(out_trade_no);
                        Response.Write("success");  //请不要修改或删除
                    }
                    else if (Request.QueryString["trade_status"] == "WAIT_BUYER_CONFIRM_GOODS")
                    {//该判断表示卖家已经发了货，但买家还没有做确认收货的操作

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        Response.Write("success");  //请不要修改或删除
                    }
                    else if (Request.QueryString["trade_status"] == "TRADE_FINISHED")
                    {//该判断表示买家已经确认收货，这笔交易完成

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        Response.Write("success");  //请不要修改或删除
                    }
                    else
                    {
                        Response.Write("success");  //其他状态判断。
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}