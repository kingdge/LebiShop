using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using System.Linq;
using System.Collections.Specialized;
using Shop.Bussiness;
namespace Shop
{

    public class P_CheckOut : ShopPage
    {
        protected Basket basket;
        protected List<Lebi_Card> cards = new List<Lebi_Card>();//优惠券
        protected List<Lebi_Card> moneycards = new List<Lebi_Card>();//购物卡
        protected List<Lebi_ProPerty> ProPerty135;
        protected Lebi_Supplier supplier;
        protected int sid = 0;//结算供应商ID 
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            if (CurrentUser.id == 0)
            {
                //未登录
                if (SYS.IsAnonymousUser != "1")
                {
                    Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain()) + ""));
                }
            }
            sid = RequestTool.RequestInt("sid", 0);
            if (SYS.IsSupplierCash == "1")
            {
                supplier = B_Lebi_Supplier.GetModel(sid);
            }
            if (supplier == null)
                supplier = new Lebi_Supplier();
            CookieTool.SetCookieString("supplier", supplier.id.ToString(), 60);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_Basket", "") + "\"><span>" + Tag("购物车") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_CheckOut", "") + "\"><span>" + Tag("收银台") + "</span></a>";
            basket = new Basket(sid);
            if (basket.Products.Count == 0)
            {
                Response.Redirect(URL("P_Basket", ""));
            }
            if (CurrentUser.id > 0)
            {
                string sqlwhere = " and Time_End>'" + FormatTime(System.DateTime.Now) + "'"; ;
                
                cards = B_Lebi_Card.GetList("User_id=" + CurrentUser.id + " and Type_id_CardType=312 and Type_id_CardStatus in (201,203) " + sqlwhere + "", "id asc");//状态为已发放或部分使用
                moneycards = B_Lebi_Card.GetList("User_id=" + CurrentUser.id + " and Type_id_CardType=311 and Type_id_CardStatus in (201,203) " + sqlwhere + "", "id asc");
            }
            ProPerty135 = B_Lebi_ProPerty.GetList("Type_id_ProPertyType=135", "Sort desc");



        }
        /// <summary>
        /// 代金券限制信息
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public string cardinfo(Lebi_Card card)
        {
            string str = "";
            if (card.Money_Buy > 0)
            {
                str = Tag("最低消费") + "：";
                str += FormatMoney(card.Money_Buy);
            }
            if (card.Pro_Type_ids != "")
            {
                str += "&nbsp;&nbsp;" + Tag("限品类") + "：";
                List<Lebi_Pro_Type> ts = B_Lebi_Pro_Type.GetList("id in (" + card.Pro_Type_ids + ")", "");
                foreach (Lebi_Pro_Type t in ts)
                {
                    str += Lang(t.Name);
                }

            }
            return str;
        }
        public string cardname(int id)
        {
            Lebi_CardOrder model = B_Lebi_CardOrder.GetModel(id);
            if (model == null)
                return "";
            return Lang(model.Name);
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
                        str = Tag("收银台");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}