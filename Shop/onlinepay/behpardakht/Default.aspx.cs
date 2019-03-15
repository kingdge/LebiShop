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


namespace behpardakht
{
    public partial class _Default : ShopPage
    {
        public string OrderCode;
        public string Pid;
        public string Money;
        public string ReturnUrl;
        public string business;
        public int order_id;
        public Lebi_OnlinePay pay;
        public string refid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            order_id = RequestTool.RequestInt("order_id", 0);
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
            pay = Shop.Bussiness.Money.GetOnlinePay(order, "behpardakht");
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
            business = pay.UserName;
            OrderCode = order.Code;
            Pid = order.Code;
            Money = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("f" + currendy.DecimalLength + "");

            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            ReturnUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/behpardakht/ReturnUrl.aspx";

            //BPService.PaymentGatewayImplService bpService = new BPService.PaymentGatewayImplService();
            //result = bpService.bpPayRequest(Int64.Parse(TerminalIdTextBox.Text),
            //    UserNameTextBox.Text,
            //    UserPasswordTextBox.Text,
            //    Int64.Parse(PayOrderIdTextBox.Text),
            //    Int64.Parse(PayAmountTextBox.Text),
            //    PayDateTextBox.Text,
            //    PayTimeTextBox.Text,
            //    PayAdditionalDataTextBox.Text,
            //    PayCallBackUrlTextBox.Text,
            //    Int64.Parse(PayPayerIdTextBox.Text));


            object[] args = new object[10];
            args[0] = Int64.Parse(pay.terminal);
            args[1] = pay.UserName;
            args[2] = pay.UserKey;
            args[3] = order.id;
            args[4] = Convert.ToInt64(Money);
            args[5] = order.Time_Add.ToString("yyyyMMdd");
            args[6] = order.Time_Add.ToString("HHmmss");
            args[7] = order.Code;
            args[8] = ReturnUrl;
            args[9] = 0;

            string web = "https://bpm.shaparak.ir/pgwchannel/services/pgw";
            object obj = WebServiceTool.InvokeWebService(web, "PaymentGatewayImplService", "bpPayRequest", args, "https://bpm.shaparak.ir/pgwchannel/services/pgw?wsdl=IPaymentGateway.wsdl");
            string result = "";
            try
            {
                result = Convert.ToString(obj);
            }
            catch
            {
                result = "";
            }
          
            String[] resultArray = result.Split(',');
            if (resultArray[0] == "0")
            {
                refid = resultArray[1];
                order.OnlinePay_Code = refid;
            }
            //ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);

        }
    }
}
