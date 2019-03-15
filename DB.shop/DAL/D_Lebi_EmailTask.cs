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
	/// 数据访问类D_Lebi_EmailTask。
	/// </summary>
	public partial class D_Lebi_EmailTask
	{
		static D_Lebi_EmailTask _Instance;
		public static D_Lebi_EmailTask Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_EmailTask("Lebi_EmailTask");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_EmailTask";
		public D_Lebi_EmailTask(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_EmailTask", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_EmailTask", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_EmailTask" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_EmailTask", 0 , cachestr,seconds);
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
		public int Add(Lebi_EmailTask model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_send")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("EmailContent")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("EmailTitle")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSubmit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_edit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_task")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids")+")");
			strSql.Append(" values (");
			strSql.Append("@Admin_id,@Admin_UserName,@Count,@Count_send,@EmailContent,@EmailTitle,@IsSubmit,@Time_add,@Time_edit,@Time_task,@User_ids,@UserLevel_ids);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@Admin_UserName", model.Admin_UserName),
					new SqlParameter("@Count", model.Count),
					new SqlParameter("@Count_send", model.Count_send),
					new SqlParameter("@EmailContent", model.EmailContent),
					new SqlParameter("@EmailTitle", model.EmailTitle),
					new SqlParameter("@IsSubmit", model.IsSubmit),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Time_edit", model.Time_edit),
					new SqlParameter("@Time_task", model.Time_task),
					new SqlParameter("@User_ids", model.User_ids),
					new SqlParameter("@UserLevel_ids", model.UserLevel_ids)};

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
		public void Update(Lebi_EmailTask model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Admin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+"= @Admin_id");
			if((","+model.UpdateCols+",").IndexOf(",Admin_UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_UserName")+"= @Admin_UserName");
			if((","+model.UpdateCols+",").IndexOf(",Count,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count")+"= @Count");
			if((","+model.UpdateCols+",").IndexOf(",Count_send,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_send")+"= @Count_send");
			if((","+model.UpdateCols+",").IndexOf(",EmailContent,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("EmailContent")+"= @EmailContent");
			if((","+model.UpdateCols+",").IndexOf(",EmailTitle,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("EmailTitle")+"= @EmailTitle");
			if((","+model.UpdateCols+",").IndexOf(",IsSubmit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSubmit")+"= @IsSubmit");
			if((","+model.UpdateCols+",").IndexOf(",Time_add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+"= @Time_add");
			if((","+model.UpdateCols+",").IndexOf(",Time_edit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_edit")+"= @Time_edit");
			if((","+model.UpdateCols+",").IndexOf(",Time_task,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_task")+"= @Time_task");
			if((","+model.UpdateCols+",").IndexOf(",User_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_ids")+"= @User_ids");
			if((","+model.UpdateCols+",").IndexOf(",UserLevel_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids")+"= @UserLevel_ids");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@Admin_UserName", model.Admin_UserName),
					new SqlParameter("@Count", model.Count),
					new SqlParameter("@Count_send", model.Count_send),
					new SqlParameter("@EmailContent", model.EmailContent),
					new SqlParameter("@EmailTitle", model.EmailTitle),
					new SqlParameter("@IsSubmit", model.IsSubmit),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Time_edit", model.Time_edit),
					new SqlParameter("@Time_task", model.Time_task),
					new SqlParameter("@User_ids", model.User_ids),
					new SqlParameter("@UserLevel_ids", model.UserLevel_ids)};
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
		public Lebi_EmailTask GetModel(int id, int seconds=0)
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
		           return obj as Lebi_EmailTask;
		   }
		   Lebi_EmailTask model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_EmailTask",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_EmailTask GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_EmailTask;
		   }
		   Lebi_EmailTask model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_EmailTask",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_EmailTask GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_EmailTask;
		   }
		   Lebi_EmailTask model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_EmailTask",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_EmailTask> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_EmailTask>;
		   }
		   List<Lebi_EmailTask> list = new List<Lebi_EmailTask>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_EmailTask", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_EmailTask> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_EmailTask>;
		   }
		   List<Lebi_EmailTask> list = new List<Lebi_EmailTask>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_EmailTask", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_EmailTask> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_EmailTask>;
		   }
		   List<Lebi_EmailTask> list = new List<Lebi_EmailTask>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_EmailTask", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_EmailTask> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_EmailTask>;
		   }
		   List<Lebi_EmailTask> list = new List<Lebi_EmailTask>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_EmailTask", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_EmailTask BindForm(Lebi_EmailTask model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["Admin_UserName"] != null)
				model.Admin_UserName=LB.Tools.RequestTool.RequestString("Admin_UserName");
			if (HttpContext.Current.Request["Count"] != null)
				model.Count=LB.Tools.RequestTool.RequestInt("Count",0);
			if (HttpContext.Current.Request["Count_send"] != null)
				model.Count_send=LB.Tools.RequestTool.RequestInt("Count_send",0);
			if (HttpContext.Current.Request["EmailContent"] != null)
				model.EmailContent=LB.Tools.RequestTool.RequestString("EmailContent");
			if (HttpContext.Current.Request["EmailTitle"] != null)
				model.EmailTitle=LB.Tools.RequestTool.RequestString("EmailTitle");
			if (HttpContext.Current.Request["IsSubmit"] != null)
				model.IsSubmit=LB.Tools.RequestTool.RequestInt("IsSubmit",0);
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_edit"] != null)
				model.Time_edit=LB.Tools.RequestTool.RequestTime("Time_edit", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_task"] != null)
				model.Time_task=LB.Tools.RequestTool.RequestTime("Time_task", System.DateTime.Now);
			if (HttpContext.Current.Request["User_ids"] != null)
				model.User_ids=LB.Tools.RequestTool.RequestString("User_ids");
			if (HttpContext.Current.Request["UserLevel_ids"] != null)
				model.UserLevel_ids=LB.Tools.RequestTool.RequestString("UserLevel_ids");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_EmailTask SafeBindForm(Lebi_EmailTask model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["Admin_UserName"] != null)
				model.Admin_UserName=LB.Tools.RequestTool.RequestSafeString("Admin_UserName");
			if (HttpContext.Current.Request["Count"] != null)
				model.Count=LB.Tools.RequestTool.RequestInt("Count",0);
			if (HttpContext.Current.Request["Count_send"] != null)
				model.Count_send=LB.Tools.RequestTool.RequestInt("Count_send",0);
			if (HttpContext.Current.Request["EmailContent"] != null)
				model.EmailContent=LB.Tools.RequestTool.RequestSafeString("EmailContent");
			if (HttpContext.Current.Request["EmailTitle"] != null)
				model.EmailTitle=LB.Tools.RequestTool.RequestSafeString("EmailTitle");
			if (HttpContext.Current.Request["IsSubmit"] != null)
				model.IsSubmit=LB.Tools.RequestTool.RequestInt("IsSubmit",0);
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_edit"] != null)
				model.Time_edit=LB.Tools.RequestTool.RequestTime("Time_edit", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_task"] != null)
				model.Time_task=LB.Tools.RequestTool.RequestTime("Time_task", System.DateTime.Now);
			if (HttpContext.Current.Request["User_ids"] != null)
				model.User_ids=LB.Tools.RequestTool.RequestSafeString("User_ids");
			if (HttpContext.Current.Request["UserLevel_ids"] != null)
				model.UserLevel_ids=LB.Tools.RequestTool.RequestSafeString("UserLevel_ids");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_EmailTask ReaderBind(IDataReader dataReader)
		{
			Lebi_EmailTask model=new Lebi_EmailTask();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Admin_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Admin_id= Convert.ToInt32(ojb);
			}
			model.Admin_UserName=dataReader["Admin_UserName"].ToString();
			ojb = dataReader["Count"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_send"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_send= Convert.ToInt32(ojb);
			}
			model.EmailContent=dataReader["EmailContent"].ToString();
			model.EmailTitle=dataReader["EmailTitle"].ToString();
			ojb = dataReader["IsSubmit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSubmit= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Time_add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_add=(DateTime)ojb;
			}
			ojb = dataReader["Time_edit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_edit=(DateTime)ojb;
			}
			ojb = dataReader["Time_task"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_task=(DateTime)ojb;
			}
			model.User_ids=dataReader["User_ids"].ToString();
			model.UserLevel_ids=dataReader["UserLevel_ids"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

