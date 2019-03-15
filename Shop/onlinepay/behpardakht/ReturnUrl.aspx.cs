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
using System.Net;
using System.IO;
namespace behpardakht
{
    public partial class ReturnUrl : System.Web.UI.Page
    {

        protected long SaleOrderId = 0;
        protected long SaleReferenceId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string RefId = Request.Params["RefId"];
                string ResCode = Request.Params["ResCode"];
                SaleOrderId = Convert.ToInt64(Request.Params["SaleOrderId"]);
                SaleReferenceId = Convert.ToInt64(Request.Params["SaleReferenceId"]);
                string OrderID = Convert.ToString(SaleOrderId);
                Lebi_Order order = B_Lebi_Order.GetModel("id=" + OrderID + "");
                if (order == null)
                {
                    Response.Write("416");
                    //Response.Write("系统错误");
                    Response.End();
                    return;
                }
                Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order.Code, "behpardakht");
                if (pay == null)
                {
                    Response.Write("系统错误");
                    Response.End();
                    return;
                }
                Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);

                string Content = "";
                Content += "OrderCode:" + order.Code + "<br>";
                Content += "ResCode:" + ResCode + "<br>";
                Content += "SaleReferenceId:" + SaleReferenceId + "<br>";
                if (yanzheng(pay) == 0)//验证成功
                {
                    Response.Write("0");
                    Order.OnlinePaySuccess("behpardakht", order.Code);
                }
                else
                {
                    Response.Write("416");
                    //SystemLog.Add("paypal验证失败");

                }
            }
            catch (Exception ex)
            {
                //Response.Write("False");
                SystemLog.Add("behpardakht验证异常" + ex.Message);
            }

        }

        private int yanzheng(Lebi_OnlinePay pay)
        {
            //try
            //{
            //    string result;
            //    BPService.
            //    BPService.PaymentGatewayImplService bpService = new BPService.PaymentGatewayImplService();
            //    result = bpService.bpVerifyRequest(Int16.Parse(TerminalIdTextBox.Text),
            //        UserNameTextBox.Text,
            //        UserPasswordTextBox.Text,
            //        Int64.Parse(VerifyOrderIdTextBox.Text),
            //        Int64.Parse(VerifySaleOrderIdTextBox.Text),
            //        Int64.Parse(VerifySaleReferenceIdTextBox.Text));
            //    VerifyOutputLabel.Text = result;
            //}
            //catch (Exception exp)
            //{
            //    VerifyOutputLabel.Text = "Error: " + exp.Message;
            //}
            //< xs:element name = "terminalId" type = "xs:long" />
            //< xs:element minOccurs = "0" name = "userName" type = "xs:string" />
            //< xs:element minOccurs = "0" name = "userPassword" type = "xs:string" />
            //< xs:element name = "orderId" type = "xs:long" />
            //< xs:element name = "refundOrderId" type = "xs:long" />
            //< xs:element name = "refundReferenceId" type = "xs:long" />
            object[] args = new object[6];
            args[0] = Int64.Parse(pay.terminal);
            args[1] = pay.UserName;
            args[2] = pay.UserKey;
            args[3] = SaleOrderId;
            args[4] = SaleOrderId;
            args[5] = SaleReferenceId;
            string web = "https://bpm.shaparak.ir/pgwchannel/services/pgw";
            object obj = WebServiceTool.InvokeWebService(web, "PaymentGatewayImplService", "bpVerifyRequest", args, "https://bpm.shaparak.ir/pgwchannel/services/pgw?wsdl=IPaymentGateway.wsdl");
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 1;
            }
            // return 0;
        }
    }
}
