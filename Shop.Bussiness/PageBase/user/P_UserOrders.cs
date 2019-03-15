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

    public class P_UserOrders : ShopPageUser
    {
        protected List<Lebi_Order> orders;
        protected string PageString;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected string status;
        protected string where;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserOrders", "") + "\"><span>" + Tag("我的订单") + "</span></a>";
            key = Rstring("key");
            dateFrom = Rstring("dateFrom");
            dateTo = Rstring("dateTo");
            status = Rstring("status");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            pageindex = RequestTool.RequestInt("page", 1);
            where = "User_id=" + CurrentUser.id + " and Type_id_OrderType=211";
            if (key != "")
                where += " and id in (select Order_id from Lebi_Order_Product where Product_Name like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + lbsql_dateFrom + "')<=0 and datediff(d,Time_Add,'" + lbsql_dateTo + "')>=0)";
            if (status == "1")
                where += " and IsPaid = 0";
            if (status == "2")
                where += " and (IsShipped = 1 or IsPaid=1) and IsReceived_All=0";
            if (status == "3")
                where += " and IsReceived = 1";
            if (status == "4")
                where += " and IsCompleted = 1";
            if (status == "5")
                where += " and IsInvalid = 1";
            orders = B_Lebi_Order.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_Order.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&status=" + status + "&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "", pageindex, PageSize, recordCount, CurrentLanguage);
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            Lebi_Theme_Page pagemodel = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
            if (pagemodel == null)
                return "";
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(pagemodel.SEO_Description) == "")
                        str = Lang(SYS.Description);
                    else
                        str = Lang(pagemodel.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(pagemodel.SEO_Keywords) == "")
                        str = Lang(SYS.Keywords);
                    else
                        str = Lang(pagemodel.SEO_Keywords);
                    break;
                default:
                    if (Lang(pagemodel.SEO_Title) == "")
                        str = Tag("我的订单") + " - " + Tag("会员中心");
                    else
                        str = Lang(pagemodel.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}