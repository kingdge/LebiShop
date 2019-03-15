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

    public class P_UserAgent : ShopPageUser
    {
        protected decimal money;//可提现金额
        protected int UserCount = 0;
        protected int UserCountday = 0;
        protected int UserCountmonth = 0;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (!Shop.Bussiness.B_API.Check("plugin_agent"))
            {
                Response.Write(Tag("权限不足"));
                Response.End();
            }
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Tag("推广佣金") + "</span></a>";
            string money_ = Common.GetValue("select sum(Money) from Lebi_Agent_Money where User_id=" + CurrentUser.id + " and Type_id_AgentMoneyStatus=382 and datediff(d,Time_add,'" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')>" + SYS.CommissionMoneyDays + "");
            decimal.TryParse(money_, out money);

            UserCount = B_Lebi_User.Counts("User_id_parent=" + CurrentUser.id + "");
            UserCountmonth = B_Lebi_User.Counts("User_id_parent=" + CurrentUser.id + " and Time_Reg>'" + System.DateTime.Now.Date.AddDays(0 - System.DateTime.Now.Day).ToString("yyyy-MM-dd") + "'");
            UserCountday = B_Lebi_User.Counts("User_id_parent=" + CurrentUser.id + " and Time_Reg>'" + System.DateTime.Now.Date.ToString("yyyy-MM-dd") + "'");
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
                        str = Tag("推广佣金") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}