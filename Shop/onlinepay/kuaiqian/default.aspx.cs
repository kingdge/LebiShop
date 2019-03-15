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
namespace onlinepay.kuaiqian
{

    public partial class _Default : ShopPage
    {
        /**
         * @Description: 快钱人民币支付网关接口范例
         * @Copyright (c) 上海快钱信息服务有限公司
         * @version 2.0
         */
        protected void Page_Load(Object sender, EventArgs E)
        {
            

            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("ERROR");
                Response.End();
                return;
            }
            Lebi_Language order_language = B_Lebi_Language.GetModel(order.Language_id);
            if (order.IsPaid == 1)
            {
                Response.Write(Language.Tag("已付款", order_language.Code));
                Response.End();
                return;
            }
            order.Site_id_pay = CurrentSite.id;
            order.Language_id = CurrentLanguage.id;
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order,"kuaiqian");
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
                order.Money_OnlinepayFee = order.Money_Pay * pay.FeeRate/100;
            }
            if (order.OnlinePay_id != pay.id)
            {
                order.OnlinePay_id = pay.id;
                order.OnlinePay_Code = pay.Code;
                order.OnlinePay = pay.Name;
            }
            B_Lebi_Order.Update(order);
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            //人民币网关账户号
            ///请登录快钱系统获取用户编号，用户编号后加01即为人民币网关账户号。
            merchantAcctId.Value = pay.UserName;

            //人民币网关密钥
            ///区分大小写.请与快钱联系索取
            String key = pay.UserKey;

            //字符集.固定选择值。可为空。
            ///只能选择1、2、3.
            ///1代表UTF-8; 2代表GBK; 3代表gb2312
            ///默认值为1
            inputCharset.Value = "1";

            //接受支付结果的页面地址.与[bgUrl]不能同时为空。必须是绝对地址。
            ///如果[bgUrl]为空，快钱将支付结果Post到[pageUrl]对应的地址。
            ///如果[bgUrl]不为空，并且[bgUrl]页面指定的<redirecturl>地址不为空，则转向到<redirecturl>对应的地址
            pageUrl.Value = "";

            //服务器接受支付结果的后台地址.与[pageUrl]不能同时为空。必须是绝对地址。
            ///快钱通过服务器连接的方式将交易结果发送到[bgUrl]对应的页面地址，在商户处理完成后输出的<result>如果为1，页面会转向到<redirecturl>对应的地址。
            ///如果快钱未接收到<redirecturl>对应的地址，快钱将把支付结果post到[pageUrl]对应的页面。
            //bgUrl.Value = "http://www.yoursite.com/receive.aspx";
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            bgUrl.Value = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/kuaiqian/receive.aspx";
            //网关版本.固定值
            ///快钱会根据版本号来调用对应的接口处理程序。
            ///本代码版本号固定为v2.0
            version.Value = "v2.0";

            //语言种类.固定选择值。
            ///只能选择1、2、3
            ///1代表中文；2代表英文
            ///默认值为1
            language.Value = "1";

            //签名类型.固定值
            ///1代表MD5签名
            ///当前版本固定为1
            signType.Value = "1";

            //支付人姓名
            ///可为中文或英文字符
            payerName.Value = order.User_UserName;

            //支付人联系方式类型.固定选择值
            ///只能选择1
            ///1代表Email
            payerContactType.Value = "1";

            //支付人联系方式
            ///只能选择Email或手机号
            payerContact.Value = order.T_Email;

            //商户订单号
            ///由字母、数字、或[-][_]组成
            orderId.Value = order.Code;

            //订单金额
            ///以分为单位，必须是整型数字
            ///比方2，代表0.02元
            orderAmount.Value = Convert.ToInt32(order.Money_Pay * currendy.ExchangeRate * 100 * (1 + (pay.FeeRate / 100))).ToString();

            //订单提交时间
            ///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
            ///如；20080101010101
            orderTime.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //商品名称
            ///可为中文或英文字符
            productName.Value = order.Code;

            //商品数量
            ///可为空，非空时必须为数字
            productNum.Value = "1";

            //商品代码
            ///可为字符或者数字
            productId.Value = "";

            //商品描述
            productDesc.Value = "";

            //扩展字段1
            ///在支付结束后原样返回给商户
            ext1.Value = "";

            //扩展字段2
            ///在支付结束后原样返回给商户
            ext2.Value = "";

            //支付方式.固定选择值
            ///只能选择00、10、11、12、13、14
            ///00：组合支付（网关支付页面显示快钱支持的各种支付方式，推荐使用）10：银行卡支付（网关支付页面只显示银行卡支付）.11：电话银行支付（网关支付页面只显示电话支付）.12：快钱账户支付（网关支付页面只显示快钱账户支付）.13：线下支付（网关支付页面只显示线下支付方式）.14：B2B支付（网关支付页面只显示B2B支付，但需要向快钱申请开通才能使用）
            payType.Value = "00";

            //银行代码
            ///实现直接跳转到银行页面去支付,只在payType=10时才需设置参数
            ///具体代码参见 接口文档银行代码列表
            bankId.Value = "";

            //同一订单禁止重复提交标志
            ///固定选择值： 1、0
            ///1代表同一订单号只允许提交1次；0表示同一订单号在没有支付成功的前提下可重复提交多次。默认为0建议实物购物车结算类商户采用0；虚拟产品类商户采用1
            redoFlag.Value = "0";

            //快钱的合作伙伴的账户号
            ///如未和快钱签订代理合作协议，不需要填写本参数
            pid.Value = "";


            //生成加密签名串
            ///请务必按照如下顺序和规则组成加密串！
            String signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset.Value);
            signMsgVal = appendParam(signMsgVal, "pageUrl", pageUrl.Value);
            signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl.Value);
            signMsgVal = appendParam(signMsgVal, "version", version.Value);
            signMsgVal = appendParam(signMsgVal, "language", language.Value);
            signMsgVal = appendParam(signMsgVal, "signType", signType.Value);
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId.Value);
            signMsgVal = appendParam(signMsgVal, "payerName", payerName.Value);
            signMsgVal = appendParam(signMsgVal, "payerContactType", payerContactType.Value);
            signMsgVal = appendParam(signMsgVal, "payerContact", payerContact.Value);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId.Value);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount.Value);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime.Value);
            signMsgVal = appendParam(signMsgVal, "productName", productName.Value);
            signMsgVal = appendParam(signMsgVal, "productNum", productNum.Value);
            signMsgVal = appendParam(signMsgVal, "productId", productId.Value);
            signMsgVal = appendParam(signMsgVal, "productDesc", productDesc.Value);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1.Value);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2.Value);
            signMsgVal = appendParam(signMsgVal, "payType", payType.Value);
            signMsgVal = appendParam(signMsgVal, "bankId", bankId.Value);
            signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag.Value);
            signMsgVal = appendParam(signMsgVal, "pid", pid.Value);
            signMsgVal = appendParam(signMsgVal, "key", key);

            //如果在web.config文件中设置了编码方式，例如<globalization requestEncoding="utf-8" responseEncoding="utf-8"/>（如未设则默认为utf-8），
            //那么，inputCharset的取值应与已设置的编码方式相一致；
            //同时，GetMD5()方法中所传递的编码方式也必须与此保持一致。
            signMsg.Value = GetMD5(signMsgVal, "utf-8").ToUpper();


            //?
            //Lab_orderId.Text = orderId.Value;
            //Lab_orderAmount.Text = orderAmount.Value;
            //Lab_payerName.Text = payerName.Value;
            //Lab_productName.Text = productName.Value;

        }


        //功能函数。将变量值不为空的参数组成字符串
        String appendParam(String returnStr, String paramId, String paramValue)
        {

            if (returnStr != "")
            {

                if (paramValue != "")
                {

                    returnStr += "&" + paramId + "=" + paramValue;
                }

            }
            else
            {

                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }

            return returnStr;
        }
        //功能函数。将变量值不为空的参数组成字符串。结束



        //功能函数。将字符串进行编码格式转换，并进行MD5加密，然后返回。开始
        private static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        //功能函数。将字符串进行编码格式转换，并进行MD5加密，然后返回。结束
    }
}