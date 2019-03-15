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
    public class P_Pay : ShopPage
    {
        protected int order_id;
        protected Lebi_Order order;
        protected List<Lebi_OnlinePay> onlinepays;
        protected List<Lebi_Order_Product> order_products;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Pay", "") + "\"><span>" + Tag("在线付款") + "</span></a>";

            order_id = Rint("order_id");
            order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                PageError();
            }
            if (order.User_id != CurrentUser.id || order.IsInvalid == 1 || order.IsPaid == 1)
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
            string onpaywhere = "IsUsed=1 and parentid=0";
            if (CurrentSite.IsMobile == 1)
            {
                onpaywhere += " and (showtype='' or showtype like '%wap%')";
            }
            else
            {
                onpaywhere += " and (showtype='' or showtype like '%web%')";
            }
            onlinepays = B_Lebi_OnlinePay.GetList(onpaywhere, "Sort desc");
            order_products = B_Lebi_Order_Product.GetList("Order_id = "+ order.id, "id desc");
        }
        /// <summary>
        /// 获取一个支付方式的对于供应商设置或（默认设置）
        /// </summary>
        /// <param name="pay"></param>
        /// <returns></returns>
        public Lebi_OnlinePay Getpay(Lebi_OnlinePay pay)
        {
            if (order.IsSupplierCash == 0)
                return pay;
            Lebi_OnlinePay p = B_Lebi_OnlinePay.GetModel("parentid=" + pay.id + " and Supplier_id=" + order.Supplier_id + " and IsUsed=1");
            return p;
        }
        public string FormatMoney(decimal money, int id)
        {
            Lebi_Currency currency = B_Lebi_Currency.GetModel(id);
            if (currency == null)
                currency = new Lebi_Currency();
            StringBuilder sb = new StringBuilder();
            sb.Append("<font class=\"msige\">" + currency.Msige + "</font>");
            sb.Append("<font class=\"money_1\">" + (money * currency.ExchangeRate).ToString("0.00") + "</font>");
            return sb.ToString();
            //return  CurrentCurrency.  + "" + money.ToString("0.00");
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
                        str = Tag("在线付款");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}