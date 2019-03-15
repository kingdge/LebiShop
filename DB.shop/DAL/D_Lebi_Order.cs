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
	/// 数据访问类D_Lebi_Order。
	/// </summary>
	public partial class D_Lebi_Order
	{
		static D_Lebi_Order _Instance;
		public static D_Lebi_Order Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Order("Lebi_Order");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Order";
		public D_Lebi_Order(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Order", 0 , cachestr,seconds);
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
		public int Add(Lebi_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Area_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Address")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_MobilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Postalcode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark_User")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Order")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Pay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Product")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Transport")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Transport_Cut")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Bill")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Market")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Give")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Cut")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_UserCut")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Cost")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_UseCard311")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_UseCard312")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UseCardCode311")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UseCardCode312")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Volume")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Product")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Free")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Price_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Mark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("EditMoney_Order")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("EditMoney_Transport")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("EditMoney_Discount")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsVerified")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPaid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShipped")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShipped_All")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReceived")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReceived_All")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCompleted")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsInvalid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Verified")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Paid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Shipped")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Received")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Completed")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark_Admin")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_TaxRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_OrderType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPrintExpress")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Promotion_Type_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Mark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_ExchangeRate")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Msige")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Flag")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_fromorder")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCreateCash")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCreateNewOrder")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_CreateCash")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_CreateNewOrder")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Buy")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BLNo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ContainerNo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SealNo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Property")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("weixin_prepay_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierCash")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_OnlinepayFee")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_id_pay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_Date")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Refund_VAT")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Refund_Fee")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Delivery_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRefund")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Refund")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Promotion_Type_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_NickName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Paid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReserve")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_fanxianpay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Tax")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("KeyCode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PayNo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Source")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Project_ids")+")");
			strSql.Append(" values (");
			strSql.Append("@Code,@User_id,@User_UserName,@T_Name,@T_Area_id,@T_Address,@T_Phone,@T_MobilePhone,@T_Postalcode,@T_Email,@Remark_User,@Pay_id,@Pay,@OnlinePay_id,@OnlinePay,@OnlinePay_Code,@Money_Order,@Money_Pay,@Money_Product,@Money_Transport,@Money_Transport_Cut,@Money_Bill,@Money_Market,@Money_Give,@Money_Cut,@Money_UserCut,@Money_Cost,@Money_UseCard311,@Money_UseCard312,@UseCardCode311,@UseCardCode312,@Weight,@Volume,@Point,@Point_Product,@Point_Free,@Transport_Name,@Transport_id,@Transport_Price_id,@Transport_Code,@Transport_Mark,@EditMoney_Order,@EditMoney_Transport,@EditMoney_Discount,@IsVerified,@IsPaid,@IsShipped,@IsShipped_All,@IsReceived,@IsReceived_All,@IsCompleted,@IsInvalid,@Time_Add,@Time_Verified,@Time_Paid,@Time_Shipped,@Time_Received,@Time_Completed,@Remark_Admin,@BillType_Name,@BillType_id,@BillType_TaxRate,@Type_id_OrderType,@Order_id,@IsPrintExpress,@Promotion_Type_ids,@Mark,@Currency_id,@Currency_Code,@Currency_ExchangeRate,@Currency_Msige,@Flag,@Money_fromorder,@IsCreateCash,@IsCreateNewOrder,@Time_CreateCash,@Time_CreateNewOrder,@Site_id,@Point_Buy,@BLNo,@ContainerNo,@SealNo,@Supplier_id,@Money_Property,@weixin_prepay_id,@IsSupplierCash,@Money_OnlinepayFee,@Site_id_pay,@PickUp_id,@PickUp_Name,@PickUp_Date,@Refund_VAT,@Refund_Fee,@Language_id,@Supplier_Delivery_id,@IsRefund,@Time_Refund,@Promotion_Type_Name,@User_NickName,@Money_Paid,@IsReserve,@Money_fanxianpay,@Money_Tax,@DT_id,@DT_Money,@IsDel,@KeyCode,@PayNo,@Source,@Project_ids);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@User_UserName", model.User_UserName),
					new SqlParameter("@T_Name", model.T_Name),
					new SqlParameter("@T_Area_id", model.T_Area_id),
					new SqlParameter("@T_Address", model.T_Address),
					new SqlParameter("@T_Phone", model.T_Phone),
					new SqlParameter("@T_MobilePhone", model.T_MobilePhone),
					new SqlParameter("@T_Postalcode", model.T_Postalcode),
					new SqlParameter("@T_Email", model.T_Email),
					new SqlParameter("@Remark_User", model.Remark_User),
					new SqlParameter("@Pay_id", model.Pay_id),
					new SqlParameter("@Pay", model.Pay),
					new SqlParameter("@OnlinePay_id", model.OnlinePay_id),
					new SqlParameter("@OnlinePay", model.OnlinePay),
					new SqlParameter("@OnlinePay_Code", model.OnlinePay_Code),
					new SqlParameter("@Money_Order", model.Money_Order),
					new SqlParameter("@Money_Pay", model.Money_Pay),
					new SqlParameter("@Money_Product", model.Money_Product),
					new SqlParameter("@Money_Transport", model.Money_Transport),
					new SqlParameter("@Money_Transport_Cut", model.Money_Transport_Cut),
					new SqlParameter("@Money_Bill", model.Money_Bill),
					new SqlParameter("@Money_Market", model.Money_Market),
					new SqlParameter("@Money_Give", model.Money_Give),
					new SqlParameter("@Money_Cut", model.Money_Cut),
					new SqlParameter("@Money_UserCut", model.Money_UserCut),
					new SqlParameter("@Money_Cost", model.Money_Cost),
					new SqlParameter("@Money_UseCard311", model.Money_UseCard311),
					new SqlParameter("@Money_UseCard312", model.Money_UseCard312),
					new SqlParameter("@UseCardCode311", model.UseCardCode311),
					new SqlParameter("@UseCardCode312", model.UseCardCode312),
					new SqlParameter("@Weight", model.Weight),
					new SqlParameter("@Volume", model.Volume),
					new SqlParameter("@Point", model.Point),
					new SqlParameter("@Point_Product", model.Point_Product),
					new SqlParameter("@Point_Free", model.Point_Free),
					new SqlParameter("@Transport_Name", model.Transport_Name),
					new SqlParameter("@Transport_id", model.Transport_id),
					new SqlParameter("@Transport_Price_id", model.Transport_Price_id),
					new SqlParameter("@Transport_Code", model.Transport_Code),
					new SqlParameter("@Transport_Mark", model.Transport_Mark),
					new SqlParameter("@EditMoney_Order", model.EditMoney_Order),
					new SqlParameter("@EditMoney_Transport", model.EditMoney_Transport),
					new SqlParameter("@EditMoney_Discount", model.EditMoney_Discount),
					new SqlParameter("@IsVerified", model.IsVerified),
					new SqlParameter("@IsPaid", model.IsPaid),
					new SqlParameter("@IsShipped", model.IsShipped),
					new SqlParameter("@IsShipped_All", model.IsShipped_All),
					new SqlParameter("@IsReceived", model.IsReceived),
					new SqlParameter("@IsReceived_All", model.IsReceived_All),
					new SqlParameter("@IsCompleted", model.IsCompleted),
					new SqlParameter("@IsInvalid", model.IsInvalid),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Verified", model.Time_Verified),
					new SqlParameter("@Time_Paid", model.Time_Paid),
					new SqlParameter("@Time_Shipped", model.Time_Shipped),
					new SqlParameter("@Time_Received", model.Time_Received),
					new SqlParameter("@Time_Completed", model.Time_Completed),
					new SqlParameter("@Remark_Admin", model.Remark_Admin),
					new SqlParameter("@BillType_Name", model.BillType_Name),
					new SqlParameter("@BillType_id", model.BillType_id),
					new SqlParameter("@BillType_TaxRate", model.BillType_TaxRate),
					new SqlParameter("@Type_id_OrderType", model.Type_id_OrderType),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@IsPrintExpress", model.IsPrintExpress),
					new SqlParameter("@Promotion_Type_ids", model.Promotion_Type_ids),
					new SqlParameter("@Mark", model.Mark),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_ExchangeRate", model.Currency_ExchangeRate),
					new SqlParameter("@Currency_Msige", model.Currency_Msige),
					new SqlParameter("@Flag", model.Flag),
					new SqlParameter("@Money_fromorder", model.Money_fromorder),
					new SqlParameter("@IsCreateCash", model.IsCreateCash),
					new SqlParameter("@IsCreateNewOrder", model.IsCreateNewOrder),
					new SqlParameter("@Time_CreateCash", model.Time_CreateCash),
					new SqlParameter("@Time_CreateNewOrder", model.Time_CreateNewOrder),
					new SqlParameter("@Site_id", model.Site_id),
					new SqlParameter("@Point_Buy", model.Point_Buy),
					new SqlParameter("@BLNo", model.BLNo),
					new SqlParameter("@ContainerNo", model.ContainerNo),
					new SqlParameter("@SealNo", model.SealNo),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Money_Property", model.Money_Property),
					new SqlParameter("@weixin_prepay_id", model.weixin_prepay_id),
					new SqlParameter("@IsSupplierCash", model.IsSupplierCash),
					new SqlParameter("@Money_OnlinepayFee", model.Money_OnlinepayFee),
					new SqlParameter("@Site_id_pay", model.Site_id_pay),
					new SqlParameter("@PickUp_id", model.PickUp_id),
					new SqlParameter("@PickUp_Name", model.PickUp_Name),
					new SqlParameter("@PickUp_Date", model.PickUp_Date),
					new SqlParameter("@Refund_VAT", model.Refund_VAT),
					new SqlParameter("@Refund_Fee", model.Refund_Fee),
					new SqlParameter("@Language_id", model.Language_id),
					new SqlParameter("@Supplier_Delivery_id", model.Supplier_Delivery_id),
					new SqlParameter("@IsRefund", model.IsRefund),
					new SqlParameter("@Time_Refund", model.Time_Refund),
					new SqlParameter("@Promotion_Type_Name", model.Promotion_Type_Name),
					new SqlParameter("@User_NickName", model.User_NickName),
					new SqlParameter("@Money_Paid", model.Money_Paid),
					new SqlParameter("@IsReserve", model.IsReserve),
					new SqlParameter("@Money_fanxianpay", model.Money_fanxianpay),
					new SqlParameter("@Money_Tax", model.Money_Tax),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@DT_Money", model.DT_Money),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@KeyCode", model.KeyCode),
					new SqlParameter("@PayNo", model.PayNo),
					new SqlParameter("@Source", model.Source),
					new SqlParameter("@Project_ids", model.Project_ids)};

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
		public void Update(Lebi_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			List<string> upcols = new List<string>();
			string[] arrcols = model.UpdateCols.ToLower().Split(',');
			foreach (string c in arrcols)
			{
			    upcols.Add(c);
			}
			if(upcols.Contains("code") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Code")+"= @Code");
			if(upcols.Contains("user_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if(upcols.Contains("user_username") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_UserName")+"= @User_UserName");
			if(upcols.Contains("t_name") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Name")+"= @T_Name");
			if(upcols.Contains("t_area_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Area_id")+"= @T_Area_id");
			if(upcols.Contains("t_address") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Address")+"= @T_Address");
			if(upcols.Contains("t_phone") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Phone")+"= @T_Phone");
			if(upcols.Contains("t_mobilephone") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_MobilePhone")+"= @T_MobilePhone");
			if(upcols.Contains("t_postalcode") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Postalcode")+"= @T_Postalcode");
			if(upcols.Contains("t_email") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("T_Email")+"= @T_Email");
			if(upcols.Contains("remark_user") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark_User")+"= @Remark_User");
			if(upcols.Contains("pay_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay_id")+"= @Pay_id");
			if(upcols.Contains("pay") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay")+"= @Pay");
			if(upcols.Contains("onlinepay_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay_id")+"= @OnlinePay_id");
			if(upcols.Contains("onlinepay") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay")+"= @OnlinePay");
			if(upcols.Contains("onlinepay_code") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay_Code")+"= @OnlinePay_Code");
			if(upcols.Contains("money_order") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Order")+"= @Money_Order");
			if(upcols.Contains("money_pay") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Pay")+"= @Money_Pay");
			if(upcols.Contains("money_product") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Product")+"= @Money_Product");
			if(upcols.Contains("money_transport") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Transport")+"= @Money_Transport");
			if(upcols.Contains("money_transport_cut") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Transport_Cut")+"= @Money_Transport_Cut");
			if(upcols.Contains("money_bill") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Bill")+"= @Money_Bill");
			if(upcols.Contains("money_market") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Market")+"= @Money_Market");
			if(upcols.Contains("money_give") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Give")+"= @Money_Give");
			if(upcols.Contains("money_cut") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Cut")+"= @Money_Cut");
			if(upcols.Contains("money_usercut") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_UserCut")+"= @Money_UserCut");
			if(upcols.Contains("money_cost") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Cost")+"= @Money_Cost");
			if(upcols.Contains("money_usecard311") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_UseCard311")+"= @Money_UseCard311");
			if(upcols.Contains("money_usecard312") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_UseCard312")+"= @Money_UseCard312");
			if(upcols.Contains("usecardcode311") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UseCardCode311")+"= @UseCardCode311");
			if(upcols.Contains("usecardcode312") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UseCardCode312")+"= @UseCardCode312");
			if(upcols.Contains("weight") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight")+"= @Weight");
			if(upcols.Contains("volume") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Volume")+"= @Volume");
			if(upcols.Contains("point") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point")+"= @Point");
			if(upcols.Contains("point_product") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Product")+"= @Point_Product");
			if(upcols.Contains("point_free") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Free")+"= @Point_Free");
			if(upcols.Contains("transport_name") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Name")+"= @Transport_Name");
			if(upcols.Contains("transport_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_id")+"= @Transport_id");
			if(upcols.Contains("transport_price_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Price_id")+"= @Transport_Price_id");
			if(upcols.Contains("transport_code") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Code")+"= @Transport_Code");
			if(upcols.Contains("transport_mark") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Mark")+"= @Transport_Mark");
			if(upcols.Contains("editmoney_order") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("EditMoney_Order")+"= @EditMoney_Order");
			if(upcols.Contains("editmoney_transport") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("EditMoney_Transport")+"= @EditMoney_Transport");
			if(upcols.Contains("editmoney_discount") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("EditMoney_Discount")+"= @EditMoney_Discount");
			if(upcols.Contains("isverified") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsVerified")+"= @IsVerified");
			if(upcols.Contains("ispaid") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPaid")+"= @IsPaid");
			if(upcols.Contains("isshipped") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShipped")+"= @IsShipped");
			if(upcols.Contains("isshipped_all") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsShipped_All")+"= @IsShipped_All");
			if(upcols.Contains("isreceived") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReceived")+"= @IsReceived");
			if(upcols.Contains("isreceived_all") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReceived_All")+"= @IsReceived_All");
			if(upcols.Contains("iscompleted") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCompleted")+"= @IsCompleted");
			if(upcols.Contains("isinvalid") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsInvalid")+"= @IsInvalid");
			if(upcols.Contains("time_add") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if(upcols.Contains("time_verified") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Verified")+"= @Time_Verified");
			if(upcols.Contains("time_paid") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Paid")+"= @Time_Paid");
			if(upcols.Contains("time_shipped") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Shipped")+"= @Time_Shipped");
			if(upcols.Contains("time_received") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Received")+"= @Time_Received");
			if(upcols.Contains("time_completed") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Completed")+"= @Time_Completed");
			if(upcols.Contains("remark_admin") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark_Admin")+"= @Remark_Admin");
			if(upcols.Contains("billtype_name") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_Name")+"= @BillType_Name");
			if(upcols.Contains("billtype_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_id")+"= @BillType_id");
			if(upcols.Contains("billtype_taxrate") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillType_TaxRate")+"= @BillType_TaxRate");
			if(upcols.Contains("type_id_ordertype") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_OrderType")+"= @Type_id_OrderType");
			if(upcols.Contains("order_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Order_id")+"= @Order_id");
			if(upcols.Contains("isprintexpress") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPrintExpress")+"= @IsPrintExpress");
			if(upcols.Contains("promotion_type_ids") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Promotion_Type_ids")+"= @Promotion_Type_ids");
			if(upcols.Contains("mark") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Mark")+"= @Mark");
			if(upcols.Contains("currency_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+"= @Currency_id");
			if(upcols.Contains("currency_code") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+"= @Currency_Code");
			if(upcols.Contains("currency_exchangerate") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_ExchangeRate")+"= @Currency_ExchangeRate");
			if(upcols.Contains("currency_msige") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Msige")+"= @Currency_Msige");
			if(upcols.Contains("flag") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Flag")+"= @Flag");
			if(upcols.Contains("money_fromorder") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_fromorder")+"= @Money_fromorder");
			if(upcols.Contains("iscreatecash") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCreateCash")+"= @IsCreateCash");
			if(upcols.Contains("iscreateneworder") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCreateNewOrder")+"= @IsCreateNewOrder");
			if(upcols.Contains("time_createcash") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_CreateCash")+"= @Time_CreateCash");
			if(upcols.Contains("time_createneworder") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_CreateNewOrder")+"= @Time_CreateNewOrder");
			if(upcols.Contains("site_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_id")+"= @Site_id");
			if(upcols.Contains("point_buy") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point_Buy")+"= @Point_Buy");
			if(upcols.Contains("blno") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BLNo")+"= @BLNo");
			if(upcols.Contains("containerno") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ContainerNo")+"= @ContainerNo");
			if(upcols.Contains("sealno") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SealNo")+"= @SealNo");
			if(upcols.Contains("supplier_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if(upcols.Contains("money_property") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Property")+"= @Money_Property");
			if(upcols.Contains("weixin_prepay_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("weixin_prepay_id")+"= @weixin_prepay_id");
			if(upcols.Contains("issuppliercash") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierCash")+"= @IsSupplierCash");
			if(upcols.Contains("money_onlinepayfee") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_OnlinepayFee")+"= @Money_OnlinepayFee");
			if(upcols.Contains("site_id_pay") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_id_pay")+"= @Site_id_pay");
			if(upcols.Contains("pickup_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_id")+"= @PickUp_id");
			if(upcols.Contains("pickup_name") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_Name")+"= @PickUp_Name");
			if(upcols.Contains("pickup_date") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_Date")+"= @PickUp_Date");
			if(upcols.Contains("refund_vat") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Refund_VAT")+"= @Refund_VAT");
			if(upcols.Contains("refund_fee") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Refund_Fee")+"= @Refund_Fee");
			if(upcols.Contains("language_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language_id")+"= @Language_id");
			if(upcols.Contains("supplier_delivery_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Delivery_id")+"= @Supplier_Delivery_id");
			if(upcols.Contains("isrefund") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRefund")+"= @IsRefund");
			if(upcols.Contains("time_refund") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Refund")+"= @Time_Refund");
			if(upcols.Contains("promotion_type_name") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Promotion_Type_Name")+"= @Promotion_Type_Name");
			if(upcols.Contains("user_nickname") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_NickName")+"= @User_NickName");
			if(upcols.Contains("money_paid") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Paid")+"= @Money_Paid");
			if(upcols.Contains("isreserve") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsReserve")+"= @IsReserve");
			if(upcols.Contains("money_fanxianpay") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_fanxianpay")+"= @Money_fanxianpay");
			if(upcols.Contains("money_tax") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Tax")+"= @Money_Tax");
			if(upcols.Contains("dt_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+"= @DT_id");
			if(upcols.Contains("dt_money") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_Money")+"= @DT_Money");
			if(upcols.Contains("isdel") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+"= @IsDel");
			if(upcols.Contains("keycode") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("KeyCode")+"= @KeyCode");
			if(upcols.Contains("payno") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PayNo")+"= @PayNo");
			if(upcols.Contains("source") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Source")+"= @Source");
			if(upcols.Contains("project_ids") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Project_ids")+"= @Project_ids");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Code", model.Code),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@User_UserName", model.User_UserName),
					new SqlParameter("@T_Name", model.T_Name),
					new SqlParameter("@T_Area_id", model.T_Area_id),
					new SqlParameter("@T_Address", model.T_Address),
					new SqlParameter("@T_Phone", model.T_Phone),
					new SqlParameter("@T_MobilePhone", model.T_MobilePhone),
					new SqlParameter("@T_Postalcode", model.T_Postalcode),
					new SqlParameter("@T_Email", model.T_Email),
					new SqlParameter("@Remark_User", model.Remark_User),
					new SqlParameter("@Pay_id", model.Pay_id),
					new SqlParameter("@Pay", model.Pay),
					new SqlParameter("@OnlinePay_id", model.OnlinePay_id),
					new SqlParameter("@OnlinePay", model.OnlinePay),
					new SqlParameter("@OnlinePay_Code", model.OnlinePay_Code),
					new SqlParameter("@Money_Order", model.Money_Order),
					new SqlParameter("@Money_Pay", model.Money_Pay),
					new SqlParameter("@Money_Product", model.Money_Product),
					new SqlParameter("@Money_Transport", model.Money_Transport),
					new SqlParameter("@Money_Transport_Cut", model.Money_Transport_Cut),
					new SqlParameter("@Money_Bill", model.Money_Bill),
					new SqlParameter("@Money_Market", model.Money_Market),
					new SqlParameter("@Money_Give", model.Money_Give),
					new SqlParameter("@Money_Cut", model.Money_Cut),
					new SqlParameter("@Money_UserCut", model.Money_UserCut),
					new SqlParameter("@Money_Cost", model.Money_Cost),
					new SqlParameter("@Money_UseCard311", model.Money_UseCard311),
					new SqlParameter("@Money_UseCard312", model.Money_UseCard312),
					new SqlParameter("@UseCardCode311", model.UseCardCode311),
					new SqlParameter("@UseCardCode312", model.UseCardCode312),
					new SqlParameter("@Weight", model.Weight),
					new SqlParameter("@Volume", model.Volume),
					new SqlParameter("@Point", model.Point),
					new SqlParameter("@Point_Product", model.Point_Product),
					new SqlParameter("@Point_Free", model.Point_Free),
					new SqlParameter("@Transport_Name", model.Transport_Name),
					new SqlParameter("@Transport_id", model.Transport_id),
					new SqlParameter("@Transport_Price_id", model.Transport_Price_id),
					new SqlParameter("@Transport_Code", model.Transport_Code),
					new SqlParameter("@Transport_Mark", model.Transport_Mark),
					new SqlParameter("@EditMoney_Order", model.EditMoney_Order),
					new SqlParameter("@EditMoney_Transport", model.EditMoney_Transport),
					new SqlParameter("@EditMoney_Discount", model.EditMoney_Discount),
					new SqlParameter("@IsVerified", model.IsVerified),
					new SqlParameter("@IsPaid", model.IsPaid),
					new SqlParameter("@IsShipped", model.IsShipped),
					new SqlParameter("@IsShipped_All", model.IsShipped_All),
					new SqlParameter("@IsReceived", model.IsReceived),
					new SqlParameter("@IsReceived_All", model.IsReceived_All),
					new SqlParameter("@IsCompleted", model.IsCompleted),
					new SqlParameter("@IsInvalid", model.IsInvalid),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_Verified", model.Time_Verified),
					new SqlParameter("@Time_Paid", model.Time_Paid),
					new SqlParameter("@Time_Shipped", model.Time_Shipped),
					new SqlParameter("@Time_Received", model.Time_Received),
					new SqlParameter("@Time_Completed", model.Time_Completed),
					new SqlParameter("@Remark_Admin", model.Remark_Admin),
					new SqlParameter("@BillType_Name", model.BillType_Name),
					new SqlParameter("@BillType_id", model.BillType_id),
					new SqlParameter("@BillType_TaxRate", model.BillType_TaxRate),
					new SqlParameter("@Type_id_OrderType", model.Type_id_OrderType),
					new SqlParameter("@Order_id", model.Order_id),
					new SqlParameter("@IsPrintExpress", model.IsPrintExpress),
					new SqlParameter("@Promotion_Type_ids", model.Promotion_Type_ids),
					new SqlParameter("@Mark", model.Mark),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_ExchangeRate", model.Currency_ExchangeRate),
					new SqlParameter("@Currency_Msige", model.Currency_Msige),
					new SqlParameter("@Flag", model.Flag),
					new SqlParameter("@Money_fromorder", model.Money_fromorder),
					new SqlParameter("@IsCreateCash", model.IsCreateCash),
					new SqlParameter("@IsCreateNewOrder", model.IsCreateNewOrder),
					new SqlParameter("@Time_CreateCash", model.Time_CreateCash),
					new SqlParameter("@Time_CreateNewOrder", model.Time_CreateNewOrder),
					new SqlParameter("@Site_id", model.Site_id),
					new SqlParameter("@Point_Buy", model.Point_Buy),
					new SqlParameter("@BLNo", model.BLNo),
					new SqlParameter("@ContainerNo", model.ContainerNo),
					new SqlParameter("@SealNo", model.SealNo),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Money_Property", model.Money_Property),
					new SqlParameter("@weixin_prepay_id", model.weixin_prepay_id),
					new SqlParameter("@IsSupplierCash", model.IsSupplierCash),
					new SqlParameter("@Money_OnlinepayFee", model.Money_OnlinepayFee),
					new SqlParameter("@Site_id_pay", model.Site_id_pay),
					new SqlParameter("@PickUp_id", model.PickUp_id),
					new SqlParameter("@PickUp_Name", model.PickUp_Name),
					new SqlParameter("@PickUp_Date", model.PickUp_Date),
					new SqlParameter("@Refund_VAT", model.Refund_VAT),
					new SqlParameter("@Refund_Fee", model.Refund_Fee),
					new SqlParameter("@Language_id", model.Language_id),
					new SqlParameter("@Supplier_Delivery_id", model.Supplier_Delivery_id),
					new SqlParameter("@IsRefund", model.IsRefund),
					new SqlParameter("@Time_Refund", model.Time_Refund),
					new SqlParameter("@Promotion_Type_Name", model.Promotion_Type_Name),
					new SqlParameter("@User_NickName", model.User_NickName),
					new SqlParameter("@Money_Paid", model.Money_Paid),
					new SqlParameter("@IsReserve", model.IsReserve),
					new SqlParameter("@Money_fanxianpay", model.Money_fanxianpay),
					new SqlParameter("@Money_Tax", model.Money_Tax),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@DT_Money", model.DT_Money),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@KeyCode", model.KeyCode),
					new SqlParameter("@PayNo", model.PayNo),
					new SqlParameter("@Source", model.Source),
					new SqlParameter("@Project_ids", model.Project_ids)};
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
		public Lebi_Order GetModel(int id, string strFieldShow, int seconds=0)
		{
		   string strTableName = TableName;
		   strFieldShow = strFieldShow == "" ? "*" : strFieldShow;
		   string strWhere = "id="+id;
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = "select "+ strFieldShow + " "+ TableName + " where id="+id+"|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as Lebi_Order;
		   }
		   Lebi_Order model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader,strFieldShow);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Order",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Order GetModel(string strWhere, string strFieldShow, int seconds=0)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, "", "");
		           return GetModel(para, seconds);
		   }
		   string strTableName =TableName;
		   strFieldShow = strFieldShow == "" ? "*" : strFieldShow;
		   string cachekey = "";
		   string cachestr = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = "select "+ strFieldShow + " "+ TableName + " where "+strWhere+"|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as Lebi_Order;
		   }
		   Lebi_Order model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader,strFieldShow);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Order",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Order GetModel(SQLPara para, int seconds=0)
		{
		   string strTableName = TableName;
		   string strFieldShow = para.ShowField;
		   string strWhere = para.Where;
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = "select "+ strFieldShow + " "+ TableName + " where "+para.Where+"|"+para.ValueString+"|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as Lebi_Order;
		   }
		   Lebi_Order model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader,strFieldShow);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Order",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Order> GetList(string strWhere, string strFieldShow, string strFieldOrder, int PageSize, int page, int seconds=0)
		{
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, strFieldOrder, "");
		       return GetList(para,PageSize,page,seconds);
		   }
		   string strTableName = TableName;
		   string strFieldKey = "id";
		   strFieldShow = strFieldShow == "" ? "*" : strFieldShow;
		   string cachestr = "";
		   string cachekey = "";
		   if (BaseUtils.BaseUtilsInstance.MemcacheInstance != null && seconds > 0)
		   {
		       cachestr = strTableName + "|" + strFieldOrder + "|" + strWhere + "|" + PageSize + "|" + page + "|" + seconds;
		       cachekey = LB.Tools.Utils.MD5(cachestr);
		       var obj = LB.DataAccess.DB.Instance.GetMemchche(cachekey);
		       if (obj != null)
		           return obj as List<Lebi_Order>;
		   }
		   List<Lebi_Order> list = new List<Lebi_Order>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strTableName, strFieldKey, strFieldShow, strFieldOrder, strWhere, PageSize, page))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader,strFieldShow));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Order> GetList(SQLPara para, int PageSize, int page, int seconds=0)
		{
		   string strTableName = TableName;
		   string strFieldKey = "id";
		   string strFieldShow = para.ShowField;
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
		           return obj as List<Lebi_Order>;
		   }
		   List<Lebi_Order> list = new List<Lebi_Order>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strTableName, strFieldKey, strFieldShow, strFieldOrder, strWhere, PageSize, page,para.Para))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader,strFieldShow));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Order> GetList(string strWhere, string strFieldShow, string strFieldOrder, int seconds=0)
		{
		   strFieldShow = strFieldShow == "" ? "*" : strFieldShow;
		   if (strWhere.IndexOf("lbsql{") > 0)
		   {
		       SQLPara para = new SQLPara(strWhere, strFieldOrder, "");
		       return GetList(para, seconds);
		   }
		   StringBuilder strSql=new StringBuilder();
		   strSql.Append("select "+ strFieldShow + " from "+ TableName + " ");
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
		           return obj as List<Lebi_Order>;
		   }
		   List<Lebi_Order> list = new List<Lebi_Order>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strSql.ToString()))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader,strFieldShow));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Order> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Order>;
		   }
		   List<Lebi_Order> list = new List<Lebi_Order>();
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReader(strSql.ToString(), para.Para))
		   {
		       if(dataReader!=null)
		       {
		           while (dataReader.Read())
		           {
		               list.Add(ReaderBind(dataReader,para.ShowField));
		           }
		       }
		   }
		   if (cachekey != "" && list.Count > 0)
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Order", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Order BindForm(Lebi_Order model)
		{
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestString("Code");
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestString("User_UserName");
			if (HttpContext.Current.Request["T_Name"] != null)
				model.T_Name=LB.Tools.RequestTool.RequestString("T_Name");
			if (HttpContext.Current.Request["T_Area_id"] != null)
				model.T_Area_id=LB.Tools.RequestTool.RequestInt("T_Area_id",0);
			if (HttpContext.Current.Request["T_Address"] != null)
				model.T_Address=LB.Tools.RequestTool.RequestString("T_Address");
			if (HttpContext.Current.Request["T_Phone"] != null)
				model.T_Phone=LB.Tools.RequestTool.RequestString("T_Phone");
			if (HttpContext.Current.Request["T_MobilePhone"] != null)
				model.T_MobilePhone=LB.Tools.RequestTool.RequestString("T_MobilePhone");
			if (HttpContext.Current.Request["T_Postalcode"] != null)
				model.T_Postalcode=LB.Tools.RequestTool.RequestString("T_Postalcode");
			if (HttpContext.Current.Request["T_Email"] != null)
				model.T_Email=LB.Tools.RequestTool.RequestString("T_Email");
			if (HttpContext.Current.Request["Remark_User"] != null)
				model.Remark_User=LB.Tools.RequestTool.RequestString("Remark_User");
			if (HttpContext.Current.Request["Pay_id"] != null)
				model.Pay_id=LB.Tools.RequestTool.RequestInt("Pay_id",0);
			if (HttpContext.Current.Request["Pay"] != null)
				model.Pay=LB.Tools.RequestTool.RequestString("Pay");
			if (HttpContext.Current.Request["OnlinePay_id"] != null)
				model.OnlinePay_id=LB.Tools.RequestTool.RequestInt("OnlinePay_id",0);
			if (HttpContext.Current.Request["OnlinePay"] != null)
				model.OnlinePay=LB.Tools.RequestTool.RequestString("OnlinePay");
			if (HttpContext.Current.Request["OnlinePay_Code"] != null)
				model.OnlinePay_Code=LB.Tools.RequestTool.RequestString("OnlinePay_Code");
			if (HttpContext.Current.Request["Money_Order"] != null)
				model.Money_Order=LB.Tools.RequestTool.RequestDecimal("Money_Order",0);
			if (HttpContext.Current.Request["Money_Pay"] != null)
				model.Money_Pay=LB.Tools.RequestTool.RequestDecimal("Money_Pay",0);
			if (HttpContext.Current.Request["Money_Product"] != null)
				model.Money_Product=LB.Tools.RequestTool.RequestDecimal("Money_Product",0);
			if (HttpContext.Current.Request["Money_Transport"] != null)
				model.Money_Transport=LB.Tools.RequestTool.RequestDecimal("Money_Transport",0);
			if (HttpContext.Current.Request["Money_Transport_Cut"] != null)
				model.Money_Transport_Cut=LB.Tools.RequestTool.RequestDecimal("Money_Transport_Cut",0);
			if (HttpContext.Current.Request["Money_Bill"] != null)
				model.Money_Bill=LB.Tools.RequestTool.RequestDecimal("Money_Bill",0);
			if (HttpContext.Current.Request["Money_Market"] != null)
				model.Money_Market=LB.Tools.RequestTool.RequestDecimal("Money_Market",0);
			if (HttpContext.Current.Request["Money_Give"] != null)
				model.Money_Give=LB.Tools.RequestTool.RequestDecimal("Money_Give",0);
			if (HttpContext.Current.Request["Money_Cut"] != null)
				model.Money_Cut=LB.Tools.RequestTool.RequestDecimal("Money_Cut",0);
			if (HttpContext.Current.Request["Money_UserCut"] != null)
				model.Money_UserCut=LB.Tools.RequestTool.RequestDecimal("Money_UserCut",0);
			if (HttpContext.Current.Request["Money_Cost"] != null)
				model.Money_Cost=LB.Tools.RequestTool.RequestDecimal("Money_Cost",0);
			if (HttpContext.Current.Request["Money_UseCard311"] != null)
				model.Money_UseCard311=LB.Tools.RequestTool.RequestDecimal("Money_UseCard311",0);
			if (HttpContext.Current.Request["Money_UseCard312"] != null)
				model.Money_UseCard312=LB.Tools.RequestTool.RequestDecimal("Money_UseCard312",0);
			if (HttpContext.Current.Request["UseCardCode311"] != null)
				model.UseCardCode311=LB.Tools.RequestTool.RequestString("UseCardCode311");
			if (HttpContext.Current.Request["UseCardCode312"] != null)
				model.UseCardCode312=LB.Tools.RequestTool.RequestString("UseCardCode312");
			if (HttpContext.Current.Request["Weight"] != null)
				model.Weight=LB.Tools.RequestTool.RequestDecimal("Weight",0);
			if (HttpContext.Current.Request["Volume"] != null)
				model.Volume=LB.Tools.RequestTool.RequestDecimal("Volume",0);
			if (HttpContext.Current.Request["Point"] != null)
				model.Point=LB.Tools.RequestTool.RequestDecimal("Point",0);
			if (HttpContext.Current.Request["Point_Product"] != null)
				model.Point_Product=LB.Tools.RequestTool.RequestDecimal("Point_Product",0);
			if (HttpContext.Current.Request["Point_Free"] != null)
				model.Point_Free=LB.Tools.RequestTool.RequestDecimal("Point_Free",0);
			if (HttpContext.Current.Request["Transport_Name"] != null)
				model.Transport_Name=LB.Tools.RequestTool.RequestString("Transport_Name");
			if (HttpContext.Current.Request["Transport_id"] != null)
				model.Transport_id=LB.Tools.RequestTool.RequestInt("Transport_id",0);
			if (HttpContext.Current.Request["Transport_Price_id"] != null)
				model.Transport_Price_id=LB.Tools.RequestTool.RequestInt("Transport_Price_id",0);
			if (HttpContext.Current.Request["Transport_Code"] != null)
				model.Transport_Code=LB.Tools.RequestTool.RequestString("Transport_Code");
			if (HttpContext.Current.Request["Transport_Mark"] != null)
				model.Transport_Mark=LB.Tools.RequestTool.RequestString("Transport_Mark");
			if (HttpContext.Current.Request["EditMoney_Order"] != null)
				model.EditMoney_Order=LB.Tools.RequestTool.RequestDecimal("EditMoney_Order",0);
			if (HttpContext.Current.Request["EditMoney_Transport"] != null)
				model.EditMoney_Transport=LB.Tools.RequestTool.RequestDecimal("EditMoney_Transport",0);
			if (HttpContext.Current.Request["EditMoney_Discount"] != null)
				model.EditMoney_Discount=LB.Tools.RequestTool.RequestDecimal("EditMoney_Discount",0);
			if (HttpContext.Current.Request["IsVerified"] != null)
				model.IsVerified=LB.Tools.RequestTool.RequestInt("IsVerified",0);
			if (HttpContext.Current.Request["IsPaid"] != null)
				model.IsPaid=LB.Tools.RequestTool.RequestInt("IsPaid",0);
			if (HttpContext.Current.Request["IsShipped"] != null)
				model.IsShipped=LB.Tools.RequestTool.RequestInt("IsShipped",0);
			if (HttpContext.Current.Request["IsShipped_All"] != null)
				model.IsShipped_All=LB.Tools.RequestTool.RequestInt("IsShipped_All",0);
			if (HttpContext.Current.Request["IsReceived"] != null)
				model.IsReceived=LB.Tools.RequestTool.RequestInt("IsReceived",0);
			if (HttpContext.Current.Request["IsReceived_All"] != null)
				model.IsReceived_All=LB.Tools.RequestTool.RequestInt("IsReceived_All",0);
			if (HttpContext.Current.Request["IsCompleted"] != null)
				model.IsCompleted=LB.Tools.RequestTool.RequestInt("IsCompleted",0);
			if (HttpContext.Current.Request["IsInvalid"] != null)
				model.IsInvalid=LB.Tools.RequestTool.RequestInt("IsInvalid",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Verified"] != null)
				model.Time_Verified=LB.Tools.RequestTool.RequestTime("Time_Verified", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Paid"] != null)
				model.Time_Paid=LB.Tools.RequestTool.RequestTime("Time_Paid", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Shipped"] != null)
				model.Time_Shipped=LB.Tools.RequestTool.RequestTime("Time_Shipped", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Received"] != null)
				model.Time_Received=LB.Tools.RequestTool.RequestTime("Time_Received", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Completed"] != null)
				model.Time_Completed=LB.Tools.RequestTool.RequestTime("Time_Completed", System.DateTime.Now);
			if (HttpContext.Current.Request["Remark_Admin"] != null)
				model.Remark_Admin=LB.Tools.RequestTool.RequestString("Remark_Admin");
			if (HttpContext.Current.Request["BillType_Name"] != null)
				model.BillType_Name=LB.Tools.RequestTool.RequestString("BillType_Name");
			if (HttpContext.Current.Request["BillType_id"] != null)
				model.BillType_id=LB.Tools.RequestTool.RequestInt("BillType_id",0);
			if (HttpContext.Current.Request["BillType_TaxRate"] != null)
				model.BillType_TaxRate=LB.Tools.RequestTool.RequestDecimal("BillType_TaxRate",0);
			if (HttpContext.Current.Request["Type_id_OrderType"] != null)
				model.Type_id_OrderType=LB.Tools.RequestTool.RequestInt("Type_id_OrderType",0);
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["IsPrintExpress"] != null)
				model.IsPrintExpress=LB.Tools.RequestTool.RequestInt("IsPrintExpress",0);
			if (HttpContext.Current.Request["Promotion_Type_ids"] != null)
				model.Promotion_Type_ids=LB.Tools.RequestTool.RequestString("Promotion_Type_ids");
			if (HttpContext.Current.Request["Mark"] != null)
				model.Mark=LB.Tools.RequestTool.RequestInt("Mark",0);
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestString("Currency_Code");
			if (HttpContext.Current.Request["Currency_ExchangeRate"] != null)
				model.Currency_ExchangeRate=LB.Tools.RequestTool.RequestDecimal("Currency_ExchangeRate",0);
			if (HttpContext.Current.Request["Currency_Msige"] != null)
				model.Currency_Msige=LB.Tools.RequestTool.RequestString("Currency_Msige");
			if (HttpContext.Current.Request["Flag"] != null)
				model.Flag=LB.Tools.RequestTool.RequestInt("Flag",0);
			if (HttpContext.Current.Request["Money_fromorder"] != null)
				model.Money_fromorder=LB.Tools.RequestTool.RequestDecimal("Money_fromorder",0);
			if (HttpContext.Current.Request["IsCreateCash"] != null)
				model.IsCreateCash=LB.Tools.RequestTool.RequestInt("IsCreateCash",0);
			if (HttpContext.Current.Request["IsCreateNewOrder"] != null)
				model.IsCreateNewOrder=LB.Tools.RequestTool.RequestInt("IsCreateNewOrder",0);
			if (HttpContext.Current.Request["Time_CreateCash"] != null)
				model.Time_CreateCash=LB.Tools.RequestTool.RequestTime("Time_CreateCash", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_CreateNewOrder"] != null)
				model.Time_CreateNewOrder=LB.Tools.RequestTool.RequestTime("Time_CreateNewOrder", System.DateTime.Now);
			if (HttpContext.Current.Request["Site_id"] != null)
				model.Site_id=LB.Tools.RequestTool.RequestInt("Site_id",0);
			if (HttpContext.Current.Request["Point_Buy"] != null)
				model.Point_Buy=LB.Tools.RequestTool.RequestDecimal("Point_Buy",0);
			if (HttpContext.Current.Request["BLNo"] != null)
				model.BLNo=LB.Tools.RequestTool.RequestString("BLNo");
			if (HttpContext.Current.Request["ContainerNo"] != null)
				model.ContainerNo=LB.Tools.RequestTool.RequestString("ContainerNo");
			if (HttpContext.Current.Request["SealNo"] != null)
				model.SealNo=LB.Tools.RequestTool.RequestString("SealNo");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Money_Property"] != null)
				model.Money_Property=LB.Tools.RequestTool.RequestDecimal("Money_Property",0);
			if (HttpContext.Current.Request["weixin_prepay_id"] != null)
				model.weixin_prepay_id=LB.Tools.RequestTool.RequestString("weixin_prepay_id");
			if (HttpContext.Current.Request["IsSupplierCash"] != null)
				model.IsSupplierCash=LB.Tools.RequestTool.RequestInt("IsSupplierCash",0);
			if (HttpContext.Current.Request["Money_OnlinepayFee"] != null)
				model.Money_OnlinepayFee=LB.Tools.RequestTool.RequestDecimal("Money_OnlinepayFee",0);
			if (HttpContext.Current.Request["Site_id_pay"] != null)
				model.Site_id_pay=LB.Tools.RequestTool.RequestInt("Site_id_pay",0);
			if (HttpContext.Current.Request["PickUp_id"] != null)
				model.PickUp_id=LB.Tools.RequestTool.RequestInt("PickUp_id",0);
			if (HttpContext.Current.Request["PickUp_Name"] != null)
				model.PickUp_Name=LB.Tools.RequestTool.RequestString("PickUp_Name");
			if (HttpContext.Current.Request["PickUp_Date"] != null)
				model.PickUp_Date=LB.Tools.RequestTool.RequestTime("PickUp_Date", System.DateTime.Now);
			if (HttpContext.Current.Request["Refund_VAT"] != null)
				model.Refund_VAT=LB.Tools.RequestTool.RequestDecimal("Refund_VAT",0);
			if (HttpContext.Current.Request["Refund_Fee"] != null)
				model.Refund_Fee=LB.Tools.RequestTool.RequestDecimal("Refund_Fee",0);
			if (HttpContext.Current.Request["Language_id"] != null)
				model.Language_id=LB.Tools.RequestTool.RequestInt("Language_id",0);
			if (HttpContext.Current.Request["Supplier_Delivery_id"] != null)
				model.Supplier_Delivery_id=LB.Tools.RequestTool.RequestInt("Supplier_Delivery_id",0);
			if (HttpContext.Current.Request["IsRefund"] != null)
				model.IsRefund=LB.Tools.RequestTool.RequestInt("IsRefund",0);
			if (HttpContext.Current.Request["Time_Refund"] != null)
				model.Time_Refund=LB.Tools.RequestTool.RequestTime("Time_Refund", System.DateTime.Now);
			if (HttpContext.Current.Request["Promotion_Type_Name"] != null)
				model.Promotion_Type_Name=LB.Tools.RequestTool.RequestString("Promotion_Type_Name");
			if (HttpContext.Current.Request["User_NickName"] != null)
				model.User_NickName=LB.Tools.RequestTool.RequestString("User_NickName");
			if (HttpContext.Current.Request["Money_Paid"] != null)
				model.Money_Paid=LB.Tools.RequestTool.RequestDecimal("Money_Paid",0);
			if (HttpContext.Current.Request["IsReserve"] != null)
				model.IsReserve=LB.Tools.RequestTool.RequestInt("IsReserve",0);
			if (HttpContext.Current.Request["Money_fanxianpay"] != null)
				model.Money_fanxianpay=LB.Tools.RequestTool.RequestDecimal("Money_fanxianpay",0);
			if (HttpContext.Current.Request["Money_Tax"] != null)
				model.Money_Tax=LB.Tools.RequestTool.RequestDecimal("Money_Tax",0);
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["DT_Money"] != null)
				model.DT_Money=LB.Tools.RequestTool.RequestDecimal("DT_Money",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["KeyCode"] != null)
				model.KeyCode=LB.Tools.RequestTool.RequestString("KeyCode");
			if (HttpContext.Current.Request["PayNo"] != null)
				model.PayNo=LB.Tools.RequestTool.RequestString("PayNo");
			if (HttpContext.Current.Request["Source"] != null)
				model.Source=LB.Tools.RequestTool.RequestInt("Source",0);
			if (HttpContext.Current.Request["Project_ids"] != null)
				model.Project_ids=LB.Tools.RequestTool.RequestString("Project_ids");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Order SafeBindForm(Lebi_Order model)
		{
			if (HttpContext.Current.Request["Code"] != null)
				model.Code=LB.Tools.RequestTool.RequestSafeString("Code");
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["User_UserName"] != null)
				model.User_UserName=LB.Tools.RequestTool.RequestSafeString("User_UserName");
			if (HttpContext.Current.Request["T_Name"] != null)
				model.T_Name=LB.Tools.RequestTool.RequestSafeString("T_Name");
			if (HttpContext.Current.Request["T_Area_id"] != null)
				model.T_Area_id=LB.Tools.RequestTool.RequestInt("T_Area_id",0);
			if (HttpContext.Current.Request["T_Address"] != null)
				model.T_Address=LB.Tools.RequestTool.RequestSafeString("T_Address");
			if (HttpContext.Current.Request["T_Phone"] != null)
				model.T_Phone=LB.Tools.RequestTool.RequestSafeString("T_Phone");
			if (HttpContext.Current.Request["T_MobilePhone"] != null)
				model.T_MobilePhone=LB.Tools.RequestTool.RequestSafeString("T_MobilePhone");
			if (HttpContext.Current.Request["T_Postalcode"] != null)
				model.T_Postalcode=LB.Tools.RequestTool.RequestSafeString("T_Postalcode");
			if (HttpContext.Current.Request["T_Email"] != null)
				model.T_Email=LB.Tools.RequestTool.RequestSafeString("T_Email");
			if (HttpContext.Current.Request["Remark_User"] != null)
				model.Remark_User=LB.Tools.RequestTool.RequestSafeString("Remark_User");
			if (HttpContext.Current.Request["Pay_id"] != null)
				model.Pay_id=LB.Tools.RequestTool.RequestInt("Pay_id",0);
			if (HttpContext.Current.Request["Pay"] != null)
				model.Pay=LB.Tools.RequestTool.RequestSafeString("Pay");
			if (HttpContext.Current.Request["OnlinePay_id"] != null)
				model.OnlinePay_id=LB.Tools.RequestTool.RequestInt("OnlinePay_id",0);
			if (HttpContext.Current.Request["OnlinePay"] != null)
				model.OnlinePay=LB.Tools.RequestTool.RequestSafeString("OnlinePay");
			if (HttpContext.Current.Request["OnlinePay_Code"] != null)
				model.OnlinePay_Code=LB.Tools.RequestTool.RequestSafeString("OnlinePay_Code");
			if (HttpContext.Current.Request["Money_Order"] != null)
				model.Money_Order=LB.Tools.RequestTool.RequestDecimal("Money_Order",0);
			if (HttpContext.Current.Request["Money_Pay"] != null)
				model.Money_Pay=LB.Tools.RequestTool.RequestDecimal("Money_Pay",0);
			if (HttpContext.Current.Request["Money_Product"] != null)
				model.Money_Product=LB.Tools.RequestTool.RequestDecimal("Money_Product",0);
			if (HttpContext.Current.Request["Money_Transport"] != null)
				model.Money_Transport=LB.Tools.RequestTool.RequestDecimal("Money_Transport",0);
			if (HttpContext.Current.Request["Money_Transport_Cut"] != null)
				model.Money_Transport_Cut=LB.Tools.RequestTool.RequestDecimal("Money_Transport_Cut",0);
			if (HttpContext.Current.Request["Money_Bill"] != null)
				model.Money_Bill=LB.Tools.RequestTool.RequestDecimal("Money_Bill",0);
			if (HttpContext.Current.Request["Money_Market"] != null)
				model.Money_Market=LB.Tools.RequestTool.RequestDecimal("Money_Market",0);
			if (HttpContext.Current.Request["Money_Give"] != null)
				model.Money_Give=LB.Tools.RequestTool.RequestDecimal("Money_Give",0);
			if (HttpContext.Current.Request["Money_Cut"] != null)
				model.Money_Cut=LB.Tools.RequestTool.RequestDecimal("Money_Cut",0);
			if (HttpContext.Current.Request["Money_UserCut"] != null)
				model.Money_UserCut=LB.Tools.RequestTool.RequestDecimal("Money_UserCut",0);
			if (HttpContext.Current.Request["Money_Cost"] != null)
				model.Money_Cost=LB.Tools.RequestTool.RequestDecimal("Money_Cost",0);
			if (HttpContext.Current.Request["Money_UseCard311"] != null)
				model.Money_UseCard311=LB.Tools.RequestTool.RequestDecimal("Money_UseCard311",0);
			if (HttpContext.Current.Request["Money_UseCard312"] != null)
				model.Money_UseCard312=LB.Tools.RequestTool.RequestDecimal("Money_UseCard312",0);
			if (HttpContext.Current.Request["UseCardCode311"] != null)
				model.UseCardCode311=LB.Tools.RequestTool.RequestSafeString("UseCardCode311");
			if (HttpContext.Current.Request["UseCardCode312"] != null)
				model.UseCardCode312=LB.Tools.RequestTool.RequestSafeString("UseCardCode312");
			if (HttpContext.Current.Request["Weight"] != null)
				model.Weight=LB.Tools.RequestTool.RequestDecimal("Weight",0);
			if (HttpContext.Current.Request["Volume"] != null)
				model.Volume=LB.Tools.RequestTool.RequestDecimal("Volume",0);
			if (HttpContext.Current.Request["Point"] != null)
				model.Point=LB.Tools.RequestTool.RequestDecimal("Point",0);
			if (HttpContext.Current.Request["Point_Product"] != null)
				model.Point_Product=LB.Tools.RequestTool.RequestDecimal("Point_Product",0);
			if (HttpContext.Current.Request["Point_Free"] != null)
				model.Point_Free=LB.Tools.RequestTool.RequestDecimal("Point_Free",0);
			if (HttpContext.Current.Request["Transport_Name"] != null)
				model.Transport_Name=LB.Tools.RequestTool.RequestSafeString("Transport_Name");
			if (HttpContext.Current.Request["Transport_id"] != null)
				model.Transport_id=LB.Tools.RequestTool.RequestInt("Transport_id",0);
			if (HttpContext.Current.Request["Transport_Price_id"] != null)
				model.Transport_Price_id=LB.Tools.RequestTool.RequestInt("Transport_Price_id",0);
			if (HttpContext.Current.Request["Transport_Code"] != null)
				model.Transport_Code=LB.Tools.RequestTool.RequestSafeString("Transport_Code");
			if (HttpContext.Current.Request["Transport_Mark"] != null)
				model.Transport_Mark=LB.Tools.RequestTool.RequestSafeString("Transport_Mark");
			if (HttpContext.Current.Request["EditMoney_Order"] != null)
				model.EditMoney_Order=LB.Tools.RequestTool.RequestDecimal("EditMoney_Order",0);
			if (HttpContext.Current.Request["EditMoney_Transport"] != null)
				model.EditMoney_Transport=LB.Tools.RequestTool.RequestDecimal("EditMoney_Transport",0);
			if (HttpContext.Current.Request["EditMoney_Discount"] != null)
				model.EditMoney_Discount=LB.Tools.RequestTool.RequestDecimal("EditMoney_Discount",0);
			if (HttpContext.Current.Request["IsVerified"] != null)
				model.IsVerified=LB.Tools.RequestTool.RequestInt("IsVerified",0);
			if (HttpContext.Current.Request["IsPaid"] != null)
				model.IsPaid=LB.Tools.RequestTool.RequestInt("IsPaid",0);
			if (HttpContext.Current.Request["IsShipped"] != null)
				model.IsShipped=LB.Tools.RequestTool.RequestInt("IsShipped",0);
			if (HttpContext.Current.Request["IsShipped_All"] != null)
				model.IsShipped_All=LB.Tools.RequestTool.RequestInt("IsShipped_All",0);
			if (HttpContext.Current.Request["IsReceived"] != null)
				model.IsReceived=LB.Tools.RequestTool.RequestInt("IsReceived",0);
			if (HttpContext.Current.Request["IsReceived_All"] != null)
				model.IsReceived_All=LB.Tools.RequestTool.RequestInt("IsReceived_All",0);
			if (HttpContext.Current.Request["IsCompleted"] != null)
				model.IsCompleted=LB.Tools.RequestTool.RequestInt("IsCompleted",0);
			if (HttpContext.Current.Request["IsInvalid"] != null)
				model.IsInvalid=LB.Tools.RequestTool.RequestInt("IsInvalid",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Verified"] != null)
				model.Time_Verified=LB.Tools.RequestTool.RequestTime("Time_Verified", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Paid"] != null)
				model.Time_Paid=LB.Tools.RequestTool.RequestTime("Time_Paid", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Shipped"] != null)
				model.Time_Shipped=LB.Tools.RequestTool.RequestTime("Time_Shipped", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Received"] != null)
				model.Time_Received=LB.Tools.RequestTool.RequestTime("Time_Received", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Completed"] != null)
				model.Time_Completed=LB.Tools.RequestTool.RequestTime("Time_Completed", System.DateTime.Now);
			if (HttpContext.Current.Request["Remark_Admin"] != null)
				model.Remark_Admin=LB.Tools.RequestTool.RequestSafeString("Remark_Admin");
			if (HttpContext.Current.Request["BillType_Name"] != null)
				model.BillType_Name=LB.Tools.RequestTool.RequestSafeString("BillType_Name");
			if (HttpContext.Current.Request["BillType_id"] != null)
				model.BillType_id=LB.Tools.RequestTool.RequestInt("BillType_id",0);
			if (HttpContext.Current.Request["BillType_TaxRate"] != null)
				model.BillType_TaxRate=LB.Tools.RequestTool.RequestDecimal("BillType_TaxRate",0);
			if (HttpContext.Current.Request["Type_id_OrderType"] != null)
				model.Type_id_OrderType=LB.Tools.RequestTool.RequestInt("Type_id_OrderType",0);
			if (HttpContext.Current.Request["Order_id"] != null)
				model.Order_id=LB.Tools.RequestTool.RequestInt("Order_id",0);
			if (HttpContext.Current.Request["IsPrintExpress"] != null)
				model.IsPrintExpress=LB.Tools.RequestTool.RequestInt("IsPrintExpress",0);
			if (HttpContext.Current.Request["Promotion_Type_ids"] != null)
				model.Promotion_Type_ids=LB.Tools.RequestTool.RequestSafeString("Promotion_Type_ids");
			if (HttpContext.Current.Request["Mark"] != null)
				model.Mark=LB.Tools.RequestTool.RequestInt("Mark",0);
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestSafeString("Currency_Code");
			if (HttpContext.Current.Request["Currency_ExchangeRate"] != null)
				model.Currency_ExchangeRate=LB.Tools.RequestTool.RequestDecimal("Currency_ExchangeRate",0);
			if (HttpContext.Current.Request["Currency_Msige"] != null)
				model.Currency_Msige=LB.Tools.RequestTool.RequestSafeString("Currency_Msige");
			if (HttpContext.Current.Request["Flag"] != null)
				model.Flag=LB.Tools.RequestTool.RequestInt("Flag",0);
			if (HttpContext.Current.Request["Money_fromorder"] != null)
				model.Money_fromorder=LB.Tools.RequestTool.RequestDecimal("Money_fromorder",0);
			if (HttpContext.Current.Request["IsCreateCash"] != null)
				model.IsCreateCash=LB.Tools.RequestTool.RequestInt("IsCreateCash",0);
			if (HttpContext.Current.Request["IsCreateNewOrder"] != null)
				model.IsCreateNewOrder=LB.Tools.RequestTool.RequestInt("IsCreateNewOrder",0);
			if (HttpContext.Current.Request["Time_CreateCash"] != null)
				model.Time_CreateCash=LB.Tools.RequestTool.RequestTime("Time_CreateCash", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_CreateNewOrder"] != null)
				model.Time_CreateNewOrder=LB.Tools.RequestTool.RequestTime("Time_CreateNewOrder", System.DateTime.Now);
			if (HttpContext.Current.Request["Site_id"] != null)
				model.Site_id=LB.Tools.RequestTool.RequestInt("Site_id",0);
			if (HttpContext.Current.Request["Point_Buy"] != null)
				model.Point_Buy=LB.Tools.RequestTool.RequestDecimal("Point_Buy",0);
			if (HttpContext.Current.Request["BLNo"] != null)
				model.BLNo=LB.Tools.RequestTool.RequestSafeString("BLNo");
			if (HttpContext.Current.Request["ContainerNo"] != null)
				model.ContainerNo=LB.Tools.RequestTool.RequestSafeString("ContainerNo");
			if (HttpContext.Current.Request["SealNo"] != null)
				model.SealNo=LB.Tools.RequestTool.RequestSafeString("SealNo");
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Money_Property"] != null)
				model.Money_Property=LB.Tools.RequestTool.RequestDecimal("Money_Property",0);
			if (HttpContext.Current.Request["weixin_prepay_id"] != null)
				model.weixin_prepay_id=LB.Tools.RequestTool.RequestSafeString("weixin_prepay_id");
			if (HttpContext.Current.Request["IsSupplierCash"] != null)
				model.IsSupplierCash=LB.Tools.RequestTool.RequestInt("IsSupplierCash",0);
			if (HttpContext.Current.Request["Money_OnlinepayFee"] != null)
				model.Money_OnlinepayFee=LB.Tools.RequestTool.RequestDecimal("Money_OnlinepayFee",0);
			if (HttpContext.Current.Request["Site_id_pay"] != null)
				model.Site_id_pay=LB.Tools.RequestTool.RequestInt("Site_id_pay",0);
			if (HttpContext.Current.Request["PickUp_id"] != null)
				model.PickUp_id=LB.Tools.RequestTool.RequestInt("PickUp_id",0);
			if (HttpContext.Current.Request["PickUp_Name"] != null)
				model.PickUp_Name=LB.Tools.RequestTool.RequestSafeString("PickUp_Name");
			if (HttpContext.Current.Request["PickUp_Date"] != null)
				model.PickUp_Date=LB.Tools.RequestTool.RequestTime("PickUp_Date", System.DateTime.Now);
			if (HttpContext.Current.Request["Refund_VAT"] != null)
				model.Refund_VAT=LB.Tools.RequestTool.RequestDecimal("Refund_VAT",0);
			if (HttpContext.Current.Request["Refund_Fee"] != null)
				model.Refund_Fee=LB.Tools.RequestTool.RequestDecimal("Refund_Fee",0);
			if (HttpContext.Current.Request["Language_id"] != null)
				model.Language_id=LB.Tools.RequestTool.RequestInt("Language_id",0);
			if (HttpContext.Current.Request["Supplier_Delivery_id"] != null)
				model.Supplier_Delivery_id=LB.Tools.RequestTool.RequestInt("Supplier_Delivery_id",0);
			if (HttpContext.Current.Request["IsRefund"] != null)
				model.IsRefund=LB.Tools.RequestTool.RequestInt("IsRefund",0);
			if (HttpContext.Current.Request["Time_Refund"] != null)
				model.Time_Refund=LB.Tools.RequestTool.RequestTime("Time_Refund", System.DateTime.Now);
			if (HttpContext.Current.Request["Promotion_Type_Name"] != null)
				model.Promotion_Type_Name=LB.Tools.RequestTool.RequestSafeString("Promotion_Type_Name");
			if (HttpContext.Current.Request["User_NickName"] != null)
				model.User_NickName=LB.Tools.RequestTool.RequestSafeString("User_NickName");
			if (HttpContext.Current.Request["Money_Paid"] != null)
				model.Money_Paid=LB.Tools.RequestTool.RequestDecimal("Money_Paid",0);
			if (HttpContext.Current.Request["IsReserve"] != null)
				model.IsReserve=LB.Tools.RequestTool.RequestInt("IsReserve",0);
			if (HttpContext.Current.Request["Money_fanxianpay"] != null)
				model.Money_fanxianpay=LB.Tools.RequestTool.RequestDecimal("Money_fanxianpay",0);
			if (HttpContext.Current.Request["Money_Tax"] != null)
				model.Money_Tax=LB.Tools.RequestTool.RequestDecimal("Money_Tax",0);
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["DT_Money"] != null)
				model.DT_Money=LB.Tools.RequestTool.RequestDecimal("DT_Money",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["KeyCode"] != null)
				model.KeyCode=LB.Tools.RequestTool.RequestSafeString("KeyCode");
			if (HttpContext.Current.Request["PayNo"] != null)
				model.PayNo=LB.Tools.RequestTool.RequestSafeString("PayNo");
			if (HttpContext.Current.Request["Source"] != null)
				model.Source=LB.Tools.RequestTool.RequestInt("Source",0);
			if (HttpContext.Current.Request["Project_ids"] != null)
				model.Project_ids=LB.Tools.RequestTool.RequestSafeString("Project_ids");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Order ReaderBind(IDataReader dataReader, string strFieldShow)
		{
			Lebi_Order model=new Lebi_Order();
			object ojb; 
			List<string> upcols = new List<string>();
			string[] arrcols = strFieldShow.ToLower().Split(',');
			bool isall = false;
			if (strFieldShow == "*")
			    isall = true;
			else
			{
			   foreach (string c in arrcols)
			   {
			       upcols.Add(c);
			   }
			}
			if(isall || upcols.Contains("id"))
			{
				ojb = dataReader["id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("code"))
			{
				ojb = dataReader["Code"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Code=dataReader["Code"].ToString();
				}
			}
			if(isall || upcols.Contains("user_id"))
			{
				ojb = dataReader["User_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.User_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("user_username"))
			{
				ojb = dataReader["User_UserName"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.User_UserName=dataReader["User_UserName"].ToString();
				}
			}
			if(isall || upcols.Contains("t_name"))
			{
				ojb = dataReader["T_Name"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_Name=dataReader["T_Name"].ToString();
				}
			}
			if(isall || upcols.Contains("t_area_id"))
			{
				ojb = dataReader["T_Area_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_Area_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("t_address"))
			{
				ojb = dataReader["T_Address"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_Address=dataReader["T_Address"].ToString();
				}
			}
			if(isall || upcols.Contains("t_phone"))
			{
				ojb = dataReader["T_Phone"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_Phone=dataReader["T_Phone"].ToString();
				}
			}
			if(isall || upcols.Contains("t_mobilephone"))
			{
				ojb = dataReader["T_MobilePhone"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_MobilePhone=dataReader["T_MobilePhone"].ToString();
				}
			}
			if(isall || upcols.Contains("t_postalcode"))
			{
				ojb = dataReader["T_Postalcode"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_Postalcode=dataReader["T_Postalcode"].ToString();
				}
			}
			if(isall || upcols.Contains("t_email"))
			{
				ojb = dataReader["T_Email"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.T_Email=dataReader["T_Email"].ToString();
				}
			}
			if(isall || upcols.Contains("remark_user"))
			{
				ojb = dataReader["Remark_User"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Remark_User=dataReader["Remark_User"].ToString();
				}
			}
			if(isall || upcols.Contains("pay_id"))
			{
				ojb = dataReader["Pay_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Pay_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("pay"))
			{
				ojb = dataReader["Pay"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Pay=dataReader["Pay"].ToString();
				}
			}
			if(isall || upcols.Contains("onlinepay_id"))
			{
				ojb = dataReader["OnlinePay_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.OnlinePay_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("onlinepay"))
			{
				ojb = dataReader["OnlinePay"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.OnlinePay=dataReader["OnlinePay"].ToString();
				}
			}
			if(isall || upcols.Contains("onlinepay_code"))
			{
				ojb = dataReader["OnlinePay_Code"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.OnlinePay_Code=dataReader["OnlinePay_Code"].ToString();
				}
			}
			if(isall || upcols.Contains("money_order"))
			{
				ojb = dataReader["Money_Order"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Order=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_pay"))
			{
				ojb = dataReader["Money_Pay"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Pay=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_product"))
			{
				ojb = dataReader["Money_Product"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Product=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_transport"))
			{
				ojb = dataReader["Money_Transport"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Transport=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_transport_cut"))
			{
				ojb = dataReader["Money_Transport_Cut"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Transport_Cut=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_bill"))
			{
				ojb = dataReader["Money_Bill"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Bill=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_market"))
			{
				ojb = dataReader["Money_Market"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Market=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_give"))
			{
				ojb = dataReader["Money_Give"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Give=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_cut"))
			{
				ojb = dataReader["Money_Cut"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Cut=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_usercut"))
			{
				ojb = dataReader["Money_UserCut"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_UserCut=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_cost"))
			{
				ojb = dataReader["Money_Cost"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Cost=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_usecard311"))
			{
				ojb = dataReader["Money_UseCard311"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_UseCard311=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_usecard312"))
			{
				ojb = dataReader["Money_UseCard312"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_UseCard312=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("usecardcode311"))
			{
				ojb = dataReader["UseCardCode311"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.UseCardCode311=dataReader["UseCardCode311"].ToString();
				}
			}
			if(isall || upcols.Contains("usecardcode312"))
			{
				ojb = dataReader["UseCardCode312"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.UseCardCode312=dataReader["UseCardCode312"].ToString();
				}
			}
			if(isall || upcols.Contains("weight"))
			{
				ojb = dataReader["Weight"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Weight=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("volume"))
			{
				ojb = dataReader["Volume"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Volume=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("point"))
			{
				ojb = dataReader["Point"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Point=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("point_product"))
			{
				ojb = dataReader["Point_Product"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Point_Product=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("point_free"))
			{
				ojb = dataReader["Point_Free"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Point_Free=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("transport_name"))
			{
				ojb = dataReader["Transport_Name"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Transport_Name=dataReader["Transport_Name"].ToString();
				}
			}
			if(isall || upcols.Contains("transport_id"))
			{
				ojb = dataReader["Transport_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Transport_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("transport_price_id"))
			{
				ojb = dataReader["Transport_Price_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Transport_Price_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("transport_code"))
			{
				ojb = dataReader["Transport_Code"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Transport_Code=dataReader["Transport_Code"].ToString();
				}
			}
			if(isall || upcols.Contains("transport_mark"))
			{
				ojb = dataReader["Transport_Mark"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Transport_Mark=dataReader["Transport_Mark"].ToString();
				}
			}
			if(isall || upcols.Contains("editmoney_order"))
			{
				ojb = dataReader["EditMoney_Order"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.EditMoney_Order=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("editmoney_transport"))
			{
				ojb = dataReader["EditMoney_Transport"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.EditMoney_Transport=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("editmoney_discount"))
			{
				ojb = dataReader["EditMoney_Discount"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.EditMoney_Discount=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("isverified"))
			{
				ojb = dataReader["IsVerified"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsVerified= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("ispaid"))
			{
				ojb = dataReader["IsPaid"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsPaid= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isshipped"))
			{
				ojb = dataReader["IsShipped"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsShipped= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isshipped_all"))
			{
				ojb = dataReader["IsShipped_All"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsShipped_All= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isreceived"))
			{
				ojb = dataReader["IsReceived"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsReceived= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isreceived_all"))
			{
				ojb = dataReader["IsReceived_All"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsReceived_All= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("iscompleted"))
			{
				ojb = dataReader["IsCompleted"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsCompleted= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isinvalid"))
			{
				ojb = dataReader["IsInvalid"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsInvalid= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("time_add"))
			{
				ojb = dataReader["Time_Add"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Add=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_verified"))
			{
				ojb = dataReader["Time_Verified"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Verified=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_paid"))
			{
				ojb = dataReader["Time_Paid"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Paid=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_shipped"))
			{
				ojb = dataReader["Time_Shipped"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Shipped=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_received"))
			{
				ojb = dataReader["Time_Received"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Received=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_completed"))
			{
				ojb = dataReader["Time_Completed"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Completed=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("remark_admin"))
			{
				ojb = dataReader["Remark_Admin"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Remark_Admin=dataReader["Remark_Admin"].ToString();
				}
			}
			if(isall || upcols.Contains("billtype_name"))
			{
				ojb = dataReader["BillType_Name"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.BillType_Name=dataReader["BillType_Name"].ToString();
				}
			}
			if(isall || upcols.Contains("billtype_id"))
			{
				ojb = dataReader["BillType_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.BillType_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("billtype_taxrate"))
			{
				ojb = dataReader["BillType_TaxRate"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.BillType_TaxRate=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("type_id_ordertype"))
			{
				ojb = dataReader["Type_id_OrderType"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Type_id_OrderType= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("order_id"))
			{
				ojb = dataReader["Order_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Order_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isprintexpress"))
			{
				ojb = dataReader["IsPrintExpress"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsPrintExpress= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("promotion_type_ids"))
			{
				ojb = dataReader["Promotion_Type_ids"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Promotion_Type_ids=dataReader["Promotion_Type_ids"].ToString();
				}
			}
			if(isall || upcols.Contains("mark"))
			{
				ojb = dataReader["Mark"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Mark= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("currency_id"))
			{
				ojb = dataReader["Currency_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Currency_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("currency_code"))
			{
				ojb = dataReader["Currency_Code"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Currency_Code=dataReader["Currency_Code"].ToString();
				}
			}
			if(isall || upcols.Contains("currency_exchangerate"))
			{
				ojb = dataReader["Currency_ExchangeRate"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Currency_ExchangeRate=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("currency_msige"))
			{
				ojb = dataReader["Currency_Msige"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Currency_Msige=dataReader["Currency_Msige"].ToString();
				}
			}
			if(isall || upcols.Contains("flag"))
			{
				ojb = dataReader["Flag"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Flag= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("money_fromorder"))
			{
				ojb = dataReader["Money_fromorder"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_fromorder=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("iscreatecash"))
			{
				ojb = dataReader["IsCreateCash"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsCreateCash= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("iscreateneworder"))
			{
				ojb = dataReader["IsCreateNewOrder"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsCreateNewOrder= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("time_createcash"))
			{
				ojb = dataReader["Time_CreateCash"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_CreateCash=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_createneworder"))
			{
				ojb = dataReader["Time_CreateNewOrder"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_CreateNewOrder=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("site_id"))
			{
				ojb = dataReader["Site_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Site_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("point_buy"))
			{
				ojb = dataReader["Point_Buy"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Point_Buy=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("blno"))
			{
				ojb = dataReader["BLNo"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.BLNo=dataReader["BLNo"].ToString();
				}
			}
			if(isall || upcols.Contains("containerno"))
			{
				ojb = dataReader["ContainerNo"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.ContainerNo=dataReader["ContainerNo"].ToString();
				}
			}
			if(isall || upcols.Contains("sealno"))
			{
				ojb = dataReader["SealNo"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.SealNo=dataReader["SealNo"].ToString();
				}
			}
			if(isall || upcols.Contains("supplier_id"))
			{
				ojb = dataReader["Supplier_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Supplier_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("money_property"))
			{
				ojb = dataReader["Money_Property"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Property=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("weixin_prepay_id"))
			{
				ojb = dataReader["weixin_prepay_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.weixin_prepay_id=dataReader["weixin_prepay_id"].ToString();
				}
			}
			if(isall || upcols.Contains("issuppliercash"))
			{
				ojb = dataReader["IsSupplierCash"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsSupplierCash= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("money_onlinepayfee"))
			{
				ojb = dataReader["Money_OnlinepayFee"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_OnlinepayFee=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("site_id_pay"))
			{
				ojb = dataReader["Site_id_pay"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Site_id_pay= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("pickup_id"))
			{
				ojb = dataReader["PickUp_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.PickUp_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("pickup_name"))
			{
				ojb = dataReader["PickUp_Name"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.PickUp_Name=dataReader["PickUp_Name"].ToString();
				}
			}
			if(isall || upcols.Contains("pickup_date"))
			{
				ojb = dataReader["PickUp_Date"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.PickUp_Date=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("refund_vat"))
			{
				ojb = dataReader["Refund_VAT"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Refund_VAT=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("refund_fee"))
			{
				ojb = dataReader["Refund_Fee"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Refund_Fee=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("language_id"))
			{
				ojb = dataReader["Language_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Language_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("supplier_delivery_id"))
			{
				ojb = dataReader["Supplier_Delivery_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Supplier_Delivery_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("isrefund"))
			{
				ojb = dataReader["IsRefund"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsRefund= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("time_refund"))
			{
				ojb = dataReader["Time_Refund"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Refund=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("promotion_type_name"))
			{
				ojb = dataReader["Promotion_Type_Name"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Promotion_Type_Name=dataReader["Promotion_Type_Name"].ToString();
				}
			}
			if(isall || upcols.Contains("user_nickname"))
			{
				ojb = dataReader["User_NickName"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.User_NickName=dataReader["User_NickName"].ToString();
				}
			}
			if(isall || upcols.Contains("money_paid"))
			{
				ojb = dataReader["Money_Paid"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Paid=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("isreserve"))
			{
				ojb = dataReader["IsReserve"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsReserve= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("money_fanxianpay"))
			{
				ojb = dataReader["Money_fanxianpay"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_fanxianpay=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("money_tax"))
			{
				ojb = dataReader["Money_Tax"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Money_Tax=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("dt_id"))
			{
				ojb = dataReader["DT_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.DT_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("dt_money"))
			{
				ojb = dataReader["DT_Money"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.DT_Money=(decimal)ojb;
				}
			}
			if(isall || upcols.Contains("isdel"))
			{
				ojb = dataReader["IsDel"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IsDel= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("keycode"))
			{
				ojb = dataReader["KeyCode"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.KeyCode=dataReader["KeyCode"].ToString();
				}
			}
			if(isall || upcols.Contains("payno"))
			{
				ojb = dataReader["PayNo"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.PayNo=dataReader["PayNo"].ToString();
				}
			}
			if(isall || upcols.Contains("source"))
			{
				ojb = dataReader["Source"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Source= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("project_ids"))
			{
				ojb = dataReader["Project_ids"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Project_ids=dataReader["Project_ids"].ToString();
				}
			}
			return model;
		}

		#endregion  成员方法
	}
}

