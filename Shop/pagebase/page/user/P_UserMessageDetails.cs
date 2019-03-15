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

    public class P_UserMessageDetails : ShopPageUser
    {
        protected Lebi_Message message;
        protected int id;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_UserMessageDetails'");
            id = Rint("id");
            message = B_Lebi_Message.GetModel("(User_id_To=" + CurrentUser.id + " or User_id_From=" + CurrentUser.id + ") and id = " + id + "");
            if (message == null)
            {
                PageError();
            }
            else
            {
                if (message.User_id_From == 0 && message.IsRead == 0)
                {
                    message.IsRead = 1;
                    B_Lebi_Message.Update(message);
                }
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em>";
            if (message.User_id_From == 0)
                path += "<a href=\"" + URL("P_UserMessage", "0") + "\"><span>" + Tag("收件箱") + "</span></a>";
            else
                path += "<a href=\"" + URL("P_UserMessage", "1") + "\"><span>" + Tag("发件箱") + "</span></a>";
            path += "<em>&raquo;</em><a class=\"text\"><span>" + message.Title + "</span></a>";
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
                        str = message.Title + " - " + Tag("站内信") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}