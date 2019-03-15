using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LB.Tools;
using Shop.Model;using DB.LebiShop;
using System.Collections.Specialized;
namespace Shop.Bussiness
{
    /// <summary>
    /// 主题路径
    /// </summary>
    public class ThemeUrl
    {
        /// <summary>
        /// id=1&pid=1 转化为 Page_{id}_{pid}.html 形式
        /// </summary>
        /// <param name="ParaStr">参数字符串</param>
        /// <param name="FormatStr">格式字符串</param>
        /// <returns></returns>
        public static string StaticUrl(string ParaStr, string FormatStr)
        {
            ParaStr = ("&" + ParaStr + "&").ToLower();
            string str = FormatStr.ToLower();//小写
            string[,] Arry = RegexTool.GetRegResultArray(str, @"{(.*?)}");
            for (int i = 0; i < Arry.GetUpperBound(0); i++)
            {
                string d = RegexTool.GetRegValue(ParaStr, @"&" + Arry[i, 1] + "=(.*?)&");
                str = str.Replace("{" + Arry[i, 1] + "}", d);
            }
            return str;
        }

        public static string ConvertURL(string ParaStr, string FormatStr, int t, string PageName, string PageParameter)
        {
            ParaStr = ParaStr.Replace("{0}", "[pageindex]");
            string[] arr = ParaStr.Split(',');
            int arrcount = arr.Length;
            string res = "";
            if (t == 121)//动态地址
            {
                if (PageParameter != "")
                {
                    string[] parr = PageParameter.Split('&');
                    for (int i = 0; i < arrcount; i++)
                    {
                        if (i >= parr.Length)
                            break;
                        if (arr[i] != "$" && arr[i] != "")
                        {
                            if (res == "")
                                res = parr[i].Replace("{" + i + "}", arr[i]);
                            else
                                res += "&" + parr[i].Replace("{" + i + "}", arr[i]);
                        }
                    }
                    if (res == "")
                        res = PageName;
                    else
                        res = PageName + "?" + res;
                }
                else
                    res = FormatStr;
            }
            else
            {
                res = FormatStr;
                if (arrcount > 0)
                {
                    for (int i = 0; i < arrcount; i++)
                    {
                        if (arr[i] == "$")
                            arr[i] = "";
                        res = res.Replace("{" + i + "}", arr[i]);
                    }

                }
                res = RegexTool.ReplaceRegValue(res, @"{\d*}", "");

            }
            res = res.Replace("[pageindex]", "{0}");
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">页面标记的代码</param>
        /// <param name="ParaStr">参数字符串</param>
        /// <param name="url">跳转地址</param>
        /// <param name="theme">主题</param>
        /// <returns></returns>
        public static string GetURL(string code, string ParaStr, string url, Lebi_Site site, Lebi_Language lang)
        {
            code = code.Trim();
            if (url != "" && url != null)
            {
                if (url.ToLower().IndexOf("http") == 0)
                    return url;
                if (url.Contains(":"))
                {
                    string[] arr = url.Split(':');
                    return GetURL(arr[0], arr[1], "", site, lang);
                }
                return url;
            }
            List<Lebi_Theme_Page> nodes = ShopCache.GetThemePage();
            Lebi_Theme_Page node = (from m in nodes
                                    where m.Code == code
                                    select m).ToList().FirstOrDefault();


            // Lebi_Theme_Page node = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
            if (node == null)
                return "";

            //Lebi_Theme_Skin skin = B_Lebi_Theme_Skin.GetList("Theme_id=" + theme.id + " and Code='" + code + "'", "").FirstOrDefault();
            //if (skin == null)
            //    return "";
            string res = "";
            string PageName = "";
            switch (node.Type_id_PublishType)
            {
                case 120:
                    res = "";
                    break;
                case 121://动态地址
                    //if (skin.PageName == "")
                    PageName = node.PageName;
                    //else
                    //    PageName = skin.PageName;
                    if (node.PageParameter == "")
                    {
                        res = "/" + PageName;
                    }
                    else
                    {
                        res = "/" + PageName + "?" + node.PageParameter;
                        res = ConvertURL(ParaStr, res, 121, PageName, node.PageParameter);
                    }

                    break;
                case 122://静态地址
                    if (node.StaticPageName != "")
                    {
                        res = "/" + node.StaticPath + "/" + node.StaticPageName;
                        res = ConvertURL(ParaStr, res, 122, "", "");
                    }
                    else
                    {
                        //没有设置静态参数时按照动态地址发布
                        PageName = node.PageName;
                        if (node.PageParameter == "")
                        {
                            res = "/" + PageName;
                        }
                        else
                        {
                            res = "/" + PageName + "?" + node.PageParameter;
                            res = ConvertURL(ParaStr, res, 121, PageName, node.PageParameter);
                        }
                    }
                    break;
                case 123://伪静态地址
                    if (node.StaticPageName != "")
                    {
                        res = "/" + node.StaticPath + "/" + node.StaticPageName;
                        res = ConvertURL(ParaStr, res, 123, "", "");
                    }
                    else
                    {
                        //没有设置伪静态参数时按照动态地址发布
                        PageName = node.PageName;
                        if (node.PageParameter == "")
                        {
                            res = "/" + PageName;
                        }
                        else
                        {
                            res = "/" + PageName + "?" + node.PageParameter;
                            res = ConvertURL(ParaStr, res, 121, PageName, node.PageParameter);
                        }
                    }
                    break;

            }
            //if (ShopCache.GetMainSite().id != site.id && site.Path != "")
            //    res = Site.Instance.WebPath + "/" + site.Path + "/" + lang.Path + "/" + res;
            //else
            res = Site.Instance.WebPath + "/" + lang.Path + "/" + res;
            //else
            //{
            //    lang.Path = lang.Path.Replace(site.Path, "");
            //    res = Site.Instance.WebPath + "/" + lang.Path + "/" + res;
            //}
            Regex r = new Regex(@"//*/", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
            res = r.Replace(res, "/");
            //string urlpara = RequestTool.GetConfigKey("URLPara");
            //if (urlpara != "")
            //{
            //    NameValueCollection nv = LB.Tools.CookieTool.GetCookie(urlpara);
            //    if (nv.Count > 0)
            //    {
            //        string str = "";
            //        foreach (string key in nv.Keys)
            //        {
            //            str += "&" + key + "=" + nv.Get(key);
            //        }
            //        if (res.Contains("?"))
            //            res = res + str;
            //        else
            //            res = res + "?" + str.TrimStart('&');
            //    }
            //}
            return res;
        }
        public static string GetURL(string code, string ParaStr, string url, Lebi_Language lang)
        {
            if (lang == null)
                lang = new Lebi_Language();
            Lebi_Site site = B_Lebi_Site.GetModel(lang.Site_id);
            if (site == null)
            {
                if (ShopPage.IsAPP() || ShopPage.IsWechat() || ShopPage.IsWap())
                {
                    site = B_Lebi_Site.GetModel("IsMobile = 1 order by Sort asc");
                }
                else
                {
                    site = B_Lebi_Site.GetModel("IsMobile = 0 order by Sort asc");
                }
                if (site == null)
                {
                    site = new Lebi_Site();
                }
            }
            return GetURL(code, ParaStr, url, site, lang);

        }
        public static string GetURL(string code, string ParaStr, string url, string langcode)
        {
            Lebi_Site site = new Lebi_Site();
            if (ShopPage.IsAPP() || ShopPage.IsWechat() || ShopPage.IsWap())
            {
                site = B_Lebi_Site.GetModel("IsMobile = 1 order by Sort asc");
                if (site == null)
                    site = B_Lebi_Site.GetModel("IsMobile = 0 order by Sort asc");
            }
            else
            {
                site = B_Lebi_Site.GetModel("IsMobile = 0 order by Sort asc");
                if (site == null)
                    site = B_Lebi_Site.GetModel("IsMobile = 1 order by Sort asc");
            }
            Lebi_Language lang = B_Lebi_Language.GetModel("Site_id =" + site.id +" and Code = '" + langcode + "'");
            if (lang == null)
            {
                lang = new Lebi_Language();
            }
            return GetURL(code, ParaStr, url, lang);
        }
        public static string GetURL(string code, string ParaStr, string url, Lebi_Language_Code langcode)
        {
            Lebi_Site site = new Lebi_Site();
            if (ShopPage.IsAPP() || ShopPage.IsWechat() || ShopPage.IsWap())
            {
                site = B_Lebi_Site.GetModel("IsMobile = 1 order by Sort asc");
                if (site == null)
                    site = B_Lebi_Site.GetModel("IsMobile = 0 order by Sort asc");
            }
            else
            {
                site = B_Lebi_Site.GetModel("IsMobile = 0 order by Sort asc");
                if (site == null)
                    site = B_Lebi_Site.GetModel("IsMobile = 1 order by Sort asc");
            }
            Lebi_Language lang = B_Lebi_Language.GetModel("Site_id = " + site.id +" and Code = '" + langcode.Code + "'");
            return GetURL(code, ParaStr, url, lang);
        }
        /// <summary>
        /// 取得管理首页，自动继承上级设置
        /// </summary>
        /// <param name="node"></param>
        /// <param name="TypeFlag"></param>
        /// <returns></returns>
        static string GetPage(Lebi_Node node)
        {
            string url = "";

            if (node.AdminPage != "")
                url = node.AdminPage;
            else
            {
                node = B_Lebi_Node.GetModel(node.parentid);
                url = GetPage(node);
            }
            return url;
        }
        /// <summary>
        /// 测试并获取完整的文件路径信息，
        /// 主要针对拼合路径中含有“//”的情况
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            try
            {
                if (path != "/")
                {
                    Regex r = new Regex(@"//*/", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
                    path = r.Replace(path, "/");
                    //去掉最后一个"/"
                    if (path.LastIndexOf("/") == path.Length - 1)
                    {
                        path = path.Remove(path.Length - 1);
                    }
                    //如果开始字符不是"/"，则加上一个
                    if (path.IndexOf("/") != 0)
                    {
                        path = "/" + path;
                    }
                }
            }
            catch
            {
                path = "";
            }
            return path;
        }
        /// <summary>
        /// 测试并获取完整的web路径信息，
        /// 过滤掉多余的//
        /// 保留http://中的//
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullWebPath(string path)
        {

            path = path.ToLower();
            path = path.Replace("http://", "http:$@%**%@$");
            path = path.Replace("https://", "http:$@%**%@$");
            path = GetFullPath(path);
            path = path.Replace("http:$@%**%@$", "http://");
            path = path.Replace("https:$@%**%@$", "https://");
            //如果开始字符是"/"，则除去
            if (path.IndexOf("/") == 0 && path.Length > 0)
            {
                // path = "/" + path;
                path = path.Substring(1, path.Length - 1);
            }
            return path;
        }
        /// <summary>
        /// 在一个包含文件名的URL字符串中获取路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPath(string path)
        {
            string str = "";
            int index = path.LastIndexOf("/");
            if (index != -1)
            {
                str = path.Substring(0, index + 1);
            }
            else
            {
                str = path;
            }
            return str;
        }
        /// <summary>
        /// 在一个包含文件名的URL字符串中获取文件名
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            return path.Replace(GetPath(path), "");
        }
        public static string CheckURL(string url)
        {
            if (url.ToLower().Contains("://"))
                url = url.Replace("://", "$$@@$$");
            Regex r = new Regex(@"//*/", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
            url = r.Replace(url, "/");
            url = url.Replace("$$@@$$", "://");
            return url;
        }
        public static string CheckPath(string url)
        {
            Regex r = new Regex(@"//*/", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
            url = r.Replace(url, "/").TrimEnd('/');
            return url;
        }

        /// <summary>
        /// 生成地址重写规则
        /// </summary>
        public static void CreateURLRewrite()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            sb.AppendLine("[ISAPI_Rewrite]");
            sb1.AppendLine("RewriteEngine On");
            sb1.AppendLine("RewriteCompatibility2 On");
            sb1.AppendLine("RepeatLimit 200");
            sb1.AppendLine("RewriteBase");

            List<Lebi_Language> langs = B_Lebi_Language.GetList("", "Sort desc,id asc");
            List<Lebi_Theme_Page> models = B_Lebi_Theme_Page.GetList("Type_id_PublishType=123", "");
            string str1 = "";
            string str2 = "";
            string language = "";
            foreach (Lebi_Theme_Page model in models)
            {
                if (model.PageParameter == "")
                {
                    str1 = model.PageName;
                    str2 = model.StaticPath + "/" + model.StaticPageName;
                    str2 = ThemeUrl.CheckURL(str2);
                    str1 = str1.Trim('/');
                    str2 = str2.Trim('/');
                    str1 = str1.Replace(".", @"\.");
                    str2 = str2.Replace(".", @"\.");
                    foreach (Lebi_Language lang in langs)
                    {
                        Lebi_Site site = B_Lebi_Site.GetModel(lang.Site_id);
                        if (site == null)
                            site = new Lebi_Site();
                        language = site.Path + lang.Path.TrimEnd('/');
                        language = language.Replace("//", "/");
                        if (language == "")
                            continue;
                        sb.AppendLine(@"RewriteRule /(.*)" + language + "/" + str2 + "(.*) /$1" + language + "/" + str1 + " [N,I]");
                        sb1.AppendLine(@"RewriteRule " + language + "/" + str2 + "(.*)$ " + language + "/" + str1 + " [NC,N]");
                    }
                    sb.AppendLine(@"RewriteRule /(.*)" + str2 + "(.*) /$1" + str1 + " [N,I]");
                    sb1.AppendLine(@"RewriteRule /" + str2 + "(.*)$ /" + str1 + " [NC,N]");
                    //RewriteRule /CN/(.*)$ /CN/Basket\.aspx\?Basket\.html$1 [NC,N]
                    //RewriteRule /(.*)$ /Basket\.aspx\?Basket\.html$1 [NC,N]
                }
                else
                {
                    str1 = model.PageName + @"\?" + model.PageParameter;
                    str2 = model.StaticPath + "/" + model.StaticPageName;
                    str2 = ThemeUrl.CheckURL(str2);
                    str1 = str1.Trim('/');
                    str2 = str2.Trim('/');
                    str1 = str1.Replace(".", @"\.");
                    str2 = str2.Replace(".", @"\.");
                    str1 = RegexTool.ReplaceRegValue(str1, @"{\d+}", ",");
                    str2 = RegexTool.ReplaceRegValue(str2, @"{\d+}", "(.*)");
                    string[] arr = str1.Split(',');
                    int j = 1;
                    string str_ini = "";
                    string str_hta = "";
                    foreach (string ar in arr)
                    {
                        if (ar != "")
                            str_hta += ar + "$" + j;
                        j++;
                        if (ar != "")
                            str_ini += ar + "$" + j;

                    }
                    foreach (Lebi_Language lang in langs)
                    {
                        Lebi_Site site = B_Lebi_Site.GetModel(lang.Site_id);
                        if (site == null)
                            site = new Lebi_Site();
                        language = site.Path + lang.Path.TrimEnd('/');
                        language = language.Replace("//", "/");
                        if (language == "")
                            continue;
                        sb.AppendLine(@"RewriteRule /(.*)" + language + "/" + str2 + "(.*) /$1" + language + "/" + str_ini + " [N,I]");
                        sb1.AppendLine(@"RewriteRule " + language + "/" + str2 + "(.*)$ " + language + "/" + str_hta + " [NC,N]");
                    }
                    sb.AppendLine(@"RewriteRule /(.*)" + str2 + "(.*) /$1" + str_ini + " [N,I]");
                    sb1.AppendLine(@"RewriteRule /" + str2 + "(.*)$ /" + str_hta + " [NC,N]");
                }
            }
            //生成商品分类重写规则
            List<Lebi_Pro_Type> tps = B_Lebi_Pro_Type.GetList("", "");
            Lebi_Theme_Page tpage = B_Lebi_Theme_Page.GetModel("Code='P_ProductCategory'");
            str1 = tpage.PageName + @"\?" + tpage.PageParameter;
            str1 = str1.Trim('/');
            str1 = str1.Replace(".", @"\.");
            str1 = RegexTool.ReplaceRegValue(str1, @"{\d+}", ",");
            string[] arr1 = str1.Split(',');
            string str_ini1 = "";
            string str_hta1 = "";
            //foreach (string ar in arr1)
            //{
            //    if (ar != "")
            //        str_hta1 += ar + "$" + j1;
            //    j1++;
            //    if (ar != "")
            //        str_ini1 += ar + "$" + j1;

            //}

            foreach (Lebi_Pro_Type tp in tps)
            {
                str_hta1 = arr1[0] + tp.id;
                str_ini1 = arr1[0] + tp.id;
                foreach (Lebi_Language lang in langs)
                {
                    if (Language.Content(tp.IsUrlrewrite, lang.Code) != "1")
                        continue;
                    str2 = Language.Content(tp.Url, lang.Code);
                    if (str2 == "")
                        continue;



                    str2 = ThemeUrl.CheckURL(str2);
                    str2 = str2.Trim('/');
                    str2 = str2.Replace(".", @"\.");
                    str2 = RegexTool.ReplaceRegValue(str2, @"{\d+}", "(.*)");


                    Lebi_Site site = B_Lebi_Site.GetModel(lang.Site_id);
                    if (site == null)
                        site = new Lebi_Site();
                    language = site.Path + lang.Path.TrimEnd('/');
                    language = language.Replace("//", "/");
                    language = language.TrimEnd('/');
                    sb.AppendLine(@"RewriteRule /(.*)" + language + "/" + str2 + "(.*) /$1" + language + "/" + str_ini1 + " [N,I]");
                    sb1.AppendLine(@"RewriteRule /(.*)" + language + "/" + str2 + "(.*)$ /$1" + language + "/" + str_hta1 + " [NC,N]");
                }
            }
            HtmlEngine save = new HtmlEngine();
            save.CreateFile("httpd.ini", sb.ToString(), "ascii");
            save.CreateFile(".htaccess", sb1.ToString(), "ascii");
            CreateURLRewrite_shop();
        }

        /// <summary>
        /// 生成域名绑定规则
        /// </summary>
        public static void CreateURLRewrite_shop()
        {
            HtmlEngine save = new HtmlEngine();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            string str = HtmlEngine.ReadTxt("httpd.ini");
            string str1 = HtmlEngine.ReadTxt(".htaccess");
            if (str == "")
            {
                sb.AppendLine("[ISAPI_Rewrite]");
            }
            else
            {
                sb.AppendLine(HtmlEngine.ReadTxt("httpd.ini"));
            }
            if (str1 == "")
            {
                sb1.AppendLine("RewriteEngine On");
                sb1.AppendLine("RewriteCompatibility2 On");
                sb1.AppendLine("RepeatLimit 200");
                sb1.AppendLine("RewriteBase");
            }
            else
            {
                sb1.AppendLine(HtmlEngine.ReadTxt(".htaccess"));
            }
            List<Lebi_Supplier> models = B_Lebi_Supplier.GetList("Domain!=''", "");
            if (models.Count == 0)
                return;
            //RewriteCond %{HTTP_HOST} ^www.shop0769.top$
            //RewriteRule ^(.*)$ http://www.shop0769.com/shop/?id=11
            Lebi_Site site = B_Lebi_Site.GetModel("1=1 order by Sort desc");
            if (site != null)
            {
                foreach (Lebi_Supplier model in models)
                {

                    //sb1.AppendLine(@"RewriteRule //" + model.Domain + "(.*)$ /shop/?id=" + model.id + " [NC,N]");
                    //sb.AppendLine(@"RewriteRule //" + model.Domain + "/(.*) /$1/shop/?id=" + model.id + " [N,I]");

                    sb.AppendLine(@"RewriteCond Host ^" + model.Domain + "$");
                    sb.AppendLine(@"RewriteRule (.*) "+ ShopCache.GetBaseConfig().HTTPServer +"://" + site.Domain + "/shop/?id=" + model.id + "$ [N,I]");

                    sb1.AppendLine(@"RewriteCond  %{HTTP_HOST} ^" + model.Domain + "$");
                    sb1.AppendLine(@"RewriteRule ^(.*)$ " + ShopCache.GetBaseConfig().HTTPServer + "://" + site.Domain + "/shop/?id=" + model.id + " [NC,N]");

                }
            }
            save.CreateFile("httpd.ini", sb.ToString(), "ascii");
            save.CreateFile(".htaccess", sb1.ToString(), "ascii");
        }
    }

}
