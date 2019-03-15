using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Shop.Model;
using Shop.SQLDataAccess;
using Shop.DataAccess;
namespace Shop.Bussiness
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBase
    {
        public static string DBType
        {
            get
            {
                return Shop.DataAccess.BaseUtils.BaseUtilsInstance.DBType;
            }
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteSql(string sql)
        {
            if (BaseUtils.BaseUtilsInstance.DBType == "access")
                return AccessUtils.Instance.TextExecuteNonQuery(sql, null);
            else
                return SqlUtils.SqlUtilsInstance.TextExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取一个值,返回字符串
        /// </summary>
        public static string GetValue(string sql)
        {
            string val = "";
            try
            {
                if (BaseUtils.BaseUtilsInstance.DBType == "access")
                    val = Convert.ToString(AccessUtils.Instance.TextExecuteScalar(sql, null));
                else
                    val = Convert.ToString(SqlUtils.SqlUtilsInstance.TextExecuteScalar(sql));
            }
            catch
            {
                val = "";
            }
            return val;
        }

    }
}

