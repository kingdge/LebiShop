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
	/// 数据访问类D_Lebi_Advert。
	/// </summary>
	public partial class D_Lebi_Advert
	{
		static D_Lebi_Advert _Instance;
		public static D_Lebi_Advert Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Advert("Lebi_Advert");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Advert";
		public D_Lebi_Advert(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Advert", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Advert", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Advert" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Advert", 0 , cachestr,seconds);
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
		public int Add(Lebi_Advert model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Height")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Image")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_Codes")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Tatget")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Theme_Advert_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Theme_Advert_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Theme_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("URL")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Width")+")");
			strSql.Append(" values (");
			strSql.Append("@Description,@Height,@Image,@ImageSmall,@Language_Codes,@Language_ids,@Sort,@Tatget,@Theme_Advert_Code,@Theme_Advert_id,@Theme_id,@Time_Add,@Title,@URL,@Width);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Height", model.Height),
					new SqlParameter("@Image", model.Image),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Language_Codes", model.Language_Codes),
					new SqlParameter("@Language_ids", model.Language_ids),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Tatget", model.Tatget),
					new SqlParameter("@Theme_Advert_Code", model.Theme_Advert_Code),
					new SqlParameter("@Theme_Advert_id", model.Theme_Advert_id),
					new SqlParameter("@Theme_id", model.Theme_id),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@URL", model.URL),
					new SqlParameter("@Width", model.Width)};

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
		public void Update(Lebi_Advert model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",Height,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Height")+"= @Height");
			if((","+model.UpdateCols+",").IndexOf(",Image,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Image")+"= @Image");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+"= @ImageSmall");
			if((","+model.UpdateCols+",").IndexOf(",Language_Codes,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_Codes")+"= @Language_Codes");
			if((","+model.UpdateCols+",").IndexOf(",Language_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_ids")+"= @Language_ids");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Tatget,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Tatget")+"= @Tatget");
			if((","+model.UpdateCols+",").IndexOf(",Theme_Advert_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Theme_Advert_Code")+"= @Theme_Advert_Code");
			if((","+model.UpdateCols+",").IndexOf(",Theme_Advert_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Theme_Advert_id")+"= @Theme_Advert_id");
			if((","+model.UpdateCols+",").IndexOf(",Theme_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Theme_id")+"= @Theme_id");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+"= @Title");
			if((","+model.UpdateCols+",").IndexOf(",URL,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("URL")+"= @URL");
			if((","+model.UpdateCols+",").IndexOf(",Width,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Width")+"= @Width");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Height", model.Height),
					new SqlParameter("@Image", model.Image),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Language_Codes", model.Language_Codes),
					new SqlParameter("@Language_ids", model.Language_ids),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Tatget", model.Tatget),
					new SqlParameter("@Theme_Advert_Code", model.Theme_Advert_Code),
					new SqlParameter("@Theme_Advert_id", model.Theme_Advert_id),
					new SqlParameter("@Theme_id", model.Theme_id),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@URL", model.URL),
					new SqlParameter("@Width", model.Width)};
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
		public Lebi_Advert GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Advert;
		   }
		   Lebi_Advert model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Advert",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Advert GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Advert;
		   }
		   Lebi_Advert model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Advert",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Advert GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Advert;
		   }
		   Lebi_Advert model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Advert",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Advert> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Advert>;
		   }
		   List<Lebi_Advert> list = new List<Lebi_Advert>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Advert", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Advert> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Advert>;
		   }
		   List<Lebi_Advert> list = new List<Lebi_Advert>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Advert", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Advert> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Advert>;
		   }
		   List<Lebi_Advert> list = new List<Lebi_Advert>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Advert", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Advert> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Advert>;
		   }
		   List<Lebi_Advert> list = new List<Lebi_Advert>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Advert", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Advert BindForm(Lebi_Advert model)
		{
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["Height"] != null)
				model.Height=LB.Tools.RequestTool.RequestInt("Height",0);
			if (HttpContext.Current.Request["Image"] != null)
				model.Image=LB.Tools.RequestTool.RequestString("Image");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestString("ImageSmall");
			if (HttpContext.Current.Request["Language_Codes"] != null)
				model.Language_Codes=LB.Tools.RequestTool.RequestString("Language_Codes");
			if (HttpContext.Current.Request["Language_ids"] != null)
				model.Language_ids=LB.Tools.RequestTool.RequestString("Language_ids");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Tatget"] != null)
				model.Tatget=LB.Tools.RequestTool.RequestString("Tatget");
			if (HttpContext.Current.Request["Theme_Advert_Code"] != null)
				model.Theme_Advert_Code=LB.Tools.RequestTool.RequestString("Theme_Advert_Code");
			if (HttpContext.Current.Request["Theme_Advert_id"] != null)
				model.Theme_Advert_id=LB.Tools.RequestTool.RequestInt("Theme_Advert_id",0);
			if (HttpContext.Current.Request["Theme_id"] != null)
				model.Theme_id=LB.Tools.RequestTool.RequestInt("Theme_id",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestString("Title");
			if (HttpContext.Current.Request["URL"] != null)
				model.URL=LB.Tools.RequestTool.RequestString("URL");
			if (HttpContext.Current.Request["Width"] != null)
				model.Width=LB.Tools.RequestTool.RequestInt("Width",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Advert SafeBindForm(Lebi_Advert model)
		{
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["Height"] != null)
				model.Height=LB.Tools.RequestTool.RequestInt("Height",0);
			if (HttpContext.Current.Request["Image"] != null)
				model.Image=LB.Tools.RequestTool.RequestSafeString("Image");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestSafeString("ImageSmall");
			if (HttpContext.Current.Request["Language_Codes"] != null)
				model.Language_Codes=LB.Tools.RequestTool.RequestSafeString("Language_Codes");
			if (HttpContext.Current.Request["Language_ids"] != null)
				model.Language_ids=LB.Tools.RequestTool.RequestSafeString("Language_ids");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Tatget"] != null)
				model.Tatget=LB.Tools.RequestTool.RequestSafeString("Tatget");
			if (HttpContext.Current.Request["Theme_Advert_Code"] != null)
				model.Theme_Advert_Code=LB.Tools.RequestTool.RequestSafeString("Theme_Advert_Code");
			if (HttpContext.Current.Request["Theme_Advert_id"] != null)
				model.Theme_Advert_id=LB.Tools.RequestTool.RequestInt("Theme_Advert_id",0);
			if (HttpContext.Current.Request["Theme_id"] != null)
				model.Theme_id=LB.Tools.RequestTool.RequestInt("Theme_id",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestSafeString("Title");
			if (HttpContext.Current.Request["URL"] != null)
				model.URL=LB.Tools.RequestTool.RequestSafeString("URL");
			if (HttpContext.Current.Request["Width"] != null)
				model.Width=LB.Tools.RequestTool.RequestInt("Width",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Advert ReaderBind(IDataReader dataReader)
		{
			Lebi_Advert model=new Lebi_Advert();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Description=dataReader["Description"].ToString();
			ojb = dataReader["Height"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Height= Convert.ToInt32(ojb);
			}
			model.Image=dataReader["Image"].ToString();
			model.ImageSmall=dataReader["ImageSmall"].ToString();
			model.Language_Codes=dataReader["Language_Codes"].ToString();
			model.Language_ids=dataReader["Language_ids"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			model.Tatget=dataReader["Tatget"].ToString();
			model.Theme_Advert_Code=dataReader["Theme_Advert_Code"].ToString();
			ojb = dataReader["Theme_Advert_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Theme_Advert_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Theme_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Theme_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			model.Title=dataReader["Title"].ToString();
			model.URL=dataReader["URL"].ToString();
			ojb = dataReader["Width"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Width= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

