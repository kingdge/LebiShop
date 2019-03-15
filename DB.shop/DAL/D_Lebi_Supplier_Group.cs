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
	/// 数据访问类D_Lebi_Supplier_Group。
	/// </summary>
	public partial class D_Lebi_Supplier_Group
	{
		static D_Lebi_Supplier_Group _Instance;
		public static D_Lebi_Supplier_Group Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Supplier_Group("Lebi_Supplier_Group");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Supplier_Group";
		public D_Lebi_Supplier_Group(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier_Group", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier_Group", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier_Group" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier_Group", 0 , cachestr,seconds);
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
		public int Add(Lebi_Supplier_Group model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillingDays")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Days_checkuserlow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Grade")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSubmit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("MarginPrice")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Menu_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Menu_ids_index")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProductTop")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ServiceDays")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ServicePrice")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Skin_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("type")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserTop")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Verified_ids")+")");
			strSql.Append(" values (");
			strSql.Append("@BillingDays,@Days_checkuserlow,@Grade,@IsShow,@IsSubmit,@MarginPrice,@Menu_ids,@Menu_ids_index,@Name,@ProductTop,@Remark,@ServiceDays,@ServicePrice,@Sort,@Supplier_Skin_ids,@type,@UserLow,@UserTop,@Verified_ids);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@BillingDays", model.BillingDays),
					new SqlParameter("@Days_checkuserlow", model.Days_checkuserlow),
					new SqlParameter("@Grade", model.Grade),
					new SqlParameter("@IsShow", model.IsShow),
					new SqlParameter("@IsSubmit", model.IsSubmit),
					new SqlParameter("@MarginPrice", model.MarginPrice),
					new SqlParameter("@Menu_ids", model.Menu_ids),
					new SqlParameter("@Menu_ids_index", model.Menu_ids_index),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@ProductTop", model.ProductTop),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@ServiceDays", model.ServiceDays),
					new SqlParameter("@ServicePrice", model.ServicePrice),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Supplier_Skin_ids", model.Supplier_Skin_ids),
					new SqlParameter("@type", model.type),
					new SqlParameter("@UserLow", model.UserLow),
					new SqlParameter("@UserTop", model.UserTop),
					new SqlParameter("@Verified_ids", model.Verified_ids)};

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
		public void Update(Lebi_Supplier_Group model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",BillingDays,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillingDays")+"= @BillingDays");
			if((","+model.UpdateCols+",").IndexOf(",Days_checkuserlow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Days_checkuserlow")+"= @Days_checkuserlow");
			if((","+model.UpdateCols+",").IndexOf(",Grade,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Grade")+"= @Grade");
			if((","+model.UpdateCols+",").IndexOf(",IsShow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShow")+"= @IsShow");
			if((","+model.UpdateCols+",").IndexOf(",IsSubmit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSubmit")+"= @IsSubmit");
			if((","+model.UpdateCols+",").IndexOf(",MarginPrice,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("MarginPrice")+"= @MarginPrice");
			if((","+model.UpdateCols+",").IndexOf(",Menu_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Menu_ids")+"= @Menu_ids");
			if((","+model.UpdateCols+",").IndexOf(",Menu_ids_index,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Menu_ids_index")+"= @Menu_ids_index");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",ProductTop,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProductTop")+"= @ProductTop");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",ServiceDays,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ServiceDays")+"= @ServiceDays");
			if((","+model.UpdateCols+",").IndexOf(",ServicePrice,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ServicePrice")+"= @ServicePrice");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_Skin_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Skin_ids")+"= @Supplier_Skin_ids");
			if((","+model.UpdateCols+",").IndexOf(",type,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("type")+"= @type");
			if((","+model.UpdateCols+",").IndexOf(",UserLow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLow")+"= @UserLow");
			if((","+model.UpdateCols+",").IndexOf(",UserTop,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserTop")+"= @UserTop");
			if((","+model.UpdateCols+",").IndexOf(",Verified_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Verified_ids")+"= @Verified_ids");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@BillingDays", model.BillingDays),
					new SqlParameter("@Days_checkuserlow", model.Days_checkuserlow),
					new SqlParameter("@Grade", model.Grade),
					new SqlParameter("@IsShow", model.IsShow),
					new SqlParameter("@IsSubmit", model.IsSubmit),
					new SqlParameter("@MarginPrice", model.MarginPrice),
					new SqlParameter("@Menu_ids", model.Menu_ids),
					new SqlParameter("@Menu_ids_index", model.Menu_ids_index),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@ProductTop", model.ProductTop),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@ServiceDays", model.ServiceDays),
					new SqlParameter("@ServicePrice", model.ServicePrice),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Supplier_Skin_ids", model.Supplier_Skin_ids),
					new SqlParameter("@type", model.type),
					new SqlParameter("@UserLow", model.UserLow),
					new SqlParameter("@UserTop", model.UserTop),
					new SqlParameter("@Verified_ids", model.Verified_ids)};
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
		public Lebi_Supplier_Group GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Supplier_Group;
		   }
		   Lebi_Supplier_Group model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Supplier_Group",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Supplier_Group GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Supplier_Group;
		   }
		   Lebi_Supplier_Group model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Supplier_Group",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Supplier_Group GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Supplier_Group;
		   }
		   Lebi_Supplier_Group model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Supplier_Group",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Supplier_Group> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Supplier_Group>;
		   }
		   List<Lebi_Supplier_Group> list = new List<Lebi_Supplier_Group>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier_Group", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Supplier_Group> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Supplier_Group>;
		   }
		   List<Lebi_Supplier_Group> list = new List<Lebi_Supplier_Group>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier_Group", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Supplier_Group> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Supplier_Group>;
		   }
		   List<Lebi_Supplier_Group> list = new List<Lebi_Supplier_Group>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier_Group", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Supplier_Group> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Supplier_Group>;
		   }
		   List<Lebi_Supplier_Group> list = new List<Lebi_Supplier_Group>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier_Group", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Supplier_Group BindForm(Lebi_Supplier_Group model)
		{
			if (HttpContext.Current.Request["BillingDays"] != null)
				model.BillingDays=LB.Tools.RequestTool.RequestInt("BillingDays",0);
			if (HttpContext.Current.Request["Days_checkuserlow"] != null)
				model.Days_checkuserlow=LB.Tools.RequestTool.RequestInt("Days_checkuserlow",0);
			if (HttpContext.Current.Request["Grade"] != null)
				model.Grade=LB.Tools.RequestTool.RequestInt("Grade",0);
			if (HttpContext.Current.Request["IsShow"] != null)
				model.IsShow=LB.Tools.RequestTool.RequestInt("IsShow",0);
			if (HttpContext.Current.Request["IsSubmit"] != null)
				model.IsSubmit=LB.Tools.RequestTool.RequestInt("IsSubmit",0);
			if (HttpContext.Current.Request["MarginPrice"] != null)
				model.MarginPrice=LB.Tools.RequestTool.RequestDecimal("MarginPrice",0);
			if (HttpContext.Current.Request["Menu_ids"] != null)
				model.Menu_ids=LB.Tools.RequestTool.RequestString("Menu_ids");
			if (HttpContext.Current.Request["Menu_ids_index"] != null)
				model.Menu_ids_index=LB.Tools.RequestTool.RequestString("Menu_ids_index");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["ProductTop"] != null)
				model.ProductTop=LB.Tools.RequestTool.RequestInt("ProductTop",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["ServiceDays"] != null)
				model.ServiceDays=LB.Tools.RequestTool.RequestInt("ServiceDays",0);
			if (HttpContext.Current.Request["ServicePrice"] != null)
				model.ServicePrice=LB.Tools.RequestTool.RequestDecimal("ServicePrice",0);
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Supplier_Skin_ids"] != null)
				model.Supplier_Skin_ids=LB.Tools.RequestTool.RequestString("Supplier_Skin_ids");
			if (HttpContext.Current.Request["type"] != null)
				model.type=LB.Tools.RequestTool.RequestString("type");
			if (HttpContext.Current.Request["UserLow"] != null)
				model.UserLow=LB.Tools.RequestTool.RequestInt("UserLow",0);
			if (HttpContext.Current.Request["UserTop"] != null)
				model.UserTop=LB.Tools.RequestTool.RequestInt("UserTop",0);
			if (HttpContext.Current.Request["Verified_ids"] != null)
				model.Verified_ids=LB.Tools.RequestTool.RequestString("Verified_ids");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Supplier_Group SafeBindForm(Lebi_Supplier_Group model)
		{
			if (HttpContext.Current.Request["BillingDays"] != null)
				model.BillingDays=LB.Tools.RequestTool.RequestInt("BillingDays",0);
			if (HttpContext.Current.Request["Days_checkuserlow"] != null)
				model.Days_checkuserlow=LB.Tools.RequestTool.RequestInt("Days_checkuserlow",0);
			if (HttpContext.Current.Request["Grade"] != null)
				model.Grade=LB.Tools.RequestTool.RequestInt("Grade",0);
			if (HttpContext.Current.Request["IsShow"] != null)
				model.IsShow=LB.Tools.RequestTool.RequestInt("IsShow",0);
			if (HttpContext.Current.Request["IsSubmit"] != null)
				model.IsSubmit=LB.Tools.RequestTool.RequestInt("IsSubmit",0);
			if (HttpContext.Current.Request["MarginPrice"] != null)
				model.MarginPrice=LB.Tools.RequestTool.RequestDecimal("MarginPrice",0);
			if (HttpContext.Current.Request["Menu_ids"] != null)
				model.Menu_ids=LB.Tools.RequestTool.RequestSafeString("Menu_ids");
			if (HttpContext.Current.Request["Menu_ids_index"] != null)
				model.Menu_ids_index=LB.Tools.RequestTool.RequestSafeString("Menu_ids_index");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["ProductTop"] != null)
				model.ProductTop=LB.Tools.RequestTool.RequestInt("ProductTop",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["ServiceDays"] != null)
				model.ServiceDays=LB.Tools.RequestTool.RequestInt("ServiceDays",0);
			if (HttpContext.Current.Request["ServicePrice"] != null)
				model.ServicePrice=LB.Tools.RequestTool.RequestDecimal("ServicePrice",0);
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Supplier_Skin_ids"] != null)
				model.Supplier_Skin_ids=LB.Tools.RequestTool.RequestSafeString("Supplier_Skin_ids");
			if (HttpContext.Current.Request["type"] != null)
				model.type=LB.Tools.RequestTool.RequestSafeString("type");
			if (HttpContext.Current.Request["UserLow"] != null)
				model.UserLow=LB.Tools.RequestTool.RequestInt("UserLow",0);
			if (HttpContext.Current.Request["UserTop"] != null)
				model.UserTop=LB.Tools.RequestTool.RequestInt("UserTop",0);
			if (HttpContext.Current.Request["Verified_ids"] != null)
				model.Verified_ids=LB.Tools.RequestTool.RequestSafeString("Verified_ids");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Supplier_Group ReaderBind(IDataReader dataReader)
		{
			Lebi_Supplier_Group model=new Lebi_Supplier_Group();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["BillingDays"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BillingDays= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Days_checkuserlow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Days_checkuserlow= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Grade"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Grade= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsShow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsShow= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSubmit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSubmit= Convert.ToInt32(ojb);
			}
			ojb = dataReader["MarginPrice"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.MarginPrice=(decimal)ojb;
			}
			model.Menu_ids=dataReader["Menu_ids"].ToString();
			model.Menu_ids_index=dataReader["Menu_ids_index"].ToString();
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["ProductTop"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ProductTop= Convert.ToInt32(ojb);
			}
			model.Remark=dataReader["Remark"].ToString();
			ojb = dataReader["ServiceDays"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ServiceDays= Convert.ToInt32(ojb);
			}
			ojb = dataReader["ServicePrice"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ServicePrice=(decimal)ojb;
			}
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			model.Supplier_Skin_ids=dataReader["Supplier_Skin_ids"].ToString();
			model.type=dataReader["type"].ToString();
			ojb = dataReader["UserLow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserLow= Convert.ToInt32(ojb);
			}
			ojb = dataReader["UserTop"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserTop= Convert.ToInt32(ojb);
			}
			model.Verified_ids=dataReader["Verified_ids"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

