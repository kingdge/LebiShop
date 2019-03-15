using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LB.Tools;
using Shop.Model;using DB.LebiShop;
namespace Shop.Bussiness
{
    public class SiteMap
    {
        public static List<PageInfo> url
        {
            get
            {
                Lebi_Theme_Page themepage;
                Lebi_Theme theme;
                List<Lebi_Language> langs = Language.AllLanguages();
                List<PageInfo> ps = new List<PageInfo>();
                PageInfo p = new PageInfo();
                //====================================
                //首页
                foreach (Lebi_Language lang in langs)
                {
                    p = new PageInfo();
                    p.loc = ThemeUrl.GetURL("P_Index", "", "", lang);
                    p.lastmod = System.DateTime.Now;
                    p.priority = "1";
                    p.changefreq = "";
                    ps.Add(p);
                }
                //====================================
                //P_About
                themepage = B_Lebi_Theme_Page.GetModel("Code='P_About'");
                Lebi_Node node = NodePage.GetNodeByCode("About");
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);

                    List<Lebi_Page> pages = B_Lebi_Page.GetList("Node_id=" + node.id + " and Language like '%" + lang.Code + "%'", "");
                    foreach (Lebi_Page page in pages)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_About", page.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);

                    }
                }
                //====================================
                //商品分类
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    p = new PageInfo();
                    p.loc = ThemeUrl.GetURL("P_AllProductCategories", "", "", lang);
                    p.lastmod = System.DateTime.Now;
                    p.priority = "1";
                    p.changefreq = "";
                    ps.Add(p);
                }
                //====================================
                //P_Article 文章列表
                node = NodePage.GetNodeByCode("Info");
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    p = new PageInfo();
                    List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + node.id + " and Language_ids like '%" + lang.id + "%'", "");
                    foreach (Lebi_Node n in nodes)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_Article", n.id.ToString() + ",1", "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_ArticleDetails 文章
                node = NodePage.GetNodeByCode("Info");
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    List<Lebi_Page> pages = B_Lebi_Page.GetList("Node_id in (select id from Lebi_Node where parentid=" + node.id + ") and Language like '%" + lang.Code + "%'", "");
                    foreach (Lebi_Page page in pages)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_ArticleDetails", page.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_Brand  品牌
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    List<Lebi_Brand> models = B_Lebi_Brand.GetList("", "");
                    foreach (Lebi_Brand model in models)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_Brand", model.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_FriendLink 友情链接
                foreach (Lebi_Language lang in langs)
                {
                    p = new PageInfo();
                    p.loc = ThemeUrl.GetURL("P_FriendLink", "", "", lang);
                    p.lastmod = System.DateTime.Now;
                    p.priority = "1";
                    p.changefreq = "";
                    ps.Add(p);
                }
                //====================================
                //P_Help 帮助中心
                node = NodePage.GetNodeByCode("Help");
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + node.id + " and Language_ids like '%" + lang.id + "%'", "");
                    foreach (Lebi_Node n in nodes)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_Help", n.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_Login 登录页面
                foreach (Lebi_Language lang in langs)
                {
                    p = new PageInfo();
                    p.loc = ThemeUrl.GetURL("P_Login", "", "", lang);
                    p.lastmod = System.DateTime.Now;
                    p.priority = "1";
                    p.changefreq = "";
                    ps.Add(p);
                }
                //====================================
                //P_News 商城动态
                node = NodePage.GetNodeByCode("News");
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    p = new PageInfo();
                    List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + node.id + " and Language_ids like '%" + lang.id + "%'", "");
                    foreach (Lebi_Node n in nodes)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_Article", n.id.ToString() + ",1", "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_NewsDetails 商城动态
                node = NodePage.GetNodeByCode("News");
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    p = new PageInfo();
                    List<Lebi_Page> pages = B_Lebi_Page.GetList("Node_id=" + node.id + " and Language like '%" + lang.Code + "%'", "");
                    foreach (Lebi_Page n in pages)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_NewsDetails", n.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_Product 商品详情
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    p = new PageInfo();
                    List<Lebi_Product> pros = B_Lebi_Product.GetList("Type_id_ProductStatus=101", "", 100, 1);
                    foreach (Lebi_Product pro in pros)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_Product", pro.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_ProductCategory 商品分类
                foreach (Lebi_Language lang in langs)
                {
                    theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    p = new PageInfo();
                    List<Lebi_Pro_Type> pros = B_Lebi_Pro_Type.GetList("", "");
                    foreach (Lebi_Pro_Type pro in pros)
                    {
                        p = new PageInfo();
                        p.loc = ThemeUrl.GetURL("P_ProductCategory", pro.id.ToString(), "", lang);
                        p.lastmod = System.DateTime.Now;
                        p.priority = "1";
                        p.changefreq = "";
                        ps.Add(p);
                    }
                }
                //====================================
                //P_Register 注册
                foreach (Lebi_Language lang in langs)
                {
                    p = new PageInfo();
                    p.loc = ThemeUrl.GetURL("P_Register", "", "", lang);
                    p.lastmod = System.DateTime.Now;
                    p.priority = "1";
                    p.changefreq = "";
                    ps.Add(p);
                }
                //====================================
                return ps;
            }
            set { }

        }

        /// <summary>
        /// 生成SiteMap字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateSiteMapString()
        {
            BaseConfig bc = ShopCache.GetBaseConfig();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"> ");
            string domain = WebConfig.Instrance.MainDomain;// bc.Domain;
            string page = "";
            foreach (PageInfo pi in url)
            {
                page = ShopCache.GetBaseConfig().HTTPServer + "://" + domain + pi.loc;
                page = page.Replace("&", "&amp;");
                sb.AppendLine("<url>");
                sb.AppendLine(string.Format("<loc>{0}</loc>", page));
                sb.AppendLine(string.Format("<lastmod>{0}</lastmod> ", pi.lastmod.ToString("yyyy-MM-dd")));
                //sb.AppendLine(string.Format("<changefreq>{0}</changefreq> ", pi.changefreq));
                sb.AppendLine(string.Format("<priority>{0}</priority> ", pi.priority));
                sb.AppendLine("</url>");
            }

            sb.AppendLine("</urlset>");
            return sb.ToString();
        }

        /// <summary>
        /// 保存Site文件
        /// </summary>
        /// <param name=”FilePath”>路径</param>
        public static void SaveSiteMap(string FilePath)
        {
            HtmlEngine save = new HtmlEngine();
            save.CreateFile(FilePath, GenerateSiteMapString());
            //System.IO.File.Write(FilePath, GenerateSiteMapString());//保存在指定目录下
        }
    }

    public class PageInfo
    {
        /// <summary>
        /// 网址
        /// </summary>
        public string loc { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime lastmod { get; set; }

        /// <summary>
        /// 更新频繁程度
        /// </summary>
        public string changefreq { get; set; }

        /// <summary>
        /// 优先级，0-1
        /// </summary>
        public string priority { get; set; }
    }
}