using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using LB.Tools;
namespace LB.DataAccess
{
    public class SqlUtils : BaseUtils, IDB
    {
        #region 构造函数
        public SqlUtils()
        {

            databaseSchema = RequestTool.GetConfigKey("DBower");
            if (databaseSchema == "")
                databaseSchema = "dbo";
        }
        public SqlUtils(string conString)
        {
            ConnectionString = conString;
            databaseSchema = RequestTool.GetConfigKey("DBower");
            if (databaseSchema == "")
                databaseSchema = "dbo";
        }

        
        #endregion

        #region 数据库连接
        protected string databaseSchema = "dbo";


        #endregion
        #region memcache缓存

        public void SetMemchche(string Key, object obj, string TableName, long Keyid, string Para, int seconds)
        {
            try
            {
                DateTime now = System.DateTime.Now;
                TextExecuteNonQuery("delete from Lebi_Memcache where CacheKey='" + Key + "'");//删除已有记录
                MemcacheInstance.Set(Key, obj, now.AddSeconds(seconds));
                if (MemcacheInstance != null)
                {
                    if (Para.Length > 1000)
                        Para = Para.Substring(0, 1000);
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into [Lebi_Memcache](");
                    strSql.Append("CacheKey,TableName,Keyid,Para,Time_Add,Time_End,seconds)");
                    strSql.Append(" values (");
                    strSql.Append("@CacheKey,@TableName,@Keyid,@Para,@Time_Add,@Time_End,@seconds)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
                    new SqlParameter("@CacheKey", Key),
                    new SqlParameter("@TableName", TableName),
                    new SqlParameter("@Keyid", Keyid),
                    new SqlParameter("@Para", Para),
                    new SqlParameter("@Time_Add", now),
                    new SqlParameter("@Time_End", now.AddSeconds(seconds)),
                    new SqlParameter("@seconds", seconds)};
                    TextExecuteNonQuery(strSql.ToString(), parameters);
                }
            }
            catch
            {

            }

        }

        public void SetMemchche(string Key, object obj, string TableName, int Keyid, string Para, int seconds)
        {

            try
            {
                DateTime now = System.DateTime.Now;
                TextExecuteNonQuery("delete from Lebi_Memcache where CacheKey='" + Key + "'");//删除已有记录
                MemcacheInstance.Set(Key, obj, now.AddSeconds(seconds));
                if (MemcacheInstance != null)
                {
                    if (Para.Length > 1000)
                        Para = Para.Substring(0, 1000);
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into [Lebi_Memcache](");
                    strSql.Append("CacheKey,TableName,Keyid,Para,Time_Add,Time_End,seconds)");
                    strSql.Append(" values (");
                    strSql.Append("@CacheKey,@TableName,@Keyid,@Para,@Time_Add,@Time_End,@seconds)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
                    new SqlParameter("@CacheKey", Key),
                    new SqlParameter("@TableName", TableName),
                    new SqlParameter("@Keyid", Keyid),
                    new SqlParameter("@Para", Para),
                    new SqlParameter("@Time_Add", now),
                    new SqlParameter("@Time_End", now.AddSeconds(seconds)),
                    new SqlParameter("@seconds", seconds)};
                    TextExecuteNonQuery(strSql.ToString(), parameters);
                }
            }
            catch
            {

            }
        }
        public object GetMemchche(string Key)
        {
            return MemcacheInstance.Get(Key);
        }

        #endregion

        private SqlParameter[] GetSqlParameter(SqlParameter[] paras)
        {
            return paras;

        }
        #region SQL语句--数据库操作
        public object TextExecute(string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, commandText, GetSqlParameter(commandParameters));
        }

        public int TextExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, commandText, GetSqlParameter(commandParameters));
        }
        public DataSet TextExecuteDataset(string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, commandText, GetSqlParameter(commandParameters));
        }
        public IDataReader TextExecuteReaderOne(string tablename, string showString, string whereString, SqlParameter[] commandParameters = null)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1" + showString + " from [" + tablename + "]");
            if (whereString != "")
                strSql.Append(" where " + whereString + "");
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, strSql.ToString(), GetSqlParameter(commandParameters));
        }
        public IDataReader TextExecuteReader(string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, commandText, GetSqlParameter(commandParameters));
        }

        public IDataReader TextExecuteReader(string tablename, string strKey, string showString, string orderString, string whereString, int pageSize, int pageIndex, SqlParameter[] paras = null)
        {
            if (paras == null)
            {
                string commandText = "usp_CommonPagination";
                return SqlHelper.ExecuteReader(ConnectionString, string.Format("{0}.{1}", databaseSchema, commandText), tablename, strKey, showString, orderString, whereString, pageSize, pageIndex);

            }
            string strTableName = "[" + tablename + "]";
            string strFieldKey = "id";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + pageSize + " " + showString + " from " + strTableName + "");
            if (whereString != null)
                strSql.Append(" where " + whereString + "");
            if (pageIndex > 1)
            {
                if (whereString != "")
                    strSql.Append(" and ");
                else
                    strSql.Append(" where ");
                strSql.Append(strFieldKey + " not in (select top " + (pageSize * (pageIndex - 1)) + " " + strFieldKey + " from " + strTableName + "");

                if (whereString != "")
                    strSql.Append(" where " + whereString + "");
                if (orderString != "")
                    strSql.Append(" order by " + orderString + "");
                strSql.Append(")");
            }
            if (orderString != "")
                strSql.Append(" order by " + orderString + "");
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, strSql.ToString(), GetSqlParameter(paras));


        }
        #endregion

    }
}
