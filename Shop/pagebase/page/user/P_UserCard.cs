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

    public class P_UserCard : ShopPageUser
    {
        protected List<Lebi_Card> cards;
        protected string PageString;
        protected int cardtype = 0;
        protected string where;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserCard", "") + "\"><span>" + Tag("我的卡券") + "</span></a>";
            cardtype = RequestTool.RequestInt("cardtype", 312);
            pageindex = RequestTool.RequestInt("page", 1);
            where = "User_id=" + CurrentUser.id + "";
            if (cardtype > 0)
                where += " and Type_id_CardType=" + cardtype;
            cards = B_Lebi_Card.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_Card.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&cardtype=" + cardtype, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = "?page=" + (pageindex + 1) + "&cardtype=" + cardtype + "";
        }
        /// <summary>
        /// 卡类型
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public string CardName(int oid)
        {
            Lebi_CardOrder order = B_Lebi_CardOrder.GetModel(oid);
            if (order == null)
                return "";
            return Lang(order.Name);
        }
        /// <summary>
        /// 卡备注
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public string CardRemark(Lebi_Card card)
        {
            string str = "";
            if (card.Money_Buy > 0)
                str += Tag("最低消费") + "：" + FormatMoney(card.Money_Buy) + "；";
            if (card.Pro_Type_ids != "")
            {
                string tstr = "";
                List<Lebi_Pro_Type> ts = B_Lebi_Pro_Type.GetList("id in (" + card.Pro_Type_ids + ")", "");
                foreach (Lebi_Pro_Type t in ts)
                {
                    if (tstr == "")
                        tstr = Lang(t.Name);
                    else
                        tstr += "," + Lang(t.Name);
                }
                str += Tag("限制商品") + "：" + tstr + "；";
            }
            return str;
        }
        public string SearchTab()
        {
            string str = "";
            List<Lebi_Type> ts = B_Lebi_Type.GetList("Class='CardType'", "id asc");
            foreach (Lebi_Type t in ts)
            {
                string sel = "";
                if (cardtype == t.id)
                    sel = "class=\"selected\"";
                str += "<li " + sel + "><a href=\"?cardtype=" + t.id + "\"><span>" + Tag(t.Name) + "</span></a></li>";
            }
            return str;
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
                        str = Tag("我的卡券") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title) ;
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}