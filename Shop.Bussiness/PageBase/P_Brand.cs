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

    public class P_Brand : ShopPage
    {
        protected List<Lebi_Product> products;
        protected Lebi_Brand brand;
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
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_Brand'");
            id = Rint_Para("0");
            cid = Rstring_Para("1");
            list = Rstring_Para("2");
            sort = Rstring_Para("3");
            pageindex = RequestTool.RequestInt("page", 1);
            brand = B_Lebi_Brand.GetModel(id);
            if (brand == null)
            {
                Response.Redirect(URL("P_BrandList", ""));
                Response.End();
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_BrandList", "") + "\"><span>" + Tag("品牌列表") + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Lang(brand.Name) + "</span></a>";
            where = "Type_id_ProductStatus = 101 and Product_id=0 and Type_id_ProductType <> 323";
            if (id > 0)
                where += " and Brand_id = " + id + "";
            if (cid != "")
                where += " and " + Categorywhere(cid);
            if (sort == "1") { order = " Count_Sales_Show desc"; ordertmp = "a"; }
            else if (sort == "1a") { order = " Count_Sales_Show asc"; ordertmp = ""; }
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
            else { order = " Sort desc,id desc"; ordertmp = ""; }
            products = B_Lebi_Product.GetList(where, order, PageSize, pageindex);
            recordCount = B_Lebi_Product.Counts(where);

            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple("?page={0}&id=" + id + "&cid=" + cid + "&sort=" + sort + "&list=" + list + "", pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&id=" + id + "&cid=" + cid + "&sort=" + sort + "&list=" + list + "", pageindex, PageSize, recordCount, CurrentLanguage);
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(brand.SEO_Description) == "")
                        str = Lang(SYS.Description);
                    else
                        str = Lang(brand.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(brand.SEO_Keywords) == "")
                        str = Lang(brand.Name);
                    else
                        str = Lang(brand.SEO_Keywords);
                    break;
                default:
                    if (Lang(brand.SEO_Title) == "")
                        str = Lang(brand.Name);
                    else
                        str = Lang(brand.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}