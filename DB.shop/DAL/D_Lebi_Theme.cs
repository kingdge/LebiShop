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
	/// 数据访问类D_Lebi_Theme。
	/// </summary>
	public partial class D_Lebi_Theme
	{
		static D_Lebi_Theme _Instance;
		public static D_Lebi_Theme Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Theme("Lebi_Theme");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Theme";
		public D_Lebi_Theme(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Theme", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Theme", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Theme" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Theme", 0 , cachestr,seconds);
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
		public int Add(Lebi_Theme model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig_Height")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig_Width")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium_Height")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium_Width")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall_Height")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall_Width")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmallUrl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageUrl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsNew")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("LebiUser")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("LebiUser_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Advert")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Create")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_CSS")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Files")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Image")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_JS")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Version")+")");
			strSql.Append(" values (");
			strSql.Append("@Code,@Description,@ImageBig_Height,@ImageBig_Width,@ImageMedium_Height,@ImageMedium_Width,@ImageSmall_Height,@ImageSmall_Width,@ImageSmallUrl,@ImageUrl,@IsNew,@IsUpdate,@Language,@LebiUser,@LebiUser_id,@Name,@Path_Advert,@Path_Create,@Path_CSS,@Path_Files,@Path_Image,@Path_JS,@Sort,@Time_Add,@Time_Update,@Version);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@ImageBig_Height", model.ImageBig_Height),
					new SqlParameter("@ImageBig_Width", model.ImageBig_Width),
					new SqlParameter("@ImageMedium_Height", model.ImageMedium_Height),
					new SqlParameter("@ImageMedium_Width", model.ImageMedium_Width),
					new SqlParameter("@ImageSmall_Height", model.ImageSmall_Height),
					new SqlParameter("@ImageSmall_Width", model.ImageSmall_Width),
					new SqlParameter("@ImageSmallUrl", model.ImageSmallUrl),
					new SqlParameter("@ImageUrl", model.ImageUrl),
					new SqlParameter("@IsNew", model.IsNew),
					new SqlParameter("@IsUpdate", model.IsUpdate),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@LebiUser", model.LebiUser),
					new SqlParameter("@LebiUser_id", model.LebiUser_id),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Path_Advert", model.Path_Advert),
					new SqlParameter("@Path_Create", model.Path_Create),
					new SqlParameter("@Path_CSS", model.Path_CSS),
					new SqlParameter("@Path_Files", model.Path_Files),
					new SqlParameter("@Path_Image", model.Path_Image),
					new SqlParameter("@Path_JS", model.Path_JS),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Version", model.Version)};

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
		public void Update(Lebi_Theme model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+"= @Code");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",ImageBig_Height,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig_Height")+"= @ImageBig_Height");
			if((","+model.UpdateCols+",").IndexOf(",ImageBig_Width,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig_Width")+"= @ImageBig_Width");
			if((","+model.UpdateCols+",").IndexOf(",ImageMedium_Height,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium_Height")+"= @ImageMedium_Height");
			if((","+model.UpdateCols+",").IndexOf(",ImageMedium_Width,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium_Width")+"= @ImageMedium_Width");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall_Height,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall_Height")+"= @ImageSmall_Height");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall_Width,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall_Width")+"= @ImageSmall_Width");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmallUrl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmallUrl")+"= @ImageSmallUrl");
			if((","+model.UpdateCols+",").IndexOf(",ImageUrl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageUrl")+"= @ImageUrl");
			if((","+model.UpdateCols+",").IndexOf(",IsNew,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsNew")+"= @IsNew");
			if((","+model.UpdateCols+",").IndexOf(",IsUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUpdate")+"= @IsUpdate");
			if((","+model.UpdateCols+",").IndexOf(",Language,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+"= @Language");
			if((","+model.UpdateCols+",").IndexOf(",LebiUser,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("LebiUser")+"= @LebiUser");
			if((","+model.UpdateCols+",").IndexOf(",LebiUser_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("LebiUser_id")+"= @LebiUser_id");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",Path_Advert,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Advert")+"= @Path_Advert");
			if((","+model.UpdateCols+",").IndexOf(",Path_Create,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Create")+"= @Path_Create");
			if((","+model.UpdateCols+",").IndexOf(",Path_CSS,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_CSS")+"= @Path_CSS");
			if((","+model.UpdateCols+",").IndexOf(",Path_Files,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Files")+"= @Path_Files");
			if((","+model.UpdateCols+",").IndexOf(",Path_Image,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_Image")+"= @Path_Image");
			if((","+model.UpdateCols+",").IndexOf(",Path_JS,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_JS")+"= @Path_JS");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Update,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+"= @Time_Update");
			if((","+model.UpdateCols+",").IndexOf(",Version,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Version")+"= @Version");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@ImageBig_Height", model.ImageBig_Height),
					new SqlParameter("@ImageBig_Width", model.ImageBig_Width),
					new SqlParameter("@ImageMedium_Height", model.ImageMedium_Height),
					new SqlParameter("@ImageMedium_Width", model.ImageMedium_Width),
					new SqlParameter("@ImageSmall_Height", model.ImageSmall_Height),
					new SqlParameter("@ImageSmall_Width", model.ImageSmall_Width),
					new SqlParameter("@ImageSmallUrl", model.ImageSmallUrl),
					new SqlParameter("@ImageUrl", model.ImageUrl),
					new SqlParameter("@IsNew", model.IsNew),
					new SqlParameter("@IsUpdate", model.IsUpdate),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@LebiUser", model.LebiUser),
					new SqlParameter("@LebiUser_id", model.LebiUser_id),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Path_Advert", model.Path_Advert),
					new SqlParameter("@Path_Create", model.Path_Create),
					new SqlParameter("@Path_CSS", model.Path_CSS),
					new SqlParameter("@Path_Files", model.Path_Files),
					new SqlParameter("@Path_Image", model.Path_Image),
					new SqlParameter("@Path_JS", model.Path_JS),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Version", model.Version)};
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
		public Lebi_Theme GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Theme;
		   }
		   Lebi_Theme model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Theme",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Theme GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Theme;
		   }
		   Lebi_Theme model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Theme",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Theme GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Theme;
		   }
		   Lebi_Theme model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Theme",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Theme> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Theme>;
		   }
		   List<Lebi_Theme> list = new List<Lebi_Theme>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Theme", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Theme> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Theme>;
		   }
		   List<Lebi_Theme> list = new List<Lebi_Theme>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Theme", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Theme> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Theme>;
		   }
		   List<Lebi_Theme> list = new List<Lebi_Theme>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Theme", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Theme> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Theme>;
		   }
		   List<Lebi_Theme> list = new List<Lebi_Theme>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Theme", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Theme BindForm(Lebi_Theme model)
		{
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestString("Code");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["ImageBig_Height"] != null)
				model.ImageBig_Height=LB.Tools.RequestTool.RequestInt("ImageBig_Height",0);
			if (HttpContext.Current.Request["ImageBig_Width"] != null)
				model.ImageBig_Width=LB.Tools.RequestTool.RequestInt("ImageBig_Width",0);
			if (HttpContext.Current.Request["ImageMedium_Height"] != null)
				model.ImageMedium_Height=LB.Tools.RequestTool.RequestInt("ImageMedium_Height",0);
			if (HttpContext.Current.Request["ImageMedium_Width"] != null)
				model.ImageMedium_Width=LB.Tools.RequestTool.RequestInt("ImageMedium_Width",0);
			if (HttpContext.Current.Request["ImageSmall_Height"] != null)
				model.ImageSmall_Height=LB.Tools.RequestTool.RequestInt("ImageSmall_Height",0);
			if (HttpContext.Current.Request["ImageSmall_Width"] != null)
				model.ImageSmall_Width=LB.Tools.RequestTool.RequestInt("ImageSmall_Width",0);
			if (HttpContext.Current.Request["ImageSmallUrl"] != null)
				model.ImageSmallUrl=LB.Tools.RequestTool.RequestString("ImageSmallUrl");
			if (HttpContext.Current.Request["ImageUrl"] != null)
				model.ImageUrl=LB.Tools.RequestTool.RequestString("ImageUrl");
			if (HttpContext.Current.Request["IsNew"] != null)
				model.IsNew=LB.Tools.RequestTool.RequestInt("IsNew",0);
			if (HttpContext.Current.Request["IsUpdate"] != null)
				model.IsUpdate=LB.Tools.RequestTool.RequestInt("IsUpdate",0);
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestString("Language");
			if (HttpContext.Current.Request["LebiUser"] != null)
				model.LebiUser=LB.Tools.RequestTool.RequestString("LebiUser");
			if (HttpContext.Current.Request["LebiUser_id"] != null)
				model.LebiUser_id=LB.Tools.RequestTool.RequestInt("LebiUser_id",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["Path_Advert"] != null)
				model.Path_Advert=LB.Tools.RequestTool.RequestString("Path_Advert");
			if (HttpContext.Current.Request["Path_Create"] != null)
				model.Path_Create=LB.Tools.RequestTool.RequestString("Path_Create");
			if (HttpContext.Current.Request["Path_CSS"] != null)
				model.Path_CSS=LB.Tools.RequestTool.RequestString("Path_CSS");
			if (HttpContext.Current.Request["Path_Files"] != null)
				model.Path_Files=LB.Tools.RequestTool.RequestString("Path_Files");
			if (HttpContext.Current.Request["Path_Image"] != null)
				model.Path_Image=LB.Tools.RequestTool.RequestString("Path_Image");
			if (HttpContext.Current.Request["Path_JS"] != null)
				model.Path_JS=LB.Tools.RequestTool.RequestString("Path_JS");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Version"] != null)
				model.Version=LB.Tools.RequestTool.RequestInt("Version",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Theme SafeBindForm(Lebi_Theme model)
		{
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestSafeString("Code");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["ImageBig_Height"] != null)
				model.ImageBig_Height=LB.Tools.RequestTool.RequestInt("ImageBig_Height",0);
			if (HttpContext.Current.Request["ImageBig_Width"] != null)
				model.ImageBig_Width=LB.Tools.RequestTool.RequestInt("ImageBig_Width",0);
			if (HttpContext.Current.Request["ImageMedium_Height"] != null)
				model.ImageMedium_Height=LB.Tools.RequestTool.RequestInt("ImageMedium_Height",0);
			if (HttpContext.Current.Request["ImageMedium_Width"] != null)
				model.ImageMedium_Width=LB.Tools.RequestTool.RequestInt("ImageMedium_Width",0);
			if (HttpContext.Current.Request["ImageSmall_Height"] != null)
				model.ImageSmall_Height=LB.Tools.RequestTool.RequestInt("ImageSmall_Height",0);
			if (HttpContext.Current.Request["ImageSmall_Width"] != null)
				model.ImageSmall_Width=LB.Tools.RequestTool.RequestInt("ImageSmall_Width",0);
			if (HttpContext.Current.Request["ImageSmallUrl"] != null)
				model.ImageSmallUrl=LB.Tools.RequestTool.RequestSafeString("ImageSmallUrl");
			if (HttpContext.Current.Request["ImageUrl"] != null)
				model.ImageUrl=LB.Tools.RequestTool.RequestSafeString("ImageUrl");
			if (HttpContext.Current.Request["IsNew"] != null)
				model.IsNew=LB.Tools.RequestTool.RequestInt("IsNew",0);
			if (HttpContext.Current.Request["IsUpdate"] != null)
				model.IsUpdate=LB.Tools.RequestTool.RequestInt("IsUpdate",0);
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestSafeString("Language");
			if (HttpContext.Current.Request["LebiUser"] != null)
				model.LebiUser=LB.Tools.RequestTool.RequestSafeString("LebiUser");
			if (HttpContext.Current.Request["LebiUser_id"] != null)
				model.LebiUser_id=LB.Tools.RequestTool.RequestInt("LebiUser_id",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["Path_Advert"] != null)
				model.Path_Advert=LB.Tools.RequestTool.RequestSafeString("Path_Advert");
			if (HttpContext.Current.Request["Path_Create"] != null)
				model.Path_Create=LB.Tools.RequestTool.RequestSafeString("Path_Create");
			if (HttpContext.Current.Request["Path_CSS"] != null)
				model.Path_CSS=LB.Tools.RequestTool.RequestSafeString("Path_CSS");
			if (HttpContext.Current.Request["Path_Files"] != null)
				model.Path_Files=LB.Tools.RequestTool.RequestSafeString("Path_Files");
			if (HttpContext.Current.Request["Path_Image"] != null)
				model.Path_Image=LB.Tools.RequestTool.RequestSafeString("Path_Image");
			if (HttpContext.Current.Request["Path_JS"] != null)
				model.Path_JS=LB.Tools.RequestTool.RequestSafeString("Path_JS");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Version"] != null)
				model.Version=LB.Tools.RequestTool.RequestInt("Version",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Theme ReaderBind(IDataReader dataReader)
		{
			Lebi_Theme model=new Lebi_Theme();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Code=dataReader["Code"].ToString();
			model.Description=dataReader["Description"].ToString();
			ojb = dataReader["ImageBig_Height"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ImageBig_Height= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ImageBig_Width"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ImageBig_Width= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ImageMedium_Height"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ImageMedium_Height= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ImageMedium_Width"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ImageMedium_Width= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ImageSmall_Height"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ImageSmall_Height= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ImageSmall_Width"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ImageSmall_Width= Convert.ToInt32(ojb);
			}
			model.ImageSmallUrl=dataReader["ImageSmallUrl"].ToString();
			model.ImageUrl=dataReader["ImageUrl"].ToString();
			ojb = dataReader["IsNew"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsNew= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsUpdate= Convert.ToInt32(ojb);
			}
			model.Language=dataReader["Language"].ToString();
			model.LebiUser=dataReader["LebiUser"].ToString();
			ojb = dataReader["LebiUser_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.LebiUser_id= Convert.ToInt32(ojb);
			}
			model.Name=dataReader["Name"].ToString();
			model.Path_Advert=dataReader["Path_Advert"].ToString();
			model.Path_Create=dataReader["Path_Create"].ToString();
			model.Path_CSS=dataReader["Path_CSS"].ToString();
			model.Path_Files=dataReader["Path_Files"].ToString();
			model.Path_Image=dataReader["Path_Image"].ToString();
			model.Path_JS=dataReader["Path_JS"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
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
			ojb = dataReader["Version"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Version= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

