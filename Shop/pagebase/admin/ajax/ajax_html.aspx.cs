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
    /// <summary>
    ///  生成静态页相关
    /// </summary>
    public partial class ajax_html : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        #region 批量生成HTML
        public void CreatePage()
        {
            int id = RequestTool.RequestInt("id", 0);
            string languageid = RequestTool.RequestString("Language");
            Lebi_Theme_Page model = B_Lebi_Theme_Page.GetModel(id);
            string Path = "";
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Path = model.StaticPath;
            string urlpath = "http://" + HttpContext.Current.Request.Url.Authority + WebPath;
            //string port=
            List<Lebi_Language> langs;
            if (languageid == "")
                langs = Language.AllLanguages();
            else
                langs = B_Lebi_Language.GetList("id in (lbsql{" + languageid + "})", "");
            foreach (Lebi_Language lang in langs)
            {
                Lebi_Theme theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                if (theme == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("语言与模板关联错误") + "\"}");
                    return;
                }
            }
            switch (model.Code)
            {
                case "P_About":
                    P_About(langs, model, urlpath);
                    break;
                case "P_ArticleDetails":
                    P_ArticleDetails(langs, model, urlpath);
                    break;
                case "P_Help":
                    P_Help(langs, model, urlpath);
                    break;
                case "P_NewsDetails":
                    P_NewsDetails(langs, model, urlpath);
                    break;
                case "P_Product":
                    P_Product(langs, model, urlpath);
                    break;
                default:
                    OnePage(langs, model, urlpath);
                    break;
            }


        }
        /// <summary>
        /// 单个页面
        /// </summary>
        /// <param name="lang"></param>
        private void OnePage(List<Lebi_Language> langs, Lebi_Theme_Page model, string urlpath)
        {
            string url = "";
            string file = "";
            List<Lebi_Site> sites = GetSites();
            Lebi_Site site;
            foreach (Lebi_Language lang in langs)
            {
                site = GetSite(sites, lang.Site_id);

                file = site.Path + "/" + lang.Path + "/" + model.StaticPath + "/" + model.StaticPageName;
                file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                file = ThemeUrl.CheckPath(file);
                url = urlpath + "/" + site.Path + "/" + lang.Path + "/" + model.PageName;
                url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                url = ThemeUrl.CheckURL(url);
                HtmlEngine.Instance.CreatHtml(url, file);
            }
            Response.Write("{\"msg\":\"OK\",\"per\":\"100\"}");
        }
        /// <summary>
        /// 关于我们页面
        /// </summary>
        private void P_About(List<Lebi_Language> langs, Lebi_Theme_Page model, string urlpath)
        {
            string url;
            string file = "";
            int pageindex = RequestTool.RequestInt("pageindex", 0);
            int pagesize = RequestTool.RequestInt("pagesize", 0);
            Lebi_Node node = NodePage.GetNodeByCode("About");
            string where = "Node_id=" + node.id + "";
            List<Lebi_Page> pages = B_Lebi_Page.GetList(where, "", pagesize, pageindex);
            int count = B_Lebi_Page.Counts(where);
            int pagecount = Pager.GetPageCount(pagesize, count);
            int per = 100;
            if (pagecount > 0)
                per = Convert.ToInt32(pageindex * 100 / pagecount);
            List<Lebi_Site> sites = GetSites();
            Lebi_Site site;
            foreach (Lebi_Page page in pages)
            {
                foreach (Lebi_Language lang in langs)
                {
                    if (page.Language.ToLower().Contains(lang.Code.ToLower()))
                    {
                        site = GetSite(sites, lang.Site_id);
                        file = site.Path + "/" + lang.Path + "/" + model.StaticPath + "/" + model.StaticPageName;
                        file = file.Replace("{0}", page.id.ToString());
                        file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                        file = ThemeUrl.CheckPath(file);

                        url = model.PageName + "?" + model.PageParameter;
                        url = url.Replace("{0}", page.id.ToString());
                        url = urlpath + "/" + site.Path + "/" + lang.Path + "/" + url;
                        url = ThemeUrl.CheckURL(url);
                        HtmlEngine.Instance.CreatHtml(url, file);
                    }
                }
            }
        }
        /// <summary>
        /// 文章列表页面
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="model"></param>
        /// <param name="theme"></param>
        /// <param name="urlpath"></param>
        private void P_Article(Lebi_Language lang, Lebi_Theme_Page model, Lebi_Theme theme, string urlpath)
        {
            string url;
            string nodeids = RequestTool.RequestString("node");
            if (nodeids == null)
                return;
            string path = lang.Path + "/" + model.StaticPath + "/" + model.StaticPageName;
            string pathto = "";
            List<Lebi_Node> nodes = B_Lebi_Node.GetList("id in (lbsql{" + nodeids + "}) and Language_Code like '%" + lang.Code + "%'", "");
            foreach (Lebi_Node node in nodes)
            {
                url = model.PageName + "?" + model.PageParameter;
                url = url.Replace("{0}", node.id.ToString());
                url = urlpath + "/" + url;
                pathto = path.Replace("{0}", node.id.ToString());
                url = ThemeUrl.CheckURL(url);
                HtmlEngine.Instance.CreatHtml(url, pathto);
            }
        }
        /// <summary>
        /// 文章查看页面
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="model"></param>
        /// <param name="theme"></param>
        /// <param name="urlpath"></param>
        private void P_ArticleDetails(List<Lebi_Language> langs, Lebi_Theme_Page model, string urlpath)
        {
            string url;
            string file = "";
            DateTime time1 = RequestTool.RequestTime("time1");
            DateTime time2 = RequestTool.RequestTime("time2");
            string nodeids = RequestTool.RequestString("node");
            if (nodeids == null)
                return;
            int pageindex = RequestTool.RequestInt("pageindex", 0);
            int pagesize = RequestTool.RequestInt("pagesize", 0);
            string where = "Time_Add>='" + time1 + "' and Time_Add<='" + time2 + "'";
            if (nodeids != "")
                where += " and Node_id in (" + nodeids + ") ";
            List<Lebi_Page> pages = B_Lebi_Page.GetList(where, "", pagesize, pageindex);
            int count = B_Lebi_Page.Counts(where);
            int pagecount = Pager.GetPageCount(pagesize, count);
            int per = 100;
            if (pagecount > 0)
                per = Convert.ToInt32(pageindex * 100 / pagecount);
            List<Lebi_Site> sites = GetSites();
            Lebi_Site site;
            foreach (Lebi_Page page in pages)
            {
                foreach (Lebi_Language lang in langs)
                {
                    if (page.Language.ToLower().Contains(lang.Code.ToLower()))
                    {
                        site = GetSite(sites, lang.Site_id);
                        file = site.Path + "/" + lang.Path + "/" + model.StaticPath + "/" + model.StaticPageName;
                        file = file.Replace("{0}", page.id.ToString());
                        file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                        file = ThemeUrl.CheckPath(file);

                        url = model.PageName + "?" + model.PageParameter;
                        url = url.Replace("{0}", page.id.ToString());
                        url = urlpath + "/" + site.Path + "/" + lang.Path + "/" + url;
                        url = ThemeUrl.CheckURL(url);
                        HtmlEngine.Instance.CreatHtml(url, file);
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\",\"per\":\"" + per + "\"}");
        }
        /// <summary>
        /// 品牌页面
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="model"></param>
        /// <param name="theme"></param>
        /// <param name="urlpath"></param>
        private void P_Brand(Lebi_Language lang, Lebi_Theme_Page page, Lebi_Theme theme, string urlpath)
        {
            string url;
            string path = lang.Path + "/" + page.StaticPath + "/" + page.StaticPageName;
            string file = "";
            List<Lebi_Brand> models = B_Lebi_Brand.GetList("", "");
            foreach (Lebi_Brand model in models)
            {
                url = page.PageName + "?" + page.PageParameter;
                url = url.Replace("{0}", model.id.ToString());
                url = urlpath + "/" + url;
                file = path.Replace("{0}", model.id.ToString());
                file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                url = ThemeUrl.CheckURL(url);
                HtmlEngine.Instance.CreatHtml(url, file);
            }
        }
        /// <summary>
        /// 帮助中心页面
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="model"></param>
        /// <param name="theme"></param>
        /// <param name="urlpath"></param>
        private void P_Help(List<Lebi_Language> langs, Lebi_Theme_Page model, string urlpath)
        {
            string url;
            Lebi_Node pnode = NodePage.GetNodeByCode("Help");
            string file = "";
            int pageindex = RequestTool.RequestInt("pageindex", 0);
            int pagesize = RequestTool.RequestInt("pagesize", 0);
            string where = "parentid=" + pnode.id + "";
            List<Lebi_Node> nodes = B_Lebi_Node.GetList(where, "", pagesize, pageindex);
            int count = B_Lebi_Node.Counts(where);
            int pagecount = Pager.GetPageCount(pagesize, count);
            int per = 100;
            if (pagecount > 0)
                per = Convert.ToInt32(pageindex * 100 / pagecount);
            List<Lebi_Site> sites = GetSites();
            Lebi_Site site;
            foreach (Lebi_Node node in nodes)
            {
                foreach (Lebi_Language lang in langs)
                {
                    if (node.Language.ToLower().Contains(lang.Code.ToLower()))
                    {
                        site = GetSite(sites, lang.Site_id);
                        file = site.Path + "/" + lang.Path + "/" + model.StaticPath + "/" + model.StaticPageName;
                        file = file.Replace("{0}", node.id.ToString());
                        file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                        file = ThemeUrl.CheckPath(file);

                        url = model.PageName + "?" + model.PageParameter;
                        url = url.Replace("{0}", node.id.ToString());
                        url = urlpath + "/" + site.Path + "/" + lang.Path + "/" + url;
                        url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                        url = ThemeUrl.CheckURL(url);
                        HtmlEngine.Instance.CreatHtml(url, file);
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\",\"per\":\"" + per + "\"}");
        }
        /// <summary>
        /// 新闻详情页面
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="model"></param>
        /// <param name="theme"></param>
        /// <param name="urlpath"></param>
        private void P_NewsDetails(List<Lebi_Language> langs, Lebi_Theme_Page model, string urlpath)
        {
            string url;
            Lebi_Node pnode = NodePage.GetNodeByCode("News");
            DateTime time1 = RequestTool.RequestTime("time1");
            DateTime time2 = RequestTool.RequestTime("time2");
            int pageindex = RequestTool.RequestInt("pageindex", 0);
            int pagesize = RequestTool.RequestInt("pagesize", 0);
            string file = "";
            string where = "Node_id=" + pnode.id + "  and Time_Add>='" + time1 + "' and Time_Add<='" + time2 + "'";
            List<Lebi_Page> pages = B_Lebi_Page.GetList(where, "", pagesize, pageindex);
            int count = B_Lebi_Page.Counts(where);
            int pagecount = Pager.GetPageCount(pagesize, count);
            int per = 100;
            if (pagecount > 0)
                per = Convert.ToInt32(pageindex * 100 / pagecount);
            List<Lebi_Site> sites = GetSites();
            Lebi_Site site;
            foreach (Lebi_Page page in pages)
            {
                foreach (Lebi_Language lang in langs)
                {
                    if (page.Language.ToLower().Contains(lang.Code.ToLower()))
                    {
                        site = GetSite(sites, lang.Site_id);
                        file = site.Path + "/" + lang.Path + "/" + model.StaticPath + "/" + model.StaticPageName;
                        file = file.Replace("{0}", page.id.ToString());
                        file = ThemeUrl.CheckPath(file);

                        url = model.PageName + "?" + model.PageParameter;
                        url = url.Replace("{0}", page.id.ToString());
                        url = urlpath + "/" + site.Path + "/" + lang.Path + "/" + url;
                        url = ThemeUrl.CheckURL(url);
                        HtmlEngine.Instance.CreatHtml(url, file);
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\",\"per\":\"" + per + "\"}");
        }
        /// <summary>
        /// 商品页面
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="model"></param>
        /// <param name="theme"></param>
        /// <param name="urlpath"></param>
        private void P_Product(List<Lebi_Language> langs, Lebi_Theme_Page page, string urlpath)
        {

            string url;
            string ids = RequestTool.RequestString("Pro_Type_id");
            DateTime time1 = RequestTool.RequestTime("time1");
            DateTime time2 = RequestTool.RequestTime("time2");

            string file = "";
            int pageindex = RequestTool.RequestInt("pageindex", 0);
            int pagesize = RequestTool.RequestInt("pagesize", 0);
            string where = "Time_Add>='" + time1 + "' and Time_Add<='" + time2 + "'";
            if (ids != "")
                where += " and Pro_Type_id in (lbsql{" + ids + "})";
            List<Lebi_Product> models = B_Lebi_Product.GetList(where, "", pagesize, pageindex);
            int count = B_Lebi_Product.Counts(where);
            int pagecount = Pager.GetPageCount(pagesize, count);
            int per = 100;
            if (pagecount > 0)
                per = Convert.ToInt32(pageindex * 100 / pagecount);
            List<Lebi_Site> sites = GetSites();
            Lebi_Site site;
            foreach (Lebi_Product model in models)
            {
                foreach (Lebi_Language lang in langs)
                {
                    site = GetSite(sites, lang.Site_id);
                    file = site.Path + "/" + lang.Path + "/" + page.StaticPath + "/" + page.StaticPageName;
                    file = file.Replace("{0}", model.id.ToString());
                    file = RegexTool.ReplaceRegValue(file, @"{.*?}", "");
                    file = ThemeUrl.CheckPath(file);

                    url = page.PageName + "?" + page.PageParameter;
                    url = url.Replace("{0}", model.id.ToString());
                    url = urlpath + "/" + site.Path + "/" + lang.Path + "/" + url;
                    url = RegexTool.ReplaceRegValue(url, @"{.*?}", "");
                    url = ThemeUrl.CheckURL(url);
                    try
                    {
                        HtmlEngine.Instance.CreatHtml(url, file);
                    }
                    catch (System.Net.WebException)
                    {
                        //Log.Add(url + "---------" + file);
                        continue;
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\",\"per\":\"" + per + "\"}");
        }
        #endregion
        /// <summary>
        /// 全部站点
        /// </summary>
        /// <returns></returns>
        public List<Lebi_Site> GetSites()
        {
            List<Lebi_Site> sites = B_Lebi_Site.GetList("", "");
            return sites;
        }
        public Lebi_Site GetSite(List<Lebi_Site> sites, int id)
        {
            Lebi_Site model = (from m in sites
                               where m.id == id
                               select m).ToList().FirstOrDefault();
            return model;
        }
        #region  计算数据数量
        public int OnePage_Count(List<Lebi_Language> langs)
        {
            return langs.Count;
        }
        /// <summary>
        /// 要生成的商品的数据
        /// </summary>
        /// <returns></returns>
        public int P_Product_Count()
        {
            string ids = RequestTool.RequestString("Pro_Type_id");
            DateTime time1 = RequestTool.RequestTime("time1");
            DateTime time2 = RequestTool.RequestTime("time2");
            int c = B_Lebi_Product.Counts("Pro_Type_id in (lbsql{" + ids + "}) and Time_Add>='" + time1 + "' and Time_Add<='" + time2 + "'");
            return c;
        }
        #endregion
    }
}
