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
    public class P_Help : ShopPage
    {
        protected int id;
        protected int pageid;
        protected string type;
        protected string Name;
        protected string Content;
        protected Lebi_Node node;
        protected Lebi_Node parentnode;
        protected List<Lebi_Node> nodes;
        public List<Lebi_Page> pages;
        protected Lebi_Page page;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_Help'");
            id = Rint_Para("0");
            pageid = Rint_Para("1");
            type = Rstring("type");
            parentnode = Node("Help");
            nodes = B_Lebi_Node.GetList("Parentid=" + parentnode.id + " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'", "Sort desc");
            if (id == 0 && type != "")
            {
                node = new Lebi_Node();
                if (type == "agreement")
                {
                    Name = Tag("注册协议");
                    Content = Lang(SYS.ServiceP).Replace("\n", "<br/>");
                }
                node.Name = Name;
                path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Help", "") + "\"><span>" + Tag(parentnode.Name) + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Tag(Name) + "</span></a>";
            }
            else
            {
                node = B_Lebi_Node.GetModel(id);
                if (node == null)
                {
                    node = nodes.FirstOrDefault();
                    if (node == null)
                    {
                        node = new Lebi_Node();
                        //PageError();
                    }
                    else
                    {
                        id = node.id;
                    }
                }
                //else
                //{
                //    if (node.id == id)
                //    {
                //        node = nodes.FirstOrDefault();
                //    }
                //}
                pages = B_Lebi_Page.GetList("Node_id=" + node.id + "", "id desc");
                if (pages == null)
                    pages = new List<Lebi_Page>();
                path = "<a href=\"" + CurrentLanguage.Path + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Help", "") + "\"><span>" + Tag(parentnode.Name) + "</span></a>";
                page = B_Lebi_Page.GetModel("id=" + pageid + "");
                if (page == null)
                {
                    page = pages.FirstOrDefault();
                    if (pages.FirstOrDefault() == null)
                    {
                        pageid = 0;
                    }
                    else
                    {
                        pageid = page.id;
                    }
                }
                else
                {
                    path += "<em>&raquo;</em><a href=\"" + URL("P_Help", node.id) + "\"><span>" + node.Name + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + page.Name + "</span></a>";
                }
                //Help_Content.pages = pages;
            }
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
                        if (type == "agreement")
                        {
                            str = Tag("注册协议") + " - " + Tag(parentnode.Name);
                        }
                        else
                        {
                            if (node.Name != "")
                            {
                                str = node.Name + " - " + Tag(parentnode.Name);
                            }
                            else
                            {
                                str = Tag(parentnode.Name);
                            }
                        }
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}