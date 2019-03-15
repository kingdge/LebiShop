using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LB.Tools;
using Shop.Model;using DB.LebiShop;
using System.Reflection;
using System.IO;

namespace Shop.Bussiness
{
    public class SystemTheme
    {
        #region 页面生成
        public static void CreateSystemPage()
        {
            string msg = "";
            try
            {
                string path = "";
                path = System.Web.HttpRuntime.AppDomainAppPath + "theme/system/systempage";
                if (!Directory.Exists(path))
                {
                    //return "模板目录不存在";
                    SystemLog.Add("系统模板目录不存在");
                    return;
                }
                DirectoryInfo mydir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = mydir.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    if(dir.Name == "admin2")
                    {
                        continue;
                    }
                    string temp = CreateAdmin(dir.Name, "");
                    if (temp != "")
                        msg += dir.Name + temp + "|";
                }

            }
            catch (Exception ex)
            {
                //return ;
                SystemLog.Add("生成系统页面发生错误：" + ex.Message);
            }

        }
        /// <summary>
        /// 生成后台|供应商文件
        /// </summary>
        /// <param name="type">admin或supplier或前台文件夹名</param>
        /// <param name="parentpath"></param>
        public static string CreateAdmin(string type, string parentpath)
        {
            string msg = "";
            Site site = new Site();
            string content = "";
            string createpath = type;
            if (type == "admin")
                createpath = site.AdminPath == "" ? "admin" : site.AdminPath;
            else if (type == "supplier")
            {
                if (!Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
                {
                    return "";
                }
                createpath = site.SupplierPath == "" ? "supplier" : site.SupplierPath;
            }
            else if (type == "front")
            {
                createpath = "/";
            }
            try
            {
                string path = "";
                path = System.Web.HttpRuntime.AppDomainAppPath + "theme/system/systempage/" + type + "/page/" + parentpath;
                if (type == "admin")
                {
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        path = System.Web.HttpRuntime.AppDomainAppPath + "theme/system/systempage/" + RequestTool.GetConfigKey("SystemAdmin").Trim() + "/page/" + parentpath;
                    }
                }
                if (!Directory.Exists(path))
                {
                    return "模板目录不存在";
                }
                DirectoryInfo mydir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = mydir.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    CreateAdmin(type, parentpath + "/" + dir.Name);
                }
                FileInfo[] files = mydir.GetFiles();
                foreach (FileInfo f in files)
                {
                    content = HtmlEngine.ReadTxt("theme/system/systempage/" + type + "/page/" + parentpath + "/" + f.Name);
                    if (type == "admin") { 
                        if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                        {
                            content = HtmlEngine.ReadTxt("theme/system/systempage/" + RequestTool.GetConfigKey("SystemAdmin").Trim() + "/page/" + parentpath + "/" + f.Name);
                        }
                    }
                    CreatAdminAspx(createpath + parentpath + "/" + f.Name.Replace(".html", ".aspx"), content, type, "");

                }
                //生成插件页面
                List<PluginConfig> plgs = Event.GetPluginConfig();
                foreach (PluginConfig plg in plgs)
                {
                    CreateALLPluginPage(plg);
                }

            }
            catch (Exception ex)
            {

            }
            return msg;
        }

        public static void CreateALLPluginPage(PluginConfig pgconf)
        {

            try
            {
                string path = System.Web.HttpRuntime.AppDomainAppPath + "/plugin/" + pgconf.Assembly.ToLower() + "/systempage";
                if (!Directory.Exists(path))
                {
                    return;
                }
                DirectoryInfo mydir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = mydir.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    CreatePluginPage(pgconf, dir.Name, "");
                }

            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 生成插件页面
        /// </summary>
        /// <param name="pgconf"></param>
        /// <param name="site"></param>
        /// <param name="type"></param>
        /// <param name="parentpath"></param>
        public static void CreatePluginPage(PluginConfig pgconf, string type, string parentpath)
        {
            string WebPath = Site.Instance.WebPath;
            string path = System.Web.HttpRuntime.AppDomainAppPath + "/plugin/" + pgconf.Assembly.ToLower() + "/systempage/" + type + "/page/" + parentpath;
            string filepath = WebPath + "/plugin/" + pgconf.Assembly.ToLower() + "/systempage/" + type + "/page/" + parentpath;
            string mediapath = "/plugin/" + pgconf.Assembly.ToLower() + "/systempage/" + type;
            if (type == "admin")
            {
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    path = System.Web.HttpRuntime.AppDomainAppPath + "/plugin/" + pgconf.Assembly.ToLower() + "/systempage/" + RequestTool.GetConfigKey("SystemAdmin").Trim() + "/page/" + parentpath;
                    filepath = WebPath + "/plugin/" + pgconf.Assembly.ToLower() + "/systempage/" + RequestTool.GetConfigKey("SystemAdmin").Trim() + "/page/" + parentpath;
                    mediapath = "/plugin/" + pgconf.Assembly.ToLower() + "/systempage/" + RequestTool.GetConfigKey("SystemAdmin").Trim();
                }
            }
            if (!Directory.Exists(path))
            {
                return;
            }
            DirectoryInfo mydir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = mydir.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                CreatePluginPage(pgconf, type, parentpath + "/" + dir.Name);//递归全部文件夹
            }
            FileInfo[] files = mydir.GetFiles();
            string content = "";
            string pagename = "";
            parentpath = parentpath.ToLower().Trim('/');

            string createpath = type;
            if (type == "admin")
            {
                createpath = Site.Instance.AdminPath;
            }
            else if (type == "supplier")
            {
                createpath = Site.Instance.SupplierPath;
            }
            //parentpath = parentpath.Substring(type.Length, parentpath.Length - type.Length);
            if (!Directory.Exists(System.Web.HttpRuntime.AppDomainAppPath + "/" + createpath))
            {
                Directory.CreateDirectory(System.Web.HttpRuntime.AppDomainAppPath + "/" + createpath);
            }
            foreach (FileInfo f in files)
            {
                pagename = f.Name.Replace(".html", ".aspx");
                pagename = ThemeUrl.GetFullPath(WebPath + "/" + createpath + "/" + parentpath + "/" + pagename);
                content = HtmlEngine.ReadTxt(filepath + "/" + f.Name);
                CreatAdminAspx(pagename, content, type, mediapath);
            }
        }
        /// <summary>
        /// 生成后台的单个文件
        /// </summary>
        /// <param name="Path">生成文件名已经路径</param>
        /// <param name="Content"></param>
        /// <param name="WebPath"></param>
        /// <returns></returns>
        public static void CreatAdminAspx(string Path, string Content, string type, string mediapath)
        {
            if (Content == "")
            {
                return;
            }
            if (type == "admin")
            {
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    type = RequestTool.GetConfigKey("SystemAdmin").Trim();
                }
            }
            string FileName = "";
            BaseConfig bf = ShopCache.GetBaseConfig();
            string WebPath = Site.Instance.WebPath;
            string serverpath = "";
            if (System.Web.HttpContext.Current == null)
                serverpath = System.Web.HttpRuntime.AppDomainAppPath;
            else
                serverpath = System.Web.HttpContext.Current.Server.MapPath(@"~/");
            try
            {
                Path = ThemeUrl.GetFullPath(Path);
                FileName = ThemeUrl.GetFileName(Path);
                Path = ThemeUrl.GetPath(Path);
                string PhysicsPath = serverpath + Path;
                if (!Directory.Exists(PhysicsPath))
                {
                    Directory.CreateDirectory(PhysicsPath);
                }
                string PhysicsFileName = serverpath + Path + FileName;
                if (System.IO.File.Exists(PhysicsFileName))
                {
                    System.IO.File.Delete(PhysicsFileName);
                }
                Content = DoCodeConvert(Content, type, WebPath, mediapath);
                //=============================================================
                //处理特殊页面引用

                string cs = RegexTool.GetRegValue(Content, @"{[Cc][Ll][Aa][Ss][Ss]:(.*?)}");
                if (cs != "")
                {
                    string pagehead = "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" Inherits=\"" + cs + "\" validateRequest=\"false\"%>";
                    Content = RegexTool.ReplaceRegValue(Content, @"<%@ Page .*? %>", "");
                    Content = pagehead + Content;
                    //if (type == "supplier")
                    //    Content = Content.Replace("Shop.Bussiness.SupplierPageBase", cs);
                    //else
                    //    Content = Content.Replace("Shop.Bussiness.AdminPageBase", cs);
                }
                //处理资源路径
                string Path_JS = WebPath + "/system/systempage/" + type + "/js";
                string Path_Image = WebPath + "/system/systempage/" + type + "/images";
                string Path_CSS = WebPath + "/system/systempage/" + type + "/css";
                Path_JS = RegexTool.ReplaceRegValue(Path_JS, @"//*/", "/");
                Path_Image = RegexTool.ReplaceRegValue(Path_Image, @"//*/", "/");
                Path_CSS = RegexTool.ReplaceRegValue(Path_CSS, @"//*/", "/");
                Content = RegexTool.ReplaceRegValue(Content, @"{/[Jj][Ss]}", Path_JS);
                Content = RegexTool.ReplaceRegValue(Content, @"{/[Cc][Ss][Ss]}", Path_CSS);
                Content = RegexTool.ReplaceRegValue(Content, @"{/[Ii][Mm][Aa][Gg][eE]}", Path_Image);
                Content = RegexTool.ReplaceRegValue(Content, @"<!--.*?-->", "");
                Content = RegexTool.ReplaceRegValue(Content, @"{[Cc][Ll][Aa][Ss][Ss]:.*?}", "");

                HtmlEngine.Instance.WriteFile(PhysicsFileName, Content);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        #region 模板代码转换

        /// <summary>
        /// 载入布局文件
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static string DoLayout(string strIn, string type, string webpath, string mediapath)
        {
            if (type == "admin")
            {
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    type = RequestTool.GetConfigKey("SystemAdmin").Trim();
                }
            }
            string layout = RegexTool.GetRegValue(strIn, @"{[Ll][Aa][Yy][Oo][Uu][Tt]:(.*?)}");
            string cs = RegexTool.GetRegValue(strIn, @"({[Cc][Ll][Aa][Ss][Ss]:.*?})");
            if (layout == "")
                return cs + strIn;

            string layoutpath = "";
            string layoutContent = "";
            if (mediapath != "")
            {
                layoutpath = webpath + mediapath + "/layout/" + layout + ".layout";
                layoutpath = ThemeUrl.GetFullPath(layoutpath);
                layoutContent = HtmlEngine.ReadTxt(layoutpath);
            }
            if (layoutContent == "")
            {
                layoutpath = webpath + "/theme/system/systempage/" + type + "/layout/" + layout + ".layout";
                layoutpath = ThemeUrl.GetFullPath(layoutpath);
                layoutContent = HtmlEngine.ReadTxt(layoutpath);
            }
            if (layoutContent == "")
            {
                layoutpath = webpath + "/theme/system/layout/" + layout + ".layout";
                layoutpath = ThemeUrl.GetFullPath(layoutpath);
                layoutContent = HtmlEngine.ReadTxt(layoutpath);
            }
            if (layoutContent == "")
            {
                return "";
            }
            string[] holderArray = RegexTool.GetSimpleRegResultArray(layoutContent, @"({[Hh][Oo][Ll][Dd][Ee][Rr]:.*?})");
            foreach (string holder in holderArray)
            {

                string tag = RegexTool.GetRegValue(holder, @"{[Hh][Oo][Ll][Dd][Ee][Rr]:(.*?)}");
                string regtag = "";
                foreach (char t in tag)
                {
                    regtag += "[" + t.ToString().ToUpper() + t.ToString().ToLower() + "]";
                }
                string holdercontent = RegexTool.GetRegValue(strIn, @"<" + regtag + ">(.*?)</" + regtag + ">");
                layoutContent = RegexTool.ReplaceRegValue(layoutContent, @"{[Hh][Oo][Ll][Dd][Ee][Rr]:" + tag + ".*?}", holdercontent);
            }
            //载入重写模块
            string[] rewriteArray = RegexTool.GetSimpleRegResultArray(strIn, @"({[Rr][Ee][Ww][Rr][Ii][Tt][Ee]:.*?})");
            foreach (string rewrite in rewriteArray)
            {

                string tag = RegexTool.GetRegValue(rewrite, @"{[Rr][Ee][Ww][Rr][Ii][Tt][Ee]:(.*?)}");
                string from = "";
                string to = "";
                from = tag.Substring(0, tag.IndexOf(" "));
                to = tag.Substring(tag.IndexOf(" ") + 1, tag.Length - from.Length - 1);
                string regfrom = "";
                string regto = "";
                foreach (char t in from)
                {
                    regfrom += "[" + t.ToString().ToUpper() + t.ToString().ToLower() + "]";
                }
                foreach (char t in to)
                {
                    regto += "[" + t.ToString().ToUpper() + t.ToString().ToLower() + "]";
                }
                string holdercontent = RegexTool.GetRegValue(strIn, @"<" + regto + ">(.*?)</" + regto + ">");
                if (holdercontent == "")
                {
                    holdercontent = "{MOD:" + to + "}";
                }
                layoutContent = RegexTool.ReplaceRegValue(layoutContent, @"{[Mm][Oo][Dd]:" + regfrom + "}", holdercontent);
            }
            layoutContent = cs + layoutContent;
            return layoutContent;
        }
        /// <summary>
        /// 执行代码替换
        /// 在${...}$内
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string DoCodeConvert(string strIn, string type, string webpath, string mediapath)
        {
            strIn = DoLayout(strIn, type, webpath, mediapath);
            //======================================================
            string[] PartArry = RegexTool.GetSimpleRegResultArray(strIn, @"({[Mm][Oo][Dd]:.*?})");
            string partTag = "";
            string partConten = "";
            string partPara = "";
            webpath = webpath.TrimEnd('/');
            Lebi_Theme_Skin partskin = new Lebi_Theme_Skin();
            foreach (string partStr in PartArry)
            {
                partTag = "";
                partConten = "";
                partPara = "";
                if (partStr.Contains("("))
                {
                    partTag = RegexTool.GetRegValue(partStr, @"{[Mm][Oo][Dd]:(.*?)\(.*?}");
                    partPara = RegexTool.GetRegValue(partStr, @"{[Mm][Oo][Dd]:.*?\((.*?)\)}");
                }
                else
                {
                    partTag = RegexTool.GetRegValue(partStr, @"{[Mm][Oo][Dd]:(.*?)}");
                }
               
                if (partTag != "")
                {
                    //模块提取优先级
                    //1插件中的BLOCK文件夹
                    //2模板中的自定义设置
                    //3系统块

                    string[] plugins = ShopCache.GetBaseConfig().PluginUsed.ToLower().Split(',');
                    foreach (string plugin in plugins)
                    {
                        partConten = HtmlEngine.ReadTxt(webpath + "/plugin/" + plugin + "/systempage/" + type + "/block/" + partTag + ".html");
                        if (partConten == "")
                        {
                            partConten = HtmlEngine.ReadTxt(webpath + "/plugin/" + plugin + "/block/" + partTag + ".html");
                        }
                        if (partConten != "")
                            break;
                    }

                    if (partConten == "")
                    {
                        if (type == "admin")
                        {
                            if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                            {
                                type = RequestTool.GetConfigKey("SystemAdmin").Trim();
                            }
                        }
                        partConten = HtmlEngine.ReadTxt(ThemeUrl.GetFullPath(webpath + "/theme/system/systempage/" + type + "/block/" + partTag + ".html"));
                    }
                    if (partConten == "")
                    {
                        partConten = HtmlEngine.ReadTxt(ThemeUrl.GetFullPath(webpath + "/theme/system/block/" + partTag + ".html"));
                    }
                    if (partConten != "")
                    {
                        partConten = "<!--MOD_start:" + partTag + "-->\r\n" + DoCodeConvert(partConten, type, webpath, "") + "<!--MOD_end:" + partTag + "-->\r\n";
                        strIn = RegexTool.ReplaceRegValue(strIn, @"{[Mm][Oo][Dd]:" + partTag + ".*?}", partConten);
                    }
                   
                }
            }
            //=========================================================
            string str = "";
            Type t = typeof(ShopPage);
            string temp = "";
            int[,] d = IndexFlagArry(strIn);//记录开始，结尾标记的数组
            if (d.Length > 0)
            {
                int begin = 0;
                string[,] tempArry = new string[d.GetUpperBound(0) + 1, 2];//用于存放一个外层标记的临时数组
                for (int i = 0; i < d.GetUpperBound(0); i++)
                {
                    temp += RegexTool.GetSubString(strIn, begin, d[i, 0]) + "$$$" + i + "$$$";//挑选非标记部组合，将标记部分替换为$$$0$$$，$$$1$$$形式
                    begin = d[i, 1] + 2;
                    tempArry[i, 0] = "$$$" + i + "$$$";
                    tempArry[i, 1] = RegexTool.GetSubString(strIn, d[i, 0] + 2, d[i, 1]);//截取不包含开始，结尾标记的块
                }
                temp += RegexTool.GetSubString(strIn, begin, strIn.Length);
                str = temp;//str已经替除了全部的标记块

                for (int i = 0; i < tempArry.GetUpperBound(0); i++)
                {

                    temp = DoPart(tempArry[i, 1], type, webpath);
                    str = str.Replace("$$$" + i + "$$$", temp);

                }
            }
            return str;
        }
        /// <summary>
        /// 处理一个标记块
        /// </summary>
        /// <param name="StrIn"></param>
        /// <returns></returns>
        private static string DoPart(string StrIn, string type, string webpath)
        {
            string str = StrIn;
            //============================================================================
            str = DoCodeConvert(str, type, webpath, "");//递归调用
            //============================================================================
            string str_Head = "";
            string str_Foot = "";
            string str_B = "";//循环第一次的字符串
            string str_E = "";//循环最后一次的字符串
            bool BEFlag = false;//开始结尾特殊处理标志
            string temp = "";
            string Table = "";
            string Order = "";
            string Where = "";
            string PageSize = "";
            string page = "";
            string model = "";          //MODEL名字
            string Source = "";
            string Top = "";
            string Tag = "";
            //============================================================================
            //参数处理
            //------------------------------
            temp = str.Replace("\r\n", "");
            //string key_ = "";
            //string key = "";
            //------------------------------

            //SQL = GetRegValue(temp, @"\$SQL:(.*?)[\n|\$]");      //这是SQL语句读取数据的方式，由于当前分页方式、分页存储过程的限制，不能实现

            Table = RegexTool.GetRegValue(temp, @"#[tT][aA][bB][lL][eE][:=](.*?)[#;]").Trim();    //参数结尾标记兼容 '换行'(\n) 和 '#',分号，空格
            model = RegexTool.GetRegValue(temp, @"#[mM][oO][dD][eE][lL][:=](.*?)[#;]").Trim();
            Source = RegexTool.GetRegValue(temp, @"#[Ss][Oo][Uu][Rr][Cc][Ee][:=](.*?)[#;]").Trim();
            model = RegexTool.GetRegValue(temp, @"#[mM][oO][dD][eE][lL][:=](.*?)[#;]").Trim();
            Tag = RegexTool.GetRegValue(temp, @"#[Tt][Aa][Gg][:=](.*?)[#;]").Trim().ToLower();


            Order = RegexTool.GetRegValue(temp, @"#[oO][rR][dD][eE][rR][:=](.*?)[#;]").Trim();
            Where = RegexTool.GetRegValue(temp, @"#[wW][hH][eE][rR][eE][:=](.*?)[#;]").Trim();
            PageSize = RegexTool.GetRegValue(temp, @"#[pP][aA][gG][eE][sS][iI][zZ][eE][:=](.*?)[#;]").Trim();
            page = RegexTool.GetRegValue(temp, @"#[pP][aA][gG][eE][iI][nN][dD][eE][xX][:=](.*?)[#;]").Trim();
            Top = RegexTool.GetRegValue(temp, @"#[Tt][Oo][Pp][:=](.*?)[#;]").Trim();

            if (Top != "")
            {
                PageSize = Top;
                page = "1";
            }
            //-------------------------------
            //过滤换行
            Table = RegexTool.ReplaceRegValue(Table, @"\r\n");
            Order = RegexTool.ReplaceRegValue(Order, @"\r\n");
            Where = RegexTool.ReplaceRegValue(Where, @"\r\n");
            PageSize = RegexTool.ReplaceRegValue(PageSize, @"\r\n");
            page = RegexTool.ReplaceRegValue(page, @"\r\n");
            model = RegexTool.ReplaceRegValue(model, @"\r\n");
            Source = RegexTool.ReplaceRegValue(Source, @"\r\n");
            //-------------------------------
            //-------------------------------
            //过滤多个空格
            Regex r = new Regex(@" +", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
            Table = r.Replace(Table, " ");
            Order = r.Replace(Order, " ");
            Where = r.Replace(Where, " ");
            PageSize = r.Replace(PageSize, " ");
            page = r.Replace(page, " ");
            model = r.Replace(model, " ");
            //-------------------------------

            if (model == "" || model == "$")
                model = GetRnd(4, false, true, true, false, ""); //取8个字母的随机字符串作为MODEL名字
            string RowsIndex = model + "_index";//行号的索引
            //============================================================================

            str_B = RegexTool.GetRegValue(str, @"[Bb]{(.*?)}[Bb]");
            str_E = RegexTool.GetRegValue(str, @"[Ee]{(.*?)}[Ee]");
            //============================================================================
            //过滤掉参数部分
            //str = ReplaceRegValue(str, @"#.*?[\n#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[tT][aA][bB][lL][eE][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[oO][rR][dD][eE][rR][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[wW][hH][eE][rR][eE][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[pP][aA][gG][eE][sS][iI][zZ][eE][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[pP][aA][gG][eE][iI][nN][dD][eE][xX][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[mM][oO][dD][eE][lL][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[Ss][Oo][Uu][Rr][Cc][Ee][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[tT][Oo][Pp][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"#[tT][Aa][Gg][:=](.*?)[#;]");
            str = RegexTool.ReplaceRegValue(str, @"[Bb]{.*?}[Bb]");
            str = RegexTool.ReplaceRegValue(str, @"[Ee]{.*?}[Ee]");

            //============================================================================


            //自定义格式
            //===================================================================================
            str_Head += "<%";
            if (Table != "$")
                str_Head += "Table=\"" + Table + "\";";
            if (Where != "$")
                str_Head += "Where=\"" + Where + "\";";
            if (Order != "" && Order != "$")
                str_Head += "Order=\"" + Order + "\";";

            if (PageSize == "")
            {
                str_Head += "PageSize=20;";
            }
            else if (PageSize == "$")
            {
            }
            else
            {
                str_Head += "PageSize=" + PageSize + ";";
            }
            if (page == "")
            {
                str_Head += "pageindex=Rint(\"page\");";
            }
            else if (PageSize == "$")
            {
            }
            else
            {
                str_Head += "pageindex=" + page + ";";
            }
            str_Head += "RecordCount=B_" + Table + ".Counts(Where);";
            //str_Head += temp;

            str_Head += "int " + RowsIndex + "=1;\r\n";//循环的行号
            str_Head += "List<" + Table + "> " + model + "s = B_" + Table + ".GetList(Where, Order,PageSize ,pageindex);";
            str_Head += "foreach (" + Table + " " + model + " in " + model + "s)";
            str_Head += "{";
            str_Head += "%>";

            str_Foot += "<%";
            str_Foot += RowsIndex + "++;";
            str_Foot += "}";
            str_Foot += "%>";

            if (str_B != "" || str_E != "")
            {
                BEFlag = true;
            }
            //============================================================================
            if (BEFlag)
            {
                temp = "";
                if (str_B != "")
                {
                    temp += "<%if(" + RowsIndex + "==1)";
                    temp += "{%>";
                    temp += ReplaceModel(str_B, model);
                    temp += "<%}%>";
                    if (str_E != "")
                    {
                        temp += "<%else if(" + RowsIndex + "==RecordCount || " + RowsIndex + "==PageSize)";
                        temp += "{%>";
                        temp += ReplaceModel(str_E, model);
                        temp += "<%}%>";
                    }
                    temp += "<%else";
                    temp += "{%>";
                    temp += ReplaceModel(str, model);
                    temp += "<%}%>";

                }
                else//没有开始匹配的时候
                {
                    temp += "<%if(" + RowsIndex + "==RecordCount || " + RowsIndex + "==PageSize)";
                    temp += "{%>";
                    temp += ReplaceModel(str_E, model);
                    temp += "<%}%>";
                    temp += "<%else";
                    temp += "{%>";
                    temp += ReplaceModel(str, model);
                    temp += "<%}%>";
                }
                str = temp;

            }
            else
            {
                str = ReplaceModel(str, model);
            }
            //============================================================================
            str = str_Head + str + str_Foot;
            return str;

        }
        /// <summary>
        /// 将 取值部分 替换model.形式
        /// 字段名替换
        /// </summary>
        /// <returns></returns>
        private static string ReplaceModel(string strIn, string mod)
        {
            //字段名替换
            //@id@ ==》model.id
            //$id$ ==》<%=model.id%>
            //@i@ ==》model_index
            //$i$ ==》<%=model_index%>
            string str = strIn;
            string temp = "";
            string temp_key = "";
            string key = "";
            string key_ = "";
            str = str.Replace("{i}", mod + "_index");
            str = str.Replace("{%i%}", "<%=" + mod + "_index%>");
            str = str.Replace("{I}", mod + "_index");
            str = str.Replace("{%I%}", "<%=" + mod + "_index%>");
            Regex r = new Regex(@"{[\w]*}|{%[\w]*%}", RegexOptions.Singleline);
            MatchCollection mc = r.Matches(str);
            foreach (Match m in mc)
            {
                temp = m.Value;
                temp_key = "";
                key_ = RegexTool.GetRegValue(temp, @"({%[\w]*%})");
                key = RegexTool.GetRegValue(temp, @"{%([\w]*)%}");
                if (key_ != "")
                {
                    //temp_key = temp.Replace(key_, mod + "." + key);
                    temp_key = temp.Replace(key_, "<%=" + mod + "." + key + "%>");
                }
                else
                {
                    key_ = RegexTool.GetRegValue(temp, @"({[\w]*})");
                    key = RegexTool.GetRegValue(temp, @"{([\w]*)}");
                    if (key_ != "")
                        temp_key = temp.Replace(key_, mod + "." + key);
                }
                str = str.Replace(temp, temp_key);
            }
            return str;
        }
        #endregion


        #region 工具
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，1=包含，默认为包含</param>
        /// <param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        /// <param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        /// <param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        /// <param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        /// <returns>指定长度的随机字符串</returns>
        private static string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }
        /// <summary>
        /// 获输入字符串内外层循环标记位置数组
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private static int[,] IndexFlagArry(string strIn)
        {
            string[,] IndexArry = RegexTool.GetRegResultArray(strIn, @"(}\$|\$[\w]*{)");//匹配 ${ 或 $Head{ 或 }$
            //string[,] IndexArry = GetRegArry(strIn, @"(}\@|\@[\w\W]*{)");//匹配 ${ 或 $Head{ 或 }$
            int beginFlagCount = 0; //开始标记个数
            int endFlagCount = 0;   //结束标记个数
            string beginFlagIndex = ""; //开始标记位置
            string endFlagIndex = "";   //结束标记位置

            if (IndexArry.GetUpperBound(0) > 0)  //包含标记时，把第一个标记作为第一个循环的开始标记
                beginFlagIndex = IndexArry[0, 0];
            List<string> indexs = new List<string>();
            for (int i = 0; i < IndexArry.GetUpperBound(0); i++)
            {
                //str += i.ToString() + "-" + IndexArry[i, 0] + IndexArry[i, 1] + "\r\n";
                if (IndexArry[i, 1].IndexOf("{") != -1)
                    beginFlagCount++;
                if (IndexArry[i, 1].IndexOf("}") != -1)
                    endFlagCount++;
                if (beginFlagCount == endFlagCount)
                {
                    endFlagIndex = IndexArry[i, 0];
                    indexs.Add(beginFlagIndex + "-" + endFlagIndex);
                    if (IndexArry.GetUpperBound(0) > i)
                    {
                        beginFlagIndex = IndexArry[i + 1, 0];
                    }
                }
            }

            int[,] FlagArry = new int[indexs.Count + 1, 2];
            for (int i = 0; i < indexs.Count; i++)
            {
                string[] tempArry = indexs[i].Split('-');
                FlagArry[i, 0] = Convert.ToInt32(tempArry[0]);
                FlagArry[i, 1] = Convert.ToInt32(tempArry[1]);
            }

            return FlagArry;
        }
        #endregion

    }

}
