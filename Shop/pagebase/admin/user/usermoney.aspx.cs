using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
namespace Shop.Admin.user
{
    public partial class UserMoney : AdminPageBase
    {
        protected List<Lebi_User_Money> models;
        protected string PageString;
        protected Lebi_User user;
        protected int type;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected int PageSize;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("usermoney_list", "会员资金列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            int user_id = RequestTool.RequestInt("user_id", 0);
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            type = RequestTool.RequestInt("type", 0);
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (user_id > 0)
                where += " and User_id="+user_id;
            if (type > 0)
                where += " and Type_id_MoneyType=" + type + "";
            if (key != "")
                where += " and (User_UserName like lbsql{'%" + key + "%'} or User_RealName like lbsql{'%" + key + "%'} or Remark like lbsql{'%" + key + "%'} or Order_PayNo like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            models = B_Lebi_User_Money.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_User_Money.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&user_id=" + user_id + "&type=" + type, page, PageSize, recordCount);
            user = B_Lebi_User.GetModel(user_id);
            if (user == null)
            {
                user = new Lebi_User();
                user.UserName = Tag("全部会员");
            }
        }
    }
}