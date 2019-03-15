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
	/// 数据访问类D_Lebi_CardOrder。
	/// </summary>
	public partial class D_Lebi_CardOrder
	{
		static D_Lebi_CardOrder _Instance;
		public static D_Lebi_CardOrder Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_CardOrder("Lebi_CardOrder");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_CardOrder";
		public D_Lebi_CardOrder(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_CardOrder", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_CardOrder", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_CardOrder" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_CardOrder", 0 , cachestr,seconds);
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
		public int Add(Lebi_CardOrder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IndexCode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCanOtherUse")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPayOnce")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Length")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Buy")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NO_End")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NO_Now")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NO_Start")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Begin")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_CardType")+")");
			strSql.Append(" values (");
			strSql.Append("@Admin_id,@IndexCode,@IsCanOtherUse,@IsPayOnce,@Length,@Money,@Money_Buy,@Name,@NO_End,@NO_Now,@NO_Start,@Pro_Type_ids,@Time_Add,@Time_Begin,@Time_End,@Time_Update,@Type_id_CardType);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@IndexCode", model.IndexCode),
					new SqlParameter("@IsCanOtherUse", model.IsCanOtherUse),
					new SqlParameter("@IsPayOnce", model.IsPayOnce),
					new SqlParameter("@Length", model.Length),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Buy", model.Money_Buy),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@NO_End", model.NO_End),
					new SqlParameter("@NO_Now", model.NO_Now),
					new SqlParameter("@NO_Start", model.NO_Start),
					new SqlParameter("@Pro_Type_ids", model.Pro_Type_ids),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Begin", model.Time_Begin),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Type_id_CardType", model.Type_id_CardType)};

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
		public void Update(Lebi_CardOrder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Admin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+"= @Admin_id");
			if((","+model.UpdateCols+",").IndexOf(",IndexCode,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IndexCode")+"= @IndexCode");
			if((","+model.UpdateCols+",").IndexOf(",IsCanOtherUse,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCanOtherUse")+"= @IsCanOtherUse");
			if((","+model.UpdateCols+",").IndexOf(",IsPayOnce,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPayOnce")+"= @IsPayOnce");
			if((","+model.UpdateCols+",").IndexOf(",Length,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Length")+"= @Length");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Money_Buy,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Buy")+"= @Money_Buy");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",NO_End,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NO_End")+"= @NO_End");
			if((","+model.UpdateCols+",").IndexOf(",NO_Now,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NO_Now")+"= @NO_Now");
			if((","+model.UpdateCols+",").IndexOf(",NO_Start,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NO_Start")+"= @NO_Start");
			if((","+model.UpdateCols+",").IndexOf(",Pro_Type_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_ids")+"= @Pro_Type_ids");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Begin,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Begin")+"= @Time_Begin");
			if((","+model.UpdateCols+",").IndexOf(",Time_End,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+"= @Time_End");
			if((","+model.UpdateCols+",").IndexOf(",Time_Update,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+"= @Time_Update");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_CardType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_CardType")+"= @Type_id_CardType");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@IndexCode", model.IndexCode),
					new SqlParameter("@IsCanOtherUse", model.IsCanOtherUse),
					new SqlParameter("@IsPayOnce", model.IsPayOnce),
					new SqlParameter("@Length", model.Length),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Buy", model.Money_Buy),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@NO_End", model.NO_End),
					new SqlParameter("@NO_Now", model.NO_Now),
					new SqlParameter("@NO_Start", model.NO_Start),
					new SqlParameter("@Pro_Type_ids", model.Pro_Type_ids),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Begin", model.Time_Begin),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Type_id_CardType", model.Type_id_CardType)};
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
		public Lebi_CardOrder GetModel(int id, int seconds=0)
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
		           return obj as Lebi_CardOrder;
		   }
		   Lebi_CardOrder model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_CardOrder",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_CardOrder GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_CardOrder;
		   }
		   Lebi_CardOrder model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_CardOrder",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_CardOrder GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_CardOrder;
		   }
		   Lebi_CardOrder model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_CardOrder",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_CardOrder> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_CardOrder>;
		   }
		   List<Lebi_CardOrder> list = new List<Lebi_CardOrder>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_CardOrder", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_CardOrder> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_CardOrder>;
		   }
		   List<Lebi_CardOrder> list = new List<Lebi_CardOrder>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_CardOrder", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_CardOrder> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_CardOrder>;
		   }
		   List<Lebi_CardOrder> list = new List<Lebi_CardOrder>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_CardOrder", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_CardOrder> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_CardOrder>;
		   }
		   List<Lebi_CardOrder> list = new List<Lebi_CardOrder>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_CardOrder", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_CardOrder BindForm(Lebi_CardOrder model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["IndexCode"] != null)
				model.IndexCode=LB.Tools.RequestTool.RequestString("IndexCode");
			if (HttpContext.Current.Request["IsCanOtherUse"] != null)
				model.IsCanOtherUse=LB.Tools.RequestTool.RequestInt("IsCanOtherUse",0);
			if (HttpContext.Current.Request["IsPayOnce"] != null)
				model.IsPayOnce=LB.Tools.RequestTool.RequestInt("IsPayOnce",0);
			if (HttpContext.Current.Request["Length"] != null)
				model.Length=LB.Tools.RequestTool.RequestInt("Length",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Buy"] != null)
				model.Money_Buy=LB.Tools.RequestTool.RequestDecimal("Money_Buy",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["NO_End"] != null)
				model.NO_End=LB.Tools.RequestTool.RequestInt("NO_End",0);
			if (HttpContext.Current.Request["NO_Now"] != null)
				model.NO_Now=LB.Tools.RequestTool.RequestInt("NO_Now",0);
			if (HttpContext.Current.Request["NO_Start"] != null)
				model.NO_Start=LB.Tools.RequestTool.RequestInt("NO_Start",0);
			if (HttpContext.Current.Request["Pro_Type_ids"] != null)
				model.Pro_Type_ids=LB.Tools.RequestTool.RequestString("Pro_Type_ids");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Begin"] != null)
				model.Time_Begin=LB.Tools.RequestTool.RequestTime("Time_Begin", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_CardType"] != null)
				model.Type_id_CardType=LB.Tools.RequestTool.RequestInt("Type_id_CardType",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_CardOrder SafeBindForm(Lebi_CardOrder model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["IndexCode"] != null)
				model.IndexCode=LB.Tools.RequestTool.RequestSafeString("IndexCode");
			if (HttpContext.Current.Request["IsCanOtherUse"] != null)
				model.IsCanOtherUse=LB.Tools.RequestTool.RequestInt("IsCanOtherUse",0);
			if (HttpContext.Current.Request["IsPayOnce"] != null)
				model.IsPayOnce=LB.Tools.RequestTool.RequestInt("IsPayOnce",0);
			if (HttpContext.Current.Request["Length"] != null)
				model.Length=LB.Tools.RequestTool.RequestInt("Length",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Buy"] != null)
				model.Money_Buy=LB.Tools.RequestTool.RequestDecimal("Money_Buy",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["NO_End"] != null)
				model.NO_End=LB.Tools.RequestTool.RequestInt("NO_End",0);
			if (HttpContext.Current.Request["NO_Now"] != null)
				model.NO_Now=LB.Tools.RequestTool.RequestInt("NO_Now",0);
			if (HttpContext.Current.Request["NO_Start"] != null)
				model.NO_Start=LB.Tools.RequestTool.RequestInt("NO_Start",0);
			if (HttpContext.Current.Request["Pro_Type_ids"] != null)
				model.Pro_Type_ids=LB.Tools.RequestTool.RequestSafeString("Pro_Type_ids");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Begin"] != null)
				model.Time_Begin=LB.Tools.RequestTool.RequestTime("Time_Begin", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_CardType"] != null)
				model.Type_id_CardType=LB.Tools.RequestTool.RequestInt("Type_id_CardType",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_CardOrder ReaderBind(IDataReader dataReader)
		{
			Lebi_CardOrder model=new Lebi_CardOrder();
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
			model.IndexCode=dataReader["IndexCode"].ToString();
			ojb = dataReader["IsCanOtherUse"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCanOtherUse= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsPayOnce"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsPayOnce= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Length"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Length= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			ojb = dataReader["Money_Buy"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Buy=(decimal)ojb;
			}
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["NO_End"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.NO_End= Convert.ToInt32(ojb);
			}
			ojb = dataReader["NO_Now"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.NO_Now= Convert.ToInt32(ojb);
			}
			ojb = dataReader["NO_Start"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.NO_Start= Convert.ToInt32(ojb);
			}
			model.Pro_Type_ids=dataReader["Pro_Type_ids"].ToString();
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Begin"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Begin=(DateTime)ojb;
			}
			ojb = dataReader["Time_End"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_End=(DateTime)ojb;
			}
			ojb = dataReader["Time_Update"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Update=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_CardType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_CardType= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

