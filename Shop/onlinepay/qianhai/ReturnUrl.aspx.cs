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
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Net;
using System.IO;

namespace qianhai
{
    public partial class ReturnUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //返回网站订单号
                string order_number = RequestTool.RequestSafeString("order_number");
                Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order_number,"qianhai");
                if (pay == null)
                {
                    Response.Write("系统错误");
                    Response.End();
                    return;
                }
                Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
                //返回商户号
                string account = RequestTool.RequestSafeString("account");
                //返回终端号
                string terminal = RequestTool.RequestSafeString("terminal");
                //返回Oceanpayment 的支付唯一号
                string payment_id = RequestTool.RequestSafeString("payment_id");
                //返回网站订单号
                //string order_number     = RequestTool.RequestSafeString("order_number");
                //返回交易币种
                string order_currency = RequestTool.RequestSafeString("order_currency");
                //返回支付金额
                string order_amount = RequestTool.RequestSafeString("order_amount");
                //返回支付状态
                string payment_status = RequestTool.RequestSafeString("payment_status");
                //返回支付详情
                string payment_details = RequestTool.RequestSafeString("payment_details");
                //返回交易安全签名
                string back_signValue = RequestTool.RequestSafeString("signValue");
                //返回备注
                string order_notes = RequestTool.RequestSafeString("order_notes");
                //未通过的风控规则
                string payment_risk = RequestTool.RequestSafeString("payment_risk");
                //获取本地的code值
                string secureCode = pay.UserKey;
                //返回支付信用卡卡号
                string card_number = RequestTool.RequestSafeString("card_number");
                //返回交易类型
                string payment_authType = RequestTool.RequestSafeString("payment_authType");

                string local_signValue = account + terminal + order_number + order_currency + order_amount + order_notes + card_number + payment_id + payment_authType + payment_status + payment_details + payment_risk + secureCode;
                local_signValue = GetSHA256(local_signValue);
                //响应代码
                //$_SESSION['op_errorCode']    = substr($payment_details,0,5);
                //加密串校验
                if (local_signValue.ToLower() == back_signValue.ToLower())
                {

                    if (payment_status == "1")
                    {
                        //支付成功
                        Order.OnlinePaySuccess("qianhai", order_number);
                        Response.Write("OK");
                    }
                    else if (payment_status == "-1")
                    {
                        //待处理
                        Response.Write("支付失败");
                    }
                    else if (payment_status == "0")
                    {
                        //支付失败
                        Response.Write("支付失败");
                    }
                }
                else
                {
                    //校验失败
                    Response.Write("校验失败");
                }

            }
            catch (Exception ex)
            {
                Response.Write("支付失败");

            }
            
        }

        public string GetSHA256(string strData)
        {
            //使用SHA256加密算法：
            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
            byte[] sha256Bytes = System.Text.Encoding.Default.GetBytes(strData);
            byte[] cryString = sha256.ComputeHash(sha256Bytes);
            string sha256Str = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X2");
            }
            return sha256Str;
        }
    }
}
