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

    public class P_UserMessage : ShopPageUser
    {
        protected int type;
        protected List<Lebi_Message> messages;
        protected List<Lebi_Message_Type> message_types;
        protected string PageString;
        protected string type_id;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected string where;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserMessage'");
            type = Rint_Para("0");
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em>";
            if (type == 0)
                path +="<a href=\"" + URL("P_UserMessage", "0") + "\"><span>" + Tag("收件箱") + "</span></a>";
            else
                path +="<a href=\"" + URL("P_UserMessage", "1") + "\"><span>" + Tag("发件箱") + "</span></a>";
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserMessage'");
            key = Rstring("key");
            type_id = Rstring("type_id");
            dateFrom = Rstring("dateFrom");
            dateTo = Rstring("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            pageindex = RequestTool.RequestInt("page", 1);
            if (type == 0)
                where = "User_id_To=" + CurrentUser.id + "";
            if (type == 1)
                where = "User_id_From=" + CurrentUser.id + "";
            if (key != "")
                where += " and (Title like lbsql{'%" + key + "%'})";
            if (type_id != "")
                where += " and Message_Type_id = " + type_id;
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            messages = B_Lebi_Message.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_Message.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&type=" + type + "&type_id=" + type_id + "&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "", pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = "?page=" + (pageindex + 1) + "&type=" + type + "&type_id=" + type_id + "&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "";
            message_types = B_Lebi_Message_Type.GetList("Type_id_MessageTypeClass = 350", "id desc");
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
                        str = Tag("站内信") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}