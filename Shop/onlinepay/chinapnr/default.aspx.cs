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
using CHINAPNRLib;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace chinapnr
{
    public partial class default_ : ShopPage
    {
        public string Version, CmdId, MerId, OrdId, OrdAmt, CurCode, Pid, RetUrl, BgRetUrl, MerPriv, GateId, UsrMp, DivDetails, PayUsrId, ChkValue;
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
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "chinapnr");
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




            Version = "10";              //版本号
            CmdId = "Buy";
            MerId = pay.UserName;        //商户号    
            OrdId = order.Code;//订单编号
            if (OrdId.Length > 20)
                OrdId = "lebiorderid_" + order.id.ToString();
            OrdAmt = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("f" + currendy.DecimalLength + "");//订单金额
            CurCode = pay.Currency_Code;//币种
            Pid = "";//商品编号
            RetUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/chinapnr/Buy_return_url.aspx";
            BgRetUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/chinapnr/Buy_notify_url.aspx";
            MerPriv = ""; //商户私有域,你可以转递一些贵网站系统中特有的参数 选填项
            GateId = "";   //银行网关号,如果输入网关号将直接跳至银行支付页面   网关号可向汇付销售人员获取  选填项
            UsrMp = "";     //用户手机号  选填项
            DivDetails = ""; //分帐信息,当存在将支付款项分别入帐时需要 选填项
            PayUsrId = "";     //付款人的汇付用户号 选填项

            //签名
            String MsgData;
            MsgData = Version + CmdId + MerId + OrdId + OrdAmt + CurCode + Pid + RetUrl + MerPriv + GateId + UsrMp + DivDetails + PayUsrId + BgRetUrl;
            CHINAPNRLib.NetpayClient SignObject = new NetpayClientClass();
            string keypath = System.Web.HttpContext.Current.Server.MapPath("~/" + pay.Email.TrimStart('/'));
            ChkValue = SignObject.SignMsg0(MerId, keypath, MsgData, MsgData.Length);           //请将此处改成你的私钥文件所在路径

        }
    }
}