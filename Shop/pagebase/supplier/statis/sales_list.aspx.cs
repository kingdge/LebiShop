using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Statis
{
    public partial class Statis_Sales_List : SupplierAjaxBase
    {
        protected string id;
        protected DateTime dateFrom;
        protected DateTime dateTo;
        protected int Pay_id;
        protected int Transport_id;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_statis", "数据统计"))
            {
                NewPageNoPower();
            }
            Pay_id = RequestTool.RequestInt("Pay_id", 0);
            Transport_id = RequestTool.RequestInt("Transport_id", 0);
            dateFrom = RequestTool.RequestDate("dateFrom");
            dateTo = RequestTool.RequestDate("dateTo");
            //dateFrom = RequestTool.RequestString("dateFrom");
            //if (dateFrom == "")
            //    dateFrom = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            //dateTo = RequestTool.RequestString("dateTo");
            //if (dateTo == "")
            //    dateTo = System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            string where = "Supplier_id = " + CurrentSupplier.id + " and (Type_id_OrderType = 211 or Type_id_OrderType = 213) and IsPaid = 1";
            where += " and (datediff(d,Time_Add,'" + dateFrom + "')<=0 and datediff(d,Time_Add,'" + dateTo + "')>=0)";
            if (Pay_id > 0)
                where += " and Paid_id = " + Pay_id;
            if (Transport_id > 0)
                where += " and Transport_id = " + Transport_id;
            pros = B_Lebi_Order_Product.GetList("Order_id in (select id from Lebi_Order where " + where + ")", "Time_Add desc");
        }
    }
}