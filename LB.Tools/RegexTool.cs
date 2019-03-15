using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LB.Tools
{
    public class RegexTool
    {
        /// <summary>
        /// 根据标记截取字符串，没有找到标记则换回原值
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <param name="start">开始标记</param>
        /// <param name="end">结束标记</param>
        /// <param name="mode">截取形式0不含标记1包含标记</param>
        /// <returns></returns>
        public static string GetSubString(string strIn, string begin, string end, int mode)
        {
            int start = strIn.IndexOf(begin);
            int stop = strIn.IndexOf(end);
            int len = strIn.Length;
            //strIn.Replace("","",)
            string str = strIn;
            //======================================
            //处理开头
            if (start != -1)//有开始标记
            {

                if (mode == 0)
                {
                    start = start + begin.Length;
                }
                str = strIn.Substring(start, (len - start));
            }
            //=====================================
            //处理结尾
            if (stop != -1)//有结尾标记
            {
                stop = str.IndexOf(end);
                if (mode == 1)
                {
                    stop = stop + end.Length;
                }
                if (stop < str.Length)
                    str = str.Remove(stop);
            }
            return str;
        }
        public static string GetSubString(string strIn, string begin, int endindex, int mode)
        {
            int start = strIn.IndexOf(begin);
            int stop = endindex;
            int len = strIn.Length;
            string str = strIn;
            //======================================
            //处理开头
            if (start != -1)//有开始标记
            {

                if (mode == 0)
                {
                    start = start + begin.Length;
                }
                str = strIn.Substring(start, (len - start));
            }
            //=====================================
            //处理结尾
            if (stop > 0)//有结尾标记
            {
                stop = stop - start;
                if (stop < str.Length)
                    str = str.Remove(stop);
            }
            return str;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="beginindex">开始位置</param>
        /// <param name="endindex">结束位置</param>
        /// <returns></returns>
        public static string GetSubString(string strIn, int beginindex, int endindex)
        {
            int start = beginindex;
            int stop = endindex;
            int len = strIn.Length;
            string str = strIn;
            //======================================
            //处理开头
            if (start > 0)
            {
                str = strIn.Substring(start, (len - start));
            }
            //=====================================
            //处理结尾
            if (stop > 0)
            {
                stop = stop - start;
                if (stop < str.Length)
                    str = str.Remove(stop);
            }
            if (start == 0 && stop == 0)
                str = "";
            return str;
        }
        /// <summary>
        /// 根据正则提取内容
        /// </summary>
        /// <param name="content">源字符串</param>
        /// <param name="pattern">正则</param>
        /// <returns></returns>
        public static string GetRegValue(string content, string pattern, bool IgnoreCase = false)
        {
            if (string.IsNullOrEmpty(content))
                return "";
            string res = "";
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);

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
        public static int GetRegCount(string content, string pattern, bool IgnoreCase = false)
        {
            int res = 0;
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            res = r.Matches(content).Count;
            return res;
        }
        /// <summary>
        /// 正则表达式匹配数组
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] GetSimpleRegResultArray(string content, string pattern, bool IgnoreCase = false)
        {
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            MatchCollection mc = r.Matches(content);
            string[] Arry = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                //res += m.Index + "-" + m.Result("$1");
                Arry[i] = m.Result("$1");
                //Arry[i] = m.Value;
                i++;
            }
            return Arry;
        }
        /// <summary>
        /// 正则表达式--检测是否匹配
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Check(string content, string pattern, bool IgnoreCase = false)
        {
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            return r.IsMatch(content);

        }
        public static string[,] GetRegResultArray(string content, string pattern, bool IgnoreCase = false)
        {
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
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
        public static string[,] GetRegArray(string content, string pattern, bool IgnoreCase = false)
        {
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            MatchCollection mc = r.Matches(content);

            string[,] Arry = new string[mc.Count + 1, 2];
            int i = 0;
            foreach (Match m in mc)
            {
                //res += m.Index + "-" + m.Result("$1");
                Arry[i, 0] = m.Index.ToString();
                Arry[i, 1] = m.Value;
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
        public static string ReplaceRegValue(string content, string pattern, bool IgnoreCase = false)
        {
            string res = "";
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            res = r.Replace(content, "");
            return res;
        }
        /// <summary>
        /// 过滤正则内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string ReplaceRegValue(string content, string pattern, string tostr, bool IgnoreCase = false)
        {
            string res = "";
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            res = r.Replace(content, tostr);
            return res;
        }
        /// <summary>
        /// 过滤正则内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <param name="tostr"></param>
        /// <param name="count">替换次数</param>
        /// <returns></returns>
        public static string ReplaceRegValue(string content, string pattern, string tostr, int count, bool IgnoreCase = false)
        {
            string res = "";
            Regex r;
            if (IgnoreCase)
                r = new Regex("" + pattern + "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            else
                r = new Regex("" + pattern + "", RegexOptions.Singleline);
            res = r.Replace(content, tostr, count);
            return res;
        }
    }
}
