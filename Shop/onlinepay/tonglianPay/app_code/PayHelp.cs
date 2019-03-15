using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace tonglianPay
{
    public static class PayHelp
    {
        /// <summary>
        /// 生成随机串，随机串包含字母或数字
        /// </summary>
        /// <returns></returns>
        internal static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string MakeSign(SortedDictionary<string, object> dict, string key = "")
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in dict)
            {
                if (pair.Key != "Sign" && pair.Value.ToString().Trim() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }

            if (string.IsNullOrEmpty(key))
            {
                buff = buff.Trim('&');
            }
            else
            {
                buff += "key=" + key;
            }

            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(buff));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }

            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 将Dictionary转成xml
        /// </summary>
        /// <returns></returns>
        internal static string ToXml(SortedDictionary<string, object> dicts)
        {
            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in dicts)
            {
                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }
    }
}