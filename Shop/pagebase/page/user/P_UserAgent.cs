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

    public class P_UserAgent : ShopPageUser
    {
        protected decimal Angent1_Commission = 0;
        protected decimal Angent2_Commission = 0;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("plugin_agent"))
            {
                Response.Write(Tag("权限不足"));
                Response.End();
            }
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            BaseConfig bc = ShopCache.GetBaseConfig();
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Tag("推广佣金") + "</span></a>";
            decimal.TryParse(bc.Angent1_Commission, out Angent1_Commission);
            decimal.TryParse(bc.Angent2_Commission, out Angent2_Commission);
            Lebi_Agent_User agent_user = B_Lebi_Agent_User.GetModel("User_id = " + CurrentUser.id + "");
            if (agent_user != null)
            {
                if (agent_user.Angent1_Commission != -1)
                    Angent1_Commission = agent_user.Angent1_Commission;
                if (agent_user.Angent2_Commission != -1)
                    Angent2_Commission = agent_user.Angent2_Commission;
            }
            Lebi_Agent_User agent_userlevel = B_Lebi_Agent_User.GetModel("User_id = " + CurrentUser.UserLevel_id + "");
            if (agent_userlevel != null)
            {
                if (agent_userlevel.Angent1_Commission != -1)
                    Angent1_Commission = agent_userlevel.Angent1_Commission;
                if (agent_userlevel.Angent2_Commission != -1)
                    Angent2_Commission = agent_userlevel.Angent2_Commission;
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
                        str = Tag("推广佣金") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}