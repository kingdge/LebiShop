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
    public partial class UserPoint : AdminPageBase
    {
        protected List<Lebi_User_Point> models;
        protected string PageString;
        protected Lebi_User user;
        protected int status;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("userpoint_list", "会员积分列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            int user_id = RequestTool.RequestInt("user_id", 0);
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            status = RequestTool.RequestInt("status",0);
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "1=1";
            if (user_id > 0)
                where += " and User_id="+user_id;
            if (status > 0)
                where += " and Type_id_PointStatus=" + status + "";
            if (key != "")
                where += " and User_id in (select id from [Lebi_User] where UserName like lbsql{'%" + key + "%'} or RealName like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            models = B_Lebi_User_Point.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_User_Point.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&status = " + status, page, PageSize, recordCount);

            user = B_Lebi_User.GetModel(user_id);
            if (user == null)
            {
                user = new Lebi_User();
                user.UserName = Tag("全部会员");
            }
        }

        public Lebi_User GetUser(int id)
        {
            Lebi_User muser = B_Lebi_User.GetModel(id);
            if (muser == null)
            {
                user = new Lebi_User();
            }
            return muser;
        }
    }
}