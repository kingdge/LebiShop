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
    public class P_Pay : ShopPage
    {
        protected int order_id;
        protected string pagefrom;
        protected Lebi_Order order;
        protected List<Lebi_OnlinePay> onlinepays;
        protected List<Lebi_Order_Product> order_products;
        protected bool Isweixin = false;
       
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Pay", "") + "\"><span>" + Tag("在线付款") + "</span></a>";

            order_id = Rint("order_id");
            pagefrom = Rstring("pagefrom");
            order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                PageError();
            }
            if (order.User_id != CurrentUser.id)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain()) + ""));
            }
            if (order.IsPaid == 1)
            {
                Response.Redirect(URL("P_UserOrderDetails", order.id));
            }
            if (order.IsInvalid == 1)
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
            //string onpaywhere = "IsUsed=1 and Supplier_id=" + order.Supplier_id + " and ','+Language_ids+',' like '%," + order.Language_id + ",%'";
            string onpaywhere = "IsUsed=1 and parentid=0 and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'";
            //if (order.Supplier_id == 0)
            //    onpaywhere += " and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%' and Supplier_id=0";
            //if (order.Supplier_id > 0)
            //{
            //    if (EX_Supplier.GetUser(order.Supplier_id).IsCash == 1)
            //    {
            //        onpaywhere += " and Supplier_id=" + order.Supplier_id + "";
            //    }
            //    else
            //    {
            //        onpaywhere += " and Supplier_id=0";
            //    }
            //}
            if (CurrentSite.IsMobile == 1)
            {
                onpaywhere += " and (showtype='' or showtype like '%wap%')";
            }
            else
            {
                onpaywhere += " and (showtype='' or showtype like '%web%')";
            }
            onlinepays = B_Lebi_OnlinePay.GetList(onpaywhere, "Sort desc");
            order_products = B_Lebi_Order_Product.GetList("Order_id = " + order.id, "id desc");
            string useragent = Request.UserAgent.ToString().ToLower();
            if (useragent.Contains("micromessenger"))
            {
                Isweixin = true;
            }
            //<-{支付快捷跳转 by lebi.kingdge 2018-07-05
            bool IsRedirect = false;
            Lebi_OnlinePay op = B_Lebi_OnlinePay.GetModel(order.OnlinePay_id);
            if (op == null)
            {
                op = new Lebi_OnlinePay();
            }
            Lebi_OnlinePay_Log log = B_Lebi_OnlinePay_Log.GetModel("Order_id = "+ order.id +" and OnlinePay_id = "+ order.OnlinePay_id + "");
            if (log == null)
            {
                IsRedirect = true;
                log = new Lebi_OnlinePay_Log();
                log.Order_id = order.id;
                log.OnlinePay_id = order.OnlinePay_id;
                log.Time_add = DateTime.Now;
                B_Lebi_OnlinePay_Log.Add(log);
            }else
            {
                B_Lebi_OnlinePay_Log.Delete(log.id);
            }
            if (!Isweixin && op.Code == "weixinpay")
            {
                IsRedirect = false;
            }
            if (IsRedirect) {
                if (op.Url.IndexOf("?") > -1)
                {
                    HttpContext.Current.Response.Redirect(WebPath.TrimEnd('/') + op.Url + "&order_id=" + order.id + "&opid=" + order.OnlinePay_id);
                }else
                {
                    HttpContext.Current.Response.Redirect(WebPath.TrimEnd('/') + op.Url + "?order_id=" + order.id + "&opid=" + order.OnlinePay_id);
                }
            }
            //}->
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
            Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
            if (supplier != null)
            {
                //供应商发货商城收款的情况
                if (supplier.IsCash == 0)
                    return pay;
            }

            Lebi_OnlinePay p = B_Lebi_OnlinePay.GetModel("parentid=" + pay.id + " and Supplier_id=" + order.Supplier_id + " and IsUsed=1");
            if (p != null)
            {
                if (Lang(p.Name) == "")
                    p.Name = pay.Name;
                if (Lang(p.Description) == "")
                    p.Name = pay.Name;
                if (p.Logo == "")
                    p.Logo = pay.Logo;
            }
            return p;
        }

        public string FormatMoney(decimal money, int id)
        {
            Lebi_Currency currency = B_Lebi_Currency.GetModel(id);
            if (currency == null)
                currency = B_Lebi_Currency.GetModel("id>0 order by IsDefault desc,Sort desc");
            if (currency == null)
                currency = new Lebi_Currency();
            StringBuilder sb = new StringBuilder();
            sb.Append("<font class=\"msige\">" + currency.Msige + "</font>");
            sb.Append("<font class=\"money_1\">" + (money * currency.ExchangeRate).ToString("f" + currency.DecimalLength + "") + "</font>");
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


        public bool IsPayShow(string code)
        {
            if (onlinepays.FindAll(p => p.Code == code).Count > 0)
                return true;
            return false;
        }
        public Lebi_OnlinePay GetPay(string code)
        {
            var m = onlinepays.Find(p => p.Code == code);
            if (m == null)
                m = new Lebi_OnlinePay();
            return m;
        }
    }
}