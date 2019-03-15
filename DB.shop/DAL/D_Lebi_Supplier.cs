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
	/// 数据访问类D_Lebi_Supplier。
	/// </summary>
	public partial class D_Lebi_Supplier
	{
		static D_Lebi_Supplier _Instance;
		public static D_Lebi_Supplier Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Supplier("Lebi_Supplier");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Supplier";
		public D_Lebi_Supplier(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Supplier", 0 , cachestr,seconds);
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
		public int Add(Lebi_Supplier model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Address")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("BillingDays")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ClassName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Company")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Days_checkuserlow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Domain")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Fax")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("FreezeRemark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("head")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCash")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSpread")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierTransport")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Level_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Logo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("longbar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("MobilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Margin_pay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Msn")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Password")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PointToMoney")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Postalcode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ProductTop")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ServicePanel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sex")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("shortbar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Status")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SubName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Group_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Skin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SupplierNumber")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Begin")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Reg")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_SupplierStatus")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLow")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserTop")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Service")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Margin")+")");
			strSql.Append(" values (");
			strSql.Append("@Address,@Area_id,@BillingDays,@ClassName,@Company,@Count_Login,@Days_checkuserlow,@Description,@Domain,@Email,@Fax,@FreezeRemark,@head,@IP_Last,@IP_This,@IsCash,@IsSpread,@IsSupplierTransport,@Language,@Level_id,@Logo,@longbar,@MobilePhone,@Money_Margin_pay,@Msn,@Name,@Password,@Phone,@PointToMoney,@Postalcode,@ProductTop,@QQ,@RealName,@Remark,@SEO_Description,@SEO_Keywords,@SEO_Title,@ServicePanel,@Sex,@shortbar,@Status,@SubName,@Supplier_Group_id,@Supplier_Skin_id,@SupplierNumber,@Time_Begin,@Time_End,@Time_Last,@Time_Reg,@Time_This,@Type_id_SupplierStatus,@User_id,@UserLow,@UserName,@UserTop,@Money,@Money_Service,@Money_Margin);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Address", model.Address),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@BillingDays", model.BillingDays),
					new SqlParameter("@ClassName", model.ClassName),
					new SqlParameter("@Company", model.Company),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Days_checkuserlow", model.Days_checkuserlow),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Domain", model.Domain),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Fax", model.Fax),
					new SqlParameter("@FreezeRemark", model.FreezeRemark),
					new SqlParameter("@head", model.head),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@IsCash", model.IsCash),
					new SqlParameter("@IsSpread", model.IsSpread),
					new SqlParameter("@IsSupplierTransport", model.IsSupplierTransport),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Level_id", model.Level_id),
					new SqlParameter("@Logo", model.Logo),
					new SqlParameter("@longbar", model.longbar),
					new SqlParameter("@MobilePhone", model.MobilePhone),
					new SqlParameter("@Money_Margin_pay", model.Money_Margin_pay),
					new SqlParameter("@Msn", model.Msn),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@PointToMoney", model.PointToMoney),
					new SqlParameter("@Postalcode", model.Postalcode),
					new SqlParameter("@ProductTop", model.ProductTop),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@ServicePanel", model.ServicePanel),
					new SqlParameter("@Sex", model.Sex),
					new SqlParameter("@shortbar", model.shortbar),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@SubName", model.SubName),
					new SqlParameter("@Supplier_Group_id", model.Supplier_Group_id),
					new SqlParameter("@Supplier_Skin_id", model.Supplier_Skin_id),
					new SqlParameter("@SupplierNumber", model.SupplierNumber),
					new SqlParameter("@Time_Begin", model.Time_Begin),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Time_Reg", model.Time_Reg),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@Type_id_SupplierStatus", model.Type_id_SupplierStatus),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@UserLow", model.UserLow),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@UserTop", model.UserTop),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Service", model.Money_Service),
					new SqlParameter("@Money_Margin", model.Money_Margin)};

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
		public void Update(Lebi_Supplier model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Address,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Address")+"= @Address");
			if((","+model.UpdateCols+",").IndexOf(",Area_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+"= @Area_id");
			if((","+model.UpdateCols+",").IndexOf(",BillingDays,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BillingDays")+"= @BillingDays");
			if((","+model.UpdateCols+",").IndexOf(",ClassName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ClassName")+"= @ClassName");
			if((","+model.UpdateCols+",").IndexOf(",Company,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Company")+"= @Company");
			if((","+model.UpdateCols+",").IndexOf(",Count_Login,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+"= @Count_Login");
			if((","+model.UpdateCols+",").IndexOf(",Days_checkuserlow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Days_checkuserlow")+"= @Days_checkuserlow");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",Domain,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Domain")+"= @Domain");
			if((","+model.UpdateCols+",").IndexOf(",Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if((","+model.UpdateCols+",").IndexOf(",Fax,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Fax")+"= @Fax");
			if((","+model.UpdateCols+",").IndexOf(",FreezeRemark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("FreezeRemark")+"= @FreezeRemark");
			if((","+model.UpdateCols+",").IndexOf(",head,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("head")+"= @head");
			if((","+model.UpdateCols+",").IndexOf(",IP_Last,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+"= @IP_Last");
			if((","+model.UpdateCols+",").IndexOf(",IP_This,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+"= @IP_This");
			if((","+model.UpdateCols+",").IndexOf(",IsCash,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCash")+"= @IsCash");
			if((","+model.UpdateCols+",").IndexOf(",IsSpread,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSpread")+"= @IsSpread");
			if((","+model.UpdateCols+",").IndexOf(",IsSupplierTransport,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSupplierTransport")+"= @IsSupplierTransport");
			if((","+model.UpdateCols+",").IndexOf(",Language,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+"= @Language");
			if((","+model.UpdateCols+",").IndexOf(",Level_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Level_id")+"= @Level_id");
			if((","+model.UpdateCols+",").IndexOf(",Logo,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Logo")+"= @Logo");
			if((","+model.UpdateCols+",").IndexOf(",longbar,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("longbar")+"= @longbar");
			if((","+model.UpdateCols+",").IndexOf(",MobilePhone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("MobilePhone")+"= @MobilePhone");
			if((","+model.UpdateCols+",").IndexOf(",Money_Margin_pay,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Margin_pay")+"= @Money_Margin_pay");
			if((","+model.UpdateCols+",").IndexOf(",Msn,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Msn")+"= @Msn");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",Password,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Password")+"= @Password");
			if((","+model.UpdateCols+",").IndexOf(",Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+"= @Phone");
			if((","+model.UpdateCols+",").IndexOf(",PointToMoney,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PointToMoney")+"= @PointToMoney");
			if((","+model.UpdateCols+",").IndexOf(",Postalcode,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Postalcode")+"= @Postalcode");
			if((","+model.UpdateCols+",").IndexOf(",ProductTop,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ProductTop")+"= @ProductTop");
			if((","+model.UpdateCols+",").IndexOf(",QQ,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+"= @QQ");
			if((","+model.UpdateCols+",").IndexOf(",RealName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+"= @RealName");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Description")+"= @SEO_Description");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Keywords,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Keywords")+"= @SEO_Keywords");
			if((","+model.UpdateCols+",").IndexOf(",SEO_Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SEO_Title")+"= @SEO_Title");
			if((","+model.UpdateCols+",").IndexOf(",ServicePanel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ServicePanel")+"= @ServicePanel");
			if((","+model.UpdateCols+",").IndexOf(",Sex,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sex")+"= @Sex");
			if((","+model.UpdateCols+",").IndexOf(",shortbar,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("shortbar")+"= @shortbar");
			if((","+model.UpdateCols+",").IndexOf(",Status,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Status")+"= @Status");
			if((","+model.UpdateCols+",").IndexOf(",SubName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SubName")+"= @SubName");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_Group_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Group_id")+"= @Supplier_Group_id");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_Skin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_Skin_id")+"= @Supplier_Skin_id");
			if((","+model.UpdateCols+",").IndexOf(",SupplierNumber,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SupplierNumber")+"= @SupplierNumber");
			if((","+model.UpdateCols+",").IndexOf(",Time_Begin,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Begin")+"= @Time_Begin");
			if((","+model.UpdateCols+",").IndexOf(",Time_End,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+"= @Time_End");
			if((","+model.UpdateCols+",").IndexOf(",Time_Last,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+"= @Time_Last");
			if((","+model.UpdateCols+",").IndexOf(",Time_Reg,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Reg")+"= @Time_Reg");
			if((","+model.UpdateCols+",").IndexOf(",Time_This,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+"= @Time_This");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_SupplierStatus,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_SupplierStatus")+"= @Type_id_SupplierStatus");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",UserLow,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLow")+"= @UserLow");
			if((","+model.UpdateCols+",").IndexOf(",UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+"= @UserName");
			if((","+model.UpdateCols+",").IndexOf(",UserTop,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserTop")+"= @UserTop");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Money_Service,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Service")+"= @Money_Service");
			if((","+model.UpdateCols+",").IndexOf(",Money_Margin,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Margin")+"= @Money_Margin");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Address", model.Address),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@BillingDays", model.BillingDays),
					new SqlParameter("@ClassName", model.ClassName),
					new SqlParameter("@Company", model.Company),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Days_checkuserlow", model.Days_checkuserlow),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Domain", model.Domain),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Fax", model.Fax),
					new SqlParameter("@FreezeRemark", model.FreezeRemark),
					new SqlParameter("@head", model.head),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@IsCash", model.IsCash),
					new SqlParameter("@IsSpread", model.IsSpread),
					new SqlParameter("@IsSupplierTransport", model.IsSupplierTransport),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Level_id", model.Level_id),
					new SqlParameter("@Logo", model.Logo),
					new SqlParameter("@longbar", model.longbar),
					new SqlParameter("@MobilePhone", model.MobilePhone),
					new SqlParameter("@Money_Margin_pay", model.Money_Margin_pay),
					new SqlParameter("@Msn", model.Msn),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@PointToMoney", model.PointToMoney),
					new SqlParameter("@Postalcode", model.Postalcode),
					new SqlParameter("@ProductTop", model.ProductTop),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@SEO_Description", model.SEO_Description),
					new SqlParameter("@SEO_Keywords", model.SEO_Keywords),
					new SqlParameter("@SEO_Title", model.SEO_Title),
					new SqlParameter("@ServicePanel", model.ServicePanel),
					new SqlParameter("@Sex", model.Sex),
					new SqlParameter("@shortbar", model.shortbar),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@SubName", model.SubName),
					new SqlParameter("@Supplier_Group_id", model.Supplier_Group_id),
					new SqlParameter("@Supplier_Skin_id", model.Supplier_Skin_id),
					new SqlParameter("@SupplierNumber", model.SupplierNumber),
					new SqlParameter("@Time_Begin", model.Time_Begin),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Time_Reg", model.Time_Reg),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@Type_id_SupplierStatus", model.Type_id_SupplierStatus),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@UserLow", model.UserLow),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@UserTop", model.UserTop),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Service", model.Money_Service),
					new SqlParameter("@Money_Margin", model.Money_Margin)};
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
		public Lebi_Supplier GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Supplier;
		   }
		   Lebi_Supplier model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Supplier",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Supplier GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Supplier;
		   }
		   Lebi_Supplier model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Supplier",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Supplier GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Supplier;
		   }
		   Lebi_Supplier model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Supplier",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Supplier> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Supplier>;
		   }
		   List<Lebi_Supplier> list = new List<Lebi_Supplier>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Supplier> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Supplier>;
		   }
		   List<Lebi_Supplier> list = new List<Lebi_Supplier>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Supplier> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Supplier>;
		   }
		   List<Lebi_Supplier> list = new List<Lebi_Supplier>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Supplier> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Supplier>;
		   }
		   List<Lebi_Supplier> list = new List<Lebi_Supplier>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Supplier", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Supplier BindForm(Lebi_Supplier model)
		{
			if (HttpContext.Current.Request["Address"] != null)
				model.Address=LB.Tools.RequestTool.RequestString("Address");
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["BillingDays"] != null)
				model.BillingDays=LB.Tools.RequestTool.RequestInt("BillingDays",0);
			if (HttpContext.Current.Request["ClassName"] != null)
				model.ClassName=LB.Tools.RequestTool.RequestString("ClassName");
			if (HttpContext.Current.Request["Company"] != null)
				model.Company=LB.Tools.RequestTool.RequestString("Company");
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Days_checkuserlow"] != null)
				model.Days_checkuserlow=LB.Tools.RequestTool.RequestInt("Days_checkuserlow",0);
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["Domain"] != null)
				model.Domain=LB.Tools.RequestTool.RequestString("Domain");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["Fax"] != null)
				model.Fax=LB.Tools.RequestTool.RequestString("Fax");
			if (HttpContext.Current.Request["FreezeRemark"] != null)
				model.FreezeRemark=LB.Tools.RequestTool.RequestString("FreezeRemark");
			if (HttpContext.Current.Request["head"] != null)
				model.head=LB.Tools.RequestTool.RequestString("head");
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestString("IP_This");
			if (HttpContext.Current.Request["IsCash"] != null)
				model.IsCash=LB.Tools.RequestTool.RequestInt("IsCash",0);
			if (HttpContext.Current.Request["IsSpread"] != null)
				model.IsSpread=LB.Tools.RequestTool.RequestInt("IsSpread",0);
			if (HttpContext.Current.Request["IsSupplierTransport"] != null)
				model.IsSupplierTransport=LB.Tools.RequestTool.RequestInt("IsSupplierTransport",0);
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestString("Language");
			if (HttpContext.Current.Request["Level_id"] != null)
				model.Level_id=LB.Tools.RequestTool.RequestInt("Level_id",0);
			if (HttpContext.Current.Request["Logo"] != null)
				model.Logo=LB.Tools.RequestTool.RequestString("Logo");
			if (HttpContext.Current.Request["longbar"] != null)
				model.longbar=LB.Tools.RequestTool.RequestString("longbar");
			if (HttpContext.Current.Request["MobilePhone"] != null)
				model.MobilePhone=LB.Tools.RequestTool.RequestString("MobilePhone");
			if (HttpContext.Current.Request["Money_Margin_pay"] != null)
				model.Money_Margin_pay=LB.Tools.RequestTool.RequestDecimal("Money_Margin_pay",0);
			if (HttpContext.Current.Request["Msn"] != null)
				model.Msn=LB.Tools.RequestTool.RequestString("Msn");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["Password"] != null)
				model.Password=LB.Tools.RequestTool.RequestString("Password");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestString("Phone");
			if (HttpContext.Current.Request["PointToMoney"] != null)
				model.PointToMoney=LB.Tools.RequestTool.RequestDecimal("PointToMoney",0);
			if (HttpContext.Current.Request["Postalcode"] != null)
				model.Postalcode=LB.Tools.RequestTool.RequestString("Postalcode");
			if (HttpContext.Current.Request["ProductTop"] != null)
				model.ProductTop=LB.Tools.RequestTool.RequestInt("ProductTop",0);
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestString("QQ");
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestString("RealName");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestString("SEO_Title");
			if (HttpContext.Current.Request["ServicePanel"] != null)
				model.ServicePanel=LB.Tools.RequestTool.RequestString("ServicePanel");
			if (HttpContext.Current.Request["Sex"] != null)
				model.Sex=LB.Tools.RequestTool.RequestString("Sex");
			if (HttpContext.Current.Request["shortbar"] != null)
				model.shortbar=LB.Tools.RequestTool.RequestString("shortbar");
			if (HttpContext.Current.Request["Status"] != null)
				model.Status=LB.Tools.RequestTool.RequestInt("Status",0);
			if (HttpContext.Current.Request["SubName"] != null)
				model.SubName=LB.Tools.RequestTool.RequestString("SubName");
			if (HttpContext.Current.Request["Supplier_Group_id"] != null)
				model.Supplier_Group_id=LB.Tools.RequestTool.RequestInt("Supplier_Group_id",0);
			if (HttpContext.Current.Request["Supplier_Skin_id"] != null)
				model.Supplier_Skin_id=LB.Tools.RequestTool.RequestInt("Supplier_Skin_id",0);
			if (HttpContext.Current.Request["SupplierNumber"] != null)
				model.SupplierNumber=LB.Tools.RequestTool.RequestString("SupplierNumber");
			if (HttpContext.Current.Request["Time_Begin"] != null)
				model.Time_Begin=LB.Tools.RequestTool.RequestTime("Time_Begin", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Reg"] != null)
				model.Time_Reg=LB.Tools.RequestTool.RequestTime("Time_Reg", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_SupplierStatus"] != null)
				model.Type_id_SupplierStatus=LB.Tools.RequestTool.RequestInt("Type_id_SupplierStatus",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["UserLow"] != null)
				model.UserLow=LB.Tools.RequestTool.RequestInt("UserLow",0);
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestString("UserName");
			if (HttpContext.Current.Request["UserTop"] != null)
				model.UserTop=LB.Tools.RequestTool.RequestInt("UserTop",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Service"] != null)
				model.Money_Service=LB.Tools.RequestTool.RequestDecimal("Money_Service",0);
			if (HttpContext.Current.Request["Money_Margin"] != null)
				model.Money_Margin=LB.Tools.RequestTool.RequestDecimal("Money_Margin",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Supplier SafeBindForm(Lebi_Supplier model)
		{
			if (HttpContext.Current.Request["Address"] != null)
				model.Address=LB.Tools.RequestTool.RequestSafeString("Address");
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["BillingDays"] != null)
				model.BillingDays=LB.Tools.RequestTool.RequestInt("BillingDays",0);
			if (HttpContext.Current.Request["ClassName"] != null)
				model.ClassName=LB.Tools.RequestTool.RequestSafeString("ClassName");
			if (HttpContext.Current.Request["Company"] != null)
				model.Company=LB.Tools.RequestTool.RequestSafeString("Company");
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Days_checkuserlow"] != null)
				model.Days_checkuserlow=LB.Tools.RequestTool.RequestInt("Days_checkuserlow",0);
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["Domain"] != null)
				model.Domain=LB.Tools.RequestTool.RequestSafeString("Domain");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["Fax"] != null)
				model.Fax=LB.Tools.RequestTool.RequestSafeString("Fax");
			if (HttpContext.Current.Request["FreezeRemark"] != null)
				model.FreezeRemark=LB.Tools.RequestTool.RequestSafeString("FreezeRemark");
			if (HttpContext.Current.Request["head"] != null)
				model.head=LB.Tools.RequestTool.RequestSafeString("head");
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestSafeString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestSafeString("IP_This");
			if (HttpContext.Current.Request["IsCash"] != null)
				model.IsCash=LB.Tools.RequestTool.RequestInt("IsCash",0);
			if (HttpContext.Current.Request["IsSpread"] != null)
				model.IsSpread=LB.Tools.RequestTool.RequestInt("IsSpread",0);
			if (HttpContext.Current.Request["IsSupplierTransport"] != null)
				model.IsSupplierTransport=LB.Tools.RequestTool.RequestInt("IsSupplierTransport",0);
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestSafeString("Language");
			if (HttpContext.Current.Request["Level_id"] != null)
				model.Level_id=LB.Tools.RequestTool.RequestInt("Level_id",0);
			if (HttpContext.Current.Request["Logo"] != null)
				model.Logo=LB.Tools.RequestTool.RequestSafeString("Logo");
			if (HttpContext.Current.Request["longbar"] != null)
				model.longbar=LB.Tools.RequestTool.RequestSafeString("longbar");
			if (HttpContext.Current.Request["MobilePhone"] != null)
				model.MobilePhone=LB.Tools.RequestTool.RequestSafeString("MobilePhone");
			if (HttpContext.Current.Request["Money_Margin_pay"] != null)
				model.Money_Margin_pay=LB.Tools.RequestTool.RequestDecimal("Money_Margin_pay",0);
			if (HttpContext.Current.Request["Msn"] != null)
				model.Msn=LB.Tools.RequestTool.RequestSafeString("Msn");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["Password"] != null)
				model.Password=LB.Tools.RequestTool.RequestSafeString("Password");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestSafeString("Phone");
			if (HttpContext.Current.Request["PointToMoney"] != null)
				model.PointToMoney=LB.Tools.RequestTool.RequestDecimal("PointToMoney",0);
			if (HttpContext.Current.Request["Postalcode"] != null)
				model.Postalcode=LB.Tools.RequestTool.RequestSafeString("Postalcode");
			if (HttpContext.Current.Request["ProductTop"] != null)
				model.ProductTop=LB.Tools.RequestTool.RequestInt("ProductTop",0);
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestSafeString("QQ");
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestSafeString("RealName");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["SEO_Description"] != null)
				model.SEO_Description=LB.Tools.RequestTool.RequestSafeString("SEO_Description");
			if (HttpContext.Current.Request["SEO_Keywords"] != null)
				model.SEO_Keywords=LB.Tools.RequestTool.RequestSafeString("SEO_Keywords");
			if (HttpContext.Current.Request["SEO_Title"] != null)
				model.SEO_Title=LB.Tools.RequestTool.RequestSafeString("SEO_Title");
			if (HttpContext.Current.Request["ServicePanel"] != null)
				model.ServicePanel=LB.Tools.RequestTool.RequestSafeString("ServicePanel");
			if (HttpContext.Current.Request["Sex"] != null)
				model.Sex=LB.Tools.RequestTool.RequestSafeString("Sex");
			if (HttpContext.Current.Request["shortbar"] != null)
				model.shortbar=LB.Tools.RequestTool.RequestSafeString("shortbar");
			if (HttpContext.Current.Request["Status"] != null)
				model.Status=LB.Tools.RequestTool.RequestInt("Status",0);
			if (HttpContext.Current.Request["SubName"] != null)
				model.SubName=LB.Tools.RequestTool.RequestSafeString("SubName");
			if (HttpContext.Current.Request["Supplier_Group_id"] != null)
				model.Supplier_Group_id=LB.Tools.RequestTool.RequestInt("Supplier_Group_id",0);
			if (HttpContext.Current.Request["Supplier_Skin_id"] != null)
				model.Supplier_Skin_id=LB.Tools.RequestTool.RequestInt("Supplier_Skin_id",0);
			if (HttpContext.Current.Request["SupplierNumber"] != null)
				model.SupplierNumber=LB.Tools.RequestTool.RequestSafeString("SupplierNumber");
			if (HttpContext.Current.Request["Time_Begin"] != null)
				model.Time_Begin=LB.Tools.RequestTool.RequestTime("Time_Begin", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Reg"] != null)
				model.Time_Reg=LB.Tools.RequestTool.RequestTime("Time_Reg", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_SupplierStatus"] != null)
				model.Type_id_SupplierStatus=LB.Tools.RequestTool.RequestInt("Type_id_SupplierStatus",0);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["UserLow"] != null)
				model.UserLow=LB.Tools.RequestTool.RequestInt("UserLow",0);
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestSafeString("UserName");
			if (HttpContext.Current.Request["UserTop"] != null)
				model.UserTop=LB.Tools.RequestTool.RequestInt("UserTop",0);
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Service"] != null)
				model.Money_Service=LB.Tools.RequestTool.RequestDecimal("Money_Service",0);
			if (HttpContext.Current.Request["Money_Margin"] != null)
				model.Money_Margin=LB.Tools.RequestTool.RequestDecimal("Money_Margin",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Supplier ReaderBind(IDataReader dataReader)
		{
			Lebi_Supplier model=new Lebi_Supplier();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Address=dataReader["Address"].ToString();
			ojb = dataReader["Area_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Area_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["BillingDays"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BillingDays= Convert.ToInt32(ojb);
			}
			model.ClassName=dataReader["ClassName"].ToString();
			model.Company=dataReader["Company"].ToString();
			ojb = dataReader["Count_Login"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Login= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Days_checkuserlow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Days_checkuserlow= Convert.ToInt32(ojb);
			}
			model.Description=dataReader["Description"].ToString();
			model.Domain=dataReader["Domain"].ToString();
			model.Email=dataReader["Email"].ToString();
			model.Fax=dataReader["Fax"].ToString();
			model.FreezeRemark=dataReader["FreezeRemark"].ToString();
			model.head=dataReader["head"].ToString();
			model.IP_Last=dataReader["IP_Last"].ToString();
			model.IP_This=dataReader["IP_This"].ToString();
			ojb = dataReader["IsCash"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCash= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSpread"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSpread= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSupplierTransport"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSupplierTransport= Convert.ToInt32(ojb);
			}
			model.Language=dataReader["Language"].ToString();
			ojb = dataReader["Level_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Level_id= Convert.ToInt32(ojb);
			}
			model.Logo=dataReader["Logo"].ToString();
			model.longbar=dataReader["longbar"].ToString();
			model.MobilePhone=dataReader["MobilePhone"].ToString();
			ojb = dataReader["Money_Margin_pay"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Margin_pay=(decimal)ojb;
			}
			model.Msn=dataReader["Msn"].ToString();
			model.Name=dataReader["Name"].ToString();
			model.Password=dataReader["Password"].ToString();
			model.Phone=dataReader["Phone"].ToString();
			ojb = dataReader["PointToMoney"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PointToMoney=(decimal)ojb;
			}
			model.Postalcode=dataReader["Postalcode"].ToString();
			ojb = dataReader["ProductTop"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ProductTop= Convert.ToInt32(ojb);
			}
			model.QQ=dataReader["QQ"].ToString();
			model.RealName=dataReader["RealName"].ToString();
			model.Remark=dataReader["Remark"].ToString();
			model.SEO_Description=dataReader["SEO_Description"].ToString();
			model.SEO_Keywords=dataReader["SEO_Keywords"].ToString();
			model.SEO_Title=dataReader["SEO_Title"].ToString();
			model.ServicePanel=dataReader["ServicePanel"].ToString();
			model.Sex=dataReader["Sex"].ToString();
			model.shortbar=dataReader["shortbar"].ToString();
			ojb = dataReader["Status"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Status= Convert.ToInt32(ojb);
			}
			model.SubName=dataReader["SubName"].ToString();
			ojb = dataReader["Supplier_Group_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_Group_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Supplier_Skin_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_Skin_id= Convert.ToInt32(ojb);
			}
			model.SupplierNumber=dataReader["SupplierNumber"].ToString();
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
			ojb = dataReader["Time_Last"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Last=(DateTime)ojb;
			}
			ojb = dataReader["Time_Reg"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Reg=(DateTime)ojb;
			}
			ojb = dataReader["Time_This"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_This=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_SupplierStatus"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_SupplierStatus= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["UserLow"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserLow= Convert.ToInt32(ojb);
			}
			model.UserName=dataReader["UserName"].ToString();
			ojb = dataReader["UserTop"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserTop= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			ojb = dataReader["Money_Service"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Service=(decimal)ojb;
			}
			ojb = dataReader["Money_Margin"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Margin=(decimal)ojb;
			}
			return model;
		}

		#endregion  成员方法
	}
}

