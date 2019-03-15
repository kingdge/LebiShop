using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using LB.Tools;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
namespace LB.DataAccess
{
    /// <summary>
    /// 数据查询参数类
    /// 20121114 by zhangshijia
    /// </summary>
    public class SQLPara
    {
        private Hashtable _ht;
        private string _where = "";
        private string _showField = "*";
        private string _orderField = "";
        private string _md5;
        private LBParameter[] _para;
        private SqlParameter[] _sqlpara;
        private OleDbParameter[] _oledpara;
        private MySqlParameter[] _mysqlpara;
        public SQLPara()
        {
            _ht = new Hashtable();
        }
        public SQLPara(string where, string order, string field)
        {
            //解析where串
            //where = "Name like lbsql{'%" + k + "%'}";
            //where = "id in in_lbsql{'%" + k + "%'}";
            _ht = new Hashtable();
            _showField = field;
            _orderField = order;
            _where = where;
            //处理in 查询
            string[] inArry = RegexTool.GetSimpleRegResultArray(_where, "[Ii][Nn] \\([Ll][Bb][Ss][Qq][Ll]\\{(.*?)\\}\\)");
            for (int i = 0; i < inArry.Length; i++)
            {
                string val = inArry[i];
                val = val.Replace("'", "");
                string cols = "";
                string[] vals = inArry[i].Split(',');
                for (int j = 0; j < vals.Length; j++)
                {
                    string col = "inpara" + i + j;
                    cols = cols + "@" + col + ",";
                    _ht.Add(col, vals[j]);
                }
                _where = RegexTool.ReplaceRegValue(_where, "[Ii][Nn] \\([Ll][Bb][Ss][Qq][Ll]\\{" + inArry[i] + "\\}\\)", "in (" + cols.TrimEnd(',') + ")", 1);
            }
            //处理一般查询
            string[] Arry = RegexTool.GetSimpleRegResultArray(_where, "[Ll][Bb][Ss][Qq][Ll]\\{(.*?)\\}");
            for (int i = 0; i < Arry.Length; i++)
            {
                string val = Arry[i];
                string col = "para" + i;
                val = val.Replace("'", "");
                _where = RegexTool.ReplaceRegValue(_where, "[Ll][Bb][Ss][Qq][Ll]\\{" + Arry[i] + "\\}", "@" + col, 1);
                _ht.Add(col, val);
            }
            CreateSqlPara();

        }
        public void Add(string key, string value)
        {
            _ht.Add(key, value);
            CreateSqlPara();
        }

        /// <summary>
        /// 遍历哈希表，生成SQL参数
        /// </summary>
        /// <returns></returns>
        private void CreateSqlPara()
        {
            int count = _ht.Count;
            if (count < 1)
            {
                return;
            }
            _sqlpara = new SqlParameter[_ht.Count];
            _oledpara = new OleDbParameter[_ht.Count];
            _mysqlpara = new MySqlParameter[_ht.Count];
            _para = new LBParameter[_ht.Count];
            int i = 0;
            foreach (DictionaryEntry de in _ht)
            {
                _md5 += de.Key.ToString() + de.Value.ToString();
                SqlParameter sp = new SqlParameter("@" + de.Key + "", de.Value);
                _sqlpara[i] = sp;
                OleDbParameter op = new OleDbParameter("@" + de.Key + "", de.Value);
                _oledpara[i] = op;
                MySqlParameter mp = new MySqlParameter("@" + de.Key + "", de.Value);
                _mysqlpara[i] = mp;
                LBParameter p = new LBParameter("@" + de.Key + "", de.Value);
                _para[i] = p;
                i++;
            }
        }
        private string GetWhere()
        {
            //不同的数据库需要在此修改代码
            return _where;
        }
        //public SqlParameter[] Para_SQL
        //{
        //    get { return _sqlpara; }
        //}
        public SqlParameter[] Para
        {
            get { return _sqlpara; }
        }
        public OleDbParameter[] Para_Oledb
        {
            get { return _oledpara; }
        }
        public MySqlParameter[] Para_MySQL
        {
            get { return _mysqlpara; }
        }
        public string Where
        {
            get { return GetWhere(); }
            set { _where = value; }
        }
        public string ShowField
        {
            get
            {
                if (_showField == "")
                    return "*";
                return _showField;
            }
            set { _showField = value; }
        }
        public string Order
        {
            get { return _orderField; }
            set { _orderField = value; }
        }
        public string ValueString
        {
            get { return _md5; }
            //get { return Shop.Tools.Utils.MD5(_md5); }
        }
    }

}