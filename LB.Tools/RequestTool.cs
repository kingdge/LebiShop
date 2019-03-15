using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace LB.Tools
{
    public static class RequestTool
    {

        #region IP,域名，URL
        /// <summary>
        /// 客户机实际IP 地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            try
            {
                return LB.Tools.IP.GetIP;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 客户机使用IP 地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            try
            {
                string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (null == result || result == String.Empty)
                {
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (null == result || result == String.Empty)
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }
                result = StringTool.HtmlFiltrate(result);
                return result;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 返回上一个页面的地址不带域名
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrerNonDomain()
        {
            try
            {
                return System.Web.HttpContext.Current.Request.UrlReferrer.PathAndQuery;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                //retVal = HttpContext.Current.Request.UrlReferrer.ToString();
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }
            if (retVal == null)
                return "";
            return retVal;
        }

        /// <summary>
        /// 返回上一个页面的地址,不带参数
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrerNoParas()
        {
            string retVal = null;
            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
                retVal = RemoveParas(retVal);

            }
            catch { }
            if (retVal == null)
                return "";
            return retVal;
        }
        /// <summary>
        /// 返回上个页面的域名
        /// </summary>
        /// <returns></returns>
        public static string GetReferrerDomain()
        {
            string retVal = "";
            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.Host;
            }
            catch
            {
                retVal = "";
            }
            return retVal;
        }
        /// <summary>
        /// 返回当前页面的域名
        /// </summary>
        /// <returns></returns>
        public static string GetRequestDomain()
        {
            string retVal = "";
            try
            {
                retVal = HttpContext.Current.Request.Url.Host;
            }
            catch
            {
                retVal = "";
            }
            return retVal;
        }
        /// <summary>
        /// 返回当前页面的域名+端口
        /// </summary>
        /// <returns></returns>
        public static string GetRequestDomainPort()
        {
            string retVal = "";
            try
            {
                retVal = HttpContext.Current.Request.Url.Authority;
            }
            catch
            {
                retVal = "";
            }
            return retVal;
        }
        /// <summary>
        /// 返回当前页面端口
        /// </summary>
        /// <returns></returns>
        public static int GetRequestPort()
        {
            int retVal = 0;
            try
            {
                retVal = HttpContext.Current.Request.Url.Port;
            }
            catch
            {
                retVal = 0;
            }
            return retVal;
        }
        /// <summary>
        /// 获取当前请求路径的完整信息，带参数 
        /// 形如：http://localhost:2721/Default.aspx?o=312 
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl()
        {
            try
            {
                return System.Web.HttpContext.Current.Request.Url.ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前请求路径及参数的信息 不带域名
        /// 形如：/Default.aspx?o=312 
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrlNonDomain()
        {
            //Request.RawUrl 也可以
            try
            {
                return System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前请求路径的文件部分 不带参数
        /// 形如：/admin/GlobalManage/ClassList.asp
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrlNoParas()
        {
            try
            {
                return System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前请求的完整路径，域名、文件、不带参数
        /// 形如：/admin/GlobalManage/ClassList.asp
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrlAllNoParas()
        {
            try
            {
                return RemoveParas(GetRequestUrl());
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 移除URL中的参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RemoveParas(string url)
        {
            int index = url.IndexOf("?");
            if (index > 0)
                url = url.Remove(index);
            return url;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string RequestString(string nKey)
        {
            return RequestString(nKey, "");
        }
        public static string RequestString(string nKey, string def)
        {
            var ojb = HttpContext.Current.Request.QueryString[nKey];
            if (ojb != null)
                return StringTool.InjectFiltrate(ojb.Trim());
            ojb = HttpContext.Current.Request.Form[nKey];
            if (ojb != null)
                return StringTool.InjectFiltrate(ojb.Trim());
            return def;
        }
        public static string RequestSafeString(string nKey, string def = "")
        {
            string temp = RequestString(nKey, def);
            if (temp == "")
                return def;
            return HttpUtility.HtmlEncode(temp);
        }
        /// <summary>
        /// 给前台编辑器使用
        /// </summary>
        /// <param name="nKey"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string RequestStringForUserEditor(string nKey, string def)
        {
            string temp = RequestString(nKey, def);
            if (temp == "")
                return def;
            return StringTool.FilterScript(temp);
        }
        public static string RequestStringForUserEditor(string nKey)
        {
            return RequestStringForUserEditor(nKey, "");
        }
        public static string RequestSafeString(string nKey)
        {
            return RequestSafeString(nKey, "");
        }
        public static int RequestInt(string nKey)
        {

            return RequestInt(nKey, 0);
        }

        public static int RequestInt(string nKey, int defaultvalue)
        {
            int temp;
            try
            {
                temp = Convert.ToInt32(RequestString(nKey, ""));
            }
            catch
            {
                temp = defaultvalue;
            }
            return temp;
        }
        public static long RequestLong(string nKey, long defaultvalue)
        {

            long temp;
            try
            {
                temp = Convert.ToInt64(RequestString(nKey, ""));
            }
            catch
            {
                temp = defaultvalue;
            }
            return temp;
        }
        public static double RequestDouble(string nKey, double defaultvalue = 0)
        {
            double temp;
            try
            {
                temp = Convert.ToDouble(RequestString(nKey, ""));
            }
            catch
            {
                temp = defaultvalue;
            }
            return temp;
        }
        public static DateTime RequestTime(string nKey, DateTime defValue)
        {
            try
            {
                return Convert.ToDateTime(RequestString(nKey, ""));
            }
            catch
            {
                return defValue;
            }
            //return TypeParseHelper.StrToDateTime(HttpContext.Current.Request[strName], defValue);
        }
        public static DateTime RequestTime(string nKey)
        {
            return RequestTime(nKey, System.DateTime.Now);
        }
        public static DateTime RequestDate(string nKey, DateTime defValue)
        {
            try
            {
                return Convert.ToDateTime(RequestString(nKey, ""));
            }
            catch
            {
                return defValue;
            }
            //return TypeParseHelper.StrToDateTime(HttpContext.Current.Request[strName], defValue);
        }
        public static DateTime RequestDate(string nKey)
        {
            return RequestTime(nKey, System.DateTime.Now.Date);
        }
        public static float RequestFloat(string nKey, float defValue)
        {
            try
            {
                return Convert.ToSingle(RequestString(nKey, ""));
            }
            catch
            {
                return defValue;
            }
        }
        public static Decimal RequestDecimal(string nKey)
        {

            return RequestDecimal(nKey, 0);

        }
        public static Decimal RequestDecimal(string nKey, Decimal defValue)
        {
            try
            {
                return Convert.ToDecimal(RequestString(nKey, ""));
            }
            catch
            {
                return defValue;
            }
        }
        /// <summary>
        /// 获取真假类型
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static bool RequestBool(string nKey)
        {
            string v = RequestString(nKey);
            if (v == "1" || v.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return true;
            return false;
        }


        /// <summary>
        /// 将用户请求的query string 转换成 JSON格式的串
        /// 如果不存在 返回 {}
        /// </summary>
        /// <returns></returns>
        public static string GetQueryJson()
        {
            var keys = System.Web.HttpContext.Current.Request.QueryString.AllKeys;
            string value = "";
            string jsonRequestParas = "{";
            for (int i = 0; i < keys.Length; i++)
            {
                value = GetQueryString(keys[i]);
                if (keys[i] != "" && value != "")
                {
                    jsonRequestParas += "\"" + keys[i] + "\":\"" + value + "\"";
                    if (i < keys.Length - 1)
                        jsonRequestParas += ",";
                }
            }
            if (jsonRequestParas != "" && jsonRequestParas[jsonRequestParas.Length - 1] == ',')
                jsonRequestParas = jsonRequestParas.Remove(jsonRequestParas.Length - 1);
            jsonRequestParas += "}";
            return jsonRequestParas;
        }


        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[strName]);
        }

        public static string GetQueryString(string strName, string defValue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null || string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[strName].ToString()))
            {
                return defValue;
            }
            return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[strName]);
        }
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }
        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return TypeParseHelper.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static int GetFormInt(string strName)
        {
            return GetFormInt(strName, 0);
        }
        public static List<int> GetFormInts(string strName)
        {
            return GetFormInts(strName, ",", true);
        }

        /// <summary>
        /// 获取1,2,4　将其转　list
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="splitChar">分割标识符</param>
        /// <param name="filtSame">是否过滤重复</param>
        /// <returns></returns>
        public static List<int> GetFormInts(string strName, string splitChar, bool filtSame)
        {
            List<int> list = new List<int>();
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return list;
            }
            var arr = StringTool.SplitString(GetFormString(strName), splitChar);
            int temp = 0;
            foreach (var i in arr)
            {
                temp = TypeParseHelper.StrToInt(i, int.MinValue);
                if (temp != int.MinValue)
                {
                    if (filtSame && list.Contains(temp))
                        continue;
                    list.Add(temp);
                }
            }
            return list;
        }

        public static long GetFormLong(string strName, long defValue)
        {
            return TypeParseHelper.StrToLong(HttpContext.Current.Request.Form[strName], defValue);
        }

        public static decimal GetFormDecimal(string strName, decimal defValue)
        {
            return TypeParseHelper.StrToDecimal(HttpContext.Current.Request.Form[strName], defValue);
        }

        public static float GetFormFloat(string strName, float defValue)
        {
            return TypeParseHelper.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }
        /// <summary>
        /// 获取日期 
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static DateTime GetFormDataTime(string strName, DateTime defValue)
        {
            return TypeParseHelper.StrToDateTime(HttpContext.Current.Request.Form[strName], defValue);
        }


        public static long GetCookieLong(string str, long def)
        {
            if (HttpContext.Current.Request.Cookies[str] != null)
            {
                return TypeParseHelper.StrToLong(HttpContext.Current.Request.Cookies[str].Value, def);
            }
            return def;
        }

        /// <summary>
        /// 获取COOKIE中的值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookieString(string cookieName, string defValue)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                return HttpUtility.UrlDecode(cookie.Value);
            }
            return defValue;
        }
        /// <summary>
        /// http POST请求url
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="method_name"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostResponse(string url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Timeout = 20000;

            HttpWebResponse response = null;

            try
            {
                StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postData);
                if (swRequestWriter != null)
                    swRequestWriter.Close();

                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        /// <summary>
        /// 获取远程内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            //string url = "http://" + HttpContext.Current.Request.Url.Authority + "/admin/SkinTemp/Part/" + path;
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] b = wc.DownloadData(url);
            string content = System.Text.Encoding.GetEncoding("utf-8").GetString(b);
            return content;
        }
        /// <summary>
        /// 返回WEBCONFIG中设置的值
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public static string GetConfigKey(string KeyName)
        {
            try
            {
                string res = System.Configuration.ConfigurationManager.AppSettings[KeyName];
                if (res == null)
                    return "";
                return res;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>  
        /// 获得IP详细地址方法一  
        /// </summary>  
        /// <param name="ip">ip地址</param>  
        /// <returns>IP详细地址</returns>  
        /// {"ret":1,"start":-1,"end":-1,"country":"\u4e2d\u56fd","province":"\u5c71\u4e1c","city":"\u9752\u5c9b","district":"","isp":"","type":"","desc":""}
        public static string getIpInfoOne(string ip)
        {
            try
            {
                string res = HtmlEngine.CetHtml("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + ip);
                string country = RegexTool.GetRegValue(res, "\"country\":\"(.*?)\"");
                string province = RegexTool.GetRegValue(res, "\"province\":\"(.*?)\"");
                string city = RegexTool.GetRegValue(res, "\"city\":\"(.*?)\"");
                string district = RegexTool.GetRegValue(res, "\"district\":\"(.*?)\"");
                string ipInfo = FromUnicodeString(country) + FromUnicodeString(province) + FromUnicodeString(city) + FromUnicodeString(district);
                return ipInfo;
            }
            catch
            {
                return "";
            }
        }
        public static string FromUnicodeString(string str)
        {
            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException ex)
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }


        /// <summary>  
        /// 获得每页显示数量
        /// </summary>  
        /// <returns>每页显示数量</returns>  
        public static int getpageSize(int pageSize)
        {
            string PageSize = LB.Tools.CookieTool.GetCookieString("pageSize");
            if (PageSize == "")
            {
                PageSize = pageSize.ToString();
            }
            else
            {
                PageSize = LB.Tools.CookieTool.GetCookieString("pageSize");
            }
            return int.Parse(PageSize);
        }

    }
}
