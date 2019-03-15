using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using LB.Tools;
using Shop.Model;
using Shop.Bussiness;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using DB.LebiShop;
namespace Shop.Platform
{
    public class LoginBase : ShopPage
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
        public string Get(string WebUrl)
        {
            string content = "";
            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;
            StringWriter sw = new StringWriter();
            string url = WebUrl;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] b = wc.DownloadData(url);
            content = System.Text.Encoding.GetEncoding("utf-8").GetString(b);
            return content;

        }
        public string Post(string WebUrl, System.Collections.Specialized.NameValueCollection nv)
        {
            string content = "";
            if (nv == null)
                nv = new System.Collections.Specialized.NameValueCollection();
            try
            {
                //调用Main_Execute,并且获取其输出 
                StringWriter sw = new StringWriter();
                string url = WebUrl;
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] b = wc.UploadValues(WebUrl, "POST", nv);
                content = System.Text.Encoding.GetEncoding("utf-8").GetString(b);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return content;

        }
        public string Post(string WebUrl, string json)
        {
            string content = "";

            try
            {
                //调用Main_Execute,并且获取其输出 
                StringWriter sw = new StringWriter();
                string url = WebUrl;
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                content = wc.UploadString(WebUrl, "POST", json);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return content;

        }
        public string Post(string WebUrl)
        {
            return Post(WebUrl, new System.Collections.Specialized.NameValueCollection());
        }


        public string ENBackuri(string url)
        {
            url = url.Replace(".", "__a__");
            url = url.Replace("/", "__b__");
            return url;
        }
        public string DEBackuri(string url)
        {
            url = url.Replace("__a__", ".");
            url = url.Replace("__b__", "/");
            return url;
        }
    }

}
