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
	/// 数据访问类D_Lebi_Version。
	/// </summary>
	public partial class D_Lebi_Version
	{
		static D_Lebi_Version _Instance;
		public static D_Lebi_Version Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Version("Lebi_Version");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Version";
		public D_Lebi_Version(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Version", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Version", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Version" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Version", 0 , cachestr,seconds);
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
		public int Add(Lebi_Version model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDBStructUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsNodeUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPageUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSystemMenuUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSystemPageUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsThemePageUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsTypeUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUpdate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_rar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Size")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Version")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Version_Check")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Version_Son")+")");
			strSql.Append(" values (");
			strSql.Append("@Content,@Description,@IsDBStructUpdate,@IsNodeUpdate,@IsPageUpdate,@IsSystemMenuUpdate,@IsSystemPageUpdate,@IsThemePageUpdate,@IsTypeUpdate,@IsUpdate,@Path,@Path_rar,@Size,@Time_Update,@Version,@Version_Check,@Version_Son);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@IsDBStructUpdate", model.IsDBStructUpdate),
					new SqlParameter("@IsNodeUpdate", model.IsNodeUpdate),
					new SqlParameter("@IsPageUpdate", model.IsPageUpdate),
					new SqlParameter("@IsSystemMenuUpdate", model.IsSystemMenuUpdate),
					new SqlParameter("@IsSystemPageUpdate", model.IsSystemPageUpdate),
					new SqlParameter("@IsThemePageUpdate", model.IsThemePageUpdate),
					new SqlParameter("@IsTypeUpdate", model.IsTypeUpdate),
					new SqlParameter("@IsUpdate", model.IsUpdate),
					new SqlParameter("@Path", model.Path),
					new SqlParameter("@Path_rar", model.Path_rar),
					new SqlParameter("@Size", model.Size),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Version", model.Version),
					new SqlParameter("@Version_Check", model.Version_Check),
					new SqlParameter("@Version_Son", model.Version_Son)};

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
		public void Update(Lebi_Version model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Content,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+"= @Content");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",IsDBStructUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDBStructUpdate")+"= @IsDBStructUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsNodeUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsNodeUpdate")+"= @IsNodeUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsPageUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPageUpdate")+"= @IsPageUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsSystemMenuUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSystemMenuUpdate")+"= @IsSystemMenuUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsSystemPageUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSystemPageUpdate")+"= @IsSystemPageUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsThemePageUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsThemePageUpdate")+"= @IsThemePageUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsTypeUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsTypeUpdate")+"= @IsTypeUpdate");
			if((","+model.UpdateCols+",").IndexOf(",IsUpdate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUpdate")+"= @IsUpdate");
			if((","+model.UpdateCols+",").IndexOf(",Path,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path")+"= @Path");
			if((","+model.UpdateCols+",").IndexOf(",Path_rar,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path_rar")+"= @Path_rar");
			if((","+model.UpdateCols+",").IndexOf(",Size,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Size")+"= @Size");
			if((","+model.UpdateCols+",").IndexOf(",Time_Update,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+"= @Time_Update");
			if((","+model.UpdateCols+",").IndexOf(",Version,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Version")+"= @Version");
			if((","+model.UpdateCols+",").IndexOf(",Version_Check,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Version_Check")+"= @Version_Check");
			if((","+model.UpdateCols+",").IndexOf(",Version_Son,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Version_Son")+"= @Version_Son");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@IsDBStructUpdate", model.IsDBStructUpdate),
					new SqlParameter("@IsNodeUpdate", model.IsNodeUpdate),
					new SqlParameter("@IsPageUpdate", model.IsPageUpdate),
					new SqlParameter("@IsSystemMenuUpdate", model.IsSystemMenuUpdate),
					new SqlParameter("@IsSystemPageUpdate", model.IsSystemPageUpdate),
					new SqlParameter("@IsThemePageUpdate", model.IsThemePageUpdate),
					new SqlParameter("@IsTypeUpdate", model.IsTypeUpdate),
					new SqlParameter("@IsUpdate", model.IsUpdate),
					new SqlParameter("@Path", model.Path),
					new SqlParameter("@Path_rar", model.Path_rar),
					new SqlParameter("@Size", model.Size),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Version", model.Version),
					new SqlParameter("@Version_Check", model.Version_Check),
					new SqlParameter("@Version_Son", model.Version_Son)};
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
		public Lebi_Version GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Version;
		   }
		   Lebi_Version model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Version",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Version GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Version;
		   }
		   Lebi_Version model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Version",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Version GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Version;
		   }
		   Lebi_Version model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Version",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Version> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Version>;
		   }
		   List<Lebi_Version> list = new List<Lebi_Version>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Version", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Version> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Version>;
		   }
		   List<Lebi_Version> list = new List<Lebi_Version>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Version", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Version> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Version>;
		   }
		   List<Lebi_Version> list = new List<Lebi_Version>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Version", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Version> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Version>;
		   }
		   List<Lebi_Version> list = new List<Lebi_Version>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Version", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Version BindForm(Lebi_Version model)
		{
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestString("Content");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["IsDBStructUpdate"] != null)
				model.IsDBStructUpdate=LB.Tools.RequestTool.RequestInt("IsDBStructUpdate",0);
			if (HttpContext.Current.Request["IsNodeUpdate"] != null)
				model.IsNodeUpdate=LB.Tools.RequestTool.RequestInt("IsNodeUpdate",0);
			if (HttpContext.Current.Request["IsPageUpdate"] != null)
				model.IsPageUpdate=LB.Tools.RequestTool.RequestInt("IsPageUpdate",0);
			if (HttpContext.Current.Request["IsSystemMenuUpdate"] != null)
				model.IsSystemMenuUpdate=LB.Tools.RequestTool.RequestInt("IsSystemMenuUpdate",0);
			if (HttpContext.Current.Request["IsSystemPageUpdate"] != null)
				model.IsSystemPageUpdate=LB.Tools.RequestTool.RequestInt("IsSystemPageUpdate",0);
			if (HttpContext.Current.Request["IsThemePageUpdate"] != null)
				model.IsThemePageUpdate=LB.Tools.RequestTool.RequestInt("IsThemePageUpdate",0);
			if (HttpContext.Current.Request["IsTypeUpdate"] != null)
				model.IsTypeUpdate=LB.Tools.RequestTool.RequestInt("IsTypeUpdate",0);
			if (HttpContext.Current.Request["IsUpdate"] != null)
				model.IsUpdate=LB.Tools.RequestTool.RequestInt("IsUpdate",0);
			if (HttpContext.Current.Request["Path"] != null)
				model.Path=LB.Tools.RequestTool.RequestString("Path");
			if (HttpContext.Current.Request["Path_rar"] != null)
				model.Path_rar=LB.Tools.RequestTool.RequestString("Path_rar");
			if (HttpContext.Current.Request["Size"] != null)
				model.Size=LB.Tools.RequestTool.RequestString("Size");
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Version"] != null)
				model.Version=LB.Tools.RequestTool.RequestInt("Version",0);
			if (HttpContext.Current.Request["Version_Check"] != null)
				model.Version_Check=LB.Tools.RequestTool.RequestString("Version_Check");
			if (HttpContext.Current.Request["Version_Son"] != null)
				model.Version_Son=LB.Tools.RequestTool.RequestDecimal("Version_Son",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Version SafeBindForm(Lebi_Version model)
		{
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestSafeString("Content");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["IsDBStructUpdate"] != null)
				model.IsDBStructUpdate=LB.Tools.RequestTool.RequestInt("IsDBStructUpdate",0);
			if (HttpContext.Current.Request["IsNodeUpdate"] != null)
				model.IsNodeUpdate=LB.Tools.RequestTool.RequestInt("IsNodeUpdate",0);
			if (HttpContext.Current.Request["IsPageUpdate"] != null)
				model.IsPageUpdate=LB.Tools.RequestTool.RequestInt("IsPageUpdate",0);
			if (HttpContext.Current.Request["IsSystemMenuUpdate"] != null)
				model.IsSystemMenuUpdate=LB.Tools.RequestTool.RequestInt("IsSystemMenuUpdate",0);
			if (HttpContext.Current.Request["IsSystemPageUpdate"] != null)
				model.IsSystemPageUpdate=LB.Tools.RequestTool.RequestInt("IsSystemPageUpdate",0);
			if (HttpContext.Current.Request["IsThemePageUpdate"] != null)
				model.IsThemePageUpdate=LB.Tools.RequestTool.RequestInt("IsThemePageUpdate",0);
			if (HttpContext.Current.Request["IsTypeUpdate"] != null)
				model.IsTypeUpdate=LB.Tools.RequestTool.RequestInt("IsTypeUpdate",0);
			if (HttpContext.Current.Request["IsUpdate"] != null)
				model.IsUpdate=LB.Tools.RequestTool.RequestInt("IsUpdate",0);
			if (HttpContext.Current.Request["Path"] != null)
				model.Path=LB.Tools.RequestTool.RequestSafeString("Path");
			if (HttpContext.Current.Request["Path_rar"] != null)
				model.Path_rar=LB.Tools.RequestTool.RequestSafeString("Path_rar");
			if (HttpContext.Current.Request["Size"] != null)
				model.Size=LB.Tools.RequestTool.RequestSafeString("Size");
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Version"] != null)
				model.Version=LB.Tools.RequestTool.RequestInt("Version",0);
			if (HttpContext.Current.Request["Version_Check"] != null)
				model.Version_Check=LB.Tools.RequestTool.RequestSafeString("Version_Check");
			if (HttpContext.Current.Request["Version_Son"] != null)
				model.Version_Son=LB.Tools.RequestTool.RequestDecimal("Version_Son",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Version ReaderBind(IDataReader dataReader)
		{
			Lebi_Version model=new Lebi_Version();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Content=dataReader["Content"].ToString();
			model.Description=dataReader["Description"].ToString();
			ojb = dataReader["IsDBStructUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsDBStructUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsNodeUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsNodeUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsPageUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsPageUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSystemMenuUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSystemMenuUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSystemPageUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSystemPageUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsThemePageUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsThemePageUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsTypeUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsTypeUpdate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsUpdate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsUpdate= Convert.ToInt32(ojb);
			}
			model.Path=dataReader["Path"].ToString();
			model.Path_rar=dataReader["Path_rar"].ToString();
			model.Size=dataReader["Size"].ToString();
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
			model.Version_Check=dataReader["Version_Check"].ToString();
			ojb = dataReader["Version_Son"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Version_Son=(decimal)ojb;
			}
			return model;
		}

		#endregion  成员方法
	}
}

