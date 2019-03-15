using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using DB.LebiShop;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using LB.DataAccess;
using MySql.Data.MySqlClient;
namespace Shop.Admin.storeConfig
{
    public partial class pluginconfig : AdminPageBase
    {
        protected List<PluginConfig> models;
        protected DataTable tables;
        protected SqlConnection conn;
        protected OleDbConnection acconn;
        protected MySqlConnection mysqlconn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("pluginconfig_edit", "插件设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }

            models = Event.GetPluginConfigALL();
            string connectionString = BaseUtils.BaseUtilsInstance.ConnectionString;


            if (BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                acconn = new OleDbConnection(connectionString);
                acconn.Open();
                tables = acconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            }
            else if (BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                mysqlconn = new MySqlConnection(connectionString);
                mysqlconn.Open();
                tables = mysqlconn.GetSchema(SqlClientMetaDataCollectionNames.Tables, new string[] { null, null, null, "BASE TABLE" });

            }
            else
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                tables = conn.GetSchema(SqlClientMetaDataCollectionNames.Tables, new string[] { null, null, null, "BASE TABLE" });
            }
            //DataTable dt = conn.GetSchema(SqlClientMetaDataCollectionNames.Columns, new string[] { null, null, "Lebi_User", null });
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    //for (int j = 0; j < dt.Columns.Count; j++)
            //    //    Response.Write(dt.Columns[j].ColumnName + ":" + dt.Rows[i][j].ToString());
            //    Response.Write(dt.Rows[i]["COLUMN_NAME"]);
            //    Response.Write(dt.Rows[i]["DATA_TYPE"]);

            //    Response.Write("<br>");
            //}
            //Response.End();
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
            else if (BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {

                dt = mysqlconn.GetSchema(SqlClientMetaDataCollectionNames.Columns, new string[] { null, null, name, null });

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
                    if (BaseUtils.BaseUtilsInstance.DBType == "mysql")
                    {
                        datatype = datatype.Replace("nvarchar", "varchar");
                        datatype = datatype.Replace("ntext", "text");
                    }
                    if (datatype.IndexOf(dt.Rows[i]["DATA_TYPE"].ToString()) == 0)
                        return true;
                }
            }
            return false;
        }
        public bool IsNew(string name)
        {
            foreach (PluginConfig model in models)
            {
                if (model.Assembly == name)
                {
                    foreach (PluginConfig.menuconfig m in model.MenuConfigs)
                    {
                        if (countmenu(model.Assembly + "_" + m.name) == 0)
                            return true;
                        foreach (PluginConfig.menuconfig m1 in m.son)
                        {
                            if (countmenu(model.Assembly + "_" + m1.name) == 0)
                                return true;
                            foreach (PluginConfig.menuconfig m2 in m1.son)
                            {
                                if (countmenu(model.Assembly + "_" + m2.name) == 0)
                                    return true;
                            }
                        }
                    }
                    foreach (PluginConfig.menurewrite m in model.MenuRewrites)
                    {
                        Lebi_Menu menu = B_Lebi_Menu.GetModel("Code='" + m.code + "'");
                        if (menu != null)
                        {
                            if (menu.URL != m.url)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsTableOK(string name)
        {
            foreach (PluginConfig model in models)
            {
                if (model.Assembly == name)
                {
                    foreach (PluginConfig.tableconfig m in model.TableConfigs)
                    {
                        if (!IsHaveTable(m.name))
                            return false;
                        DataTable dt = GetCols(m.name);
                        string[] cols = m.cols.Replace("\r", "").Split('\n');
                        foreach (string col in cols)
                        {
                            string temp = col.Trim();
                            if (temp == "")
                                continue;
                            int index = temp.IndexOf(" ");
                            string cname = temp.Substring(0, index);
                            string ctype = temp.Substring(index, temp.Length - index).Trim();
                            if (!IsHaveCol(cname, ctype, dt))
                            {
                                //Response.Write(cname + "__" + ctype + "+++++++++++++++");
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public int countmenu(string code)
        {
            int count = B_Lebi_Menu.Counts("Code='" + code + "'");
            return count;

        }
        public bool GetStatus(string name)
        {
            if (("," + SYS.PluginUsed + ",").Contains("," + name + ","))
                return true;
            return false;
        }
    }
}