using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Log : AdminPageBase
    {
        protected List<Lebi_Log> models;
        protected string PageString;
        protected int id;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected string type;
        protected string lb;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("log_list", "操作日志列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            id = RequestTool.RequestInt("id", 0);
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            type = RequestTool.RequestString("type");
            lb = RequestTool.RequestString("lb");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (id > 0)
                where += " and Admin_id=" + id;
            if (key != "")
                where += " and (Content like lbsql{'%" + key + "%'} or Description like lbsql{'%" + key + "%'} or TableName like lbsql{'%" + key + "%'} or IP_Add like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            if (type == "0")
                where += " and charindex(TableName,'Login')=0";
            if (type == "1")
                where += " and TableName='Login'";
            if (lb != "all")
                where += " and TableName!='system' and TableName!=''";
            models = B_Lebi_Log.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Log.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&lb="+lb+"&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&type=" + type + "&id=" + id, page, PageSize, recordCount);
        }
        public static string Administrator_TypeOption(string class_, int id)
        {
            List<Lebi_Administrator> models = B_Lebi_Administrator.GetList("1=1", "id desc");
            string str = "";
            foreach (Lebi_Administrator model in models)
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.UserName + "</option>";
            }
            return str;

        }
    }
}