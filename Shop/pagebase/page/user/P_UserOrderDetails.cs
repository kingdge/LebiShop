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

    public class P_UserOrderDetails : ShopPageUser
    {
        protected Lebi_Order order;
        protected int id;
        protected List<Lebi_Comment> comments;
        protected List<Lebi_Transport_Order> transport_orders;
        protected List<Lebi_Bill> bills;
        protected List<Lebi_Order_Product> order_products;
        protected Lebi_Pay pay;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserOrderDetails'");
            id = Rint("id");
            order = B_Lebi_Order.GetModel("(IsDel!=1 or IsDel is null) and User_id = " + CurrentUser.id + " and id = " + id + "");
            if (order == null)
            {
                PageError();
            }
            if (order.User_id != CurrentUser.id)
            {
                PageError();
            }
            if (order.Type_id_OrderType == 212)
            {
                Response.Redirect(URL("P_UserReturnDetails", id));
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserOrders", "") + "\"><span>" + Tag("我的订单") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserOrderDetails", id) + "\"><span>" + Tag("订单") + "：" + order.Code + "</span></a>";
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserOrderDetails'");
            id = Rint_Para("0");
            comments = B_Lebi_Comment.GetList("TableName='Order' and Keyid=" + order.id, "id desc");
            transport_orders = B_Lebi_Transport_Order.GetList("Order_id=" + order.id, "id desc");
            bills = B_Lebi_Bill.GetList("Order_id=" + order.id, "id desc");
            order_products = B_Lebi_Order_Product.GetList("Order_id=" + order.id, "id desc");
            pay = B_Lebi_Pay.GetModel(order.Pay_id);
            if (pay == null)
                pay = new Lebi_Pay();
            if (order.OnlinePay_id > 0)
            {
                pay.Code = "OnlinePay";
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
                        str = Tag("订单") + "：" + order.Code + " - " + Tag("我的订单") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}