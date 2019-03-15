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
    public class P_FindPassword : ShopPage
    {
        protected int id;
        protected int type;
        protected string v;
        protected List<Lebi_User_Answer> user_answers;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            id = RequestTool.RequestInt("id", 0);
            v = RequestTool.RequestString("v");
            type = RequestTool.RequestInt("type", 0);
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("Ê×Ò³") + "\"><span>" + Tag("Ê×Ò³") + "</span></a><em class=\"home\">&raquo;</em><a class=\"text\"><span>" + Tag("Íü¼ÇÃÜÂë") + "</span></a>";
            Lebi_User user = B_Lebi_User.GetModel("id=" + id + " and CheckCode = lbsql{'" + v + "'}");
            if (user == null)
            {
                id = 0;
                type = 0;
            }
            user_answers = B_Lebi_User_Answer.GetList("User_id=" + id + "", "id asc", 20, 1);
            if (user_answers.Count == 0)
            {
                id = 0;
                type = 0;
            }
            EX_User.CheckForgetPWD(CurrentTheme, CurrentLanguage, type);
        }
        public string QuestionName(int id)
        {
            Lebi_User_Question model = B_Lebi_User_Question.GetModel(id);
            if (model != null)
                return Lang(model.Name);
            return "";
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
                        str = Tag("Íü¼ÇÃÜÂë");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}