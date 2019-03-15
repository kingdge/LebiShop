using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Web;
using LB.DataAccess;
namespace DB.LebiShop
{
	/// <summary>
	/// 数据访问类D_Lebi_Table。
	/// </summary>
	public partial class D_Lebi_Table
	{
		static D_Lebi_Table _Instance;
		public static D_Lebi_Table Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Table("Lebi_Table");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Table";
		public D_Lebi_Table(string tablename)
		{
		    TableName = tablename;
		}

		#region  成员方法
		/// <summary>
		/// 根据字段名，where条件获取一个值,返回字符串
		/// </summary>
		public string GetValue(string colName,string strWhere,int seconds=0)
		{
		   string val = "";
		   try
		   {
		       StringBuilder strSql=new StringBuilder();
		       strSql.Append("select " + colName + " from "+ TableName + "");
		       if(strWhere.Trim()!="")
		       {
		           strSql.Append(" where "+strWhere);
		       }
		       string cachestr = "";
		       string cachekey = "";
		       if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		       {
		           cachestr = strSql.ToString() + "|" + seconds;
		           cachekey = LB.Tools.Utils.MD5(cachestr);
		           var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		           if (obj != null)
		               return obj.ToString();
		       }
		       val = Convert.ToString(LB.DataAccess.DB.Instance.TextExecute(strSql.ToString()));
		       if (cachekey != "")
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Table", 0 , cachestr , seconds);
		   }
		   catch
		   {
		       val = "";
		   }
		   return val;
		}
		public string GetValue(string colName,SQLPara para, int seconds=0)
		{
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select  "+colName+" from "+ TableName + "");
		   if(para.Where!="")
		       strSql.Append(" where "+para.Where);
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strSql.ToString() + "|" + para.ValueString + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj.ToString();
		   }
		   string val = "";
		   val = Convert.ToString( LB.DataAccess.DB.Instance.TextExecute(strSql.ToString(), para.Para)); 
		   if (cachekey != "")
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Table", 0 , cachestr , seconds);
		   return val;
		}

