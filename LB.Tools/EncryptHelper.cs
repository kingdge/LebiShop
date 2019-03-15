using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace LB.Tools
{
    public class EncryptHelper
    {
        public static string CreateUserPwd(string pwd)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToLower().Substring(8, 16);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="codeType">utf-8</param>
        /// <returns></returns>
        public static string MD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
