using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LB.Tools
{
    public class StringTool
    {
        /**
        * 将JSON字符串处理成javascript可以识别的
        *
        * @param ors
        * @return
        */
        public static String stringToJson(string s)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    // case '\\': //如果不处理单引号，可以释放此段代码，若结合下面的方法处理单引号就必须注释掉该段代码 
                    // sb.append("\\\\"); 
                    // break; 
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b': //退格 
                        sb.Append("\\b");
                        break;
                    case '\f': //走纸换页 
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n"); //换行 
                        break;
                    case '\r': //回车 
                        sb.Append("\\r");
                        break;
                    case '\t': //横向跳格
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }


        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        /// <summary>
        /// 防止SQL注入
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string InjectFiltrate(string str)
        {
            if (IsSafeSqlString(str))
            {

            }
            else
            {
                //str = str.Replace("<", "&lt;");
                //str = str.Replace(">", "&gt;");
                str = str.Replace("'", "&#180;");
                //str = str.Replace("exec", "");
                //str = str.Replace(";", "");
            }
            return str;
        }
        public static string HtmlFiltrate(string str)
        {
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }
        //移除非法或不友好字符
        public static string FilterBadWords(string[] bwords, string chkStr)
        {
            //这里的非法和不友好字符由你任意加，用“|”分隔，支持正则表达式,由于本Blog禁止贴非法和不友好字符，所以这里无法加上。
            if (chkStr == "")
            {
                return "";
            }

            int i, j;
            string str;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < bwords.Length; i++)
            {
                str = bwords[i].ToString().Trim();
                string regStr, toStr;
                regStr = str;
                Regex r = new Regex(regStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                Match m = r.Match(chkStr);
                if (m.Success)
                {
                    j = m.Value.Length;
                    sb.Insert(0, "*", j);
                    toStr = sb.ToString();
                    chkStr = Regex.Replace(chkStr, regStr, toStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                }
                sb.Remove(0, sb.Length);
            }
            return chkStr;
        }

        //替换后台数据录入过程中的中文字符
        public static string ReplaceChieseChar(string str)
        {
            return str.Replace("，", ",");
        }
        //提取图片
        public static string GetImageUrlFromContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            string imageUrl = "";
            string regexImgSRC = @"<img(?<Attributes1>[\s\S]*?)src=(""{1}|'{1}|)(?<picture>[^\[^>]*?(gif|jpg|jpeg|bmp|png|tif|tiff))(""{1}|'{1}|)(?<Attributes2>[\s\S]*?)>";
            Regex regImgSrc = new Regex(regexImgSRC, RegexOptions.IgnoreCase);
            Match matchImgSrc = regImgSrc.Match(content);
            if (matchImgSrc.Success)
            {
                imageUrl = matchImgSrc.Groups["picture"].Value;
            }
            return imageUrl;
        }


        public static string FilterScript(string content)
        {
            string commentPattern = @"(?'comment'<!--.*?--[ \n\r]*>)";
            string embeddedScriptComments = @"(\/\*.*?\*\/|\/\/.*?[\n\r])";
            string scriptPattern = String.Format(@"(?'script'<[ \n\r]*script[^>]*>(.*?{0}?)*<[ \n\r]*/script[^>]*>)", embeddedScriptComments);
            // 包含注释和Script语句
            string pattern = String.Format(@"(?s)({0}|{1})", commentPattern, scriptPattern);

            return StripScriptAttributesFromTags(Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase));
        }

        private static string StripScriptAttributesFromTags(string content)
        {
            string eventAttribs = @"on(blur|c(hange|lick)|dblclick|focus|keypress|(key|mouse)(down|up)|(un)?load
                    |mouse(move|o(ut|ver))|reset|s(elect|ubmit))";

            string pattern = String.Format(@"(?inx)\<(\w )\s((?'attribute'(?'attributeName'{0})\s*=\s*(?'delim'['""]?)(?'attributeValue'[^'"">] )(\3))|(?'attribute'(?'attributeName'href)\s*=\s*(?'delim'['""]?)(?'attributeValue'javascript[^'"">] )(\3))|[^>])*\>", eventAttribs);
            Regex re = new Regex(pattern);
            // 使用MatchEvaluator的委托
            return re.Replace(content, new MatchEvaluator(StripAttributesHandler));
        }

        private static string StripAttributesHandler(Match m)
        {
            if (m.Groups["attribute"].Success)
            {
                return m.Value.Replace(m.Groups["attribute"].Value, "");
            }
            else
            {
                return m.Value;
            }
        }

        public static string FilterAHrefScript(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @" href[ ^=]*= *[\s\S]*script *:";
            return Regex.Replace(newstr, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterSrc(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @" src *= *['""]?[^\.] \.(js|vbs|asp|aspx|php|jsp)['""]";
            return Regex.Replace(newstr, regexstr, @"", RegexOptions.IgnoreCase);
        }


        public static string FilterInclude(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @"<[\s\S]*include *(file|virtual) *= *[\s\S]*\.(js|vbs|asp|aspx|php|jsp)[^>]*>";
            return Regex.Replace(newstr, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterHtml(string content)
        {
            System.Text.RegularExpressions.Regex regexScript = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string newstr = regexScript.Replace(content, "");
            //string newstr = FilterScript(content);

            string regexstr = @"<[^>]*>";
            return Regex.Replace(newstr, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterObject(string content)
        {
            string regexstr = @"(?i)<Object([^>])*>(\w|\W)*</Object([^>])*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterIframe(string content)
        {
            string regexstr = @"(?i)<Iframe([^>])*>(\w|\W)*</Iframe([^>])*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterFrameset(string content)
        {
            string regexstr = @"(?i)<Frameset([^>])*>(\w|\W)*</Frameset([^>])*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        //移除非法或不友好字符
        private static string FilterBadWords(string chkStr)
        {
            //这里的非法和不友好字符由你任意加，用“|”分隔，支持正则表达式,由于本Blog禁止贴非法和不友好字符，所以这里无法加上。
            string BadWords = @"";
            if (chkStr == "")
            {
                return "";
            }

            string[] bwords = BadWords.Split('#');
            int i, j;
            string str;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < bwords.Length; i++)
            {
                str = bwords[i].ToString().Trim();
                string regStr, toStr;
                regStr = str;
                Regex r = new Regex(regStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                Match m = r.Match(chkStr);
                if (m.Success)
                {
                    j = m.Value.Length;
                    sb.Insert(0, "*", j);
                    toStr = sb.ToString();
                    chkStr = Regex.Replace(chkStr, regStr, toStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                }
                sb.Remove(0, sb.Length);
            }
            return chkStr;
        }

        /// <summary>
        /// 过滤所有
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FilterAll(string content)
        {
            content = FilterHtml(content);
            content = FilterScript(content);
            content = FilterAHrefScript(content);
            content = FilterObject(content);
            content = FilterIframe(content);
            content = FilterFrameset(content);
            content = FilterSrc(content);
            content = FilterBadWords(content);
            //content = FilterInclude(content);
            return content;
        }

        /// <summary>
        /// 清除脚本
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string ClearScript(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return "";
            }

            string content = strIn;
            content = FilterScript(content);
            content = FilterAHrefScript(content);
            content = FilterObject(content);
            content = FilterIframe(content);
            content = FilterFrameset(content);
            content = FilterSrc(content);
            content = FilterInclude(content);
            return content;


        }
        /// <summary>
        /// 按照给定位置截取字符串
        /// </summary>
        /// <param name="strIn">待截字符串</param>
        /// <param name="start">开始位置</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string SubString(string strIn, int start, int length)
        {
            if (strIn.Length > (length + start))
            {
                return strIn.Substring(start, length);
            }
            else
                return strIn;
        }
        /// <summary>
        /// 加密不安全的SQL关键字
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string UnSafeStringEncoce(string strIn)
        {

            if (string.IsNullOrEmpty(strIn))
            {

                return "";
            }
            strIn = strIn.Replace(" ", "&nbsp;");
            strIn = strIn.Replace("'", "&#8217;");
            strIn = strIn.Replace("select", "sel&#101;ct");
            strIn = strIn.Replace("join", "jo&#105;n");
            strIn = strIn.Replace("union", "un&#105;on");
            strIn = strIn.Replace("where", "wh&#101;re");
            strIn = strIn.Replace("insert", "ins&#101;rt");
            strIn = strIn.Replace("delete", "del&#101;te");
            strIn = strIn.Replace("update", "up&#100;ate");
            strIn = strIn.Replace("like", "lik&#101;");
            strIn = strIn.Replace("drop", "dro&#112;");
            strIn = strIn.Replace("create", "cr&#101;ate");
            strIn = strIn.Replace("modify", "mod&#105;fy");
            strIn = strIn.Replace("rename", "ren&#097;me");
            strIn = strIn.Replace("alter", "alt&#101;r");
            strIn = strIn.Replace("cast", "ca&#115;t");
            strIn = strIn.Replace("script", "scr&#105;pt");
            strIn = strIn.Replace("src", "&#115;rc");
            return strIn;
        }
        public static string UnSafeStringDecode(string strIn)
        {

            if (string.IsNullOrEmpty(strIn))
            {

                return "";
            }
            strIn = strIn.Replace("&#115;rc", "src");
            strIn = strIn.Replace("scr&#105;pt", "script");
            strIn = strIn.Replace("ca&#115;t", "cast");
            strIn = strIn.Replace("alt&#101;r", "alter");
            strIn = strIn.Replace("ren&#097;me", "rename");
            strIn = strIn.Replace("mod&#105;fy", "modify");
            strIn = strIn.Replace("cr&#101;ate", "create");
            strIn = strIn.Replace("dro&#112;", "drop");
            strIn = strIn.Replace("lik&#101;", "like");
            strIn = strIn.Replace("up&#100;ate", "update");
            strIn = strIn.Replace("del&#101;te", "delete");
            strIn = strIn.Replace("ins&#101;rt", "insert");
            strIn = strIn.Replace("wh&#101;re", "where");
            strIn = strIn.Replace("un&#105;on", "union");
            strIn = strIn.Replace("jo&#105;n", "join");
            strIn = strIn.Replace("sel&#101;ct", "select");
            strIn = strIn.Replace("&#8217;", "'");
            strIn = strIn.Replace("&nbsp;", " ");

            return strIn;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static List<string> SplitStringList(string strContent, string strSplit)
        {
            List<string> lists = new List<string>();

            if (strContent.IndexOf(strSplit) < 0)
            {
                lists.Add(strContent);
                return lists;
            }
            //除了.$^{[(|)*+?\外
            // @strSplit.Replace(".", @"\.").Replace("$", @"\$").Replace("^", @"\^").Replace("{", @"\{").Replace("[", @"\[").Replace("(", @"\(").Replace("|", @"\|").Replace(")", @"\)").Replace("*", @"\*").Replace("+", @"\+").Replace("?", @"\?").Replace(@"\", @"\\")
            foreach (string list in Regex.Split(strContent, @strSplit.Replace(".", @"\.").Replace("$", @"\$").Replace("^", @"\^"), RegexOptions.IgnoreCase))
            {
                lists.Add(list);
            }
            return lists;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static List<int> SplitStringListInt(string strContent, string strSplit)
        {
            List<int> lists = new List<int>();

            int temp = int.MinValue;
            if (strContent.IndexOf(strSplit) < 0)
            {
                temp = TypeParseHelper.StrToInt(strContent, temp);
                if (int.MinValue != temp)
                    lists.Add(temp);
                return lists;
            }
            foreach (string list in Regex.Split(strContent, @strSplit.Replace(".", @"\."), RegexOptions.IgnoreCase))
            {
                temp = TypeParseHelper.StrToInt(list, temp);
                if (int.MinValue != temp)
                    lists.Add(temp);
            }
            return lists;
        }

        /// <summary>
        /// 在字符串中插入指定个数的字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="insertContent"></param>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string InsertString(string str, string insertContent, int count, int index)
        {
            if (str.Length >= index)
            {
                StringBuilder builder = new StringBuilder();
                while (count > 0)
                {
                    builder.AppendFormat(insertContent);
                    count--;
                }

                str = str.Insert(index, builder.ToString());
            }
            return str;
        }

        /// <summary>
        /// 判断二个LIST是否含有相同的元素
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static bool JudgeListSameContent(List<int> l1, List<int> l2)
        {
            bool result = true;
            foreach (var i in l1)
            {
                if (!l2.Contains(i))
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                foreach (var i in l2)
                {
                    if (!l1.Contains(i))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        public static string GetGoodString(string strSource, int maxLength)
        {

            if (StringTool.GetLength(strSource) > maxLength)
            {

                return StringTool.GetSubString(strSource, maxLength, "");
            }
            return strSource;
        }
        public static string GetGoodStringByOther(string strScoure, string strOther, int maxTotal)
        {
            return GetGoodString(strScoure, maxTotal - StringTool.GetLength(strOther));
        }
        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                if (bsSrcString.Length > p_Length)
                {
                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = 0; i < p_Length; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_Length - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, bsResult, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }

            }

            return myResult;
        }
        public static int GetLength(string strSource)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            int nLength = strSource.Length;

            for (int i = 0; i < strSource.Length; i++)
            {
                if (regex.IsMatch(strSource.Substring(i, 1)))
                {
                    nLength++;
                }
            }

            return nLength;
        }
        /// <summary>
        /// 判断是否包含汉字
        /// </summary>
        /// <param name="text">要检查的字符串</param>
        public static bool ishanzi(string text)
        {
            char[] c = text.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                    return true;
            }
            return false;
        }
    }
}
