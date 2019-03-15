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

namespace yinlianqmf
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //OrderTime 订单时间 N 14 M 格式： hhmmss 
            //OrderDate 订单日期 N 8 M 格式：yyyyMMdd 
            //MerOrderId 商户订单号 AN 32 M 
            //TransType 交易类型 AN 32 M 固定值：NoticePay 
            //TransAmt 交易金额 N 12 M 单位分 
            //MerId 商户号 N 32 M 
            //MerTermId 终端号 N 8 M 
            //TransId 银商订单号 AN 32 M 
            //TransState 交易状态 N 2 M 
            //RefId 系统检索号 N 12 M 
            //Account 支付卡号 N 30 M 支付卡号 
            //TransDesc 交易描述 ANS 256 O 
            //LiqDate 清算日期 N 8 M yyyyMMdd 
            //Reserve 备用字段 ANS 256 O 备用字段 
            //Signature 签名数据 ANS 256 M HexStr
            string OrderTime = RequestTool.RequestString("OrderTime");
            string OrderDate = RequestTool.RequestString("OrderDate");
            string MerOrderId = RequestTool.RequestString("MerOrderId");
            string TransType = RequestTool.RequestString("TransType");
            string TransAmt = RequestTool.RequestString("TransAmt");
            string MerId = RequestTool.RequestString("MerId");
            string MerTermId = RequestTool.RequestString("MerTermId");
            string TransId = RequestTool.RequestString("TransId");
            string TransState = RequestTool.RequestString("TransState");
            string RefId = RequestTool.RequestString("RefId");
            string Account = RequestTool.RequestString("Account");
            string TransDesc = RequestTool.RequestString("TransDesc");
            string LiqDate = RequestTool.RequestString("LiqDate");
            string Reserve = RequestTool.RequestString("Reserve");
            string Signature = RequestTool.RequestString("Signature");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("OrderTime", "OrderTime");
            dic.Add("OrderDate", "OrderDate");
            dic.Add("MerOrderId", "MerOrderId");
            dic.Add("TransType", "TransType");
            dic.Add("TransAmt", "TransAmt");
            dic.Add("MerId", "MerId");
            dic.Add("MerTermId", "MerTermId");
            dic.Add("TransId", "TransId");
            dic.Add("TransState", "TransState");
            dic.Add("RefId", "RefId");
            dic.Add("Account", "Account");
            dic.Add("TransDesc", "TransDesc");
            dic.Add("LiqDate", "LiqDate");
            dic.Add("Reserve", "Reserve");
            dic.Add("Signature", "Signature");

            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            Lebi_Order order = B_Lebi_Order.GetModel("Code='" + MerOrderId + "'");
            if (order == null)
            {
                Response.Write("订单错误");
                return;
            }
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "yinlianqmf");
            if (pay == null)
            {
                return;
            }
            UmsPayConfig config = new UmsPayConfig(pay);
            if (CallbackVerify(config, dic))
            {
                Order.OnlinePaySuccess("yinlianqmf", MerOrderId);
            }

        }
        /// <summary>
        /// 验证支付成功后台服务通知
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="inParams">银联传送过来的参数信息</param>
        /// <returns></returns>
        bool CallbackVerify(UmsPayConfig config, Dictionary<string, string> inParams)
        {
            // 组装验签内容信息
            var signField = new string[] { "OrderTime", "OrderDate", "MerOrderId", "TransType", "TransAmt", "MerId", "MerTermId", "TransId", "TransState", "RefId", "Account", "TransDesc", "Reserve" };
            var sbSign = new StringBuilder();
            foreach (var s in signField)
            {
                var r = inParams.Keys.Any(n => String.Equals(n, s.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                if (!r) continue;
                var dict = inParams.FirstOrDefault(n => String.Equals(n.Key, s.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                sbSign.Append(dict.Value);
            }
            // 判断验签是否成功
            var result = RSAUtil.Verify(sbSign.ToString(), inParams["signature"], config.PublicKey);
            if (result)
            {
                Lebi_Order order = B_Lebi_Order.GetModel("Code='" + inParams["MerOrderId"] + "'");
                // 验签成功后，向银联服务发送接收通知消息响应请求
                this.NotifyResponse(config, inParams);
            }
            return result;
        }

        /// <summary>
        /// 将接收到的通知信息向银联服务器响应
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="inParams">银联传送过来的参数信息</param>
        void NotifyResponse(UmsPayConfig config, Dictionary<string, string> inParams)
        {
            try
            {
                var signField = new string[] { "OrderTime", "OrderDate", "MerOrderId", "TransType", "TransAmt", "MerId", "MerTermId", "TransId", "TransState", "RefId", "Account", "TransDesc", "Reserve" };
                var sbSign = new StringBuilder();
                foreach (var s in signField)
                {
                    sbSign.Append(inParams[s]);
                }
                var merSign = RSAUtil.RSASign(sbSign.ToString(), config.PrivateKey);
                var responseObject = new
                {
                    TransCode = "201202",
                    MerOrderId = inParams["MerOrderId"],
                    TransType = "NoticePay",
                    MerId = inParams["MerId"],
                    MerTermId = inParams["MerTermId"],
                    TransId = inParams["TransId"],
                    MerPlatTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    MerOrderState = "00",//00销账成功 11销账失败
                    Reserve = inParams["Reserve"],
                    MerSign = merSign
                };
                var response = HttpContext.Current.Response;
                response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(responseObject));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}