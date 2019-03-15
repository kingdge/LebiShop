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
	/// 数据访问类D_Lebi_Agent_Product_User。
	/// </summary>
	public partial class D_Lebi_Agent_Product_User
	{
		static D_Lebi_Agent_Product_User _Instance;
		public static D_Lebi_Agent_Product_User Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Agent_Product_User("Lebi_Agent_Product_User");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Agent_Product_User";
		public D_Lebi_Agent_Product_User(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Product_User", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Product_User", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Product_User" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Product_User", 0 , cachestr,seconds);
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
		public int Add(Lebi_Agent_Product_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Agent_Product_Level_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Commission")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Product")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_product_change")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_product_change_used")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsFailure")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_end")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+")");
			strSql.Append(" values (");
			strSql.Append("@Agent_Product_Level_id,@Commission,@Count_Product,@Count_product_change,@Count_product_change_used,@IsFailure,@Remark,@Time_add,@Time_end,@User_id,@User_UserName);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Agent_Product_Level_id", model.Agent_Product_Level_id),
					new SqlParameter("@Commission", model.Commission),
					new SqlParameter("@Count_Product", model.Count_Product),
					new SqlParameter("@Count_product_change", model.Count_product_change),
					new SqlParameter("@Count_product_change_used", model.Count_product_change_used),
					new SqlParameter("@IsFailure", model.IsFailure),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Time_end", model.Time_end),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@User_UserName", model.User_UserName)};

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
		public void Update(Lebi_Agent_Product_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Agent_Product_Level_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Agent_Product_Level_id")+"= @Agent_Product_Level_id");
			if((","+model.UpdateCols+",").IndexOf(",Commission,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Commission")+"= @Commission");
			if((","+model.UpdateCols+",").IndexOf(",Count_Product,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Product")+"= @Count_Product");
			if((","+model.UpdateCols+",").IndexOf(",Count_product_change,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_product_change")+"= @Count_product_change");
			if((","+model.UpdateCols+",").IndexOf(",Count_product_change_used,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_product_change_used")+"= @Count_product_change_used");
			if((","+model.UpdateCols+",").IndexOf(",IsFailure,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsFailure")+"= @IsFailure");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",Time_add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+"= @Time_add");
			if((","+model.UpdateCols+",").IndexOf(",Time_end,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_end")+"= @Time_end");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",User_UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+"= @User_UserName");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Agent_Product_Level_id", model.Agent_Product_Level_id),
					new SqlParameter("@Commission", model.Commission),
					new SqlParameter("@Count_Product", model.Count_Product),
					new SqlParameter("@Count_product_change", model.Count_product_change),
					new SqlParameter("@Count_product_change_used", model.Count_product_change_used),
					new SqlParameter("@IsFailure", model.IsFailure),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Time_end", model.Time_end),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@User_UserName", model.User_UserName)};
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
		public Lebi_Agent_Product_User GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Agent_Product_User;
		   }
		   Lebi_Agent_Product_User model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Agent_Product_User",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Agent_Product_User GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Agent_Product_User;
		   }
		   Lebi_Agent_Product_User model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Agent_Product_User",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Agent_Product_User GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Agent_Product_User;
		   }
		   Lebi_Agent_Product_User model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Agent_Product_User",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Agent_Product_User> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Agent_Product_User>;
		   }
		   List<Lebi_Agent_Product_User> list = new List<Lebi_Agent_Product_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Product_User", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Agent_Product_User> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Agent_Product_User>;
		   }
		   List<Lebi_Agent_Product_User> list = new List<Lebi_Agent_Product_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Product_User", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Agent_Product_User> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Agent_Product_User>;
		   }
		   List<Lebi_Agent_Product_User> list = new List<Lebi_Agent_Product_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Product_User", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Agent_Product_User> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Agent_Product_User>;
		   }
		   List<Lebi_Agent_Product_User> list = new List<Lebi_Agent_Product_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Product_User", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Agent_Product_User BindForm(Lebi_Agent_Product_User model)
		{
			if (HttpContext.Current.Request["Agent_Product_Level_id"] != null)
				model.Agent_Product_Level_id=LB.Tools.RequestTool.RequestInt("Agent_Product_Level_id",0);
			if (HttpContext.Current.Request["Commission"] != null)
				model.Commission=LB.Tools.RequestTool.RequestDecimal("Commission",0);
			if (HttpContext.Current.Request["Count_Product"] != null)
				model.Count_Product=LB.Tools.RequestTool.RequestInt("Count_Product",0);
			if (HttpContext.Current.Request["Count_product_change"] != null)
				model.Count_product_change=LB.Tools.RequestTool.RequestInt("Count_product_change",0);
			if (HttpContext.Current.Request["Count_product_change_used"] != null)
				model.Count_product_change_used=LB.Tools.RequestTool.RequestInt("Count_product_change_used",0);
			if (HttpContext.Current.Request["IsFailure"] != null)
				model.IsFailure=LB.Tools.RequestTool.RequestInt("IsFailure",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_end"] != null)
				model.Time_end=LB.Tools.RequestTool.RequestTime("Time_end", System.DateTime.Now);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestString("User_UserName");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Agent_Product_User SafeBindForm(Lebi_Agent_Product_User model)
		{
			if (HttpContext.Current.Request["Agent_Product_Level_id"] != null)
				model.Agent_Product_Level_id=LB.Tools.RequestTool.RequestInt("Agent_Product_Level_id",0);
			if (HttpContext.Current.Request["Commission"] != null)
				model.Commission=LB.Tools.RequestTool.RequestDecimal("Commission",0);
			if (HttpContext.Current.Request["Count_Product"] != null)
				model.Count_Product=LB.Tools.RequestTool.RequestInt("Count_Product",0);
			if (HttpContext.Current.Request["Count_product_change"] != null)
				model.Count_product_change=LB.Tools.RequestTool.RequestInt("Count_product_change",0);
			if (HttpContext.Current.Request["Count_product_change_used"] != null)
				model.Count_product_change_used=LB.Tools.RequestTool.RequestInt("Count_product_change_used",0);
			if (HttpContext.Current.Request["IsFailure"] != null)
				model.IsFailure=LB.Tools.RequestTool.RequestInt("IsFailure",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_end"] != null)
				model.Time_end=LB.Tools.RequestTool.RequestTime("Time_end", System.DateTime.Now);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestSafeString("User_UserName");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Agent_Product_User ReaderBind(IDataReader dataReader)
		{
			Lebi_Agent_Product_User model=new Lebi_Agent_Product_User();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Agent_Product_Level_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Agent_Product_Level_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Commission"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Commission=(decimal)ojb;
			}
			ojb = dataReader["Count_Product"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Product= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_product_change"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_product_change= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_product_change_used"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_product_change_used= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsFailure"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsFailure= Convert.ToInt32(ojb);
			}
			model.Remark=dataReader["Remark"].ToString();
			ojb = dataReader["Time_add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_add=(DateTime)ojb;
			}
			ojb = dataReader["Time_end"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_end=(DateTime)ojb;
			}
			ojb = dataReader["User_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id= Convert.ToInt32(ojb);
			}
			model.User_UserName=dataReader["User_UserName"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

