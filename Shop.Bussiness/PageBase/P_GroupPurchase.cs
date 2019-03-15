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

    public class P_GroupPurchase: ShopPage
    {
        protected List<Lebi_Product> products;
        protected int id;
        protected string cid;
        protected string list;
        protected string sort;
        protected string where;
        protected string order;
        protected string ordertmp;
        protected int recordCount;
        protected string HeadPage;
        protected string FootPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_GroupPurchase'");
            list = Rstring_Para("0");
            sort = Rstring_Para("1");
            pageindex = RequestTool.RequestInt("page", 1);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a class=\"text\"><span>" + Tag("团购") + "</span></a>";
            where = "Type_id_ProductStatus = 101 and Type_id_ProductType = 322 and Product_id=0";
            if (sort == "1") { order = " Count_Sales desc"; ordertmp = "a"; }
            else if (sort == "1a") { order = " Count_Sales asc"; ordertmp = ""; }
            else if (sort == "2") { order = " Price desc"; ordertmp = "a"; }
            else if (sort == "2a") { order = " Price asc"; ordertmp = ""; }
            else if (sort == "3") { order = " Count_Comment desc"; ordertmp = "a"; }
            else if (sort == "3a") { order = " Count_Comment asc"; ordertmp = ""; }
            else if (sort == "4") { order = " Time_Add desc"; ordertmp = "a"; }
            else if (sort == "4a") { order = " Time_Add asc"; ordertmp = ""; }
            else if (sort == "5") { order = " Count_Views_Show desc"; ordertmp = "a"; }
            else if (sort == "5a") { order = " Count_Views_Show asc"; ordertmp = ""; }
            else if (sort == "6") { order = " Count_Stock desc"; ordertmp = "a"; }
            else if (sort == "6a") { order = " Count_Stock asc"; ordertmp = ""; }
            else { order = " Count_Sales desc"; ordertmp = "a"; }
            products = B_Lebi_Product.GetList(where, order, PageSize, pageindex);
            recordCount = B_Lebi_Product.Counts(where);

            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple("?page={0}&sort=" + sort + "&list=" + list + "", pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&sort=" + sort + "&list=" + list + "", pageindex, PageSize, recordCount, CurrentLanguage);
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
                        str = Tag("团购");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}