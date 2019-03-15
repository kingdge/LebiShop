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
using LB.Tools;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using System.Collections.Specialized;
namespace weixinpayh5
{
    //'==================
    //'通知验证逻辑
    //'==================


    public partial class payNotifyUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            //判断签名
            try
            {

                //string return_code = RequestTool.RequestString("return_code");//SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查看 result_code 来判断
                //string return_msg = RequestTool.RequestString("return_msg");//返回信息，如非空，为错误原因/签名失败/参数格式校验错误
                //string appid = RequestTool.RequestString("appid");
                //string mch_id = RequestTool.RequestString("mch_id");
                //string nonce_str = RequestTool.RequestString("nonce_str");
                //string sign = RequestTool.RequestString("sign");
                //string result_code = RequestTool.RequestString("result_code");//业务结果SUCCESS/FAIL
                //string error_code = RequestTool.RequestString("error_code");
                //string error_code_des = RequestTool.RequestString("error_code_des");
                //string openid = RequestTool.RequestString("openid");
                //string trade_type = RequestTool.RequestString("trade_type");//交易类型
                //string bank_type = RequestTool.RequestString("bank_type");//out_trade_no
                //string total_fee = RequestTool.RequestString("total_fee");
                //string out_trade_no = RequestTool.RequestString("out_trade_no");
                //string attach = RequestTool.RequestString("attach");//商家数据包，原样返回
                //string time_end = RequestTool.RequestString("time_end");
                //string transaction_id = RequestTool.RequestString("transaction_id");//微信支付单号
                #region 协议参数=====================================
                //--------------协议参数--------------------------------------------------------
                //SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查
                string return_code = resHandler.getParameter("return_code");
                //返回信息，如非空，为错误原因签名失败参数格式校验错误
                string return_msg = resHandler.getParameter("return_msg");
                //微信分配的公众账号 ID
                string appid = resHandler.getParameter("appid");

                //以下字段在 return_code 为 SUCCESS 的时候有返回--------------------------------
                //微信支付分配的商户号
                string mch_id = resHandler.getParameter("mch_id");
                //微信支付分配的终端设备号
                string device_info = resHandler.getParameter("device_info");
                //微信分配的公众账号 ID
                string nonce_str = resHandler.getParameter("nonce_str");
                //业务结果 SUCCESS/FAIL
                string result_code = resHandler.getParameter("result_code");
                //错误代码 
                string err_code = resHandler.getParameter("err_code");
                //结果信息描述
                string err_code_des = resHandler.getParameter("err_code_des");

                //以下字段在 return_code 和 result_code 都为 SUCCESS 的时候有返回---------------
                //-------------业务参数---------------------------------------------------------
                //用户在商户 appid 下的唯一标识
                string openid = resHandler.getParameter("openid");
                //用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
                string is_subscribe = resHandler.getParameter("is_subscribe");
                //JSAPI、NATIVE、MICROPAY、APP
                string trade_type = resHandler.getParameter("trade_type");
                //银行类型，采用字符串类型的银行标识
                string bank_type = resHandler.getParameter("bank_type");
                //订单总金额，单位为分
                string total_fee = resHandler.getParameter("total_fee");
                //货币类型，符合 ISO 4217 标准的三位字母代码，默认人民币：CNY
                string fee_type = resHandler.getParameter("fee_type");
                //微信支付订单号
                string transaction_id = resHandler.getParameter("transaction_id");
                //商户系统的订单号，与请求一致。
                string out_trade_no = resHandler.getParameter("out_trade_no");
                out_trade_no = out_trade_no.Split('|')[0];
                //商家数据包，原样返回
                string attach = resHandler.getParameter("attach");
                //支 付 完 成 时 间 ， 格 式 为yyyyMMddhhmmss，如 2009 年12 月27日 9点 10分 10 秒表示为 20091227091010。时区为 GMT+8 beijing。该时间取自微信支付服务器
                string time_end = resHandler.getParameter("time_end");
                #endregion
                SystemLog.Add("JSAPI微信支付回调payNotifyUrl");
                Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + out_trade_no + "'}");
                if (order == null)
                {
                    Response.Write("系统错误");
                    SystemLog.Add("JSAPI微信支付-订单号" + out_trade_no + "不存在");
                    Response.End();
                    return;
                }
                TenpayUtil tu = new TenpayUtil(order);
                //SystemLog.Add(return_code + "|||" + result_code + "|||" + openid + "|||" + mch_id + "|||" + out_trade_no);

                if (return_code == "SUCCESS" && result_code == "SUCCESS")
                {
                    Lebi_User user = B_Lebi_User.GetModel(order.User_id);
                    Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "weixinpayh5"); 
                    if (pay == null)
                    {
                        Log.Add("在线支付接口 weixinpay 配置错误");
                        SystemLog.Add("JSAPI微信支付-接口配置错误");
                        return;
                    }
                    //if (user.bind_weixin_id == openid && pay.UserName == mch_id)
                    //{
                    Order.OnlinePaySuccess("weixinpayh5", out_trade_no, transaction_id, false);
                    Response.Write(paysuccess());
                    return;
                    //}

                }
                SystemLog.Add("JSAPI微信支付-签名失败");
                Response.Write(payerror("签名失败"));

            }
            catch (Exception ex)
            {
                SystemLog.Add("JSAPI微信支付-参数格式校验错误[" + ex.ToString() + "]");
                Response.Write(payerror("参数格式校验错误"));
            }
        }

        private string paysuccess()
        {
            string str = "<xml><return_code>SUCCESS</return_code><return_msg></return_msg></xml>";
            return str;
        }
        private string payerror(string msg)
        {
            string str = "<xml><return_code>FAIL</return_code><return_msg><![CDATA[" + msg + "]]></return_msg></xml>";
            return str;
        }
    }
}