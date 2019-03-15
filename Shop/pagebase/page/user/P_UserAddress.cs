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

    public class P_UserAddress : ShopPageUser
    {
        protected List<Lebi_User_Address> user_addresss;
        protected string PageString;
        protected string key;
        protected string where;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserAddress", "") + "\"><span>" + Tag("收货地址") + "</span></a>";

            key = Rstring("key");
            pageindex = RequestTool.RequestInt("page", 1);
            where = "User_id=" + CurrentUser.id + "";
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Address like lbsql{'%" + key + "%'} or MobilePhone like lbsql{'%" + key + "%'} or Phone like lbsql{'%" + key + "%'})";
            user_addresss = B_Lebi_User_Address.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_User_Address.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&key=" + key, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = "?page=" + (pageindex + 1) + "&key=" + key + "";
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
                        str = Tag("收货地址") + " - " + Tag("会员中心") ;
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}