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
    public class P_ProductCommentDetails : ShopPage
    {
        protected Lebi_Product product;
        protected Lebi_Pro_Type pro_type;
        protected Lebi_Comment comment;
        protected List<Lebi_Comment> comments;
        protected List<Lebi_Comment> productcomments;
        protected string PageString;
        protected int ProductStar;//整数的商品评分，最大为5
        protected string DefaultImage = "";//默认图片
        protected string[] smalls;
        protected string[] bigs;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            int id = Rint_Para("0");
            comment = B_Lebi_Comment.GetModel(id);
            if (comment.id == 0)
            {
                Response.Redirect(URL("P_404", ""));
                Response.End();
            }
            product = GetProduct(comment.Keyid);
            string where = "Parentid =" + id;
            comments = B_Lebi_Comment.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_Comment.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&id=" + id, pageindex, PageSize, recordCount, CurrentLanguage);
            ProductStar = Convert.ToInt32(product.Star_Comment);
            if (ProductStar > 5)
                ProductStar = 5;
            if (ProductStar < 0)
                ProductStar = 0;
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
            path += "<a><span>" + Tag("晒单") + "</span></a>";
            //path += "<a href=\"" + URL("P_Product", id) + "\"><span>" + Lang(product.Name) + "</span></a>";

            smalls = comment.ImagesSmall.Split('@');
            bigs = comment.Images.Split('@');
            if (bigs.Count() > 1)
                DefaultImage = bigs[1];

            productcomments = B_Lebi_Comment.GetList("TableName='Product' and Keyid=" + comment.Keyid + " and id!=" + comment.id + "", "id desc", 5, 1);
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
                        str = Tag("晒单");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}