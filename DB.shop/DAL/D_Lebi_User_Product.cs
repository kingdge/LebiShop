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
	/// 数据访问类D_Lebi_User_Product。
	/// </summary>
	public partial class D_Lebi_User_Product
	{
		static D_Lebi_User_Product _Instance;
		public static D_Lebi_User_Product Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_User_Product("Lebi_User_Product");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_User_Product";
		public D_Lebi_User_Product(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User_Product", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User_Product", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User_Product" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User_Product", 0 , cachestr,seconds);
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
		public int Add(Lebi_User_Product model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("count")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Discount")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pointagain")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Number")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Point")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty_Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_addemail")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_UserProductType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("WarnDays")+")");
			strSql.Append(" values (");
			strSql.Append("@count,@Discount,@ImageBig,@ImageMedium,@ImageOriginal,@ImageSmall,@Pointagain,@Pro_Type_id,@Product_id,@Product_Number,@Product_Point,@Product_Price,@ProPerty_Price,@ProPerty134,@Time_Add,@Time_addemail,@Type_id_UserProductType,@User_id,@WarnDays);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@count", model.count),
					new SqlParameter("@Discount", model.Discount),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Pointagain", model.Pointagain),
					new SqlParameter("@Pro_Type_id", model.Pro_Type_id),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@Product_Number", model.Product_Number),
					new SqlParameter("@Product_Point", model.Product_Point),
					new SqlParameter("@Product_Price", model.Product_Price),
					new SqlParameter("@ProPerty_Price", model.ProPerty_Price),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_addemail", model.Time_addemail),
					new SqlParameter("@Type_id_UserProductType", model.Type_id_UserProductType),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@WarnDays", model.WarnDays)};

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
		public void Update(Lebi_User_Product model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",count,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("count")+"= @count");
			if((","+model.UpdateCols+",").IndexOf(",Discount,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Discount")+"= @Discount");
			if((","+model.UpdateCols+",").IndexOf(",ImageBig,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+"= @ImageBig");
			if((","+model.UpdateCols+",").IndexOf(",ImageMedium,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+"= @ImageMedium");
			if((","+model.UpdateCols+",").IndexOf(",ImageOriginal,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+"= @ImageOriginal");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+"= @ImageSmall");
			if((","+model.UpdateCols+",").IndexOf(",Pointagain,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pointagain")+"= @Pointagain");
			if((","+model.UpdateCols+",").IndexOf(",Pro_Type_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_id")+"= @Pro_Type_id");
			if((","+model.UpdateCols+",").IndexOf(",Product_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+"= @Product_id");
			if((","+model.UpdateCols+",").IndexOf(",Product_Number,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Number")+"= @Product_Number");
			if((","+model.UpdateCols+",").IndexOf(",Product_Point,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Point")+"= @Product_Point");
			if((","+model.UpdateCols+",").IndexOf(",Product_Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Price")+"= @Product_Price");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty_Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty_Price")+"= @ProPerty_Price");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty134,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+"= @ProPerty134");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_addemail,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_addemail")+"= @Time_addemail");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_UserProductType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_UserProductType")+"= @Type_id_UserProductType");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",WarnDays,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("WarnDays")+"= @WarnDays");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@count", model.count),
					new SqlParameter("@Discount", model.Discount),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Pointagain", model.Pointagain),
					new SqlParameter("@Pro_Type_id", model.Pro_Type_id),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@Product_Number", model.Product_Number),
					new SqlParameter("@Product_Point", model.Product_Point),
					new SqlParameter("@Product_Price", model.Product_Price),
					new SqlParameter("@ProPerty_Price", model.ProPerty_Price),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_addemail", model.Time_addemail),
					new SqlParameter("@Type_id_UserProductType", model.Type_id_UserProductType),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@WarnDays", model.WarnDays)};
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
		public Lebi_User_Product GetModel(int id, int seconds=0)
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
		           return obj as Lebi_User_Product;
		   }
		   Lebi_User_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_User_Product",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_User_Product GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_User_Product;
		   }
		   Lebi_User_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_User_Product",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_User_Product GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_User_Product;
		   }
		   Lebi_User_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_User_Product",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_User_Product> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_User_Product>;
		   }
		   List<Lebi_User_Product> list = new List<Lebi_User_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User_Product", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_User_Product> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_User_Product>;
		   }
		   List<Lebi_User_Product> list = new List<Lebi_User_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User_Product", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_User_Product> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_User_Product>;
		   }
		   List<Lebi_User_Product> list = new List<Lebi_User_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User_Product", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_User_Product> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_User_Product>;
		   }
		   List<Lebi_User_Product> list = new List<Lebi_User_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User_Product", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_User_Product BindForm(Lebi_User_Product model)
		{
			if (HttpContext.Current.Request["count"] != null)
				model.count=LB.Tools.RequestTool.RequestInt("count",0);
			if (HttpContext.Current.Request["Discount"] != null)
				model.Discount=LB.Tools.RequestTool.RequestInt("Discount",0);
			if (HttpContext.Current.Request["ImageBig"] != null)
				model.ImageBig=LB.Tools.RequestTool.RequestString("ImageBig");
			if (HttpContext.Current.Request["ImageMedium"] != null)
				model.ImageMedium=LB.Tools.RequestTool.RequestString("ImageMedium");
			if (HttpContext.Current.Request["ImageOriginal"] != null)
				model.ImageOriginal=LB.Tools.RequestTool.RequestString("ImageOriginal");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestString("ImageSmall");
			if (HttpContext.Current.Request["Pointagain"] != null)
				model.Pointagain=LB.Tools.RequestTool.RequestInt("Pointagain",0);
			if (HttpContext.Current.Request["Pro_Type_id"] != null)
				model.Pro_Type_id=LB.Tools.RequestTool.RequestInt("Pro_Type_id",0);
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["Product_Number"] != null)
				model.Product_Number=LB.Tools.RequestTool.RequestString("Product_Number");
			if (HttpContext.Current.Request["Product_Point"] != null)
				model.Product_Point=LB.Tools.RequestTool.RequestDecimal("Product_Point",0);
			if (HttpContext.Current.Request["Product_Price"] != null)
				model.Product_Price=LB.Tools.RequestTool.RequestDecimal("Product_Price",0);
			if (HttpContext.Current.Request["ProPerty_Price"] != null)
				model.ProPerty_Price=LB.Tools.RequestTool.RequestDecimal("ProPerty_Price",0);
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestString("ProPerty134");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_addemail"] != null)
				model.Time_addemail=LB.Tools.RequestTool.RequestTime("Time_addemail", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_UserProductType"] != null)
				model.Type_id_UserProductType=LB.Tools.RequestTool.RequestInt("Type_id_UserProductType",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["WarnDays"] != null)
				model.WarnDays=LB.Tools.RequestTool.RequestInt("WarnDays",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_User_Product SafeBindForm(Lebi_User_Product model)
		{
			if (HttpContext.Current.Request["count"] != null)
				model.count=LB.Tools.RequestTool.RequestInt("count",0);
			if (HttpContext.Current.Request["Discount"] != null)
				model.Discount=LB.Tools.RequestTool.RequestInt("Discount",0);
			if (HttpContext.Current.Request["ImageBig"] != null)
				model.ImageBig=LB.Tools.RequestTool.RequestSafeString("ImageBig");
			if (HttpContext.Current.Request["ImageMedium"] != null)
				model.ImageMedium=LB.Tools.RequestTool.RequestSafeString("ImageMedium");
			if (HttpContext.Current.Request["ImageOriginal"] != null)
				model.ImageOriginal=LB.Tools.RequestTool.RequestSafeString("ImageOriginal");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestSafeString("ImageSmall");
			if (HttpContext.Current.Request["Pointagain"] != null)
				model.Pointagain=LB.Tools.RequestTool.RequestInt("Pointagain",0);
			if (HttpContext.Current.Request["Pro_Type_id"] != null)
				model.Pro_Type_id=LB.Tools.RequestTool.RequestInt("Pro_Type_id",0);
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["Product_Number"] != null)
				model.Product_Number=LB.Tools.RequestTool.RequestSafeString("Product_Number");
			if (HttpContext.Current.Request["Product_Point"] != null)
				model.Product_Point=LB.Tools.RequestTool.RequestDecimal("Product_Point",0);
			if (HttpContext.Current.Request["Product_Price"] != null)
				model.Product_Price=LB.Tools.RequestTool.RequestDecimal("Product_Price",0);
			if (HttpContext.Current.Request["ProPerty_Price"] != null)
				model.ProPerty_Price=LB.Tools.RequestTool.RequestDecimal("ProPerty_Price",0);
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestSafeString("ProPerty134");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_addemail"] != null)
				model.Time_addemail=LB.Tools.RequestTool.RequestTime("Time_addemail", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_UserProductType"] != null)
				model.Type_id_UserProductType=LB.Tools.RequestTool.RequestInt("Type_id_UserProductType",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["WarnDays"] != null)
				model.WarnDays=LB.Tools.RequestTool.RequestInt("WarnDays",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_User_Product ReaderBind(IDataReader dataReader)
		{
			Lebi_User_Product model=new Lebi_User_Product();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["count"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.count= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Discount"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Discount= Convert.ToInt32(ojb);
			}
			model.ImageBig=dataReader["ImageBig"].ToString();
			model.ImageMedium=dataReader["ImageMedium"].ToString();
			model.ImageOriginal=dataReader["ImageOriginal"].ToString();
			model.ImageSmall=dataReader["ImageSmall"].ToString();
			ojb = dataReader["Pointagain"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Pointagain= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Pro_Type_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Pro_Type_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Product_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_id= Convert.ToInt32(ojb);
			}
			model.Product_Number=dataReader["Product_Number"].ToString();
			ojb = dataReader["Product_Point"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_Point=(decimal)ojb;
			}
			ojb = dataReader["Product_Price"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_Price=(decimal)ojb;
			}
			ojb = dataReader["ProPerty_Price"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ProPerty_Price=(decimal)ojb;
			}
			model.ProPerty134=dataReader["ProPerty134"].ToString();
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_addemail"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_addemail=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_UserProductType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_UserProductType= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["WarnDays"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.WarnDays= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

