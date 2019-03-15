using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using System.Text;

namespace weixinpayh5
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
        private string key;
        // 参与签名的参数列表
        private static string SignField = "appid,appkey,timestamp,openid,noncestr,issubscribe";
        // 微信服务器编码方式
        private string charset = "gb2312";

        //参与签名的参数列表
        protected HttpContext httpContext;
        //protected Hashtable parameters;
        private Hashtable xmlMap;

        //获取页面提交的get和post参数
        public ResponseHandler(HttpContext httpContext)
        {
            //parameters = new Hashtable();
            xmlMap = new Hashtable();

            this.httpContext = httpContext;
            //NameValueCollection collection;
            //post data
            //if (this.httpContext.Request.HttpMethod == "POST")
            //{
            //    collection = this.httpContext.Request.Form;
            //    foreach (string k in collection)
            //    {
            //        string v = collection[k];
            //        setParameter(k, v);
            //    }
            //}
            //query string
            //collection = this.httpContext.Request.QueryString;
            //foreach (string k in collection)
            //{
            //    string v = collection[k];
            //    setParameter(k, v);
            //}
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

        #region 参数=======================================
        /// <summary>
        /// 初始化加载
        /// </summary>
        public virtual void init()
        {
        }

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string getKey()
        {
            return key;
        }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key"></param>
        public void setKey(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string getParameter(string parameter)
        {
            var s = (string)xmlMap[parameter];
            return (null == s) ? "" : s;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        public void setParameter(string parameter, string parameterValue)
        {
            //if (parameter != null && parameter != "")
            //{
            //    if (parameters.Contains(parameter))
            //    {
            //        parameters.Remove(parameter);
            //    }

            //    parameters.Add(parameter, parameterValue);
            //}
        }
        #endregion

        #region 辅助方法===================================
        /// <summary>
        /// 判断微信签名
        /// </summary>
        /// <returns></returns>
        public virtual bool isWXsign(out string error)
        {
            StringBuilder sb = new StringBuilder();
            Hashtable signMap = new Hashtable();
            foreach (string k in xmlMap.Keys)
            {
                if (k != "sign")
                {
                    signMap.Add(k.ToLower(), xmlMap[k]);
                }
            }

            ArrayList akeys = new ArrayList(signMap.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)signMap[k];
                sb.Append(k + "=" + v + "&");
            }
            sb.Append("key=" + this.key);

            string sign = MD5Util.GetMD5(sb.ToString(), charset).ToString().ToUpper();
            error = "sign = " + sign + "\r\n xmlMap[sign]=" + xmlMap["sign"].ToString();
            return sign.Equals(xmlMap["sign"]);

        }

        /// <summary>
        /// 判断微信维权签名
        /// </summary>
        /// <returns></returns>
        public virtual bool isWXsignfeedback()
        {
            return true;
            //StringBuilder sb = new StringBuilder();
            //Hashtable signMap = new Hashtable();

            //foreach (string k in xmlMap.Keys)
            //{
            //    if (SignField.IndexOf(k.ToLower()) != -1)
            //    {
            //        signMap.Add(k.ToLower(), xmlMap[k]);
            //    }
            //}
            //signMap.Add("appkey", this.appkey);


            //ArrayList akeys = new ArrayList(signMap.Keys);
            //akeys.Sort();

            //foreach (string k in akeys)
            //{
            //    string v = (string)signMap[k];
            //    if (sb.Length == 0)
            //    {
            //        sb.Append(k + "=" + v);
            //    }
            //    else
            //    {
            //        sb.Append("&" + k + "=" + v);
            //    }
            //}

            //string sign = SHA1Util.getSha1(sb.ToString()).ToString().ToLower();

            //this.setDebugInfo(sb.ToString() + " => SHA1 sign:" + sign);

            //return sign.Equals(xmlMap["AppSignature"]);

        }

        /// <summary>
        /// 获取编码方式
        /// </summary>
        /// <returns></returns>
        protected virtual string getCharset()
        {
            return httpContext.Request.ContentEncoding.BodyName;
        }
        #endregion
    }
}