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
using tenpayApp;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

public partial class payNotifyUrl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string out_trade_no = Request["out_trade_no"];
        //Lebi_Order order = B_Lebi_Order.GetModel(out_trade_no);
        string out_trade_no = Request.QueryString["out_trade_no"];
        Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + out_trade_no + "'}");
        if (order == null)
        {
            Response.Write("系统错误");
            Response.End();
            return;
        }
        TenpayUtil tu = new TenpayUtil(order);
        //创建ResponseHandler实例
        ResponseHandler resHandler = new ResponseHandler(Context);
        resHandler.setKey(tu.tenpay_key);

        //判断签名
        if (resHandler.isTenpaySign())
        {
            ///通知id
            string notify_id = resHandler.getParameter("notify_id");
            //通过通知ID查询，确保通知来至财付通
            //创建查询请求
            RequestHandler queryReq = new RequestHandler(Context);
            queryReq.init();
            queryReq.setKey(tu.tenpay_key);
            queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
            queryReq.setParameter("partner", tu.bargainor_id);
            queryReq.setParameter("notify_id", notify_id);

            //通信对象
            TenpayHttpClient httpClient = new TenpayHttpClient();
            httpClient.setTimeOut(5);
            //设置请求内容
            httpClient.setReqContent(queryReq.getRequestURL());
            //后台调用
            if (httpClient.call())
            {
                //设置结果参数
                ClientResponseHandler queryRes = new ClientResponseHandler();
                queryRes.setContent(httpClient.getResContent());
                queryRes.setKey(tu.tenpay_key);
                //判断签名及结果
                //只有签名正确,retcode为0，trade_state为0才是支付成功
                if (queryRes.isTenpaySign())
                {
                    //取结果参数做业务处理
                    out_trade_no = queryRes.getParameter("out_trade_no");
                    //财付通订单号
                    string transaction_id = queryRes.getParameter("transaction_id");
                    //金额,以分为单位
                    string total_fee = queryRes.getParameter("total_fee");
                    //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                    string discount = queryRes.getParameter("discount");
                    //支付结果
                    string trade_state = resHandler.getParameter("trade_state");
                    //交易模式，1即时到帐 2中介担保
                    string trade_mode = resHandler.getParameter("trade_mode");
                    #region
                    //判断签名及结果
                    if ("0".Equals(queryRes.getParameter("retcode")))
                    {
                        //Response.Write("id验证成功");

                        if ("1".Equals(trade_mode))
                        {       //即时到账 
                            if ("0".Equals(trade_state))
                            {
                                //------------------------------
                                //即时到账处理业务开始
                                //------------------------------

                                //处理数据库逻辑
                                //注意交易单不要重复处理
                                //注意判断返回金额

                                //------------------------------
                                //即时到账处理业务完毕
                                //------------------------------
                                Order.OnlinePaySuccess("tenpayJSDZ", out_trade_no, transaction_id);
                                //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                Response.Write("success");
                            }
                            else
                            {
                                Response.Write("即时到账支付失败");
                            }
                        }
                        else if ("2".Equals(trade_mode))
                        { //中介担保
                            //------------------------------
                            //中介担保处理业务开始
                            //------------------------------
                            //处理数据库逻辑
                            //注意交易单不要重复处理
                            //注意判断返回金额

                            int iStatus = Convert.ToInt32(trade_state);
                            switch (iStatus)
                            {
                                case 0:		//付款成功

                                    break;
                                case 1:		//交易创建

                                    break;
                                case 2:		//收获地址填写完毕

                                    break;
                                case 4:		//卖家发货成功

                                    break;
                                case 5:		//买家收货确认，交易成功

                                    break;
                                case 6:		//交易关闭，未完成超时关闭

                                    break;
                                case 7:		//修改交易价格成功

                                    break;
                                case 8:		//买家发起退款

                                    break;
                                case 9:		//退款成功

                                    break;
                                case 10:	//退款关闭

                                    break;

                            }


                            //------------------------------
                            //中介担保处理业务开始
                            //------------------------------


                            //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                            Response.Write("success");
                        }
                    }
                    else
                    {
                        //错误时，返回结果可能没有签名，写日志trade_state、retcode、retmsg看失败详情。
                        //通知财付通处理失败，需要重新通知
                        Response.Write("查询验证签名失败或id验证失败");
                        Response.Write("retcode:" + queryRes.getParameter("retcode"));
                    }
                    #endregion
                }
                else
                {
                    Response.Write("通知ID查询签名验证失败");
                }
            }
            else
            {
                //通知财付通处理失败，需要重新通知
                Response.Write("后台调用通信失败");
                //写错误日志
                Response.Write("call err:" + httpClient.getErrInfo() + "<br>" + httpClient.getResponseCode() + "<br>");

            }
        }
        else
        {
            Response.Write("签名验证失败");
        }
        Response.End();
    }

}