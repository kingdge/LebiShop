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

    public class P_UserAddressEdit : ShopPageUser
    {
        protected int id;
        protected Lebi_User_Address user_address;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserAddressEdit'");
            id = Rint("id");
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserAddress", "") + "\"><span>" + Tag("收货地址") + "</span></a>";
            if (id > 0)
            {
                path += "<em>&raquo;</em><a href=\"" + URL("P_UserAddressEdit", "" + id + "") + "\"><span>" + Tag("编辑") + "</span></a>";
            }
            else
            {
                path += "<em>&raquo;</em><a href=\"" + URL("P_UserAddressEdit", "0") + "\"><span>" + Tag("添加") + "</span></a>";
            }
            user_address = B_Lebi_User_Address.GetModel("User_id=" + CurrentUser.id + " and id = " + id + "");
            if (user_address == null)
            {
                if (id == 0)
                {
                    user_address = new Lebi_User_Address();
                }
                else
                {
                    PageError();
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
                        str = Tag("收货地址") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}