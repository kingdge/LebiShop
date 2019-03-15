using System;
using System.Collections.Generic;
using System.Web;
using LB.Tools;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
namespace weixinpayh5
{
    public class NativePay
    {
        /**
        * 生成扫描支付模式一URL
        * @param productId 商品ID
        * @return 模式一URL
        */
        public static string GetPrePayUrl(string productId)
        {
            Lebi_Order order = B_Lebi_Order.GetModel("id = lbsql{" + productId + "}");
            if (order == null)
            {
                throw new WxPayException("订单不存在！");
                SystemLog.Add("weixinpay-NativePay" + "productId : " + productId);
            }
            WxPayConfig conf = new WxPayConfig(order);
            WxPayData data = new WxPayData();
            data.SetValue("appid", conf.APPID);//公众帐号id
            data.SetValue("mch_id", conf.MCHID);//商户号
            data.SetValue("time_stamp", TenpayUtil.getTimestamp());//时间戳
            data.SetValue("nonce_str", TenpayUtil.getNoncestr());//随机字符串
            data.SetValue("product_id", productId);//商品ID
            data.SetValue("sign", data.MakeSign(order));//签名
            string str = ToUrlParams(data.GetValues());//转换为URL串
            string url = "weixin://wxpay/bizpayurl?" + str;

            return url;
        }

        /**
        * 生成直接支付url，支付url有效期为2小时,模式二
        * @param productId 商品ID
        * @return 模式二URL
        */
        public static string GetPayUrl(string productId)
        {
            //Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            Lebi_Order order = B_Lebi_Order.GetModel("id=" + productId);
            if (order == null)
                return "";
            Lebi_OnlinePay pay = B_Lebi_OnlinePay.GetModel("id = "+ order.OnlinePay_id +" and Code='weixinpayh5'");
            if (pay == null)
            {
                Log.Add("在线支付接口 weixinpay 配置错误");
                return "";
            }
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            if (pay.FeeRate > 0)
            {
                order.Money_OnlinepayFee = order.Money_Pay * pay.FeeRate / 100;
            }
            string order_price = (order.Money_Pay * currendy.ExchangeRate * 100 * (1 + (pay.FeeRate / 100))).ToString("0");


            WxPayData data = new WxPayData();
            data.SetValue("body", "订单号：" + order.Code);//商品描述
            data.SetValue("attach", "");//附加数据
            data.SetValue("out_trade_no", order.Code);
            data.SetValue("total_fee", order_price);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", "订单号：" + order.Code);//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", productId);//商品ID
            try
            {
                WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
                string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
                //Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
                return url;
            }
            catch (System.NullReferenceException)
            {
                return "";
            }
            
        }

        /**
        * 参数数组转换为url格式
        * @param map 参数名与参数值的映射表
        * @return URL字符串
        */
        private static string ToUrlParams(SortedDictionary<string, object> map)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in map)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}