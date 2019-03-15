using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;

namespace Shop.Bussiness
{
    /// <summary>
    /// 数据访问类。
    /// </summary>
    public class Common
    {
        //#region Static Instance
        //private static Common _Instance;
        //public static Common Instance
        //{
        //    get
        //    {
        //        if (_Instance == null)
        //        {

        //            _Instance = new Common();
        //        }
        //        return _Instance;
        //    }
        //    set
        //    {
        //        _Instance = value;
        //    }
        //}
        //#endregion
        public Common()
        { }
        #region  成员方法


        public static int ExecuteSql(string sql)
        {
            return LB.DataAccess.DB.Instance.TextExecuteNonQuery(sql);
        }
        public static DataSet GetDataSet(string sql)
        {
            return LB.DataAccess.DB.Instance.TextExecuteDataset(sql);
        }
        /// <summary>
        /// 获取一个值,返回字符串
        /// </summary>
        public static string GetValue(string sql)
        {
            string val = "";
            try
            {
                val = Convert.ToString(LB.DataAccess.DB.Instance.TextExecute(sql));
            }
            catch
            {
                val = "";
            }
            return val;
        }

        public static string FormatTime(DateTime intime)
        {
            return intime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string FormatDate(DateTime intime)
        {
            return intime.ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// 序列化键值对
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string KeyValueToJson(List<KeyValue> list)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            json = jss.Serialize(list);
            return json;
        }
        /// <summary>
        /// 反序列化键值对
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static List<KeyValue> KeyValueToList(string con)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<KeyValue> list = jss.Deserialize<List<KeyValue>>(con);
                return list;
            }
            catch
            {
                return new List<KeyValue>();
            }
        }
        #endregion  成员方法

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length"></param>
        /// <param name="useNum"></param>
        /// <param name="useLow"></param>
        /// <param name="useUpp"></param>
        /// <param name="useSpe"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        public static string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }
        #region 访问码加密/解密
        static string[] keys = { "qxa", "wcs", "evd", "rbf", "tng", "ymh", "uj", "ik", "ol", "pz" };
        public static string CreateViewCode(int num = 0)
        {
            //1444744053
            //  31536000
            int tamp = GetGetTimeStamp(System.DateTime.Now) - 1444744053;
            num = num < 1 ? 1 : num;
            int i = num % 10;
            if (i == 0)
                i = 2;
            tamp = tamp * i;
            string str = tamp.ToString();
            str = str.Replace("0", keys[0]);
            str = str.Replace("1", keys[1]);
            str = str.Replace("2", keys[2]);
            str = str.Replace("3", keys[3]);
            str = str.Replace("4", keys[4]);
            str = str.Replace("5", keys[5]);
            str = str.Replace("6", keys[6]);
            str = str.Replace("7", keys[7]);
            str = str.Replace("8", keys[8]);
            str = str.Replace("9", keys[9]);
            return Common.GetRnd(5, false, true, false, false, "") + str;
        }
        public static string GetViewCode(string str, int num = 0)
        {
            try
            {
                str = str.Substring(5, str.Length - 5);
                num = num < 1 ? 1 : num;
                int i = num % 10;
                if (i == 0)
                    i = 2;
                str = str.Replace(keys[0], "0");
                str = str.Replace(keys[1], "1");
                str = str.Replace(keys[2], "2");
                str = str.Replace(keys[3], "3");
                str = str.Replace(keys[4], "4");
                str = str.Replace(keys[5], "5");
                str = str.Replace(keys[6], "6");
                str = str.Replace(keys[7], "7");
                str = str.Replace(keys[8], "8");
                str = str.Replace(keys[9], "9");
                int tamp = 0;
                int.TryParse(str, out tamp);
                tamp = tamp / i + 1444744053;
                return tamp.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static int GetGetTimeStamp(System.DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (int)(time - startTime).TotalSeconds;
            return intResult;
        }

        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion

        public static KeyValue FindKeyValue(string data, string key)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                List<KeyValue> list = jss.Deserialize<List<KeyValue>>(data);
                KeyValue m = list.Find(p => p.K == key);
                return m;
            }
            catch
            {
                return null;
            }
        }
    }
}

