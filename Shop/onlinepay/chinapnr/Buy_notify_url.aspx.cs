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
    ///订单支付交易通知页
    public partial class Buy_notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CmdId, MerId, RespCode, TrxId, OrdAmt, CurCode, Pid, OrdId, MerPriv, RetType, DivDetails, GateId, ChkValue;

            CmdId = RequestString("CmdId");				//消息类型
            MerId = RequestString("MerId"); 	 		    //商户号
            RespCode = RequestString("RespCode"); 		//应答返回码
            TrxId = RequestString("TrxId"); 			    //钱管家交易唯一标识
            OrdAmt = RequestString("OrdAmt"); 			//金额
            CurCode = RequestString("CurCode");  		//币种
            Pid = RequestString("Pid");  				//商品编号
            OrdId = RequestString("OrdId");  			//订单号
            MerPriv = RequestString("MerPriv"); 		    //商户私有域
            RetType = RequestString("RetType");  		//返回类型
            DivDetails = RequestString("DivDetails");  	//分账明细
            GateId = RequestString("GateId");			//银行ID
            ChkValue = RequestString("ChkValue");		//签名信息

            Lebi_Order order;
            if (OrdId.Contains("lebiorderid_"))
            {
                //OrdId = OrdId.Replace("lebiorderid_", "");
                order = B_Lebi_Order.GetModel("id=lbsql{'" + OrdId.Replace("lebiorderid_", "") + "'}");
            }
            else
            {
                order = B_Lebi_Order.GetModel("Code=lbsql{'" + OrdId + "'}");
            }
            if (order == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "chinapnr");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            //验证签名
            string MsgData, SignData;
            MsgData = CmdId + MerId + RespCode + TrxId + OrdAmt + CurCode + Pid + OrdId + MerPriv + RetType + DivDetails + GateId;  	//参数顺序不能错
            CHINAPNRLib.NetpayClient SignObject = new NetpayClientClass();
            string keypath = System.Web.HttpContext.Current.Server.MapPath("~/" + pay.UserKey.TrimStart('/'));
            SignData = SignObject.VeriSignMsg0(keypath, MsgData, MsgData.Length, ChkValue);           //请将此处改成你的私钥文件所在路径

            if (SignData == "0")
            {
                if (RespCode == "000000")
                {
                    //交易成功
                    //根据订单号 进行相应业务操作
                    //在些插入代码
                    Order.OnlinePaySuccess("chinapnr", order.Code);
                }
                else
                {
                    //交易失败
                    //根据订单号 进行相应业务操作
                    //在些插入代码
                }
                Response.Write("RECV_ORD_ID_" + OrdId);
            }
            else
            {
                //验签失败
            }

        }
        public string RequestString(string key)
        {
            if (Request[key] == null)
                return "";
            return Request.Form[key].ToString();
        }
    }
}