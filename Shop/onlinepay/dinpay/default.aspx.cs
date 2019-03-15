using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
/* *
 *功能：即时到账交易接口接入页
 *版本：3.0
 *日期：2013-08-01
 *说明：
 *以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,
 *并非一定要使用该代码。该代码仅供学习和研究智付接口使用，仅为提供一个参考。
 **/
namespace dinpay
{
    public partial class _Default : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                Shop.Bussiness.Site site = new Shop.Bussiness.Site();
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
                Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "dinpay");
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
                //参数编码字符集(必选)
                string input_charset1 = "UTF-8";

                //接口版本(必选)固定值:V3.0
                string interface_version1 = "V3.0";

                //商家号（必填）
                string merchant_code1 = pay.UserName;

                //后台通知地址(必填)
                string notify_url1 = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/pinpay/notify_url.aspx";

                //定单金额（必填）
                string order_amount1 = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("f" + currendy.DecimalLength + ""); ;

                //商家定单号(必填)
                string order_no1 = order.Code;

                //商家定单时间(必填)
                string order_time1 = order.Time_Add.ToString("yyyy-MM-dd HH:mm:ss");

                //签名方式(必填)
                string sign_type1 = "MD5";

                //商品编号(选填)
                string product_code1 = RequestTool.RequestString("product_code");

                //商品描述（选填）
                string product_desc1 = RequestTool.RequestString("product_desc");

                //商品名称（必填）
                string product_name1 = order.Code;

                //端口数量(选填)
                string product_num1 = RequestTool.RequestString("product_num");

                //页面跳转同步通知地址(选填)
                string return_url1 = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/pinpay/return_url.aspx";

                //业务类型(必填)
                string service_type1 = "direct_pay";

                //商品展示地址(选填)
                string show_url1 = RequestTool.RequestString("show_url");

                //公用业务扩展参数（选填）
                string extend_param1 = RequestTool.RequestString("extend_param");

                //公用业务回传参数（选填）
                string extra_return_param1 = RequestTool.RequestString("extra_return_param");

                // 直联通道代码（选填）
                string bank_code1 = RequestTool.RequestString("bank_code");

                //客户端IP（选填）
                string client_ip1 = RequestTool.RequestString("client_ip");

                /* 注  new String(参数.getBytes("ISO-8859-1"),"此页面编码格式"); 若为GBK编码 则替换UTF-8 为GBK*/


                /*
                **
                 ** 签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，同时将商家支付密钥key放在最后参与签名，
                 ** 组成规则如下：
                 ** 参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n&key=key值
                 **/

                string signSrc = "";

                //组织订单信息
                if (bank_code1 != "")
                {
                    signSrc = signSrc + "bank_code=" + bank_code1 + "&";
                }
                if (client_ip1 != "")
                {
                    signSrc = signSrc + "client_ip=" + client_ip1 + "&";
                }
                if (extend_param1 != "")
                {
                    signSrc = signSrc + "extend_param=" + extend_param1 + "&";
                }
                if (extra_return_param1 != "")
                {
                    signSrc = signSrc + "extra_return_param=" + extra_return_param1 + "&";
                }
                if (input_charset1 != "")
                {
                    signSrc = signSrc + "input_charset=" + input_charset1 + "&";
                }
                if (interface_version1 != "")
                {
                    signSrc = signSrc + "interface_version=" + interface_version1 + "&";
                }
                if (merchant_code1 != "")
                {
                    signSrc = signSrc + "merchant_code=" + merchant_code1 + "&";
                }
                if (notify_url1 != "")
                {
                    signSrc = signSrc + "notify_url=" + notify_url1 + "&";
                }
                if (order_amount1 != "")
                {
                    signSrc = signSrc + "order_amount=" + order_amount1 + "&";
                }
                if (order_no1 != "")
                {
                    signSrc = signSrc + "order_no=" + order_no1 + "&";
                }
                if (order_time1 != "")
                {
                    signSrc = signSrc + "order_time=" + order_time1 + "&";
                }
                if (product_code1 != "")
                {
                    signSrc = signSrc + "product_code=" + product_code1 + "&";
                }
                if (product_desc1 != "")
                {
                    signSrc = signSrc + "product_desc=" + product_desc1 + "&";
                }
                if (product_name1 != "")
                {
                    signSrc = signSrc + "product_name=" + product_name1 + "&";
                }
                if (product_num1 != "")
                {
                    signSrc = signSrc + "product_num=" + product_num1 + "&";
                }
                if (return_url1 != "")
                {
                    signSrc = signSrc + "return_url=" + return_url1 + "&";
                }
                if (service_type1 != "")
                {
                    signSrc = signSrc + "service_type=" + service_type1 + "&";
                }
                if (show_url1 != "")
                {
                    signSrc = signSrc + "show_url=" + show_url1 + "&";
                }

                //设置密钥
                string key = pay.UserKey;


                signSrc = signSrc + "key=" + key;

                string singInfo = signSrc;
                //Response.Write("singInfo=" + singInfo + "<br>");


                //签名
                string sign1 = FormsAuthentication.HashPasswordForStoringInConfigFile(singInfo, "md5").ToLower();
                //Response.Write("sign1=" + sign1 + "<br>");

                sign.Value = sign1;
                merchant_code.Value = merchant_code1;
                bank_code.Value = bank_code1;
                order_no.Value = order_no1;
                order_amount.Value = order_amount1;
                service_type.Value = service_type1;
                input_charset.Value = input_charset1;
                notify_url.Value = notify_url1;
                interface_version.Value = interface_version1;
                sign_type.Value = sign_type1;
                order_time.Value = order_time1;
                product_name.Value = product_name1;
                client_ip.Value = client_ip1;
                extend_param.Value = extend_param1;
                extra_return_param.Value = extra_return_param1;
                product_code.Value = product_code1;
                product_desc.Value = product_desc1;
                product_num.Value = product_num1;
                return_url.Value = return_url1;
                show_url.Value = show_url1;


            }
            finally
            {

            }
        }
    }
}