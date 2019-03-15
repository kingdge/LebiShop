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
	/// 数据访问类D_Lebi_Page。
	/// </summary>
	public partial class D_Lebi_Page
	{
		static D_Lebi_Page _Instance;
		public static D_Lebi_Page Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Page("Lebi_Page");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Page";
		public D_Lebi_Page(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Page", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Page", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Page" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Page", 0 , cachestr,seconds);
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
		public int Add(Lebi_Page model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("admin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Author")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Comment")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Views")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Editor")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NameColor")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Node_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("source")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("sourceurl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SubName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("target")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("url")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("user_id")+")");
			strSql.Append(" values (");
			strSql.Append("@admin_id,@Author,@Content,@Count_Comment,@Count_Views,@Description,@Editor,@Email,@ImageBig,@ImageMedium,@ImageOriginal,@ImageSmall,@Language,@Language_ids,@Name,@NameColor,@Node_id,@SEO_Description,@SEO_Keywords,@SEO_Title,@Sort,@source,@sourceurl,@SubName,@Supplier_id,@target,@Time_Add,@Time_Update,@url,@user_id);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@admin_id", model.admin_id),
					new SqlParameter("@Author", model.Author),
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@Count_Comment", model.Count_Comment),
					new SqlParameter("@Count_Views", model.Count_Views),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Editor", model.Editor),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Language_ids", model.Language_ids),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@NameColor", model.NameColor),
					new SqlParameter("@Node_id", model.Node_id),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@source", model.source),
					new SqlParameter("@sourceurl", model.sourceurl),
					new SqlParameter("@SubName", model.SubName),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@target", model.target),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@url", model.url),
					new SqlParameter("@user_id", model.user_id)};

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
		public void Update(Lebi_Page model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",admin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("admin_id")+"= @admin_id");
			if((","+model.UpdateCols+",").IndexOf(",Author,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Author")+"= @Author");
			if((","+model.UpdateCols+",").IndexOf(",Content,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+"= @Content");
			if((","+model.UpdateCols+",").IndexOf(",Count_Comment,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Comment")+"= @Count_Comment");
			if((","+model.UpdateCols+",").IndexOf(",Count_Views,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Views")+"= @Count_Views");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",Editor,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Editor")+"= @Editor");
			if((","+model.UpdateCols+",").IndexOf(",Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if((","+model.UpdateCols+",").IndexOf(",ImageBig,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+"= @ImageBig");
			if((","+model.UpdateCols+",").IndexOf(",ImageMedium,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+"= @ImageMedium");
			if((","+model.UpdateCols+",").IndexOf(",ImageOriginal,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+"= @ImageOriginal");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+"= @ImageSmall");
			if((","+model.UpdateCols+",").IndexOf(",Language,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+"= @Language");
			if((","+model.UpdateCols+",").IndexOf(",Language_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_ids")+"= @Language_ids");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",NameColor,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NameColor")+"= @NameColor");
			if((","+model.UpdateCols+",").IndexOf(",Node_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Node_id")+"= @Node_id");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+"= @SEO_Description");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Keywords,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+"= @SEO_Keywords");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+"= @SEO_Title");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",source,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("source")+"= @source");
			if((","+model.UpdateCols+",").IndexOf(",sourceurl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("sourceurl")+"= @sourceurl");
			if((","+model.UpdateCols+",").IndexOf(",SubName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SubName")+"= @SubName");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",target,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("target")+"= @target");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Update,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+"= @Time_Update");
			if((","+model.UpdateCols+",").IndexOf(",url,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("url")+"= @url");
			if((","+model.UpdateCols+",").IndexOf(",user_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("user_id")+"= @user_id");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@admin_id", model.admin_id),
					new SqlParameter("@Author", model.Author),
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@Count_Comment", model.Count_Comment),
					new SqlParameter("@Count_Views", model.Count_Views),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Editor", model.Editor),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Language_ids", model.Language_ids),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@NameColor", model.NameColor),
					new SqlParameter("@Node_id", model.Node_id),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@source", model.source),
					new SqlParameter("@sourceurl", model.sourceurl),
					new SqlParameter("@SubName", model.SubName),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@target", model.target),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@url", model.url),
					new SqlParameter("@user_id", model.user_id)};
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
		public Lebi_Page GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Page;
		   }
		   Lebi_Page model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Page",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Page GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Page;
		   }
		   Lebi_Page model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Page",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Page GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Page;
		   }
		   Lebi_Page model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Page",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Page> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Page>;
		   }
		   List<Lebi_Page> list = new List<Lebi_Page>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Page", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Page> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Page>;
		   }
		   List<Lebi_Page> list = new List<Lebi_Page>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Page", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Page> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Page>;
		   }
		   List<Lebi_Page> list = new List<Lebi_Page>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Page", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Page> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Page>;
		   }
		   List<Lebi_Page> list = new List<Lebi_Page>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Page", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Page BindForm(Lebi_Page model)
		{
			if (HttpContext.Current.Request["admin_id"] != null)
				model.admin_id=LB.Tools.RequestTool.RequestInt("admin_id",0);
			if (HttpContext.Current.Request["Author"] != null)
				model.Author=LB.Tools.RequestTool.RequestString("Author");
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestString("Content");
			if (HttpContext.Current.Request["Count_Comment"] != null)
				model.Count_Comment=LB.Tools.RequestTool.RequestInt("Count_Comment",0);
			if (HttpContext.Current.Request["Count_Views"] != null)
				model.Count_Views=LB.Tools.RequestTool.RequestInt("Count_Views",0);
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["Editor"] != null)
				model.Editor=LB.Tools.RequestTool.RequestString("Editor");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["ImageBig"] != null)
				model.ImageBig=LB.Tools.RequestTool.RequestString("ImageBig");
			if (HttpContext.Current.Request["ImageMedium"] != null)
				model.ImageMedium=LB.Tools.RequestTool.RequestString("ImageMedium");
			if (HttpContext.Current.Request["ImageOriginal"] != null)
				model.ImageOriginal=LB.Tools.RequestTool.RequestString("ImageOriginal");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestString("ImageSmall");
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestString("Language");
			if (HttpContext.Current.Request["Language_ids"] != null)
				model.Language_ids=LB.Tools.RequestTool.RequestString("Language_ids");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["NameColor"] != null)
				model.NameColor=LB.Tools.RequestTool.RequestString("NameColor");
			if (HttpContext.Current.Request["Node_id"] != null)
				model.Node_id=LB.Tools.RequestTool.RequestInt("Node_id",0);
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestString("SEO_Title");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["source"] != null)
				model.source=LB.Tools.RequestTool.RequestString("source");
			if (HttpContext.Current.Request["sourceurl"] != null)
				model.sourceurl=LB.Tools.RequestTool.RequestString("sourceurl");
			if (HttpContext.Current.Request["SubName"] != null)
				model.SubName=LB.Tools.RequestTool.RequestString("SubName");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["target"] != null)
				model.target=LB.Tools.RequestTool.RequestString("target");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["url"] != null)
				model.url=LB.Tools.RequestTool.RequestString("url");
			if (HttpContext.Current.Request["user_id"] != null)
				model.user_id=LB.Tools.RequestTool.RequestInt("user_id",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Page SafeBindForm(Lebi_Page model)
		{
			if (HttpContext.Current.Request["admin_id"] != null)
				model.admin_id=LB.Tools.RequestTool.RequestInt("admin_id",0);
			if (HttpContext.Current.Request["Author"] != null)
				model.Author=LB.Tools.RequestTool.RequestSafeString("Author");
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestSafeString("Content");
			if (HttpContext.Current.Request["Count_Comment"] != null)
				model.Count_Comment=LB.Tools.RequestTool.RequestInt("Count_Comment",0);
			if (HttpContext.Current.Request["Count_Views"] != null)
				model.Count_Views=LB.Tools.RequestTool.RequestInt("Count_Views",0);
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["Editor"] != null)
				model.Editor=LB.Tools.RequestTool.RequestSafeString("Editor");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["ImageBig"] != null)
				model.ImageBig=LB.Tools.RequestTool.RequestSafeString("ImageBig");
			if (HttpContext.Current.Request["ImageMedium"] != null)
				model.ImageMedium=LB.Tools.RequestTool.RequestSafeString("ImageMedium");
			if (HttpContext.Current.Request["ImageOriginal"] != null)
				model.ImageOriginal=LB.Tools.RequestTool.RequestSafeString("ImageOriginal");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestSafeString("ImageSmall");
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestSafeString("Language");
			if (HttpContext.Current.Request["Language_ids"] != null)
				model.Language_ids=LB.Tools.RequestTool.RequestSafeString("Language_ids");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["NameColor"] != null)
				model.NameColor=LB.Tools.RequestTool.RequestSafeString("NameColor");
			if (HttpContext.Current.Request["Node_id"] != null)
				model.Node_id=LB.Tools.RequestTool.RequestInt("Node_id",0);
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestSafeString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestSafeString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestSafeString("SEO_Title");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["source"] != null)
				model.source=LB.Tools.RequestTool.RequestSafeString("source");
			if (HttpContext.Current.Request["sourceurl"] != null)
				model.sourceurl=LB.Tools.RequestTool.RequestSafeString("sourceurl");
			if (HttpContext.Current.Request["SubName"] != null)
				model.SubName=LB.Tools.RequestTool.RequestSafeString("SubName");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["target"] != null)
				model.target=LB.Tools.RequestTool.RequestSafeString("target");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["url"] != null)
				model.url=LB.Tools.RequestTool.RequestSafeString("url");
			if (HttpContext.Current.Request["user_id"] != null)
				model.user_id=LB.Tools.RequestTool.RequestInt("user_id",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Page ReaderBind(IDataReader dataReader)
		{
			Lebi_Page model=new Lebi_Page();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["admin_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.admin_id= Convert.ToInt32(ojb);
			}
			model.Author=dataReader["Author"].ToString();
			model.Content=dataReader["Content"].ToString();
			ojb = dataReader["Count_Comment"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Comment= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Views"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Views= Convert.ToInt32(ojb);
			}
			model.Description=dataReader["Description"].ToString();
			model.Editor=dataReader["Editor"].ToString();
			model.Email=dataReader["Email"].ToString();
			model.ImageBig=dataReader["ImageBig"].ToString();
			model.ImageMedium=dataReader["ImageMedium"].ToString();
			model.ImageOriginal=dataReader["ImageOriginal"].ToString();
			model.ImageSmall=dataReader["ImageSmall"].ToString();
			model.Language=dataReader["Language"].ToString();
			model.Language_ids=dataReader["Language_ids"].ToString();
			model.Name=dataReader["Name"].ToString();
			model.NameColor=dataReader["NameColor"].ToString();
			ojb = dataReader["Node_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Node_id= Convert.ToInt32(ojb);
			}
			model.SEO_Description=dataReader["SEO_Description"].ToString();
			model.SEO_Keywords=dataReader["SEO_Keywords"].ToString();
			model.SEO_Title=dataReader["SEO_Title"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			model.source=dataReader["source"].ToString();
			model.sourceurl=dataReader["sourceurl"].ToString();
			model.SubName=dataReader["SubName"].ToString();
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			model.target=dataReader["target"].ToString();
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Update"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Update=(DateTime)ojb;
			}
			model.url=dataReader["url"].ToString();
			ojb = dataReader["user_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.user_id= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

