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
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Web.Script.Serialization;
namespace tonglianPay
{

    public partial class _Default : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string baseUrl = "http://自己的域名";
            string merId = "00000***21";//商户号(MerId)
            string appId = "000000***2";//应用ID(AppId)
            string payKey = "6823fcfe11517******5b8e4c4dc9be9";//支付Key(PayKey)

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
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "tonglianPay");
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

            //支付类型
            int payment_type = RequestTool.RequestInt("paytype");
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/tonglianPay/notify_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/tonglianPay/return_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/


            merId = pay.UserName;
            appId = pay.Appid;
            payKey = pay.UserKey;
            //商户订单号
            string out_trade_no = order.Code;
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = order.Code;
            //必填

            //付款金额
            int total_fee = Convert.ToInt32(order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100)) * 100);
            //必填

            SortedDictionary<string, object> dict = new SortedDictionary<string, object>();

            dict["MerId"] = merId;
            dict["AppId"] = appId;
            dict["NonceStr"] = PayHelp.GenerateNonceStr();
            dict["OrderId"] = order.Code;
            dict["TotalFee"] = total_fee;//单位分
            dict["PayType"] = payment_type;//1: 支付宝H5 2：微信H5 3：快捷支付 4：网关支付
            dict["IPVal"] = GetHostAddress();
            dict["NotifyUrl"] = return_url;
            dict["ReturnUrl"] = notify_url;

            dict["Sign"] = PayHelp.MakeSign(dict, payKey);

            string resultStr = HttpHelp.PostMode("http://www.6q0b15.cn/PaySDK/PayAPI/ReadyPay", "xmlData=" + PayHelp.ToXml(dict));
            SystemLog.Add(resultStr);
            // return resultStr;
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();


                var model = jss.Deserialize<payreturn>(resultStr);
                if (model.status == 1)
                {
                    Response.Redirect(model.url);
                }
                else
                {
                    Response.Write(model.info);
                }


            }
            catch (Exception ex)
            {
                SystemLog.Add(ex.ToString());
            }

        }
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public string GetHostAddress()
        {
            string result = String.Empty;
            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理 
                if (result.IndexOf(".") == -1)     //没有“.”肯定是非IPv4格式 
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIP(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];     //找到不是内网的地址 
                            }
                        }
                    }
                    else if (IsIP(result)) //代理即是IP格式 
                        return result;
                    else
                        result = null;     //代理中的内容 非IP，取IP 
                }
            }

            string IpAddress = (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null &&
                System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ?
                System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] :
                System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.UserHostAddress;

            return result;
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private bool IsIP(string ip)
        {
            if (ip == null || ip == string.Empty || ip.Length < 7 || ip.Length > 15)
                return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

            return regex.IsMatch(ip);
        }
    }

    public class payreturn
    {
        public string url { set; get; }
        public string info { set; get; }
        public int status { set; get; }
    }
}