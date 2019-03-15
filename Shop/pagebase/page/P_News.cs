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

    public class P_News : ShopPage
    {
        protected int id;
        protected Lebi_Node node;
        protected List<Lebi_Page> pages;
        protected string PageString;
        protected string FootPage;
        protected int recordCount;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            pageindex = RequestTool.RequestInt("page", 1);
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_News'");
            node = Node("News");
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_News", "") + "\"><span>" + Tag(node.Name) + "</span></a>";
            string where = "Node_id=" + node.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'";
            pages = B_Lebi_Page.GetList(where, "Sort desc,id desc", PageSize, pageindex);
            recordCount = B_Lebi_Page.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}", pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = PageString;
            NextPage = "?page=" + (pageindex + 1) + "";
        }
        override public string ThemePageMeta(string code, string tag)
        {
            string str = "";
            Lebi_Theme_Page theme_page = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
            if (theme_page == null)
                return "";
            switch (tag.ToLower())
            {
                case "description":
                    if (theme_page.SEO_Description != "")
                        str = theme_page.SEO_Description;
                    else
                        str = Lang(SYS.Description);
                    break;
                case "keywords":
                    if (theme_page.SEO_Keywords != "")
                        str = theme_page.SEO_Keywords;
                    else
                        str = Lang(SYS.Keywords);
                    break;
                default:
                    if (theme_page.SEO_Title != "")
                        str = theme_page.SEO_Title;
                    else
                        str = Tag(node.Name);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}