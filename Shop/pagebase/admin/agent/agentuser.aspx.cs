using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class agentuser : AdminPageBase
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
        protected int id;
        protected int parent_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("agent_user_list", "推广列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            OrderBy = RequestTool.RequestString("OrderBy");
            id = RequestTool.RequestInt("id", 0);
            parent_id = RequestTool.RequestInt("parent_id", 0);
            string where = "IsAnonymous<>1 " + su.SQL;
            if (id == 0)
                where += " and Count_sonuser>0";
            if (id > 0)
                where += " and User_id_parent = " + id;
            if (parent_id > 0)
                where += " and id = " + parent_id;
            if (key != "")
                where += " and (UserName like lbsql{'%" + key + "%'} or RealName like lbsql{'%" + key + "%'} or Address like lbsql{'%" + key + "%'} or City like lbsql{'%" + key + "%'})";
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
            else if (OrderBy == "CountSonAsc")
            {
                orderstr = " Count_sonuser asc";
            }
            else if (OrderBy == "CountSonDesc")
            {
                orderstr = " Count_sonuser desc";
            }
            else
            {
                orderstr = " id desc";
            }
            models = B_Lebi_User.GetList(where, orderstr, PageSize, page);
            int recordCount = B_Lebi_User.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&OrderBy=" + OrderBy + "&lang=" + lang + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&level=" + level + "&datetype=" + datetype + "&key=" + key + "&id=" + id + "&parent_id=" + parent_id, page, PageSize, recordCount);
        }
    }
}