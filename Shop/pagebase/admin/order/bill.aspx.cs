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
    public partial class Bill : AdminPageBase
    {
        protected List<Lebi_Bill> models;
        protected string PageString;
        protected int t;
        protected int status;
        protected string dateFrom;
        protected string dateTo;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("bill_edit", "修改发票"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            t = RequestTool.RequestInt("t",0);
            status = RequestTool.RequestInt("status", 0);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string  where = "1=1";
            if (key != "")
                where += " and (Order_Code like lbsql{'%" + key + "%'} or User_UserName like lbsql{'%" + key + "%'})";
            if (t > 0)
                where += "and Type_id_BillType="+t;
            if (status > 0)
                where += "and Type_id_BillStatus=" + status;
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            PageSize = RequestTool.getpageSize(25);

            models = B_Lebi_Bill.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Bill.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&t=" + t + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo , page, PageSize, recordCount);
        }
        public Lebi_BillType BillType(int id)
        {
            Lebi_BillType m = B_Lebi_BillType.GetModel(id);
            if (m == null)
                m = new Lebi_BillType();
            return m;
        }
        public string GetTypeName(int id)
        {
            string res = Shop.Bussiness.EX_Type.TypeName(id);
            if(res=="")
                res="全部";
            return Tag(res);
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