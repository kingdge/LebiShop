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
	/// 数据访问类D_Lebi_Agent_Money。
	/// </summary>
	public partial class D_Lebi_Agent_Money
	{
		static D_Lebi_Agent_Money _Instance;
		public static D_Lebi_Agent_Money Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Agent_Money("Lebi_Agent_Money");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Agent_Money";
		public D_Lebi_Agent_Money(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Money", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Money", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Money" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Agent_Money", 0 , cachestr,seconds);
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
		public int Add(Lebi_Agent_Money model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id_parent")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Number")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_AgentMoneyStatus")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_AgentMoneyType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+")");
			strSql.Append(" values (");
			strSql.Append("@Area_id,@DT_id,@Money,@Order_Code,@Order_id,@Product_id,@Product_id_parent,@Product_Number,@Remark,@Supplier_id,@Time_add,@Type_id_AgentMoneyStatus,@Type_id_AgentMoneyType,@User_id,@User_UserName);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Order_Code", model.Order_Code),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@Product_id_parent", model.Product_id_parent),
					new SqlParameter("@Product_Number", model.Product_Number),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Type_id_AgentMoneyStatus", model.Type_id_AgentMoneyStatus),
					new SqlParameter("@Type_id_AgentMoneyType", model.Type_id_AgentMoneyType),
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
		public void Update(Lebi_Agent_Money model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Area_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+"= @Area_id");
			if((","+model.UpdateCols+",").IndexOf(",DT_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+"= @DT_id");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Order_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_Code")+"= @Order_Code");
			if((","+model.UpdateCols+",").IndexOf(",Order_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+"= @Order_id");
			if((","+model.UpdateCols+",").IndexOf(",Product_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+"= @Product_id");
			if((","+model.UpdateCols+",").IndexOf(",Product_id_parent,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id_parent")+"= @Product_id_parent");
			if((","+model.UpdateCols+",").IndexOf(",Product_Number,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Number")+"= @Product_Number");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",Time_add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+"= @Time_add");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_AgentMoneyStatus,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_AgentMoneyStatus")+"= @Type_id_AgentMoneyStatus");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_AgentMoneyType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_AgentMoneyType")+"= @Type_id_AgentMoneyType");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",User_UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+"= @User_UserName");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Order_Code", model.Order_Code),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@Product_id_parent", model.Product_id_parent),
					new SqlParameter("@Product_Number", model.Product_Number),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Type_id_AgentMoneyStatus", model.Type_id_AgentMoneyStatus),
					new SqlParameter("@Type_id_AgentMoneyType", model.Type_id_AgentMoneyType),
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
		public Lebi_Agent_Money GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Agent_Money;
		   }
		   Lebi_Agent_Money model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Agent_Money",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Agent_Money GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Agent_Money;
		   }
		   Lebi_Agent_Money model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Agent_Money",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Agent_Money GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Agent_Money;
		   }
		   Lebi_Agent_Money model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Agent_Money",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Agent_Money> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Agent_Money>;
		   }
		   List<Lebi_Agent_Money> list = new List<Lebi_Agent_Money>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Money", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Agent_Money> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Agent_Money>;
		   }
		   List<Lebi_Agent_Money> list = new List<Lebi_Agent_Money>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Money", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Agent_Money> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Agent_Money>;
		   }
		   List<Lebi_Agent_Money> list = new List<Lebi_Agent_Money>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Money", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Agent_Money> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Agent_Money>;
		   }
		   List<Lebi_Agent_Money> list = new List<Lebi_Agent_Money>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Agent_Money", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Agent_Money BindForm(Lebi_Agent_Money model)
		{
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Order_Code"] != null)
				model.Order_Code=LB.Tools.RequestTool.RequestString("Order_Code");
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["Product_id_parent"] != null)
				model.Product_id_parent=LB.Tools.RequestTool.RequestInt("Product_id_parent",0);
			if (HttpContext.Current.Request["Product_Number"] != null)
				model.Product_Number=LB.Tools.RequestTool.RequestString("Product_Number");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_AgentMoneyStatus"] != null)
				model.Type_id_AgentMoneyStatus=LB.Tools.RequestTool.RequestInt("Type_id_AgentMoneyStatus",0);
			if (HttpContext.Current.Request["Type_id_AgentMoneyType"] != null)
				model.Type_id_AgentMoneyType=LB.Tools.RequestTool.RequestInt("Type_id_AgentMoneyType",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestString("User_UserName");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Agent_Money SafeBindForm(Lebi_Agent_Money model)
		{
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Order_Code"] != null)
				model.Order_Code=LB.Tools.RequestTool.RequestSafeString("Order_Code");
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["Product_id_parent"] != null)
				model.Product_id_parent=LB.Tools.RequestTool.RequestInt("Product_id_parent",0);
			if (HttpContext.Current.Request["Product_Number"] != null)
				model.Product_Number=LB.Tools.RequestTool.RequestSafeString("Product_Number");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_AgentMoneyStatus"] != null)
				model.Type_id_AgentMoneyStatus=LB.Tools.RequestTool.RequestInt("Type_id_AgentMoneyStatus",0);
			if (HttpContext.Current.Request["Type_id_AgentMoneyType"] != null)
				model.Type_id_AgentMoneyType=LB.Tools.RequestTool.RequestInt("Type_id_AgentMoneyType",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestSafeString("User_UserName");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Agent_Money ReaderBind(IDataReader dataReader)
		{
			Lebi_Agent_Money model=new Lebi_Agent_Money();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Area_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Area_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["DT_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.DT_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			model.Order_Code=dataReader["Order_Code"].ToString();
			ojb = dataReader["Order_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Order_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Product_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Product_id_parent"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_id_parent= Convert.ToInt32(ojb);
			}
			model.Product_Number=dataReader["Product_Number"].ToString();
			model.Remark=dataReader["Remark"].ToString();
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Time_add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_add=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_AgentMoneyStatus"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_AgentMoneyStatus= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Type_id_AgentMoneyType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_AgentMoneyType= Convert.ToInt32(ojb);
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

