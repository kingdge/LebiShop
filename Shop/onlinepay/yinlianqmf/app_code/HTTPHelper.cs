using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
namespace yinlianqmf
{
    public  class HTTPHelper
    {
        internal class AcceptAllCertificatePolicy : ICertificatePolicy
        {
            public AcceptAllCertificatePolicy()
            {
            }

            public bool CheckValidationResult(ServicePoint sPoint,
               X509Certificate cert, WebRequest wRequest, int certProb)
            {
                // Always accept  
                return true;
            }
        }

        public static string PostPage(string url, string postData)
        {
            HttpWebRequest request = null;
            Stream writer = null;
            try
            {
                ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
                byte[] data = Encoding.UTF8.GetBytes(postData);
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 30000;
                request.AllowAutoRedirect = false;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.LongLength;


                writer = request.GetRequestStream();
                writer.Write(data, 0, data.Length);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader res = new StreamReader(response.GetResponseStream());
                string restring = res.ReadToEnd();
                return restring;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (request != null) request = null;
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
            return string.Empty;
        }




    }
}
