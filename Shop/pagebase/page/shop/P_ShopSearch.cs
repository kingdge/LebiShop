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

    public class P_ShopSearch : ShopPage
    {
        protected List<Lebi_Supplier> shops;
        protected string keyword;
        protected string sort;
        protected string where = "";
        protected int recordCount;
        protected string HeadPage;
        protected string FootPage;
        protected int id;
        protected int area_id = 0;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_ShopSearch'");
            keyword = RequestTool.RequestSafeString("keyword");
            string key = RequestTool.RequestSafeString("key");
            if (keyword == "" && key != "")
            {
                keyword = key;
            }
            sort = Rstring_Para("1");
            area_id = RequestTool.RequestInt("area_id",0);
            id = 0;
            where = "Supplier_Group_id in (select w.id from [Lebi_Supplier_Group] as w where w.type='supplier' or w.type='')";
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a class=\"text\"><span>" + Tag("µêÆÌËÑË÷") + "</span></a><em>&raquo;</em><a class=\"text\"><span>¡°" + keyword + "¡±</span></a>";

            if (keyword != "")
            {

                string wherekeyword = "";
                if (keyword.IndexOf(" ") > -1)
                {
                    string[] keywordsArr;
                    keywordsArr = keyword.Split(new char[1] { ' ' });
                    foreach (string keywords in keywordsArr)
                    {
                        if (keywords != "")
                            if (wherekeyword == "")
                                wherekeyword = "(Name like lbsql{'%" + keywords + "%'} or SubName like lbsql{'%" + keywords + "%'})";
                            else
                                wherekeyword += " and (Name like lbsql{'%" + keywords + "%'} or SubName like lbsql{'%" + keywords + "%'})";
                    }
                }
                else
                {
                    wherekeyword = "(Name like lbsql{'%" + keyword + "%'} or SubName like lbsql{'%" + keyword + "%'})";
                }
                where = wherekeyword;
            }
            if (area_id > 0)
            {
                where += " and area_id in (" + EX_Area.Area_ids(area_id) + ")";
            }

            //SQLDataAccess.SQLPara sp = new SQLDataAccess.SQLPara(where, "id desc", "*");
            shops = B_Lebi_Supplier.GetList(where, "id desc", PageSize, pageindex);
            recordCount = B_Lebi_Supplier.Counts(where);
            string url = URL("P_ShopSearch", keyword + "," + sort + ",{0}");
            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple(url, pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = URL("P_ShopSearch", keyword + "," + sort + "," + (pageindex + 1) + "");
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
                        str = keyword + " - " + Tag("µêÆÌËÑË÷");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}