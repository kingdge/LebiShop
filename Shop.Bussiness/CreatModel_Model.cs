using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Bussiness
{
   public class CreatModel_Model
    {
         #region 公有属性
        protected string _modelname = ""; //model类名
        protected string _namespace = "CZ.Model"; //顶级命名空间名
        /// <summary>
        /// 顶级命名空间名 
        /// </summary>        
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// model类名
        /// </summary>
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }
        #endregion



        #region 生成完整Model类
       /// <summary>
       /// 生成实体
       /// </summary>
       /// <param name="fieldlist"></param>
       /// <param name="path"></param>
       /// <param name="filename"></param>
        public void CreatModel(List<ColumnInfo> fieldlist,string path)
        {
            string str = CreatModelStr(fieldlist);
            CodeCommon.CreatFile(path, _modelname+".cs", str);
        }
        /// <summary>
        /// 生成完整sModel类
        /// </summary>		
        public string CreatModelStr(List<ColumnInfo> fieldlist)
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("using System;");
            strclass.AppendLine("namespace " + _namespace);
            strclass.AppendLine("{");
            strclass.AppendSpaceLine(1, "/// <summary>");
            strclass.AppendSpaceLine(1, "/// 实体类" + _modelname + " 。(属性说明自动提取数据库字段的描述信息)");
            strclass.AppendSpaceLine(1, "/// </summary>");
            strclass.AppendSpaceLine(1, "[Serializable]");
            strclass.AppendSpaceLine(1, "public class " + _modelname);
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendSpaceLine(2, "public " + _modelname + "()");
            strclass.AppendSpaceLine(2, "{}");
            strclass.AppendLine(CreatModelMethod(fieldlist));
            strclass.AppendSpaceLine(1, "}");
            strclass.AppendLine("}");
            strclass.AppendLine("");
            return strclass.Value;
        }
        #endregion

        #region 生成Model属性部分
        /// <summary>
        /// 生成实体类的属性
        /// </summary>
        /// <returns></returns>
        public string CreatModelMethod(List<ColumnInfo> fieldlist)
        {

            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendSpaceLine(2, "#region Model");
            foreach (ColumnInfo field in fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                bool ispk = field.IsPK;
                bool cisnull = field.cisNull;
                string deText = field.DeText;
                columnType = CodeCommon.DbTypeToCS(columnType);
                string isnull = "";
                string devalue = "";
                //if (CodeCommon.isValueType(columnType))
                //{
                //    if ((!IsIdentity) && (!ispk) && (cisnull))
                //    {
                //        isnull = "?";//代表可空类型
                //    }
                //}
                switch (columnType.ToLower())
                {
                    case "int":
                        devalue = "0";
                        break;
                    case "longint":
                        devalue = "0";
                        break;
                    case "string":
                        devalue = "\"\"";
                        break;
                    case "datetime":
                        devalue = "DateTime.Now";
                        break;
                    case "bool":
                        devalue = "false";
                        break;
                    case "decimal":
                        devalue = "0";
                        break;
                }
                
                strclass1.AppendSpaceLine(2, "private " + columnType + isnull + " _" + columnName.ToLower() + "=" + devalue + ";");//私有变量
                strclass2.AppendSpaceLine(2, "/// <summary>");
                strclass2.AppendSpaceLine(2, "/// " + deText);
                strclass2.AppendSpaceLine(2, "/// </summary>");
                strclass2.AppendSpaceLine(2, "public " + columnType + isnull + " " + columnName);//属性
                strclass2.AppendSpaceLine(2, "{");
                strclass2.AppendSpaceLine(3, "set{" + " _" + columnName.ToLower() + "=value;}");
                strclass2.AppendSpaceLine(3, "get{return " + "_" + columnName.ToLower() + ";}");
                strclass2.AppendSpaceLine(2, "}");
            }
            strclass.Append(strclass1.Value);
            strclass.Append(strclass2.Value);
            strclass.AppendSpaceLine(2, "#endregion Model");

            return strclass.Value;
        }

        #endregion
    }
}
