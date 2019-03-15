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
using System.Net;
using System.IO;

namespace molpay
{
    public partial class ReturnUrl : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            
            string tranID = RequestTool.RequestString("tranID");
            string orderid = RequestTool.RequestString("orderid");

            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(orderid,"molpay");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }

            string status = RequestTool.RequestString("status");
            string domain = RequestTool.RequestString("domain");
            string amount = RequestTool.RequestString("amount");
            string currency = RequestTool.RequestString("currency");
            string paydate = RequestTool.RequestString("paydate");
            string appcode = RequestTool.RequestString("appcode");
            string skey = RequestTool.RequestString("skey");
            string key0 = MOLMD5.Sign(tranID + orderid + status + domain + amount + currency);
            string key1 = MOLMD5.Sign(paydate + domain + key0 + appcode + pay.UserKey);

            if (skey != key1)
            {
                status = "1";
            }
            if (status == "00")
            {
                //验证成功
                Order.OnlinePaySuccess("molpay", orderid);
            }
            else
            { }

        }


    }
}
