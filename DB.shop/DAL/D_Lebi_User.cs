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
	/// 数据访问类D_Lebi_User。
	/// </summary>
	public partial class D_Lebi_User
	{
		static D_Lebi_User _Instance;
		public static D_Lebi_User Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_User("Lebi_User");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_User";
		public D_Lebi_User(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_User", 0 , cachestr,seconds);
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
		public int Add(Lebi_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Address")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("AgentMoney")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("AgentMoney_history")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("alipay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("aliwangwang")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_facebook_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_facebook_nickname")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_facebook_token")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_qq_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_qq_nickname")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_qq_token")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_taobao_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_taobao_nickname")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_taobao_token")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weibo_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weibo_nickname")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weibo_token")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weixin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weixin_nickname")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weixin_token")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Birthday")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("CashAccount_Bank")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("CashAccount_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("CashAccount_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("CheckCode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("City")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Order")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Order_OK")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_sonuser")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Device_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Device_system")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Face")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Fax")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IDNumber")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IDType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Introduce")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsAnonymous")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCheckedEmail")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCheckedMobilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsOpen")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPlatformAccount")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("job")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("JYpwd")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("lnum")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("MobilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("momo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Bill")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_fanxian")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Order")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Product")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Transport")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_xiaofei")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Msn")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("NickName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Password")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay_Password")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_Date")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Point")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Postalcode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("pwdda")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("pwdwen")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RandNum")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sex")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_lastorder")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Reg")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Price_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Uavatar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Upass")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_Address_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id_parent")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserNumber")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("weixin")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Yfmoney")+")");
			strSql.Append(" values (");
			strSql.Append("@Address,@AgentMoney,@AgentMoney_history,@alipay,@aliwangwang,@Area_id,@bind_facebook_id,@bind_facebook_nickname,@bind_facebook_token,@bind_qq_id,@bind_qq_nickname,@bind_qq_token,@bind_taobao_id,@bind_taobao_nickname,@bind_taobao_token,@bind_weibo_id,@bind_weibo_nickname,@bind_weibo_token,@bind_weixin_id,@bind_weixin_nickname,@bind_weixin_token,@Birthday,@CashAccount_Bank,@CashAccount_Code,@CashAccount_Name,@CheckCode,@City,@Count_Login,@Count_Order,@Count_Order_OK,@Count_sonuser,@Currency_Code,@Currency_id,@Device_id,@Device_system,@DT_id,@Email,@Face,@Fax,@IDNumber,@IDType,@Introduce,@IP_Last,@IP_This,@IsAnonymous,@IsCheckedEmail,@IsCheckedMobilePhone,@IsDel,@IsOpen,@IsPlatformAccount,@job,@JYpwd,@Language,@lnum,@MobilePhone,@momo,@Money,@Money_Bill,@Money_fanxian,@Money_Order,@Money_Product,@Money_Transport,@Money_xiaofei,@Msn,@NickName,@OnlinePay_id,@Password,@Pay_id,@Pay_Password,@Phone,@PickUp_Date,@PickUp_id,@Point,@Postalcode,@pwdda,@pwdwen,@QQ,@RandNum,@RealName,@Sex,@Site_id,@Time_End,@Time_Last,@Time_lastorder,@Time_Reg,@Time_This,@Transport_Price_id,@Uavatar,@Upass,@User_Address_id,@User_id_parent,@UserLevel_id,@UserName,@UserNumber,@weixin,@Yfmoney);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Address", model.Address),
					new SqlParameter("@AgentMoney", model.AgentMoney),
					new SqlParameter("@AgentMoney_history", model.AgentMoney_history),
					new SqlParameter("@alipay", model.alipay),
					new SqlParameter("@aliwangwang", model.aliwangwang),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@bind_facebook_id", model.bind_facebook_id),
					new SqlParameter("@bind_facebook_nickname", model.bind_facebook_nickname),
					new SqlParameter("@bind_facebook_token", model.bind_facebook_token),
					new SqlParameter("@bind_qq_id", model.bind_qq_id),
					new SqlParameter("@bind_qq_nickname", model.bind_qq_nickname),
					new SqlParameter("@bind_qq_token", model.bind_qq_token),
					new SqlParameter("@bind_taobao_id", model.bind_taobao_id),
					new SqlParameter("@bind_taobao_nickname", model.bind_taobao_nickname),
					new SqlParameter("@bind_taobao_token", model.bind_taobao_token),
					new SqlParameter("@bind_weibo_id", model.bind_weibo_id),
					new SqlParameter("@bind_weibo_nickname", model.bind_weibo_nickname),
					new SqlParameter("@bind_weibo_token", model.bind_weibo_token),
					new SqlParameter("@bind_weixin_id", model.bind_weixin_id),
					new SqlParameter("@bind_weixin_nickname", model.bind_weixin_nickname),
					new SqlParameter("@bind_weixin_token", model.bind_weixin_token),
					new SqlParameter("@Birthday", model.Birthday),
					new SqlParameter("@CashAccount_Bank", model.CashAccount_Bank),
					new SqlParameter("@CashAccount_Code", model.CashAccount_Code),
					new SqlParameter("@CashAccount_Name", model.CashAccount_Name),
					new SqlParameter("@CheckCode", model.CheckCode),
					new SqlParameter("@City", model.City),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Count_Order", model.Count_Order),
					new SqlParameter("@Count_Order_OK", model.Count_Order_OK),
					new SqlParameter("@Count_sonuser", model.Count_sonuser),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Device_id", model.Device_id),
					new SqlParameter("@Device_system", model.Device_system),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Face", model.Face),
					new SqlParameter("@Fax", model.Fax),
					new SqlParameter("@IDNumber", model.IDNumber),
					new SqlParameter("@IDType", model.IDType),
					new SqlParameter("@Introduce", model.Introduce),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@IsAnonymous", model.IsAnonymous),
					new SqlParameter("@IsCheckedEmail", model.IsCheckedEmail),
					new SqlParameter("@IsCheckedMobilePhone", model.IsCheckedMobilePhone),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsOpen", model.IsOpen),
					new SqlParameter("@IsPlatformAccount", model.IsPlatformAccount),
					new SqlParameter("@job", model.job),
					new SqlParameter("@JYpwd", model.JYpwd),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@lnum", model.lnum),
					new SqlParameter("@MobilePhone", model.MobilePhone),
					new SqlParameter("@momo", model.momo),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Bill", model.Money_Bill),
					new SqlParameter("@Money_fanxian", model.Money_fanxian),
					new SqlParameter("@Money_Order", model.Money_Order),
					new SqlParameter("@Money_Product", model.Money_Product),
					new SqlParameter("@Money_Transport", model.Money_Transport),
					new SqlParameter("@Money_xiaofei", model.Money_xiaofei),
					new SqlParameter("@Msn", model.Msn),
					new SqlParameter("@NickName", model.NickName),
					new SqlParameter("@OnlinePay_id", model.OnlinePay_id),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@Pay_id", model.Pay_id),
					new SqlParameter("@Pay_Password", model.Pay_Password),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@PickUp_Date", model.PickUp_Date),
					new SqlParameter("@PickUp_id", model.PickUp_id),
					new SqlParameter("@Point", model.Point),
					new SqlParameter("@Postalcode", model.Postalcode),
					new SqlParameter("@pwdda", model.pwdda),
					new SqlParameter("@pwdwen", model.pwdwen),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@RandNum", model.RandNum),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@Sex", model.Sex),
					new SqlParameter("@Site_id", model.Site_id),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Time_lastorder", model.Time_lastorder),
					new SqlParameter("@Time_Reg", model.Time_Reg),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@Transport_Price_id", model.Transport_Price_id),
					new SqlParameter("@Uavatar", model.Uavatar),
					new SqlParameter("@Upass", model.Upass),
					new SqlParameter("@User_Address_id", model.User_Address_id),
					new SqlParameter("@User_id_parent", model.User_id_parent),
					new SqlParameter("@UserLevel_id", model.UserLevel_id),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@UserNumber", model.UserNumber),
					new SqlParameter("@weixin", model.weixin),
					new SqlParameter("@Yfmoney", model.Yfmoney)};

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
		public void Update(Lebi_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Address,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Address")+"= @Address");
			if((","+model.UpdateCols+",").IndexOf(",AgentMoney,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("AgentMoney")+"= @AgentMoney");
			if((","+model.UpdateCols+",").IndexOf(",AgentMoney_history,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("AgentMoney_history")+"= @AgentMoney_history");
			if((","+model.UpdateCols+",").IndexOf(",alipay,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("alipay")+"= @alipay");
			if((","+model.UpdateCols+",").IndexOf(",aliwangwang,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("aliwangwang")+"= @aliwangwang");
			if((","+model.UpdateCols+",").IndexOf(",Area_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+"= @Area_id");
			if((","+model.UpdateCols+",").IndexOf(",bind_facebook_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_facebook_id")+"= @bind_facebook_id");
			if((","+model.UpdateCols+",").IndexOf(",bind_facebook_nickname,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_facebook_nickname")+"= @bind_facebook_nickname");
			if((","+model.UpdateCols+",").IndexOf(",bind_facebook_token,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_facebook_token")+"= @bind_facebook_token");
			if((","+model.UpdateCols+",").IndexOf(",bind_qq_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_qq_id")+"= @bind_qq_id");
			if((","+model.UpdateCols+",").IndexOf(",bind_qq_nickname,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_qq_nickname")+"= @bind_qq_nickname");
			if((","+model.UpdateCols+",").IndexOf(",bind_qq_token,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_qq_token")+"= @bind_qq_token");
			if((","+model.UpdateCols+",").IndexOf(",bind_taobao_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_taobao_id")+"= @bind_taobao_id");
			if((","+model.UpdateCols+",").IndexOf(",bind_taobao_nickname,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_taobao_nickname")+"= @bind_taobao_nickname");
			if((","+model.UpdateCols+",").IndexOf(",bind_taobao_token,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_taobao_token")+"= @bind_taobao_token");
			if((","+model.UpdateCols+",").IndexOf(",bind_weibo_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weibo_id")+"= @bind_weibo_id");
			if((","+model.UpdateCols+",").IndexOf(",bind_weibo_nickname,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weibo_nickname")+"= @bind_weibo_nickname");
			if((","+model.UpdateCols+",").IndexOf(",bind_weibo_token,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weibo_token")+"= @bind_weibo_token");
			if((","+model.UpdateCols+",").IndexOf(",bind_weixin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weixin_id")+"= @bind_weixin_id");
			if((","+model.UpdateCols+",").IndexOf(",bind_weixin_nickname,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weixin_nickname")+"= @bind_weixin_nickname");
			if((","+model.UpdateCols+",").IndexOf(",bind_weixin_token,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("bind_weixin_token")+"= @bind_weixin_token");
			if((","+model.UpdateCols+",").IndexOf(",Birthday,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Birthday")+"= @Birthday");
			if((","+model.UpdateCols+",").IndexOf(",CashAccount_Bank,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("CashAccount_Bank")+"= @CashAccount_Bank");
			if((","+model.UpdateCols+",").IndexOf(",CashAccount_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("CashAccount_Code")+"= @CashAccount_Code");
			if((","+model.UpdateCols+",").IndexOf(",CashAccount_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("CashAccount_Name")+"= @CashAccount_Name");
			if((","+model.UpdateCols+",").IndexOf(",CheckCode,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("CheckCode")+"= @CheckCode");
			if((","+model.UpdateCols+",").IndexOf(",City,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("City")+"= @City");
			if((","+model.UpdateCols+",").IndexOf(",Count_Login,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+"= @Count_Login");
			if((","+model.UpdateCols+",").IndexOf(",Count_Order,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Order")+"= @Count_Order");
			if((","+model.UpdateCols+",").IndexOf(",Count_Order_OK,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Order_OK")+"= @Count_Order_OK");
			if((","+model.UpdateCols+",").IndexOf(",Count_sonuser,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_sonuser")+"= @Count_sonuser");
			if((","+model.UpdateCols+",").IndexOf(",Currency_Code,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_Code")+"= @Currency_Code");
			if((","+model.UpdateCols+",").IndexOf(",Currency_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Currency_id")+"= @Currency_id");
			if((","+model.UpdateCols+",").IndexOf(",Device_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Device_id")+"= @Device_id");
			if((","+model.UpdateCols+",").IndexOf(",Device_system,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Device_system")+"= @Device_system");
			if((","+model.UpdateCols+",").IndexOf(",DT_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+"= @DT_id");
			if((","+model.UpdateCols+",").IndexOf(",Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if((","+model.UpdateCols+",").IndexOf(",Face,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Face")+"= @Face");
			if((","+model.UpdateCols+",").IndexOf(",Fax,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Fax")+"= @Fax");
			if((","+model.UpdateCols+",").IndexOf(",IDNumber,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IDNumber")+"= @IDNumber");
			if((","+model.UpdateCols+",").IndexOf(",IDType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IDType")+"= @IDType");
			if((","+model.UpdateCols+",").IndexOf(",Introduce,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Introduce")+"= @Introduce");
			if((","+model.UpdateCols+",").IndexOf(",IP_Last,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+"= @IP_Last");
			if((","+model.UpdateCols+",").IndexOf(",IP_This,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+"= @IP_This");
			if((","+model.UpdateCols+",").IndexOf(",IsAnonymous,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsAnonymous")+"= @IsAnonymous");
			if((","+model.UpdateCols+",").IndexOf(",IsCheckedEmail,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCheckedEmail")+"= @IsCheckedEmail");
			if((","+model.UpdateCols+",").IndexOf(",IsCheckedMobilePhone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCheckedMobilePhone")+"= @IsCheckedMobilePhone");
			if((","+model.UpdateCols+",").IndexOf(",IsDel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsDel")+"= @IsDel");
			if((","+model.UpdateCols+",").IndexOf(",IsOpen,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsOpen")+"= @IsOpen");
			if((","+model.UpdateCols+",").IndexOf(",IsPlatformAccount,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsPlatformAccount")+"= @IsPlatformAccount");
			if((","+model.UpdateCols+",").IndexOf(",job,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("job")+"= @job");
			if((","+model.UpdateCols+",").IndexOf(",JYpwd,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("JYpwd")+"= @JYpwd");
			if((","+model.UpdateCols+",").IndexOf(",Language,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+"= @Language");
			if((","+model.UpdateCols+",").IndexOf(",lnum,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("lnum")+"= @lnum");
			if((","+model.UpdateCols+",").IndexOf(",MobilePhone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("MobilePhone")+"= @MobilePhone");
			if((","+model.UpdateCols+",").IndexOf(",momo,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("momo")+"= @momo");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Money_Bill,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Bill")+"= @Money_Bill");
			if((","+model.UpdateCols+",").IndexOf(",Money_fanxian,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_fanxian")+"= @Money_fanxian");
			if((","+model.UpdateCols+",").IndexOf(",Money_Order,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Order")+"= @Money_Order");
			if((","+model.UpdateCols+",").IndexOf(",Money_Product,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Product")+"= @Money_Product");
			if((","+model.UpdateCols+",").IndexOf(",Money_Transport,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_Transport")+"= @Money_Transport");
			if((","+model.UpdateCols+",").IndexOf(",Money_xiaofei,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money_xiaofei")+"= @Money_xiaofei");
			if((","+model.UpdateCols+",").IndexOf(",Msn,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Msn")+"= @Msn");
			if((","+model.UpdateCols+",").IndexOf(",NickName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("NickName")+"= @NickName");
			if((","+model.UpdateCols+",").IndexOf(",OnlinePay_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OnlinePay_id")+"= @OnlinePay_id");
			if((","+model.UpdateCols+",").IndexOf(",Password,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Password")+"= @Password");
			if((","+model.UpdateCols+",").IndexOf(",Pay_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay_id")+"= @Pay_id");
			if((","+model.UpdateCols+",").IndexOf(",Pay_Password,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pay_Password")+"= @Pay_Password");
			if((","+model.UpdateCols+",").IndexOf(",Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+"= @Phone");
			if((","+model.UpdateCols+",").IndexOf(",PickUp_Date,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_Date")+"= @PickUp_Date");
			if((","+model.UpdateCols+",").IndexOf(",PickUp_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PickUp_id")+"= @PickUp_id");
			if((","+model.UpdateCols+",").IndexOf(",Point,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Point")+"= @Point");
			if((","+model.UpdateCols+",").IndexOf(",Postalcode,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Postalcode")+"= @Postalcode");
			if((","+model.UpdateCols+",").IndexOf(",pwdda,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("pwdda")+"= @pwdda");
			if((","+model.UpdateCols+",").IndexOf(",pwdwen,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("pwdwen")+"= @pwdwen");
			if((","+model.UpdateCols+",").IndexOf(",QQ,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+"= @QQ");
			if((","+model.UpdateCols+",").IndexOf(",RandNum,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RandNum")+"= @RandNum");
			if((","+model.UpdateCols+",").IndexOf(",RealName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+"= @RealName");
			if((","+model.UpdateCols+",").IndexOf(",Sex,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sex")+"= @Sex");
			if((","+model.UpdateCols+",").IndexOf(",Site_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_id")+"= @Site_id");
			if((","+model.UpdateCols+",").IndexOf(",Time_End,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+"= @Time_End");
			if((","+model.UpdateCols+",").IndexOf(",Time_Last,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+"= @Time_Last");
			if((","+model.UpdateCols+",").IndexOf(",Time_lastorder,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_lastorder")+"= @Time_lastorder");
			if((","+model.UpdateCols+",").IndexOf(",Time_Reg,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Reg")+"= @Time_Reg");
			if((","+model.UpdateCols+",").IndexOf(",Time_This,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+"= @Time_This");
			if((","+model.UpdateCols+",").IndexOf(",Transport_Price_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_Price_id")+"= @Transport_Price_id");
			if((","+model.UpdateCols+",").IndexOf(",Uavatar,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Uavatar")+"= @Uavatar");
			if((","+model.UpdateCols+",").IndexOf(",Upass,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Upass")+"= @Upass");
			if((","+model.UpdateCols+",").IndexOf(",User_Address_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_Address_id")+"= @User_Address_id");
			if((","+model.UpdateCols+",").IndexOf(",User_id_parent,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id_parent")+"= @User_id_parent");
			if((","+model.UpdateCols+",").IndexOf(",UserLevel_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_id")+"= @UserLevel_id");
			if((","+model.UpdateCols+",").IndexOf(",UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+"= @UserName");
			if((","+model.UpdateCols+",").IndexOf(",UserNumber,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserNumber")+"= @UserNumber");
			if((","+model.UpdateCols+",").IndexOf(",weixin,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("weixin")+"= @weixin");
			if((","+model.UpdateCols+",").IndexOf(",Yfmoney,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Yfmoney")+"= @Yfmoney");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Address", model.Address),
					new SqlParameter("@AgentMoney", model.AgentMoney),
					new SqlParameter("@AgentMoney_history", model.AgentMoney_history),
					new SqlParameter("@alipay", model.alipay),
					new SqlParameter("@aliwangwang", model.aliwangwang),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@bind_facebook_id", model.bind_facebook_id),
					new SqlParameter("@bind_facebook_nickname", model.bind_facebook_nickname),
					new SqlParameter("@bind_facebook_token", model.bind_facebook_token),
					new SqlParameter("@bind_qq_id", model.bind_qq_id),
					new SqlParameter("@bind_qq_nickname", model.bind_qq_nickname),
					new SqlParameter("@bind_qq_token", model.bind_qq_token),
					new SqlParameter("@bind_taobao_id", model.bind_taobao_id),
					new SqlParameter("@bind_taobao_nickname", model.bind_taobao_nickname),
					new SqlParameter("@bind_taobao_token", model.bind_taobao_token),
					new SqlParameter("@bind_weibo_id", model.bind_weibo_id),
					new SqlParameter("@bind_weibo_nickname", model.bind_weibo_nickname),
					new SqlParameter("@bind_weibo_token", model.bind_weibo_token),
					new SqlParameter("@bind_weixin_id", model.bind_weixin_id),
					new SqlParameter("@bind_weixin_nickname", model.bind_weixin_nickname),
					new SqlParameter("@bind_weixin_token", model.bind_weixin_token),
					new SqlParameter("@Birthday", model.Birthday),
					new SqlParameter("@CashAccount_Bank", model.CashAccount_Bank),
					new SqlParameter("@CashAccount_Code", model.CashAccount_Code),
					new SqlParameter("@CashAccount_Name", model.CashAccount_Name),
					new SqlParameter("@CheckCode", model.CheckCode),
					new SqlParameter("@City", model.City),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Count_Order", model.Count_Order),
					new SqlParameter("@Count_Order_OK", model.Count_Order_OK),
					new SqlParameter("@Count_sonuser", model.Count_sonuser),
					new SqlParameter("@Currency_Code", model.Currency_Code),
					new SqlParameter("@Currency_id", model.Currency_id),
					new SqlParameter("@Device_id", model.Device_id),
					new SqlParameter("@Device_system", model.Device_system),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Face", model.Face),
					new SqlParameter("@Fax", model.Fax),
					new SqlParameter("@IDNumber", model.IDNumber),
					new SqlParameter("@IDType", model.IDType),
					new SqlParameter("@Introduce", model.Introduce),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@IsAnonymous", model.IsAnonymous),
					new SqlParameter("@IsCheckedEmail", model.IsCheckedEmail),
					new SqlParameter("@IsCheckedMobilePhone", model.IsCheckedMobilePhone),
					new SqlParameter("@IsDel", model.IsDel),
					new SqlParameter("@IsOpen", model.IsOpen),
					new SqlParameter("@IsPlatformAccount", model.IsPlatformAccount),
					new SqlParameter("@job", model.job),
					new SqlParameter("@JYpwd", model.JYpwd),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@lnum", model.lnum),
					new SqlParameter("@MobilePhone", model.MobilePhone),
					new SqlParameter("@momo", model.momo),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Money_Bill", model.Money_Bill),
					new SqlParameter("@Money_fanxian", model.Money_fanxian),
					new SqlParameter("@Money_Order", model.Money_Order),
					new SqlParameter("@Money_Product", model.Money_Product),
					new SqlParameter("@Money_Transport", model.Money_Transport),
					new SqlParameter("@Money_xiaofei", model.Money_xiaofei),
					new SqlParameter("@Msn", model.Msn),
					new SqlParameter("@NickName", model.NickName),
					new SqlParameter("@OnlinePay_id", model.OnlinePay_id),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@Pay_id", model.Pay_id),
					new SqlParameter("@Pay_Password", model.Pay_Password),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@PickUp_Date", model.PickUp_Date),
					new SqlParameter("@PickUp_id", model.PickUp_id),
					new SqlParameter("@Point", model.Point),
					new SqlParameter("@Postalcode", model.Postalcode),
					new SqlParameter("@pwdda", model.pwdda),
					new SqlParameter("@pwdwen", model.pwdwen),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@RandNum", model.RandNum),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@Sex", model.Sex),
					new SqlParameter("@Site_id", model.Site_id),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Time_lastorder", model.Time_lastorder),
					new SqlParameter("@Time_Reg", model.Time_Reg),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@Transport_Price_id", model.Transport_Price_id),
					new SqlParameter("@Uavatar", model.Uavatar),
					new SqlParameter("@Upass", model.Upass),
					new SqlParameter("@User_Address_id", model.User_Address_id),
					new SqlParameter("@User_id_parent", model.User_id_parent),
					new SqlParameter("@UserLevel_id", model.UserLevel_id),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@UserNumber", model.UserNumber),
					new SqlParameter("@weixin", model.weixin),
					new SqlParameter("@Yfmoney", model.Yfmoney)};
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
		public Lebi_User GetModel(int id, int seconds=0)
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
		           return obj as Lebi_User;
		   }
		   Lebi_User model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_User",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_User GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_User;
		   }
		   Lebi_User model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_User",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_User GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_User;
		   }
		   Lebi_User model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_User",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_User> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_User>;
		   }
		   List<Lebi_User> list = new List<Lebi_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_User> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_User>;
		   }
		   List<Lebi_User> list = new List<Lebi_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_User> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_User>;
		   }
		   List<Lebi_User> list = new List<Lebi_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_User> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_User>;
		   }
		   List<Lebi_User> list = new List<Lebi_User>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_User", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_User BindForm(Lebi_User model)
		{
			if (HttpContext.Current.Request["Address"] != null)
				model.Address=LB.Tools.RequestTool.RequestString("Address");
			if (HttpContext.Current.Request["AgentMoney"] != null)
				model.AgentMoney=LB.Tools.RequestTool.RequestDecimal("AgentMoney",0);
			if (HttpContext.Current.Request["AgentMoney_history"] != null)
				model.AgentMoney_history=LB.Tools.RequestTool.RequestDecimal("AgentMoney_history",0);
			if (HttpContext.Current.Request["alipay"] != null)
				model.alipay=LB.Tools.RequestTool.RequestString("alipay");
			if (HttpContext.Current.Request["aliwangwang"] != null)
				model.aliwangwang=LB.Tools.RequestTool.RequestString("aliwangwang");
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["bind_facebook_id"] != null)
				model.bind_facebook_id=LB.Tools.RequestTool.RequestString("bind_facebook_id");
			if (HttpContext.Current.Request["bind_facebook_nickname"] != null)
				model.bind_facebook_nickname=LB.Tools.RequestTool.RequestString("bind_facebook_nickname");
			if (HttpContext.Current.Request["bind_facebook_token"] != null)
				model.bind_facebook_token=LB.Tools.RequestTool.RequestString("bind_facebook_token");
			if (HttpContext.Current.Request["bind_qq_id"] != null)
				model.bind_qq_id=LB.Tools.RequestTool.RequestString("bind_qq_id");
			if (HttpContext.Current.Request["bind_qq_nickname"] != null)
				model.bind_qq_nickname=LB.Tools.RequestTool.RequestString("bind_qq_nickname");
			if (HttpContext.Current.Request["bind_qq_token"] != null)
				model.bind_qq_token=LB.Tools.RequestTool.RequestString("bind_qq_token");
			if (HttpContext.Current.Request["bind_taobao_id"] != null)
				model.bind_taobao_id=LB.Tools.RequestTool.RequestString("bind_taobao_id");
			if (HttpContext.Current.Request["bind_taobao_nickname"] != null)
				model.bind_taobao_nickname=LB.Tools.RequestTool.RequestString("bind_taobao_nickname");
			if (HttpContext.Current.Request["bind_taobao_token"] != null)
				model.bind_taobao_token=LB.Tools.RequestTool.RequestString("bind_taobao_token");
			if (HttpContext.Current.Request["bind_weibo_id"] != null)
				model.bind_weibo_id=LB.Tools.RequestTool.RequestString("bind_weibo_id");
			if (HttpContext.Current.Request["bind_weibo_nickname"] != null)
				model.bind_weibo_nickname=LB.Tools.RequestTool.RequestString("bind_weibo_nickname");
			if (HttpContext.Current.Request["bind_weibo_token"] != null)
				model.bind_weibo_token=LB.Tools.RequestTool.RequestString("bind_weibo_token");
			if (HttpContext.Current.Request["bind_weixin_id"] != null)
				model.bind_weixin_id=LB.Tools.RequestTool.RequestString("bind_weixin_id");
			if (HttpContext.Current.Request["bind_weixin_nickname"] != null)
				model.bind_weixin_nickname=LB.Tools.RequestTool.RequestString("bind_weixin_nickname");
			if (HttpContext.Current.Request["bind_weixin_token"] != null)
				model.bind_weixin_token=LB.Tools.RequestTool.RequestString("bind_weixin_token");
			if (HttpContext.Current.Request["Birthday"] != null)
				model.Birthday=LB.Tools.RequestTool.RequestTime("Birthday", System.DateTime.Now);
			if (HttpContext.Current.Request["CashAccount_Bank"] != null)
				model.CashAccount_Bank=LB.Tools.RequestTool.RequestString("CashAccount_Bank");
			if (HttpContext.Current.Request["CashAccount_Code"] != null)
				model.CashAccount_Code=LB.Tools.RequestTool.RequestString("CashAccount_Code");
			if (HttpContext.Current.Request["CashAccount_Name"] != null)
				model.CashAccount_Name=LB.Tools.RequestTool.RequestString("CashAccount_Name");
			if (HttpContext.Current.Request["CheckCode"] != null)
				model.CheckCode=LB.Tools.RequestTool.RequestString("CheckCode");
			if (HttpContext.Current.Request["City"] != null)
				model.City=LB.Tools.RequestTool.RequestString("City");
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Count_Order"] != null)
				model.Count_Order=LB.Tools.RequestTool.RequestInt("Count_Order",0);
			if (HttpContext.Current.Request["Count_Order_OK"] != null)
				model.Count_Order_OK=LB.Tools.RequestTool.RequestInt("Count_Order_OK",0);
			if (HttpContext.Current.Request["Count_sonuser"] != null)
				model.Count_sonuser=LB.Tools.RequestTool.RequestInt("Count_sonuser",0);
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestString("Currency_Code");
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Device_id"] != null)
				model.Device_id=LB.Tools.RequestTool.RequestString("Device_id");
			if (HttpContext.Current.Request["Device_system"] != null)
				model.Device_system=LB.Tools.RequestTool.RequestString("Device_system");
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["Face"] != null)
				model.Face=LB.Tools.RequestTool.RequestString("Face");
			if (HttpContext.Current.Request["Fax"] != null)
				model.Fax=LB.Tools.RequestTool.RequestString("Fax");
			if (HttpContext.Current.Request["IDNumber"] != null)
				model.IDNumber=LB.Tools.RequestTool.RequestString("IDNumber");
			if (HttpContext.Current.Request["IDType"] != null)
				model.IDType=LB.Tools.RequestTool.RequestString("IDType");
			if (HttpContext.Current.Request["Introduce"] != null)
				model.Introduce=LB.Tools.RequestTool.RequestString("Introduce");
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestString("IP_This");
			if (HttpContext.Current.Request["IsAnonymous"] != null)
				model.IsAnonymous=LB.Tools.RequestTool.RequestInt("IsAnonymous",0);
			if (HttpContext.Current.Request["IsCheckedEmail"] != null)
				model.IsCheckedEmail=LB.Tools.RequestTool.RequestInt("IsCheckedEmail",0);
			if (HttpContext.Current.Request["IsCheckedMobilePhone"] != null)
				model.IsCheckedMobilePhone=LB.Tools.RequestTool.RequestInt("IsCheckedMobilePhone",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsOpen"] != null)
				model.IsOpen=LB.Tools.RequestTool.RequestInt("IsOpen",0);
			if (HttpContext.Current.Request["IsPlatformAccount"] != null)
				model.IsPlatformAccount=LB.Tools.RequestTool.RequestInt("IsPlatformAccount",0);
			if (HttpContext.Current.Request["job"] != null)
				model.job=LB.Tools.RequestTool.RequestString("job");
			if (HttpContext.Current.Request["JYpwd"] != null)
				model.JYpwd=LB.Tools.RequestTool.RequestString("JYpwd");
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestString("Language");
			if (HttpContext.Current.Request["lnum"] != null)
				model.lnum=LB.Tools.RequestTool.RequestInt("lnum",0);
			if (HttpContext.Current.Request["MobilePhone"] != null)
				model.MobilePhone=LB.Tools.RequestTool.RequestString("MobilePhone");
			if (HttpContext.Current.Request["momo"] != null)
				model.momo=LB.Tools.RequestTool.RequestString("momo");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Bill"] != null)
				model.Money_Bill=LB.Tools.RequestTool.RequestDecimal("Money_Bill",0);
			if (HttpContext.Current.Request["Money_fanxian"] != null)
				model.Money_fanxian=LB.Tools.RequestTool.RequestDecimal("Money_fanxian",0);
			if (HttpContext.Current.Request["Money_Order"] != null)
				model.Money_Order=LB.Tools.RequestTool.RequestDecimal("Money_Order",0);
			if (HttpContext.Current.Request["Money_Product"] != null)
				model.Money_Product=LB.Tools.RequestTool.RequestDecimal("Money_Product",0);
			if (HttpContext.Current.Request["Money_Transport"] != null)
				model.Money_Transport=LB.Tools.RequestTool.RequestDecimal("Money_Transport",0);
			if (HttpContext.Current.Request["Money_xiaofei"] != null)
				model.Money_xiaofei=LB.Tools.RequestTool.RequestDecimal("Money_xiaofei",0);
			if (HttpContext.Current.Request["Msn"] != null)
				model.Msn=LB.Tools.RequestTool.RequestString("Msn");
			if (HttpContext.Current.Request["NickName"] != null)
				model.NickName=LB.Tools.RequestTool.RequestString("NickName");
			if (HttpContext.Current.Request["OnlinePay_id"] != null)
				model.OnlinePay_id=LB.Tools.RequestTool.RequestInt("OnlinePay_id",0);
			if (HttpContext.Current.Request["Password"] != null)
				model.Password=LB.Tools.RequestTool.RequestString("Password");
			if (HttpContext.Current.Request["Pay_id"] != null)
				model.Pay_id=LB.Tools.RequestTool.RequestInt("Pay_id",0);
			if (HttpContext.Current.Request["Pay_Password"] != null)
				model.Pay_Password=LB.Tools.RequestTool.RequestString("Pay_Password");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestString("Phone");
			if (HttpContext.Current.Request["PickUp_Date"] != null)
				model.PickUp_Date=LB.Tools.RequestTool.RequestTime("PickUp_Date", System.DateTime.Now);
			if (HttpContext.Current.Request["PickUp_id"] != null)
				model.PickUp_id=LB.Tools.RequestTool.RequestString("PickUp_id");
			if (HttpContext.Current.Request["Point"] != null)
				model.Point=LB.Tools.RequestTool.RequestDecimal("Point",0);
			if (HttpContext.Current.Request["Postalcode"] != null)
				model.Postalcode=LB.Tools.RequestTool.RequestString("Postalcode");
			if (HttpContext.Current.Request["pwdda"] != null)
				model.pwdda=LB.Tools.RequestTool.RequestString("pwdda");
			if (HttpContext.Current.Request["pwdwen"] != null)
				model.pwdwen=LB.Tools.RequestTool.RequestString("pwdwen");
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestString("QQ");
			if (HttpContext.Current.Request["RandNum"] != null)
				model.RandNum=LB.Tools.RequestTool.RequestInt("RandNum",0);
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestString("RealName");
			if (HttpContext.Current.Request["Sex"] != null)
				model.Sex=LB.Tools.RequestTool.RequestString("Sex");
			if (HttpContext.Current.Request["Site_id"] != null)
				model.Site_id=LB.Tools.RequestTool.RequestInt("Site_id",0);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_lastorder"] != null)
				model.Time_lastorder=LB.Tools.RequestTool.RequestTime("Time_lastorder", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Reg"] != null)
				model.Time_Reg=LB.Tools.RequestTool.RequestTime("Time_Reg", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["Transport_Price_id"] != null)
				model.Transport_Price_id=LB.Tools.RequestTool.RequestString("Transport_Price_id");
			if (HttpContext.Current.Request["Uavatar"] != null)
				model.Uavatar=LB.Tools.RequestTool.RequestString("Uavatar");
			if (HttpContext.Current.Request["Upass"] != null)
				model.Upass=LB.Tools.RequestTool.RequestInt("Upass",0);
			if (HttpContext.Current.Request["User_Address_id"] != null)
				model.User_Address_id=LB.Tools.RequestTool.RequestInt("User_Address_id",0);
			if (HttpContext.Current.Request["User_id_parent"] != null)
				model.User_id_parent=LB.Tools.RequestTool.RequestInt("User_id_parent",0);
			if (HttpContext.Current.Request["UserLevel_id"] != null)
				model.UserLevel_id=LB.Tools.RequestTool.RequestInt("UserLevel_id",0);
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestString("UserName");
			if (HttpContext.Current.Request["UserNumber"] != null)
				model.UserNumber=LB.Tools.RequestTool.RequestString("UserNumber");
			if (HttpContext.Current.Request["weixin"] != null)
				model.weixin=LB.Tools.RequestTool.RequestString("weixin");
			if (HttpContext.Current.Request["Yfmoney"] != null)
				model.Yfmoney=LB.Tools.RequestTool.RequestDecimal("Yfmoney",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_User SafeBindForm(Lebi_User model)
		{
			if (HttpContext.Current.Request["Address"] != null)
				model.Address=LB.Tools.RequestTool.RequestSafeString("Address");
			if (HttpContext.Current.Request["AgentMoney"] != null)
				model.AgentMoney=LB.Tools.RequestTool.RequestDecimal("AgentMoney",0);
			if (HttpContext.Current.Request["AgentMoney_history"] != null)
				model.AgentMoney_history=LB.Tools.RequestTool.RequestDecimal("AgentMoney_history",0);
			if (HttpContext.Current.Request["alipay"] != null)
				model.alipay=LB.Tools.RequestTool.RequestSafeString("alipay");
			if (HttpContext.Current.Request["aliwangwang"] != null)
				model.aliwangwang=LB.Tools.RequestTool.RequestSafeString("aliwangwang");
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["bind_facebook_id"] != null)
				model.bind_facebook_id=LB.Tools.RequestTool.RequestSafeString("bind_facebook_id");
			if (HttpContext.Current.Request["bind_facebook_nickname"] != null)
				model.bind_facebook_nickname=LB.Tools.RequestTool.RequestSafeString("bind_facebook_nickname");
			if (HttpContext.Current.Request["bind_facebook_token"] != null)
				model.bind_facebook_token=LB.Tools.RequestTool.RequestSafeString("bind_facebook_token");
			if (HttpContext.Current.Request["bind_qq_id"] != null)
				model.bind_qq_id=LB.Tools.RequestTool.RequestSafeString("bind_qq_id");
			if (HttpContext.Current.Request["bind_qq_nickname"] != null)
				model.bind_qq_nickname=LB.Tools.RequestTool.RequestSafeString("bind_qq_nickname");
			if (HttpContext.Current.Request["bind_qq_token"] != null)
				model.bind_qq_token=LB.Tools.RequestTool.RequestSafeString("bind_qq_token");
			if (HttpContext.Current.Request["bind_taobao_id"] != null)
				model.bind_taobao_id=LB.Tools.RequestTool.RequestSafeString("bind_taobao_id");
			if (HttpContext.Current.Request["bind_taobao_nickname"] != null)
				model.bind_taobao_nickname=LB.Tools.RequestTool.RequestSafeString("bind_taobao_nickname");
			if (HttpContext.Current.Request["bind_taobao_token"] != null)
				model.bind_taobao_token=LB.Tools.RequestTool.RequestSafeString("bind_taobao_token");
			if (HttpContext.Current.Request["bind_weibo_id"] != null)
				model.bind_weibo_id=LB.Tools.RequestTool.RequestSafeString("bind_weibo_id");
			if (HttpContext.Current.Request["bind_weibo_nickname"] != null)
				model.bind_weibo_nickname=LB.Tools.RequestTool.RequestSafeString("bind_weibo_nickname");
			if (HttpContext.Current.Request["bind_weibo_token"] != null)
				model.bind_weibo_token=LB.Tools.RequestTool.RequestSafeString("bind_weibo_token");
			if (HttpContext.Current.Request["bind_weixin_id"] != null)
				model.bind_weixin_id=LB.Tools.RequestTool.RequestSafeString("bind_weixin_id");
			if (HttpContext.Current.Request["bind_weixin_nickname"] != null)
				model.bind_weixin_nickname=LB.Tools.RequestTool.RequestSafeString("bind_weixin_nickname");
			if (HttpContext.Current.Request["bind_weixin_token"] != null)
				model.bind_weixin_token=LB.Tools.RequestTool.RequestSafeString("bind_weixin_token");
			if (HttpContext.Current.Request["Birthday"] != null)
				model.Birthday=LB.Tools.RequestTool.RequestTime("Birthday", System.DateTime.Now);
			if (HttpContext.Current.Request["CashAccount_Bank"] != null)
				model.CashAccount_Bank=LB.Tools.RequestTool.RequestSafeString("CashAccount_Bank");
			if (HttpContext.Current.Request["CashAccount_Code"] != null)
				model.CashAccount_Code=LB.Tools.RequestTool.RequestSafeString("CashAccount_Code");
			if (HttpContext.Current.Request["CashAccount_Name"] != null)
				model.CashAccount_Name=LB.Tools.RequestTool.RequestSafeString("CashAccount_Name");
			if (HttpContext.Current.Request["CheckCode"] != null)
				model.CheckCode=LB.Tools.RequestTool.RequestSafeString("CheckCode");
			if (HttpContext.Current.Request["City"] != null)
				model.City=LB.Tools.RequestTool.RequestSafeString("City");
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Count_Order"] != null)
				model.Count_Order=LB.Tools.RequestTool.RequestInt("Count_Order",0);
			if (HttpContext.Current.Request["Count_Order_OK"] != null)
				model.Count_Order_OK=LB.Tools.RequestTool.RequestInt("Count_Order_OK",0);
			if (HttpContext.Current.Request["Count_sonuser"] != null)
				model.Count_sonuser=LB.Tools.RequestTool.RequestInt("Count_sonuser",0);
			if (HttpContext.Current.Request["Currency_Code"] != null)
				model.Currency_Code=LB.Tools.RequestTool.RequestSafeString("Currency_Code");
			if (HttpContext.Current.Request["Currency_id"] != null)
				model.Currency_id=LB.Tools.RequestTool.RequestInt("Currency_id",0);
			if (HttpContext.Current.Request["Device_id"] != null)
				model.Device_id=LB.Tools.RequestTool.RequestSafeString("Device_id");
			if (HttpContext.Current.Request["Device_system"] != null)
				model.Device_system=LB.Tools.RequestTool.RequestSafeString("Device_system");
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["Face"] != null)
				model.Face=LB.Tools.RequestTool.RequestSafeString("Face");
			if (HttpContext.Current.Request["Fax"] != null)
				model.Fax=LB.Tools.RequestTool.RequestSafeString("Fax");
			if (HttpContext.Current.Request["IDNumber"] != null)
				model.IDNumber=LB.Tools.RequestTool.RequestSafeString("IDNumber");
			if (HttpContext.Current.Request["IDType"] != null)
				model.IDType=LB.Tools.RequestTool.RequestSafeString("IDType");
			if (HttpContext.Current.Request["Introduce"] != null)
				model.Introduce=LB.Tools.RequestTool.RequestSafeString("Introduce");
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestSafeString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestSafeString("IP_This");
			if (HttpContext.Current.Request["IsAnonymous"] != null)
				model.IsAnonymous=LB.Tools.RequestTool.RequestInt("IsAnonymous",0);
			if (HttpContext.Current.Request["IsCheckedEmail"] != null)
				model.IsCheckedEmail=LB.Tools.RequestTool.RequestInt("IsCheckedEmail",0);
			if (HttpContext.Current.Request["IsCheckedMobilePhone"] != null)
				model.IsCheckedMobilePhone=LB.Tools.RequestTool.RequestInt("IsCheckedMobilePhone",0);
			if (HttpContext.Current.Request["IsDel"] != null)
				model.IsDel=LB.Tools.RequestTool.RequestInt("IsDel",0);
			if (HttpContext.Current.Request["IsOpen"] != null)
				model.IsOpen=LB.Tools.RequestTool.RequestInt("IsOpen",0);
			if (HttpContext.Current.Request["IsPlatformAccount"] != null)
				model.IsPlatformAccount=LB.Tools.RequestTool.RequestInt("IsPlatformAccount",0);
			if (HttpContext.Current.Request["job"] != null)
				model.job=LB.Tools.RequestTool.RequestSafeString("job");
			if (HttpContext.Current.Request["JYpwd"] != null)
				model.JYpwd=LB.Tools.RequestTool.RequestSafeString("JYpwd");
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestSafeString("Language");
			if (HttpContext.Current.Request["lnum"] != null)
				model.lnum=LB.Tools.RequestTool.RequestInt("lnum",0);
			if (HttpContext.Current.Request["MobilePhone"] != null)
				model.MobilePhone=LB.Tools.RequestTool.RequestSafeString("MobilePhone");
			if (HttpContext.Current.Request["momo"] != null)
				model.momo=LB.Tools.RequestTool.RequestSafeString("momo");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Money_Bill"] != null)
				model.Money_Bill=LB.Tools.RequestTool.RequestDecimal("Money_Bill",0);
			if (HttpContext.Current.Request["Money_fanxian"] != null)
				model.Money_fanxian=LB.Tools.RequestTool.RequestDecimal("Money_fanxian",0);
			if (HttpContext.Current.Request["Money_Order"] != null)
				model.Money_Order=LB.Tools.RequestTool.RequestDecimal("Money_Order",0);
			if (HttpContext.Current.Request["Money_Product"] != null)
				model.Money_Product=LB.Tools.RequestTool.RequestDecimal("Money_Product",0);
			if (HttpContext.Current.Request["Money_Transport"] != null)
				model.Money_Transport=LB.Tools.RequestTool.RequestDecimal("Money_Transport",0);
			if (HttpContext.Current.Request["Money_xiaofei"] != null)
				model.Money_xiaofei=LB.Tools.RequestTool.RequestDecimal("Money_xiaofei",0);
			if (HttpContext.Current.Request["Msn"] != null)
				model.Msn=LB.Tools.RequestTool.RequestSafeString("Msn");
			if (HttpContext.Current.Request["NickName"] != null)
				model.NickName=LB.Tools.RequestTool.RequestSafeString("NickName");
			if (HttpContext.Current.Request["OnlinePay_id"] != null)
				model.OnlinePay_id=LB.Tools.RequestTool.RequestInt("OnlinePay_id",0);
			if (HttpContext.Current.Request["Password"] != null)
				model.Password=LB.Tools.RequestTool.RequestSafeString("Password");
			if (HttpContext.Current.Request["Pay_id"] != null)
				model.Pay_id=LB.Tools.RequestTool.RequestInt("Pay_id",0);
			if (HttpContext.Current.Request["Pay_Password"] != null)
				model.Pay_Password=LB.Tools.RequestTool.RequestSafeString("Pay_Password");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestSafeString("Phone");
			if (HttpContext.Current.Request["PickUp_Date"] != null)
				model.PickUp_Date=LB.Tools.RequestTool.RequestTime("PickUp_Date", System.DateTime.Now);
			if (HttpContext.Current.Request["PickUp_id"] != null)
				model.PickUp_id=LB.Tools.RequestTool.RequestSafeString("PickUp_id");
			if (HttpContext.Current.Request["Point"] != null)
				model.Point=LB.Tools.RequestTool.RequestDecimal("Point",0);
			if (HttpContext.Current.Request["Postalcode"] != null)
				model.Postalcode=LB.Tools.RequestTool.RequestSafeString("Postalcode");
			if (HttpContext.Current.Request["pwdda"] != null)
				model.pwdda=LB.Tools.RequestTool.RequestSafeString("pwdda");
			if (HttpContext.Current.Request["pwdwen"] != null)
				model.pwdwen=LB.Tools.RequestTool.RequestSafeString("pwdwen");
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestSafeString("QQ");
			if (HttpContext.Current.Request["RandNum"] != null)
				model.RandNum=LB.Tools.RequestTool.RequestInt("RandNum",0);
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestSafeString("RealName");
			if (HttpContext.Current.Request["Sex"] != null)
				model.Sex=LB.Tools.RequestTool.RequestSafeString("Sex");
			if (HttpContext.Current.Request["Site_id"] != null)
				model.Site_id=LB.Tools.RequestTool.RequestInt("Site_id",0);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_lastorder"] != null)
				model.Time_lastorder=LB.Tools.RequestTool.RequestTime("Time_lastorder", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Reg"] != null)
				model.Time_Reg=LB.Tools.RequestTool.RequestTime("Time_Reg", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["Transport_Price_id"] != null)
				model.Transport_Price_id=LB.Tools.RequestTool.RequestSafeString("Transport_Price_id");
			if (HttpContext.Current.Request["Uavatar"] != null)
				model.Uavatar=LB.Tools.RequestTool.RequestSafeString("Uavatar");
			if (HttpContext.Current.Request["Upass"] != null)
				model.Upass=LB.Tools.RequestTool.RequestInt("Upass",0);
			if (HttpContext.Current.Request["User_Address_id"] != null)
				model.User_Address_id=LB.Tools.RequestTool.RequestInt("User_Address_id",0);
			if (HttpContext.Current.Request["User_id_parent"] != null)
				model.User_id_parent=LB.Tools.RequestTool.RequestInt("User_id_parent",0);
			if (HttpContext.Current.Request["UserLevel_id"] != null)
				model.UserLevel_id=LB.Tools.RequestTool.RequestInt("UserLevel_id",0);
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestSafeString("UserName");
			if (HttpContext.Current.Request["UserNumber"] != null)
				model.UserNumber=LB.Tools.RequestTool.RequestSafeString("UserNumber");
			if (HttpContext.Current.Request["weixin"] != null)
				model.weixin=LB.Tools.RequestTool.RequestSafeString("weixin");
			if (HttpContext.Current.Request["Yfmoney"] != null)
				model.Yfmoney=LB.Tools.RequestTool.RequestDecimal("Yfmoney",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_User ReaderBind(IDataReader dataReader)
		{
			Lebi_User model=new Lebi_User();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Address=dataReader["Address"].ToString();
			ojb = dataReader["AgentMoney"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.AgentMoney=(decimal)ojb;
			}
			ojb = dataReader["AgentMoney_history"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.AgentMoney_history=(decimal)ojb;
			}
			model.alipay=dataReader["alipay"].ToString();
			model.aliwangwang=dataReader["aliwangwang"].ToString();
			ojb = dataReader["Area_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Area_id= Convert.ToInt32(ojb);
			}
			model.bind_facebook_id=dataReader["bind_facebook_id"].ToString();
			model.bind_facebook_nickname=dataReader["bind_facebook_nickname"].ToString();
			model.bind_facebook_token=dataReader["bind_facebook_token"].ToString();
			model.bind_qq_id=dataReader["bind_qq_id"].ToString();
			model.bind_qq_nickname=dataReader["bind_qq_nickname"].ToString();
			model.bind_qq_token=dataReader["bind_qq_token"].ToString();
			model.bind_taobao_id=dataReader["bind_taobao_id"].ToString();
			model.bind_taobao_nickname=dataReader["bind_taobao_nickname"].ToString();
			model.bind_taobao_token=dataReader["bind_taobao_token"].ToString();
			model.bind_weibo_id=dataReader["bind_weibo_id"].ToString();
			model.bind_weibo_nickname=dataReader["bind_weibo_nickname"].ToString();
			model.bind_weibo_token=dataReader["bind_weibo_token"].ToString();
			model.bind_weixin_id=dataReader["bind_weixin_id"].ToString();
			model.bind_weixin_nickname=dataReader["bind_weixin_nickname"].ToString();
			model.bind_weixin_token=dataReader["bind_weixin_token"].ToString();
			ojb = dataReader["Birthday"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Birthday=(DateTime)ojb;
			}
			model.CashAccount_Bank=dataReader["CashAccount_Bank"].ToString();
			model.CashAccount_Code=dataReader["CashAccount_Code"].ToString();
			model.CashAccount_Name=dataReader["CashAccount_Name"].ToString();
			model.CheckCode=dataReader["CheckCode"].ToString();
			model.City=dataReader["City"].ToString();
			ojb = dataReader["Count_Login"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Login= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Order"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Order= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Order_OK"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Order_OK= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_sonuser"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_sonuser= Convert.ToInt32(ojb);
			}
			model.Currency_Code=dataReader["Currency_Code"].ToString();
			ojb = dataReader["Currency_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Currency_id= Convert.ToInt32(ojb);
			}
			model.Device_id=dataReader["Device_id"].ToString();
			model.Device_system=dataReader["Device_system"].ToString();
			ojb = dataReader["DT_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.DT_id= Convert.ToInt32(ojb);
			}
			model.Email=dataReader["Email"].ToString();
			model.Face=dataReader["Face"].ToString();
			model.Fax=dataReader["Fax"].ToString();
			model.IDNumber=dataReader["IDNumber"].ToString();
			model.IDType=dataReader["IDType"].ToString();
			model.Introduce=dataReader["Introduce"].ToString();
			model.IP_Last=dataReader["IP_Last"].ToString();
			model.IP_This=dataReader["IP_This"].ToString();
			ojb = dataReader["IsAnonymous"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsAnonymous= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCheckedEmail"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCheckedEmail= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCheckedMobilePhone"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCheckedMobilePhone= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsDel"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsDel= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsOpen"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsOpen= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsPlatformAccount"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsPlatformAccount= Convert.ToInt32(ojb);
			}
			model.job=dataReader["job"].ToString();
			model.JYpwd=dataReader["JYpwd"].ToString();
			model.Language=dataReader["Language"].ToString();
			ojb = dataReader["lnum"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.lnum= Convert.ToInt32(ojb);
			}
			model.MobilePhone=dataReader["MobilePhone"].ToString();
			model.momo=dataReader["momo"].ToString();
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			ojb = dataReader["Money_Bill"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Bill=(decimal)ojb;
			}
			ojb = dataReader["Money_fanxian"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_fanxian=(decimal)ojb;
			}
			ojb = dataReader["Money_Order"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Order=(decimal)ojb;
			}
			ojb = dataReader["Money_Product"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Product=(decimal)ojb;
			}
			ojb = dataReader["Money_Transport"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_Transport=(decimal)ojb;
			}
			ojb = dataReader["Money_xiaofei"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money_xiaofei=(decimal)ojb;
			}
			model.Msn=dataReader["Msn"].ToString();
			model.NickName=dataReader["NickName"].ToString();
			ojb = dataReader["OnlinePay_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OnlinePay_id= Convert.ToInt32(ojb);
			}
			model.Password=dataReader["Password"].ToString();
			ojb = dataReader["Pay_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Pay_id= Convert.ToInt32(ojb);
			}
			model.Pay_Password=dataReader["Pay_Password"].ToString();
			model.Phone=dataReader["Phone"].ToString();
			ojb = dataReader["PickUp_Date"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PickUp_Date=(DateTime)ojb;
			}
			model.PickUp_id=dataReader["PickUp_id"].ToString();
			ojb = dataReader["Point"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Point=(decimal)ojb;
			}
			model.Postalcode=dataReader["Postalcode"].ToString();
			model.pwdda=dataReader["pwdda"].ToString();
			model.pwdwen=dataReader["pwdwen"].ToString();
			model.QQ=dataReader["QQ"].ToString();
			ojb = dataReader["RandNum"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.RandNum= Convert.ToInt32(ojb);
			}
			model.RealName=dataReader["RealName"].ToString();
			model.Sex=dataReader["Sex"].ToString();
			ojb = dataReader["Site_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Site_id= Convert.ToInt32(ojb);
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
			ojb = dataReader["Time_lastorder"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_lastorder=(DateTime)ojb;
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
			model.Transport_Price_id=dataReader["Transport_Price_id"].ToString();
			model.Uavatar=dataReader["Uavatar"].ToString();
			ojb = dataReader["Upass"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Upass= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_Address_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_Address_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_id_parent"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id_parent= Convert.ToInt32(ojb);
			}
			ojb = dataReader["UserLevel_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserLevel_id= Convert.ToInt32(ojb);
			}
			model.UserName=dataReader["UserName"].ToString();
			model.UserNumber=dataReader["UserNumber"].ToString();
			model.weixin=dataReader["weixin"].ToString();
			ojb = dataReader["Yfmoney"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Yfmoney=(decimal)ojb;
			}
			return model;
		}

		#endregion  成员方法
	}
}

