using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using Shop.Tools;
using System.Linq;
using System.Collections.Specialized;

namespace Shop.Bussiness
{

    public class P_UserComment : ShopPageUser
    {
        protected List<Lebi_Comment> comments;
        protected List<Lebi_Order_Product> order_products;
        protected List<Lebi_Type> types;
        protected string PageString;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected int status;
        protected int type;
        protected string where;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserComment", "") + "\"><span>" + Tag("商品评价") + "</span></a>";
            //key={0}&type={1}&status={2}&dateFrom={3}&dateTo={4}
            key = Rstring("key");
            type = Rint("type");
            status = Rint("status");
            dateFrom = Rstring("dateFrom");
            dateTo = Rstring("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            pageindex = RequestTool.RequestInt("page", 1);
            types = B_Lebi_Type.GetList("Class='CommentStatus'", "sort desc");
            if (type == 0)
            {
                where = "Parentid = 0 and TableName = 'Product' and User_id=" + CurrentUser.id + "";
                if (key != "")
                    where += " and (Content like lbsql{'%" + key + "%'})";
                if (dateFrom != "" && dateTo != "")
                    where += " and (datediff(d,Time_Add,'" + lbsql_dateFrom + "')<=0 and datediff(d,Time_Add,'" + lbsql_dateTo + "')>=0)";
                if (status > 0)
                    where += " and (Status = " + status + ")";
                comments = B_Lebi_Comment.GetList(where, "id desc", PageSize, pageindex);
                foreach (Shop.Model.Lebi_Comment c in comments)
                {
                    c.IsRead = 1;
                    B_Lebi_Comment.Update(c);
                }
                int recordCount = B_Lebi_Comment.Counts(where);
                //string url = URL("P_UserComment", key + "," + type + "," + status + "," + dateFrom + "," + dateTo + ",{0}");
                //PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, page, PageSize, recordCount, CurrentLanguage);
                PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&key=" + key + "&type=" + type + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "", pageindex, PageSize, recordCount, CurrentLanguage);
            }
            else
            {
                //待评价商品
                where = "IsCommented = 0 and User_id=" + CurrentUser.id + "";
                if (key != "")
                    where += " and (Product_Name like lbsql{'%" + key + "%'})";
                if (dateFrom != "" && dateTo != "")
                    where += " and (datediff(d,Time_Add,'" + lbsql_dateFrom + "')<=0 and datediff(d,Time_Add,'" + lbsql_dateTo + "')>=0)"; 
                order_products = B_Lebi_Order_Product.GetList(where, "id desc", PageSize, pageindex);
                int recordCount = B_Lebi_Order_Product.Counts(where);
                string url = URL("P_UserComment", key + "," + type + "," + status + "," + dateFrom + "," + dateTo + ",{0}");
                PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&key=" + key + "&type=" + type + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "", pageindex, PageSize, recordCount, CurrentLanguage);
            }
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
                        str = Tag("商品评价") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}