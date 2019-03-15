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

    public class P_NewsDetails : ShopPage
    {
        protected List<Lebi_Node> nodes;
        protected Lebi_Page page;
        protected Lebi_Node node;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_NewsDetails'");
            int id = Rint_Para("0");
            node = Node("News");
            page = B_Lebi_Page.GetModel(id);
            if (page == null)
            {
                    PageError();
            }
            else
            {
                page.Count_Views += 1;
                B_Lebi_Page.Update(page);
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_News", "") + "\"><span>" + Tag(node.Name) + "</span></a><em>&raquo;</em><a href=\"" + URL("P_NewsDetails", page.id) + "\"><span>" + page.Name + "</span></a>";
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
                        str = page.SEO_Title ;
                    else
                        str = page.Name + " - " + Tag(node.Name) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}