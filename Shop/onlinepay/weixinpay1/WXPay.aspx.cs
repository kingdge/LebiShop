using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;
//using Newtonsoft.Json;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
namespace weixinpay
{
    /// <summary>
    /// 根据Code获取OpenID等信息
    /// </summary>
    public class ModelOpenID
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }


    public partial class WXPay : Page
    {

        public static string tenpay = "1";  //人民币
        public static string partnerid = ""; //PartnerID
        public static string partnerkey = ""; //PartnerKey
        public static string mchid = ""; //mchid
        public static string appid = ""; //appid
        public static string appsecret = ""; //appsecret
        public static string appkey = ""; //paysignkey(非appkey 在微信商户平台设置 (md5)111111111111) 
        public static string timeStamp = ""; //时间戳 
        public static string nonceStr = ""; //随机字符串 

        public static string notify_url = "http:/111111111111.aspx"; //支付完成后的回调处理页面,*替换成notify_url.asp所在路径

        public static string code = "";     //微信端传来的code
        public static string prepayId = "";     //预支付ID
        public static string sign = "";     //为了获取预支付ID的签名
        public static string paySign = "";  //进行支付需要的签名
        public static string package = "";  //进行支付需要的包

        protected void Page_Load(object sender, EventArgs e)
        {
            Lebi_OnlinePay pay = B_Lebi_OnlinePay.GetModel("Code='weixinpay'");
            if (pay == null)
            {
                Log.Add("在线支付接口 weixinpay 配置错误");
                return;
            }
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            B_BaseConfig bconfig = new B_BaseConfig();
            BaseConfig SYS = bconfig.LoadConfig();
            partnerid = pay.UserName;
            partnerkey = pay.UserKey;
            appid = SYS.platform_weixin_id;
            appsecret = SYS.platform_weixin_secret;
            appkey = "";
            notify_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/weixinpay.aspx";

            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("订单错误");
                Response.End();
                return;
            }

            string order_price = (order.Money_Pay * currendy.ExchangeRate * 100).ToString();
            string sp_billno = order.Code;

            Lebi_User user = B_Lebi_User.GetModel(order.User_id);
            if (user == null)
            {
                Response.Write("订单错误");
                Response.End();
                return;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request["order_no"];
            }

            sp_billno = partnerid + sp_billno;


            //创建支付应答对象
            var packageReqHandler = new RequestHandler(Context);
            //初始化
            packageReqHandler.init();

            timeStamp = TenpayUtil.getTimestamp();
            nonceStr = TenpayUtil.getNoncestr();


            //设置package订单参数

            packageReqHandler.setParameter("body", "test"); //商品信息 127字符
            packageReqHandler.setParameter("appid", user.bind_weixin_id);
            packageReqHandler.setParameter("mch_id", mchid);
            packageReqHandler.setParameter("nonce_str", nonceStr.ToLower());
            packageReqHandler.setParameter("notify_url", notify_url);
            packageReqHandler.setParameter("openid", "openid");
            packageReqHandler.setParameter("out_trade_no", sp_billno); //商家订单号
            packageReqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress); //用户的公网ip，不是商户服务器IP
            packageReqHandler.setParameter("total_fee", "1"); //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.setParameter("trade_type", "JSAPI");

            //获取package包
            sign = packageReqHandler.CreateMd5Sign("key", appkey);
            WriteFile(Server.MapPath("") + "\\Log.txt", sign);
            packageReqHandler.setParameter("sign", sign);

            string data = packageReqHandler.parseXML();

            WriteFile(Server.MapPath("") + "\\Log.txt", data);

            string prepayXml = HttpUtil.Send(data, "https://api.mch.weixin.qq.com/pay/unifiedorder");

            WriteFile(Server.MapPath("") + "\\Log.txt", prepayXml);

            //获取预支付ID
            var xdoc = new XmlDocument();
            xdoc.LoadXml(prepayXml);
            XmlNode xn = xdoc.SelectSingleNode("xml");
            XmlNodeList xnl = xn.ChildNodes;
            if (xnl.Count > 7)
            {
                prepayId = xnl[7].InnerText;
                package = string.Format("prepay_id={0}", prepayId);
                WriteFile(Server.MapPath("") + "\\Log.txt", package);
            }

            //设置支付参数
            var paySignReqHandler = new RequestHandler(Context);
            paySignReqHandler.setParameter("appId", appId);
            paySignReqHandler.setParameter("timeStamp", timeStamp);
            paySignReqHandler.setParameter("nonceStr", nonceStr);
            paySignReqHandler.setParameter("package", package);
            paySignReqHandler.setParameter("signType", "MD5");
            paySign = paySignReqHandler.CreateMd5Sign("key", appkey);


            WriteFile(Server.MapPath("") + "\\Log.txt", paySign);

        }

        public static void WriteFile(string pathWrite, string content)
        {
            if (File.Exists(pathWrite))
            {
                //File.Delete(pathWrite);
            }
            File.AppendAllText(pathWrite, content + "\r\n----------------------------------------\r\n",
                Encoding.GetEncoding("utf-8"));
        }
    }
}