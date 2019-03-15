using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Order_ProPerty : AdminPageBase
    {
        protected List<Lebi_Order_ProPerty> models;
        protected string PageString;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_property_list", "订单调查"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (key != "")
                where += " and (ProPertyName like lbsql{'%" + key + "%'} or ProPertyValue like lbsql{'%" + key + "%'} or User_UserName like lbsql{'%" + key + "%'} or Order_Code like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            PageSize = RequestTool.getpageSize(25);
            models = B_Lebi_Order_ProPerty.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Order_ProPerty.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo, page, PageSize, recordCount);
        }
        public Lebi_Order GetOrder(int id)
        {
            Lebi_Order Order = B_Lebi_Order.GetModel(id);
            if (Order == null)
                Order = new Lebi_Order();
            return Order;
        }
    }
}