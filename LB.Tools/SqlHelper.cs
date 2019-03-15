using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LB.Tools
{
    public sealed class SqlHelper
    {
        public SqlHelper()
        {

        }
        public SqlHelper(string conString)
        {
            ConString = conString;

        }
        private string conString;
        public string ConString
        {
            get { return conString; }
            set
            {
                conString = value;

            }
        }

        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(ConString);
            return con;
        }

        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }

                    if ((p.Direction == ParameterDirection.Input) && (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    command.Parameters.Add(p);
                }
            }
        }

        public DataSet ExecuteDataSet(string sql)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                con.Close();
                return ds;
            }
        }

        public DataSet ExecuteDataSet(string sql, SqlParameter[] paras)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                AttachParameters(cmd, paras);
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                con.Close();
                return ds;
            }
        }
        public DataSet ExecuteDataSetProc(string proc, SqlParameter[] paras)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                AttachParameters(cmd, paras);
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                con.Close();
                return ds;
            }
        }

        public DataTable ExecuteDataTable(string sql)
        {
            DataSet ds = ExecuteDataSet(sql);
            if (ds != null && ds.Tables.Count == 1)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public DataTable ExecuteDataTable(string sql, SqlParameter[] paras)
        {
            DataSet ds = ExecuteDataSet(sql, paras);
            if (ds != null && ds.Tables.Count == 1)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable ExecuteDataTableProc(string procName, SqlParameter[] paras)
        {
            DataSet ds = ExecuteDataSetProc(procName, paras);
            if (ds != null && ds.Tables.Count == 1)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 300;
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }
        }
        public int ExecuteNonQuery(string sql, SqlParameter[] paras)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                AttachParameters(cmd, paras);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }
        }

        public int ExecuteSqlNonQuery(string sql, SqlParameter[] paras)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                AttachParameters(cmd, paras);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }
        }


        public object ExecuteScalar(string sql)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                object result = cmd.ExecuteScalar();
                con.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql, SqlParameter[] paras)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                AttachParameters(cmd, paras);
                object result = cmd.ExecuteScalar();
                con.Close();
                return result;
            }
        }

        public string ExecuteScalarString(string sql, string nullReplace)
        {
            object result = ExecuteScalar(sql);
            return (result == null || result == DBNull.Value) ? nullReplace : result.ToString();
        }

        public string ExecuteScalarString(string sql, SqlParameter[] paras, string nullReplace)
        {
            object result = ExecuteScalar(sql, paras);
            return (result == null || result == DBNull.Value) ? nullReplace : result.ToString();
        }

        public int ExecuteScalarInt(string sql, int nullReplace)
        {
            object result = ExecuteScalar(sql);
            return (result == null || result == DBNull.Value) ? nullReplace : Convert.ToInt32(result);
        }


        public long ExecuteScalarLong(string sql, long nullReplace)
        {
            object result = ExecuteScalar(sql);
            return (result == null || result == DBNull.Value) ? nullReplace : Convert.ToInt64(result);
        }
        public long ExecuteScalarLong(string sql, SqlParameter[] paras, long nullReplace)
        {
            object result = ExecuteScalar(sql, paras);
            return (result == null || result == DBNull.Value) ? nullReplace : Convert.ToInt64(result);
        }

        public int ExecuteScalarInt(string sql, SqlParameter[] paras, int nullReplace)
        {
            object result = ExecuteScalar(sql, paras);
            return (result == null || result == DBNull.Value) ? nullReplace : Convert.ToInt32(result);
        }


    }

}