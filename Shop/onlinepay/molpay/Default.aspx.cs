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
using Com.Alipay;


using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;


namespace molpay
{
    public partial class _Default : ShopPage
    {
        public string OrderCode;
        public string mid;
        public string Money;
        public string ReturnUrl;
        public string business;
        public Lebi_Order order;
        public string vcode;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            int order_id = RequestTool.RequestInt("order_id", 0);
            order = B_Lebi_Order.GetModel(order_id);
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
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order,"molpay");
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
                order.Money_OnlinepayFee = order.Money_Pay * pay.FeeRate/100;
            }
            if (order.OnlinePay_id != pay.id)
            {
                order.OnlinePay_id = pay.id;
                order.OnlinePay_Code = pay.Code;
                order.OnlinePay = pay.Name;
            }
            B_Lebi_Order.Update(order);
            business = pay.UserName;
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            OrderCode = order.Code;
            mid = pay.UserName;
            Money = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("f" + currendy.DecimalLength + "");

            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            ReturnUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/paypal/ReturnUrl.aspx";
            vcode = MOLMD5.Sign(Money + pay.UserName + order.Code + pay.UserKey);
        }
    }
}
