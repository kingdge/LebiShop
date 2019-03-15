using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using Shop.Bussiness;
namespace weixinpayh5
{
    /// <summary>
    /// 扫码支付模式一回调处理类
    /// 接收微信支付后台发送的扫码结果，调用统一下单接口并将下单结果返回给微信支付后台
    /// </summary>
    public class NativeNotify : Notify
    {
        public NativeNotify(Page page)
            : base(page)
        {

        }

        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();

            //检查openid和product_id是否返回
            if (!notifyData.IsSet("openid") || !notifyData.IsSet("product_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "回调数据异常");
                SystemLog.Add(this.GetType().ToString() + "The data WeChat post is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            //调统一下单接口，获得下单结果
            string openid = notifyData.GetValue("openid").ToString();
            string product_id = notifyData.GetValue("product_id").ToString();
            string Order_Code = notifyData.GetValue("out_trade_no").ToString();
            Lebi_Order order = B_Lebi_Order.GetModel("Code = lbsql{'" + Order_Code + "'}");
            if (order == null)
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL Order");
                res.SetValue("return_msg", "统一下单失败");
                SystemLog.Add(this.GetType().ToString() + "UnifiedOrder failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            WxPayData unifiedOrderResult = new WxPayData();
            try
            {
                unifiedOrderResult = UnifiedOrder(openid, product_id);
            }
            catch (Exception ex)//若在调统一下单接口时抛异常，立即返回结果给微信支付后台
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");
                SystemLog.Add(this.GetType().ToString() + "UnifiedOrder failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            //若下单失败，则立即返回结果给微信支付后台
            if (!unifiedOrderResult.IsSet("appid") || !unifiedOrderResult.IsSet("mch_id") || !unifiedOrderResult.IsSet("prepay_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");
                SystemLog.Add(this.GetType().ToString() + "UnifiedOrder failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            WxPayConfig conf = new WxPayConfig(order);
            //统一下单成功,则返回成功结果给微信支付后台
            WxPayData data = new WxPayData();
            data.SetValue("return_code", "SUCCESS");
            data.SetValue("return_msg", "OK");
            data.SetValue("appid", conf.APPID);
            data.SetValue("mch_id", conf.MCHID);
            data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
            data.SetValue("prepay_id", unifiedOrderResult.GetValue("prepay_id"));
            data.SetValue("result_code", "SUCCESS");
            data.SetValue("err_code_des", "OK");
            data.SetValue("sign", data.MakeSign(order));

            //SystemLog.Add(this.GetType().ToString() + "UnifiedOrder success , send data to WeChat : " + data.ToXml());
            page.Response.Write(data.ToXml());
            page.Response.End();
        }

        private WxPayData UnifiedOrder(string openId, string productId)
        {
            //统一下单
            Lebi_Order order = B_Lebi_Order.GetModel("id = lbsql{" + productId + "}");
            if (order == null)
            {
                throw new WxPayException("订单不存在！");
            }
            WxPayData req = new WxPayData();
            req.SetValue("body", "test");
            req.SetValue("attach", "test");
            req.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo(order));
            req.SetValue("total_fee", 1);
            req.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            req.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            req.SetValue("goods_tag", "test");
            req.SetValue("trade_type", "NATIVE");
            req.SetValue("openid", openId);
            req.SetValue("product_id", productId);
            WxPayData result = WxPayApi.UnifiedOrder(req);
            return result;
        }
    }
}