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

    public class P_Recharge : ShopPageUser
    {
        public Lebi_Currency DefaultCurrency;
        protected List<Lebi_OnlinePay> onlinepays;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserMoney", "") + "\"><span>" + Tag("资金记录") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserChangePassword", "") + "\"><span>" + Tag("充值") + "</span></a>";
            DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (DefaultCurrency == null)
                DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();
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
            if (CurrentUser.OnlinePay_id == 0)
            {
                CurrentUser.OnlinePay_id = onlinepays.FirstOrDefault().id;
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
                        str = Tag("充值") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}