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
	/// 数据访问类D_Lebi_Product。
	/// </summary>
	public partial class D_Lebi_Product
	{
		static D_Lebi_Product _Instance;
		public static D_Lebi_Product Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Product("Lebi_Product");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Product";
		public D_Lebi_Product(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Product", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Product", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Product" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Product", 0 , cachestr,seconds);
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
		public int Add(Lebi_Product model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Brand_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Comment")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Freeze")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Like")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Limit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Sales")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Sales_Show")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_SalesFalse")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Stock")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_StockCaution")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Views")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Views_Show")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_ViewsFalse")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("FreezeRemark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Images")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Introduction")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCombo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsNullStockSale")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierTransport")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("MobileDescription")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NetWeight")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Number")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PackageRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Packing")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Cost")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Market")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_reserve")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_reserve_per")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Sale")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Tag_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_id_other")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty131")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty132")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty133")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPertyMain")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remarks")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Reserve_days")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Service")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Specification")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Star_Comment")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("StepPrice")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_ProductType_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Tags")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("taobaoid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("taobaoid_type")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Edit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Expired")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_OnSale")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Start")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_ProductStatus")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_ProductType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Units_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids_buy")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids_priceshow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids_show")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevelCount")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevelPrice")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("VolumeH")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("VolumeL")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("VolumeW")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Location")+")");
			strSql.Append(" values (");
			strSql.Append("@Brand_id,@Code,@Count_Comment,@Count_Freeze,@Count_Like,@Count_Limit,@Count_Sales,@Count_Sales_Show,@Count_SalesFalse,@Count_Stock,@Count_StockCaution,@Count_Views,@Count_Views_Show,@Count_ViewsFalse,@Description,@FreezeRemark,@ImageBig,@ImageMedium,@ImageOriginal,@Images,@ImageSmall,@Introduction,@IsCombo,@IsDel,@IsNullStockSale,@IsSupplierTransport,@MobileDescription,@Name,@NetWeight,@Number,@PackageRate,@Packing,@Price,@Price_Cost,@Price_Market,@Price_reserve,@Price_reserve_per,@Price_Sale,@Pro_Tag_id,@Pro_Type_id,@Pro_Type_id_other,@Product_id,@ProPerty131,@ProPerty132,@ProPerty133,@ProPerty134,@ProPertyMain,@Remarks,@Reserve_days,@SEO_Description,@SEO_Keywords,@SEO_Title,@Service,@Site_ids,@Sort,@Specification,@Star_Comment,@StepPrice,@Supplier_id,@Supplier_ProductType_ids,@Tags,@taobaoid,@taobaoid_type,@Time_Add,@Time_Edit,@Time_Expired,@Time_OnSale,@Time_Start,@Type_id_ProductStatus,@Type_id_ProductType,@Units_id,@UserLevel_ids_buy,@UserLevel_ids_priceshow,@UserLevel_ids_show,@UserLevelCount,@UserLevelPrice,@VolumeH,@VolumeL,@VolumeW,@Weight,@Location);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Brand_id", model.Brand_id),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@Count_Comment", model.Count_Comment),
					new SqlParameter("@Count_Freeze", model.Count_Freeze),
					new SqlParameter("@Count_Like", model.Count_Like),
					new SqlParameter("@Count_Limit", model.Count_Limit),
					new SqlParameter("@Count_Sales", model.Count_Sales),
					new SqlParameter("@Count_Sales_Show", model.Count_Sales_Show),
					new SqlParameter("@Count_SalesFalse", model.Count_SalesFalse),
					new SqlParameter("@Count_Stock", model.Count_Stock),
					new SqlParameter("@Count_StockCaution", model.Count_StockCaution),
					new SqlParameter("@Count_Views", model.Count_Views),
					new SqlParameter("@Count_Views_Show", model.Count_Views_Show),
					new SqlParameter("@Count_ViewsFalse", model.Count_ViewsFalse),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@FreezeRemark", model.FreezeRemark),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@Images", model.Images),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Introduction", model.Introduction),
					new SqlParameter("@IsCombo", model.IsCombo),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsNullStockSale", model.IsNullStockSale),
					new SqlParameter("@IsSupplierTransport", model.IsSupplierTransport),
					new SqlParameter("@MobileDescription", model.MobileDescription),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@NetWeight", model.NetWeight),
					new SqlParameter("@Number", model.Number),
					new SqlParameter("@PackageRate", model.PackageRate),
					new SqlParameter("@Packing", model.Packing),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@Price_Cost", model.Price_Cost),
					new SqlParameter("@Price_Market", model.Price_Market),
					new SqlParameter("@Price_reserve", model.Price_reserve),
					new SqlParameter("@Price_reserve_per", model.Price_reserve_per),
					new SqlParameter("@Price_Sale", model.Price_Sale),
					new SqlParameter("@Pro_Tag_id", model.Pro_Tag_id),
					new SqlParameter("@Pro_Type_id", model.Pro_Type_id),
					new SqlParameter("@Pro_Type_id_other", model.Pro_Type_id_other),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@ProPerty131", model.ProPerty131),
					new SqlParameter("@ProPerty132", model.ProPerty132),
					new SqlParameter("@ProPerty133", model.ProPerty133),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@ProPertyMain", model.ProPertyMain),
					new SqlParameter("@Remarks", model.Remarks),
					new SqlParameter("@Reserve_days", model.Reserve_days),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@Service", model.Service),
					new SqlParameter("@Site_ids", model.Site_ids),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Specification", model.Specification),
					new SqlParameter("@Star_Comment", model.Star_Comment),
					new SqlParameter("@StepPrice", model.StepPrice),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Supplier_ProductType_ids", model.Supplier_ProductType_ids),
					new SqlParameter("@Tags", model.Tags),
					new SqlParameter("@taobaoid", model.taobaoid),
					new SqlParameter("@taobaoid_type", model.taobaoid_type),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Edit", model.Time_Edit),
					new SqlParameter("@Time_Expired", model.Time_Expired),
					new SqlParameter("@Time_OnSale", model.Time_OnSale),
					new SqlParameter("@Time_Start", model.Time_Start),
					new SqlParameter("@Type_id_ProductStatus", model.Type_id_ProductStatus),
					new SqlParameter("@Type_id_ProductType", model.Type_id_ProductType),
					new SqlParameter("@Units_id", model.Units_id),
					new SqlParameter("@UserLevel_ids_buy", model.UserLevel_ids_buy),
					new SqlParameter("@UserLevel_ids_priceshow", model.UserLevel_ids_priceshow),
					new SqlParameter("@UserLevel_ids_show", model.UserLevel_ids_show),
					new SqlParameter("@UserLevelCount", model.UserLevelCount),
					new SqlParameter("@UserLevelPrice", model.UserLevelPrice),
					new SqlParameter("@VolumeH", model.VolumeH),
					new SqlParameter("@VolumeL", model.VolumeL),
					new SqlParameter("@VolumeW", model.VolumeW),
					new SqlParameter("@Weight", model.Weight),
					new SqlParameter("@Location", model.Location)};

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
		public void Update(Lebi_Product model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Brand_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Brand_id")+"= @Brand_id");
			if((","+model.UpdateCols+",").IndexOf(",Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+"= @Code");
			if((","+model.UpdateCols+",").IndexOf(",Count_Comment,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Comment")+"= @Count_Comment");
			if((","+model.UpdateCols+",").IndexOf(",Count_Freeze,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Freeze")+"= @Count_Freeze");
			if((","+model.UpdateCols+",").IndexOf(",Count_Like,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Like")+"= @Count_Like");
			if((","+model.UpdateCols+",").IndexOf(",Count_Limit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Limit")+"= @Count_Limit");
			if((","+model.UpdateCols+",").IndexOf(",Count_Sales,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Sales")+"= @Count_Sales");
			if((","+model.UpdateCols+",").IndexOf(",Count_Sales_Show,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Sales_Show")+"= @Count_Sales_Show");
			if((","+model.UpdateCols+",").IndexOf(",Count_SalesFalse,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_SalesFalse")+"= @Count_SalesFalse");
			if((","+model.UpdateCols+",").IndexOf(",Count_Stock,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Stock")+"= @Count_Stock");
			if((","+model.UpdateCols+",").IndexOf(",Count_StockCaution,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_StockCaution")+"= @Count_StockCaution");
			if((","+model.UpdateCols+",").IndexOf(",Count_Views,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Views")+"= @Count_Views");
			if((","+model.UpdateCols+",").IndexOf(",Count_Views_Show,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Views_Show")+"= @Count_Views_Show");
			if((","+model.UpdateCols+",").IndexOf(",Count_ViewsFalse,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_ViewsFalse")+"= @Count_ViewsFalse");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",FreezeRemark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("FreezeRemark")+"= @FreezeRemark");
			if((","+model.UpdateCols+",").IndexOf(",ImageBig,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageBig")+"= @ImageBig");
			if((","+model.UpdateCols+",").IndexOf(",ImageMedium,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageMedium")+"= @ImageMedium");
			if((","+model.UpdateCols+",").IndexOf(",ImageOriginal,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageOriginal")+"= @ImageOriginal");
			if((","+model.UpdateCols+",").IndexOf(",Images,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Images")+"= @Images");
			if((","+model.UpdateCols+",").IndexOf(",ImageSmall,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageSmall")+"= @ImageSmall");
			if((","+model.UpdateCols+",").IndexOf(",Introduction,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Introduction")+"= @Introduction");
			if((","+model.UpdateCols+",").IndexOf(",IsCombo,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCombo")+"= @IsCombo");
			if((","+model.UpdateCols+",").IndexOf(",IsDel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+"= @IsDel");
			if((","+model.UpdateCols+",").IndexOf(",IsNullStockSale,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsNullStockSale")+"= @IsNullStockSale");
			if((","+model.UpdateCols+",").IndexOf(",IsSupplierTransport,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierTransport")+"= @IsSupplierTransport");
			if((","+model.UpdateCols+",").IndexOf(",MobileDescription,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("MobileDescription")+"= @MobileDescription");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",NetWeight,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NetWeight")+"= @NetWeight");
			if((","+model.UpdateCols+",").IndexOf(",Number,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Number")+"= @Number");
			if((","+model.UpdateCols+",").IndexOf(",PackageRate,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PackageRate")+"= @PackageRate");
			if((","+model.UpdateCols+",").IndexOf(",Packing,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Packing")+"= @Packing");
			if((","+model.UpdateCols+",").IndexOf(",Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+"= @Price");
			if((","+model.UpdateCols+",").IndexOf(",Price_Cost,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Cost")+"= @Price_Cost");
			if((","+model.UpdateCols+",").IndexOf(",Price_Market,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Market")+"= @Price_Market");
			if((","+model.UpdateCols+",").IndexOf(",Price_reserve,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_reserve")+"= @Price_reserve");
			if((","+model.UpdateCols+",").IndexOf(",Price_reserve_per,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_reserve_per")+"= @Price_reserve_per");
			if((","+model.UpdateCols+",").IndexOf(",Price_Sale,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Sale")+"= @Price_Sale");
			if((","+model.UpdateCols+",").IndexOf(",Pro_Tag_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Tag_id")+"= @Pro_Tag_id");
			if((","+model.UpdateCols+",").IndexOf(",Pro_Type_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_id")+"= @Pro_Type_id");
			if((","+model.UpdateCols+",").IndexOf(",Pro_Type_id_other,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_id_other")+"= @Pro_Type_id_other");
			if((","+model.UpdateCols+",").IndexOf(",Product_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_id")+"= @Product_id");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty131,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty131")+"= @ProPerty131");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty132,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty132")+"= @ProPerty132");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty133,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty133")+"= @ProPerty133");
			if((","+model.UpdateCols+",").IndexOf(",ProPerty134,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPerty134")+"= @ProPerty134");
			if((","+model.UpdateCols+",").IndexOf(",ProPertyMain,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProPertyMain")+"= @ProPertyMain");
			if((","+model.UpdateCols+",").IndexOf(",Remarks,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remarks")+"= @Remarks");
			if((","+model.UpdateCols+",").IndexOf(",Reserve_days,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Reserve_days")+"= @Reserve_days");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+"= @SEO_Description");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Keywords,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+"= @SEO_Keywords");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+"= @SEO_Title");
			if((","+model.UpdateCols+",").IndexOf(",Service,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Service")+"= @Service");
			if((","+model.UpdateCols+",").IndexOf(",Site_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_ids")+"= @Site_ids");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Specification,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Specification")+"= @Specification");
			if((","+model.UpdateCols+",").IndexOf(",Star_Comment,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Star_Comment")+"= @Star_Comment");
			if((","+model.UpdateCols+",").IndexOf(",StepPrice,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("StepPrice")+"= @StepPrice");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_ProductType_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_ProductType_ids")+"= @Supplier_ProductType_ids");
			if((","+model.UpdateCols+",").IndexOf(",Tags,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Tags")+"= @Tags");
			if((","+model.UpdateCols+",").IndexOf(",taobaoid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("taobaoid")+"= @taobaoid");
			if((","+model.UpdateCols+",").IndexOf(",taobaoid_type,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("taobaoid_type")+"= @taobaoid_type");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Edit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Edit")+"= @Time_Edit");
			if((","+model.UpdateCols+",").IndexOf(",Time_Expired,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Expired")+"= @Time_Expired");
			if((","+model.UpdateCols+",").IndexOf(",Time_OnSale,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_OnSale")+"= @Time_OnSale");
			if((","+model.UpdateCols+",").IndexOf(",Time_Start,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Start")+"= @Time_Start");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_ProductStatus,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_ProductStatus")+"= @Type_id_ProductStatus");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_ProductType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_ProductType")+"= @Type_id_ProductType");
			if((","+model.UpdateCols+",").IndexOf(",Units_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Units_id")+"= @Units_id");
			if((","+model.UpdateCols+",").IndexOf(",UserLevel_ids_buy,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids_buy")+"= @UserLevel_ids_buy");
			if((","+model.UpdateCols+",").IndexOf(",UserLevel_ids_priceshow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids_priceshow")+"= @UserLevel_ids_priceshow");
			if((","+model.UpdateCols+",").IndexOf(",UserLevel_ids_show,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_ids_show")+"= @UserLevel_ids_show");
			if((","+model.UpdateCols+",").IndexOf(",UserLevelCount,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevelCount")+"= @UserLevelCount");
			if((","+model.UpdateCols+",").IndexOf(",UserLevelPrice,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevelPrice")+"= @UserLevelPrice");
			if((","+model.UpdateCols+",").IndexOf(",VolumeH,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("VolumeH")+"= @VolumeH");
			if((","+model.UpdateCols+",").IndexOf(",VolumeL,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("VolumeL")+"= @VolumeL");
			if((","+model.UpdateCols+",").IndexOf(",VolumeW,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("VolumeW")+"= @VolumeW");
			if((","+model.UpdateCols+",").IndexOf(",Weight,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight")+"= @Weight");
			if((","+model.UpdateCols+",").IndexOf(",Location,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Location")+"= @Location");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Brand_id", model.Brand_id),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@Count_Comment", model.Count_Comment),
					new SqlParameter("@Count_Freeze", model.Count_Freeze),
					new SqlParameter("@Count_Like", model.Count_Like),
					new SqlParameter("@Count_Limit", model.Count_Limit),
					new SqlParameter("@Count_Sales", model.Count_Sales),
					new SqlParameter("@Count_Sales_Show", model.Count_Sales_Show),
					new SqlParameter("@Count_SalesFalse", model.Count_SalesFalse),
					new SqlParameter("@Count_Stock", model.Count_Stock),
					new SqlParameter("@Count_StockCaution", model.Count_StockCaution),
					new SqlParameter("@Count_Views", model.Count_Views),
					new SqlParameter("@Count_Views_Show", model.Count_Views_Show),
					new SqlParameter("@Count_ViewsFalse", model.Count_ViewsFalse),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@FreezeRemark", model.FreezeRemark),
					new SqlParameter("@ImageBig", model.ImageBig),
					new SqlParameter("@ImageMedium", model.ImageMedium),
					new SqlParameter("@ImageOriginal", model.ImageOriginal),
					new SqlParameter("@Images", model.Images),
					new SqlParameter("@ImageSmall", model.ImageSmall),
					new SqlParameter("@Introduction", model.Introduction),
					new SqlParameter("@IsCombo", model.IsCombo),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsNullStockSale", model.IsNullStockSale),
					new SqlParameter("@IsSupplierTransport", model.IsSupplierTransport),
					new SqlParameter("@MobileDescription", model.MobileDescription),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@NetWeight", model.NetWeight),
					new SqlParameter("@Number", model.Number),
					new SqlParameter("@PackageRate", model.PackageRate),
					new SqlParameter("@Packing", model.Packing),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@Price_Cost", model.Price_Cost),
					new SqlParameter("@Price_Market", model.Price_Market),
					new SqlParameter("@Price_reserve", model.Price_reserve),
					new SqlParameter("@Price_reserve_per", model.Price_reserve_per),
					new SqlParameter("@Price_Sale", model.Price_Sale),
					new SqlParameter("@Pro_Tag_id", model.Pro_Tag_id),
					new SqlParameter("@Pro_Type_id", model.Pro_Type_id),
					new SqlParameter("@Pro_Type_id_other", model.Pro_Type_id_other),
					new SqlParameter("@Product_id", model.Product_id),
					new SqlParameter("@ProPerty131", model.ProPerty131),
					new SqlParameter("@ProPerty132", model.ProPerty132),
					new SqlParameter("@ProPerty133", model.ProPerty133),
					new SqlParameter("@ProPerty134", model.ProPerty134),
					new SqlParameter("@ProPertyMain", model.ProPertyMain),
					new SqlParameter("@Remarks", model.Remarks),
					new SqlParameter("@Reserve_days", model.Reserve_days),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@Service", model.Service),
					new SqlParameter("@Site_ids", model.Site_ids),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Specification", model.Specification),
					new SqlParameter("@Star_Comment", model.Star_Comment),
					new SqlParameter("@StepPrice", model.StepPrice),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Supplier_ProductType_ids", model.Supplier_ProductType_ids),
					new SqlParameter("@Tags", model.Tags),
					new SqlParameter("@taobaoid", model.taobaoid),
					new SqlParameter("@taobaoid_type", model.taobaoid_type),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Edit", model.Time_Edit),
					new SqlParameter("@Time_Expired", model.Time_Expired),
					new SqlParameter("@Time_OnSale", model.Time_OnSale),
					new SqlParameter("@Time_Start", model.Time_Start),
					new SqlParameter("@Type_id_ProductStatus", model.Type_id_ProductStatus),
					new SqlParameter("@Type_id_ProductType", model.Type_id_ProductType),
					new SqlParameter("@Units_id", model.Units_id),
					new SqlParameter("@UserLevel_ids_buy", model.UserLevel_ids_buy),
					new SqlParameter("@UserLevel_ids_priceshow", model.UserLevel_ids_priceshow),
					new SqlParameter("@UserLevel_ids_show", model.UserLevel_ids_show),
					new SqlParameter("@UserLevelCount", model.UserLevelCount),
					new SqlParameter("@UserLevelPrice", model.UserLevelPrice),
					new SqlParameter("@VolumeH", model.VolumeH),
					new SqlParameter("@VolumeL", model.VolumeL),
					new SqlParameter("@VolumeW", model.VolumeW),
					new SqlParameter("@Weight", model.Weight),
					new SqlParameter("@Location", model.Location)};
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
		public Lebi_Product GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Product;
		   }
		   Lebi_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Product",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Product GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Product;
		   }
		   Lebi_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Product",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Product GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Product;
		   }
		   Lebi_Product model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Product",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Product> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Product>;
		   }
		   List<Lebi_Product> list = new List<Lebi_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Product", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Product> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Product>;
		   }
		   List<Lebi_Product> list = new List<Lebi_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Product", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Product> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Product>;
		   }
		   List<Lebi_Product> list = new List<Lebi_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Product", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Product> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Product>;
		   }
		   List<Lebi_Product> list = new List<Lebi_Product>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Product", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Product BindForm(Lebi_Product model)
		{
			if (HttpContext.Current.Request["Brand_id"] != null)
				model.Brand_id=LB.Tools.RequestTool.RequestInt("Brand_id",0);
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestString("Code");
			if (HttpContext.Current.Request["Count_Comment"] != null)
				model.Count_Comment=LB.Tools.RequestTool.RequestInt("Count_Comment",0);
			if (HttpContext.Current.Request["Count_Freeze"] != null)
				model.Count_Freeze=LB.Tools.RequestTool.RequestInt("Count_Freeze",0);
			if (HttpContext.Current.Request["Count_Like"] != null)
				model.Count_Like=LB.Tools.RequestTool.RequestInt("Count_Like",0);
			if (HttpContext.Current.Request["Count_Limit"] != null)
				model.Count_Limit=LB.Tools.RequestTool.RequestInt("Count_Limit",0);
			if (HttpContext.Current.Request["Count_Sales"] != null)
				model.Count_Sales=LB.Tools.RequestTool.RequestInt("Count_Sales",0);
			if (HttpContext.Current.Request["Count_Sales_Show"] != null)
				model.Count_Sales_Show=LB.Tools.RequestTool.RequestInt("Count_Sales_Show",0);
			if (HttpContext.Current.Request["Count_SalesFalse"] != null)
				model.Count_SalesFalse=LB.Tools.RequestTool.RequestInt("Count_SalesFalse",0);
			if (HttpContext.Current.Request["Count_Stock"] != null)
				model.Count_Stock=LB.Tools.RequestTool.RequestInt("Count_Stock",0);
			if (HttpContext.Current.Request["Count_StockCaution"] != null)
				model.Count_StockCaution=LB.Tools.RequestTool.RequestInt("Count_StockCaution",0);
			if (HttpContext.Current.Request["Count_Views"] != null)
				model.Count_Views=LB.Tools.RequestTool.RequestInt("Count_Views",0);
			if (HttpContext.Current.Request["Count_Views_Show"] != null)
				model.Count_Views_Show=LB.Tools.RequestTool.RequestInt("Count_Views_Show",0);
			if (HttpContext.Current.Request["Count_ViewsFalse"] != null)
				model.Count_ViewsFalse=LB.Tools.RequestTool.RequestInt("Count_ViewsFalse",0);
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["FreezeRemark"] != null)
				model.FreezeRemark=LB.Tools.RequestTool.RequestString("FreezeRemark");
			if (HttpContext.Current.Request["ImageBig"] != null)
				model.ImageBig=LB.Tools.RequestTool.RequestString("ImageBig");
			if (HttpContext.Current.Request["ImageMedium"] != null)
				model.ImageMedium=LB.Tools.RequestTool.RequestString("ImageMedium");
			if (HttpContext.Current.Request["ImageOriginal"] != null)
				model.ImageOriginal=LB.Tools.RequestTool.RequestString("ImageOriginal");
			if (HttpContext.Current.Request["Images"] != null)
				model.Images=LB.Tools.RequestTool.RequestString("Images");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestString("ImageSmall");
			if (HttpContext.Current.Request["Introduction"] != null)
				model.Introduction=LB.Tools.RequestTool.RequestString("Introduction");
			if (HttpContext.Current.Request["IsCombo"] != null)
				model.IsCombo=LB.Tools.RequestTool.RequestInt("IsCombo",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsNullStockSale"] != null)
				model.IsNullStockSale=LB.Tools.RequestTool.RequestInt("IsNullStockSale",0);
			if (HttpContext.Current.Request["IsSupplierTransport"] != null)
				model.IsSupplierTransport=LB.Tools.RequestTool.RequestInt("IsSupplierTransport",0);
			if (HttpContext.Current.Request["MobileDescription"] != null)
				model.MobileDescription=LB.Tools.RequestTool.RequestString("MobileDescription");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["NetWeight"] != null)
				model.NetWeight=LB.Tools.RequestTool.RequestDecimal("NetWeight",0);
			if (HttpContext.Current.Request["Number"] != null)
				model.Number=LB.Tools.RequestTool.RequestString("Number");
			if (HttpContext.Current.Request["PackageRate"] != null)
				model.PackageRate=LB.Tools.RequestTool.RequestInt("PackageRate",0);
			if (HttpContext.Current.Request["Packing"] != null)
				model.Packing=LB.Tools.RequestTool.RequestString("Packing");
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["Price_Cost"] != null)
				model.Price_Cost=LB.Tools.RequestTool.RequestDecimal("Price_Cost",0);
			if (HttpContext.Current.Request["Price_Market"] != null)
				model.Price_Market=LB.Tools.RequestTool.RequestDecimal("Price_Market",0);
			if (HttpContext.Current.Request["Price_reserve"] != null)
				model.Price_reserve=LB.Tools.RequestTool.RequestDecimal("Price_reserve",0);
			if (HttpContext.Current.Request["Price_reserve_per"] != null)
				model.Price_reserve_per=LB.Tools.RequestTool.RequestDecimal("Price_reserve_per",0);
			if (HttpContext.Current.Request["Price_Sale"] != null)
				model.Price_Sale=LB.Tools.RequestTool.RequestDecimal("Price_Sale",0);
			if (HttpContext.Current.Request["Pro_Tag_id"] != null)
				model.Pro_Tag_id=LB.Tools.RequestTool.RequestString("Pro_Tag_id");
			if (HttpContext.Current.Request["Pro_Type_id"] != null)
				model.Pro_Type_id=LB.Tools.RequestTool.RequestInt("Pro_Type_id",0);
			if (HttpContext.Current.Request["Pro_Type_id_other"] != null)
				model.Pro_Type_id_other=LB.Tools.RequestTool.RequestString("Pro_Type_id_other");
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["ProPerty131"] != null)
				model.ProPerty131=LB.Tools.RequestTool.RequestString("ProPerty131");
			if (HttpContext.Current.Request["ProPerty132"] != null)
				model.ProPerty132=LB.Tools.RequestTool.RequestString("ProPerty132");
			if (HttpContext.Current.Request["ProPerty133"] != null)
				model.ProPerty133=LB.Tools.RequestTool.RequestString("ProPerty133");
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestString("ProPerty134");
			if (HttpContext.Current.Request["ProPertyMain"] != null)
				model.ProPertyMain=LB.Tools.RequestTool.RequestInt("ProPertyMain",0);
			if (HttpContext.Current.Request["Remarks"] != null)
				model.Remarks=LB.Tools.RequestTool.RequestString("Remarks");
			if (HttpContext.Current.Request["Reserve_days"] != null)
				model.Reserve_days=LB.Tools.RequestTool.RequestInt("Reserve_days",0);
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestString("SEO_Title");
			if (HttpContext.Current.Request["Service"] != null)
				model.Service=LB.Tools.RequestTool.RequestString("Service");
			if (HttpContext.Current.Request["Site_ids"] != null)
				model.Site_ids=LB.Tools.RequestTool.RequestString("Site_ids");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Specification"] != null)
				model.Specification=LB.Tools.RequestTool.RequestString("Specification");
			if (HttpContext.Current.Request["Star_Comment"] != null)
				model.Star_Comment=LB.Tools.RequestTool.RequestDecimal("Star_Comment",0);
			if (HttpContext.Current.Request["StepPrice"] != null)
				model.StepPrice=LB.Tools.RequestTool.RequestString("StepPrice");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Supplier_ProductType_ids"] != null)
				model.Supplier_ProductType_ids=LB.Tools.RequestTool.RequestString("Supplier_ProductType_ids");
			if (HttpContext.Current.Request["Tags"] != null)
				model.Tags=LB.Tools.RequestTool.RequestString("Tags");
			if (HttpContext.Current.Request["taobaoid"] != null)
				model.taobaoid=LB.Tools.RequestTool.RequestString("taobaoid");
			if (HttpContext.Current.Request["taobaoid_type"] != null)
				model.taobaoid_type=LB.Tools.RequestTool.RequestString("taobaoid_type");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Edit"] != null)
				model.Time_Edit=LB.Tools.RequestTool.RequestTime("Time_Edit", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Expired"] != null)
				model.Time_Expired=LB.Tools.RequestTool.RequestTime("Time_Expired", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_OnSale"] != null)
				model.Time_OnSale=LB.Tools.RequestTool.RequestTime("Time_OnSale", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Start"] != null)
				model.Time_Start=LB.Tools.RequestTool.RequestTime("Time_Start", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_ProductStatus"] != null)
				model.Type_id_ProductStatus=LB.Tools.RequestTool.RequestInt("Type_id_ProductStatus",0);
			if (HttpContext.Current.Request["Type_id_ProductType"] != null)
				model.Type_id_ProductType=LB.Tools.RequestTool.RequestInt("Type_id_ProductType",0);
			if (HttpContext.Current.Request["Units_id"] != null)
				model.Units_id=LB.Tools.RequestTool.RequestInt("Units_id",0);
			if (HttpContext.Current.Request["UserLevel_ids_buy"] != null)
				model.UserLevel_ids_buy=LB.Tools.RequestTool.RequestString("UserLevel_ids_buy");
			if (HttpContext.Current.Request["UserLevel_ids_priceshow"] != null)
				model.UserLevel_ids_priceshow=LB.Tools.RequestTool.RequestString("UserLevel_ids_priceshow");
			if (HttpContext.Current.Request["UserLevel_ids_show"] != null)
				model.UserLevel_ids_show=LB.Tools.RequestTool.RequestString("UserLevel_ids_show");
			if (HttpContext.Current.Request["UserLevelCount"] != null)
				model.UserLevelCount=LB.Tools.RequestTool.RequestString("UserLevelCount");
			if (HttpContext.Current.Request["UserLevelPrice"] != null)
				model.UserLevelPrice=LB.Tools.RequestTool.RequestString("UserLevelPrice");
			if (HttpContext.Current.Request["VolumeH"] != null)
				model.VolumeH=LB.Tools.RequestTool.RequestDecimal("VolumeH",0);
			if (HttpContext.Current.Request["VolumeL"] != null)
				model.VolumeL=LB.Tools.RequestTool.RequestDecimal("VolumeL",0);
			if (HttpContext.Current.Request["VolumeW"] != null)
				model.VolumeW=LB.Tools.RequestTool.RequestDecimal("VolumeW",0);
			if (HttpContext.Current.Request["Weight"] != null)
				model.Weight=LB.Tools.RequestTool.RequestDecimal("Weight",0);
			if (HttpContext.Current.Request["Location"] != null)
				model.Location=LB.Tools.RequestTool.RequestString("Location");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Product SafeBindForm(Lebi_Product model)
		{
			if (HttpContext.Current.Request["Brand_id"] != null)
				model.Brand_id=LB.Tools.RequestTool.RequestInt("Brand_id",0);
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestSafeString("Code");
			if (HttpContext.Current.Request["Count_Comment"] != null)
				model.Count_Comment=LB.Tools.RequestTool.RequestInt("Count_Comment",0);
			if (HttpContext.Current.Request["Count_Freeze"] != null)
				model.Count_Freeze=LB.Tools.RequestTool.RequestInt("Count_Freeze",0);
			if (HttpContext.Current.Request["Count_Like"] != null)
				model.Count_Like=LB.Tools.RequestTool.RequestInt("Count_Like",0);
			if (HttpContext.Current.Request["Count_Limit"] != null)
				model.Count_Limit=LB.Tools.RequestTool.RequestInt("Count_Limit",0);
			if (HttpContext.Current.Request["Count_Sales"] != null)
				model.Count_Sales=LB.Tools.RequestTool.RequestInt("Count_Sales",0);
			if (HttpContext.Current.Request["Count_Sales_Show"] != null)
				model.Count_Sales_Show=LB.Tools.RequestTool.RequestInt("Count_Sales_Show",0);
			if (HttpContext.Current.Request["Count_SalesFalse"] != null)
				model.Count_SalesFalse=LB.Tools.RequestTool.RequestInt("Count_SalesFalse",0);
			if (HttpContext.Current.Request["Count_Stock"] != null)
				model.Count_Stock=LB.Tools.RequestTool.RequestInt("Count_Stock",0);
			if (HttpContext.Current.Request["Count_StockCaution"] != null)
				model.Count_StockCaution=LB.Tools.RequestTool.RequestInt("Count_StockCaution",0);
			if (HttpContext.Current.Request["Count_Views"] != null)
				model.Count_Views=LB.Tools.RequestTool.RequestInt("Count_Views",0);
			if (HttpContext.Current.Request["Count_Views_Show"] != null)
				model.Count_Views_Show=LB.Tools.RequestTool.RequestInt("Count_Views_Show",0);
			if (HttpContext.Current.Request["Count_ViewsFalse"] != null)
				model.Count_ViewsFalse=LB.Tools.RequestTool.RequestInt("Count_ViewsFalse",0);
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["FreezeRemark"] != null)
				model.FreezeRemark=LB.Tools.RequestTool.RequestSafeString("FreezeRemark");
			if (HttpContext.Current.Request["ImageBig"] != null)
				model.ImageBig=LB.Tools.RequestTool.RequestSafeString("ImageBig");
			if (HttpContext.Current.Request["ImageMedium"] != null)
				model.ImageMedium=LB.Tools.RequestTool.RequestSafeString("ImageMedium");
			if (HttpContext.Current.Request["ImageOriginal"] != null)
				model.ImageOriginal=LB.Tools.RequestTool.RequestSafeString("ImageOriginal");
			if (HttpContext.Current.Request["Images"] != null)
				model.Images=LB.Tools.RequestTool.RequestSafeString("Images");
			if (HttpContext.Current.Request["ImageSmall"] != null)
				model.ImageSmall=LB.Tools.RequestTool.RequestSafeString("ImageSmall");
			if (HttpContext.Current.Request["Introduction"] != null)
				model.Introduction=LB.Tools.RequestTool.RequestSafeString("Introduction");
			if (HttpContext.Current.Request["IsCombo"] != null)
				model.IsCombo=LB.Tools.RequestTool.RequestInt("IsCombo",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsNullStockSale"] != null)
				model.IsNullStockSale=LB.Tools.RequestTool.RequestInt("IsNullStockSale",0);
			if (HttpContext.Current.Request["IsSupplierTransport"] != null)
				model.IsSupplierTransport=LB.Tools.RequestTool.RequestInt("IsSupplierTransport",0);
			if (HttpContext.Current.Request["MobileDescription"] != null)
				model.MobileDescription=LB.Tools.RequestTool.RequestSafeString("MobileDescription");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["NetWeight"] != null)
				model.NetWeight=LB.Tools.RequestTool.RequestDecimal("NetWeight",0);
			if (HttpContext.Current.Request["Number"] != null)
				model.Number=LB.Tools.RequestTool.RequestSafeString("Number");
			if (HttpContext.Current.Request["PackageRate"] != null)
				model.PackageRate=LB.Tools.RequestTool.RequestInt("PackageRate",0);
			if (HttpContext.Current.Request["Packing"] != null)
				model.Packing=LB.Tools.RequestTool.RequestSafeString("Packing");
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["Price_Cost"] != null)
				model.Price_Cost=LB.Tools.RequestTool.RequestDecimal("Price_Cost",0);
			if (HttpContext.Current.Request["Price_Market"] != null)
				model.Price_Market=LB.Tools.RequestTool.RequestDecimal("Price_Market",0);
			if (HttpContext.Current.Request["Price_reserve"] != null)
				model.Price_reserve=LB.Tools.RequestTool.RequestDecimal("Price_reserve",0);
			if (HttpContext.Current.Request["Price_reserve_per"] != null)
				model.Price_reserve_per=LB.Tools.RequestTool.RequestDecimal("Price_reserve_per",0);
			if (HttpContext.Current.Request["Price_Sale"] != null)
				model.Price_Sale=LB.Tools.RequestTool.RequestDecimal("Price_Sale",0);
			if (HttpContext.Current.Request["Pro_Tag_id"] != null)
				model.Pro_Tag_id=LB.Tools.RequestTool.RequestSafeString("Pro_Tag_id");
			if (HttpContext.Current.Request["Pro_Type_id"] != null)
				model.Pro_Type_id=LB.Tools.RequestTool.RequestInt("Pro_Type_id",0);
			if (HttpContext.Current.Request["Pro_Type_id_other"] != null)
				model.Pro_Type_id_other=LB.Tools.RequestTool.RequestSafeString("Pro_Type_id_other");
			if (HttpContext.Current.Request["Product_id"] != null)
				model.Product_id=LB.Tools.RequestTool.RequestInt("Product_id",0);
			if (HttpContext.Current.Request["ProPerty131"] != null)
				model.ProPerty131=LB.Tools.RequestTool.RequestSafeString("ProPerty131");
			if (HttpContext.Current.Request["ProPerty132"] != null)
				model.ProPerty132=LB.Tools.RequestTool.RequestSafeString("ProPerty132");
			if (HttpContext.Current.Request["ProPerty133"] != null)
				model.ProPerty133=LB.Tools.RequestTool.RequestSafeString("ProPerty133");
			if (HttpContext.Current.Request["ProPerty134"] != null)
				model.ProPerty134=LB.Tools.RequestTool.RequestSafeString("ProPerty134");
			if (HttpContext.Current.Request["ProPertyMain"] != null)
				model.ProPertyMain=LB.Tools.RequestTool.RequestInt("ProPertyMain",0);
			if (HttpContext.Current.Request["Remarks"] != null)
				model.Remarks=LB.Tools.RequestTool.RequestSafeString("Remarks");
			if (HttpContext.Current.Request["Reserve_days"] != null)
				model.Reserve_days=LB.Tools.RequestTool.RequestInt("Reserve_days",0);
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestSafeString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestSafeString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestSafeString("SEO_Title");
			if (HttpContext.Current.Request["Service"] != null)
				model.Service=LB.Tools.RequestTool.RequestSafeString("Service");
			if (HttpContext.Current.Request["Site_ids"] != null)
				model.Site_ids=LB.Tools.RequestTool.RequestSafeString("Site_ids");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Specification"] != null)
				model.Specification=LB.Tools.RequestTool.RequestSafeString("Specification");
			if (HttpContext.Current.Request["Star_Comment"] != null)
				model.Star_Comment=LB.Tools.RequestTool.RequestDecimal("Star_Comment",0);
			if (HttpContext.Current.Request["StepPrice"] != null)
				model.StepPrice=LB.Tools.RequestTool.RequestSafeString("StepPrice");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Supplier_ProductType_ids"] != null)
				model.Supplier_ProductType_ids=LB.Tools.RequestTool.RequestSafeString("Supplier_ProductType_ids");
			if (HttpContext.Current.Request["Tags"] != null)
				model.Tags=LB.Tools.RequestTool.RequestSafeString("Tags");
			if (HttpContext.Current.Request["taobaoid"] != null)
				model.taobaoid=LB.Tools.RequestTool.RequestSafeString("taobaoid");
			if (HttpContext.Current.Request["taobaoid_type"] != null)
				model.taobaoid_type=LB.Tools.RequestTool.RequestSafeString("taobaoid_type");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Edit"] != null)
				model.Time_Edit=LB.Tools.RequestTool.RequestTime("Time_Edit", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Expired"] != null)
				model.Time_Expired=LB.Tools.RequestTool.RequestTime("Time_Expired", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_OnSale"] != null)
				model.Time_OnSale=LB.Tools.RequestTool.RequestTime("Time_OnSale", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Start"] != null)
				model.Time_Start=LB.Tools.RequestTool.RequestTime("Time_Start", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_ProductStatus"] != null)
				model.Type_id_ProductStatus=LB.Tools.RequestTool.RequestInt("Type_id_ProductStatus",0);
			if (HttpContext.Current.Request["Type_id_ProductType"] != null)
				model.Type_id_ProductType=LB.Tools.RequestTool.RequestInt("Type_id_ProductType",0);
			if (HttpContext.Current.Request["Units_id"] != null)
				model.Units_id=LB.Tools.RequestTool.RequestInt("Units_id",0);
			if (HttpContext.Current.Request["UserLevel_ids_buy"] != null)
				model.UserLevel_ids_buy=LB.Tools.RequestTool.RequestSafeString("UserLevel_ids_buy");
			if (HttpContext.Current.Request["UserLevel_ids_priceshow"] != null)
				model.UserLevel_ids_priceshow=LB.Tools.RequestTool.RequestSafeString("UserLevel_ids_priceshow");
			if (HttpContext.Current.Request["UserLevel_ids_show"] != null)
				model.UserLevel_ids_show=LB.Tools.RequestTool.RequestSafeString("UserLevel_ids_show");
			if (HttpContext.Current.Request["UserLevelCount"] != null)
				model.UserLevelCount=LB.Tools.RequestTool.RequestSafeString("UserLevelCount");
			if (HttpContext.Current.Request["UserLevelPrice"] != null)
				model.UserLevelPrice=LB.Tools.RequestTool.RequestSafeString("UserLevelPrice");
			if (HttpContext.Current.Request["VolumeH"] != null)
				model.VolumeH=LB.Tools.RequestTool.RequestDecimal("VolumeH",0);
			if (HttpContext.Current.Request["VolumeL"] != null)
				model.VolumeL=LB.Tools.RequestTool.RequestDecimal("VolumeL",0);
			if (HttpContext.Current.Request["VolumeW"] != null)
				model.VolumeW=LB.Tools.RequestTool.RequestDecimal("VolumeW",0);
			if (HttpContext.Current.Request["Weight"] != null)
				model.Weight=LB.Tools.RequestTool.RequestDecimal("Weight",0);
			if (HttpContext.Current.Request["Location"] != null)
				model.Location=LB.Tools.RequestTool.RequestSafeString("Location");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Product ReaderBind(IDataReader dataReader)
		{
			Lebi_Product model=new Lebi_Product();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Brand_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Brand_id= Convert.ToInt32(ojb);
			}
			model.Code=dataReader["Code"].ToString();
			ojb = dataReader["Count_Comment"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Comment= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Freeze"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Freeze= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Like"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Like= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Limit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Limit= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Sales"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Sales= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Sales_Show"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Sales_Show= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_SalesFalse"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_SalesFalse= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Stock"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Stock= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_StockCaution"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_StockCaution= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Views"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Views= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Views_Show"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Views_Show= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_ViewsFalse"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_ViewsFalse= Convert.ToInt32(ojb);
			}
			model.Description=dataReader["Description"].ToString();
			model.FreezeRemark=dataReader["FreezeRemark"].ToString();
			model.ImageBig=dataReader["ImageBig"].ToString();
			model.ImageMedium=dataReader["ImageMedium"].ToString();
			model.ImageOriginal=dataReader["ImageOriginal"].ToString();
			model.Images=dataReader["Images"].ToString();
			model.ImageSmall=dataReader["ImageSmall"].ToString();
			model.Introduction=dataReader["Introduction"].ToString();
			ojb = dataReader["IsCombo"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCombo= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsDel"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsDel= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsNullStockSale"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsNullStockSale= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSupplierTransport"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSupplierTransport= Convert.ToInt32(ojb);
			}
			model.MobileDescription=dataReader["MobileDescription"].ToString();
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["NetWeight"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.NetWeight=(decimal)ojb;
			}
			model.Number=dataReader["Number"].ToString();
			ojb = dataReader["PackageRate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PackageRate= Convert.ToInt32(ojb);
			}
			model.Packing=dataReader["Packing"].ToString();
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
			ojb = dataReader["Price_Market"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_Market=(decimal)ojb;
			}
			ojb = dataReader["Price_reserve"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_reserve=(decimal)ojb;
			}
			ojb = dataReader["Price_reserve_per"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_reserve_per=(decimal)ojb;
			}
			ojb = dataReader["Price_Sale"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_Sale=(decimal)ojb;
			}
			model.Pro_Tag_id=dataReader["Pro_Tag_id"].ToString();
			ojb = dataReader["Pro_Type_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Pro_Type_id= Convert.ToInt32(ojb);
			}
			model.Pro_Type_id_other=dataReader["Pro_Type_id_other"].ToString();
			ojb = dataReader["Product_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Product_id= Convert.ToInt32(ojb);
			}
			model.ProPerty131=dataReader["ProPerty131"].ToString();
			model.ProPerty132=dataReader["ProPerty132"].ToString();
			model.ProPerty133=dataReader["ProPerty133"].ToString();
			model.ProPerty134=dataReader["ProPerty134"].ToString();
			ojb = dataReader["ProPertyMain"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ProPertyMain= Convert.ToInt32(ojb);
			}
			model.Remarks=dataReader["Remarks"].ToString();
			ojb = dataReader["Reserve_days"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Reserve_days= Convert.ToInt32(ojb);
			}
			model.SEO_Description=dataReader["SEO_Description"].ToString();
			model.SEO_Keywords=dataReader["SEO_Keywords"].ToString();
			model.SEO_Title=dataReader["SEO_Title"].ToString();
			model.Service=dataReader["Service"].ToString();
			model.Site_ids=dataReader["Site_ids"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			model.Specification=dataReader["Specification"].ToString();
			ojb = dataReader["Star_Comment"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Star_Comment=(decimal)ojb;
			}
			model.StepPrice=dataReader["StepPrice"].ToString();
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			model.Supplier_ProductType_ids=dataReader["Supplier_ProductType_ids"].ToString();
			model.Tags=dataReader["Tags"].ToString();
			model.taobaoid=dataReader["taobaoid"].ToString();
			model.taobaoid_type=dataReader["taobaoid_type"].ToString();
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Edit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Edit=(DateTime)ojb;
			}
			ojb = dataReader["Time_Expired"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Expired=(DateTime)ojb;
			}
			ojb = dataReader["Time_OnSale"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_OnSale=(DateTime)ojb;
			}
			ojb = dataReader["Time_Start"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Start=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_ProductStatus"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_ProductStatus= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Type_id_ProductType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_ProductType= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Units_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Units_id= Convert.ToInt32(ojb);
			}
			model.UserLevel_ids_buy=dataReader["UserLevel_ids_buy"].ToString();
			model.UserLevel_ids_priceshow=dataReader["UserLevel_ids_priceshow"].ToString();
			model.UserLevel_ids_show=dataReader["UserLevel_ids_show"].ToString();
			model.UserLevelCount=dataReader["UserLevelCount"].ToString();
			model.UserLevelPrice=dataReader["UserLevelPrice"].ToString();
			ojb = dataReader["VolumeH"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.VolumeH=(decimal)ojb;
			}
			ojb = dataReader["VolumeL"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.VolumeL=(decimal)ojb;
			}
			ojb = dataReader["VolumeW"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.VolumeW=(decimal)ojb;
			}
			ojb = dataReader["Weight"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Weight=(decimal)ojb;
			}
			model.Location=dataReader["Location"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

