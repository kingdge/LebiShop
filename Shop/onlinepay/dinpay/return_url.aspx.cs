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
using System.Net;
using System.IO;
/* *
 功能：智付页面跳转同步通知页面
 版本：3.0
 日期：2013-08-01
 说明：
 以下代码仅为了方便商户安装接口而提供的样例具体说明以文档为准，商户可以根据自己网站的需要，按照技术文档编写。
 * */
namespace dinpay
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取智付反馈信息
            //商户号
            string merchant_code = RequestTool.RequestString("merchant_code");

            //通知类型
            string notify_type = RequestTool.RequestString("notify_type");

            //通知校验ID
            string notify_id = RequestTool.RequestString("notify_id");

            //接口版本
            string interface_version = RequestTool.RequestString("interface_version");

            //签名方式
            string sign_type = RequestTool.RequestString("sign_type");

            //签名
            string dinpaySign = RequestTool.RequestString("sign");

            //商家订单号
            string order_no = RequestTool.RequestString("order_no");

            //商家订单时间
            string order_time = RequestTool.RequestString("order_time");

            //商家订单金额
            string order_amount = RequestTool.RequestString("order_amount");

            //回传参数
            string extra_return_param = RequestTool.RequestString("extra_return_param");

            //智付交易定单号
            string trade_no = RequestTool.RequestString("trade_no");

            //智付交易时间
            string trade_time = RequestTool.RequestString("trade_time");

            //交易状态 SUCCESS 成功  FAILED 失败
            string trade_status = RequestTool.RequestString("trade_status");

            //银行交易流水号
            string bank_seq_no = RequestTool.RequestString("bank_seq_no");

            /**
             *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
            *同时将商家支付密钥key放在最后参与签名，组成规则如下：
            *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n&key=key值
            **/


            //组织订单信息
            string signStr = "";

            if (null != bank_seq_no && bank_seq_no != "")
            {
                signStr = signStr + "bank_seq_no=" + bank_seq_no.ToString().Trim() + "&";
            }

            if (null != extra_return_param && extra_return_param != "")
            {
                signStr = signStr + "extra_return_param=" + extra_return_param + "&";
            }
            signStr = signStr + "interface_version=V3.0" + "&";
            signStr = signStr + "merchant_code=" + merchant_code + "&";


            if (null != notify_id && notify_id != "")
            {
                signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
            }

            signStr = signStr + "order_amount=" + order_amount + "&";
            signStr = signStr + "order_no=" + order_no + "&";
            signStr = signStr + "order_time=" + order_time + "&";
            signStr = signStr + "trade_no=" + trade_no + "&";
            signStr = signStr + "trade_status=" + trade_status + "&";

            if (null != trade_time && trade_time != "")
            {
                signStr = signStr + "trade_time=" + trade_time + "&";
            }
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order_no,"dinpay");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }

            string key = pay.UserKey;

            signStr = signStr + "key=" + key;
            string signInfo = signStr;

            //将组装好的信息MD5签名
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(signInfo, "md5").ToLower(); //注意与支付签名不同  此处对String进行加密

            //比较智付返回的签名串与商家这边组装的签名串是否一致
            if (dinpaySign == sign)
            {
                //验签成功   
                Order.OnlinePaySuccess("dinpay", order_no, trade_no);
            }
            else
            {
                //验签失败 业务结束
            }
            
        }
    }
}