using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;


namespace huifubao
{
    public partial class PostAction : ShopPage
    {
        public string agent_id { get; set; }
        public int pay_type { get; set; }
        public string pay_amt { get; set; }
        public string remark { get; set; }
        public string key { get; set; }
        public string sign { get; set; }
        public int version { get; set; }
        public string agent_bill_id { get; set; }
        public string agent_bill_time { get; set; }
        public string pay_flag { get; set; }
        public string notify_url { get; set; }
        public string return_url { get; set; }
        public string user_ip { get; set; }
        public string goods_name { get; set; }
        public string goods_num { get; set; }
        public string goods_note { get; set; }
        public int is_test { get; set; }
        public string pay_code { get; set; }

        public string OrderCode;
        public string Pid;
        public string Money;
        public string ReturnUrl;
        public string business;
        public Lebi_OnlinePay pay;
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
            pay = Shop.Bussiness.Money.GetOnlinePay(order, "huifubao");
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
            ReturnUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/paypal/ReturnUrl.aspx";





            #region 获取参数值
            version = 1;                                                            //当前接口版本号 1  
            user_ip = GetIP();                                                      //用户所在客户端的真实ip。如 127.127.12.12
            goods_name = order.Code;                               //商品名称, 长度最长50字符

            agent_bill_id = order.Code;                              //商户系统内部的定单号（要保证唯一）。长度最长50字符
            goods_note = order.Code;                                  //支付说明, 长度50字符
            remark = order.Code;                                    //商户自定义 原样返回,长度最长50字符
            is_test = 0; //是否测试 1 为测试
            pay_type = 20;                                          //支付类型见7.1表                   

            pay_code = "";       //银行

            pay_amt = Money;                                       //订单总金额 不可为空，单位：元，小数点后保留两位。12.37
            goods_num ="1";                                     //产品数量,长度最长20字符
            agent_bill_time = DateTime.Now.ToString("yyyyMMddHHmmss");              //提交单据的时间yyyyMMddHHmmss 20100225102000
                                                                                    //agent_id = "商户ID";                                                      //商户编号
            agent_id = pay.UserName;
            //key = "商户key";                                                          //商户密钥
            key = pay.UserKey;
            /*
            //如果需要测试，请把取消关于is_test的注释  订单会显示详细信息

            is_test = "1";
            */
          
            notify_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/huifubao/Return.aspx";
            return_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/huifubao/Notify.aspx";

            sign = GetSign();

            #endregion
        }


        /// <summary>
        /// 获取sign的值
        /// </summary>
        /// <returns></returns>
        private string GetSign()
        {
            StringBuilder _StringSign = new StringBuilder();
            //注意拼接顺序,详情请看《汇付宝即时到帐接口开发指南2.0.4.pdf》
            _StringSign.Append("version=" + version)
                .Append("&agent_id=" + agent_id)
                .Append("&agent_bill_id=" + agent_bill_id)
                .Append("&agent_bill_time=" + agent_bill_time)
                .Append("&pay_type=" + pay_type)
                .Append("&pay_amt=" + pay_amt)
                .Append("&notify_url=" + notify_url)
                .Append("&return_url=" + return_url)
                .Append("&user_ip=" + user_ip);
            if (is_test == 1)
            {
                _StringSign.Append("&is_test=" + is_test);
            }
            _StringSign.Append("&key=" + key);
            //Response.Write(_StringSign.ToString());
            return this.MD5Hash(_StringSign.ToString()).ToLower();
        }

        /// <summary>
        /// 32 位
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns></returns>
        private string MD5Hash(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToLower();
        }

        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>
        private string GetIP()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return "";
        }
    }

}