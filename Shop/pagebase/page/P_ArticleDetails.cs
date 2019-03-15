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

    public class P_ArticleDetails : ShopPage
    {
        protected List<Lebi_Node> nodes;
        protected Lebi_Page page;
        protected Lebi_Node node;
        protected Lebi_Node parentnode;
        protected int id;
        protected Lebi_Page NextPage;
        protected Lebi_Page PreviousPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_ArticleDetails'");
            id = Rint_Para("0");
            parentnode = Node("Info");
            nodes = B_Lebi_Node.GetList("Parentid=" + parentnode.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'", "Sort desc");
            page = B_Lebi_Page.GetModel(id);
            if (page == null)
            {
                node = nodes.FirstOrDefault();
                page = NodePage.GetPageByNode(node);
                if (page == null)
                    PageError();
            }
            else
            {
                page.Count_Views += 1;
                B_Lebi_Page.Update(page);
                node = B_Lebi_Node.GetModel(page.Node_id);
            }

            string where = "Node_id=" + node.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'";
            PreviousPage = B_Lebi_Page.GetModel(where + " and Sort>=" + page.Sort + " and id>" + page.id + " order by Sort asc,id asc");
            NextPage = B_Lebi_Page.GetModel(where + " and Sort<=" + page.Sort + " and id<" + page.id + " order by Sort desc,id desc");
            if (PreviousPage == null)
                PreviousPage = new Lebi_Page();
            if (NextPage == null)
                NextPage = new Lebi_Page();

            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Article", "") + "\"><span>" + Tag(parentnode.Name) + "</span></a><em>&raquo;</em><a href=\"" + URL("P_Article", node.id) + "\"><span>" + node.Name + "</span></a><em>&raquo;</em><a href=\"" + URL("P_ArticleDetails", page.id) + "\"><span>" + page.Name + "</span></a>";
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
                        str = page.Name;
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
                        str = page.Name + " - " + node.Name + " - " + Tag(parentnode.Name);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}