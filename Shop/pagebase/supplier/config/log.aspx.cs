using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class Log : SupplierPageBase
    {
        protected List<Lebi_Log> models;
        protected string PageString;
        protected int id;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_log_list", "操作日志"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            id = RequestTool.RequestInt("id", 0);
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            type = RequestTool.RequestString("type");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (key != "")
                where += " and (Content like lbsql{'%" + key + "%'} or Description like lbsql{'%" + key + "%'} or TableName like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";
            if (type == "0")
                where += " and charindex(TableName,'Login')=0";
            if (type == "1")
                where += " and TableName='Login'";
            models = B_Lebi_Log.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Log.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&type=" + type, page, PageSize, recordCount);
        }
    }
}