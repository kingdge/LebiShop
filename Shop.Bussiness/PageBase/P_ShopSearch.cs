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

    public class P_ShopSearch : ShopPage
    {
        protected List<Lebi_Supplier> shops;

        protected string keyword;
        
        protected string sort;
        protected string where="";
        
        protected int recordCount;
        protected string HeadPage;
        protected string FootPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_ShopSearch'");
            keyword = Rstring_Para("0");
            
            sort = Rstring_Para("1");
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
            
            
            //SQLDataAccess.SQLPara sp = new SQLDataAccess.SQLPara(where, "id desc", "*");
            shops = B_Lebi_Supplier.GetList(where,"id desc", PageSize, pageindex);
            recordCount = B_Lebi_Supplier.Counts(where);
            string url = URL("P_ShopSearch", keyword + ","+ sort + ",{0}");
            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple(url, pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, pageindex, PageSize, recordCount, CurrentLanguage);
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
                        str = Lang(theme_page.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}