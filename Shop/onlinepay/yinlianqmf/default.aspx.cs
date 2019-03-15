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
    public partial class _Default : ShopPage
    {
        public PayMessage msg;
        public UmsPayConfig config;
        public string returnurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Shop.Bussiness.Site site = new Shop.Bussiness.Site();
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
                Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "yinlianqmf");
                if (pay == null)
                {
                    Response.Write(Language.Tag("系统错误", language.Code));
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
                config = new UmsPayConfig(pay);
                Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
                //string notify_url = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/pinpay/notify_url.aspx";


                Lebi_Site lebisite = B_Lebi_Site.GetModel(order.Site_id_pay);
                if (site == null)
                    returnurl = Shop.Bussiness.Site.Instance.WebPath;
                else
                {
                    Lebi_Language lang = B_Lebi_Language.GetModel(order.Language_id);
                    string path = "";
                    if (lang != null)
                        path = lang.Path.TrimEnd('/');
                    if (lebisite.Domain == "")
                        returnurl = Shop.Bussiness.Site.Instance.WebPath.TrimEnd('/') + lebisite.Path.TrimEnd('/') + path;
                    else
                    {
                        returnurl = Shop.Bussiness.Site.Instance.WebPath.TrimEnd('/') + path;
                    }
                }


                returnurl = "http://" + RequestTool.GetRequestDomain() + returnurl.TrimEnd('/') + "/user/OrderDetails.aspx?id=" + order.id;
                decimal order_amount1 = order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100));
                msg = Payment(config, order, order_amount1);

            }
            finally
            {

            }
        }


        /// <summary>
        /// 付款操作
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="productName">商品名称</param>
        /// <param name="amount">支付金额</param>
        /// <returns></returns>
        PayMessage Payment(UmsPayConfig config, Lebi_Order order, decimal amount)
        {
            var message = new PayMessage() { IsSuccess = true };

            var now = DateTime.Now;
            // 参数组装
            var inParams = new Dictionary<string, string>
            {
                {"TransCode", "201201"},
                {"OrderTime", now.ToString("HHmmss")},
                {"OrderDate", now.ToString("yyyyMMdd")},
                {"MerOrderId", order.Code},
                {"TransType", "NoticePay"},
                {"TransAmt", (amount*100).ToString("F0")},
                {"MerId", config.MerId},
                {"MerTermId", config.MerTermId},
                {"NotifyUrl", config.NotifyUrl},
                {"Reserve", order.Code},
                {"OrderDesc", order.Code},
                {"EffectiveTime", "0"}
            };
            var signContent = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", inParams["OrderTime"], inParams["EffectiveTime"], inParams["OrderDate"], inParams["MerOrderId"], inParams["TransType"], inParams["TransAmt"], inParams["MerId"], inParams["MerTermId"], inParams["NotifyUrl"], inParams["Reserve"], inParams["OrderDesc"]);
            var merSign = RSAUtil.RSASign(signContent, config.PrivateKey);
            inParams.Add("MerSign", merSign);
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);

            //client.PostingData.Add("jsonString", jsonString);
            //var result = client.GetString();
            System.Collections.Specialized.NameValueCollection nv = new System.Collections.Specialized.NameValueCollection();
            nv.Add("jsonString", jsonString);
            string result = HtmlEngine.Post(config.OrderUrl, nv);
            if (string.IsNullOrEmpty(result))
            {
                message.IsSuccess = false;
                message.Msg = "调用支付下单接口无数据返回。";
                return message;
            }
            var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            // 判断是否下单成功
            var respCode = dictionary["RespCode"];
            if (respCode != "00000")
            {
                message.IsSuccess = false;
                message.Msg = dictionary["RespMsg"];
                return message;
            }
            // 组装验签字符串
            var content = string.Format("{0}{1}{2}{3}{4}{5}", dictionary["MerOrderId"], dictionary["ChrCode"],
                          dictionary["TransId"], dictionary["Reserve"].Trim(), dictionary["RespCode"], dictionary["RespMsg"].Trim());
            var r = RSAUtil.Verify(content, dictionary["Signature"], config.PublicKey);
            if (!r)
            {
                message.IsSuccess = false;
                message.Msg = "下单成功，返回数据签名验证失败。";
                return message;
            }
            var chrCode = dictionary["ChrCode"];
            var transId = dictionary["TransId"];
            inParams.Add("ChrCode", chrCode);
            inParams.Add("TransId", transId);
            merSign = RSAUtil.RSASign(string.Format("{0}{1}", transId, chrCode), config.PrivateKey);
            inParams["MerSign"] = merSign;
            // 处理各个支付渠道对应的订单编码
            // 以下处理是根据自己业务处理，添加业务订单号
            if (!inParams.ContainsKey("OrderId"))
                inParams.Add("OrderId", inParams["MerOrderId"]);
            var data = new Dictionary<string, string>
            {
                {"MerSign",merSign},
                {"ChrCode", chrCode},
                {"TransId",transId},
                {"MerchantId",config.MerId}
            };
            message.Data = data;
            message.OtherData = inParams;
            return message;
        }

        /// <summary>
        /// 订单查询
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="inParams">查询参数集合（k1:v1,k2:v2）</param>
        /// <returns></returns>
        //public override PayMessage QuerySingleOrder(Dictionary<string, string> inParams)
        //{
        //    var message = new PayMessage() { IsSuccess = true };
        //    var now = DateTime.Now;
        //    var dict = new Dictionary<string, string>
        //    {
        //        {"TransCode", "201203"},
        //        {"ReqTime", now.ToString("yyyyMMddHHmmss")},
        //        {"OrderDate", inParams["OrderDate"]},
        //        {"MerOrderId", inParams["MerOrderId"]},
        //        {"TransId", inParams["TransId"]},
        //        {"MerId", UnionPayConfig.MerId},
        //        {"MerTermId", UnionPayConfig.MerTermId},
        //        {"Reserve", inParams["Reserve"]}
        //    };
        //    var content = string.Format("{0}{1}{2}{3}{4}{5}{6}", dict["ReqTime"], dict["OrderDate"], dict["MerOrderId"], dict["TransId"], dict["MerId"], dict["MerTermId"], dict["Reserve"]);
        //    var sign = RSAUtils.RsaSign(content, UnionPayConfig.PrivateKey);
        //    dict.Add("MerSign", sign);
        //    var client = new HttpClient(UnionPayConfig.QueryOrderUrl);
        //    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
        //    client.PostingData.Add("jsonString", jsonString);
        //    var result = client.GetString();
        //    if (string.IsNullOrEmpty(result))
        //    {
        //        message.IsSuccess = false;
        //        message.Msg = "调用支付查询订单接口无数据返回。";
        //        return message;
        //    }
        //    var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
        //    var respCode = dictionary["RespCode"];
        //    // 判断是否下单成功
        //    if (respCode != "00000")
        //    {
        //        message.IsSuccess = false;
        //        message.Msg = dictionary["RespMsg"];
        //        return message;
        //    }
        //    var transState = dictionary["TransState"];
        //    if (transState != "1")
        //    {
        //        message.IsSuccess = false;
        //        message.Msg = string.Format("订单查询成功，银联订单系统返回交易状态为：【{0}】，交易状态说明（0:新订单 1:付款成功 2:付款失败 3:支付中）", transState);
        //        return message;
        //    }
        //    // 验签（RefId：系统检索号 支付成功会存在）
        //    // 组装验签字符串
        //    var verifySignContent = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}", dictionary["OrderTime"], dictionary["OrderDate"], dictionary["MerOrderId"], dictionary["TransType"], dictionary["TransAmt"], dictionary["MerId"], dictionary["MerTermId"], dictionary["TransId"], dictionary["TransState"], dictionary["RefId"], dictionary["Reserve"], dictionary["RespCode"], dictionary["RespMsg"]);
        //    var r = RSAUtils.Verify(verifySignContent, dictionary["Signature"], UnionPayConfig.PublicKey);
        //    if (!r)
        //    {
        //        message.IsSuccess = false;
        //        message.Msg = "订单查询成功，返回数据签名验证失败。";
        //        return message;
        //    }
        //    message.Data = dictionary;
        //    return message;
        //}
    }
}