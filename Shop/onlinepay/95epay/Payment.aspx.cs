using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace onlinepay.epay95
{
    public partial class Payment : ShopPage
    {
        //商户号
        public string MerNo;
        //商户网站产生的订单号[唯一]
        public string BillNo;
        //该笔订单总金额,必须为正数，保留两位有效小数。
        public string Amount;
        //商户返回网址,支付后返回到商户网站的地址，同时要在我们系统里绑定的地址。
        public string ReturnURL;
        //服务器返回地址
        public string NotifyURL;
        //Md5加密信息
        public string MD5info;
        //银行代码
        //工行		"ICBC"
        //农行		"ABC"
        //中行		"BOCSH"
        //建行		"CCB"
        //招行		"CMB"
        //浦发		"SPDB"
        //广发		"GDB"
        //交行		"BOCOM"
        //邮储		"PSBC"
        //中信		"CNCB"
        //民生		"CMBC"
        //光大		"CEB"
        //华夏		"HXB"
        //兴业		"CIB"
        public string PaymentType;
        //CSPAY:网银支付 NCPAY:无卡支付 UNION:银联无卡网银合并
        public string PayType;
        //商户备注信息
        public string MerRemark;
        //订单产品信息
        public string products;
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
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "95epay");
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

            MerNo = pay.UserName;
            BillNo = order.Code;
            Amount = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("f" + currendy.DecimalLength + "");
            ReturnURL = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/95epay/PayResult.aspx";
            NotifyURL = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/95epay/PayNotify.aspx";

            string MD5key = pay.UserKey;//Md5加密私钥[注册时产生]
            string md5md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(MD5key, "MD5").ToUpper();
            string md5src = "Amount=" + Amount + "&BillNo=" + BillNo + "&MerNo=" + MerNo + "&ReturnURL=" + ReturnURL + "&" + md5md5;
            MD5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5").ToUpper();

            PaymentType = "ICBC";
            PayType = "CSPAY";//KJPAY手机端
            try
            {
                //Lebi_Site currentsite = Session["CurrentSite"] as Lebi_Site;
                if (CurrentSite != null)
                {
                    if (CurrentSite.IsMobile == 1)
                        PayType = "KJPAY";//KJPAY手机端
                }
            }
            catch
            {
                PayType = "CSPAY";
            }
            MerRemark = HttpContext.Current.Request.Url.Host;
            products = order.Code;
        }
    }
}