using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Linq;
using System.Collections.Specialized;
using Shop.Bussiness;
namespace Shop
{

    public class P_UserAgentList : ShopPageUser
    {
        protected string key;
        protected List<Lebi_User> models;
        protected string PageString;
        protected string dateFrom;
        protected string dateTo;
        protected int id;
        protected int parent_id;
        protected string OrderBy;
        protected string orderstr;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("plugin_agent"))
            {
                Response.Write(Tag("权限不足"));
                Response.End();
            }
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserAgent", "") + "\"><span>" + Tag("推广佣金") + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Tag("佣金查询") + "</span></a>";
            pageindex = RequestTool.RequestInt("page", 1);
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            OrderBy = RequestTool.RequestString("OrderBy");
            dateFrom = Rstring("dateFrom");
            dateTo = Rstring("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            id = RequestTool.RequestInt("id", 0);
            parent_id = RequestTool.RequestInt("parent_id", 0);
            if (id > 0)
            {
                if (!CheckFlag(id))
                {
                    Response.Write(Tag("权限不足"));
                    Response.End();
                }
            }
            string where = "1=1";
            if (id == 0)
                where += " and User_id_parent=" + CurrentUser.id;
            if (id > 0)
                where += " and User_id_parent = " + id;
            if (parent_id > 0)
                where += " and id = " + parent_id;
            if (key != "")
                where += " and (UserName like lbsql{'%" + key + "%'} or RealName like lbsql{'%" + key + "%'} or Address like lbsql{'%" + key + "%'} or City like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Reg>='" + FormatDate(lbsql_dateFrom) + "' and Time_Reg<='" + FormatDate(lbsql_dateTo) + "'";
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
            models = B_Lebi_User.GetList(where, orderstr, PageSize, pageindex);
            int recordCount = B_Lebi_User.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&id=" + id + "&OrderBy=" + OrderBy + "&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = "?page=" + (pageindex + 1) + "&id=" + id + "&OrderBy=" + OrderBy + "&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo +"";
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            Lebi_Theme_Page theme_page = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
            if (theme_page == null)
                return "";
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(theme_page.SEO_Description) == "")
                        str = Lang(SYS.Description);
                    else
                        str = Lang(theme_page.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(theme_page.SEO_Keywords) == "")
                        str = Lang(SYS.Keywords);
                    else
                        str = Lang(theme_page.SEO_Keywords);
                    break;
                default:
                    if (Lang(theme_page.SEO_Title) == "")
                        str = Tag("推广列表") + " - " + Tag("推广佣金") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
        public Lebi_Product GetProduct(int id)
        {
            Lebi_Product pro = B_Lebi_Product.GetModel(id);
            if (pro == null)
                pro = new Lebi_Product();
            return pro;
        }
        public bool CheckFlag(int id)
        {
            string ids = ",";
            List<Lebi_User> models = B_Lebi_User.GetList("User_id_parent=" + CurrentUser.id, "");
            if (models != null)
            {
                foreach (Lebi_User model in models)
                {
                    ids += model.id + ",";
                    models = B_Lebi_User.GetList("User_id_parent=" + model.id, "");
                    if (models != null)
                    {
                        foreach (Lebi_User model2 in models)
                        {
                            ids += model2.id + ",";
                            //models = B_Lebi_User.GetList("User_id_parent=" + model.id, "");
                            //if (models != null)
                            //{
                            //    foreach (DB.LebiShop.Lebi_User model3 in models)
                            //    {
                            //        ids += model3.id + ",";
                            //    }
                            //}
                        }
                    }
                }
            }
            if (ids.Contains("," + id + ","))
            {
                return true;
            }
            else
            {
                return false;
            }
            //Lebi_User user = B_Lebi_User.GetModel(id);
            //if (user == null)
            //{
            //    return false;
            //}
            //else
            //{
            //    if (user.User_id_parent == CurrentUser.id)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        CheckFlag(user.User_id_parent);
            //    }
            //}
            //return false;
        }
    }
}