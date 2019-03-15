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
    public class P_BrandList : ShopPage
    {
        protected string keyword;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_BrandList'");
            keyword = Rstring_Para("0");
            pageindex = RequestTool.RequestInt("page", 1);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em>";
            if (keyword == "")
            {
                path += "<a class=\"text\">" + Tag("品牌列表") + "</span></a>";
            }else{
                path += "<a href=\"" + URL("P_BrandList", "") + "\"><span>" + Tag("品牌列表") + "</span></a><em>&raquo;</em><a class=\"text\">" + keyword + "</span></a>";
            }
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
                    {
                        if (keyword != "")
                        {
                            str = keyword + " - " + Tag("品牌列表");
                        }
                        else
                        {
                            str = Tag("品牌列表");
                        }
                    }else{
                        str = Lang(theme_page.SEO_Title);
                    }
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
        public string Brandwhere(string fist,string keyword)
        {
            string where = "Type_id_BrandStatus = 452 and FirstLetter = lbsql{'" + fist + "'}";
            if (keyword != "")
            {
                where += " and Name like lbsql{'%" + keyword + "%'}";
            }
            return where;
        }
        public static int BrandCount(string fist, string keyword)
        {
            string where = "Type_id_BrandStatus = 452 and FirstLetter=lbsql{'" + fist + "'}";
            if (keyword != "")
            {
                where += " and Name like lbsql{'%" + keyword + "%'}";
            }
            int count = B_Lebi_Brand.Counts(where);
            return count;
        }
    }
}