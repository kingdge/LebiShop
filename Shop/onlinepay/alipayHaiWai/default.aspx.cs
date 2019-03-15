using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Com.Alipay.HaiWai
{
    /// <summary>
    /// 功能：支付宝境外商户交易创建接口接入页
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// /////////////////注意///////////////////////////////////////////////////////////////
    /// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
    /// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
    /// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
    /// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
    /// 
    /// 如果不想使用扩展功能请把扩展功能参数赋空值。
    /// </summary>
    public partial class _Default : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("ERROR");
                Response.End();
                return;
            }
            Lebi_Language language = B_Lebi_Language.GetModel(order.Language_id);
            if (order.IsPaid == 1)
            {
                Response.Write(Language.Tag("已付款", language.Code));
                Response.End();
                return;
            }
            order.Site_id_pay = CurrentSite.id;
            order.Language_id = CurrentLanguage.id;
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "alipayHaiWai");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            if (pay.FreeFeeRate == 1)
            {
                pay.FeeRate = 0;
            }
            if (pay.FeeRate > 0)
            {
                order.Money_OnlinepayFee = order.Money_Pay * pay.FeeRate / 100;
            }
            if (order.OnlinePay_id != pay.id)
            {
                order.OnlinePay_id = pay.id;
                order.OnlinePay_Code = pay.Code;
                order.OnlinePay = pay.Name;
            }
            B_Lebi_Order.Update(order);
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            BaseConfig SYS = ShopCache.GetBaseConfig();
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //Return URL
            string return_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/alipayHaiWai/return_url.aspx";
            //After the payment transaction is done
            //Notification URL
            string notify_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/alipayHaiWai/notify_url.aspx";
            //The URL for receiving notifications after the payment process.

            //Goods name
            string subject = order.Code;
            //required，The name of the goods.

            //Goods description
            string body = order.Code;
            //A detailed description of the goods.

            //Outside trade ID
            string out_trade_no = order.Code;
            //required，A numbered transaction ID （Unique inside the partner system）

            //Currency
            string currency = pay.Currency_Code;
            //required，The settlement currency merchant named in contract.

            //Payment sum
            string total_fee = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("f" + currendy.DecimalLength + "");
            //required，A floating number ranging 0.01～1000000.00


            ////////////////////////////////////////////////////////////////////////////////////////////////
            Config config = new Config(order);
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", config.Partner);
            sParaTemp.Add("_input_charset", config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_forex_trade");
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("currency", currency);
            if (currency.ToLower() == "cny")
                sParaTemp.Add("rmb_fee", total_fee);
            else
                sParaTemp.Add("total_fee", total_fee);


            //建立请求
            Submit submit = new Submit(order);
            string sHtmlText = submit.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);

        }
    }
}