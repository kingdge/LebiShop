using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace weixinpayh5
{
    //=================================
    //H5支付
    //=================================
    public partial class _Default : ShopPage
    {
        public string appId = "";
        public string timeStamp = "";
        public string nonceStr = "";
        public string packageValue = "";
        public string paySign = "";
        public string yufustr = "";
        public Lebi_Order order;
        public string shopname = "";
        public string returnurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            int order_id = RequestTool.RequestInt("order_id", 0);
            order = B_Lebi_Order.GetModel(order_id);
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
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "weixinpayh5");
            if (pay == null)
            {
                Log.Add("在线支付接口 weixinpay 配置错误");
                return;
            }

            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
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
            Lebi_User user = B_Lebi_User.GetModel(order.User_id);
            if (user == null)
            {
                Response.Write("订单错误");
                Response.End();
                return;
            }


            returnurl = URL("P_UserOrderDetails", order.id);
            TenpayUtil tu = new TenpayUtil(order);
            appId = tu.appid;
            timeStamp = TenpayUtil.getTimestamp();
            nonceStr = TenpayUtil.getNoncestr();
            string order_price = (order.Money_Pay * currendy.ExchangeRate * 100 * (1 + (pay.FeeRate / 100))).ToString("0");
            string sp_billno = order.Code + "|" + TenpayUtil.UnixStamp();
            shopname = Lang(CurrentSite.Name);
            string prepayId = order.weixin_prepay_id;
            //if (prepayId == "" || prepayId == "INVALID_REQUEST")
            //{
            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(Context);
            //初始化
            packageReqHandler.init();
            //设置package订单参数
            packageReqHandler.setParameter("body", order.Code); //商品信息 127字符
            packageReqHandler.setParameter("appid", tu.appid);
            packageReqHandler.setParameter("mch_id", tu.mch_id);
            packageReqHandler.setParameter("nonce_str", nonceStr);
            packageReqHandler.setParameter("openid", user.bind_weixin_id);
            packageReqHandler.setParameter("out_trade_no", sp_billno); //商家订单号
            packageReqHandler.setParameter("spbill_create_ip", RequestTool.GetClientIP()); //Page.Request.UserHostAddress); //用户的公网ip，不是商户服务器IP
            packageReqHandler.setParameter("total_fee", order_price); //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.setParameter("trade_type", "MWEB");
            //packageReqHandler.setParameter("notify_url", HttpUtility.UrlEncode(tu.tenpay_notify));
            packageReqHandler.setParameter("notify_url", tu.tenpay_notify);
            //Response.Write("body:" + order.Code + ",appid:" + tu.appid + ",mch_id:" + tu.mch_id + ",nonce_str:" + nonceStr + ",notify_url:" + HttpUtility.UrlEncode(tu.tenpay_notify) + ",openid:" + user.bind_weixin_id + ",out_trade_no:" + sp_billno + ",spbill_create_ip:" + RequestTool.GetClientIP() + ",total_fee:" + order_price + ",key:" + tu.key + "");
            string sign = packageReqHandler.createMd5Sign("key", tu.key);
            packageReqHandler.setParameter("sign", sign);
            string data = packageReqHandler.parseXML();
            string prepayXml = HttpUtil.Send(data, "https://api.mch.weixin.qq.com/pay/unifiedorder");

            SystemLog.Add("prepayXml:" + prepayXml);
            Response.Write("prepayXml:" + prepayXml);

            //return;
            //获取预支付ID
            var xdoc = new XmlDocument();
            xdoc.LoadXml(prepayXml);
            XmlNode xn = xdoc.SelectSingleNode("xml");
            XmlNodeList xnl = xn.ChildNodes;
            //Response.Write("<br/>xml:" + xnl[0].InnerText + "|" + xnl[1].InnerText + "|" + xnl[2].InnerText + "|" + xnl[3].InnerText + "|" + xnl[4].InnerText + "|" + xnl[5].InnerText + "|" + xnl[6].InnerText + "|" + xnl[7].InnerText + "|" + xnl[8].InnerText + "");
            if (xnl.Count > 7)
            {
                prepayId = xnl[7].InnerText;
                order.weixin_prepay_id = prepayId;
                B_Lebi_Order.Update(order);
            }
            if (xnl.Count > 9)
            {
                string url = xnl[9].InnerText;
                Response.Redirect(url);
            }
            //}


        }
    }
}