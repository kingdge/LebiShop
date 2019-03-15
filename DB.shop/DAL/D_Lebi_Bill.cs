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
	/// 数据访问类D_Lebi_Bill。
	/// </summary>
	public partial class D_Lebi_Bill
	{
		static D_Lebi_Bill _Instance;
		public static D_Lebi_Bill Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Bill("Lebi_Bill");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Bill";
		public D_Lebi_Bill(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Bill", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Bill", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Bill" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Bill", 0 , cachestr,seconds);
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
		public int Add(Lebi_Bill model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BillNo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Address")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Bank")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Bank_User")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_ExchangeRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Msige")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("TaxRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Finish")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_BillStatus")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_BillType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+")");
			strSql.Append(" values (");
			strSql.Append("@Admin_id,@Admin_UserName,@BillNo,@BillType_id,@Company_Address,@Company_Bank,@Company_Bank_User,@Company_Code,@Company_Name,@Company_Phone,@Content,@Currency_Code,@Currency_ExchangeRate,@Currency_id,@Currency_Msige,@Money,@Order_Code,@Order_id,@TaxRate,@Time_Add,@Time_Finish,@Title,@Type_id_BillStatus,@Type_id_BillType,@User_id,@User_UserName);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@Admin_UserName", model.Admin_UserName),
					new SqlParameter("@BillNo", model.BillNo),
					new SqlParameter("@BillType_id", model.BillType_id),
					new SqlParameter("@Company_Address", model.Company_Address),
					new SqlParameter("@Company_Bank", model.Company_Bank),
					new SqlParameter("@Company_Bank_User", model.Company_Bank_User),
					new SqlParameter("@Company_Code", model.Company_Code),
					new SqlParameter("@Company_Name", model.Company_Name),
					new SqlParameter("@Company_Phone", model.Company_Phone),
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_ExchangeRate", model.Currency_ExchangeRate),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Currency_Msige", model.Currency_Msige),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Order_Code", model.Order_Code),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@TaxRate", model.TaxRate),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Finish", model.Time_Finish),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@Type_id_BillStatus", model.Type_id_BillStatus),
					new SqlParameter("@Type_id_BillType", model.Type_id_BillType),
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
		public void Update(Lebi_Bill model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Admin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+"= @Admin_id");
			if((","+model.UpdateCols+",").IndexOf(",Admin_UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_UserName")+"= @Admin_UserName");
			if((","+model.UpdateCols+",").IndexOf(",BillNo,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillNo")+"= @BillNo");
			if((","+model.UpdateCols+",").IndexOf(",BillType_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_id")+"= @BillType_id");
			if((","+model.UpdateCols+",").IndexOf(",Company_Address,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Address")+"= @Company_Address");
			if((","+model.UpdateCols+",").IndexOf(",Company_Bank,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Bank")+"= @Company_Bank");
			if((","+model.UpdateCols+",").IndexOf(",Company_Bank_User,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Bank_User")+"= @Company_Bank_User");
			if((","+model.UpdateCols+",").IndexOf(",Company_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Code")+"= @Company_Code");
			if((","+model.UpdateCols+",").IndexOf(",Company_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Name")+"= @Company_Name");
			if((","+model.UpdateCols+",").IndexOf(",Company_Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company_Phone")+"= @Company_Phone");
			if((","+model.UpdateCols+",").IndexOf(",Content,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+"= @Content");
			if((","+model.UpdateCols+",").IndexOf(",Currency_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+"= @Currency_Code");
			if((","+model.UpdateCols+",").IndexOf(",Currency_ExchangeRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_ExchangeRate")+"= @Currency_ExchangeRate");
			if((","+model.UpdateCols+",").IndexOf(",Currency_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+"= @Currency_id");
			if((","+model.UpdateCols+",").IndexOf(",Currency_Msige,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Msige")+"= @Currency_Msige");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Order_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_Code")+"= @Order_Code");
			if((","+model.UpdateCols+",").IndexOf(",Order_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+"= @Order_id");
			if((","+model.UpdateCols+",").IndexOf(",TaxRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("TaxRate")+"= @TaxRate");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Finish,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Finish")+"= @Time_Finish");
			if((","+model.UpdateCols+",").IndexOf(",Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+"= @Title");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_BillStatus,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_BillStatus")+"= @Type_id_BillStatus");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_BillType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_BillType")+"= @Type_id_BillType");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",User_UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+"= @User_UserName");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@Admin_UserName", model.Admin_UserName),
					new SqlParameter("@BillNo", model.BillNo),
					new SqlParameter("@BillType_id", model.BillType_id),
					new SqlParameter("@Company_Address", model.Company_Address),
					new SqlParameter("@Company_Bank", model.Company_Bank),
					new SqlParameter("@Company_Bank_User", model.Company_Bank_User),
					new SqlParameter("@Company_Code", model.Company_Code),
					new SqlParameter("@Company_Name", model.Company_Name),
					new SqlParameter("@Company_Phone", model.Company_Phone),
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_ExchangeRate", model.Currency_ExchangeRate),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Currency_Msige", model.Currency_Msige),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Order_Code", model.Order_Code),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@TaxRate", model.TaxRate),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Finish", model.Time_Finish),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@Type_id_BillStatus", model.Type_id_BillStatus),
					new SqlParameter("@Type_id_BillType", model.Type_id_BillType),
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
		public Lebi_Bill GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Bill;
		   }
		   Lebi_Bill model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Bill",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Bill GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Bill;
		   }
		   Lebi_Bill model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Bill",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Bill GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Bill;
		   }
		   Lebi_Bill model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Bill",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Bill> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Bill>;
		   }
		   List<Lebi_Bill> list = new List<Lebi_Bill>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Bill", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Bill> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Bill>;
		   }
		   List<Lebi_Bill> list = new List<Lebi_Bill>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Bill", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Bill> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Bill>;
		   }
		   List<Lebi_Bill> list = new List<Lebi_Bill>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Bill", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Bill> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Bill>;
		   }
		   List<Lebi_Bill> list = new List<Lebi_Bill>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Bill", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Bill BindForm(Lebi_Bill model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["Admin_UserName"] != null)
				model.Admin_UserName=LB.Tools.RequestTool.RequestString("Admin_UserName");
			if (HttpContext.Current.Request["BillNo"] != null)
				model.BillNo=LB.Tools.RequestTool.RequestString("BillNo");
			if (HttpContext.Current.Request["BillType_id"] != null)
				model.BillType_id=LB.Tools.RequestTool.RequestInt("BillType_id",0);
			if (HttpContext.Current.Request["Company_Address"] != null)
				model.Company_Address=LB.Tools.RequestTool.RequestString("Company_Address");
			if (HttpContext.Current.Request["Company_Bank"] != null)
				model.Company_Bank=LB.Tools.RequestTool.RequestString("Company_Bank");
			if (HttpContext.Current.Request["Company_Bank_User"] != null)
				model.Company_Bank_User=LB.Tools.RequestTool.RequestString("Company_Bank_User");
			if (HttpContext.Current.Request["Company_Code"] != null)
				model.Company_Code=LB.Tools.RequestTool.RequestString("Company_Code");
			if (HttpContext.Current.Request["Company_Name"] != null)
				model.Company_Name=LB.Tools.RequestTool.RequestString("Company_Name");
			if (HttpContext.Current.Request["Company_Phone"] != null)
				model.Company_Phone=LB.Tools.RequestTool.RequestString("Company_Phone");
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestString("Content");
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestString("Currency_Code");
			if (HttpContext.Current.Request["Currency_ExchangeRate"] != null)
				model.Currency_ExchangeRate=LB.Tools.RequestTool.RequestDecimal("Currency_ExchangeRate",0);
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Currency_Msige"] != null)
				model.Currency_Msige=LB.Tools.RequestTool.RequestString("Currency_Msige");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Order_Code"] != null)
				model.Order_Code=LB.Tools.RequestTool.RequestString("Order_Code");
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["TaxRate"] != null)
				model.TaxRate=LB.Tools.RequestTool.RequestDecimal("TaxRate",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Finish"] != null)
				model.Time_Finish=LB.Tools.RequestTool.RequestTime("Time_Finish", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestString("Title");
			if (HttpContext.Current.Request["Type_id_BillStatus"] != null)
				model.Type_id_BillStatus=LB.Tools.RequestTool.RequestInt("Type_id_BillStatus",0);
			if (HttpContext.Current.Request["Type_id_BillType"] != null)
				model.Type_id_BillType=LB.Tools.RequestTool.RequestInt("Type_id_BillType",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestString("User_UserName");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Bill SafeBindForm(Lebi_Bill model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["Admin_UserName"] != null)
				model.Admin_UserName=LB.Tools.RequestTool.RequestSafeString("Admin_UserName");
			if (HttpContext.Current.Request["BillNo"] != null)
				model.BillNo=LB.Tools.RequestTool.RequestSafeString("BillNo");
			if (HttpContext.Current.Request["BillType_id"] != null)
				model.BillType_id=LB.Tools.RequestTool.RequestInt("BillType_id",0);
			if (HttpContext.Current.Request["Company_Address"] != null)
				model.Company_Address=LB.Tools.RequestTool.RequestSafeString("Company_Address");
			if (HttpContext.Current.Request["Company_Bank"] != null)
				model.Company_Bank=LB.Tools.RequestTool.RequestSafeString("Company_Bank");
			if (HttpContext.Current.Request["Company_Bank_User"] != null)
				model.Company_Bank_User=LB.Tools.RequestTool.RequestSafeString("Company_Bank_User");
			if (HttpContext.Current.Request["Company_Code"] != null)
				model.Company_Code=LB.Tools.RequestTool.RequestSafeString("Company_Code");
			if (HttpContext.Current.Request["Company_Name"] != null)
				model.Company_Name=LB.Tools.RequestTool.RequestSafeString("Company_Name");
			if (HttpContext.Current.Request["Company_Phone"] != null)
				model.Company_Phone=LB.Tools.RequestTool.RequestSafeString("Company_Phone");
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestSafeString("Content");
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestSafeString("Currency_Code");
			if (HttpContext.Current.Request["Currency_ExchangeRate"] != null)
				model.Currency_ExchangeRate=LB.Tools.RequestTool.RequestDecimal("Currency_ExchangeRate",0);
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Currency_Msige"] != null)
				model.Currency_Msige=LB.Tools.RequestTool.RequestSafeString("Currency_Msige");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Order_Code"] != null)
				model.Order_Code=LB.Tools.RequestTool.RequestSafeString("Order_Code");
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["TaxRate"] != null)
				model.TaxRate=LB.Tools.RequestTool.RequestDecimal("TaxRate",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Finish"] != null)
				model.Time_Finish=LB.Tools.RequestTool.RequestTime("Time_Finish", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestSafeString("Title");
			if (HttpContext.Current.Request["Type_id_BillStatus"] != null)
				model.Type_id_BillStatus=LB.Tools.RequestTool.RequestInt("Type_id_BillStatus",0);
			if (HttpContext.Current.Request["Type_id_BillType"] != null)
				model.Type_id_BillType=LB.Tools.RequestTool.RequestInt("Type_id_BillType",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestSafeString("User_UserName");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Bill ReaderBind(IDataReader dataReader)
		{
			Lebi_Bill model=new Lebi_Bill();
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
			model.BillNo=dataReader["BillNo"].ToString();
			ojb = dataReader["BillType_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BillType_id= Convert.ToInt32(ojb);
			}
			model.Company_Address=dataReader["Company_Address"].ToString();
			model.Company_Bank=dataReader["Company_Bank"].ToString();
			model.Company_Bank_User=dataReader["Company_Bank_User"].ToString();
			model.Company_Code=dataReader["Company_Code"].ToString();
			model.Company_Name=dataReader["Company_Name"].ToString();
			model.Company_Phone=dataReader["Company_Phone"].ToString();
			model.Content=dataReader["Content"].ToString();
			model.Currency_Code=dataReader["Currency_Code"].ToString();
			ojb = dataReader["Currency_ExchangeRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Currency_ExchangeRate=(decimal)ojb;
			}
			ojb = dataReader["Currency_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Currency_id= Convert.ToInt32(ojb);
			}
			model.Currency_Msige=dataReader["Currency_Msige"].ToString();
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
			ojb = dataReader["TaxRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.TaxRate=(decimal)ojb;
			}
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Finish"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Finish=(DateTime)ojb;
			}
			model.Title=dataReader["Title"].ToString();
			ojb = dataReader["Type_id_BillStatus"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_BillStatus= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Type_id_BillType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_BillType= Convert.ToInt32(ojb);
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

