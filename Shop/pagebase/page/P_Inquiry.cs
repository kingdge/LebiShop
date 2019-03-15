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

    public class P_Inquiry : ShopPage
    {
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag(" ◊“≥") + "\"><span>" + Tag(" ◊“≥") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Inquiry", "") + "\"><span>" + Tag("¡Ù—‘∑¥¿°") + "</span></a>";
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
                        str = Tag("¡Ù—‘∑¥¿°");
                    else
                        str = Lang(theme_page.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}