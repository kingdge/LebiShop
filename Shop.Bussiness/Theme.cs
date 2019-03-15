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
    public class Theme
    {

        #region 模板代码转换
        /// <summary>
        /// 转换{Tag：汉字}标签
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string DoTagConvert(string strIn, string langcode)
        {
            //string str = "";

            string[,] arr = RegexTool.GetRegArray(strIn, @"{[Tt][Aa][Gg]:(.*?)}");
            for (int i = 0; i < arr.GetUpperBound(0); i++)
            {
                string v = RegexTool.GetRegValue(arr[i, 1], @"{[Tt][Aa][Gg]:(.*?)}");
                strIn = strIn.Replace(arr[i, 1], Language.Tag(v, langcode));
            }
            return strIn;
        }
        /// <summary>
        /// 载入布局文件
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static string DoLayout(string strIn, Lebi_Theme theme, Lebi_Site site)
        {

            string layout = RegexTool.GetRegValue(strIn, @"{[Ll][Aa][Yy][Oo][Uu][Tt]:(.*?)}");
            string cs = RegexTool.GetRegValue(strIn, @"({[Cc][Ll][Aa][Ss][Ss]:.*?})");
            if (layout == "")
                return cs + strIn;
            string layoutpath = theme.Path_Files + "/layout/" + layout + ".layout";
            layoutpath = ThemeUrl.GetFullPath(layoutpath);
            string layoutContent = HtmlEngine.ReadTxt(layoutpath);
            if (layoutContent == "")
            {
                if (site.IsMobile == 1)
                    layoutpath = Site.Instance.WebPath + "/theme/system/wap/layout/" + layout + ".layout";
                else
                    layoutpath = Site.Instance.WebPath + "/theme/system/layout/" + layout + ".layout";
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
        public static string DoCodeConvert(string strIn, string syspara, Lebi_Theme theme, Lebi_Site site)
        {
            strIn = DoLayout(strIn, theme, site);
            //======================================================
            string[] PartArry = RegexTool.GetSimpleRegResultArray(strIn, @"({[Mm][Oo][Dd]:.*?})");
            string partTag = "";
            string partConten = "";
            string partPara = "";
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

                    string[] plugins = ShopCache.GetBaseConfig().PluginUsed.ToLower().Trim(',').Split(',');
                    foreach (string plugin in plugins)
                    {
                        if (plugin == "")
                            continue;
                        if (site.IsMobile == 1)
                            partConten = HtmlEngine.ReadTxt(Site.Instance.WebPath.TrimEnd('/') + "/plugin/" + plugin + "/wap/block/" + partTag + ".html");
                        //else
                        //    partConten = HtmlEngine.ReadTxt(Site.Instance.WebPath.TrimEnd('/') + "/plugin/" + plugin + "/page/block/" + partTag + ".html");
                        if (partConten == "")
                            partConten = HtmlEngine.ReadTxt(Site.Instance.WebPath.TrimEnd('/') + "/plugin/" + plugin + "/block/" + partTag + ".html");
                        if (partConten != "")
                            break;
                    }
                    if (partConten == "")
                    {
                        partConten = HtmlEngine.ReadTxt(ThemeUrl.GetFullPath(theme.Path_Files + "/block/" + partTag + ".html"));//在当前模板中读取块
                        if (partConten == "")
                        {
                            partConten = HtmlEngine.ReadTxt(ThemeUrl.GetFullPath(theme.Path_Files + "/inc/" + partTag + ".html"));//在当前模板中读取块
                            if (partConten == "")
                            {
                                //在系统块中读取块
                                if (site.IsMobile == 1)
                                {
                                    partConten = HtmlEngine.ReadTxt(Site.Instance.WebPath.TrimEnd('/') + "/theme/system/wap/block/" + partTag + ".html");
                                    if (partConten == "")
                                        partConten = HtmlEngine.ReadTxt(Site.Instance.WebPath.TrimEnd('/') + "/theme/system/block/" + partTag + ".html");
                                }
                                else
                                    partConten = HtmlEngine.ReadTxt(Site.Instance.WebPath.TrimEnd('/') + "/theme/system/block/" + partTag + ".html");
                            }
                        }
                    }
                    if (partConten != "")
                    {
                        //if (partPara != "")
                        //    partConten = "{SYSPARA:" + partPara + "}" + partConten;
                        partConten = "<!--MOD_start:" + partTag + "-->\r\n" + Shop.Bussiness.Theme.DoCodeConvert(partConten, partPara, theme, site) + "<!--MOD_end:" + partTag + "-->\r\n";
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

                    temp = DoPart(tempArry[i, 1], t, syspara, theme, site);
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
        private static string DoPart(string StrIn, Type t, string syspara, Lebi_Theme theme, Lebi_Site site)
        {
            string str = StrIn;
            //============================================================================
            str = DoCodeConvert(str, syspara, theme, site);//递归调用
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
            syspara = syspara.ToLower();
            if (syspara != "")
            {

                if (RegexTool.GetRegCount(syspara, @"#" + Tag + "order[:=].*?[#;]") > 0)
                    Order = RegexTool.GetRegValue(syspara, @"#" + Tag + "order[:=](.*?)[#;]").Trim();
                else if (RegexTool.GetRegCount(syspara, @"#order[:=].*?[#;]") > 0)
                    Order = RegexTool.GetRegValue(syspara, @"#order[:=](.*?)[#;]").Trim();
                else
                    Order = RegexTool.GetRegValue(temp, @"#order[:=](.*?)[#;]").Trim();
                //============================

                if (RegexTool.GetRegCount(syspara, @"#" + Tag + "where[:=].*?[#;]") > 0)
                    Where = RegexTool.GetRegValue(syspara, @"#" + Tag + "where[:=](.*?)[#;]").Trim();
                else if (RegexTool.GetRegCount(syspara, @"#where[:=].*?[#;]") > 0)
                    Where = RegexTool.GetRegValue(syspara, @"#where[:=](.*?)[#;]").Trim();
                else
                    Where = RegexTool.GetRegValue(temp, @"#where[:=](.*?)[#;]").Trim();
                //============================
                if (RegexTool.GetRegCount(syspara, @"#" + Tag + "pagesize[:=].*?[#;]") > 0)
                    PageSize = RegexTool.GetRegValue(syspara, @"#" + Tag + "pagesize[:=](.*?)[#;]").Trim();
                else if (RegexTool.GetRegCount(syspara, @"#pagesize[:=].*?[#;]") > 0)
                    PageSize = RegexTool.GetRegValue(syspara, @"#pagesize[:=](.*?)[#;]").Trim();
                else
                    PageSize = RegexTool.GetRegValue(temp, @"#pageindex[:=](.*?)[#;]").Trim();
                //=============================
                if (RegexTool.GetRegCount(syspara, @"#" + Tag + "pageindex[:=].*?[#;]") > 0)
                    page = RegexTool.GetRegValue(syspara, @"#" + Tag + "pageindex[:=](.*?)[#;]").Trim();
                else if (RegexTool.GetRegCount(syspara, @"#pageindex[:=].*?[#;]") > 0)
                    page = RegexTool.GetRegValue(syspara, @"#pageindex[:=](.*?)[#;]").Trim();
                else
                    page = RegexTool.GetRegValue(temp, @"#pageindex[:=](.*?)[#;]").Trim();
                //=============================
                if (RegexTool.GetRegCount(syspara, @"#" + Tag + "top[:=].*?[#;]") > 0)
                    Top = RegexTool.GetRegValue(syspara, @"#" + Tag + "top[:=](.*?)[#;]").Trim();
                else if (RegexTool.GetRegCount(syspara, @"#top[:=].*?[#;]") > 0)
                    Top = RegexTool.GetRegValue(syspara, @"#top[:=](.*?)[#;]").Trim();
                else
                    Top = RegexTool.GetRegValue(temp, @"#top[:=](.*?)[#;]").Trim();


            }
            else
            {
                Order = RegexTool.GetRegValue(temp, @"#[oO][rR][dD][eE][rR][:=](.*?)[#;]").Trim();
                Where = RegexTool.GetRegValue(temp, @"#[wW][hH][eE][rR][eE][:=](.*?)[#;]").Trim();
                PageSize = RegexTool.GetRegValue(temp, @"#[pP][aA][gG][eE][sS][iI][zZ][eE][:=](.*?)[#;]").Trim();
                page = RegexTool.GetRegValue(temp, @"#[pP][aA][gG][eE][iI][nN][dD][eE][xX][:=](.*?)[#;]").Trim();
                Top = RegexTool.GetRegValue(temp, @"#[Tt][Oo][Pp][:=](.*?)[#;]").Trim();
            }
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
            if (Source == "")
            {
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

            }
            else
            {
                //数据源格式
                //===================================================================================
                string methodname = Source;
                string methodpara = "";
                if (methodname.Contains("("))
                {
                    methodname = RegexTool.GetRegValue(Source, @"(.*?)\(.*?").Trim();
                    methodpara = RegexTool.GetRegValue(Source, @".*?\((.*?)\).*?").Trim();
                }
                MemberInfo[] members = t.GetMembers();
                MemberInfo mc = (from m in members
                                 where m.Name.ToLower() == methodname.ToLower()
                                 select m).FirstOrDefault();
                if (mc == null)
                    return StrIn;
                Table = RegexTool.GetRegValue(mc.ToString(), @".*?\[(.*?)\].*?").Trim();
                str_Head += "<%";
                str_Head += "List<" + Table + "> " + model + "s=" + mc.Name + "(" + methodpara + ");";
                str_Head += "RecordCount=" + model + "s.Count;";
                //str_Head += temp;

                str_Head += "int " + RowsIndex + "=1;\r\n";//循环的行号
                str_Head += "foreach (" + Table + " " + model + " in " + model + "s)";
                str_Head += "{";
                str_Head += "%>";
            }





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

        #region 页面生成
        /// <summary>
        /// 生成全部的前台模板页面
        /// </summary>
        public static void CreateThemeALL()
        {
            List<Lebi_Site> sites = Site.Instance.Sites();
            foreach (Lebi_Site site in sites)
            {
                List<Lebi_Language> langs = B_Lebi_Language.GetList("Site_id=" + site.id + "", "");
                foreach (Lebi_Language lang in langs)
                {
                    try
                    {
                        string res = "";
                        Lebi_Theme theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                        if (theme != null)
                            res= CreateThemeByLanguage(lang, theme);
                        SystemLog.Add("生成前台：" + lang.Code + "___site:" + lang.Site_id + res);
                    }
                    catch(Exception ex)
                    {
                        SystemLog.Add(ex.Message);
                    }

                }
            }
        }

        /// <summary>
        /// 生成针对一个语言的页面
        /// </summary>
        public static string CreateThemeByLanguage(Lebi_Language lang, Lebi_Theme theme)
        {

            List<Lebi_Theme_Skin> skins = B_Lebi_Theme_Skin.GetList("Theme_id=" + theme.id + " and IsPage=1", "");
            string SkinPath = "";
            string Content = "";
            string Path = "";
            string Msg = "";
            Lebi_Site s = B_Lebi_Site.GetModel(lang.Site_id);

            foreach (Lebi_Theme_Skin skin in skins)
            {
                SkinPath = theme.Path_Files + "/" + skin.Path_Skin;
                SkinPath = ThemeUrl.GetFullPath(SkinPath);
                Content = HtmlEngine.ReadTxt(SkinPath);
                if (Content == "")
                {
                    if (s.IsMobile == 1)
                    {
                        SkinPath = Site.Instance.WebPath + "/theme/system/wap/page/" + skin.Path_Skin;
                        Content = HtmlEngine.ReadTxt(SkinPath);
                    }
                    if (Content == "")
                        SkinPath = Site.Instance.WebPath + "/theme/system/page/" + skin.Path_Skin;
                    SkinPath = ThemeUrl.GetFullPath(SkinPath);
                    Content = HtmlEngine.ReadTxt(SkinPath);
                }
                if (Content == "")
                    continue;
                if (s == null)
                    Path = lang.Path + "/" + skin.PageName;
                else
                    Path = s.Path + lang.Path + "/" + skin.PageName;
                Msg = CreatAspx(s, lang, theme, skin, Path, Content);
            }
            //生成模板中没有的系统页面
            List<Lebi_Theme_Page> pages = B_Lebi_Theme_Page.GetList("Code not in (select Code from Lebi_Theme_Skin where Theme_id=" + theme.id + " and IsPage=1)", "");
            Lebi_Theme_Skin tempskin = new Lebi_Theme_Skin();
            foreach (Lebi_Theme_Page tp in pages)
            {

                SkinPath = Site.Instance.WebPath + "/theme/system/page/" + tp.Code.Replace("P_", "") + ".html";
                SkinPath = ThemeUrl.GetFullPath(SkinPath);
                Content = HtmlEngine.ReadTxt(SkinPath);
                if (Content == "")
                {
                    if (s.IsMobile == 1)
                    {
                        SkinPath = Site.Instance.WebPath + "/theme/system/wap/page/" + tp.PageName.Replace(".aspx", ".html");
                        Content = HtmlEngine.ReadTxt(SkinPath);
                    }
                    if (Content == "")
                        SkinPath = Site.Instance.WebPath + "/theme/system/page/" + tp.PageName.Replace(".aspx", ".html");
                    SkinPath = ThemeUrl.GetFullPath(SkinPath);
                    Content = HtmlEngine.ReadTxt(SkinPath);
                }
                if (Content == "")
                    continue;
                if (s == null)
                    Path = lang.Path + "/" + tp.PageName;
                else
                    Path = s.Path + lang.Path + "/" + tp.PageName;
                tempskin.Code = tp.Code;
                Msg = CreatAspx(s, lang, theme, tempskin, Path, Content);
            }
            //生成插件的页面
            CreatePluginPageByLanguage(s, lang, theme);
            //生成店铺首页
            List<Lebi_Supplier_Skin> shopskins = B_Lebi_Supplier_Skin.GetList("", "");
            foreach (Lebi_Supplier_Skin shopskin in shopskins)
            {
                SkinPath = shopskin.Path.TrimEnd('/') + "/index.html";
                Content = HtmlEngine.ReadTxt(SkinPath);
                if (Content == "")
                    continue;
                Path = lang.Path + "/shop/default" + shopskin.id + ".aspx";
                Msg = CreatAspx(s, lang, theme, null, Path, Content);
            }

            return Msg;

        }
        /// <summary>
        /// 生成插件页面
        /// </summary>
        /// <param name="pgconf">插件配置信息</param>
        /// <param name="site">站点</param>
        /// <param name="lang">语言</param>
        /// <param name="theme">主题</param>
        /// <param name="type">类型page、wap</param>
        /// <param name="parentpath"></param>
        public static void CreatePluginPage(PluginConfig pgconf, Lebi_Site site, Lebi_Language lang, Lebi_Theme theme, string parentpath)
        {

            string type = "page";
            string pagepath = type;
            if (site.IsMobile == 1)
            {
                type = "wap";
                pagepath = "wap/page";
            }
            string path = System.Web.HttpRuntime.AppDomainAppPath + "plugin/" + pgconf.Assembly.ToLower() + "/" + pagepath + "/" + parentpath;
            if (!Directory.Exists(path))
            {
                return;
            }

            DirectoryInfo mydir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = mydir.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                CreatePluginPage(pgconf, site, lang, theme, parentpath + "/" + dir.Name);//递归全部文件夹
            }
            FileInfo[] files = mydir.GetFiles();
            string WebPath = Site.Instance.WebPath;
            string content = "";
            string pagename = "";
            string filepath = WebPath + "/plugin/" + pgconf.Assembly.ToLower() + "/" + pagepath + "/" + parentpath;
            foreach (FileInfo f in files)
            {
                pagename = (parentpath + "/" + f.Name).Replace(".html", ".aspx");
                pagename = ThemeUrl.GetFullPath(WebPath + "/" + site.Path + lang.Path + pagename);
                content = HtmlEngine.ReadTxt(filepath + "/" + f.Name);
                CreatAspx(site, lang, theme, null, pagename, content);
            }
        }
        /// <summary>
        /// 生成全部插件页面
        /// </summary>
        public static void CreatePluginPageByLanguage(Lebi_Site site, Lebi_Language lang, Lebi_Theme theme)
        {
            //Lebi_Theme theme = B_Lebi_Theme.GetModel(lang.Theme_id);
            //if (theme == null)
            //    return;
            //Lebi_Site site = B_Lebi_Site.GetModel(lang.Site_id);
            //if (site == null)
            //    return;
            string SkinPath = "";
            string Content = "";
            string Path = "";
            List<PluginConfig> plgs = Event.GetPluginConfig();
            //生成插件中配置文件中定义的页面-旧方式-将淘汰
            foreach (PluginConfig plg in plgs)
            {
                foreach (PluginConfig.pageconfig p in plg.PageConfigs)
                {
                    if (site.IsMobile == 1)
                        SkinPath = Site.Instance.WebPath + "/plugin/" + plg.Assembly.ToLower() + "/" + p.wapskin;
                    else
                        SkinPath = Site.Instance.WebPath + "/plugin/" + plg.Assembly.ToLower() + "/" + p.skin;
                    SkinPath = ThemeUrl.GetFullPath(SkinPath);
                    Content = HtmlEngine.ReadTxt(SkinPath);
                    if (Content == null)
                        continue;
                    if (site == null)
                        Path = lang.Path + "/" + p.page;
                    else
                        Path = site.Path + lang.Path + "/" + p.page;
                    CreatAspx(site, lang, theme, null, Path, Content);
                }
            }
            //生成插件中的页面
            foreach (PluginConfig plg in plgs)
            {
                CreatePluginPage(plg, site, lang, theme, "");
            }


        }
        /// <summary>
        /// 生成全部插件页面
        /// </summary>
        public static void CreateALLPluginPage()
        {
            List<Lebi_Site> sites = Site.Instance.Sites();
            foreach (Lebi_Site site in sites)
            {
                List<Lebi_Language> langs = B_Lebi_Language.GetList("Site_id=" + site.id + "", "");
                foreach (Lebi_Language lang in langs)
                {
                    Lebi_Theme theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                    if (theme == null)
                        continue;
                    CreatePluginPageByLanguage(site, lang, theme);
                }
            }
        }
        /// <summary>
        /// 生成ASPX页面
        /// </summary>
        /// <returns></returns>
        public static string CreatAspx(Lebi_Site site, Lebi_Language lang, Lebi_Theme theme, Lebi_Theme_Skin skin, string Path, string Content)
        {

            string Msg = "";
            if (skin == null)
                skin = new Lebi_Theme_Skin();
            if (Content == "")
                return "OK";
            string serverpath = "";
            //if (System.Web.HttpContext.Current == null)
                serverpath = System.Web.HttpRuntime.AppDomainAppPath;
           // else
             //   serverpath = System.Web.HttpContext.Current.Server.MapPath(@"~/");
            string FileName = "";
            Type t = typeof(ShopPage);
            Lebi_Site mainsite = ShopCache.GetMainSite();
            string WebPath = Site.Instance.WebPath;
            if (site == null)
                return "";
            BaseConfig bf = ShopCache.GetBaseConfig();
            string LanguagePath = WebPath + site.Path + lang.Path;
            string SitePath = WebPath + site.Path;
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
                //=============================================================
                //处理{SYS:Tag}兼容mod
                string[] RegArray = RegexTool.GetSimpleRegResultArray(Content, @"({[Ss][Yy][Ss]:.*?})");
                foreach (string str in RegArray)
                {
                    Content = Content.Replace(str, "{MOD:" + RegexTool.GetRegValue(str, @"{[Ss][Yy][Ss]:(.*?)}") + "}");
                }
                //=============================================================
                //代码转换
                Content = Shop.Bussiness.Theme.DoCodeConvert(Content, "", theme, site);
                //=============================================================
                //处理特殊页面引用
                string cs = RegexTool.GetRegValue(Content, @"{[Cc][Ll][Aa][Ss][Ss]:(.*?)}");
                if (cs != "")
                {
                    //Content = Content.Replace("Shop.Bussiness.ShopPage", cs);
                    string pagehead = "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" Inherits=\"" + cs + "\" validateRequest=\"false\"%>";
                    Content = RegexTool.ReplaceRegValue(Content, @"<%@ Page .*? %>", "");
                    Content = pagehead + Content;
                }
                else
                {
                    System.Reflection.Assembly ass = System.Reflection.Assembly.Load("Shop");
                    Type[] types = ass.GetTypes();
                    Type type = (from m in types
                                 where m.Name.ToLower() == skin.Code.ToLower()
                                 select m).ToList().FirstOrDefault();
                    if (type != null)
                    {
                        //Content = Content.Replace("Shop.Bussiness.ShopPage", "Shop." + type.Name + "");
                        string pagehead = "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" Inherits=\"Shop." + type.Name + "\" validateRequest=\"false\"%>";
                        Content = RegexTool.ReplaceRegValue(Content, @"<%@ Page .*? %>", "");
                        Content = pagehead + Content;
                    }
                    //foreach (Type type in types)
                    //{
                    //    if (type.Name.ToLower() == skin.Code.ToLower())
                    //    {
                    //        //Content = Content.Replace("Shop.Bussiness.PageBase.ShopPage.cs", "Shop." + type.Name + ".cs");
                    //        Content = Content.Replace("Shop.Bussiness.ShopPage", "Shop." + type.Name + "");
                    //    }
                    //}
                }
                //=============================================================
                //处理广告位
                string[] adArray = RegexTool.GetSimpleRegResultArray(Content, @"({[Aa][Dd]:.*?})");
                foreach (string str in adArray)
                {
                    Content = Content.Replace(str, "<% Advertisement(\"" + RegexTool.GetRegValue(str, @"{[Aa][Dd]:(.*?)}") + "\"); %>");
                }
                //=============================================================
                //处理资源路径
                string SYSPath_JS = WebPath + "/Theme/system/js/";
                string SYSPath_Image = WebPath + "/Theme/system/images/";
                string SYSPath_CSS = WebPath + "/Theme/system/css/";

                string WAPPath_JS = WebPath + "/Theme/system/wap/js/";
                string WAPPath_Image = WebPath + "/Theme/system/wap/images/";
                string WAPPath_CSS = WebPath + "/Theme/system/wap/css/";

                string Path_JS = WebPath + theme.Path_JS + "/";
                string Path_Image = WebPath + theme.Path_Image + "/";
                string Path_CSS = WebPath + theme.Path_CSS + "/";

                string ThemeDomain = Site.Instance.ThemeDomain;
                if (!string.IsNullOrEmpty(ThemeDomain))
                {
                    SYSPath_JS = "/system/js/";
                    SYSPath_Image = "/system/images/";
                    SYSPath_CSS = "/system/css/";

                    WAPPath_JS = "/system/wap/js/";
                    WAPPath_Image = "/system/wap/images/";
                    WAPPath_CSS = "/system/wap/css/";

                    Path_JS = theme.Path_JS.ToLower().Replace("/theme","") + "/";
                    Path_Image = theme.Path_Image.ToLower().Replace("/theme", "") + "/";
                    Path_CSS = theme.Path_CSS.ToLower().Replace("/theme", "") + "/";
                }

                SYSPath_JS = RegexTool.ReplaceRegValue(SYSPath_JS, @"//*/", "/");
                SYSPath_Image = RegexTool.ReplaceRegValue(SYSPath_Image, @"//*/", "/");
                SYSPath_CSS = RegexTool.ReplaceRegValue(SYSPath_CSS, @"//*/", "/");

                WAPPath_JS = RegexTool.ReplaceRegValue(WAPPath_JS, @"//*/", "/");
                WAPPath_Image = RegexTool.ReplaceRegValue(WAPPath_Image, @"//*/", "/");
                WAPPath_CSS = RegexTool.ReplaceRegValue(WAPPath_CSS, @"//*/", "/");

                Path_JS = RegexTool.ReplaceRegValue(Path_JS, @"//*/", "/");
                Path_Image = RegexTool.ReplaceRegValue(Path_Image, @"//*/", "/");
                Path_CSS = RegexTool.ReplaceRegValue(Path_CSS, @"//*/", "/");
                if (!string.IsNullOrEmpty(ThemeDomain))
                {
                    SYSPath_JS = ThemeDomain + SYSPath_JS;
                    SYSPath_Image = ThemeDomain + SYSPath_Image;
                    SYSPath_CSS = ThemeDomain + SYSPath_CSS;

                    WAPPath_JS = ThemeDomain + WAPPath_JS;
                    WAPPath_Image = ThemeDomain + WAPPath_Image;
                    WAPPath_CSS = ThemeDomain + WAPPath_CSS;

                    Path_JS = ThemeDomain + Path_JS;
                    Path_Image = ThemeDomain + Path_Image;
                    Path_CSS = ThemeDomain + Path_CSS;
                }
                LanguagePath = RegexTool.ReplaceRegValue(LanguagePath, @"/*/", "/");
                SitePath = RegexTool.ReplaceRegValue(SitePath, @"/*/", "/");
                //LanguagePath = LanguagePath.TrimEnd('/');
                if (mainsite.id != site.id && site.Domain != "")
                {
                    string mainsitepath = "" + ShopCache.GetBaseConfig().HTTPServer + "://" + mainsite.Domain;
                    SYSPath_JS = mainsitepath + SYSPath_JS;
                    SYSPath_Image = mainsitepath + SYSPath_Image;
                    SYSPath_CSS = mainsitepath + SYSPath_CSS;

                    WAPPath_JS = mainsitepath + WAPPath_JS;
                    WAPPath_Image = mainsitepath + WAPPath_Image;
                    WAPPath_CSS = mainsitepath + WAPPath_CSS;

                    Path_JS = mainsitepath + Path_JS;
                    Path_Image = mainsitepath + Path_Image;
                    Path_CSS = mainsitepath + Path_CSS;

                    //WebPath = mainsitepath + "/";

                    LanguagePath = WebPath + lang.Path;
                    SitePath = WebPath;
                }
                //WebPath = WebPath.TrimEnd('/');
                WebPath = WebPath.TrimEnd('/');
                LanguagePath = LanguagePath.TrimEnd('/');
                SitePath = SitePath.TrimEnd('/');
                Content = Content.Replace("{/}", WebPath);
                Content = RegexTool.ReplaceRegValue(Content, @"{/[Ll][Aa][Nn][Gg]}", LanguagePath);
                Content = RegexTool.ReplaceRegValue(Content, @"{/[Ss][Ii][Tt][Ee]}", SitePath);
                if (site.IsMobile == 1)
                {
                    Content = RegexTool.ReplaceRegValue(Content, @"{/[Jj][Ss]}", WAPPath_JS);
                    Content = RegexTool.ReplaceRegValue(Content, @"{/[Cc][Ss][Ss]}", WAPPath_CSS);
                    Content = RegexTool.ReplaceRegValue(Content, @"{/[Ii][Mm][Aa][Gg][eE]}", WAPPath_Image);
                }
                else
                {
                    Content = RegexTool.ReplaceRegValue(Content, @"{/[Jj][Ss]}", SYSPath_JS);
                    Content = RegexTool.ReplaceRegValue(Content, @"{/[Cc][Ss][Ss]}", SYSPath_CSS);
                    Content = RegexTool.ReplaceRegValue(Content, @"{/[Ii][Mm][Aa][Gg][eE]}", SYSPath_Image);
                }
                Content = RegexTool.ReplaceRegValue(Content, @"{[Jj][Ss]}", Path_JS);
                Content = RegexTool.ReplaceRegValue(Content, @"{[Cc][Ss][Ss]}", Path_CSS);
                Content = RegexTool.ReplaceRegValue(Content, @"{[Ii][Mm][Aa][Gg][eE]}", Path_Image);
                Content = RegexTool.ReplaceRegValue(Content, @"{\$[Jj][Ss]}", Path_JS);
                Content = RegexTool.ReplaceRegValue(Content, @"{\$[Cc][Ss][Ss]}", Path_CSS);
                Content = RegexTool.ReplaceRegValue(Content, @"{\$[Ii][Mm][Aa][Gg][eE]}", Path_Image);
                Content = RegexTool.ReplaceRegValue(Content, @"{[Ll][Oo][Aa][Dd][Tt][Hh][Ee][Mm][Ee]}", "<% LoadPage(\"" + theme.Code + "\"," + lang.Site_id + ",\"" + lang.Code + "\",\"" + skin.Code + "\"); %>");

                //替换meta标签内容
                //if (skin.Code != "")
                //{

                Content = RegexTool.ReplaceRegValue(Content, @"{[Tt][Ii][Tt][Ll][eE]}", "<%=ThemePageMeta(\"" + skin.Code + "\",\"title\")%>");
                Content = RegexTool.ReplaceRegValue(Content, @"{[Kk][Ee][Yy][Ww][Oo][Rr][Dd][Ss]}", "<%=ThemePageMeta(\"" + skin.Code + "\",\"keywords\")%>");
                Content = RegexTool.ReplaceRegValue(Content, @"{[Dd][Ee][Ss][Cc][Rr][Ii][Pp][Tt][Ii][Oo][Nn]}", "<%=ThemePageMeta(\"" + skin.Code + "\",\"description\")%>");

                //Content = RegexTool.ReplaceRegValue(Content, @"{[Tt][Ii][Tt][Ll][eE].*?}", "<%=ThemePageMeta(\"" + skin.Code + "\",\"title\"," + RegexTool.GetRegValue(Content, @"{[Tt][Ii][Tt][Ll][eE],(.*?)}") + ")%>");
                //Content = RegexTool.ReplaceRegValue(Content, @"{[Kk][Ee][Yy][Ww][Oo][Rr][Dd][Ss].*?}", "<%=ThemePageMeta(\"" + skin.Code + "\",\"keywords\"," + RegexTool.GetRegValue(Content, @"{[Kk][Ee][Yy][Ww][Oo][Rr][Dd][Ss],(.*?)}") + ")%>");
                //Content = RegexTool.ReplaceRegValue(Content, @"{[Dd][Ee][Ss][Cc][Rr][Ii][Pp][Tt][Ii][Oo][Nn].*?}", "<%=ThemePageMeta(\"" + skin.Code + "\",\"description\"," + RegexTool.GetRegValue(Content, @"{[Dd][Ee][Ss][Cc][Rr][Ii][Pp][Tt][Ii][Oo][Nn],(.*?)}") + ")%>");
                //}

                //Content = RegexTool.ReplaceRegValue(Content, @"\t");
                //Content = RegexTool.ReplaceRegValue(Content, @"\r\n", " ");
                Content = RegexTool.ReplaceRegValue(Content, @"<!--.*?-->", "");
                Content = RegexTool.ReplaceRegValue(Content, @"{[Cc][Ll][Aa][Ss][Ss]:.*?}", "");
                //=============================================================


                Content = Shop.Bussiness.Theme.DoTagConvert(Content, lang.Code);//翻译语言标签
                if (skin.Code != "")
                {
                    if (!Shop.LebiAPI.Service.Instanse.Check("lebilicense"))
                    {

                        StringBuilder vsb = new StringBuilder();
                        vsb.Append("<div style=\"width:100%;text-align:center;font-size:12px;color:#999\">");
                        //vsb.Append("<asp:Label ID=\"LeBiLicense\" runat=\"server\" style=\"display:none;\"></asp:Label>");
                        vsb.Append("Powered by <a style=\"font-size:12px;color:#00497f\" href=\"http://www.lebi.cn/support/license/?url=" + bf.LicenseDomain + "\" target=\"_blank\" title=\"LebiShop多语言网店系统\">LebiShop</a> ");
                        vsb.Append("V<%=SYS.Version%>.<%=SYS.Version_Son%>");
                        vsb.Append("</div>");
                        Content += vsb.ToString();
                        //Content += "\r\n<!--此页面由Lebi主题管理器生成，请勿直接修改-->";
                    }
                }

                //Content += "<form runat=\"server\"></form>";

                HtmlEngine.Instance.WriteFile(PhysicsFileName, Content);
                Msg = "OK";
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Msg;
        }
        #endregion

        #region 生成后台自定义页面

        public static string AdminSkin_DoCodeConvert(string strIn)
        {
            string str = "";
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

                    temp = AdminSkin_DoPart(tempArry[i, 1]);
                    str = str.Replace("$$$" + i + "$$$", temp);

                }
            }
            return str;
        }
        /// <summary>
        /// 生成后台自定义页面
        /// </summary>
        /// <param name="StrIn"></param>
        /// <returns></returns>
        private static string AdminSkin_DoPart(string StrIn)
        {
            string str = StrIn;
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

        #endregion

        #region 广告位生成


        public static string DoAdvertCodeConvert(string strIn, Lebi_Theme_Advert advert, Lebi_Language lang, Lebi_Theme theme)
        {
            string str = "";
            string temp = "";
            int[,] d = IndexFlagArry(strIn);//记录开始，结尾标记的数组
            if (d.Length > 0)
            {
                int begin = 0;
                string[,] tempArry = new string[d.GetUpperBound(0) + 1, 2];//用于存放一个外层标记的临时数组
                for (int i = 0; i < d.GetUpperBound(0); i++)
                {
                    string vv = RegexTool.GetSubString(strIn, begin, d[i, 0]);
                    string v1 = RegexTool.GetSubString(strIn, begin, 0);
                    temp += RegexTool.GetSubString(strIn, begin, d[i, 0]) + "$$$" + i + "$$$";//挑选非标记部组合，将标记部分替换为$$$0$$$，$$$1$$$形式
                    begin = d[i, 1] + 2;
                    tempArry[i, 0] = "$$$" + i + "$$$";
                    tempArry[i, 1] = RegexTool.GetSubString(strIn, d[i, 0] + 2, d[i, 1]);//截取不包含开始，结尾标记的块
                }
                temp += RegexTool.GetSubString(strIn, begin, strIn.Length);
                str = temp;//str已经替除了全部的标记块

                for (int i = 0; i < tempArry.GetUpperBound(0); i++)
                {

                    temp = DoAdvertPart(tempArry[i, 1], advert, lang, theme);
                    str = str.Replace("$$$" + i + "$$$", temp);

                }
            }
            str = RegexTool.ReplaceRegValue(str, @"{%[Ww][Ii][Dd][tT][Hh]%}", advert.Width.ToString());
            str = RegexTool.ReplaceRegValue(str, @"{%[Hh][Ee][Ii][Gg][Hh][Tt]%}", advert.Height.ToString());
            return str;
        }
        private static string DoAdvertPart(string StrIn, Lebi_Theme_Advert advert, Lebi_Language lang, Lebi_Theme theme)
        {
            string str = StrIn;
            Site site = new Site();
            //============================================================================
            str = DoAdvertCodeConvert(StrIn, advert, lang, theme);//递归调用
            //============================================================================
            string Table = "";
            string Order = "";
            string Where = "";
            string PageSize = "";
            string page = "";
            string Top = "";
            int Mod = 1;
            string temp = str.Clone().ToString();
            string str_B = "";
            string str_E = "";
            string str_F = "";//mod数值失败时在循环尾部补齐的内容
            //============================================================================
            //参数处理
            //------------------------------
            //string key_ = "";
            //string key = "";
            //------------------------------

            //SQL = GetRegValue(temp, @"\$SQL:(.*?)[\n|\$]");      //这是SQL语句读取数据的方式，由于当前分页方式、分页存储过程的限制，不能实现

            Table = RegexTool.GetRegValue(temp, @"#[tT][aA][bB][lL][eE][:=](.*?)[#;]").Trim();    //参数结尾标记兼容 '换行'(\n) 和 '#',分号，空格
            Top = RegexTool.GetRegValue(temp, @"#[Tt][Oo][Pp][:=](.*?)[#;]").Trim();
            Order = RegexTool.GetRegValue(temp, @"#[oO][rR][dD][eE][rR][:=](.*?)[#;]").Trim();
            Where = RegexTool.GetRegValue(temp, @"#[wW][hH][eE][rR][eE][:=](.*?)[#;]").Trim();
            PageSize = RegexTool.GetRegValue(temp, @"#[pP][aA][gG][eE][sS][iI][zZ][eE][:=](.*?)[#;]").Trim();
            page = RegexTool.GetRegValue(temp, @"#[pP][aA][gG][eE][iI][nN][dD][eE][xX][:=](.*?)[#;]").Trim();
            str_B = RegexTool.GetRegValue(str, @"[Bb]{(.*?)}[Bb]");
            str_E = RegexTool.GetRegValue(str, @"[Ee]{(.*?)}[Ee]");
            str_F = RegexTool.GetRegValue(str, @"[Ff]{(.*?)}[Ff]");
            string mod = RegexTool.GetRegValue(temp, @"#[Mm][Oo][Dd][:=](.*?)[#;]").Trim();
            if (Top != "")
            {
                PageSize = Top;
                page = "1";
            }
            if (page == "")
                page = "1";
            if (PageSize == "")
                PageSize = "1";
            int.TryParse(mod, out Mod);
            if (Mod == 0)
                Mod = 1;
            //-------------------------------
            //过滤换行
            Table = RegexTool.ReplaceRegValue(Table, @"\r\n");
            Order = RegexTool.ReplaceRegValue(Order, @"\r\n");
            Where = RegexTool.ReplaceRegValue(Where, @"\r\n");
            PageSize = RegexTool.ReplaceRegValue(PageSize, @"\r\n");
            page = RegexTool.ReplaceRegValue(page, @"\r\n");
            //-------------------------------
            //-------------------------------
            //过滤多个空格
            Regex r = new Regex(@" +", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
            Table = r.Replace(Table, " ");
            Order = r.Replace(Order, " ");
            Where = r.Replace(Where, " ");
            PageSize = r.Replace(PageSize, " ");
            page = r.Replace(page, " ");

            //-------------------------------

            //过滤掉参数部分
            //str = ReplaceRegValue(str, @"#.*?[\n#;]");
            string show = str.Clone().ToString();
            show = RegexTool.ReplaceRegValue(show, @"#[tT][aA][bB][lL][eE][:=](.*?)[#;]");
            show = RegexTool.ReplaceRegValue(show, @"#[oO][rR][dD][eE][rR][:=](.*?)[#;]");
            show = RegexTool.ReplaceRegValue(show, @"#[wW][hH][eE][rR][eE][:=](.*?)[#;]");
            show = RegexTool.ReplaceRegValue(show, @"#[pP][aA][gG][eE][sS][iI][zZ][eE][:=](.*?)[#;]");
            show = RegexTool.ReplaceRegValue(show, @"#[pP][aA][gG][eE][iI][nN][dD][eE][xX][:=](.*?)[#;]");
            show = RegexTool.ReplaceRegValue(show, @"#[tT][Oo][Pp][:=](.*?)[#;]");
            show = RegexTool.ReplaceRegValue(show, @"[Bb]{(.*?)}[Bb]");
            show = RegexTool.ReplaceRegValue(show, @"[Ee]{(.*?)}[Ee]");
            show = RegexTool.ReplaceRegValue(show, @"\d+{(.*?)}\d+");
            show = RegexTool.ReplaceRegValue(show, @"[Ff]{(.*?)}[Ff]");
            //============================================================================

            //取数据
            StringBuilder sb = new StringBuilder();
            //===================================================================================
            List<Lebi_Advert> imgs = B_Lebi_Advert.GetList("Theme_Advert_id=" + advert.id + " and ','+Language_ids+',' like '%," + lang.id + ",%'", "Sort desc", Convert.ToInt32(PageSize), 1);
            int i = 1;
            int count = imgs.Count;
            foreach (Lebi_Advert img in imgs)
            {
                if (str_B != "" && i == 1)
                    temp = str_B;
                else if (str_E != "" && i == count)
                    temp = str_E;
                else
                {
                    temp = RegexTool.GetRegValue(str, @"" + i + "{(.*?)}" + i + "");
                    if (temp == "")
                        temp = show.Clone().ToString();
                }
                if (ShopCache.GetMainSite().Domain == "" || ShopCache.GetMainSite().id == lang.Site_id)
                    temp = RegexTool.ReplaceRegValue(temp, @"{%[Ii][Mm][Aa][Gg][Ee]%}", ThemeUrl.CheckURL(site.WebPath + "/" + theme.Path_Advert + "/" + img.Image));
                else
                    temp = RegexTool.ReplaceRegValue(temp, @"{%[Ii][Mm][Aa][Gg][Ee]%}", ThemeUrl.CheckURL("" + ShopCache.GetBaseConfig().HTTPServer + "://" + ShopCache.GetMainSite().Domain + "/" + site.WebPath + "/" + theme.Path_Advert + "/" + img.Image));
                temp = RegexTool.ReplaceRegValue(temp, @"{%[Uu][rR][Ll]%}", img.URL);
                temp = RegexTool.ReplaceRegValue(temp, @"{%[Tt][Ii][Tt][Ll][Ee]%}", img.Title);
                temp = RegexTool.ReplaceRegValue(temp, @"{%[Dd][Ee][Ss][Cc][Rr][Ii][Pp][Tt][Ii][Oo][Nn]%}", img.Description);
                temp = RegexTool.ReplaceRegValue(temp, @"{%[Ii]%}", i.ToString());
                temp = RegexTool.ReplaceRegValue(temp, @"{%[Cc][Oo][Uu][Nn][Tt]%}", count.ToString());
                temp = RegexTool.ReplaceRegValue(temp, @"{[Ii]}", i.ToString());
                temp = RegexTool.ReplaceRegValue(temp, @"{[Cc][Oo][Uu][Nn][Tt]}", count.ToString());
                sb.Append(temp);
                i++;
            }
            if (count % Mod > 0 && Mod > 1)
            {
                sb.Append(str_F);
            }
            return sb.ToString();

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