		/// <summary>
		/// 计算记录条数
		/// </summary>
		public int Counts(string strWhere, int seconds=0)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, "", "");
		       return Counts(para, seconds);
		   }
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select count(1) from "+ TableName + "");
		   if(strWhere.Trim()!="")
		       strSql.Append(" where "+strWhere);
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strSql.ToString() + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return Convert.ToInt32(obj);
		   }
		   int val = 0;
		   val= Convert.ToInt32( LB.DataAccess.DB.Instance.TextExecute(strSql.ToString())); 
		   if (cachekey != "")
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Table" , 0 , cachestr , seconds);
		   return val;
		}
		public int Counts(SQLPara para, int seconds=0)
		{
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select count(1) from "+ TableName + "");
		   if(para.Where!="")
		       strSql.Append(" where "+para.Where);
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strSql.ToString() + "|" + para.ValueString + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return Convert.ToInt32(obj);
		   }
		   int val = 0;
		   val = Convert.ToInt32( LB.DataAccess.DB.Instance.TextExecute(strSql.ToString(), para.Para)); 
		   if (cachekey != "")
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Table", 0 , cachestr,seconds);
		   return val;
		}


		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxID(string strWhere)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, "", "");
		       return GetMaxID(para);
		   }
		   StringBuilder strSql = new StringBuilder();
		   strSql.Append("select max(id) from "+ TableName + "");
		   if(strWhere.Trim()!="")
		       strSql.Append(" where "+strWhere);
		   return Convert.ToInt32(LB.DataAccess.DB.Instance.TextExecute(strSql.ToString()));
		}
		public int GetMaxID(SQLPara para)
		{
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select count(1) "+ TableName + "");
		   if(para.Where!="")
		       strSql.Append(" where "+para.Where);
		  return Convert.ToInt32( LB.DataAccess.DB.Instance.TextExecute(strSql.ToString(), para.Para)); 
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Lebi_Table model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("char_length")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("defaultval")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("isidentity")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("isnullable")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ispk")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("numeric_length")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("numeric_scale")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("parentid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("parentname")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("type")+")");
			strSql.Append(" values (");
			strSql.Append("@char_length,@defaultval,@isidentity,@isnullable,@ispk,@name,@numeric_length,@numeric_scale,@parentid,@parentname,@remark,@type);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@char_length", model.char_length),
					new SqlParameter("@defaultval", model.defaultval),
					new SqlParameter("@isidentity", model.isidentity),
					new SqlParameter("@isnullable", model.isnullable),
					new SqlParameter("@ispk", model.ispk),
					new SqlParameter("@name", model.name),
					new SqlParameter("@numeric_length", model.numeric_length),
					new SqlParameter("@numeric_scale", model.numeric_scale),
					new SqlParameter("@parentid", model.parentid),
					new SqlParameter("@parentname", model.parentname),
					new SqlParameter("@remark", model.remark),
					new SqlParameter("@type", model.type)};

			object obj = LB.DataAccess.DB.Instance.TextExecute(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Lebi_Table model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",char_length,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("char_length")+"= @char_length");
			if((","+model.UpdateCols+",").IndexOf(",defaultval,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("defaultval")+"= @defaultval");
			if((","+model.UpdateCols+",").IndexOf(",isidentity,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("isidentity")+"= @isidentity");
			if((","+model.UpdateCols+",").IndexOf(",isnullable,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("isnullable")+"= @isnullable");
			if((","+model.UpdateCols+",").IndexOf(",ispk,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ispk")+"= @ispk");
			if((","+model.UpdateCols+",").IndexOf(",name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("name")+"= @name");
			if((","+model.UpdateCols+",").IndexOf(",numeric_length,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("numeric_length")+"= @numeric_length");
			if((","+model.UpdateCols+",").IndexOf(",numeric_scale,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("numeric_scale")+"= @numeric_scale");
			if((","+model.UpdateCols+",").IndexOf(",parentid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("parentid")+"= @parentid");
			if((","+model.UpdateCols+",").IndexOf(",parentname,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("parentname")+"= @parentname");
			if((","+model.UpdateCols+",").IndexOf(",remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("remark")+"= @remark");
			if((","+model.UpdateCols+",").IndexOf(",type,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("type")+"= @type");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@char_length", model.char_length),
					new SqlParameter("@defaultval", model.defaultval),
					new SqlParameter("@isidentity", model.isidentity),
					new SqlParameter("@isnullable", model.isnullable),
					new SqlParameter("@ispk", model.ispk),
					new SqlParameter("@name", model.name),
					new SqlParameter("@numeric_length", model.numeric_length),
					new SqlParameter("@numeric_scale", model.numeric_scale),
					new SqlParameter("@parentid", model.parentid),
					new SqlParameter("@parentname", model.parentname),
					new SqlParameter("@remark", model.remark),
					new SqlParameter("@type", model.type)};
			LB.DataAccess.DB.Instance.TextExecuteNonQuery(strSql.ToString().Replace(", where id=@id", " where id=@id"),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("delete from "+ TableName + " ");
		   strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", id)};

		LB.DataAccess.DB.Instance.TextExecuteNonQuery(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 删除多条数据  by where条件
		/// </summary>
		public void Delete(string strWhere)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, "", "");
		       Delete(para);
		       return;
		   }
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("delete from "+ TableName + " ");
		   strSql.Append(" where "+ strWhere +"");
		   LB.DataAccess.DB.Instance.TextExecuteNonQuery(strSql.ToString());
		}
		public void Delete(SQLPara para)
		{
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("delete from "+ TableName + " ");
		   if (para.Where != "")
		   strSql.Append(" where "+ para.Where +"");
		   LB.DataAccess.DB.Instance.TextExecuteNonQuery(strSql.ToString(),para.Para);
		}


		/// <summary>
		/// 得到一个对象实体 by id
		/// </summary>
		public Lebi_Table GetModel(int id, int seconds=0)
		{
		   string strTableName = TableName;
		   string strFieldShow = "*";
		   string strWhere = "id="+id;
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = "select * "+ TableName + " where id="+id+"|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as Lebi_Table;
		   }
		   Lebi_Table model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Table",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Table GetModel(string strWhere, int seconds=0)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, "", "");
		           return GetModel(para, seconds);
		   }
		   string strTableName =TableName;
		   string strFieldShow = "*";
		   string cachekey = "";
		   string cachestr = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = "select * "+ TableName + " where "+strWhere+"|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as Lebi_Table;
		   }
		   Lebi_Table model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Table",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Table GetModel(SQLPara para, int seconds=0)
		{
		   string strTableName = TableName;
		   string strFieldShow = "*";
		   string strWhere = para.Where;
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = "select * "+ TableName + " where "+para.Where+"|"+para.ValueString+"|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as Lebi_Table;
		   }
		   Lebi_Table model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Table",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Table> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, strFieldOrder, "");
		       return GetList(para,PageSize,page,seconds);
		   }
		   string strTableName = TableName;
		   string strFieldKey = "id";
		   string strFieldShow = "*";
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strTableName + "|" + strFieldOrder + "|" + strWhere + "|" + PageSize + "|" + page + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as List<Lebi_Table>;
		   }
		   List<Lebi_Table> list = new List<Lebi_Table>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strTableName, strFieldKey, strFieldShow, strFieldOrder, strWhere, PageSize, page))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Table", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Table> GetList(SQLPara para, int PageSize, int page, int seconds=0)
		{
		   string strTableName = TableName;
		   string strFieldKey = "id";
		   string strFieldShow = "*";
		   string strFieldOrder = para.Order;
		   string strWhere = para.Where;
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strTableName + "|" + strFieldOrder + "|" + strWhere + "|" + PageSize + "|" + page + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as List<Lebi_Table>;
		   }
		   List<Lebi_Table> list = new List<Lebi_Table>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strTableName, strFieldKey, strFieldShow, strFieldOrder, strWhere, PageSize, page,para.Para))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Table", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Table> GetList(string strWhere,string strFieldOrder, int seconds=0)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, strFieldOrder, "");
		       return GetList(para, seconds);
		   }
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select * from "+ TableName + " ");
		   if(strWhere.Trim()!="")
		   {
		       strSql.Append(" where "+strWhere);
		   }
		   if(strFieldOrder.Trim()!="")
		   {
		       strSql.Append(" order by "+strFieldOrder);
		   }
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strSql.ToString() + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as List<Lebi_Table>;
		   }
		   List<Lebi_Table> list = new List<Lebi_Table>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strSql.ToString()))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Table", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Table> GetList(SQLPara para, int seconds=0)
		{
		   string strTableName = TableName;
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select " + para.ShowField + " from " + strTableName + "");
		   if (para != null)
		       strSql.Append(" where " + para.Where + "");
		   if (para.Order != "")
		       strSql.Append(" order by " + para.Order + "");
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strSql.ToString() + "|" + para.ValueString + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as List<Lebi_Table>;
		   }
		   List<Lebi_Table> list = new List<Lebi_Table>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strSql.ToString(), para.Para))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Table", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Table BindForm(Lebi_Table model)
		{
			if (HttpContext.Current.Request["char_length"] != null)
				model.char_length=LB.Tools.RequestTool.RequestInt("char_length",0);
			if (HttpContext.Current.Request["defaultval"] != null)
				model.defaultval=LB.Tools.RequestTool.RequestString("defaultval");
			if (HttpContext.Current.Request["isidentity"] != null)
				model.isidentity=LB.Tools.RequestTool.RequestInt("isidentity",0);
			if (HttpContext.Current.Request["isnullable"] != null)
				model.isnullable=LB.Tools.RequestTool.RequestInt("isnullable",0);
			if (HttpContext.Current.Request["ispk"] != null)
				model.ispk=LB.Tools.RequestTool.RequestInt("ispk",0);
			if (HttpContext.Current.Request["name"] != null)
				model.name=LB.Tools.RequestTool.RequestString("name");
			if (HttpContext.Current.Request["numeric_length"] != null)
				model.numeric_length=LB.Tools.RequestTool.RequestInt("numeric_length",0);
			if (HttpContext.Current.Request["numeric_scale"] != null)
				model.numeric_scale=LB.Tools.RequestTool.RequestInt("numeric_scale",0);
			if (HttpContext.Current.Request["parentid"] != null)
				model.parentid=LB.Tools.RequestTool.RequestInt("parentid",0);
			if (HttpContext.Current.Request["parentname"] != null)
				model.parentname=LB.Tools.RequestTool.RequestString("parentname");
			if (HttpContext.Current.Request["remark"] != null)
				model.remark=LB.Tools.RequestTool.RequestString("remark");
			if (HttpContext.Current.Request["type"] != null)
				model.type=LB.Tools.RequestTool.RequestString("type");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Table SafeBindForm(Lebi_Table model)
		{
			if (HttpContext.Current.Request["char_length"] != null)
				model.char_length=LB.Tools.RequestTool.RequestInt("char_length",0);
			if (HttpContext.Current.Request["defaultval"] != null)
				model.defaultval=LB.Tools.RequestTool.RequestSafeString("defaultval");
			if (HttpContext.Current.Request["isidentity"] != null)
				model.isidentity=LB.Tools.RequestTool.RequestInt("isidentity",0);
			if (HttpContext.Current.Request["isnullable"] != null)
				model.isnullable=LB.Tools.RequestTool.RequestInt("isnullable",0);
			if (HttpContext.Current.Request["ispk"] != null)
				model.ispk=LB.Tools.RequestTool.RequestInt("ispk",0);
			if (HttpContext.Current.Request["name"] != null)
				model.name=LB.Tools.RequestTool.RequestSafeString("name");
			if (HttpContext.Current.Request["numeric_length"] != null)
				model.numeric_length=LB.Tools.RequestTool.RequestInt("numeric_length",0);
			if (HttpContext.Current.Request["numeric_scale"] != null)
				model.numeric_scale=LB.Tools.RequestTool.RequestInt("numeric_scale",0);
			if (HttpContext.Current.Request["parentid"] != null)
				model.parentid=LB.Tools.RequestTool.RequestInt("parentid",0);
			if (HttpContext.Current.Request["parentname"] != null)
				model.parentname=LB.Tools.RequestTool.RequestSafeString("parentname");
			if (HttpContext.Current.Request["remark"] != null)
				model.remark=LB.Tools.RequestTool.RequestSafeString("remark");
			if (HttpContext.Current.Request["type"] != null)
				model.type=LB.Tools.RequestTool.RequestSafeString("type");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Table ReaderBind(IDataReader dataReader)
		{
			Lebi_Table model=new Lebi_Table();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["char_length"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.char_length= Convert.ToInt32(ojb);
			}
			model.defaultval=dataReader["defaultval"].ToString();
			ojb = dataReader["isidentity"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.isidentity= Convert.ToInt32(ojb);
			}
			ojb = dataReader["isnullable"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.isnullable= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ispk"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ispk= Convert.ToInt32(ojb);
			}
			model.name=dataReader["name"].ToString();
			ojb = dataReader["numeric_length"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.numeric_length= Convert.ToInt32(ojb);
			}
			ojb = dataReader["numeric_scale"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.numeric_scale= Convert.ToInt32(ojb);
			}
			ojb = dataReader["parentid"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.parentid= Convert.ToInt32(ojb);
			}
			model.parentname=dataReader["parentname"].ToString();
			model.remark=dataReader["remark"].ToString();
			model.type=dataReader["type"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

