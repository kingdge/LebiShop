using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace LB.Tools
{
    public sealed class CookieTool
    {
        #region Write cookie
        /// <summary>
        /// write cookie value
        /// </summary>
        /// <param name="CookieName">cookie name</param>
        /// <param name="Nvc">NameValueCollection</param>
        /// <param name="days">cookie date</param>
        /// <param name="Domain">Domain</param>
        /// <returns>bool</returns>
        public static bool WriteCookie(string CookieName, NameValueCollection Nvc, int days, string Domain)
        {
            bool ReturnBoolValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName))
            {
                HttpCookie httpCookie = new HttpCookie(CookieName);
                for (int i = 0; i < Nvc.Count; i++)
                {
                    httpCookie.Values.Add(Nvc.GetKey(i), Nvc.Get(i));
                }
                if (days > 0)
                    httpCookie.Expires = DateTime.Now.AddDays(days);
                if (!string.IsNullOrEmpty(Domain))
                {
                    httpCookie.Domain = Domain;
                }
                //httpCookie.HttpOnly = true;
                HttpContext.Current.Response.AppendCookie(httpCookie);
                ReturnBoolValue = true;
            }
            return ReturnBoolValue;
        }

        /// <summary>
        /// write cookie value
        /// </summary>
        /// <param name="CookieName">cookie name</param>
        /// <param name="Nvc">NameValueCollection</param>
        /// <param name="days">cookie date</param>
        /// <returns>bool</returns>
        public static bool WriteCookie(string CookieName, NameValueCollection Nvc, int days)
        {
            return WriteCookie(CookieName, Nvc, days, null);
        }
        public static bool WriteCookie(string CookieName, NameValueCollection Nvc)
        {
            return WriteCookie(CookieName, Nvc, 0, null);
        }
        #endregion

        #region Add Cookie Key value
        public static bool AddCookie(string CookieName, NameValueCollection Nvc)
        {
            bool ReturnBoolValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName))
            {
                for (int i = 0; i < Nvc.Count; i++)
                {
                    HttpContext.Current.Request.Cookies[CookieName].Values.Add(Nvc.GetKey(i), Nvc.Get(i));
                }
            }
            return ReturnBoolValue;
        }
        #endregion


        #region update cookie
        /// <summary>
        /// update cookie
        /// </summary>
        /// <param name="CookieName">cookie name</param>
        /// <param name="Nvc">NameValueCollection</param>
        /// <returns>bool</returns>
        public static bool UpdateCookie(string CookieName, NameValueCollection Nvc)
        {
            bool ReturnBoolValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName))
            {
                HttpCookie httpCookie = new HttpCookie(CookieName);
                NameValueCollection NonceNvc = GetCookie(CookieName);
                if (NonceNvc != null)
                {
                    string CookieValue = string.Empty;
                    for (int i = 0; i < NonceNvc.Count; i++)
                    {
                        CookieValue = NonceNvc.Get(i);
                        for (int y = 0; y < Nvc.Count; y++)
                        {
                            if (NonceNvc.GetKey(i) == Nvc.GetKey(y))
                            {
                                if (CookieValue != Nvc.Get(y))
                                {
                                    CookieValue = Nvc.Get(y);
                                }
                                break;
                            }
                        }
                        httpCookie.Values.Add(NonceNvc.GetKey(i), CookieValue);
                        CookieValue = string.Empty;
                    }
                    HttpContext.Current.Response.AppendCookie(httpCookie);
                    ReturnBoolValue = true;
                }
            }
            return ReturnBoolValue;
        }


        #endregion

        #region get cookie
        /// <summary>
        /// get cookie 
        /// </summary>
        /// <param name="CookieName">cookie name</param>
        /// <returns>NameValueCollection</returns>
        public static NameValueCollection GetCookie(string CookieName)
        {
            NameValueCollection Nvc = new NameValueCollection();
            if (!string.IsNullOrEmpty(CookieName))
            {
                try
                {
                    var dd = HttpContext.Current.Request.Cookies[CookieName];
                    if (dd != null)
                    {
                        Nvc = HttpContext.Current.Request.Cookies[CookieName].Values;
                    }
                }
                catch
                {
                    //Nvc=null;
                }
            }
            return Nvc;
        }
        /// <summary>
        ///读取一个COOKIE
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static string GetCookieString(string CookieName)
        {
            string result = "";
            if (!string.IsNullOrEmpty(CookieName))
            {
                if (HttpContext.Current.Request.Cookies[CookieName] != null)
                {
                    result = HttpContext.Current.Request.Cookies[CookieName].Value;
                }
            }
            return result;
        }
        /// <summary>
        /// 写入一个COOKIE
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static void SetCookieString(string CookieName, string value, int minute)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(CookieName, value));
            HttpContext.Current.Response.Cookies[CookieName].Expires = DateTime.Now.AddMinutes(minute);
        }

        #endregion

        #region delete cookie
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static bool DeleteCookie(string CookieName)
        {
            bool ReturnBoolValue = false;
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookieName];

            if (httpCookie != null)
            {
                httpCookie = new HttpCookie(CookieName);
                httpCookie.Expires = DateTime.Now.AddDays(-30);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
                ReturnBoolValue = true;
            }
            return ReturnBoolValue;
        }

        #endregion
    }
}
