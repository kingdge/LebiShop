using System;
using System.Data;
using MySql.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using LB.Tools;
using System.Collections.Generic;
namespace LB.DataAccess
{
    /// <summary>
    ///SQLHelper 的摘要说明
    /// </summary>
    public class MySQLUtils : BaseUtils, IDB
    {
        #region 构造函数

        protected string ConnString = "";
        public MySQLUtils()
        {
            ConnString = ConnectionString;


        }
        public MySQLUtils(string connstring)
        {
            ConnString = connstring;

        }

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
                    strSql.Append("insert into `Lebi_Memcache`(");
                    strSql.Append("CacheKey,TableName,Keyid,Para,Time_Add,Time_End,seconds)");
                    strSql.Append(" values (");
                    strSql.Append("@CacheKey,@TableName,@Keyid,@Para,@Time_Add,@Time_End,@seconds)");
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
            catch (Exception ex)
            {
                TxtLog.Add(ex.Message);
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
                    strSql.Append("insert into `Lebi_Memcache`(");
                    strSql.Append("CacheKey,TableName,Keyid,Para,Time_Add,Time_End,seconds)");
                    strSql.Append(" values (");
                    strSql.Append("@CacheKey,@TableName,@Keyid,@Para,@Time_Add,@Time_End,@seconds)");
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
            catch (Exception ex)
            {
                TxtLog.Add(ex.Message);
            }
        }

        public string GetWhere(string where)
        {
            where = where.Replace("[", "`");
            where = where.Replace("]", "`");
            where = where.Replace("GetDate()", "CURDATE()");
            where = where.Replace("datediff(d,", "datediff(");
            where = where.Replace("newid()", "rand()");
            if (where.IndexOf("top 1") > -1)
            {
                where = where.Replace("top 1", "");
                where += " limit 0,1";
            }
            return where;
        }

        public object GetMemchche(string Key)
        {
            return MemcacheInstance.Get(Key);
        }

        #endregion

        private MySqlParameter GetMySqlParameter(SqlParameter para)
        {
            if (para == null)
                return null;
            MySqlParameter p = new MySqlParameter();
            p.ParameterName = para.ParameterName;
            p.Value = para.Value;
            return p;
        }
        /// <summary>
        /// Command预处理
        /// </summary>
        /// <param name="conn">MySqlConnection对象</param>
        /// <param name="trans">MySqlTransaction对象，可为null</param>
        /// <param name="cmd">MySqlCommand对象</param>
        /// <param name="cmdType">CommandType，存储过程或命令行</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组，可为null</param>
        private void PrepareCommand(MySqlConnection conn, MySqlCommand cmd, string cmdText, SqlParameter[] paras)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = GetWhere(cmdText);
            if (paras != null)
            {
                foreach (SqlParameter parm in paras)
                    cmd.Parameters.AddWithValue(parm.ParameterName, parm.Value);
            }

        }
        /// <summary>
        ///  执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="paras">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public int TextExecuteNonQuery(string cmdText, params SqlParameter[] paras)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {
                PrepareCommand(conn, cmd, cmdText, paras);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;

            }
        }
        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="paras">执行命令所用参数的集合</param>
        public object TextExecute(string cmdText, params SqlParameter[] paras)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(ConnString))
            {

                PrepareCommand(connection, cmd, cmdText, paras);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public IDataReader TextExecuteReaderOne(string tablename, string showString, string whereString, SqlParameter[] paras = null)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + showString + " from `" + tablename + "`");
            if (whereString != "")
                strSql.Append(" where " + whereString + "");
            strSql.Append(" Limit 0,1");

            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                PrepareCommand(conn, cmd, strSql.ToString(), paras);

                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                cmd.Parameters.Clear();

                return dr;

            }
            catch
            {
                conn.Close();
                return null;
            }
        }
        /// <summary>
        /// 返回数据集的sql命令
        /// </summary>
        /// <param name="paras">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public IDataReader TextExecuteReader(string tablename, string strKey, string showString, string orderString, string whereString, int pageSize, int pageIndex, SqlParameter[] paras)
        {
            //MySqlConnection conn = GetConn();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + showString + " from `" + tablename + "`");
            if (whereString != "")
            {
                strSql.Append(" where " + whereString + "");
            }
            if (orderString != "")
            {
                if (orderString.ToLower().IndexOf("rand()") > -1 || orderString.ToLower().IndexOf("newid()") > -1)
                {
                    if (whereString == "")
                        strSql.Append(" where id in (" + GetRandomIDs(tablename, strKey, whereString, pageSize, paras) + ")");
                    else
                        strSql.Append(" and id in (" + GetRandomIDs(tablename, strKey, whereString, pageSize, paras) + ")");
                }
                else
                {
                    strSql.Append(" order by " + orderString);
                }
            }
            pageIndex = pageIndex - 1;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;
            pageIndex = pageIndex * pageSize;
            strSql.Append(" Limit " + pageIndex + "," + pageSize);
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                PrepareCommand(conn, cmd, strSql.ToString(), paras);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;

            }
            catch
            {
                conn.Close();
                return null;
            }
        }
        /// <summary>
        /// 返回数据集的sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public IDataReader TextExecuteReader(string cmdText, SqlParameter[] paras = null)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                PrepareCommand(conn, cmd, cmdText, paras);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch(Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public DataSet TextExecuteDataset(string cmdText, SqlParameter[] paras = null)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {

                PrepareCommand(conn, cmd, cmdText, paras);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                cmd.Parameters.Clear();
                return ds;
            }
        }
        /// <summary>
        /// 随机取得指定数量的ID
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="strKey"></param>
        /// <param name="whereString"></param>
        /// <param name="pageSize"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string GetRandomIDs(string tablename, string strKey, string whereString, int pageSize, SqlParameter[] paras)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + strKey + " from `" + tablename + "`");
            if (whereString != "")
                strSql.Append(" where " + whereString + "");
            strSql.Append(" order by " + strKey + " desc");
            strSql.Append(" limit 0," + (pageSize + 99));
            DataSet ds = TextExecuteDataset(strSql.ToString(), paras);
            List<string> ids = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ids.Add(ds.Tables[0].Rows[i][strKey].ToString());
            }
            ids.OrderBy(x => Guid.NewGuid());
            pageSize = pageSize > ids.Count ? ids.Count : pageSize;
            string res = "";
            for (int i = 0; i < pageSize; i++)
            {
                res += ids[i] + ",";
            }
            return res.TrimEnd(',');
        }


    }
}