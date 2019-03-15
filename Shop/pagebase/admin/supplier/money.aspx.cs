using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;
namespace Shop.Admin.Supplier
{
    public partial class UserMoney : AdminPageBase
    {
        protected List<Lebi_Supplier_Money> models;
        protected Lebi_Supplier user;
        protected string PageString;
        protected int status;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_money_list", "资金明细"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            user_id = RequestTool.RequestInt("user_id", 0);
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            status = RequestTool.RequestInt("status",0);
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (status > 0)
                where += " and Type_id_MoneyStatus=" + status + "";
            if (user_id > 0)
                where += " and Supplier_id=" + user_id + "";
            if (key != "")
                where += " and (Supplier_id in (select id from [Lebi_Supplier] where UserName like lbsql{'%" + key + "%'} or RealName like lbsql{'%" + key + "%'}) or Remark like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";
            models = B_Lebi_Supplier_Money.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Money.Counts(where);
            PageString =Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&status = " + status + "&user_id = " + user_id, page, PageSize, recordCount);
            user = B_Lebi_Supplier.GetModel(user_id);
            if (user == null)
            {
                user = new Lebi_Supplier();
            }
        }
    }
}