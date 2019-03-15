using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Xml;

namespace weixinpay
{
    /** 
    '============================================================================
    'api说明：
    'getKey()/setKey(),获取/设置密钥
    'getParameter()/setParameter(),获取/设置参数值
    'getAllParameters(),获取所有参数
    'isTenpaySign(),是否正确的签名,true:是 false:否
    'isWXsign(),是否正确的签名,true:是 false:否
    ' * isWXsignfeedback判断微信维权签名
    ' *getDebugInfo(),获取debug信息
    '============================================================================
    */

    public class ResponseHandler
    {
        // 密钥 

        // appkey
        private static string SignField = "appid,appkey,timestamp,openid,noncestr,issubscribe";
        private string appkey;

        //xmlMap

        private string charset = "gb2312";
        protected string content;
        private string debugInfo;

        //参与签名的参数列表

        protected HttpContext httpContext;
        private string key;
        protected Hashtable parameters;
        private Hashtable xmlMap;

        //初始化函数

        //获取页面提交的get和post参数
        public ResponseHandler(HttpContext httpContext)
        {
            parameters = new Hashtable();
            xmlMap = new Hashtable();

            this.httpContext = httpContext;
            NameValueCollection collection;
            //post data
            if (this.httpContext.Request.HttpMethod == "POST")
            {
                collection = this.httpContext.Request.Form;
                foreach (string k in collection)
                {
                    string v = collection[k];
                    setParameter(k, v);
                }
            }
            //query string
            collection = this.httpContext.Request.QueryString;
            foreach (string k in collection)
            {
                string v = collection[k];
                setParameter(k, v);
            }
            if (this.httpContext.Request.InputStream.Length > 0)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(this.httpContext.Request.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    xmlMap.Add(xnf.Name, xnf.InnerText);
                }
            }
        }

        public virtual void init()
        {
        }


        /** 获取密钥 */

        public string getKey()
        {
            return key;
        }

        /** 设置密钥 */

        public void setKey(string key, string appkey)
        {
            this.key = key;
            this.appkey = appkey;
        }

        /** 获取参数值 */

        public string getParameter(string parameter)
        {
            var s = (string) parameters[parameter];
            return (null == s) ? "" : s;
        }

        /** 设置参数值 */

        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        protected virtual string getCharset()
        {
            return httpContext.Request.ContentEncoding.BodyName;
        }
    }
}