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
    public partial class cash : AdminPageBase
    {
        protected List<Lebi_Cash> models;
        protected string PageString;
        protected int t;
        protected int status;
        protected int type;
        protected string paytype;
        protected string dateFrom;
        protected string dateTo;
        protected string key;
        protected int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("cash_list", "提现管理"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            t = RequestTool.RequestInt("t",0);
            status = RequestTool.RequestInt("status", 0);
            type = RequestTool.RequestInt("type", 0);
            paytype = RequestTool.RequestSafeString("paytype");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            key = RequestTool.RequestSafeString("key");
            user_id = RequestTool.RequestInt("user_id", 0);
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (paytype != "")
                where += " and PayType = lbsql{'" + paytype + "'}";
            if (key != "")
                where += " and (User_UserName like lbsql{'%" + key + "%'} or Supplier_SubName like lbsql{'%" + key + "%'} or AccountCode like lbsql{'%" + key + "%'} or AccountName like lbsql{'%" + key + "%'} or Bank like lbsql{'%" + key + "%'})";
            if (status > 0)
                where += " and Type_id_CashStatus=" + status;
            if (type == 1)
                where += " and Supplier_id=0";
            if (type == 2)
                where += " and Supplier_id=1";
            if (user_id > 0)
                where += " and User_id =" + user_id + "";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            PageSize = RequestTool.getpageSize(25);

            models = B_Lebi_Cash.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Cash.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&status=" + status + "&paytype=" + paytype + "&type=" + type + "&user_id=" + user_id + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&key=" + key, page, PageSize, recordCount);
        }
        public string GetTypeName(int id)
        {
            string res = Shop.Bussiness.EX_Type.TypeName(id);
            if(res=="")
                res="全部";
            return Tag(res);
        }
    }
}