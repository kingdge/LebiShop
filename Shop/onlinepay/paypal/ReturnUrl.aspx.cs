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

namespace Paypal
{
    public partial class ReturnUrl : System.Web.UI.Page
    {
        public string OrderCode;
        public string Pid;
        public string Money;
        public string returnUrl;
        public string business;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ppTx = RequestTool.RequestSafeString("txn_id");
                string ppStatus = RequestTool.RequestSafeString("payment_status");
                string ppDate = RequestTool.RequestSafeString("payment_date");
                string ppItem = RequestTool.RequestSafeString("item_name");
                string ppPrice = RequestTool.RequestSafeString("mc_gross");
                string ppitem_number = RequestTool.RequestSafeString("item_number");//存储站内订单号


                Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(ppitem_number, "paypal");
                if (pay == null)
                {
                    Response.Write("系统错误");
                    Response.End();
                    return;
                }
                Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);

                string Content = "";
                Content += "txn_id:" + ppTx + "<br>";
                Content += "payment_status:" + ppStatus + "<br>";
                Content += "payment_date:" + ppDate + "<br>";
                Content += "item_name:" + ppItem + "<br>";
                Content += "mc_gross:" + ppPrice + "<br>";
                Content += "ppitem_number:" + ppitem_number + "<br>";
                string txToken = Request.QueryString["tx"];//paypal流水号

                if (VerifyIPN())//验证成功
                {
                    Response.Write("OK");
                    Order.OnlinePaySuccess("paypal", ppitem_number);
                }
                else
                {
                    if (VerifyPDT(pay))
                    {
                        Response.Write("OK");
                        Order.OnlinePaySuccess("paypal", ppitem_number);
                    }
                    else
                    {
                        Response.Write("False");
                        //SystemLog.Add("paypal验证失败");
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("False");
                SystemLog.Add("paypal验证异常" + ex.Message);
            }

        }

        bool VerifyIPN()
        {

            string serverURL = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            if (strRequest == "")
            {
                strRequest = Request.Form.ToString();
            }
            if (strRequest == "")
            {
                strRequest = Request.QueryString.ToString();
            }
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();
            //SystemLog.Add("ipn验证---------------" + strRequest);
            SystemLog.Add("ipn验证结果" + strResponse);
            return strResponse == "VERIFIED";
        }

        bool VerifyPDT(Lebi_OnlinePay pay)
        {
            string strFormValues;
            string strNewValues;
            string strResponse;
            string txToken = Request.QueryString["tx"].ToString();
            string serverURL = "https://www.paypal.com/cgi-bin/webscr";
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + pay.UserKey;
            //请注意,请将PayPaypal.PDTToken设置为上文中你记录下来的身份标记
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            strFormValues = Encoding.ASCII.GetString(param);
            strNewValues = strFormValues + query;
            //query附加上去一起发送给paypal
            req.ContentLength = strNewValues.Length;
            StreamWriter stout = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            stout.Write(strNewValues);
            stout.Close();
            StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = sr.ReadToEnd();
            sr.Close();
            SystemLog.Add("pdt验证结果" + strResponse);
            if (strResponse.IndexOf("SUCCESS") == 0)
                return true;
            return false;
            //string res = strResponse.Substring(0, strResponse.IndexOf("\n"));
            //return res == "SUCCESS";

        }
    }
}
