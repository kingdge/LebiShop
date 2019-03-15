using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using LB.DataAccess;
namespace Shop.Bussiness
{
    public class DBStruct
    {
        private DataTable tables;
        private SqlConnection conn;
        private OleDbConnection acconn;
        /// <summary>
        /// 更新插件数据表
        /// </summary>
        public DBStruct()
        {

            string connectionString = BaseUtils.BaseUtilsInstance.ConnectionString;
            if (BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                acconn = new OleDbConnection(connectionString);
                acconn.Open();
                tables = acconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            }
            else
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                tables = conn.GetSchema(SqlClientMetaDataCollectionNames.Tables, new string[] { null, null, null, "BASE TABLE" });
            }
            
        }
        public void UpdateTable(string sql)
        {
            try
            {
                Common.ExecuteSql(sql);
            }
            catch {
                string s = "";
            }
        }
        /// <summary>
        /// 获取一个表的字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetCols(string name)
        {
            DataTable dt = new DataTable();
            if (BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                dt = acconn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new string[] { null, null, name, null });
            }
            else
            {
                dt = conn.GetSchema(SqlClientMetaDataCollectionNames.Columns, new string[] { null, null, name, null });
            }
            return dt;
        }
        /// <summary>
        /// 检查一个表是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsHaveTable(string name)
        {
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                if (tables.Rows[i]["TABLE_NAME"].ToString() == name)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 检查一个表的字段是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsHaveCol(string name, string datatype, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["COLUMN_NAME"].ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }

    }

}

