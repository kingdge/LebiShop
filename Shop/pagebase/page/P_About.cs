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

    public class P_About : ShopPage
    {
        protected int id;
        protected Lebi_Node node;
        protected Lebi_Page page;
        protected List<Lebi_Page> pages;
        protected int node_id;
        protected string node_name;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_About'");
            id = Rint_Para("0");
            node = Node("About");
            page = B_Lebi_Page.GetModel(id);
            pages = B_Lebi_Page.GetList("Node_id=" + node.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'", "Sort desc");
            if (page == null)
            {
                page = pages.FirstOrDefault();
            }
            else
            {
                page.Count_Views += 1;
                B_Lebi_Page.Update(page);
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_About", "") + "\"><span>" + Tag(node.Name) + "</span></a>";
            if (page != null)
                path += "<em>&raquo;</em><a href=\"" + URL("P_About", page.id) + "\"><span>" + page.Name + "</span></a>";
            if (page == null)
                page = new Lebi_Page();
        }
        override public string ThemePageMeta(string code, string tag)
        {
            string str = "";
            switch (tag.ToLower())
            {
                case "description":
                    if (page.SEO_Description != "")
                        str = page.SEO_Description;
                    else
                        str = page.Description;
                    break;
                case "keywords":
                    if (page.SEO_Keywords != "")
                        str = page.SEO_Keywords;
                    else
                        str = page.Name;
                    break;
                default:
                    if (page.SEO_Title != "")
                        str = page.SEO_Title;
                    else
                        str = Tag(node.Name);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}