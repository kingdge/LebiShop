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
    public class P_ProductAsk : ShopPage
    {

        protected Lebi_Product product;
        protected Lebi_Pro_Type pro_type;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            int id = Rint_Para("0");
            product = GetProduct(id);
            if (product.id == 0)
            {
                Response.Redirect(URL("P_404", ""));
                Response.End();
            }
            pro_type = B_Lebi_Pro_Type.GetModel(product.Pro_Type_id);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em>";
            if (pro_type != null)
            {
                string[,] parr = Categorypath(pro_type.id);
                for (int i = 0; i <= parr.GetUpperBound(0); i++)
                {
                    path += "<a href=\"" + URL("P_ProductCategory", "" + parr[i, 0] + "") + "\"><span>" + parr[i, 1] + "</span></a><em>&raquo;</em>";
                }
            }
            path += "<a href=\"" + URL("P_Product", product.id) + "\"><span>" + Lang(product.Name) + "</span></a><em>&raquo;</em>";
            path += "<a><span>" + Tag("商品咨询") + "</span></a>";
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
                        str = Tag("商品咨询");
                    else
                        str = Lang(theme_page.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}