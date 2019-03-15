using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;

namespace Shop.Bussiness
{
    public class ColumnInfo
    {
        // Fields
        private bool _cisNull;
        private string _colorder;
        private string _columnName;
        private string _defaultVal;
        private string _deText;
        private bool _isIdentity;
        private bool _ispk;
        private string _length;
        private string _preci;
        private string _scale;
        private string _typeName;
        private string _cstypeName;

        // Methods
        //public ColumnInfo();

        // Properties
        public bool cisNull
        {
            get
            {
                return this._cisNull;
            }
            set
            {
                this._cisNull = value;
            }
        }
        public string Colorder
        {
            get
            {
                return this._colorder;
            }
            set
            {
                this._colorder = value;
            }
        }
        public string ColumnName
        {
            get
            {
                return this._columnName;
            }
            set
            {
                this._columnName = value;
            }
        }
        public string DefaultVal
        {
            get
            {
                return this._defaultVal;
            }
            set
            {
                this._defaultVal = value;
            }
        }
        public string DeText
        {
            get
            {
                return this._deText;
            }
            set
            {
                this._deText = value;
            }
        }
        public bool IsIdentity
        {
            get
            {
                return this._isIdentity;
            }
            set
            {
                this._isIdentity = value;
            }
        }
        public bool IsPK
        {
            get
            {
                return this._ispk;
            }
            set
            {
                this._ispk = value;
            }
        }
        public string Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        public string Preci
        {
            get
            {
                return this._preci;
            }
            set
            {
                this._preci = value;
            }
        }
        public string Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                this._scale = value;
            }
        }
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }
        public string CSTypeName
        {
            get
            {
                //return this._cstypeName;
                return CodeCommon.DbTypeToCS(this._typeName);
            }
            set
            {
                this._cstypeName = value;
            }
        }
    }

    
    public class CodeCommon
    {
        public static List<ColumnInfo> GetColumnInfos(DataTable dt)
        {
            List<ColumnInfo> list = new List<ColumnInfo>();
            if (dt == null)
            {
                return null;
            }
            //ArrayList list2 = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                string str = row["Colorder"].ToString();
                string item = row["ColumnName"].ToString();
                string str3 = row["TypeName"].ToString();
                string str4 = row["IsIdentity"].ToString();
                string str5 = row["IsPK"].ToString();
                string str6 = row["Length"].ToString();
                string str7 = row["Preci"].ToString();
                string str8 = row["Scale"].ToString();
                string str9 = row["cisNull"].ToString();
                string str10 = row["DefaultVal"].ToString();
                string str11 = row["DeText"].ToString();
                ColumnInfo info = new ColumnInfo();
                info.Colorder = str;
                info.ColumnName = item;
                info.TypeName = str3;
                info.IsIdentity = str4.ToLower() == "true";
                info.IsPK = str5.ToLower() == "true";
                info.Length = str6;
                info.Preci = str7;
                info.Scale = str8;
                info.cisNull = str9.ToLower() == "true";
                info.DefaultVal = str10;
                info.DeText = str11;
                
                list.Add(info);

            }
            return list;
        }
        public static string DbTypeToCS(string dbtype)
        {
            string str = "string";
            switch (dbtype)
            {
                case "date":
                    str = "DateTime";
                    break;
                case "datetime":
                    str = "DateTime";
                    break;
                case "smalldatetime":
                    str = "DateTime";
                    break;
                case "smallint":
                    str = "int";
                    break;
                case "single":
                    str = "Single";
                    break;
                case "int":
                    str = "int";
                    break;
                case "number":
                    str = "int";
                    break;
                case "bigint":
                    str = "long";
                    break;
                case "tinyint":
                    str = "int";
                    break;
                case "float":
                    str = "decimal";
                    break;
                case "numeric":
                    str = "decimal";
                    break;
                case "decimal":
                    str = "decimal";
                    break;
                case "money":
                    str = "decimal";
                    break;
                case "smallmoney":
                    str = "decimal";
                    break;
                case "real":
                    str = "decimal";
                    break;
                case "bit":
                    str = "bool";
                    break;
                case "binary":
                    str = "byte[]";
                    break;
                case "varbinary":
                    str = "byte[]";
                    break;
                case "image":
                    str = "byte[]";
                    break;
                case "raw":
                    str = "byte[]";
                    break;
                case "long":
                    str = "byte[]";
                    break;
                case "long raw":
                    str = "byte[]";
                    break;
                case "blob":
                    str = "byte[]";
                    break;
                case "bfile":
                    str = "byte[]";
                    break;
                default:
                    str = "string";
                    break;
            }
            return str;
            /*
            varchar=string
            varchar2=string
            nvarchar=string
            nvarchar2=string
            char=string
            nchar=string
            text=string
            longtext=string
            ntext=string
            string=string
             */



        }

        public static void CreatFile(string Path, string FileName, string txt)
        {
            string PhysicsPath = HttpContext.Current.Server.MapPath(@"~/" + Path);
            if (!Directory.Exists(PhysicsPath))
            {
                Directory.CreateDirectory(PhysicsPath);
            }
            string PhysicsFileName = HttpContext.Current.Server.MapPath(@"~/" + Path + FileName);
            if (System.IO.File.Exists(PhysicsFileName))
            {
                System.IO.File.Delete(PhysicsFileName);
            }
            HtmlEngine.Instance.WriteFile(PhysicsFileName, txt);
        }
        public static string DbTypeLength(string dbtype, string datatype, string Length)
        {
            string str = "";
            string str2 = dbtype;
            if (str2 == null)
            {
                return str;
            }
            //if (!(str2 == "SQL2000") && !(str2 == "SQL2005"))
            //{
            //    if (str2 != "Oracle")
            //    {
            //        if (str2 == "MySQL")
            //        {
            //return DbTypeLengthMySQL(datatype, Length);
            //        }
            //        if (str2 != "OleDb")
            //        {
            //            return str;
            //        }
            //        return DbTypeLengthOleDb(datatype, Length);
            //    }
            //}
            //else
            //{
                return DbTypeLengthSQL(dbtype, datatype, Length);
            //}
            //return DbTypeLengthOra(datatype, Length);
        }
        private static string DbTypeLengthSQL(string dbtype, string datatype, string Length)
        {
            string dataTypeLenVal = GetDataTypeLenVal(datatype, Length);
            if (dataTypeLenVal != "")
            {
                return (CSToProcType(dbtype, datatype) + "," + dataTypeLenVal);
            }
            return CSToProcType(dbtype, datatype);
        }
        public static string GetDataTypeLenVal(string datatype, string Length)
        {
            string str = "";
            try
            {
                switch (datatype.Trim())
                {
                    case "int":
                        return "4";

                    case "char":
                        if (!(Length.Trim() == ""))
                        {
                            return Length;
                        }
                        return "10";

                    case "nchar":
                        str = Length;
                        if (Length.Trim() == "")
                        {
                            str = "10";
                        }
                        return str;

                    case "varchar":
                        str = Length;
                        if (!(Length.Trim() == ""))
                        {
                            if (int.Parse(Length.Trim()) < 1)
                            {
                                str = "";
                            }
                            return str;
                        }
                        return "50";

                    case "nvarchar":
                        str = Length;
                        if (!(Length.Trim() == ""))
                        {
                            if (int.Parse(Length.Trim()) < 1)
                            {
                                str = "";
                            }
                            return str;
                        }
                        return "50";

                    case "varbinary":
                        str = Length;
                        if (!(Length.Trim() == ""))
                        {
                            if (int.Parse(Length.Trim()) < 1)
                            {
                                str = "";
                            }
                            return str;
                        }
                        return "50";

                    case "bit":
                        return "1";

                    case "float":
                    case "numeric":
                    case "decimal":
                    case "money":
                    case "smallmoney":
                    case "binary":
                    case "smallint":
                    case "bigint":
                        return Length;

                    case "image ":
                    case "datetime":
                    case "smalldatetime":
                    case "ntext":
                    case "text":
                        return str;
                }
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            return Length;
        }
        public static string CSToProcType(string DbType, string cstype)
        {
            string str = cstype;
            string str2 = DbType;
            if (str2 == null)
            {
                return str;
            }
            //if (!(str2 == "SQL2000") && !(str2 == "SQL2005"))
            //{
            //    if (str2 != "Oracle")
            //    {
            //        if (str2 == "MySQL")
            //        {
            //            return CSToProcTypeMySQL(cstype);
            //        }
            //        if (str2 != "OleDb")
            //        {
            //            return str;
            //        }
            //        return CSToProcTypeOleDb(cstype);
            //    }
            //}
            //else
            //{
            return CSToProcTypeSQL(cstype);
            //}
            //return CSToProcTypeOra(cstype);
        }
        private static string CSToProcTypeSQL(string cstype)
        {
            string str = cstype;
            switch (cstype)
            {
                case "varchar":
                    str = "VarChar";
                    break;
                case "string":
                    str = "VarChar";
                    break;
                case "nvarchar":
                    str = "NVarChar";
                    break;
                case "char":
                    str = "Char";
                    break;
                case "nchar":
                    str = "NChar";
                    break;
                case "text":
                    str = "Text";
                    break;
                case "ntext":
                    str = "NText";
                    break;
                case "datetime":
                    str = "DateTime";
                    break;
                case "smalldatetime":
                    str = "SmallDateTime";
                    break;
                case "smallint":
                    str = "SmallInt";
                    break;
                case "tinyint":
                    str = "TinyInt";
                    break;
                case "int":
                    str = "Int";
                    break;
                case "bigint":
                    str = "BigInt";
                    break;
                case "float":
                    str = "Float";
                    break;
                case "real":
                    str = "Real";
                    break;
                case "numeric":
                    str = "Decimal";
                    break;
                case "decimal":
                    str = "Decimal";
                    break;
                case "money":
                    str = "Money";
                    break;
                case "smallmoney":
                    str = "SmallMoney";
                    break;
                case "bool":
                    str = "Bit";
                    break;
                case "bit":
                    str = "Bit";
                    break;
                case "binary":
                    str = "Binary";
                    break;
                case "varbinary":
                    str = "VarBinary";
                    break;
                case "image":
                    str = "Image";
                    break;
                case "uniqueidentifier":
                    str = "UniqueIdentifier";
                    break;
                case "timestamp":
                    str = "Timestamp";
                    break;
                default:
                    str = cstype;
                    break;

            }
            return str;
        }

        public static string Space(int num)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                builder.Append("\t");
            }
            return builder.ToString();
        }
        public static ColumnInfo GetPKCOL(List<ColumnInfo> cols) 
        {
            foreach (ColumnInfo field in cols)
            {
                if (field.IsPK)
                    return field;
            }
            return new ColumnInfo();
        }
 

 



















    }
    public class StringPlus
    {
        // Fields
        private StringBuilder str;
        public StringPlus()
        {
            this.str = new StringBuilder();
        }
        // Methods
        public string Append(string Text)
        {
            this.str.Append(Text);
            return this.str.ToString();
        }
        public string AppendLine()
        {
            this.str.Append("\r\n");
            return this.str.ToString();
        }
        public string AppendLine(string Text)
        {
            this.str.Append(Text + "\r\n");
            return this.str.ToString();
        }
        public string AppendSpace(int SpaceNum, string Text)
        {
            this.str.Append(this.Space(SpaceNum));
            this.str.Append(Text);
            return this.str.ToString();
        }
        public string AppendSpaceLine(int SpaceNum, string Text)
        {
            this.str.Append(this.Space(SpaceNum));
            this.str.Append(Text);
            this.str.Append("\r\n");
            return this.str.ToString();
        }
        public string Space(int SpaceNum)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < SpaceNum; i++)
            {
                builder.Append("\t");
            }
            return builder.ToString();
        }
        public void DelLastComma()
        {
            string str = this.str.ToString();
            int length = str.LastIndexOf(",");
            if (length > 0)
            {
                this.str = new StringBuilder();
                this.str.Append(str.Substring(0, length));
            }
        }

        public void DelLastChar(string strchar)
        {
            string str = this.str.ToString();
            int length = str.LastIndexOf(strchar);
            if (length > 0)
            {
                this.str = new StringBuilder();
                this.str.Append(str.Substring(0, length));
            }
        }












        public string Value
        {
            get
            {
                return this.str.ToString();
            }
        }





    }



}
