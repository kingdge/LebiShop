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

    public partial class receive : System.Web.UI.Page
    {

        /**
         * @Description: 快钱人民币支付网关接口范例
         * @Copyright (c) 上海快钱信息服务有限公司
         * @version 2.0
         */
        //初始化结果及地址
        public int rtnOk = 0;
        public string rtnUrl = "";
        protected void Page_Load(Object sender, EventArgs E)
        {
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(Request["orderId"].ToString().Trim(),"kuaiqian");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }

            //获取人民币网关账户号
            String merchantAcctId = Request["merchantAcctId"].ToString().Trim();

            //设置人民币网关密钥
            ///区分大小写
            String key = pay.UserKey;

            //获取网关版本.固定值
            ///快钱会根据版本号来调用对应的接口处理程序。
            ///本代码版本号固定为v2.0
            String version = Request["version"].ToString().Trim();

            //获取语言种类.固定选择值。
            ///只能选择1、2、3
            ///1代表中文；2代表英文
            ///默认值为1
            String language = Request["language"].ToString().Trim();

            //签名类型.固定值
            ///1代表MD5签名
            ///当前版本固定为1
            String signType = Request["signType"].ToString().Trim();

            //获取支付方式
            ///值为：10、11、12、13、14
            ///00：组合支付（网关支付页面显示快钱支持的各种支付方式，推荐使用）10：银行卡支付（网关支付页面只显示银行卡支付）.11：电话银行支付（网关支付页面只显示电话支付）.12：快钱账户支付（网关支付页面只显示快钱账户支付）.13：线下支付（网关支付页面只显示线下支付方式）.14：B2B支付（网关支付页面只显示B2B支付，但需要向快钱申请开通才能使用）
            String payType = Request["payType"].ToString().Trim();

            //获取银行代码
            ///参见银行代码列表
            String bankId = Request["bankId"].ToString().Trim();

            //获取商户订单号
            String orderId = Request["orderId"].ToString().Trim();

            //获取订单提交时间
            ///获取商户提交订单时的时间.14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
            ///如：20080101010101
            String orderTime = Request["orderTime"].ToString().Trim();

            //获取原始订单金额
            ///订单提交到快钱时的金额，单位为分。
            ///比方2 ，代表0.02元
            String orderAmount = Request["orderAmount"].ToString().Trim();

            //获取快钱交易号
            ///获取该交易在快钱的交易号
            String dealId = Request["dealId"].ToString().Trim();

            //获取银行交易号
            ///如果使用银行卡支付时，在银行的交易号。如不是通过银行支付，则为空
            String bankDealId = Request["bankDealId"].ToString().Trim();

            //获取在快钱交易时间
            ///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
            ///如；20080101010101
            String dealTime = Request["dealTime"].ToString().Trim();

            //获取实际支付金额
            ///单位为分
            ///比方 2 ，代表0.02元
            String payAmount = Request["payAmount"].ToString().Trim();

            //获取交易手续费
            ///单位为分
            ///比方 2 ，代表0.02元
            String fee = Request["fee"].ToString().Trim();

            //获取扩展字段1
            String ext1 = Request["ext1"].ToString().Trim();

            //获取扩展字段2
            String ext2 = Request["ext2"].ToString().Trim();

            //获取处理结果
            ///10代表 成功; 11代表 失败
            String payResult = Request["payResult"].ToString().Trim();

            //获取错误代码
            ///详细见文档错误代码列表
            String errCode = Request["errCode"].ToString().Trim();

            //获取加密签名串
            String signMsg = Request["signMsg"].ToString().Trim();



            //生成加密串。必须保持如下顺序。
            String merchantSignMsgVal = "";
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "merchantAcctId", merchantAcctId);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "version", version);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "language", language);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "signType", signType);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "payType", payType);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "bankId", bankId);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderId", orderId);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderTime", orderTime);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderAmount", orderAmount);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "dealId", dealId);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "bankDealId", bankDealId);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "dealTime", dealTime);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "payAmount", payAmount);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "fee", fee);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "ext1", ext1);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "ext2", ext2);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "payResult", payResult);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "errCode", errCode);
            merchantSignMsgVal = appendParam(merchantSignMsgVal, "key", key);

            //如果在web.config文件中设置了编码方式，例如<globalization requestEncoding="utf-8" responseEncoding="utf-8"/>（如未设则默认为utf-8），
            //那么，GetMD5()方法中所传递的编码方式也必须与此保持一致。
            String merchantSignMsg = GetMD5(merchantSignMsgVal, "utf-8");
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            //商家进行数据处理，并跳转会商家显示支付结果的页面
            ///首先进行签名字符串验证
            if (signMsg.ToUpper() == merchantSignMsg.ToUpper())
            {

                switch (payResult)
                {

                    case "10":
                        /*  
                         ' 商户网站逻辑处理，比方更新订单支付状态为成功
                        ' 特别注意：只有signMsg.ToUpper() == merchantSignMsg.ToUpper()，且payResult=10，才表示支付成功！同时将订单金额与提交订单前的订单金额进行对比校验。
                        */
                        Order.OnlinePaySuccess("kuaiqian", orderId, dealId,false);
                        //报告给快钱处理结果，并提供将要重定向的地址。
                        rtnOk = 1;
                        rtnUrl = site.WebPath + "/user/Orders.aspx";
                        break;

                    default:

                        rtnOk = 1;
                        //rtnUrl = "http://www.yoursite.com/show.aspx?msg=false";
                        rtnUrl = site.WebPath + "/user/Orders.aspx";
                        break;
                }

            }
            else
            {
                rtnOk = 1;
                //rtnUrl = "http://www.yoursite.com/show.aspx?msg=error";
                rtnUrl = site.WebPath + "/user/Orders.aspx";

            }


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

        //以下报告给快钱处理结果，并提供将要重定向的地址
    }
}