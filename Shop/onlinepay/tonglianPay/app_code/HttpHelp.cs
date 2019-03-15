using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace tonglianPay
{
    /// <summary>
    /// Http请求
    /// </summary>
    public static class HttpHelp
    {
        /// <summary>
        /// Get提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <returns></returns>
        public static string GetMode(string getUrl, Encoding encoding = null)
        {
            return GetMode(getUrl, "", null, null, encoding);
        }

        /// <summary>
        /// Get提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public static string GetMode(string getUrl, string getData, Encoding encoding = null)
        {
            return GetMode(getUrl, getData, null, null, encoding);
        }

        /// <summary>
        /// Get提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="postData">参数</param>
        /// <param name="headDict">headDict</param>
        /// <returns></returns>
        public static string GetMode(string getUrl, string getData, Dictionary<string, string> headDict, Encoding encoding = null)
        {
            return GetMode(getUrl, getData, null, headDict, encoding);
        }

        /// <summary>
        /// Get提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="postData">参数</param>
        /// <param name="cookie">cookie</param>
        /// <param name="headDict">headDict</param>
        /// <returns></returns>
        public static string GetMode(string getUrl, string getData, Cookie cookie, Dictionary<string, string> headDict,Encoding encoding=null)
        {
            HttpWebRequest request = null;
            try
            {
                if (!string.IsNullOrEmpty(getData))
                {
                    getUrl = getUrl + "?" + getData;
                }

                request = (HttpWebRequest)WebRequest.Create(getUrl);

                CookieContainer cookieContainer = new CookieContainer();
                if (cookie != null)
                {
                    cookieContainer.Add(cookie);
                }
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;

                if (headDict != null && headDict.Count > 0)
                {
                    foreach (var key in headDict.Keys)
                    {
                        request.Headers.Add(key, headDict[key]);
                    }
                }

                using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
                {
                    if (encoding == null)
                    {
                        encoding = Encoding.GetEncoding("utf-8");
                    }
                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), encoding))
                    {
                        string line = sr.ReadToEnd().Trim();

                        return line;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (request != null)
                    request.Abort();
            }
        }

        /// <summary>
        /// POST提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <returns></returns>
        public static string PostMode(string postUrl)
        {
            return PostMode(postUrl, "", null,null,false);
        }

        /// <summary>
        /// POST提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public static string PostMode(string postUrl, string postData, bool isXml = false)
        {
            return PostMode(postUrl, postData, null, null, isXml);
        }

        /// <summary>
        /// Get提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="postData">参数</param>
        /// <param name="headDict">headDict</param>
        /// <returns></returns>
        public static string PostMode(string getUrl, string getData, Dictionary<string, string> headDict)
        {
            return PostMode(getUrl, getData, null, headDict,false);
        }

        /// <summary>
        /// POST提交-UTF-8编码
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="postData">参数</param>
        /// <param name="cookie">cookie</param>
        /// <returns></returns>
        public static string PostMode(string postUrl, string postData, Cookie cookie, 
            Dictionary<string, string> headDict,bool isXml)
        {
            HttpWebRequest request = null;
            // 准备请求...
            try
            {
                Stream outstream = null;
                Stream instream = null;
                StreamReader sr = null;
                HttpWebResponse response = null;

                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] data = encoding.GetBytes(postData);
                // 设置参数
                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
                request = WebRequest.Create(postUrl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                if (cookie != null)
                {
                    cookieContainer.Add(cookie);
                }
                request.CookieContainer = cookieContainer;
                
                if (headDict != null && headDict.Count > 0)
                {
                    foreach (var key in headDict.Keys)
                    {
                        request.Headers.Add(key, headDict[key]);
                    }
                }

                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = isXml ? "text/xml" : "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        private static bool RemoteCertificateValidate(object sender, 
            X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            //为了通过证书验证，总是返回true
            return true;
        }
    }
}