using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using LB.Tools;
using BeIT.MemCached;
namespace LB.DataAccess
{
    public class BaseUtils
    {
        string connectionString;
        string dbtype;
        public MemcachedClient MemcacheInstance;
        public BaseUtils()
        {
            try
            {
                connectionString = GetConnString("constr");
            }
            catch
            {
                connectionString = "";

            }
            string dbtype_ = RequestTool.GetConfigKey("DBType").Trim();
            dbtype = dbtype_;
            if (string.IsNullOrEmpty(dbtype)) { 
                dbtype = "sqlserver";
            }
            if (dbtype == "access")
            {
                string p = System.Web.HttpContext.Current.Server.MapPath("~/");
                connectionString = connectionString.Replace("~/", p);
            }
            else if (dbtype == "mysql")
                dbtype = "mysql";
            else
                dbtype = "sqlserver";

            if (RequestTool.GetConfigKey("IsOpenMemcache") == "True")
                MemcacheInstance = MemcachedClient.GetInstance("Memcache");
            else
                MemcacheInstance = null;
        }
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
        public string DBType
        {
            get
            {
                return dbtype;
            }
            set
            {
                dbtype = value;
            }
        }
        private static BaseUtils _BaseUtilsInstance;
        public static BaseUtils BaseUtilsInstance
        {
            get
            {
                if (_BaseUtilsInstance == null)
                {
                    _BaseUtilsInstance = new BaseUtils();
                }
                return _BaseUtilsInstance;
            }
            set
            {
                _BaseUtilsInstance = value;
            }
        }

        public string ColName(string name)
        {
            switch (dbtype)
            {
                case "access":
                    if (name.IndexOf("[") != 0)
                        name = "[" + name + "]";
                    break;
                case "sqlserver":
                    if (name.IndexOf("[") != 0)
                        name = "[" + name + "]";
                    break;
                case "mysql":
                    if (name.IndexOf("`") != 0)
                        name = "`" + name + "`";
                    break;
            }
            return name;
        }
        public static string GetConnString(string node)
        {
            string c = "";
            try
            {
                c = ConfigurationManager.ConnectionStrings[node].ToString();
            }
            catch
            {
                try
                {
                    c = LB.Tools.ConfigManager.ReadConnectionStringByName(node);
                }
                catch
                {
                    c = "";
                }
            }
            return c;
        }

    }


    public interface IDB
    {
        object TextExecute(string cmdText, SqlParameter[] paras = null);

        int TextExecuteNonQuery(string cmdText, SqlParameter[] paras = null);
        IDataReader TextExecuteReader(string tablename, string strKey, string showString, string orderString, string whereString, int pageSize, int pageIndex, SqlParameter[] paras = null);
        IDataReader TextExecuteReader(string sql, SqlParameter[] paras = null);
        IDataReader TextExecuteReaderOne(string tablename, string showString, string whereString, SqlParameter[] paras = null);
        DataSet TextExecuteDataset(string sql, SqlParameter[] paras = null);
        void SetMemchche(string Key, object obj, string TableName, int Keyid, string Para, int seconds);
        void SetMemchche(string Key, object obj, string TableName, long Keyid, string Para, int seconds);
        object GetMemchche(string Key);
    }
}
