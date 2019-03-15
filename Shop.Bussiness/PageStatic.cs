using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.Web;

namespace Shop.Bussiness
{
    public class PageStatic
    {
        /// <summary>
        /// 关于我们页面/新闻/文章
        /// </summary>
        public static bool Greate_InfoPage(Lebi_Page model, Lebi_Theme_Page page)
        {
            Site site = new Site();
            string[] langcodes = model.Language.Split(',');
            string urlpath = "http://" + HttpContext.Current.Request.Url.Authority + site.WebPath;
            string url = "";
            string file = "";
            foreach (string langcode in langcodes)
            {
                Lebi_Language lang = B_Lebi_Language.GetModel("Code='" + langcode + "'");
                string path = lang.Path + "/" + page.StaticPath + "/" + page.StaticPageName;
                url = page.PageName + "?" + page.PageParameter;
                url = url.Replace("{0}", model.id.ToString());
                url = urlpath + "/" + url;
                file = path.Replace("{0}", model.id.ToString());
                file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                url = ThemeUrl.CheckURL(url);
                HtmlEngine.Instance.CreatHtml(url, file);
            }
            return true;
        }
        /// <summary>
        /// 商品分类页面
        /// </summary>
        public static bool Greate_OnePage(Lebi_Theme_Page page)
        {
            Site site = new Site();
            //Lebi_Theme_Page page = B_Lebi_Theme_Page.GetModel("Code='P_AllProductCategories'");
            string urlpath = "http://" + HttpContext.Current.Request.Url.Authority + site.WebPath;
            string url = "";
            string file = "";
            foreach (Lebi_Language lang in Language.AllLanguages())
            {
                string path = lang.Path + "/" + page.StaticPath + "/" + page.StaticPageName;
                url = page.PageName + "?" + page.PageParameter;
                url = urlpath + "/" + url;
                file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                url = ThemeUrl.CheckURL(url);
                HtmlEngine.Instance.CreatHtml(url, file);
            }
            return true;
        }
        /// <summary>
        /// 生成帮助中心
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool Greate_Help(Lebi_Theme_Page page)
        {
            string url;
            Site site = new Site();
            Lebi_Node pnode = NodePage.GetNodeByCode("Help");
            List<Lebi_Language> langs = Language.AllLanguages();
            string urlpath = "http://" + HttpContext.Current.Request.Url.Authority + site.WebPath;
            foreach (Lebi_Language lang in langs)
            {
                string path = lang.Path + "/" + page.StaticPath + "/" + page.StaticPageName;
                string file = "";
                List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + pnode.id + " and Language_Code like '%" + lang.Code + "%'", "");
                foreach (Lebi_Node node in nodes)
                {
                    url = page.PageName + "?" + page.PageParameter;
                    url = url.Replace("{0}", node.id.ToString());
                    url = urlpath + "/" + url;
                    file = path.Replace("{0}", node.id.ToString());
                    file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                    url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                    url = ThemeUrl.CheckURL(url);
                    HtmlEngine.Instance.CreatHtml(url, file);
                }
            }
            return true;
        }
        /// <summary>
        /// 商品页面
        /// </summary>
        public static bool Greate_Product(Lebi_Product model, Lebi_Theme_Page page)
        {
            Site site = new Site();
            
            string urlpath = "http://" + HttpContext.Current.Request.Url.Authority + site.WebPath;
            string url = "";
            string file = "";
            foreach (Lebi_Language lang in Language.AllLanguages())
            {
                string path = lang.Path + "/" + page.StaticPath + "/" + page.StaticPageName;
                url = page.PageName + "?" + page.PageParameter;
                url = url.Replace("{0}", model.id.ToString());
                url = urlpath + "/" + url;
                file = path.Replace("{0}", model.id.ToString());
                file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                url = ThemeUrl.CheckURL(url);
                HtmlEngine.Instance.CreatHtml(url, file);
            }
            return true;
        }
    }

}

