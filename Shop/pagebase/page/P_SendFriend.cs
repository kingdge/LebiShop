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

    public class P_SendFriend : ShopPage
    {
        protected Lebi_Product product;
        protected int id;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_SendFriend'");
            id = Rint_Para("0");
            product = GetProduct(id);
            if (product.id == 0)
            {
                Response.Redirect(URL("P_404", ""));
                Response.End();
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Product", id) + "\"><span>" + Lang(product.Name) + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Tag("邮件分享") + "</span></a>";
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
                    if (Lang(product.SEO_Description) == "")
                        str = Lang(product.Name);
                    else
                        str = Lang(product.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(product.SEO_Keywords) == "")
                        str = Lang(product.Name);
                    else
                        str = Lang(product.SEO_Keywords);
                    break;
                default:
                    if (Lang(product.SEO_Title) == "")
                        str = Tag("邮件分享") +" - "+ Lang(product.Name);
                    else
                        str = Tag("邮件分享") + " - " + Lang(product.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}