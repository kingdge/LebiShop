using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Globalization;
using System.Web.Script.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
using LB.DataAccess;
using System.Data.OleDb;
namespace Shop.Admin.Ajax
{
    public partial class ajax_plugin : AdminAjaxBase //System.Web.UI.Page
    {
        protected DataTable tables;
        protected SqlConnection conn;
        protected MySqlConnection mysqlconn;
        protected OleDbConnection acconn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);
        }
        /// <summary>
        /// 更新插件菜单
        /// </summary>
        public void Plugin_updatemenu()
        {
            if (!EX_Admin.Power("pluginconfig_edit", "插件设置"))
            {
                AjaxNoPower();
                return;
            }
            string name = RequestTool.RequestString("id");
            List<PluginConfig> models = Event.GetPluginConfig();
            foreach (PluginConfig model in models)
            {
                if (model.Assembly == name)
                {
                    foreach (PluginConfig.menuconfig m in model.MenuConfigs)
                    {
                        Lebi_Menu menu0 = updatemenu(model.Assembly, m, null);
                        foreach (PluginConfig.menuconfig m1 in m.son)
                        {
                            Lebi_Menu menu1 = updatemenu(model.Assembly, m1, menu0);
                            foreach (PluginConfig.menuconfig m2 in m1.son)
                            {
                                updatemenu(model.Assembly, m2, menu1);
                            }
                        }
                    }
                    foreach (PluginConfig.menurewrite m in model.MenuRewrites)
                    {
                        Lebi_Menu menu = B_Lebi_Menu.GetModel("Code='" + m.code + "'");
                        if (menu != null)
                        {
                            if (menu.URL != m.url)
                                menu.URL = m.url;
                            B_Lebi_Menu.Update(menu);
                        }
                    }
                }
            }

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 更新插件菜单
        /// </summary>
        /// <param name="c"></param>
        /// <param name="m"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Lebi_Menu updatemenu(string c, PluginConfig.menuconfig m, Lebi_Menu parent)
        {
            string code = c + "_" + m.name;

            Lebi_Menu menu = B_Lebi_Menu.GetModel("Code=lbsql{'" + code + "'}");
            if (parent == null && m.parentcode != "")
            {
                parent = B_Lebi_Menu.GetModel("Code=lbsql{'" + m.parentcode + "'}");
            }
            if (parent == null)
                parent = new Lebi_Menu();
            if (menu == null)
            {
                menu = new Lebi_Menu();
                menu.Code = code;
                menu.Isshow = 1;
                menu.Name = m.name;
                menu.parentCode = parent.Code;
                menu.parentid = parent.id;
                if (m.url == null)
                    menu.URL = "";
                else
                    menu.URL = m.url.TrimStart('/');
                B_Lebi_Menu.Add(menu);
                menu.id = B_Lebi_Menu.GetMaxId();
            }
            return menu;
        }
        /// <summary>
        /// 更新插件状态
        /// </summary>
        public void Plugin_updatestatus()
        {
            if (!EX_Admin.Power("pluginconfig_edit", "插件设置"))
            {
                AjaxNoPower();
                return;
            }
            string name = RequestTool.RequestString("id");
            string status = "";
            string[] arr = SYS.PluginUsed.Split(',');
            bool ishave = false;
            foreach (string aname in arr)
            {
                if (aname == name)
                {
                    ishave = true;
                    continue;
                }
                if (aname != "")
                    status += "," + aname;
            }
            if (!ishave)
            {
                //启用一个插件
                status += "," + name;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.PluginUsed = status;
            dob.SaveConfig(model);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 刷新插件页面
        /// </summary>
        public void Plugin_createpage()
        {
            if (!EX_Admin.Power("pluginconfig_edit", "插件设置"))
            {
                AjaxNoPower();
                return;
            }
            string name = RequestTool.RequestString("id");

            //生成插件页面
            List<PluginConfig> plgs = Event.GetPluginConfig();
            foreach (PluginConfig plg in plgs)
            {
                Shop.Bussiness.SystemTheme.CreateALLPluginPage(plg);
                Shop.Bussiness.Theme.CreateALLPluginPage();
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 刷新插件页面
        /// </summary>
        public void Plugin_updatepage()
        {
            //生成插件页面
            List<PluginConfig> plgs = Event.GetPluginConfig();
            foreach (PluginConfig plg in plgs)
            {
                Shop.Bussiness.SystemTheme.CreateALLPluginPage(plg);//后端页面
                Shop.Bussiness.Theme.CreateALLPluginPage();//前端页面
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 更新插件数据表
        /// </summary>
        public void Plugin_updatetable()
        {
            if (!EX_Admin.Power("pluginconfig_edit", "插件设置"))
            {
                AjaxNoPower();
                return;
            }
            string name = RequestTool.RequestString("id");
            List<PluginConfig> models = Event.GetPluginConfig();

            string connectionString = ConfigurationManager.ConnectionStrings["constr"].ToString();
            //conn = new SqlConnection(connectionString);
            //conn.Open();
            //tables = conn.GetSchema(SqlClientMetaDataCollectionNames.Tables, new string[] { null, null, null, "BASE TABLE" });

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

            foreach (PluginConfig model in models)
            {
                if (model.Assembly == name)
                {
                    foreach (PluginConfig.tableconfig m in model.TableConfigs)
                    {
                        string[] cols = m.cols.Replace("\r", "").Split('\n');
                        if (!IsHaveTable(m.name))
                        {
                            //添加表
                            string sql = "CREATE TABLE " + m.name + " (";
                            foreach (string col in cols)
                            {
                                string temp = col.Trim();
                                if (temp == "")
                                    continue;
                                int index = temp.IndexOf(" ");
                                string cname = temp.Substring(0, index);
                                string ctype = temp.Substring(index, temp.Length - index).Trim();
                                string cmethod = "[" + cname + "] " + ctype;
                                if (cname == m.primarykey)
                                {

                                    if (!cmethod.ToUpper().Contains("PRIMARY KEY"))
                                        cmethod += " PRIMARY KEY";
                                }
                                sql += cmethod + ",";
                            }
                            sql = sql.TrimEnd(',') + ")";
                            UpdateTable(sql);
                        }
                        else
                        {
                            DataTable dt = GetCols(m.name);
                            foreach (string col in cols)
                            {
                                string temp = col.Trim();
                                if (temp == "")
                                    continue;
                                int index = temp.IndexOf(" ");
                                string cname = temp.Substring(0, index);
                                string ctype = temp.Substring(index, temp.Length - index).Trim();
                                string sql = "";
                                if (!IsHaveCol(cname, ctype, dt))
                                {
                                    //添加字段
                                    sql = "ALTER TABLE " + m.name + " ADD [" + cname + "] " + ctype;
                                }
                                else
                                {
                                    //更新字段
                                    sql = "ALTER TABLE " + m.name + " ALTER COLUMN [" + cname + "] " + ctype;
                                }
                                UpdateTable(sql);
                            }

                        }
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void UpdateTable(string sql)
        {
            try
            {
                if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "mysql")
                {
                    sql = sql.Replace("nvarchar", "varchar");
                    sql = sql.Replace("ntext", "text");
                    sql = sql.Replace("numeric", "decimal");
                    sql = sql.Replace("IDENTITY(1,1)", "AUTO_INCREMENT");
                    sql = sql.Replace("IDENTITY(1, 1) ", "AUTO_INCREMENT");
                    
                    sql = sql.Replace("[", "`");
                    sql = sql.Replace("]", "`");
                    sql = sql.Replace("int ", "int(10) ");
                }

                Common.ExecuteSql(sql);
            }
            catch( Exception  ex) {
                SystemLog.Add(ex.ToString()+"\r\n\r\n"+sql);
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
                    return true;
                }
            }
            return false;
        }
    }
}