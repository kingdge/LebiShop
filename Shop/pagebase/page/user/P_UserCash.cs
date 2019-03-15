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

    public class P_UserCash : ShopPageUser
    {
        public Lebi_Currency DefaultCurrency;
        protected List<Lebi_OnlinePay> onlinepays;
        protected List<Lebi_User_Bank> user_bank;
        protected List<Lebi_Cash> cashs;
        protected string PageString;
        protected int CashCount;
        protected bool SubmitShow;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserMoney", "") + "\"><span>" + Tag("资金记录") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserCash", "") + "\"><span>" + Tag("提现") + "</span></a>";
            DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (DefaultCurrency == null)
                DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();
            onlinepays = B_Lebi_OnlinePay.GetList("IsUsed=1 and parentid=0 and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'", "Sort desc");
            if (CurrentUser.OnlinePay_id == 0)
            {
                if (onlinepays.Count > 0)
                {
                    CurrentUser.OnlinePay_id = onlinepays.FirstOrDefault().id;
                }
                else
                {
                    CurrentUser.OnlinePay_id = 0;
                }
            }
            pageindex = RequestTool.RequestInt("page", 1);
            string where = "User_id=" + CurrentUser.id + "";
            cashs = B_Lebi_Cash.GetList(where, "Time_add desc", PageSize, pageindex);
            CashCount = B_Lebi_Cash.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}", pageindex, PageSize, CashCount, CurrentLanguage);
            NextPage = "?page=" + (pageindex + 1) + "";
            decimal TakeMoneyLimit = 0;
            decimal.TryParse(SYS.TakeMoneyLimit, out TakeMoneyLimit);
            if (TakeMoneyLimit > CurrentUser.Money)
                SubmitShow = false;
            else
                SubmitShow = true;
            user_bank = B_Lebi_User_Bank.GetList("User_id=" + CurrentUser.id + "", "id desc");
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
                        str = Tag("提现") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}