using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Bussiness
{
    public class CreatModel_DAL
    {
        #region 私有变量
        protected string _dalname = ""; //model类名
        protected string _dalspace = "CZ.DAL"; //顶级命名空间名

        protected string _tablename = "";//数据表名称
        protected string _modelspace = "";//实体层命名空间
        protected string _modelname = "";//实体层命名空间

        protected List<ColumnInfo> _columninfos; //字段属性
        protected string DbHelperName = "SqlUtils.SqlUtilsInstance";
        protected string AccessHelperName = "AccessUtils.Instance";

        /// <summary>
        /// 命名空间名 
        /// </summary>        
        public string DALSpace
        {
            set { _dalspace = value; }
            get { return _dalspace; }
        }
        /// <summary>
        /// 类名
        /// </summary>
        public string DALName
        {
            set { _dalname = value; }
            get { return _dalname; }
        }
        /// <summary>
        /// 实体层命名空间 
        /// </summary>        
        public string ModelSpace
        {
            set { _modelspace = value; }
            get { return _modelspace; }
        }
        /// <summary>
        /// model类名
        /// </summary>
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 字段属性
        /// </summary>
        public List<ColumnInfo> ColumnInfos
        {
            set { _columninfos = value; }
            get { return _columninfos; }
        }
        /// <summary>
        /// 主键标识字段
        /// </summary>
        public ColumnInfo Key
        {
            get
            {
                return CodeCommon.GetPKCOL(_columninfos);
            }
        }
        public bool IsHasIdentity
        {
            get
            {
                bool isid = false;
                if (_columninfos.Count > 0)
                {
                    foreach (ColumnInfo key in _columninfos)
                    {
                        if (key.IsIdentity)
                        {
                            isid = true;
                        }
                    }
                }
                return isid;
            }
        }
        #endregion

        #region 数据层(整个类)
        public void CreatDAL(string path)
        {
            string str = GetDALCodeStr();
            CodeCommon.CreatFile(path, DALName + ".cs", str);
        }
        /// <summary>
        /// 得到整个类的代码
        /// </summary>     
        public string GetDALCodeStr()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("using System;");
            strclass.AppendLine("using System.Data;");
            strclass.AppendLine("using System.Text;");
            strclass.AppendLine("using System.Collections.Generic;");
            strclass.AppendLine("using System.Data.SqlClient;");
            strclass.AppendLine("using System.Data.OleDb;");
            strclass.AppendLine("using System.Text.RegularExpressions;");
            strclass.AppendLine("using System.Web;");
            strclass.AppendLine("using LB.DataAccess;");
            strclass.AppendLine("using Shop.Model;using DB.LebiShop;");

            strclass.AppendLine("namespace " + DALSpace);

            strclass.AppendLine("{");
            strclass.AppendLine(CreatInterface());
            strclass.AppendSpaceLine(1, "/// <summary>");
            strclass.AppendSpaceLine(1, "/// 数据访问类" + DALName + "。");
            strclass.AppendSpaceLine(1, "/// </summary>");
            strclass.AppendSpace(1, "public class " + DALName);
            strclass.AppendLine("");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendLine(CreatInstance());

            strclass.AppendSpaceLine(2, "public " + DALName + "()");
            strclass.AppendSpaceLine(2, "{}");
            strclass.AppendSpaceLine(2, "#region  成员方法");

            #region  方法代码
            strclass.AppendSpaceLine(1, "class sqlserver_D_" + TableName + " : " + TableName + "_interface");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendLine(CreatGetValue());
            strclass.AppendLine(CreatCounts());
            strclass.AppendLine(CreatGetMaxID());
            strclass.AppendLine(CreatAdd());
            strclass.AppendLine(CreatUpdate());
            strclass.AppendLine(CreatDelete());
            strclass.AppendLine(CreatGetModel());
            strclass.AppendLine(CreatGetListArray());
            strclass.AppendLine(CreatGetListArray_ALL());
            strclass.AppendLine(CreatBindForm());
            strclass.AppendLine(CreatReaderBind());
            strclass.AppendSpaceLine(1, "}");

            strclass.AppendSpaceLine(1, "class access_D_" + TableName + " : " + TableName + "_interface");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendLine(CreatGetValue_access());
            strclass.AppendLine(CreatCounts_access());
            strclass.AppendLine(CreatGetMaxID_access());
            strclass.AppendLine(CreatAdd_access());
            strclass.AppendLine(CreatUpdate_access());
            strclass.AppendLine(CreatDelete_access());
            strclass.AppendLine(CreatGetModel_access());
            strclass.AppendLine(CreatGetListArray_access());
            strclass.AppendLine(CreatGetListArray_ALL_access());
            strclass.AppendLine(CreatBindForm());
            strclass.AppendLine(CreatReaderBind());
            strclass.AppendSpaceLine(1, "}");

            #endregion

            strclass.AppendSpaceLine(2, "#endregion  成员方法");
            strclass.AppendSpaceLine(1, "}");
            strclass.AppendLine("}");
            strclass.AppendLine("");

            return strclass.Value;
        }

        #endregion
        #region 生成接口
        public string CreatInterface()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("");
            strclass.AppendSpaceLine(1, "public interface " + TableName + "_interface");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendSpaceLine(2, "string GetValue(string colName, string strWhere);");
            strclass.AppendSpaceLine(2, "string GetValue(string colName, SQLPara para);");
            strclass.AppendSpaceLine(2, "int Counts(string strWhere);");
            strclass.AppendSpaceLine(2, "int Counts(SQLPara para);");
            strclass.AppendSpaceLine(2, "int GetMaxID(string strWhere);");
            strclass.AppendSpaceLine(2, "int GetMaxID(SQLPara para);");
            strclass.AppendSpaceLine(2, "int Add(Shop.Model." + TableName + " model);");
            strclass.AppendSpaceLine(2, "void Update(Shop.Model." + TableName + " model);");
            strclass.AppendSpaceLine(2, "void Delete(int id);");
            strclass.AppendSpaceLine(2, "void Delete(string strWhere);");
            strclass.AppendSpaceLine(2, "void Delete(SQLPara para);");
            strclass.AppendSpaceLine(2, "Shop.Model." + TableName + " GetModel(int id);");
            strclass.AppendSpaceLine(2, "Shop.Model." + TableName + " GetModel(string strWhere);");
            strclass.AppendSpaceLine(2, "Shop.Model." + TableName + " GetModel(SQLPara para);");
            strclass.AppendSpaceLine(2, "List<Shop.Model." + TableName + "> GetList(string strWhere, string strFieldOrder, int PageSize, int page);");
            strclass.AppendSpaceLine(2, "List<Shop.Model." + TableName + "> GetList(SQLPara para, int PageSize, int page);");
            strclass.AppendSpaceLine(2, "List<Shop.Model." + TableName + "> GetList(string strWhere, string strFieldOrder);");
            strclass.AppendSpaceLine(2, "List<Shop.Model." + TableName + "> GetList(SQLPara para);");
            strclass.AppendSpaceLine(2, "Shop.Model." + TableName + " BindForm(Shop.Model." + TableName + " model);");
            strclass.AppendSpaceLine(2, "Shop.Model." + TableName + " SafeBindForm(Shop.Model." + TableName + " model);");
            strclass.AppendSpaceLine(2, "Shop.Model." + TableName + " ReaderBind(IDataReader dataReader);");
            strclass.AppendSpaceLine(1, "}");
            return strclass.Value;
        }
        public string CreatInstance()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "static " + TableName + "_interface _Instance;");
            strclass.AppendSpaceLine(2, "public static " + TableName + "_interface Instance");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(2, "   get");
            strclass.AppendSpaceLine(2, "   {");
            strclass.AppendSpaceLine(2, "        if (_Instance == null)");
            strclass.AppendSpaceLine(2, "        {");
            strclass.AppendSpaceLine(2, "            if (BaseUtils.BaseUtilsInstance.DBType == \"access\")");
            strclass.AppendSpaceLine(2, "                _Instance = new access_D_" + TableName + "();");
            strclass.AppendSpaceLine(2, "            else");
            strclass.AppendSpaceLine(2, "                _Instance = new sqlserver_D_" + TableName + "();");
            strclass.AppendSpaceLine(2, "        }");
            strclass.AppendSpaceLine(2, "        return _Instance;");
            strclass.AppendSpaceLine(2, "    }");
            strclass.AppendSpaceLine(2, "    set");
            strclass.AppendSpaceLine(2, "    {");
            strclass.AppendSpaceLine(2, "        _Instance = value;");
            strclass.AppendSpaceLine(2, "    }");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        #endregion
        #region 数据层 for sqlserver
        /// <summary>
        /// 得到最大ID的方法代码
        /// </summary>
        /// <param name="TabName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string CreatGetMaxID()
        {
            StringPlus strclass = new StringPlus();
            if (_columninfos.Count > 0)
            {
                string keyname = "";
                foreach (ColumnInfo obj in _columninfos)
                {
                    if (CodeCommon.DbTypeToCS(obj.TypeName) == "int")
                    {
                        keyname = obj.ColumnName;
                        if (obj.IsPK)
                        {
                            strclass.AppendLine("");
                            strclass.AppendSpaceLine(2, "/// <summary>");
                            strclass.AppendSpaceLine(2, "/// 得到最大ID");
                            strclass.AppendSpaceLine(2, "/// </summary>");
                            strclass.AppendSpaceLine(2, "public int GetMaxID(string strWhere)");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
                            strclass.AppendSpaceLine(3, "{");
                            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
                            strclass.AppendSpaceLine(4, "return GetMaxID(para);");
                            strclass.AppendSpaceLine(3, "}");
                            strclass.AppendSpaceLine(3, "StringBuilder strSql = new StringBuilder();");
                            strclass.AppendSpaceLine(3, "strSql.Append(\"select max(" + keyname + ") from [" + TableName + "]\");");
                            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
                            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
                            strclass.AppendSpaceLine(3, "return Convert.ToInt32(" + DbHelperName + ".TextExecuteScalar(strSql.ToString()));");
                            strclass.AppendSpaceLine(2, "}");
                            strclass.AppendSpaceLine(2, "public int GetMaxID(SQLPara para)");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
                            strclass.AppendSpaceLine(3, "strSql.Append(\"select count(1) from [" + TableName + "]\");");
                            strclass.AppendSpaceLine(3, "if(para.Where!=\"\")");
                            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+para.Where);");
                            strclass.AppendSpaceLine(3, "return Convert.ToInt32( " + DbHelperName + ".TextExecuteScalar(strSql.ToString(), para.Para)); ");
                            strclass.AppendSpaceLine(2, "}");
                            break;
                        }
                    }
                }
            }
            return strclass.Value;
        }

        public string CreatCounts()
        {
            StringPlus strclass = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 计算记录条数");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public int Counts(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
            strclass.AppendSpaceLine(4, "return Counts(para);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select count(1) from [" + TableName + "]\");");
            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(3, "return Convert.ToInt32( " + DbHelperName + ".TextExecuteScalar(strSql.ToString())); ");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public int Counts(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select count(1) from [" + TableName + "]\");");
            strclass.AppendSpaceLine(3, "if(para.Where!=\"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+para.Where);");
            strclass.AppendSpaceLine(3, "return Convert.ToInt32( " + DbHelperName + ".TextExecuteScalar(strSql.ToString(), para.Para)); ");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }
        public string CreatGetValue()
        {
            StringPlus strclass = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 根据字段名，where条件获取一个值,返回字符串");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public string GetValue(string colName,string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "string val = \"\";");
            strclass.AppendSpaceLine(3, "try");
            strclass.AppendSpaceLine(3, "{");
            //strclass.AppendSpaceLine(4, "val = Convert.ToString(" + DbHelperName + ".TextExecuteScalar(sql));");
            strclass.AppendSpaceLine(4, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(4, "strSql.Append(\"select \" + colName + \" from [" + TableName + "]\");");
            strclass.AppendSpaceLine(4, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(4, "val = Convert.ToString(" + DbHelperName + ".TextExecuteScalar(strSql.ToString()));");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "catch");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "val = \"\";");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return val;");
            strclass.AppendSpaceLine(2, "}");

            strclass.AppendSpaceLine(2, "public string GetValue(string colName,SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select  \"+colName+\" from [" + TableName + "]\");");
            strclass.AppendSpaceLine(3, "if(para.Where!=\"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+para.Where);");
            strclass.AppendSpaceLine(3, "return Convert.ToString( " + DbHelperName + ".TextExecuteScalar(strSql.ToString(), para.Para)); ");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }

        /// <summary>
        /// 得到Add()的代码
        /// </summary>        
        public string CreatAdd()
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            StringPlus strclass3 = new StringPlus();
            StringPlus strclass4 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 增加一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            string strretu = "int";

            //方法定义头
            string strFun = CodeCommon.Space(2) + "public " + strretu + " Add(" + ModelName + " model)";
            strclass.AppendLine(strFun);
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"insert into [" + TableName + "](\");");
            strclass1.AppendSpace(3, "strSql.Append(\"");
            int n = 0;
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                string Length = field.Length;
                if (field.IsIdentity)
                {
                    continue;
                }
                strclass3.AppendSpaceLine(5, "new SqlParameter(\"@" + columnName + "\", model." + columnName + "),");
                strclass1.Append(columnName + ",");
                strclass2.Append("@" + columnName + ",");
                //strclass4.AppendSpaceLine(3, "parameters[" + n + "].Value = model." + columnName + ";");
                n++;
            }

            //去掉最后的逗号
            strclass1.DelLastComma();
            strclass2.DelLastComma();
            strclass3.DelLastComma();
            strclass1.AppendLine(")\");");
            strclass.Append(strclass1.Value);
            strclass.AppendSpaceLine(3, "strSql.Append(\" values (\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\"" + strclass2.Value + ")\");");
            if (IsHasIdentity)
            {
                strclass.AppendSpaceLine(3, "strSql.Append(\";select @@IDENTITY\");");
            }
            strclass.AppendSpaceLine(3, "SqlParameter[] parameters = {");
            strclass.Append(strclass3.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass4.Value);

            //重新定义方法头
            if (IsHasIdentity)
            {
                strclass.AppendSpaceLine(3, "object obj = " + DbHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
                strclass.AppendSpaceLine(3, "if (obj == null)");
                strclass.AppendSpaceLine(3, "{");
                strclass.AppendSpaceLine(4, "return 1;");
                strclass.AppendSpaceLine(3, "}");
                strclass.AppendSpaceLine(3, "else");
                strclass.AppendSpaceLine(3, "{");
                strclass.AppendSpaceLine(4, "return Convert.ToInt32(obj);");
                strclass.AppendSpaceLine(3, "}");

            }
            else
            {
                strclass.AppendSpaceLine(3, "" + DbHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
                strclass.AppendSpaceLine(4, "return 1;");
            }
            strclass.AppendSpace(2, "}");
            return strclass.Value;
        }
        /// <summary>
        /// 得到Update（）的代码
        /// </summary>
        /// <param name="DbName"></param>
        /// <param name="_tablename"></param>
        /// <param name="_key"></param>
        /// <param name="ModelName"></param>
        /// <returns></returns>
        public string CreatUpdate()
        {

            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 更新一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Update(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"update [" + TableName + "] set \");");
            int n = 0;
            string PKCol = "";
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPK;
                if (field.IsPK)
                    PKCol = columnName;
                strclass1.AppendSpaceLine(5, "new SqlParameter(\"@" + columnName + "\", SqlDbType." + CodeCommon.DbTypeLength("SQL", columnType, Length) + "),");
                strclass2.AppendSpaceLine(3, "parameters[" + n + "].Value = model." + columnName + ";");
                n++;
                if (field.IsIdentity || field.IsPK)
                {
                    continue;
                }
                strclass.AppendSpaceLine(3, "strSql.Append(\"" + columnName + "= @" + columnName + ",\");");
            }


            //去掉最后的逗号			
            strclass.DelLastComma();
            strclass.AppendLine("\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + PKCol + "=@" + PKCol + "\");");

            strclass.AppendSpaceLine(3, "SqlParameter[] parameters = {");
            strclass1.DelLastComma();
            strclass.Append(strclass1.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass2.Value);
            strclass.AppendSpaceLine(3, "" + DbHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }

        /// <summary>
        /// 得到Delete的代码
        /// </summary>
        /// <param name="_tablename"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public string CreatDelete()
        {
            StringPlus strclass = new StringPlus();
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            List<ColumnInfo> PKCOLS = new List<ColumnInfo>();
            PKCOLS.Add(PKCOL);
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Delete(" + PKCOL.TypeName + " " + PKCOL.ColumnName + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            //if (dbobj.DbType != "OleDb")
            //{
            //    strclass.AppendSpaceLine(3, "strSql.Append(\"delete [" + _tablename + "] \");");
            //}
            //else
            //{
            strclass.AppendSpaceLine(3, "strSql.Append(\"delete from [" + TableName + "] \");");
            //}
            strclass.AppendSpaceLine(3, "strSql.Append(\" where @" + PKCOL.ColumnName + "=" + PKCOL.ColumnName + "\");");

            strclass.AppendLine(GetPreParameter(PKCOLS));

            strclass.AppendSpaceLine(3, "" + DbHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(2, "}");

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除多条数据  by where条件");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Delete(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
            strclass.AppendSpaceLine(4, "Delete(para);");
            strclass.AppendSpaceLine(4, "return;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"delete from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where \"+ strWhere +\"\");");
            strclass.AppendSpaceLine(3, "" + DbHelperName + ".TextExecuteNonQuery(strSql.ToString());");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public void Delete(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"delete from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "if (para.Where != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+ para.Where +\"\");");
            strclass.AppendSpaceLine(3, "" + DbHelperName + ".TextExecuteNonQuery(strSql.ToString(),para.Para);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }

        /// <summary>
        /// 得到GetModel()的代码
        /// </summary>
        /// <param name="DbName"></param>
        /// <param name="_tablename"></param>
        /// <param name="_key"></param>
        /// <param name="ModelName"></param>
        /// <returns></returns>
        public string CreatGetModel()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            List<ColumnInfo> PKCOLS = new List<ColumnInfo>();
            PKCOLS.Add(PKCOL);

            StringPlus strclass = new StringPlus();
            strclass.AppendLine();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by id");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " GetModel(" + PKCOL.TypeName + " " + PKCOL.ColumnName + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.Append(" top 1 ");
            strclass.AppendLine(" * from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + PKCOL.ColumnName + "=@" + PKCOL.ColumnName + "\");");
            strclass.AppendLine(GetPreParameter(PKCOLS));
            strclass.AppendSpaceLine(3, "" + ModelName + " model=new " + ModelName + "();");
            strclass.AppendSpaceLine(3, "DataSet ds=" + DbHelperName + ".TextExecuteDataset(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
            strclass.AppendSpaceLine(3, "{");
            #region 字段赋值
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=int.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "long":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=long.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=decimal.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=float.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=DateTime.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "if((ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString().ToLower()==\"true\"))");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=true;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(5, "else");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=false;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=(byte[])ds.Tables[0].Rows[0][\"" + columnName + "\"];");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "Guid":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=new Guid(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    default:
                        strclass.AppendSpaceLine(4, "//model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        break;
                }
            }
            #endregion
            strclass.AppendSpaceLine(4, "return model;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "else");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "return null;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by where条件");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " GetModel(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
            strclass.AppendSpaceLine(4, "return GetModel(para);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.Append(" top 1 ");
            strclass.AppendLine(" * from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where \"+ strWhere +\"\");");
            //strclass.AppendLine(GetPreParameter(PKCOLS));
            strclass.AppendSpaceLine(3, "" + ModelName + " model=new " + ModelName + "();");
            strclass.AppendSpaceLine(3, "DataSet ds=" + DbHelperName + ".TextExecuteDataset(strSql.ToString());");
            strclass.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
            strclass.AppendSpaceLine(3, "{");
            #region 字段赋值
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=int.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "long":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=long.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=decimal.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=float.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=DateTime.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "if((ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString().ToLower()==\"true\"))");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=true;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(5, "else");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=false;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=(byte[])ds.Tables[0].Rows[0][\"" + columnName + "\"];");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "Guid":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=new Guid(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    default:
                        strclass.AppendSpaceLine(4, "//model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        break;
                }
            }
            #endregion
            strclass.AppendSpaceLine(4, "return model;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "else");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "return null;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by SQLpara");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " GetModel(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select top 1");
            strclass.AppendLine(" * from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "if (para.Where != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+ para.Where +\"\");");
            //strclass.AppendLine(GetPreParameter(PKCOLS));
            strclass.AppendSpaceLine(3, "" + ModelName + " model=new " + ModelName + "();");
            strclass.AppendSpaceLine(3, "DataSet ds=" + DbHelperName + ".TextExecuteDataset(strSql.ToString(),para.Para);");
            strclass.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
            strclass.AppendSpaceLine(3, "{");
            #region 字段赋值
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=int.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "long":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=long.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=decimal.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=float.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=DateTime.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "if((ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString().ToLower()==\"true\"))");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=true;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(5, "else");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=false;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=(byte[])ds.Tables[0].Rows[0][\"" + columnName + "\"];");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "Guid":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=new Guid(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    default:
                        strclass.AppendSpaceLine(4, "//model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        break;
                }
            }
            #endregion
            strclass.AppendSpaceLine(4, "return model;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "else");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "return null;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;

        }
        public string CreatBindForm()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            List<ColumnInfo> PKCOLS = new List<ColumnInfo>();
            PKCOLS.Add(PKCOL);
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 绑定对象表单");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " BindForm(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            #region 字段赋值
            foreach (ColumnInfo field in _columninfos)
            {
                if (field.IsIdentity)
                    continue;
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                strclass.AppendSpaceLine(3, "if (HttpContext.Current.Request[\"" + columnName + "\"] != null)");
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestInt(\"" + columnName + "\",0);");
                        }
                        break;
                    case "long":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestInt(\"" + columnName + "\",0);");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestDecimal(\"" + columnName + "\",0);");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestFloat(\"" + columnName + "\",0);");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestTime(\"" + columnName + "\", System.DateTime.Now);");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestString(\"" + columnName + "\");");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestBool(\"" + columnName + "\");");
                        }
                        break;
                    default:
                        continue;
                    //break;
                }
            }
            #endregion
            strclass.AppendSpaceLine(4, "return model;");
            strclass.AppendSpaceLine(2, "}");
            //return strclass.Value;

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 安全方式绑定对象表单");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " SafeBindForm(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            #region 字段赋值
            foreach (ColumnInfo field in _columninfos)
            {
                if (field.IsIdentity)
                    continue;
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                strclass.AppendSpaceLine(3, "if (HttpContext.Current.Request[\"" + columnName + "\"] != null)");
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestInt(\"" + columnName + "\",0);");
                        }
                        break;
                    case "long":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestInt(\"" + columnName + "\",0);");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestDecimal(\"" + columnName + "\",0);");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestFloat(\"" + columnName + "\",0);");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestTime(\"" + columnName + "\", System.DateTime.Now);");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestSafeString(\"" + columnName + "\");");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=Shop.Tools.RequestTool.RequestBool(\"" + columnName + "\");");
                        }
                        break;
                    default:
                        continue;
                    //break;
                }
            }
            #endregion
            strclass.AppendSpaceLine(4, "return model;");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        /// <summary>
        /// 获得数据列表-带分页
        /// </summary>
        /// <returns></returns>
        public string CreatGetListArray()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            string strList = "List<" + ModelName + ">";
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 获得数据列表-带分页");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(string strWhere, string strFieldOrder, int PageSize, int page)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, strFieldOrder, \"\");");
            strclass.AppendSpaceLine(4, "return GetList(para,PageSize,page);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "string strTableName = \"[" + TableName + "]\";");
            strclass.AppendSpaceLine(3, "string strFieldKey = \"" + PKCOL.ColumnName + "\";");
            strclass.AppendSpaceLine(3, "string strFieldShow = \"*\";");
            strclass.AppendSpaceLine(3, strList + " list = new " + strList + "();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = " + DbHelperName + ".StoredProcedureExecuteReader(\"usp_CommonPagination\", strTableName, strFieldKey, strFieldShow, strFieldOrder, strWhere, PageSize, page))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "while (dataReader.Read())");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(4, "return list;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(2, "}");
            //生成参数式查询
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(SQLPara para, int PageSize, int page)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "string strTableName = \"[" + TableName + "]\";");
            strclass.AppendSpaceLine(3, "string strFieldKey = \"" + PKCOL.ColumnName + "\";");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select top \" + PageSize + \" \" + para.ShowField + \" from \" + strTableName + \"\");");
            strclass.AppendSpaceLine(3, "if (para != null)");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \" + para.Where + \"\");");
            strclass.AppendSpaceLine(3, "if (page > 1)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "if (para != null)");
            strclass.AppendSpaceLine(5, "strSql.Append(\" and \");");
            strclass.AppendSpaceLine(4, "else");
            strclass.AppendSpaceLine(5, "strSql.Append(\" where \");");
            strclass.AppendSpaceLine(4, "strSql.Append(strFieldKey + \" not in (select top \" + (PageSize * (page - 1)) + \" \" + strFieldKey + \" from \" + strTableName + \"\");");
            strclass.AppendSpaceLine(4, "if (para != null)");
            strclass.AppendSpaceLine(5, "strSql.Append(\" where \" + para.Where + \"\");");
            strclass.AppendSpaceLine(4, "if (para.Order != \"\")");
            strclass.AppendSpaceLine(5, "strSql.Append(\" order by \" + para.Order + \"\");");
            strclass.AppendSpaceLine(4, "strSql.Append(\")\");");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "if (para.Order != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" order by \" + para.Order + \"\");");
            strclass.AppendSpaceLine(3, "List<Shop.Model." + TableName + "> list = new List<Shop.Model." + TableName + ">();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = SqlUtils.SqlUtilsInstance.TextExecuteReader(strSql.ToString(), para.Para))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "while (dataReader.Read())");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return list;");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }
        /// <summary>
        /// 获得数据列表-不带分页
        /// </summary>
        /// <returns></returns>
        public string CreatGetListArray_ALL()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            string strList = "List<" + ModelName + ">";
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 获得数据列表-不带分页");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(string strWhere,string strFieldOrder)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, strFieldOrder, \"\");");
            strclass.AppendSpaceLine(4, "return GetList(para);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.AppendLine("* \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" FROM [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "if(strFieldOrder.Trim()!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "strSql.Append(\" order by \"+strFieldOrder);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, strList + " list = new " + strList + "();");
            //strclass.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = " + DbHelperName + ".TextExecuteReader(strSql.ToString()))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "while (dataReader.Read())");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return list;");
            strclass.AppendSpaceLine(2, "}");
            //生成参数式查询
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "string strTableName = \"[" + TableName + "]\";");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select \" + para.ShowField + \" from \" + strTableName + \"\");");
            strclass.AppendSpaceLine(3, "if (para != null)");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \" + para.Where + \"\");");
            strclass.AppendSpaceLine(3, "if (para.Order != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" order by \" + para.Order + \"\");");
            strclass.AppendSpaceLine(3, "List<Shop.Model." + TableName + "> list = new List<Shop.Model." + TableName + ">();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = SqlUtils.SqlUtilsInstance.TextExecuteReader(strSql.ToString(), para.Para))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "while (dataReader.Read())");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return list;");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        /// <summary>
        /// 生成对象实体绑定数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string CreatReaderBind()
        {

            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            strclass.AppendLine("");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 对象实体绑定数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " ReaderBind(IDataReader dataReader)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, ModelName + " model=new " + ModelName + "();");

            bool isobj = false;
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                string Length = field.Length;

                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "=(int)ojb;");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    case "long":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "=(long)ojb;");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    case "decimal":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");

                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "=(decimal)ojb;");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "=(DateTime)ojb;");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass1.AppendSpaceLine(3, "model." + columnName + "=dataReader[\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");

                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "=(bool)ojb;");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");

                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "=(byte[])ojb;");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    case "Guid":
                        {
                            isobj = true;
                            strclass1.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
                            strclass1.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
                            strclass1.AppendSpaceLine(3, "{");
                            strclass1.AppendSpaceLine(4, "model." + columnName + "= new Guid(ojb.ToString());");
                            strclass1.AppendSpaceLine(3, "}");
                        }
                        break;
                    default:
                        strclass1.AppendSpaceLine(3, "model." + columnName + "=dataReader[\"" + columnName + "\"].ToString();\r\n");
                        break;
                }
            }
            if (isobj)
            {
                strclass.AppendSpaceLine(3, "object ojb; ");
            }
            strclass.Append(strclass1.Value);
            strclass.AppendSpaceLine(3, "return model;");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        /// <summary>
        /// 生成sql语句中的参数列表(例如：用于Add  Exists  Update Delete  GetModel 的参数传入)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetPreParameter(List<ColumnInfo> keys)
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendSpaceLine(3, "SqlParameter[] parameters = {");
            int n = 0;
            foreach (ColumnInfo key in keys)
            {
                strclass.AppendSpaceLine(5, "new SqlParameter(\"@" + key.ColumnName + "\", SqlDbType." + CodeCommon.DbTypeLength("SQL", key.TypeName, "") + "),");
                strclass2.AppendSpaceLine(3, "parameters[" + n.ToString() + "].Value = " + key.ColumnName + ";");
                n++;
            }
            strclass.DelLastComma();
            strclass.AppendLine("};");
            strclass.Append(strclass2.Value);
            return strclass.Value;
        }

        #endregion
        #region 数据层 for access
        /// <summary>
        /// 得到最大ID的方法代码
        /// </summary>
        /// <param name="TabName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string CreatGetMaxID_access()
        {
            StringPlus strclass = new StringPlus();
            if (_columninfos.Count > 0)
            {
                string keyname = "";
                foreach (ColumnInfo obj in _columninfos)
                {
                    if (CodeCommon.DbTypeToCS(obj.TypeName) == "int")
                    {
                        keyname = obj.ColumnName;
                        if (obj.IsPK)
                        {
                            strclass.AppendLine("");
                            strclass.AppendSpaceLine(2, "/// <summary>");
                            strclass.AppendSpaceLine(2, "/// 得到最大ID");
                            strclass.AppendSpaceLine(2, "/// </summary>");
                            strclass.AppendSpaceLine(2, "public int GetMaxID(string strWhere)");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
                            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
                            strclass.AppendSpaceLine(3, "{");
                            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
                            strclass.AppendSpaceLine(4, "return GetMaxID(para);");
                            strclass.AppendSpaceLine(3, "}");
                            strclass.AppendSpaceLine(3, "StringBuilder strSql = new StringBuilder();");
                            strclass.AppendSpaceLine(3, "strSql.Append(\"select max(" + keyname + ") from [" + TableName + "]\");");
                            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
                            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
                            strclass.AppendSpaceLine(3, "return Convert.ToInt32(" + AccessHelperName + ".TextExecuteScalar(strSql.ToString(),null));");
                            strclass.AppendSpaceLine(2, "}");
                            strclass.AppendSpaceLine(2, "public int GetMaxID(SQLPara para)");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
                            strclass.AppendSpaceLine(3, "strSql.Append(\"select count(1) from [" + TableName + "]\");");
                            strclass.AppendSpaceLine(3, "if(para.Where!=\"\")");
                            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+para.Where);");
                            strclass.AppendSpaceLine(3, "return Convert.ToInt32( " + AccessHelperName + ".TextExecuteScalar(strSql.ToString(), para.Para_Oledb)); ");
                            strclass.AppendSpaceLine(2, "}");
                            break;
                        }
                    }
                }
            }
            return strclass.Value;
        }

        public string CreatCounts_access()
        {
            StringPlus strclass = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 计算记录条数");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public int Counts(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
            strclass.AppendSpaceLine(4, "return Counts(para);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select count(*) from [" + TableName + "]\");");
            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(3, "return Convert.ToInt32( " + AccessHelperName + ".TextExecuteScalar(strSql.ToString(),null)); ");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public int Counts(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select count(*) from [" + TableName + "]\");");
            strclass.AppendSpaceLine(3, "if(para.Where!=\"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+para.Where);");
            strclass.AppendSpaceLine(3, "return Convert.ToInt32( " + AccessHelperName + ".TextExecuteScalar(strSql.ToString(), para.Para_Oledb)); ");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }
        public string CreatGetValue_access()
        {
            StringPlus strclass = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 根据字段名，where条件获取一个值,返回字符串");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public string GetValue(string colName,string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
            strclass.AppendSpaceLine(3, "string val = \"\";");
            strclass.AppendSpaceLine(3, "try");
            strclass.AppendSpaceLine(3, "{");
            //strclass.AppendSpaceLine(4, "val = Convert.ToString(" + DbHelperName + ".TextExecuteScalar(sql));");
            strclass.AppendSpaceLine(4, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(4, "strSql.Append(\"select \" + colName + \" from [" + TableName + "]\");");
            strclass.AppendSpaceLine(4, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(4, "val = Convert.ToString(" + AccessHelperName + ".TextExecuteScalar(strSql.ToString(),null));");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "catch");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "val = \"\";");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return val;");
            strclass.AppendSpaceLine(2, "}");

            strclass.AppendSpaceLine(2, "public string GetValue(string colName,SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select  \"+colName+\" from [" + TableName + "]\");");
            strclass.AppendSpaceLine(3, "if(para.Where!=\"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+para.Where);");
            strclass.AppendSpaceLine(3, "return Convert.ToString(" + AccessHelperName + ".TextExecuteScalar(strSql.ToString(), para.Para_Oledb)); ");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }

        /// <summary>
        /// 得到Add()的代码
        /// </summary>        
        public string CreatAdd_access()
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            StringPlus strclass3 = new StringPlus();
            StringPlus strclass4 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 增加一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            string strretu = "int";

            //方法定义头
            string strFun = CodeCommon.Space(2) + "public " + strretu + " Add(" + ModelName + " model)";
            strclass.AppendLine(strFun);
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"insert into [" + TableName + "](\");");
            strclass1.AppendSpace(3, "strSql.Append(\"");
            int n = 0;
            foreach (ColumnInfo field in _columninfos)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                string Length = field.Length;
                if (field.IsIdentity)
                {
                    continue;
                }
                if (columnType=="datetime")
                    strclass3.AppendSpaceLine(5, "new OleDbParameter(\"@" + columnName + "\", model." + columnName + ".ToString(\"yyyy-MM-dd HH:mm:ss\")),");
                else
                    strclass3.AppendSpaceLine(5, "new OleDbParameter(\"@" + columnName + "\", model." + columnName + "),");
                strclass1.Append("["+columnName + "],");
                strclass2.Append("@" + columnName + ",");
                //strclass4.AppendSpaceLine(3, "parameters[" + n + "].Value = model." + columnName + ";");
                n++;
            }

            //去掉最后的逗号
            strclass1.DelLastComma();
            strclass2.DelLastComma();
            strclass3.DelLastComma();
            strclass1.AppendLine(")\");");
            strclass.Append(strclass1.Value);
            strclass.AppendSpaceLine(3, "strSql.Append(\" values (\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\"" + strclass2.Value + ")\");");
            strclass.AppendSpaceLine(3, "OleDbParameter[] parameters = {");
            strclass.Append(strclass3.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass4.Value);


            strclass.AppendSpaceLine(3, "" + AccessHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(4, "return 1;");

            strclass.AppendSpace(2, "}");
            return strclass.Value;
        }
        /// <summary>
        /// 得到Update（）的代码
        /// </summary>
        /// <param name="DbName"></param>
        /// <param name="_tablename"></param>
        /// <param name="_key"></param>
        /// <param name="ModelName"></param>
        /// <returns></returns>
        public string CreatUpdate_access()
        {

            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 更新一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Update(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"update [" + TableName + "] set \");");
            int n = 0;
            string PKCol = "";
            if (TableName == "Lebi_Type")
                PKCol = "";
            foreach (ColumnInfo field in _columninfos)
            {
                
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPK;
                if (field.IsPK)
                    PKCol = columnName;
                if (field.IsIdentity || field.IsPK)
                {
                    continue;
                }
                if (columnType == "datetime")
                    strclass1.AppendSpaceLine(5, "new OleDbParameter(\"@" + columnName + "\", model." + columnName + ".ToString(\"yyyy-MM-dd HH:mm:ss\")),");
                else
                    strclass1.AppendSpaceLine(5, "new OleDbParameter(\"@" + columnName + "\", model." + columnName + "),");
                //strclass2.AppendSpaceLine(3, "parameters[" + n + "].Value = model." + columnName + ";");
                n++;
                
                strclass.AppendSpaceLine(3, "strSql.Append(\"[" + columnName + "]=@" + columnName + ",\");");
            }


            //去掉最后的逗号			
            strclass.DelLastComma();
            strclass.AppendLine("\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + PKCol + "=\"+model." + PKCol+");");
            strclass.AppendSpaceLine(3, "OleDbParameter[] parameters = {");

            //strclass1.AppendSpaceLine(5, "new OleDbParameter(\"@" + PKCol + "\", model." + PKCol + "),");
            strclass1.DelLastComma();
            strclass.Append(strclass1.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass2.Value);
            strclass.AppendSpaceLine(3, "" + AccessHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }

        /// <summary>
        /// 得到Delete的代码
        /// </summary>
        /// <param name="_tablename"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public string CreatDelete_access()
        {
            StringPlus strclass = new StringPlus();
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            List<ColumnInfo> PKCOLS = new List<ColumnInfo>();
            PKCOLS.Add(PKCOL);
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Delete(" + PKCOL.TypeName + " " + PKCOL.ColumnName + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            //if (dbobj.DbType != "OleDb")
            //{
            //    strclass.AppendSpaceLine(3, "strSql.Append(\"delete [" + _tablename + "] \");");
            //}
            //else
            //{
            strclass.AppendSpaceLine(3, "strSql.Append(\"delete from [" + TableName + "] \");");
            //}
            strclass.AppendSpaceLine(3, "strSql.Append(\" where @" + PKCOL.ColumnName + "=" + PKCOL.ColumnName + "\");");

            strclass.AppendLine(GetPreParameter_access(PKCOLS));

            strclass.AppendSpaceLine(3, "" + AccessHelperName + ".TextExecuteNonQuery(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(2, "}");

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除多条数据  by where条件");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Delete(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
            strclass.AppendSpaceLine(4, "Delete(para);");
            strclass.AppendSpaceLine(4, "return;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"delete from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where \"+ strWhere +\"\");");
            strclass.AppendSpaceLine(3, "" + AccessHelperName + ".TextExecuteNonQuery(strSql.ToString(),null);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public void Delete(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"delete from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "if (para.Where != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+ para.Where +\"\");");
            strclass.AppendSpaceLine(3, "" + AccessHelperName + ".TextExecuteNonQuery(strSql.ToString(),para.Para_Oledb);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }

        /// <summary>
        /// 得到GetModel()的代码
        /// </summary>
        /// <param name="DbName"></param>
        /// <param name="_tablename"></param>
        /// <param name="_key"></param>
        /// <param name="ModelName"></param>
        /// <returns></returns>
        public string CreatGetModel_access()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            List<ColumnInfo> PKCOLS = new List<ColumnInfo>();
            PKCOLS.Add(PKCOL);

            StringPlus strclass = new StringPlus();
            strclass.AppendLine();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by id");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " GetModel(" + PKCOL.TypeName + " " + PKCOL.ColumnName + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.Append(" top 1 ");
            strclass.AppendLine(" * from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + PKCOL.ColumnName + "=@" + PKCOL.ColumnName + "\");");
            strclass.AppendLine(GetPreParameter_access(PKCOLS));
            strclass.AppendSpaceLine(3, "" + ModelName + " model;");
            strclass.AppendSpaceLine(3, "using (OleDbDataReader dataReader = AccessUtils.Instance.DataReader(strSql.ToString(), parameters))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "    if (dataReader != null)");
            strclass.AppendSpaceLine(3, "    {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           model = ReaderBind(dataReader);");
            strclass.AppendSpaceLine(3, "           return model;");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "    }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return null;");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by where条件");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " GetModel(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, \"\", \"\");");
            strclass.AppendSpaceLine(4, "return GetModel(para);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.Append(" top 1 ");
            strclass.AppendLine(" * from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where \"+ strWhere +\"\");");
            //strclass.AppendLine(GetPreParameter(PKCOLS));
            strclass.AppendSpaceLine(3, "" + ModelName + " model;");
            strclass.AppendSpaceLine(3, "using (OleDbDataReader dataReader = AccessUtils.Instance.DataReader(strSql.ToString(), null))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "    if (dataReader != null)");
            strclass.AppendSpaceLine(3, "    {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           model = ReaderBind(dataReader);");
            strclass.AppendSpaceLine(3, "           return model;");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "    }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return null;");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by SQLpara");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelName + " GetModel(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select top 1");
            strclass.AppendLine(" * from [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "if (para.Where != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+ para.Where +\"\");");
            //strclass.AppendLine(GetPreParameter(PKCOLS));
            strclass.AppendSpaceLine(3, "" + ModelName + " model;");
            strclass.AppendSpaceLine(3, "using (OleDbDataReader dataReader = AccessUtils.Instance.DataReader(strSql.ToString(), para.Para_Oledb))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "    if (dataReader != null)");
            strclass.AppendSpaceLine(3, "    {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           model = ReaderBind(dataReader);");
            strclass.AppendSpaceLine(3, "           return model;");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "    }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return null;");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;

        }
        /// <summary>
        /// 获得数据列表-带分页
        /// </summary>
        /// <returns></returns>
        public string CreatGetListArray_access()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            string strList = "List<" + ModelName + ">";
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 获得数据列表-带分页");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(string strWhere, string strFieldOrder, int PageSize, int page)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, strFieldOrder, \"\");");
            strclass.AppendSpaceLine(4, "return GetList(para,PageSize,page);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "string strTableName = \"[" + TableName + "]\";");
            strclass.AppendSpaceLine(3, "string strFieldKey = \"" + PKCOL.ColumnName + "\";");
            strclass.AppendSpaceLine(3, "string strFieldShow = \"*\";");
            strclass.AppendSpaceLine(3, strList + " list = new " + strList + "();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = " + AccessHelperName + ".DataReader(strTableName, strFieldKey, strFieldShow, strFieldOrder, strWhere, PageSize, page,null))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "   if(dataReader!=null)");
            strclass.AppendSpaceLine(3, "   {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "   }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(4, "return list;");
            strclass.AppendSpaceLine(3, "}");
            //生成参数式查询
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(SQLPara para, int PageSize, int page)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "string strTableName = \"[" + TableName + "]\";");
            strclass.AppendSpaceLine(3, "string strFieldKey = \"" + PKCOL.ColumnName + "\";");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select top \" + PageSize + \" \" + para.ShowField + \" from \" + strTableName + \"\");");
            strclass.AppendSpaceLine(3, "if (para != null)");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \" + para.Where + \"\");");
            strclass.AppendSpaceLine(3, "if (page > 1)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "if (para != null)");
            strclass.AppendSpaceLine(5, "strSql.Append(\" and \");");
            strclass.AppendSpaceLine(4, "else");
            strclass.AppendSpaceLine(5, "strSql.Append(\" where \");");
            strclass.AppendSpaceLine(4, "strSql.Append(strFieldKey + \" not in (select top \" + (PageSize * (page - 1)) + \" \" + strFieldKey + \" from \" + strTableName + \"\");");
            strclass.AppendSpaceLine(4, "if (para != null)");
            strclass.AppendSpaceLine(5, "strSql.Append(\" where \" + para.Where + \"\");");
            strclass.AppendSpaceLine(4, "if (para.Order != \"\")");
            strclass.AppendSpaceLine(5, "strSql.Append(\" order by \" + para.Order + \"\");");
            strclass.AppendSpaceLine(4, "strSql.Append(\")\");");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "if (para.Order != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" order by \" + para.Order + \"\");");
            strclass.AppendSpaceLine(3, "List<Shop.Model." + TableName + "> list = new List<Shop.Model." + TableName + ">();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = " + AccessHelperName + ".DataReader(strSql.ToString(), para.Para_Oledb))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "   if(dataReader!=null)");
            strclass.AppendSpaceLine(3, "   {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "   }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return list;");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }
        /// <summary>
        /// 获得数据列表-不带分页
        /// </summary>
        /// <returns></returns>
        public string CreatGetListArray_ALL_access()
        {
            ColumnInfo PKCOL = CodeCommon.GetPKCOL(_columninfos);
            string strList = "List<" + ModelName + ">";
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 获得数据列表-不带分页");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(string strWhere,string strFieldOrder)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "strWhere=BaseUtils.BaseUtilsInstance.SetWhere(strWhere);");
            strclass.AppendSpaceLine(3, "if (strWhere.IndexOf(\"lbsql{\") > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "SQLPara para = new SQLPara(strWhere, strFieldOrder, \"\");");
            strclass.AppendSpaceLine(4, "return GetList(para);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.AppendLine("* \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" FROM [" + TableName + "] \");");
            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "if(strFieldOrder.Trim()!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "strSql.Append(\" order by \"+strFieldOrder);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, strList + " list = new " + strList + "();");
            //strclass.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
            strclass.AppendSpaceLine(3, "using (OleDbDataReader dataReader = " + AccessHelperName + ".DataReader(strSql.ToString(),null))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "   if(dataReader!=null)");
            strclass.AppendSpaceLine(3, "   {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "   }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return list;");
            strclass.AppendSpaceLine(2, "}");
            //生成参数式查询
            strclass.AppendSpaceLine(2, "public " + strList + " GetList(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "string strTableName = \"[" + TableName + "]\";");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select \" + para.ShowField + \" from \" + strTableName + \"\");");
            strclass.AppendSpaceLine(3, "if (para != null)");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \" + para.Where + \"\");");
            strclass.AppendSpaceLine(3, "if (para.Order != \"\")");
            strclass.AppendSpaceLine(4, "strSql.Append(\" order by \" + para.Order + \"\");");
            strclass.AppendSpaceLine(3, "List<Shop.Model." + TableName + "> list = new List<Shop.Model." + TableName + ">();");
            strclass.AppendSpaceLine(3, "using (IDataReader dataReader = " + AccessHelperName + ".DataReader(strSql.ToString(), para.Para_Oledb))");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(3, "   if(dataReader!=null)");
            strclass.AppendSpaceLine(3, "   {");
            strclass.AppendSpaceLine(3, "       while (dataReader.Read())");
            strclass.AppendSpaceLine(3, "       {");
            strclass.AppendSpaceLine(3, "           list.Add(ReaderBind(dataReader));");
            strclass.AppendSpaceLine(3, "       }");
            strclass.AppendSpaceLine(3, "   }");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return list;");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        /// <summary>
        /// 生成sql语句中的参数列表(例如：用于Add  Exists  Update Delete  GetModel 的参数传入)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetPreParameter_access(List<ColumnInfo> keys)
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendSpaceLine(3, "OleDbParameter[] parameters = {");
            int n = 0;
            foreach (ColumnInfo key in keys)
            {
                strclass.AppendSpaceLine(5, "new OleDbParameter(\"@" + key.ColumnName + "\", " + key.ColumnName + "),");
                //strclass2.AppendSpaceLine(3, "parameters[" + n.ToString() + "].Value = " + key.ColumnName + ";");
                n++;
            }
            strclass.DelLastComma();
            strclass.AppendLine("};");
            strclass.Append(strclass2.Value);
            return strclass.Value;
        }

        #endregion
        #region CSToProcType
        /// <summary>
        /// 企业库数据库字段对应
        /// </summary>
        /// <param name="cstype"></param>
        /// <returns></returns>
        private static string CSToProcType(string cstype)
        {
            string ProcType = cstype;
            switch (cstype.Trim().ToLower())
            {

                case "string":
                case "nvarchar":
                case "nchar":
                case "ntext":
                    ProcType = "String";
                    break;
                case "text":
                case "char":
                case "varchar":
                    ProcType = "AnsiString";
                    break;
                case "datetime":
                case "smalldatetime":
                    ProcType = "DateTime";
                    break;
                case "smallint":
                    ProcType = "Int16";
                    break;
                case "tinyint":
                    ProcType = "Byte";
                    break;
                case "int":
                    ProcType = "Int32";
                    break;
                case "bigint":
                case "long":
                    ProcType = "Int64";
                    break;
                case "float":
                    ProcType = "Double";
                    break;
                case "real":
                case "numeric":
                case "decimal":
                    ProcType = "Decimal";
                    break;
                case "money":
                case "smallmoney":
                    ProcType = "Currency";
                    break;
                case "bool":
                case "bit":
                    ProcType = "Boolean";
                    break;
                case "binary":
                case "varbinary":
                    ProcType = "Binary";
                    break;
                case "image":
                    ProcType = "Image";
                    break;
                case "uniqueidentifier":
                    ProcType = "Guid";
                    break;
                case "timestamp":
                    ProcType = "String";
                    break;
                default:
                    ProcType = "String";
                    break;
            }
            return ProcType;
        }

        #endregion


    }
}
