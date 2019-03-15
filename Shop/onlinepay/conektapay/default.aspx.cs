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
using conekta;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace conektapay
{

    /// </summary>
    public partial class _Default : ShopPage
    {
        public string reference = "";
        public string money = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            conekta.Api.version = "2.0.0";
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
            //SystemLog.Add("订单：" + order.Code + "-" + CurrentSite.id + "--" + CurrentLanguage.id);
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "conekta");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            conekta.Api.apiKey = pay.UserKey;
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
            if (order.weixin_prepay_id.Contains("oxxopay"))
            {
                //"oxxopay:" + JsonConvert.SerializeObject(jo) + ";";
                string oxxo = RegexTool.GetRegValue(order.weixin_prepay_id, "oxxopay:(.*?)};") + "}";
                try
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(oxxo);
                    reference = jo["reference"].ToString();
                    money = jo["money"].ToString();


                }
                catch (Exception ex)
                {
                    //Response.Write(ex.ToString());
                }
            }
            else
            {

            }

            if (money == order.Money_Pay.ToString())
                return;
            money = order.Money_Pay.ToString();
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            BaseConfig SYS = ShopCache.GetBaseConfig();
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            ////////////////////////////////////////////请求参数////////////////////////////////////////////


            //页面跳转同步通知页面路径
            string return_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/conektapay/return_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //卖家支付宝帐户
            string seller_email = pay.Email;
            //必填

            //商户订单号
            string out_trade_no = order.Code;
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = order.Code;
            //必填

            //付款金额
            string total_fee = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100)) * 100).ToString("f0");
            //必填

            try
            {

                string ostring = @"{
                    ""line_items"": [{
                        ""name"": ""ordercode_{ordercode}"",
                        ""unit_price"": {money},
                        ""quantity"": 1
                    }],
                    ""shipping_lines"": [{
                        ""amount"": 0,
                        ""carrier"": ""xxxxxx""
                    }], //shipping_lines - physical goods only
                    ""currency"": ""MXN"",
                    ""customer_info"": {
                      ""name"": ""{u_name}"",
                      ""email"": ""{u_email}"",
                      ""phone"": ""{u_phone}""
                    },
                    ""shipping_contact"":{
                       ""address"": {
                         ""street1"": ""xxx xxx"",
                         ""postal_code"": ""06100"",
                         ""country"": ""MX""
                       }
                    }, //required only for physical goods
                    ""charges"":[{
                      ""payment_method"": {
                        ""type"": ""oxxo_cash""
                      }
                    }]
               }";

                ostring = ostring.Replace("{ordercode}", order.Code);
                ostring = ostring.Replace("{money}", total_fee);
                ostring = ostring.Replace("{u_name}", order.User_UserName);
                ostring = ostring.Replace("{u_email}", order.T_Email);
                ostring = ostring.Replace("{u_phone}", order.T_MobilePhone);
                ostring = ostring.Replace("{t_name}", order.T_Name);

                conekta.Order order1 = new conekta.Order().create(ostring);


                //ostring = string.Format(ostring, order.Code, total_fee, order.T_Name, order.T_Email, order.T_MobilePhone);
                //conekta.Order corder = new conekta.Order().create(ostring);
                //Response.Write("ID: " + order1.id);
                //Response.Write("ID111: " + order1.charges.toJSON());

                //ID: ord_2jDzPMV51K1i9z9Ti
                //ID111: { "has_more":false,"data":[{"id":"5b91d84aedbb6e28f3ef1639","livemode":false,"created_at":1536284746,"currency":"MXN","payment_method":{"service_name":"OxxoPay","barcode_url":"https://s3.amazonaws.com/cash_payment_barcodes/sandbox_reference.png","object":"cash_payment","type":"oxxo","expires_at":1538870400,"store_name":"OXXO","reference":"98000000140098"},"object":"charge","description":"Payment from order","status":"pending_payment","amount":13500,"fee":549,"customer_id":"","order_id":"ord_2jDzPMV51K1i9z9Ti"}],"_type":"conekta.Charge, conekta, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"}


                //Response.Write("Payment Method: " + order1.charges[0].payment_method.service_name);
                //Response.Write("Reference: " + order1.charges[0].payment_method.reference);
                //Response.Write("$" + (order1.amount / 100) + order1.currency);
                //Response.Write("Order");
                //Response.Write(order1.line_items[0].quantity + " - "
                //            + order1.line_items[0].name + " - "
                //            + (order1.line_items.unit_price / 100));
                reference = RegexTool.GetRegValue(order1.charges.toJSON(), "\"reference\":\"(\\d+)\"");
                order.OnlinePay_Code = order1.id;
                JObject jo = new JObject();
                jo["reference"] = reference;
                jo["money"] = money;
                order.weixin_prepay_id = "oxxopay:" + JsonConvert.SerializeObject(jo) + ";";
                B_Lebi_Order.Update(order);
            }
            catch (ConektaException ex)
            {
                foreach (JObject obj in ex.details)
                {
                    Response.Write("message:\t" + obj.GetValue("message") + "___" + "debug:\t" + obj.GetValue("debug_message") + "___" + "code:\t" + obj.GetValue("code"));
                    Response.End();
                    SystemLog.Add("message:\t" + obj.GetValue("message") + "___" + "debug:\t" + obj.GetValue("debug_message") + "___" + "code:\t" + obj.GetValue("code"));
                    //System.Console.WriteLine("\n [ERROR]:\n");
                    //System.Console.WriteLine("message:\t" + obj.GetValue("message"));
                    //System.Console.WriteLine("debug:\t" + obj.GetValue("debug_message"));
                    //System.Console.WriteLine("code:\t" + obj.GetValue("code"));
                }
            }





        }
    }
}