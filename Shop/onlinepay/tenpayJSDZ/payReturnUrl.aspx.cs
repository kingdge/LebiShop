using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using tenpayApp;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

public partial class payReturnUrl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string out_trade_no = Request.QueryString["out_trade_no"];
        Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + out_trade_no + "'}");
        if (order == null)
        {
            Response.Write("系统错误");
            Response.End();
            return;
        }
        TenpayUtil tu = new TenpayUtil(order);
        //创建ResponseHandler实例
        ResponseHandler resHandler = new ResponseHandler(Context);
        resHandler.setKey(tu.tenpay_key);

        //判断签名
        if (resHandler.isTenpaySign())
        {

            ///通知id
            string notify_id = resHandler.getParameter("notify_id");
            //商户订单号
            out_trade_no = resHandler.getParameter("out_trade_no");
            //财付通订单号
            string transaction_id = resHandler.getParameter("transaction_id");
            //金额,以分为单位
            string total_fee = resHandler.getParameter("total_fee");
            //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
            string discount = resHandler.getParameter("discount");
            //支付结果
            string trade_state = resHandler.getParameter("trade_state");
            //交易模式，1即时到账，2中介担保
            string trade_mode = resHandler.getParameter("trade_mode");

            if ("1".Equals(trade_mode))
            {       //即时到账 
                if ("0".Equals(trade_state))
                {



                    Order.OnlinePaySuccess("tenpayJSDZ", out_trade_no, transaction_id);
                    //Response.Write("即时到帐付款成功");
                    //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知

                }
                else
                {
                    Response.Write("即时到账支付失败");
                }

            }
            else if ("2".Equals(trade_mode))
            {    //中介担保
                if ("0".Equals(trade_state))
                {




                    Response.Write("中介担保付款成功");
                    //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知

                }
                else
                {
                    Response.Write("trade_state=" + trade_state);
                }
            }
        }
        else
        {
            Response.Write("认证签名失败");
        }

        //获取debug信息,建议把debug信息写入日志，方便定位问题
        string debuginfo = resHandler.getDebugInfo();
        Response.Write("<br/>debuginfo:" + debuginfo + "<br/>");

    }
}
