using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.IO;

namespace Shop.Admin.Ajax
{
    public partial class Ajax_aspx : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }

        /// <summary>
        /// 生成页面皮肤
        /// 针对单个皮肤生成的情况
        /// </summary>
        /// <returns></returns>
        public void CreateSkin()
        {
            string Msg = "";
            int ThemeID = 0;
            int SkinID = 0;
            string Content = "";
            string SkinPath = "";//皮肤路径
            string Path = "";  //输入Path要包含路径及文件名
            SkinID = RequestTool.RequestInt("id", 0);
            Lebi_Theme_Skin skin = B_Lebi_Theme_Skin.GetModel(SkinID);
            Lebi_Theme theme;
            if (skin == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;

            }
            ThemeID = skin.Theme_id;
            theme = B_Lebi_Theme.GetModel(ThemeID);
            SkinPath = theme.Path_Files + "/" + skin.Path_Skin;
            SkinPath = ThemeUrl.GetFullPath(SkinPath);
            Content = HtmlEngine.ReadTxt(SkinPath);
            List<Lebi_Language> langs = B_Lebi_Language.GetList("Theme_id=" + theme.id + "", "");
            if (langs.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("请在站点语言设置中关联此模板") + "\"}");
                return;
            }
            Site site = new Site();
            foreach (Lebi_Language lang in langs)
            {
                if (lang.Theme_id != ThemeID)
                    continue;
                Lebi_Site s = B_Lebi_Site.GetModel(lang.Site_id);
                if (s == null)
                    Path = lang.Path + "/" + skin.PageName;
                else
                    Path = s.Path + lang.Path + "/" + skin.PageName;
                Msg = Shop.Bussiness.Theme.CreatAspx(s,lang, theme, skin, Path, Content);

            }
            Response.Write("{\"msg\":\"" + Msg + "\"}");
        }

        /// <summary>
        /// 生成页面皮肤
        /// 针对生成整个主题
        /// </summary>
        /// <returns></returns>
        public void CreateTheme()
        {
            int Theme_id = RequestTool.RequestInt("Theme_id", 0);
            Lebi_Theme theme = B_Lebi_Theme.GetModel(Theme_id);
            List<Lebi_Language> langs = B_Lebi_Language.GetList("Theme_id=" + theme.id + "", "");
            if (langs.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("请在站点语言设置中关联此模板") + "\"}");
                return;
            }
            string Msg = "";
            Site site = new Site();
            foreach (Lebi_Language lang in langs)
            {
                if (lang.Theme_id != theme.id)
                    continue;
                Msg = Shop.Bussiness.Theme.CreateThemeByLanguage(lang, theme);
            }
            Response.Write("{\"msg\":\"" + Msg + "\"}");

        }
        /// <summary>
        /// 生成页面皮肤
        /// 针对生成整个主题-针对一个语言
        /// </summary>
        /// <returns></returns>
        public void CreateLanguageTheme()
        {
            int Language_id = RequestTool.RequestInt("Language_id", 0);

            Lebi_Language lang = B_Lebi_Language.GetModel(Language_id);
            Lebi_Theme theme = B_Lebi_Theme.GetModel(lang.Theme_id);
            if (theme == null)
            {
                Response.Write("{\"msg\":\"" + Tag("未关联任何模板") + "\"}");
                return;
            }
            string Msg = "";
            Site site = new Site();
            Msg = Shop.Bussiness.Theme.CreateThemeByLanguage(lang, theme);
            Response.Write("{\"msg\":\"" + Msg + "\"}");

        }


        #region 正则表达式相关函数
        /// <summary>
        /// 根据正则提取内容
        /// </summary>
        /// <param name="content">源字符串</param>
        /// <param name="pattern">正则</param>
        /// <returns></returns>
        private string GetRegValue(string content, string pattern)
        {
            string res = "";
            Regex r = new Regex("" + pattern + "", RegexOptions.Singleline);

            MatchCollection mc = r.Matches(content);
            foreach (Match m in mc)
            {
                res = m.Result("$1");
                //res = m.Value;
            }
            return res;
        }
        /// <summary>
        /// 根据正则获得匹配内容的个数
        /// </summary>
        /// <param name="content">源字符串</param>
        /// <param name="pattern">正则</param>
        /// <returns></returns>
        private int GetRegCount(string content, string pattern)
        {
            int res = 0;
            Regex r = new Regex("" + pattern + "", RegexOptions.Singleline);
            res = r.Matches(content).Count;
            return res;
        }
        /// <summary>
        /// 正则表达式匹配数组
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private string[,] GetRegArry(string content, string pattern)
        {
            Regex r = new Regex("" + pattern + "", RegexOptions.Singleline);
            MatchCollection mc = r.Matches(content);

            string[,] Arry = new string[mc.Count + 1, 2];
            int i = 0;
            foreach (Match m in mc)
            {
                //res += m.Index + "-" + m.Result("$1");
                Arry[i, 0] = m.Index.ToString();
                Arry[i, 1] = m.Result("$1");
                i++;
            }
            return Arry;
        }
        /// <summary>
        /// 过滤正则内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private string ReplaceRegValue(string content, string pattern)
        {
            string res = "";
            Regex r = new Regex("" + pattern + "", RegexOptions.Singleline);
            res = r.Replace(content, "");
            return res;
        }
        #endregion
    }
}
