using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
namespace weixinpay
{
    //=================================
    //原生支付获取订单信息返回的xml
    //=================================
    public partial class nativecall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sp_billno = Request["order_no"];
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");
            //订单号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            string out_trade_no = "" + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request["order_no"].ToString();
            }

            sp_billno = TenpayUtil.partner + sp_billno;



            //创建RequestHandler实例
            RequestHandler packageReqHandler = new RequestHandler(Context);
            //初始化
            packageReqHandler.init();
            packageReqHandler.setKey(TenpayUtil.key);

            //设置package订单参数
            packageReqHandler.setParameter("partner", TenpayUtil.partner);		  //商户号
            packageReqHandler.setParameter("bank_type", "WX");		                      //银行类型
            packageReqHandler.setParameter("fee_type", "1");                    //币种，1人民币
            packageReqHandler.setParameter("input_charset", "GBK");
            packageReqHandler.setParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.setParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.setParameter("notify_url", TenpayUtil.tenpay_notify);		    //接收财付通通知的URL
            packageReqHandler.setParameter("body", "nativecall");	                    //商品描述
            packageReqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP

            //获取package包
            string packageValue = packageReqHandler.getRequestURL();

            //调起微信支付签名
            string timeStamp = TenpayUtil.getTimestamp();
            string nonceStr = TenpayUtil.getNoncestr();

            //设置支付参数
            RequestHandler payHandler = new RequestHandler(Context);
            payHandler.setParameter("appid", TenpayUtil.appid);
            payHandler.setParameter("noncestr", nonceStr);
            payHandler.setParameter("timestamp", timeStamp);
            payHandler.setParameter("package", packageValue);
            payHandler.setParameter("RetCode", "0");
            payHandler.setParameter("RetErrMsg", "成功");
            string paySign = payHandler.createSHA1Sign();
            payHandler.setParameter("app_signature", paySign);
            payHandler.setParameter("sign_method", "SHA1");


            Response.ContentType = "text/xml";
            Response.Clear();
            Response.Write(payHandler.parseXML());

        }
    }
}