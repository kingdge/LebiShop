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

using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;


namespace qianhai
{
    public partial class submit : ShopPage
    {
        public string OrderCode;
        public string Pid;
        public string Money;
        public string ReturnUrl;
        public string business;
        public string scode = "";
        public string currency = "";

        public string billing_firstName = "";
        public string billing_lastName = "";
        public string billing_email = "";
        public string billing_phone = "";
        public string billing_country = "";
        public string billing_city = "";
        public string billing_address = "";
        public string billing_zip = "";
        public string terminal = "";
        public int pages = 0;//1手机页面
        protected void Page_Load(object sender, EventArgs e)
        {


            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("订单错误");
                Response.End();
                return;
            }
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "qianhai");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            if (pay.FeeRate > 0)
            {
                order.Money_OnlinepayFee = order.Money_Pay * pay.FeeRate / 100;
            }
            order.Site_id_pay = CurrentSite.id;
            B_Lebi_Order.Update(order);
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            business = pay.UserName;
            OrderCode = order.Code;
            Pid = order.Code;
            currency = pay.Currency_Code;
            terminal = pay.terminal;
            Money = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("0.00");
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            ReturnUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/qianhai/ReturnUrl.aspx";

            billing_firstName = RequestTool.RequestSafeString("billing_firstName");
            billing_lastName = RequestTool.RequestSafeString("billing_lastName");
            billing_email = RequestTool.RequestSafeString("billing_email");
            billing_phone = RequestTool.RequestSafeString("billing_phone");
            billing_country = RequestTool.RequestSafeString("billing_country");
            billing_city = RequestTool.RequestSafeString("billing_city");
            billing_address = RequestTool.RequestSafeString("billing_address");
            billing_zip = RequestTool.RequestSafeString("billing_zip");



            //account+terminal+backUrl+order_number+order_currency+order_amount+billing_firstName+billing_lastName+billing_email+secureCode
            scode = pay.UserName + pay.terminal + ReturnUrl + order.Code + currency + Money + billing_firstName + billing_lastName + billing_email + pay.UserKey;
            scode = GetSHA256(scode).ToUpper();

            if (CurrentSite != null)
            {
                if (CurrentSite.IsMobile == 1)
                    pages = 1;//1手机端
            }
        }
        public string GetSHA256(string strData)
        {
            //使用SHA256加密算法：
            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
            byte[] sha256Bytes = System.Text.Encoding.Default.GetBytes(strData);
            byte[] cryString = sha256.ComputeHash(sha256Bytes);
            string sha256Str = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X2");
            }
            return sha256Str;
        }
    }
}
