using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using LB.Tools;
using System.Data.OleDb;

namespace LB.DataAccess
{
    public class AccessUtils : BaseUtils
    {
        #region 构造函数
        public AccessUtils()
        {
            conn = new OleDbConnection(ConnectionString);
            conn.Open();
        }
        #endregion
        #region Static Instance
        private static AccessUtils _Instance;
        public static AccessUtils Instance
        {
            get
            {
                if (_Instance == null)
                {

                    _Instance = new AccessUtils();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        #endregion
        protected OleDbConnection conn;
        //protected OleDbCommand comm = new OleDbCommand();


        /// <summary>
        /// 打开数据库
        /// </summary>
        //private void openConnection()
        //{
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.ConnectionString = ConnectionString;
        //        // comm.Connection = conn;
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch (Exception e)
        //        { throw new Exception(e.Message); }

        //    }

        //}
        /// <summary>
        /// 关闭数据库
        /// </summary>
        //private void closeConnection()
        //{
        //    if (conn.State == ConnectionState.Open)
        //    {
        //        conn.Close();
        //        conn.Dispose();
        //        //comm.Dispose();
        //    }
        //}
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlstr"></param>
        public int TextExecuteNonQuery(string sqlstr, OleDbParameter[] paras = null)
        {
            //OleDbCommand comm = new OleDbCommand();

            //openConnection();
            //OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand comm = new OleDbCommand(sqlstr, conn);
            //conn.Open();
            //comm.Connection = conn;
            //comm.CommandType = CommandType.Text;
            //comm.CommandText = sqlstr;
            if (paras != null)
            {
                foreach (OleDbParameter p in paras)
                {
                    comm.Parameters.Add(p);
                }
            }
            //try
            //{
            int res = comm.ExecuteNonQuery();
            //closeConnection();
            //conn.Close();
            //conn.Dispose();
            comm.Parameters.Clear();
            comm.Dispose();

            //}
            //catch
            //{ 
            //}

            return res;
        }
        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象，使用时请注意关闭这个对象。
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public OleDbDataReader DataReader(string sqlstr, OleDbParameter[] paras = null)
        {

            OleDbDataReader dr = null;
            //try
            //{
            // openConnection();
            //OleDbConnection conn = new OleDbConnection(ConnectionString);


            OleDbCommand comm = new OleDbCommand(sqlstr, conn);
            //conn.Open();
            //comm.CommandText = sqlstr;
            //comm.Connection = conn;
            //comm.CommandType = CommandType.Text;
            if (paras != null)
            {
                foreach (OleDbParameter p in paras)
                {
                    comm.Parameters.Add(p);
                }
            }
            dr = comm.ExecuteReader();
            //dr.Close();
            //conn.Close();
            comm.Parameters.Clear();
            comm.Dispose();
            //}
            //catch
            //{
            //}
            return dr;
        }
        //ExecuteScalar
        public object TextExecuteScalar(string sqlstr, OleDbParameter[] paras = null)
        {
            object dr = null;
            //OleDbCommand comm = new OleDbCommand();

            //openConnection();
            //OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand comm = new OleDbCommand(sqlstr, conn);
            //conn.Open();
            //comm.Connection = conn;
            //comm.CommandText = sqlstr;
            //comm.CommandType = CommandType.Text;
            if (paras != null)
            {
                foreach (OleDbParameter p in paras)
                {
                    comm.Parameters.Add(p);
                }
            }
            dr = comm.ExecuteScalar();
            comm.Parameters.Clear();
            comm.Dispose();
            //conn.Close();
            //conn.Dispose();


            return dr;
        }
        /// <summary>
        /// ACCESS高效分页
        /// </summary>
        public OleDbDataReader DataReader(string tablename, string strKey, string showString, string orderString, string whereString, int pageSize, int pageIndex, OleDbParameter[] paras = null)
        {
            OleDbDataReader dr = null;


            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = strKey + " asc ";


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + pageSize + " " + showString + " from " + tablename + "");
            if (whereString != "")
                strSql.Append(" where " + whereString + "");
            if (pageIndex > 1)
            {
                if (whereString != "")
                    strSql.Append(" and ");
                else
                    strSql.Append(" where ");
                strSql.Append(strKey + " not in (select top " + (pageSize * (pageIndex - 1)) + " " + strKey + " from " + tablename + "");
                if (whereString != "")
                    strSql.Append(" where " + whereString + "");
                if (orderString != "")
                    strSql.Append(" order by " + orderString + "," + strKey);
                else
                    strSql.Append(" order by " + strKey);
                strSql.Append(")");
            }
            if (orderString != "")
                strSql.Append(" order by " + orderString + "," + strKey);
            else
                strSql.Append(" order by " + strKey);

            //conn1.ConnectionString = ConnectionString;
            //if (conn1.State == ConnectionState.Closed)
            //    conn1.Open();
            //openConnection();
            //OleDbConnection conn = new OleDbConnection(ConnectionString);

            OleDbCommand comm = new OleDbCommand(strSql.ToString(), conn);
            //conn.Open();

            if (paras != null)
            {
                foreach (OleDbParameter p in paras)
                {
                    comm.Parameters.Add(p);
                }
            }
            try
            {
                dr = comm.ExecuteReader();
                comm.Parameters.Clear();
                comm.Dispose();

            }
            catch (Exception ex)
            {
                //throw ex.Message;
            }
            //if (conn1.State == ConnectionState.Open)
            //      conn1.Close();
            //  dr.Close();

            return dr;

        }

        public DataSet TextExecuteDataset(string sql, OleDbParameter[] paras = null)
        {
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            if (paras != null)
            {
                foreach (OleDbParameter p in paras)
                {
                    cmd.Parameters.Add(p);
                }
            }
            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                try
                {
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return ds;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
