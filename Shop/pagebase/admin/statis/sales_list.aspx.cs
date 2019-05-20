using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.statis
{
    public partial class sales_list : AdminAjaxBase
    {
        protected string id;
        protected int dateType;
        protected string dateFrom;
        protected string dateTo;
        protected int Pay_id;
        protected int Transport_id;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("statis_sales", "销售报表"))
            {
                NewPageNoPower();
            }
            dateType = RequestTool.RequestInt("dateType", 0);
            Pay_id = RequestTool.RequestInt("Pay_id", 0);
            Transport_id = RequestTool.RequestInt("Transport_id", 0);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            //if (dateFrom == "")
            //    dateFrom = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            //if (dateTo == "")
            //    dateTo = System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            string where = "Type_id_OrderType=211 and IsPaid = 1";
            if (dateType == 0) { 
                where += " and Time_Add>='" + dateFrom + "'and Time_Add<='" + dateTo + " 23:59:59'";
            }
            else if (dateType == 1) { 
                where += " and Time_Paid>='" + dateFrom + "'and Time_Paid<='" + dateTo + " 23:59:59'";
            }
            else
            {
                where += " and Time_Shipped>='" + dateFrom + "'and Time_Shipped<='" + dateTo + " 23:59:59'";
            }
            if (Pay_id > 0)
                where += " and Pay_id = " + Pay_id;
            if (Transport_id > 0)
                where += " and Transport_id = " + Transport_id;
            pros = B_Lebi_Order_Product.GetList("Order_id in (select id from Lebi_Order where " + where + ")", "Time_Add desc");
        }
    }
}