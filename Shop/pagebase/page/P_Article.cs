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

    public class P_Article : ShopPage
    {
        protected int id;
        protected Lebi_Node node;
        protected Lebi_Node parentnode;
        protected List<Lebi_Node> nodes;
        protected List<Lebi_Page> pages;
        protected int node_id;
        protected string node_name;
        protected string PageString;
        protected string FootPage;
        protected int recordCount;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_Article'");
            id = Rint_Para("0");
            parentnode = Node("Info");
            pageindex = RequestTool.RequestInt("page", 1);
            node = B_Lebi_Node.GetModel(id);
            nodes = B_Lebi_Node.GetList("parentid=" + parentnode.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'", "Sort desc,id desc");
            if (node == null)
                node = nodes.FirstOrDefault();
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Article", "") + "\"><span>" + Tag(parentnode.Name) + "</span></a>";
            if (node != null)
                path += "<em>&raquo;</em><a href=\"" + URL("P_Article", node.id) + "\"><span>" + node.Name + "</span></a>";
            if (node == null)
                node = new Lebi_Node();
            string where = "Node_id=" + node.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'";
            pages = B_Lebi_Page.GetList(where, "Sort desc,id desc", PageSize, pageindex);
            recordCount = B_Lebi_Page.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?id=" + id + "&page={0}", pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = PageString;
            NextPage = "?id=" + id + "&page=" + (pageindex + 1) + "";
        }
        override public string ThemePageMeta(string code, string tag)
        {
            string str = "";
            switch (tag.ToLower())
            {
                case "description":
                    if (node.SEO_Description != "")
                        str = node.SEO_Description;
                    else
                        str = Lang(SYS.Description);
                    break;
                case "keywords":
                    if (node.SEO_Keywords != "")
                        str = node.SEO_Keywords;
                    else
                        str = Lang(SYS.Keywords);
                    break;
                default:
                    if (node.SEO_Title != "")
                        str = node.SEO_Title;
                    else
                        str = Tag(parentnode.Name) + " - " + node.Name;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}