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
	/// 数据访问类D_Lebi_Pro_Type。
	/// </summary>
	public partial class D_Lebi_Pro_Type
	{
		static D_Lebi_Pro_Type _Instance;
		public static D_Lebi_Pro_Type Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Pro_Type("Lebi_Pro_Type");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Pro_Type";
		public D_Lebi_Pro_Type(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Pro_Type", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Pro_Type", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Pro_Type" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Pro_Type", 0 , cachestr,seconds);
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
		public int Add(Lebi_Pro_Type model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageUrl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsIndexShow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUrlrewrite")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Level")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Parentid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty131")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty132")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty133")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("taobaoid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Url")+")");
			strSql.Append(" values (");
			strSql.Append("@ImageSmall,@ImageUrl,@IsDel,@IsIndexShow,@IsShow,@IsUrlrewrite,@Level,@Name,@Parentid,@Path,@ProPerty131,@ProPerty132,@ProPerty133,@ProPerty134,@SEO_Description,@SEO_Keywords,@SEO_Title,@Site_ids,@Sort,@taobaoid,@Url);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@ImageUrl", model.ImageUrl),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsIndexShow", model.IsIndexShow),
					new SqlParameter("@IsShow", model.IsShow),
					new SqlParameter("@IsUrlrewrite", model.IsUrlrewrite),
					new SqlParameter("@Level", model.Level),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Parentid", model.Parentid),
					new SqlParameter("@Path", model.Path),
					new SqlParameter("@ProPerty131", model.ProPerty131),
					new SqlParameter("@ProPerty132", model.ProPerty132),
					new SqlParameter("@ProPerty133", model.ProPerty133),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@Site_ids", model.Site_ids),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@taobaoid", model.taobaoid),
					new SqlParameter("@Url", model.Url)};

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
		public void Update(Lebi_Pro_Type model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+"= @ImageSmall");
			if((","+model.UpdateCols+",").IndexOf(",ImageUrl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageUrl")+"= @ImageUrl");
			if((","+model.UpdateCols+",").IndexOf(",IsDel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+"= @IsDel");
			if((","+model.UpdateCols+",").IndexOf(",IsIndexShow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsIndexShow")+"= @IsIndexShow");
			if((","+model.UpdateCols+",").IndexOf(",IsShow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShow")+"= @IsShow");
			if((","+model.UpdateCols+",").IndexOf(",IsUrlrewrite,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUrlrewrite")+"= @IsUrlrewrite");
			if((","+model.UpdateCols+",").IndexOf(",Level,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Level")+"= @Level");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",Parentid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Parentid")+"= @Parentid");
			if((","+model.UpdateCols+",").IndexOf(",Path,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path")+"= @Path");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty131,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty131")+"= @ProPerty131");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty132,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty132")+"= @ProPerty132");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty133,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty133")+"= @ProPerty133");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty134,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+"= @ProPerty134");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+"= @SEO_Description");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Keywords,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+"= @SEO_Keywords");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+"= @SEO_Title");
			if((","+model.UpdateCols+",").IndexOf(",Site_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_ids")+"= @Site_ids");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",taobaoid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("taobaoid")+"= @taobaoid");
			if((","+model.UpdateCols+",").IndexOf(",Url,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Url")+"= @Url");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@ImageUrl", model.ImageUrl),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsIndexShow", model.IsIndexShow),
					new SqlParameter("@IsShow", model.IsShow),
					new SqlParameter("@IsUrlrewrite", model.IsUrlrewrite),
					new SqlParameter("@Level", model.Level),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Parentid", model.Parentid),
					new SqlParameter("@Path", model.Path),
					new SqlParameter("@ProPerty131", model.ProPerty131),
					new SqlParameter("@ProPerty132", model.ProPerty132),
					new SqlParameter("@ProPerty133", model.ProPerty133),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@Site_ids", model.Site_ids),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@taobaoid", model.taobaoid),
					new SqlParameter("@Url", model.Url)};
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
		public Lebi_Pro_Type GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Pro_Type;
		   }
		   Lebi_Pro_Type model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Pro_Type",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Pro_Type GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Pro_Type;
		   }
		   Lebi_Pro_Type model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Pro_Type",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Pro_Type GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Pro_Type;
		   }
		   Lebi_Pro_Type model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Pro_Type",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Pro_Type> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Pro_Type>;
		   }
		   List<Lebi_Pro_Type> list = new List<Lebi_Pro_Type>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Pro_Type", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Pro_Type> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Pro_Type>;
		   }
		   List<Lebi_Pro_Type> list = new List<Lebi_Pro_Type>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Pro_Type", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Pro_Type> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Pro_Type>;
		   }
		   List<Lebi_Pro_Type> list = new List<Lebi_Pro_Type>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Pro_Type", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Pro_Type> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Pro_Type>;
		   }
		   List<Lebi_Pro_Type> list = new List<Lebi_Pro_Type>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Pro_Type", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Pro_Type BindForm(Lebi_Pro_Type model)
		{
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestString("ImageSmall");
			if (HttpContext.Current.Request["ImageUrl"] != null)
				model.ImageUrl=LB.Tools.RequestTool.RequestString("ImageUrl");
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsIndexShow"] != null)
				model.IsIndexShow=LB.Tools.RequestTool.RequestInt("IsIndexShow",0);
			if (HttpContext.Current.Request["IsShow"] != null)
				model.IsShow=LB.Tools.RequestTool.RequestInt("IsShow",0);
			if (HttpContext.Current.Request["IsUrlrewrite"] != null)
				model.IsUrlrewrite=LB.Tools.RequestTool.RequestString("IsUrlrewrite");
			if (HttpContext.Current.Request["Level"] != null)
				model.Level=LB.Tools.RequestTool.RequestInt("Level",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["Parentid"] != null)
				model.Parentid=LB.Tools.RequestTool.RequestInt("Parentid",0);
			if (HttpContext.Current.Request["Path"] != null)
				model.Path=LB.Tools.RequestTool.RequestString("Path");
			if (HttpContext.Current.Request["ProPerty131"] != null)
				model.ProPerty131=LB.Tools.RequestTool.RequestString("ProPerty131");
			if (HttpContext.Current.Request["ProPerty132"] != null)
				model.ProPerty132=LB.Tools.RequestTool.RequestString("ProPerty132");
			if (HttpContext.Current.Request["ProPerty133"] != null)
				model.ProPerty133=LB.Tools.RequestTool.RequestString("ProPerty133");
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestString("ProPerty134");
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestString("SEO_Title");
			if (HttpContext.Current.Request["Site_ids"] != null)
				model.Site_ids=LB.Tools.RequestTool.RequestString("Site_ids");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["taobaoid"] != null)
				model.taobaoid=LB.Tools.RequestTool.RequestString("taobaoid");
			if (HttpContext.Current.Request["Url"] != null)
				model.Url=LB.Tools.RequestTool.RequestString("Url");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Pro_Type SafeBindForm(Lebi_Pro_Type model)
		{
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestSafeString("ImageSmall");
			if (HttpContext.Current.Request["ImageUrl"] != null)
				model.ImageUrl=LB.Tools.RequestTool.RequestSafeString("ImageUrl");
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsIndexShow"] != null)
				model.IsIndexShow=LB.Tools.RequestTool.RequestInt("IsIndexShow",0);
			if (HttpContext.Current.Request["IsShow"] != null)
				model.IsShow=LB.Tools.RequestTool.RequestInt("IsShow",0);
			if (HttpContext.Current.Request["IsUrlrewrite"] != null)
				model.IsUrlrewrite=LB.Tools.RequestTool.RequestSafeString("IsUrlrewrite");
			if (HttpContext.Current.Request["Level"] != null)
				model.Level=LB.Tools.RequestTool.RequestInt("Level",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["Parentid"] != null)
				model.Parentid=LB.Tools.RequestTool.RequestInt("Parentid",0);
			if (HttpContext.Current.Request["Path"] != null)
				model.Path=LB.Tools.RequestTool.RequestSafeString("Path");
			if (HttpContext.Current.Request["ProPerty131"] != null)
				model.ProPerty131=LB.Tools.RequestTool.RequestSafeString("ProPerty131");
			if (HttpContext.Current.Request["ProPerty132"] != null)
				model.ProPerty132=LB.Tools.RequestTool.RequestSafeString("ProPerty132");
			if (HttpContext.Current.Request["ProPerty133"] != null)
				model.ProPerty133=LB.Tools.RequestTool.RequestSafeString("ProPerty133");
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestSafeString("ProPerty134");
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestSafeString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestSafeString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestSafeString("SEO_Title");
			if (HttpContext.Current.Request["Site_ids"] != null)
				model.Site_ids=LB.Tools.RequestTool.RequestSafeString("Site_ids");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["taobaoid"] != null)
				model.taobaoid=LB.Tools.RequestTool.RequestSafeString("taobaoid");
			if (HttpContext.Current.Request["Url"] != null)
				model.Url=LB.Tools.RequestTool.RequestSafeString("Url");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Pro_Type ReaderBind(IDataReader dataReader)
		{
			Lebi_Pro_Type model=new Lebi_Pro_Type();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.ImageSmall=dataReader["ImageSmall"].ToString();
			model.ImageUrl=dataReader["ImageUrl"].ToString();
			ojb = dataReader["IsDel"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsDel= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsIndexShow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsIndexShow= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsShow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsShow= Convert.ToInt32(ojb);
			}
			model.IsUrlrewrite=dataReader["IsUrlrewrite"].ToString();
			ojb = dataReader["Level"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Level= Convert.ToInt32(ojb);
			}
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["Parentid"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Parentid= Convert.ToInt32(ojb);
			}
			model.Path=dataReader["Path"].ToString();
			model.ProPerty131=dataReader["ProPerty131"].ToString();
			model.ProPerty132=dataReader["ProPerty132"].ToString();
			model.ProPerty133=dataReader["ProPerty133"].ToString();
			model.ProPerty134=dataReader["ProPerty134"].ToString();
			model.SEO_Description=dataReader["SEO_Description"].ToString();
			model.SEO_Keywords=dataReader["SEO_Keywords"].ToString();
			model.SEO_Title=dataReader["SEO_Title"].ToString();
			model.Site_ids=dataReader["Site_ids"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			model.taobaoid=dataReader["taobaoid"].ToString();
			model.Url=dataReader["Url"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

