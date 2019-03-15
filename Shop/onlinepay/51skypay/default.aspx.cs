using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
namespace onlinepay.epay
{
    public partial class _default : System.Web.UI.Page
    {
        public SendOrder sorder = new SendOrder();

        protected void Page_Load(object sender, EventArgs e)
        {
            Lebi_OnlinePay pay = B_Lebi_OnlinePay.GetModel("Code='95epay'");
            if (pay == null)
            {
                Log.Add("在线支付接口 95epay 配置错误");
                Response.End();
                return;
            }
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("订单错误");
                Response.End();
                return;
            }

            sorder.ord_no = order.Code; //订单编号
            sorder.ord_amt = order.Money_Pay * currendy.ExchangeRate; //订单金额
            sorder.ord_date = DateTime.Now.ToString("yyyyMMdd"); //交易日期时间
            sorder.ver = "01";
            sorder.mrch_no = pay.UserName;
        }
    }
}