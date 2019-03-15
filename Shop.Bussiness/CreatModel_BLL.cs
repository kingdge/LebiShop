using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Bussiness
{
    public class CreatModel_BLL
    {

        #region 公有属性
        private List<ColumnInfo> _columninfos; //全部字段
        private List<ColumnInfo> _keys = new List<ColumnInfo>();      //关键字
        private string _bllspace; //命名空间名 
        private string _bllname;//model类名 

        private string _modelname;//model类名    
        private string _modelspace;//dal类名

        private string _dalspace;
        private string _dalname;

        private bool isHasIdentity;


        /// <summary>
        /// 选择的字段集合
        /// </summary>
        public List<ColumnInfo> ColumnInfos
        {
            set { _columninfos = value; }
            get { return _columninfos; }
        }
        /// <summary>
        /// 主键或条件字段列表 
        /// </summary>
        public List<ColumnInfo> Keys
        {
            set { _keys = value; }
            get
            {
                _keys.Clear();
                foreach (ColumnInfo key in ColumnInfos)
                {
                    if (key.IsPK)
                        _keys.Add(key);
                }
                return _keys;
            }
        }

        /// <summary>
        /// 命名空间名
        /// </summary>
        public string BLLSpace
        {
            set { _bllspace = value; }
            get { return _bllspace; }
        }
        /// <summary>
        /// 类名称
        /// </summary>
        public string BLLName
        {
            set { _bllname = value; }
            get { return _bllname; }
        }
        /// <summary>
        /// Model类名
        /// </summary>
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }
        /// <summary>
        /// 实体类的命名空间
        /// </summary>
        public string ModelSpace
        {
            get { return _modelspace; }
            set { _modelspace = value; }
        }

        /*============================*/

        /// <summary>
        /// DAL类名
        /// </summary>
        public string DALName
        {
            set { _dalname = value; }
            get { return _dalname; }
        }

        /*============================*/
        /// <summary>
        /// DAL的命名空间
        /// </summary>
        public string DALSpace
        {
            set { _dalspace = value; }
            get { return _dalspace; }
        }
        /*============================*/

        /// <summary>
        /// 是否有自动增长标识列
        /// </summary>
        public bool IsHasIdentity
        {
            set { isHasIdentity = value; }
            get { return isHasIdentity; }
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
        private string KeysNullTip
        {
            get
            {

                if (Keys.Count == 0)
                {
                    return "//该表无主键信息，请自定义主键/条件字段";
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region  构造函数
        public CreatModel_BLL()
        {
        }

        #endregion

        #region 业务层方法
        public void CreatBLL(string path)
        {
            string str = GetBLLCodeStr();
            CodeCommon.CreatFile(path, BLLName + ".cs", str);
        }
        /// <summary>
        /// 得到整个类的代码
        /// </summary>      
        public string GetBLLCodeStr()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("using System;");
            strclass.AppendLine("using System.Data;");
            strclass.AppendLine("using System.Collections.Generic;");
            strclass.AppendLine("using System.Text.RegularExpressions;");
            strclass.AppendLine("using " + ModelSpace + ";");
            strclass.AppendLine("using " + DALSpace + ";");

            strclass.AppendLine("namespace " + BLLSpace + "");
            strclass.AppendLine("{");
            strclass.AppendSpaceLine(1, "/// <summary>");
            strclass.AppendSpaceLine(1, "/// 业务逻辑类" + BLLName + " 的摘要说明。");
            strclass.AppendSpaceLine(1, "/// </summary>");
            strclass.AppendSpaceLine(1, "public class " + BLLName);
            strclass.AppendSpaceLine(1, "{");

            strclass.AppendSpaceLine(2, "public " + BLLName + "()");
            strclass.AppendSpaceLine(2, "{}");


            strclass.AppendSpaceLine(2, "#region  成员方法");

            #region  方法代码

            strclass.AppendLine(CreatBLLGetVaule());
            strclass.AppendLine(CreatBLLGetCounts());
            strclass.AppendLine(CreatBLLADD());
            strclass.AppendLine(CreatBLLUpdate());
            strclass.AppendLine(CreatBLLDelete());
            strclass.AppendLine(CreatBLLGetModel());
            strclass.AppendLine(CreatBLLGetMaxID());
            strclass.AppendLine(CreatBLLGetList());
            strclass.AppendLine(CreatBLLBindForm());


            #endregion
            strclass.AppendSpaceLine(2, "#endregion  成员方法");
            strclass.AppendSpaceLine(1, "}");
            strclass.AppendLine("}");
            strclass.AppendLine("");

            return strclass.Value;
        }

        #endregion

        #region 具体方法代码
        public string CreatBLLGetVaule()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 返回单个字符串");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static string GetValue(string col,string where)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetValue(col,where);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        public string CreatBLLGetCounts()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 返回记录条数");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static int Counts(string where)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.Counts(where);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public static int Counts(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.Counts(para);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        public string CreatBLLGetMaxID()
        {
            StringPlus strclass = new StringPlus();
            if (_keys.Count > 0)
            {
                string keyname = "";
                foreach (ColumnInfo obj in _keys)
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
                            strclass.AppendSpaceLine(2, "public static int GetMaxId()");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetMaxID(\"\");");
                            strclass.AppendSpaceLine(2, "}");
                            strclass.AppendSpaceLine(2, "public static int GetMaxId(SQLPara para)");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetMaxID(para);");
                            strclass.AppendSpaceLine(2, "}");
                            strclass.AppendSpaceLine(2, "public static int GetMaxId(string strWhere)");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetMaxID(strWhere);");
                            strclass.AppendSpaceLine(2, "}");
                            break;
                        }
                    }
                }
            }


            return strclass.Value;
        }

        public string CreatBLLADD()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 增加一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            string strretu = "int";

            strclass.AppendSpaceLine(2, "public static " + strretu + " Add(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");

            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.Add(model);");

            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        public string CreatBLLUpdate()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 更新一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static void Update(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "" + DALName + ".Instance.Update(model);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }
        public string CreatBLLDelete()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除一条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static void Delete(" + Key.CSTypeName + " " + Key.ColumnName + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "" + DALName + ".Instance.Delete(" + Key.ColumnName + ");");
            strclass.AppendSpaceLine(2, "}");

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除多条数据  by where条件");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static void Delete(string where)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "" + DALName + ".Instance.Delete(where);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 删除多条数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static void Delete(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "" + DALName + ".Instance.Delete(para);");
            strclass.AppendSpaceLine(2, "}");

            return strclass.Value;
        }
        public string CreatBLLGetModel()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static " + ModelName + " GetModel(" + Key.CSTypeName + " " + Key.ColumnName + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetModel(" + Key.ColumnName + ");");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 得到一个对象实体 by where条件");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static " + ModelName + " GetModel(string where)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetModel(where);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public static " + ModelName + " GetModel(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetModel(para);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;

        }
        public string CreatBLLBindForm()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 绑定表单数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static " + ModelName + " BindForm(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.BindForm(model);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 安全方式绑定表单数据");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static " + ModelName + " SafeBindForm(" + ModelName + " model)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.SafeBindForm(model);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;

        }
        public string CreatBLLGetList()
        {
            StringPlus strclass = new StringPlus();
            //返回DataSet
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// 获得数据列表");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public static List<" + ModelName + "> GetList(string strWhere,string strFieldOrder)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetList(strWhere,strFieldOrder);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public static List<" + ModelName + "> GetList(SQLPara para)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetList(para);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public static List<" + ModelName + "> GetList(string strWhere, string strFieldOrder, int PageSize, int page)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetList(strWhere,strFieldOrder,PageSize,page);");
            strclass.AppendSpaceLine(2, "}");
            strclass.AppendSpaceLine(2, "public static List<" + ModelName + "> GetList(SQLPara para, int PageSize, int page)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "return " + DALName + ".Instance.GetList(para,PageSize,page);");
            strclass.AppendSpaceLine(2, "}");


            return strclass.Value;

        }

        #endregion


    }
}
