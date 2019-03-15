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

    public class P_Cashier : ShopPage
    {
        protected int order_id;
        protected Lebi_Order order;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            if (CurrentUser.id == 0)
            {
                //Response.Redirect("login.aspx");
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Basket", "") + "\"><span>" + Tag("购物车") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_Cashier", "") + "\"><span>" + Tag("订单已提交") + "</span></a>";

            order_id = Rint("order_id");
            order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                PageError();
            }
            if (order.User_id != CurrentUser.id || order.IsVerified != 0)
            {
                PageError();
            }
            else
            {
                if (order.Type_id_OrderType == 215)
                {
                    List<Lebi_Order> ors = B_Lebi_Order.GetList("Order_id=" + order.id + "", "");
                    decimal moeny = 0;
                    foreach (Lebi_Order or in ors)
                    {
                        moeny += or.Money_Pay;
                    }
                    order.Money_Pay = moeny;
                    B_Lebi_Order.Update(order);
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
                        str = Tag("订单已提交");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}