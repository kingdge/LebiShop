using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace onlinepay.epay95
{
    public partial class PayNotify : System.Web.UI.Page
    {
        //商户号
        public string MerNo;
        //商户网站产生的订单号[唯一]
        public string BillNo;
        //该笔订单总金额,必须为正数，保留两位有效小数。
        public string Amount;
        //支付结果返回码
        public string Succeed;
        //支付状态说明
        public string Result;
        //MD5加密信息
        public string MD5info;
        //商户备注信息
        public string MerRemark;
        protected void Page_Load(object sender, EventArgs e)
        {

            MerNo = System.Web.HttpContext.Current.Request.Params["MerNo"].ToString();
            BillNo = System.Web.HttpContext.Current.Request.Params["BillNo"].ToString();
            Amount = System.Web.HttpContext.Current.Request.Params["Amount"].ToString();
            Succeed = System.Web.HttpContext.Current.Request.Params["Succeed"].ToString();
            Result = System.Web.HttpContext.Current.Request.Params["Result"].ToString();
            MD5info = System.Web.HttpContext.Current.Request.Params["MD5info"].ToString();
            MerRemark = System.Web.HttpContext.Current.Request.Params["MerRemark"].ToString();

            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(BillNo,"95epay");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }

            string MD5key = pay.UserKey;//Md5加密私钥[注册时产生]
            string md5md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(MD5key, "MD5").ToUpper();
            string md5src = "Amount=" + Amount + "&BillNo=" + BillNo + "&MerNo=" + MerNo + "&Succeed=" + Succeed + "&" + md5md5;
            string md5str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5").ToUpper();

            if (MD5info == md5str)
            {
                if (Succeed == "88")
                {
                    //交易成功
                    Order.OnlinePaySuccess("95epay", BillNo, "");
                    Response.Write("success"); 
                }
                else
                {
                    //交易失败
                    Response.Write("交易失败"); 
                }
            }
            else
            {
                //验证失败
                Response.Write("验证失败"); 
            }
        }
    }
}