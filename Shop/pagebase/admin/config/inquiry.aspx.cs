using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.Config
{
    public partial class Inquiry : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected int status;
        protected List<Lebi_Inquiry> models;
        protected string PageString;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("inquiry_list", "留言反馈"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            status = RequestTool.RequestInt("status",0);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (key != "")
                where += " and Subject like lbsql{'%" + key + "%'}";
            if (status > 0)
                where += " and Type_id_CommentStatus = " + status + "";
            if (lang != "")
                where += " and Language = lbsql{'" + lang + "'}";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            models = B_Lebi_Inquiry.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Inquiry.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&lang=" + lang + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&key=" + key, page, PageSize, recordCount);
            
        }
    }
}