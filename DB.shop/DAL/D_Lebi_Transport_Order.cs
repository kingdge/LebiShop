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
	/// 数据访问类D_Lebi_Transport_Order。
	/// </summary>
	public partial class D_Lebi_Transport_Order
	{
		static D_Lebi_Transport_Order _Instance;
		public static D_Lebi_Transport_Order Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Transport_Order("Lebi_Transport_Order");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Transport_Order";
		public D_Lebi_Transport_Order(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Order", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Order", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Order" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Order", 0 , cachestr,seconds);
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
		public int Add(Lebi_Transport_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("AdminName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("HtmlLog")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsHaveNewLog")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Log")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ramark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_SubName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Address")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_MobilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Received")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_TransportOrderStatus")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+")");
			strSql.Append(" values (");
			strSql.Append("@Admin_id,@AdminName,@Code,@HtmlLog,@IsHaveNewLog,@Log,@Money,@Order_id,@Product,@Ramark,@Supplier_id,@Supplier_SubName,@T_Address,@T_Email,@T_MobilePhone,@T_Name,@T_Phone,@Time_Add,@Time_Received,@Transport_Code,@Transport_id,@Transport_Name,@Type_id_TransportOrderStatus,@User_id);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@AdminName", model.AdminName),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@HtmlLog", model.HtmlLog),
					new SqlParameter("@IsHaveNewLog", model.IsHaveNewLog),
					new SqlParameter("@Log", model.Log),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@Product", model.Product),
					new SqlParameter("@Ramark", model.Ramark),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Supplier_SubName", model.Supplier_SubName),
					new SqlParameter("@T_Address", model.T_Address),
					new SqlParameter("@T_Email", model.T_Email),
					new SqlParameter("@T_MobilePhone", model.T_MobilePhone),
					new SqlParameter("@T_Name", model.T_Name),
					new SqlParameter("@T_Phone", model.T_Phone),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Received", model.Time_Received),
					new SqlParameter("@Transport_Code", model.Transport_Code),
					new SqlParameter("@Transport_id", model.Transport_id),
					new SqlParameter("@Transport_Name", model.Transport_Name),
					new SqlParameter("@Type_id_TransportOrderStatus", model.Type_id_TransportOrderStatus),
					new SqlParameter("@User_id", model.User_id)};

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
		public void Update(Lebi_Transport_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Admin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+"= @Admin_id");
			if((","+model.UpdateCols+",").IndexOf(",AdminName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("AdminName")+"= @AdminName");
			if((","+model.UpdateCols+",").IndexOf(",Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+"= @Code");
			if((","+model.UpdateCols+",").IndexOf(",HtmlLog,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("HtmlLog")+"= @HtmlLog");
			if((","+model.UpdateCols+",").IndexOf(",IsHaveNewLog,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsHaveNewLog")+"= @IsHaveNewLog");
			if((","+model.UpdateCols+",").IndexOf(",Log,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Log")+"= @Log");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Order_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+"= @Order_id");
			if((","+model.UpdateCols+",").IndexOf(",Product,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product")+"= @Product");
			if((","+model.UpdateCols+",").IndexOf(",Ramark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ramark")+"= @Ramark");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_SubName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_SubName")+"= @Supplier_SubName");
			if((","+model.UpdateCols+",").IndexOf(",T_Address,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Address")+"= @T_Address");
			if((","+model.UpdateCols+",").IndexOf(",T_Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Email")+"= @T_Email");
			if((","+model.UpdateCols+",").IndexOf(",T_MobilePhone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_MobilePhone")+"= @T_MobilePhone");
			if((","+model.UpdateCols+",").IndexOf(",T_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Name")+"= @T_Name");
			if((","+model.UpdateCols+",").IndexOf(",T_Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Phone")+"= @T_Phone");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Received,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Received")+"= @Time_Received");
			if((","+model.UpdateCols+",").IndexOf(",Transport_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Code")+"= @Transport_Code");
			if((","+model.UpdateCols+",").IndexOf(",Transport_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_id")+"= @Transport_id");
			if((","+model.UpdateCols+",").IndexOf(",Transport_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Name")+"= @Transport_Name");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_TransportOrderStatus,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_TransportOrderStatus")+"= @Type_id_TransportOrderStatus");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@AdminName", model.AdminName),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@HtmlLog", model.HtmlLog),
					new SqlParameter("@IsHaveNewLog", model.IsHaveNewLog),
					new SqlParameter("@Log", model.Log),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@Product", model.Product),
					new SqlParameter("@Ramark", model.Ramark),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Supplier_SubName", model.Supplier_SubName),
					new SqlParameter("@T_Address", model.T_Address),
					new SqlParameter("@T_Email", model.T_Email),
					new SqlParameter("@T_MobilePhone", model.T_MobilePhone),
					new SqlParameter("@T_Name", model.T_Name),
					new SqlParameter("@T_Phone", model.T_Phone),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Received", model.Time_Received),
					new SqlParameter("@Transport_Code", model.Transport_Code),
					new SqlParameter("@Transport_id", model.Transport_id),
					new SqlParameter("@Transport_Name", model.Transport_Name),
					new SqlParameter("@Type_id_TransportOrderStatus", model.Type_id_TransportOrderStatus),
					new SqlParameter("@User_id", model.User_id)};
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
		public Lebi_Transport_Order GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Transport_Order;
		   }
		   Lebi_Transport_Order model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Transport_Order",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Transport_Order GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Transport_Order;
		   }
		   Lebi_Transport_Order model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Transport_Order",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Transport_Order GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Transport_Order;
		   }
		   Lebi_Transport_Order model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Transport_Order",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Transport_Order> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Transport_Order>;
		   }
		   List<Lebi_Transport_Order> list = new List<Lebi_Transport_Order>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Order", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Transport_Order> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Transport_Order>;
		   }
		   List<Lebi_Transport_Order> list = new List<Lebi_Transport_Order>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Order", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Transport_Order> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Transport_Order>;
		   }
		   List<Lebi_Transport_Order> list = new List<Lebi_Transport_Order>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Order", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Transport_Order> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Transport_Order>;
		   }
		   List<Lebi_Transport_Order> list = new List<Lebi_Transport_Order>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Order", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Transport_Order BindForm(Lebi_Transport_Order model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["AdminName"] != null)
				model.AdminName=LB.Tools.RequestTool.RequestString("AdminName");
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestString("Code");
			if (HttpContext.Current.Request["HtmlLog"] != null)
				model.HtmlLog=LB.Tools.RequestTool.RequestString("HtmlLog");
			if (HttpContext.Current.Request["IsHaveNewLog"] != null)
				model.IsHaveNewLog=LB.Tools.RequestTool.RequestInt("IsHaveNewLog",0);
			if (HttpContext.Current.Request["Log"] != null)
				model.Log=LB.Tools.RequestTool.RequestString("Log");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["Product"] != null)
				model.Product=LB.Tools.RequestTool.RequestString("Product");
			if (HttpContext.Current.Request["Ramark"] != null)
				model.Ramark=LB.Tools.RequestTool.RequestString("Ramark");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Supplier_SubName"] != null)
				model.Supplier_SubName=LB.Tools.RequestTool.RequestString("Supplier_SubName");
			if (HttpContext.Current.Request["T_Address"] != null)
				model.T_Address=LB.Tools.RequestTool.RequestString("T_Address");
			if (HttpContext.Current.Request["T_Email"] != null)
				model.T_Email=LB.Tools.RequestTool.RequestString("T_Email");
			if (HttpContext.Current.Request["T_MobilePhone"] != null)
				model.T_MobilePhone=LB.Tools.RequestTool.RequestString("T_MobilePhone");
			if (HttpContext.Current.Request["T_Name"] != null)
				model.T_Name=LB.Tools.RequestTool.RequestString("T_Name");
			if (HttpContext.Current.Request["T_Phone"] != null)
				model.T_Phone=LB.Tools.RequestTool.RequestString("T_Phone");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Received"] != null)
				model.Time_Received=LB.Tools.RequestTool.RequestTime("Time_Received", System.DateTime.Now);
			if (HttpContext.Current.Request["Transport_Code"] != null)
				model.Transport_Code=LB.Tools.RequestTool.RequestString("Transport_Code");
			if (HttpContext.Current.Request["Transport_id"] != null)
				model.Transport_id=LB.Tools.RequestTool.RequestInt("Transport_id",0);
			if (HttpContext.Current.Request["Transport_Name"] != null)
				model.Transport_Name=LB.Tools.RequestTool.RequestString("Transport_Name");
			if (HttpContext.Current.Request["Type_id_TransportOrderStatus"] != null)
				model.Type_id_TransportOrderStatus=LB.Tools.RequestTool.RequestInt("Type_id_TransportOrderStatus",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Transport_Order SafeBindForm(Lebi_Transport_Order model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["AdminName"] != null)
				model.AdminName=LB.Tools.RequestTool.RequestSafeString("AdminName");
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestSafeString("Code");
			if (HttpContext.Current.Request["HtmlLog"] != null)
				model.HtmlLog=LB.Tools.RequestTool.RequestSafeString("HtmlLog");
			if (HttpContext.Current.Request["IsHaveNewLog"] != null)
				model.IsHaveNewLog=LB.Tools.RequestTool.RequestInt("IsHaveNewLog",0);
			if (HttpContext.Current.Request["Log"] != null)
				model.Log=LB.Tools.RequestTool.RequestSafeString("Log");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["Product"] != null)
				model.Product=LB.Tools.RequestTool.RequestSafeString("Product");
			if (HttpContext.Current.Request["Ramark"] != null)
				model.Ramark=LB.Tools.RequestTool.RequestSafeString("Ramark");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Supplier_SubName"] != null)
				model.Supplier_SubName=LB.Tools.RequestTool.RequestSafeString("Supplier_SubName");
			if (HttpContext.Current.Request["T_Address"] != null)
				model.T_Address=LB.Tools.RequestTool.RequestSafeString("T_Address");
			if (HttpContext.Current.Request["T_Email"] != null)
				model.T_Email=LB.Tools.RequestTool.RequestSafeString("T_Email");
			if (HttpContext.Current.Request["T_MobilePhone"] != null)
				model.T_MobilePhone=LB.Tools.RequestTool.RequestSafeString("T_MobilePhone");
			if (HttpContext.Current.Request["T_Name"] != null)
				model.T_Name=LB.Tools.RequestTool.RequestSafeString("T_Name");
			if (HttpContext.Current.Request["T_Phone"] != null)
				model.T_Phone=LB.Tools.RequestTool.RequestSafeString("T_Phone");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Received"] != null)
				model.Time_Received=LB.Tools.RequestTool.RequestTime("Time_Received", System.DateTime.Now);
			if (HttpContext.Current.Request["Transport_Code"] != null)
				model.Transport_Code=LB.Tools.RequestTool.RequestSafeString("Transport_Code");
			if (HttpContext.Current.Request["Transport_id"] != null)
				model.Transport_id=LB.Tools.RequestTool.RequestInt("Transport_id",0);
			if (HttpContext.Current.Request["Transport_Name"] != null)
				model.Transport_Name=LB.Tools.RequestTool.RequestSafeString("Transport_Name");
			if (HttpContext.Current.Request["Type_id_TransportOrderStatus"] != null)
				model.Type_id_TransportOrderStatus=LB.Tools.RequestTool.RequestInt("Type_id_TransportOrderStatus",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Transport_Order ReaderBind(IDataReader dataReader)
		{
			Lebi_Transport_Order model=new Lebi_Transport_Order();
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
			model.AdminName=dataReader["AdminName"].ToString();
			model.Code=dataReader["Code"].ToString();
			model.HtmlLog=dataReader["HtmlLog"].ToString();
			ojb = dataReader["IsHaveNewLog"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsHaveNewLog= Convert.ToInt32(ojb);
			}
			model.Log=dataReader["Log"].ToString();
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			ojb = dataReader["Order_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Order_id= Convert.ToInt32(ojb);
			}
			model.Product=dataReader["Product"].ToString();
			model.Ramark=dataReader["Ramark"].ToString();
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			model.Supplier_SubName=dataReader["Supplier_SubName"].ToString();
			model.T_Address=dataReader["T_Address"].ToString();
			model.T_Email=dataReader["T_Email"].ToString();
			model.T_MobilePhone=dataReader["T_MobilePhone"].ToString();
			model.T_Name=dataReader["T_Name"].ToString();
			model.T_Phone=dataReader["T_Phone"].ToString();
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Received"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Received=(DateTime)ojb;
			}
			model.Transport_Code=dataReader["Transport_Code"].ToString();
			ojb = dataReader["Transport_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Transport_id= Convert.ToInt32(ojb);
			}
			model.Transport_Name=dataReader["Transport_Name"].ToString();
			ojb = dataReader["Type_id_TransportOrderStatus"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_TransportOrderStatus= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

