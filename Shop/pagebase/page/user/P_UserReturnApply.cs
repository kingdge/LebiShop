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

    public class P_UserReturnApply : ShopPageUser
    {
        protected Lebi_Order order;
        protected int id;
        protected List<Lebi_Order_Product> order_products;
        protected string where;
        protected string shouhuoren="";
        protected string shouhuodianhua = "";
        protected string shouhuodizhi = "";
        protected string shouhuoyoubian = "";
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            if (SYS.IsClosetuihuo == "1")
            {
                PageError();
            }
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserReturnApply'");
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserReturn", "" + id + "") + "\"><span>" + Tag("退货订单") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserReturnApply", "") + "\"><span>" + Tag("退货申请") + "</span></a>";
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserReturnApply'");
            id = Rint_Para("0");
            order = B_Lebi_Order.GetModel("User_id = " + CurrentUser.id + " and id = " + id + "");
            if (order == null)
            {
                PageError();
            }
            if (order.User_id != CurrentUser.id)
            {
                PageError();
            }
            where = "Order_id=" + order.id + "";
            order_products = B_Lebi_Order_Product.GetList(where, "id desc");
            if (order.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                if (supplier != null)
                {
                    shouhuoren = supplier.RealName;
                    shouhuodizhi = supplier.Address;
                    shouhuodianhua = supplier.Phone;
                    shouhuoyoubian = supplier.Postalcode;
                }
            }
            else
            {
                Lebi_Express_Shipper shipper = B_Lebi_Express_Shipper.GetList("", "Sort desc", 1, 1).FirstOrDefault();
                if (shipper != null)
                {
                    shouhuoren = shipper.UserName;
                    shouhuodizhi = shipper.Address;
                    shouhuodianhua = shipper.Tel;
                    shouhuoyoubian = shipper.ZipCode;
                }
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
                        str = Tag("退货申请") + " - " + Tag("退货订单") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}