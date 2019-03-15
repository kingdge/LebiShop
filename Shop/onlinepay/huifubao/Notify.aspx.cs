using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Text;

namespace huifubao
{

    public partial class Notify : System.Web.UI.Page
    {
        protected string result = "";
        protected string pay_message = "";
        protected string agent_id = "";
        protected string jnet_bill_no = "";
        protected string agent_bill_id = "";
        protected string pay_type = "";
        protected string pay_amt = "";
        protected string remark = "";
        protected string returnSign = "";
        protected string sign = "";
        Lebi_Order order;
        public Lebi_OnlinePay pay;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 获取参数值
            result = Request["result"];
            pay_message = Request["pay_message"];
            agent_id = Request["agent_id"];
            jnet_bill_no = Request["jnet_bill_no"];
            agent_bill_id = Request["agent_bill_id"];
            pay_type = Request["pay_type"];
            pay_amt = Request["pay_amt"];
            remark = Request["remark"];
            returnSign = Request["sign"];
            #endregion
            order = B_Lebi_Order.GetModel("Code=lbsql{'" + agent_bill_id + "'}");
            if (order == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            pay = Shop.Bussiness.Money.GetOnlinePay(order, "huifubao");
            sign = GetSign();
            //比较MD5签名结果 是否相等
            if (sign.Equals(returnSign))
            {
                Order.OnlinePaySuccess("huifubao", agent_bill_id);
                Response.Write("ok");
            }
            else
            {
                Response.Write("error");
            }
            Response.End();
        }

        /// <summary>
        /// 获取sign的值
        /// 具体的字符串拼接说明，请参考汇元网公司汇付宝支付网关商户开发指南2.0.2.doc
        /// </summary>
        /// <returns></returns>
        private string GetSign()
        {
            StringBuilder sbSign = new StringBuilder();
            sbSign.Append("result=" + result)
                .Append("&agent_id=" + agent_id)
                .Append("&jnet_bill_no=" + jnet_bill_no)
                .Append("&agent_bill_id=" + agent_bill_id)
                .Append("&pay_type=" + pay_type)
                .Append("&pay_amt=" + pay_amt)
                .Append("&remark=" + remark)
                .Append("&key=" + pay.UserKey);

            return this.MD5Hash(sbSign.ToString()).ToLower();
        }

        /// <summary>
        /// 32 位
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns></returns>
        private string MD5Hash(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToLower();
        }
    }
}
