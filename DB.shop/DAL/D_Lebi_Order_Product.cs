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
	/// 数据访问类D_Lebi_Order_Product。
	/// </summary>
	public partial class D_Lebi_Order_Product
	{
		static D_Lebi_Order_Product _Instance;
		public static D_Lebi_Order_Product Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Order_Product("Lebi_Order_Product");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Order_Product";
		public D_Lebi_Order_Product(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order_Product", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order_Product", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order_Product" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order_Product", 0 , cachestr,seconds);
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
		public int Add(Lebi_Order_Product model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Received")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Return")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Shipped")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Discount")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCommented")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPaid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPaidReserve")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReserve")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsStockOK")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierTransport")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Card312_one")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Give_one")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NetWeight")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PackageRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Buy_one")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Give_one")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Product")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pointagain")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Cost")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Reserve")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Number")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty_Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Paid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_StockOK")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_OrderProductType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Units_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Volume")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight")+")");
			strSql.Append(" values (");
			strSql.Append("@Count,@Count_Received,@Count_Return,@Count_Shipped,@Discount,@ImageBig,@ImageMedium,@ImageOriginal,@ImageSmall,@IsCommented,@IsDel,@IsPaid,@IsPaidReserve,@IsReserve,@IsStockOK,@IsSupplierTransport,@Money,@Money_Card312_one,@Money_Give_one,@NetWeight,@Order_Code,@Order_id,@PackageRate,@Point,@Point_Buy_one,@Point_Give_one,@Point_Product,@Pointagain,@Price,@Price_Cost,@Price_Reserve,@Product_id,@Product_Name,@Product_Number,@ProPerty_Price,@ProPerty134,@Remark,@Supplier_id,@Time_Add,@Time_Paid,@Time_StockOK,@Type_id_OrderProductType,@Units_id,@User_id,@Volume,@Weight);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Count", model.Count),
					new SqlParameter("@Count_Received", model.Count_Received),
					new SqlParameter("@Count_Return", model.Count_Return),
					new SqlParameter("@Count_Shipped", model.Count_Shipped),
					new SqlParameter("@Discount", model.Discount),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@IsCommented", model.IsCommented),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsPaid", model.IsPaid),
					new SqlParameter("@IsPaidReserve", model.IsPaidReserve),
					new SqlParameter("@IsReserve", model.IsReserve),
					new SqlParameter("@IsStockOK", model.IsStockOK),
					new SqlParameter("@IsSupplierTransport", model.IsSupplierTransport),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Card312_one", model.Money_Card312_one),
					new SqlParameter("@Money_Give_one", model.Money_Give_one),
					new SqlParameter("@NetWeight", model.NetWeight),
					new SqlParameter("@Order_Code", model.Order_Code),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@PackageRate", model.PackageRate),
					new SqlParameter("@Point", model.Point),
					new SqlParameter("@Point_Buy_one", model.Point_Buy_one),
					new SqlParameter("@Point_Give_one", model.Point_Give_one),
					new SqlParameter("@Point_Product", model.Point_Product),
					new SqlParameter("@Pointagain", model.Pointagain),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@Price_Cost", model.Price_Cost),
					new SqlParameter("@Price_Reserve", model.Price_Reserve),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@Product_Name", model.Product_Name),
					new SqlParameter("@Product_Number", model.Product_Number),
					new SqlParameter("@ProPerty_Price", model.ProPerty_Price),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Paid", model.Time_Paid),
					new SqlParameter("@Time_StockOK", model.Time_StockOK),
					new SqlParameter("@Type_id_OrderProductType", model.Type_id_OrderProductType),
					new SqlParameter("@Units_id", model.Units_id),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@Volume", model.Volume),
					new SqlParameter("@Weight", model.Weight)};

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
		public void Update(Lebi_Order_Product model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Count,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count")+"= @Count");
			if((","+model.UpdateCols+",").IndexOf(",Count_Received,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Received")+"= @Count_Received");
			if((","+model.UpdateCols+",").IndexOf(",Count_Return,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Return")+"= @Count_Return");
			if((","+model.UpdateCols+",").IndexOf(",Count_Shipped,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Shipped")+"= @Count_Shipped");
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
			if((","+model.UpdateCols+",").IndexOf(",IsCommented,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCommented")+"= @IsCommented");
			if((","+model.UpdateCols+",").IndexOf(",IsDel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+"= @IsDel");
			if((","+model.UpdateCols+",").IndexOf(",IsPaid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPaid")+"= @IsPaid");
			if((","+model.UpdateCols+",").IndexOf(",IsPaidReserve,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPaidReserve")+"= @IsPaidReserve");
			if((","+model.UpdateCols+",").IndexOf(",IsReserve,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReserve")+"= @IsReserve");
			if((","+model.UpdateCols+",").IndexOf(",IsStockOK,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsStockOK")+"= @IsStockOK");
			if((","+model.UpdateCols+",").IndexOf(",IsSupplierTransport,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierTransport")+"= @IsSupplierTransport");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Money_Card312_one,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Card312_one")+"= @Money_Card312_one");
			if((","+model.UpdateCols+",").IndexOf(",Money_Give_one,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Give_one")+"= @Money_Give_one");
			if((","+model.UpdateCols+",").IndexOf(",NetWeight,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NetWeight")+"= @NetWeight");
			if((","+model.UpdateCols+",").IndexOf(",Order_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_Code")+"= @Order_Code");
			if((","+model.UpdateCols+",").IndexOf(",Order_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+"= @Order_id");
			if((","+model.UpdateCols+",").IndexOf(",PackageRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PackageRate")+"= @PackageRate");
			if((","+model.UpdateCols+",").IndexOf(",Point,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point")+"= @Point");
			if((","+model.UpdateCols+",").IndexOf(",Point_Buy_one,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Buy_one")+"= @Point_Buy_one");
			if((","+model.UpdateCols+",").IndexOf(",Point_Give_one,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Give_one")+"= @Point_Give_one");
			if((","+model.UpdateCols+",").IndexOf(",Point_Product,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Product")+"= @Point_Product");
			if((","+model.UpdateCols+",").IndexOf(",Pointagain,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pointagain")+"= @Pointagain");
			if((","+model.UpdateCols+",").IndexOf(",Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+"= @Price");
			if((","+model.UpdateCols+",").IndexOf(",Price_Cost,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Cost")+"= @Price_Cost");
			if((","+model.UpdateCols+",").IndexOf(",Price_Reserve,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Reserve")+"= @Price_Reserve");
			if((","+model.UpdateCols+",").IndexOf(",Product_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+"= @Product_id");
			if((","+model.UpdateCols+",").IndexOf(",Product_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Name")+"= @Product_Name");
			if((","+model.UpdateCols+",").IndexOf(",Product_Number,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_Number")+"= @Product_Number");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty_Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty_Price")+"= @ProPerty_Price");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty134,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+"= @ProPerty134");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Paid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Paid")+"= @Time_Paid");
			if((","+model.UpdateCols+",").IndexOf(",Time_StockOK,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_StockOK")+"= @Time_StockOK");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_OrderProductType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_OrderProductType")+"= @Type_id_OrderProductType");
			if((","+model.UpdateCols+",").IndexOf(",Units_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Units_id")+"= @Units_id");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",Volume,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Volume")+"= @Volume");
			if((","+model.UpdateCols+",").IndexOf(",Weight,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight")+"= @Weight");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Count", model.Count),
					new SqlParameter("@Count_Received", model.Count_Received),
					new SqlParameter("@Count_Return", model.Count_Return),
					new SqlParameter("@Count_Shipped", model.Count_Shipped),
					new SqlParameter("@Discount", model.Discount),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@IsCommented", model.IsCommented),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsPaid", model.IsPaid),
					new SqlParameter("@IsPaidReserve", model.IsPaidReserve),
					new SqlParameter("@IsReserve", model.IsReserve),
					new SqlParameter("@IsStockOK", model.IsStockOK),
					new SqlParameter("@IsSupplierTransport", model.IsSupplierTransport),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Card312_one", model.Money_Card312_one),
					new SqlParameter("@Money_Give_one", model.Money_Give_one),
					new SqlParameter("@NetWeight", model.NetWeight),
					new SqlParameter("@Order_Code", model.Order_Code),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@PackageRate", model.PackageRate),
					new SqlParameter("@Point", model.Point),
					new SqlParameter("@Point_Buy_one", model.Point_Buy_one),
					new SqlParameter("@Point_Give_one", model.Point_Give_one),
					new SqlParameter("@Point_Product", model.Point_Product),
					new SqlParameter("@Pointagain", model.Pointagain),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@Price_Cost", model.Price_Cost),
					new SqlParameter("@Price_Reserve", model.Price_Reserve),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@Product_Name", model.Product_Name),
					new SqlParameter("@Product_Number", model.Product_Number),
					new SqlParameter("@ProPerty_Price", model.ProPerty_Price),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Paid", model.Time_Paid),
					new SqlParameter("@Time_StockOK", model.Time_StockOK),
					new SqlParameter("@Type_id_OrderProductType", model.Type_id_OrderProductType),
					new SqlParameter("@Units_id", model.Units_id),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@Volume", model.Volume),
					new SqlParameter("@Weight", model.Weight)};
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
		public Lebi_Order_Product GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Order_Product;
		   }
		   Lebi_Order_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Order_Product",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Order_Product GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Order_Product;
		   }
		   Lebi_Order_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Order_Product",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Order_Product GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Order_Product;
		   }
		   Lebi_Order_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Order_Product",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Order_Product> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Order_Product>;
		   }
		   List<Lebi_Order_Product> list = new List<Lebi_Order_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order_Product", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Order_Product> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Order_Product>;
		   }
		   List<Lebi_Order_Product> list = new List<Lebi_Order_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order_Product", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Order_Product> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Order_Product>;
		   }
		   List<Lebi_Order_Product> list = new List<Lebi_Order_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order_Product", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Order_Product> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Order_Product>;
		   }
		   List<Lebi_Order_Product> list = new List<Lebi_Order_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order_Product", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Order_Product BindForm(Lebi_Order_Product model)
		{
			if (HttpContext.Current.Request["Count"] != null)
				model.Count=LB.Tools.RequestTool.RequestInt("Count",0);
			if (HttpContext.Current.Request["Count_Received"] != null)
				model.Count_Received=LB.Tools.RequestTool.RequestInt("Count_Received",0);
			if (HttpContext.Current.Request["Count_Return"] != null)
				model.Count_Return=LB.Tools.RequestTool.RequestInt("Count_Return",0);
			if (HttpContext.Current.Request["Count_Shipped"] != null)
				model.Count_Shipped=LB.Tools.RequestTool.RequestInt("Count_Shipped",0);
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
			if (HttpContext.Current.Request["IsCommented"] != null)
				model.IsCommented=LB.Tools.RequestTool.RequestInt("IsCommented",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsPaid"] != null)
				model.IsPaid=LB.Tools.RequestTool.RequestInt("IsPaid",0);
			if (HttpContext.Current.Request["IsPaidReserve"] != null)
				model.IsPaidReserve=LB.Tools.RequestTool.RequestInt("IsPaidReserve",0);
			if (HttpContext.Current.Request["IsReserve"] != null)
				model.IsReserve=LB.Tools.RequestTool.RequestInt("IsReserve",0);
			if (HttpContext.Current.Request["IsStockOK"] != null)
				model.IsStockOK=LB.Tools.RequestTool.RequestInt("IsStockOK",0);
			if (HttpContext.Current.Request["IsSupplierTransport"] != null)
				model.IsSupplierTransport=LB.Tools.RequestTool.RequestInt("IsSupplierTransport",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Card312_one"] != null)
				model.Money_Card312_one=LB.Tools.RequestTool.RequestDecimal("Money_Card312_one",0);
			if (HttpContext.Current.Request["Money_Give_one"] != null)
				model.Money_Give_one=LB.Tools.RequestTool.RequestDecimal("Money_Give_one",0);
			if (HttpContext.Current.Request["NetWeight"] != null)
				model.NetWeight=LB.Tools.RequestTool.RequestDecimal("NetWeight",0);
			if (HttpContext.Current.Request["Order_Code"] != null)
				model.Order_Code=LB.Tools.RequestTool.RequestString("Order_Code");
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["PackageRate"] != null)
				model.PackageRate=LB.Tools.RequestTool.RequestInt("PackageRate",0);
			if (HttpContext.Current.Request["Point"] != null)
				model.Point=LB.Tools.RequestTool.RequestDecimal("Point",0);
			if (HttpContext.Current.Request["Point_Buy_one"] != null)
				model.Point_Buy_one=LB.Tools.RequestTool.RequestDecimal("Point_Buy_one",0);
			if (HttpContext.Current.Request["Point_Give_one"] != null)
				model.Point_Give_one=LB.Tools.RequestTool.RequestDecimal("Point_Give_one",0);
			if (HttpContext.Current.Request["Point_Product"] != null)
				model.Point_Product=LB.Tools.RequestTool.RequestDecimal("Point_Product",0);
			if (HttpContext.Current.Request["Pointagain"] != null)
				model.Pointagain=LB.Tools.RequestTool.RequestInt("Pointagain",0);
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["Price_Cost"] != null)
				model.Price_Cost=LB.Tools.RequestTool.RequestDecimal("Price_Cost",0);
			if (HttpContext.Current.Request["Price_Reserve"] != null)
				model.Price_Reserve=LB.Tools.RequestTool.RequestDecimal("Price_Reserve",0);
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["Product_Name"] != null)
				model.Product_Name=LB.Tools.RequestTool.RequestString("Product_Name");
			if (HttpContext.Current.Request["Product_Number"] != null)
				model.Product_Number=LB.Tools.RequestTool.RequestString("Product_Number");
			if (HttpContext.Current.Request["ProPerty_Price"] != null)
				model.ProPerty_Price=LB.Tools.RequestTool.RequestDecimal("ProPerty_Price",0);
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestString("ProPerty134");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Paid"] != null)
				model.Time_Paid=LB.Tools.RequestTool.RequestTime("Time_Paid", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_StockOK"] != null)
				model.Time_StockOK=LB.Tools.RequestTool.RequestTime("Time_StockOK", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_OrderProductType"] != null)
				model.Type_id_OrderProductType=LB.Tools.RequestTool.RequestInt("Type_id_OrderProductType",0);
			if (HttpContext.Current.Request["Units_id"] != null)
				model.Units_id=LB.Tools.RequestTool.RequestInt("Units_id",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["Volume"] != null)
				model.Volume=LB.Tools.RequestTool.RequestDecimal("Volume",0);
			if (HttpContext.Current.Request["Weight"] != null)
				model.Weight=LB.Tools.RequestTool.RequestDecimal("Weight",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Order_Product SafeBindForm(Lebi_Order_Product model)
		{
			if (HttpContext.Current.Request["Count"] != null)
				model.Count=LB.Tools.RequestTool.RequestInt("Count",0);
			if (HttpContext.Current.Request["Count_Received"] != null)
				model.Count_Received=LB.Tools.RequestTool.RequestInt("Count_Received",0);
			if (HttpContext.Current.Request["Count_Return"] != null)
				model.Count_Return=LB.Tools.RequestTool.RequestInt("Count_Return",0);
			if (HttpContext.Current.Request["Count_Shipped"] != null)
				model.Count_Shipped=LB.Tools.RequestTool.RequestInt("Count_Shipped",0);
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
			if (HttpContext.Current.Request["IsCommented"] != null)
				model.IsCommented=LB.Tools.RequestTool.RequestInt("IsCommented",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsPaid"] != null)
				model.IsPaid=LB.Tools.RequestTool.RequestInt("IsPaid",0);
			if (HttpContext.Current.Request["IsPaidReserve"] != null)
				model.IsPaidReserve=LB.Tools.RequestTool.RequestInt("IsPaidReserve",0);
			if (HttpContext.Current.Request["IsReserve"] != null)
				model.IsReserve=LB.Tools.RequestTool.RequestInt("IsReserve",0);
			if (HttpContext.Current.Request["IsStockOK"] != null)
				model.IsStockOK=LB.Tools.RequestTool.RequestInt("IsStockOK",0);
			if (HttpContext.Current.Request["IsSupplierTransport"] != null)
				model.IsSupplierTransport=LB.Tools.RequestTool.RequestInt("IsSupplierTransport",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Card312_one"] != null)
				model.Money_Card312_one=LB.Tools.RequestTool.RequestDecimal("Money_Card312_one",0);
			if (HttpContext.Current.Request["Money_Give_one"] != null)
				model.Money_Give_one=LB.Tools.RequestTool.RequestDecimal("Money_Give_one",0);
			if (HttpContext.Current.Request["NetWeight"] != null)
				model.NetWeight=LB.Tools.RequestTool.RequestDecimal("NetWeight",0);
			if (HttpContext.Current.Request["Order_Code"] != null)
				model.Order_Code=LB.Tools.RequestTool.RequestSafeString("Order_Code");
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["PackageRate"] != null)
				model.PackageRate=LB.Tools.RequestTool.RequestInt("PackageRate",0);
			if (HttpContext.Current.Request["Point"] != null)
				model.Point=LB.Tools.RequestTool.RequestDecimal("Point",0);
			if (HttpContext.Current.Request["Point_Buy_one"] != null)
				model.Point_Buy_one=LB.Tools.RequestTool.RequestDecimal("Point_Buy_one",0);
			if (HttpContext.Current.Request["Point_Give_one"] != null)
				model.Point_Give_one=LB.Tools.RequestTool.RequestDecimal("Point_Give_one",0);
			if (HttpContext.Current.Request["Point_Product"] != null)
				model.Point_Product=LB.Tools.RequestTool.RequestDecimal("Point_Product",0);
			if (HttpContext.Current.Request["Pointagain"] != null)
				model.Pointagain=LB.Tools.RequestTool.RequestInt("Pointagain",0);
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["Price_Cost"] != null)
				model.Price_Cost=LB.Tools.RequestTool.RequestDecimal("Price_Cost",0);
			if (HttpContext.Current.Request["Price_Reserve"] != null)
				model.Price_Reserve=LB.Tools.RequestTool.RequestDecimal("Price_Reserve",0);
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["Product_Name"] != null)
				model.Product_Name=LB.Tools.RequestTool.RequestSafeString("Product_Name");
			if (HttpContext.Current.Request["Product_Number"] != null)
				model.Product_Number=LB.Tools.RequestTool.RequestSafeString("Product_Number");
			if (HttpContext.Current.Request["ProPerty_Price"] != null)
				model.ProPerty_Price=LB.Tools.RequestTool.RequestDecimal("ProPerty_Price",0);
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestSafeString("ProPerty134");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Paid"] != null)
				model.Time_Paid=LB.Tools.RequestTool.RequestTime("Time_Paid", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_StockOK"] != null)
				model.Time_StockOK=LB.Tools.RequestTool.RequestTime("Time_StockOK", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_OrderProductType"] != null)
				model.Type_id_OrderProductType=LB.Tools.RequestTool.RequestInt("Type_id_OrderProductType",0);
			if (HttpContext.Current.Request["Units_id"] != null)
				model.Units_id=LB.Tools.RequestTool.RequestInt("Units_id",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["Volume"] != null)
				model.Volume=LB.Tools.RequestTool.RequestDecimal("Volume",0);
			if (HttpContext.Current.Request["Weight"] != null)
				model.Weight=LB.Tools.RequestTool.RequestDecimal("Weight",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Order_Product ReaderBind(IDataReader dataReader)
		{
			Lebi_Order_Product model=new Lebi_Order_Product();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Received"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Received= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Return"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Return= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Shipped"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Shipped= Convert.ToInt32(ojb);
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
			ojb = dataReader["IsCommented"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCommented= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsDel"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsDel= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsPaid"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsPaid= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsPaidReserve"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsPaidReserve= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsReserve"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsReserve= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsStockOK"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsStockOK= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSupplierTransport"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSupplierTransport= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			ojb = dataReader["Money_Card312_one"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Card312_one=(decimal)ojb;
			}
			ojb = dataReader["Money_Give_one"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Give_one=(decimal)ojb;
			}
			ojb = dataReader["NetWeight"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.NetWeight=(decimal)ojb;
			}
			model.Order_Code=dataReader["Order_Code"].ToString();
			ojb = dataReader["Order_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Order_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["PackageRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PackageRate= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Point"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Point=(decimal)ojb;
			}
			ojb = dataReader["Point_Buy_one"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Point_Buy_one=(decimal)ojb;
			}
			ojb = dataReader["Point_Give_one"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Point_Give_one=(decimal)ojb;
			}
			ojb = dataReader["Point_Product"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Point_Product=(decimal)ojb;
			}
			ojb = dataReader["Pointagain"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Pointagain= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Price"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price=(decimal)ojb;
			}
			ojb = dataReader["Price_Cost"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_Cost=(decimal)ojb;
			}
			ojb = dataReader["Price_Reserve"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_Reserve=(decimal)ojb;
			}
			ojb = dataReader["Product_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_id= Convert.ToInt32(ojb);
			}
			model.Product_Name=dataReader["Product_Name"].ToString();
			model.Product_Number=dataReader["Product_Number"].ToString();
			ojb = dataReader["ProPerty_Price"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ProPerty_Price=(decimal)ojb;
			}
			model.ProPerty134=dataReader["ProPerty134"].ToString();
			model.Remark=dataReader["Remark"].ToString();
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Paid"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Paid=(DateTime)ojb;
			}
			ojb = dataReader["Time_StockOK"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_StockOK=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_OrderProductType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_OrderProductType= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Units_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Units_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Volume"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Volume=(decimal)ojb;
			}
			ojb = dataReader["Weight"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Weight=(decimal)ojb;
			}
			return model;
		}

		#endregion  成员方法
	}
}

