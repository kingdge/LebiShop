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
	/// 数据访问类D_Lebi_OnlinePay。
	/// </summary>
	public partial class D_Lebi_OnlinePay
	{
		static D_Lebi_OnlinePay _Instance;
		public static D_Lebi_OnlinePay Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_OnlinePay("Lebi_OnlinePay");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_OnlinePay";
		public D_Lebi_OnlinePay(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_OnlinePay", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_OnlinePay", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_OnlinePay" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_OnlinePay", 0 , cachestr,seconds);
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
		public int Add(Lebi_OnlinePay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Appid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Appkey")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ExchangeRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("FeeRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUsed")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Logo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("parentid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("showtype")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("terminal")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("TypeName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Url")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserKey")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserRealName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("FreeFeeRate")+")");
			strSql.Append(" values (");
			strSql.Append("@Appid,@Appkey,@Code,@Currency_Code,@Currency_id,@Currency_Name,@Description,@Email,@ExchangeRate,@FeeRate,@IsUsed,@Language_ids,@Logo,@Name,@parentid,@Remark,@showtype,@Sort,@Supplier_id,@terminal,@TypeName,@Url,@UserKey,@UserName,@UserRealName,@FreeFeeRate);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Appid", model.Appid),
					new SqlParameter("@Appkey", model.Appkey),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Currency_Name", model.Currency_Name),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@ExchangeRate", model.ExchangeRate),
					new SqlParameter("@FeeRate", model.FeeRate),
					new SqlParameter("@IsUsed", model.IsUsed),
					new SqlParameter("@Language_ids", model.Language_ids),
					new SqlParameter("@Logo", model.Logo),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@parentid", model.parentid),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@showtype", model.showtype),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@terminal", model.terminal),
					new SqlParameter("@TypeName", model.TypeName),
					new SqlParameter("@Url", model.Url),
					new SqlParameter("@UserKey", model.UserKey),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@UserRealName", model.UserRealName),
					new SqlParameter("@FreeFeeRate", model.FreeFeeRate)};

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
		public void Update(Lebi_OnlinePay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Appid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Appid")+"= @Appid");
			if((","+model.UpdateCols+",").IndexOf(",Appkey,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Appkey")+"= @Appkey");
			if((","+model.UpdateCols+",").IndexOf(",Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+"= @Code");
			if((","+model.UpdateCols+",").IndexOf(",Currency_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+"= @Currency_Code");
			if((","+model.UpdateCols+",").IndexOf(",Currency_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+"= @Currency_id");
			if((","+model.UpdateCols+",").IndexOf(",Currency_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Name")+"= @Currency_Name");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if((","+model.UpdateCols+",").IndexOf(",ExchangeRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ExchangeRate")+"= @ExchangeRate");
			if((","+model.UpdateCols+",").IndexOf(",FeeRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("FeeRate")+"= @FeeRate");
			if((","+model.UpdateCols+",").IndexOf(",IsUsed,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUsed")+"= @IsUsed");
			if((","+model.UpdateCols+",").IndexOf(",Language_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_ids")+"= @Language_ids");
			if((","+model.UpdateCols+",").IndexOf(",Logo,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Logo")+"= @Logo");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",parentid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("parentid")+"= @parentid");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",showtype,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("showtype")+"= @showtype");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",terminal,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("terminal")+"= @terminal");
			if((","+model.UpdateCols+",").IndexOf(",TypeName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("TypeName")+"= @TypeName");
			if((","+model.UpdateCols+",").IndexOf(",Url,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Url")+"= @Url");
			if((","+model.UpdateCols+",").IndexOf(",UserKey,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserKey")+"= @UserKey");
			if((","+model.UpdateCols+",").IndexOf(",UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+"= @UserName");
			if((","+model.UpdateCols+",").IndexOf(",UserRealName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserRealName")+"= @UserRealName");
			if((","+model.UpdateCols+",").IndexOf(",FreeFeeRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("FreeFeeRate")+"= @FreeFeeRate");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Appid", model.Appid),
					new SqlParameter("@Appkey", model.Appkey),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Currency_Name", model.Currency_Name),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@ExchangeRate", model.ExchangeRate),
					new SqlParameter("@FeeRate", model.FeeRate),
					new SqlParameter("@IsUsed", model.IsUsed),
					new SqlParameter("@Language_ids", model.Language_ids),
					new SqlParameter("@Logo", model.Logo),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@parentid", model.parentid),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@showtype", model.showtype),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@terminal", model.terminal),
					new SqlParameter("@TypeName", model.TypeName),
					new SqlParameter("@Url", model.Url),
					new SqlParameter("@UserKey", model.UserKey),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@UserRealName", model.UserRealName),
					new SqlParameter("@FreeFeeRate", model.FreeFeeRate)};
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
		public Lebi_OnlinePay GetModel(int id, int seconds=0)
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
		           return obj as Lebi_OnlinePay;
		   }
		   Lebi_OnlinePay model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_OnlinePay",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_OnlinePay GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_OnlinePay;
		   }
		   Lebi_OnlinePay model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_OnlinePay",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_OnlinePay GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_OnlinePay;
		   }
		   Lebi_OnlinePay model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_OnlinePay",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_OnlinePay> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_OnlinePay>;
		   }
		   List<Lebi_OnlinePay> list = new List<Lebi_OnlinePay>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_OnlinePay", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_OnlinePay> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_OnlinePay>;
		   }
		   List<Lebi_OnlinePay> list = new List<Lebi_OnlinePay>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_OnlinePay", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_OnlinePay> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_OnlinePay>;
		   }
		   List<Lebi_OnlinePay> list = new List<Lebi_OnlinePay>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_OnlinePay", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_OnlinePay> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_OnlinePay>;
		   }
		   List<Lebi_OnlinePay> list = new List<Lebi_OnlinePay>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_OnlinePay", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_OnlinePay BindForm(Lebi_OnlinePay model)
		{
			if (HttpContext.Current.Request["Appid"] != null)
				model.Appid=LB.Tools.RequestTool.RequestString("Appid");
			if (HttpContext.Current.Request["Appkey"] != null)
				model.Appkey=LB.Tools.RequestTool.RequestString("Appkey");
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestString("Code");
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestString("Currency_Code");
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Currency_Name"] != null)
				model.Currency_Name=LB.Tools.RequestTool.RequestString("Currency_Name");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["ExchangeRate"] != null)
				model.ExchangeRate=LB.Tools.RequestTool.RequestDecimal("ExchangeRate",0);
			if (HttpContext.Current.Request["FeeRate"] != null)
				model.FeeRate=LB.Tools.RequestTool.RequestDecimal("FeeRate",0);
			if (HttpContext.Current.Request["IsUsed"] != null)
				model.IsUsed=LB.Tools.RequestTool.RequestInt("IsUsed",0);
			if (HttpContext.Current.Request["Language_ids"] != null)
				model.Language_ids=LB.Tools.RequestTool.RequestString("Language_ids");
			if (HttpContext.Current.Request["Logo"] != null)
				model.Logo=LB.Tools.RequestTool.RequestString("Logo");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["parentid"] != null)
				model.parentid=LB.Tools.RequestTool.RequestInt("parentid",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["showtype"] != null)
				model.showtype=LB.Tools.RequestTool.RequestString("showtype");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["terminal"] != null)
				model.terminal=LB.Tools.RequestTool.RequestString("terminal");
			if (HttpContext.Current.Request["TypeName"] != null)
				model.TypeName=LB.Tools.RequestTool.RequestString("TypeName");
			if (HttpContext.Current.Request["Url"] != null)
				model.Url=LB.Tools.RequestTool.RequestString("Url");
			if (HttpContext.Current.Request["UserKey"] != null)
				model.UserKey=LB.Tools.RequestTool.RequestString("UserKey");
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestString("UserName");
			if (HttpContext.Current.Request["UserRealName"] != null)
				model.UserRealName=LB.Tools.RequestTool.RequestString("UserRealName");
			if (HttpContext.Current.Request["FreeFeeRate"] != null)
				model.FreeFeeRate=LB.Tools.RequestTool.RequestInt("FreeFeeRate",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_OnlinePay SafeBindForm(Lebi_OnlinePay model)
		{
			if (HttpContext.Current.Request["Appid"] != null)
				model.Appid=LB.Tools.RequestTool.RequestSafeString("Appid");
			if (HttpContext.Current.Request["Appkey"] != null)
				model.Appkey=LB.Tools.RequestTool.RequestSafeString("Appkey");
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestSafeString("Code");
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestSafeString("Currency_Code");
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Currency_Name"] != null)
				model.Currency_Name=LB.Tools.RequestTool.RequestSafeString("Currency_Name");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["ExchangeRate"] != null)
				model.ExchangeRate=LB.Tools.RequestTool.RequestDecimal("ExchangeRate",0);
			if (HttpContext.Current.Request["FeeRate"] != null)
				model.FeeRate=LB.Tools.RequestTool.RequestDecimal("FeeRate",0);
			if (HttpContext.Current.Request["IsUsed"] != null)
				model.IsUsed=LB.Tools.RequestTool.RequestInt("IsUsed",0);
			if (HttpContext.Current.Request["Language_ids"] != null)
				model.Language_ids=LB.Tools.RequestTool.RequestSafeString("Language_ids");
			if (HttpContext.Current.Request["Logo"] != null)
				model.Logo=LB.Tools.RequestTool.RequestSafeString("Logo");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["parentid"] != null)
				model.parentid=LB.Tools.RequestTool.RequestInt("parentid",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["showtype"] != null)
				model.showtype=LB.Tools.RequestTool.RequestSafeString("showtype");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["terminal"] != null)
				model.terminal=LB.Tools.RequestTool.RequestSafeString("terminal");
			if (HttpContext.Current.Request["TypeName"] != null)
				model.TypeName=LB.Tools.RequestTool.RequestSafeString("TypeName");
			if (HttpContext.Current.Request["Url"] != null)
				model.Url=LB.Tools.RequestTool.RequestSafeString("Url");
			if (HttpContext.Current.Request["UserKey"] != null)
				model.UserKey=LB.Tools.RequestTool.RequestSafeString("UserKey");
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestSafeString("UserName");
			if (HttpContext.Current.Request["UserRealName"] != null)
				model.UserRealName=LB.Tools.RequestTool.RequestSafeString("UserRealName");
			if (HttpContext.Current.Request["FreeFeeRate"] != null)
				model.FreeFeeRate=LB.Tools.RequestTool.RequestInt("FreeFeeRate",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_OnlinePay ReaderBind(IDataReader dataReader)
		{
			Lebi_OnlinePay model=new Lebi_OnlinePay();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Appid=dataReader["Appid"].ToString();
			model.Appkey=dataReader["Appkey"].ToString();
			model.Code=dataReader["Code"].ToString();
			model.Currency_Code=dataReader["Currency_Code"].ToString();
			ojb = dataReader["Currency_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Currency_id= Convert.ToInt32(ojb);
			}
			model.Currency_Name=dataReader["Currency_Name"].ToString();
			model.Description=dataReader["Description"].ToString();
			model.Email=dataReader["Email"].ToString();
			ojb = dataReader["ExchangeRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ExchangeRate=(decimal)ojb;
			}
			ojb = dataReader["FeeRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FeeRate=(decimal)ojb;
			}
			ojb = dataReader["IsUsed"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsUsed= Convert.ToInt32(ojb);
			}
			model.Language_ids=dataReader["Language_ids"].ToString();
			model.Logo=dataReader["Logo"].ToString();
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["parentid"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.parentid= Convert.ToInt32(ojb);
			}
			model.Remark=dataReader["Remark"].ToString();
			model.showtype=dataReader["showtype"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			model.terminal=dataReader["terminal"].ToString();
			model.TypeName=dataReader["TypeName"].ToString();
			model.Url=dataReader["Url"].ToString();
			model.UserKey=dataReader["UserKey"].ToString();
			model.UserName=dataReader["UserName"].ToString();
			model.UserRealName=dataReader["UserRealName"].ToString();
			ojb = dataReader["FreeFeeRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FreeFeeRate= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

