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

    public class P_Search : ShopPage
    {
        protected List<Lebi_Product> products;
        protected int id;
        protected int pid;
        protected string keyword;
        protected string list;
        protected string sort;
        protected string where;
        protected string order;
        protected string ordertmp;
        protected int recordCount;
        protected string HeadPage;
        protected string FootPage;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_Search'");
            keyword = Rstring_Para("0");
            list = Rstring_Para("1");
            sort = Rstring_Para("2");
            pageindex = Rint_Para("3",1);
            id = Rint_Para("4");
            pid = Rint_Para("5");
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a class=\"text\"><span>" + Tag("商品搜索") + "</span></a><em>&raquo;</em><a class=\"text\"><span>“" + keyword + "”</span></a>";
            where = ProductWhere +" and Type_id_ProductType <> 323";
            if (keyword != "")
            {
                //增加空格划词搜索 by kingdge 2013-09-18
                string wherekeyword = "";
                if (keyword.IndexOf(" ") > -1)
                {
                    string[] keywordsArr;
                    keywordsArr = keyword.Split(new char[1] { ' ' });
                    foreach (string keywords in keywordsArr)
                    {
                        if (keywords != "")
                            if (wherekeyword == "")
                                wherekeyword = "Name like lbsql{'%" + keywords + "%'}";
                            else
                                wherekeyword += " and Name like lbsql{'%" + keywords + "%'}";
                    }
                }
                else
                {
                    wherekeyword = "Name like lbsql{'%" + keyword + "%'}";
                }
                where += " and (";
                where += " (" + wherekeyword + ") or Number like lbsql{'%" + keyword + "%'} or Code like lbsql{'%" + keyword + "%'}";
                if (keyword.Length >= 3)
                {
                    List<Lebi_Product> parents = B_Lebi_Product.GetList("(IsDel!=1 or IsDel is null) and Product_id >0 and Name like lbsql{'%" + keyword + "%'} or Number like lbsql{'%" + keyword + "%'} or Code like lbsql{'%" + keyword + "%'}", "");
                    if (parents.Count > 0)
                    {
                        string ids = "";
                        foreach (Lebi_Product parent in parents)
                        {
                            ids += parent.Product_id +",";
                        }
                        ids = ids.Substring(0, ids.Length - 1);
                        if (ids != "")
                        {
                            where += " or id in(" + ids + ")";
                        }
                    }
                }
                where += " )";
            }
            if (id > 0)
            {
                where += " and " + CategoryWhere(id);
            }
            if (pid > 0)
            {
                where += " and Brand_id=" + pid + "";
            }
            if (sort == "1") { order = " Count_Sales_Show desc"; ordertmp = "a"; }
            else if (sort == "1a") { order = " Count_Sales_Show asc"; ordertmp = ""; }
            else if (sort == "2") { order = " Price desc"; ordertmp = "a"; }
            else if (sort == "2a") { order = " Price asc"; ordertmp = ""; }
            else if (sort == "3") { order = " Count_Comment desc"; ordertmp = "a"; }
            else if (sort == "3a") { order = " Count_Comment asc"; ordertmp = ""; }
            else if (sort == "4") { order = " Time_Add desc"; ordertmp = "a"; }
            else if (sort == "4a") { order = " Time_Add asc"; ordertmp = ""; }
            else if (sort == "5") { order = " Count_Views_Show desc"; ordertmp = "a"; }
            else if (sort == "5a") { order = " Count_Views_Show asc"; ordertmp = ""; }
            else if (sort == "6") { order = " Count_Stock desc"; ordertmp = "a"; }
            else if (sort == "6a") { order = " Count_Stock asc"; ordertmp = ""; }
            else { order = " Sort desc,id desc"; ordertmp = ""; }
            LB.DataAccess.SQLPara sp = new LB.DataAccess.SQLPara(where, order, "*");
            products = B_Lebi_Product.GetList(sp, PageSize, pageindex);
            recordCount = B_Lebi_Product.Counts(sp);
            string url = URL("P_Search", keyword + "," + list + "," + sort + ",{0}");
            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple(url, pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = URL("P_Search", keyword + "," + list + "," + sort + "," + (pageindex + 1) + "");
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
                        str = keyword + " - " + Tag("商品搜索");
                    else
                        str = Lang(theme_page.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}