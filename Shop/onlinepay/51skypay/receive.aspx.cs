using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
namespace onlinepay.epay
{
    public partial class receive : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string mac;
                ROrder order = new ROrder();
                order.ver = epayCommon.UnToBase64(Request.Form["ver"].Trim());
                order.mrch_no = epayCommon.UnToBase64(Request.Form["mrch_no"].Trim());
                order.ord_no = epayCommon.UnToBase64(Request.Form["ord_no"].Trim());
                order.ord_amt = decimal.Parse(epayCommon.UnToBase64(Request.Form["ord_amt"].Trim()));
                order.ord_date = epayCommon.UnToBase64(Request.Form["ord_date"].Trim());
                order.ord_seq = epayCommon.UnToBase64(Request.Form["ord_seq"].Trim());
                order.sno = epayCommon.UnToBase64(Request.Form["sno"].Trim());
                order.ord_status = epayCommon.UnToBase64(Request.Form["ord_status"].Trim());
                order.ord_result = epayCommon.UnToBase64(Request.Form["ord_result"].Trim());
                mac = Request.Form["mac"].Trim();
                order.add_msg = epayCommon.UnToBase64(Request.Form["add_msg"].Trim());
                //Log.Add(order.ord_no + "|" + mac + "|" + order.mac + "|" + order.sno + "|" + order.ord_seq + "|" + order.mrch_no);
                if (mac == order.mac)
                {
                    Order.OnlinePaySuccess(order.ord_no, order.mrch_no);
                    Response.Write("成功");
                }
                else
                {
                    Response.Write("失败");
                }
            }
            catch (Exception ex)
            {
                Response.Write("失败");
                Response.End();
            }
        }
    }
}