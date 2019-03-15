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
    public partial class Default : AdminPageBase
    {
        protected List<Lebi_User> models;
        protected string lang;
        protected string PageString;
        protected int level;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected string OrderBy;
        protected string orderstr;
        protected string datetype;
        protected SearchUser su;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("user_list", "会员列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            PageSize = RequestTool.getpageSize(25);
            //lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            level = RequestTool.RequestInt("level", 0);
            //dateFrom = RequestTool.RequestString("dateFrom");
            //dateTo = RequestTool.RequestString("dateTo");
            //datetype = RequestTool.RequestString("datetype");
            OrderBy = RequestTool.RequestString("OrderBy");
            string where = "(IsDel!=1 or IsDel is null)";
            if (su.SQL != "")
            {
                where += " and " + su.SQL;
            }
            if (level > 1)
            {
                where += " and UserLevel_id = " + level;
            }
            else if (level == 1)
            {
                where += " and (IsAnonymous=1 or UserLevel_id=1)";
            }
            else
            {
                where += " and IsAnonymous<>1";
            }
            if (domain3admin && CurrentAdmin.Site_ids != "")
            {
                where += " and Site_id in (" + CurrentAdmin.Site_ids + ")";
            }
            if (key != "")
                where += " and (UserName like lbsql{'%" + key + "%'} or RealName like lbsql{'%" + key + "%'} or Address like lbsql{'%" + key + "%'} or City like lbsql{'%" + key + "%'} or UserNumber like lbsql{'%" + key + "%'} or id like lbsql{'%" + key + "%'})";
            //if (lang != "")
            //    where += " and Language = '" + lang + "'";
            //if (dateFrom != "" && dateTo != "")
            //    if (datetype == "")
            //    {
            //        where += " and ((datediff(d,Time_reg,'" + dateFrom + "')<=0 and datediff(d,Time_reg,'" + dateTo + "')>=0) or (datediff(d,Time_Last,'" + dateFrom + "')<=0 and datediff(d,Time_Last,'" + dateTo + "')>=0))";
            //    }
            //    else if (datetype == "1")
            //    {
            //        where += " and datediff(d,Time_reg,'" + dateFrom + "')<=0 and datediff(d,Time_reg,'" + dateTo + "')>=0";
            //    }
            //    else if (datetype == "2")
            //    {
            //        where += " and datediff(d,Time_Last,'" + dateFrom + "')<=0 and datediff(d,Time_Last,'" + dateTo + "')>=0";
            //    }
            //    else if (datetype == "3")
            //    {
            //        where += " and datediff(d,Birthday,'" + dateFrom + "')<=0 and datediff(d,Birthday,'" + dateTo + "')>=0";
            //    }
            if (OrderBy == "UserNameDesc")
            {
                orderstr = " UserName desc";
            }
            else if (OrderBy == "UserNameAsc")
            {
                orderstr = " UserName asc";
            }
            else if (OrderBy == "RealNameDesc")
            {
                orderstr = " RealName desc";
            }
            else if (OrderBy == "RealNameAsc")
            {
                orderstr = " RealName asc";
            }
            else if (OrderBy == "UserLevelDesc")
            {
                orderstr = " UserLevel_id desc";
            }
            else if (OrderBy == "UserLevelAsc")
            {
                orderstr = " UserLevel_id asc";
            }
            else if (OrderBy == "MoneyDesc")
            {
                orderstr = " Money desc";
            }
            else if (OrderBy == "MoneyAsc")
            {
                orderstr = " Money asc";
            }
            else if (OrderBy == "PointDesc")
            {
                orderstr = " Point desc";
            }
            else if (OrderBy == "PointAsc")
            {
                orderstr = " Point asc";
            }
            else if (OrderBy == "Time_RegDesc")
            {
                orderstr = " Time_reg desc";
            }
            else if (OrderBy == "Time_RegAsc")
            {
                orderstr = " Time_reg asc";
            }
            else if (OrderBy == "Time_LastDesc")
            {
                orderstr = " Time_Last desc";
            }
            else if (OrderBy == "Time_LastAsc")
            {
                orderstr = " Time_Last asc";
            }
            else
            {
                orderstr = " id desc";
            }
            models = B_Lebi_User.GetList(where, orderstr, PageSize, page);
            int recordCount = B_Lebi_User.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&OrderBy=" + OrderBy + "&lang=" + lang + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&level=" + level + "&datetype=" + datetype + "&key=" + key + "&" + su.URL, page, PageSize, recordCount);
        }
    }
}