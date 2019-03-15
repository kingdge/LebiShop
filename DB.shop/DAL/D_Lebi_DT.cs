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
	/// 数据访问类D_Lebi_DT。
	/// </summary>
	public partial class D_Lebi_DT
	{
		static D_Lebi_DT _Instance;
		public static D_Lebi_DT Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_DT("Lebi_DT");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_DT";
		public D_Lebi_DT(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_DT", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_DT", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_DT" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_DT", 0 , cachestr,seconds);
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
		public int Add(Lebi_DT model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Address")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("CommissionLevel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Domain")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Group_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Logo")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("MobilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Msn")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Postalcode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Copyright")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Keywords")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Logoimg")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_QQ")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Status")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Reg")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+")");
			strSql.Append(" values (");
			strSql.Append("@Address,@Area_id,@CommissionLevel,@Count_Login,@Domain,@Email,@Group_id,@IP_Last,@IP_This,@Language,@Logo,@MobilePhone,@Money,@Msn,@Phone,@Postalcode,@Product_ids,@QQ,@RealName,@Remark,@Site_Copyright,@Site_Description,@Site_Email,@Site_Keywords,@Site_Logoimg,@Site_Name,@Site_Phone,@Site_QQ,@Site_Title,@Status,@Time_Last,@Time_Reg,@Time_This,@User_id,@UserLevel_id,@UserName);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Address", model.Address),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@CommissionLevel", model.CommissionLevel),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Domain", model.Domain),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Group_id", model.Group_id),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Logo", model.Logo),
					new SqlParameter("@MobilePhone", model.MobilePhone),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Msn", model.Msn),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@Postalcode", model.Postalcode),
					new SqlParameter("@Product_ids", model.Product_ids),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Site_Copyright", model.Site_Copyright),
					new SqlParameter("@Site_Description", model.Site_Description),
					new SqlParameter("@Site_Email", model.Site_Email),
					new SqlParameter("@Site_Keywords", model.Site_Keywords),
					new SqlParameter("@Site_Logoimg", model.Site_Logoimg),
					new SqlParameter("@Site_Name", model.Site_Name),
					new SqlParameter("@Site_Phone", model.Site_Phone),
					new SqlParameter("@Site_QQ", model.Site_QQ),
					new SqlParameter("@Site_Title", model.Site_Title),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Time_Reg", model.Time_Reg),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@UserLevel_id", model.UserLevel_id),
					new SqlParameter("@UserName", model.UserName)};

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
		public void Update(Lebi_DT model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Address,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Address")+"= @Address");
			if((","+model.UpdateCols+",").IndexOf(",Area_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+"= @Area_id");
			if((","+model.UpdateCols+",").IndexOf(",CommissionLevel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("CommissionLevel")+"= @CommissionLevel");
			if((","+model.UpdateCols+",").IndexOf(",Count_Login,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+"= @Count_Login");
			if((","+model.UpdateCols+",").IndexOf(",Domain,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Domain")+"= @Domain");
			if((","+model.UpdateCols+",").IndexOf(",Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if((","+model.UpdateCols+",").IndexOf(",Group_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Group_id")+"= @Group_id");
			if((","+model.UpdateCols+",").IndexOf(",IP_Last,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+"= @IP_Last");
			if((","+model.UpdateCols+",").IndexOf(",IP_This,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+"= @IP_This");
			if((","+model.UpdateCols+",").IndexOf(",Language,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+"= @Language");
			if((","+model.UpdateCols+",").IndexOf(",Logo,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Logo")+"= @Logo");
			if((","+model.UpdateCols+",").IndexOf(",MobilePhone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("MobilePhone")+"= @MobilePhone");
			if((","+model.UpdateCols+",").IndexOf(",Money,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Money")+"= @Money");
			if((","+model.UpdateCols+",").IndexOf(",Msn,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Msn")+"= @Msn");
			if((","+model.UpdateCols+",").IndexOf(",Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+"= @Phone");
			if((","+model.UpdateCols+",").IndexOf(",Postalcode,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Postalcode")+"= @Postalcode");
			if((","+model.UpdateCols+",").IndexOf(",Product_ids,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Product_ids")+"= @Product_ids");
			if((","+model.UpdateCols+",").IndexOf(",QQ,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+"= @QQ");
			if((","+model.UpdateCols+",").IndexOf(",RealName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+"= @RealName");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",Site_Copyright,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Copyright")+"= @Site_Copyright");
			if((","+model.UpdateCols+",").IndexOf(",Site_Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Description")+"= @Site_Description");
			if((","+model.UpdateCols+",").IndexOf(",Site_Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Email")+"= @Site_Email");
			if((","+model.UpdateCols+",").IndexOf(",Site_Keywords,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Keywords")+"= @Site_Keywords");
			if((","+model.UpdateCols+",").IndexOf(",Site_Logoimg,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Logoimg")+"= @Site_Logoimg");
			if((","+model.UpdateCols+",").IndexOf(",Site_Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Name")+"= @Site_Name");
			if((","+model.UpdateCols+",").IndexOf(",Site_Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Phone")+"= @Site_Phone");
			if((","+model.UpdateCols+",").IndexOf(",Site_QQ,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_QQ")+"= @Site_QQ");
			if((","+model.UpdateCols+",").IndexOf(",Site_Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_Title")+"= @Site_Title");
			if((","+model.UpdateCols+",").IndexOf(",Status,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Status")+"= @Status");
			if((","+model.UpdateCols+",").IndexOf(",Time_Last,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+"= @Time_Last");
			if((","+model.UpdateCols+",").IndexOf(",Time_Reg,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Reg")+"= @Time_Reg");
			if((","+model.UpdateCols+",").IndexOf(",Time_This,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+"= @Time_This");
			if((","+model.UpdateCols+",").IndexOf(",User_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id")+"= @User_id");
			if((","+model.UpdateCols+",").IndexOf(",UserLevel_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserLevel_id")+"= @UserLevel_id");
			if((","+model.UpdateCols+",").IndexOf(",UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+"= @UserName");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Address", model.Address),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@CommissionLevel", model.CommissionLevel),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Domain", model.Domain),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Group_id", model.Group_id),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Logo", model.Logo),
					new SqlParameter("@MobilePhone", model.MobilePhone),
					new SqlParameter("@Money", model.Money),
					new SqlParameter("@Msn", model.Msn),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@Postalcode", model.Postalcode),
					new SqlParameter("@Product_ids", model.Product_ids),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Site_Copyright", model.Site_Copyright),
					new SqlParameter("@Site_Description", model.Site_Description),
					new SqlParameter("@Site_Email", model.Site_Email),
					new SqlParameter("@Site_Keywords", model.Site_Keywords),
					new SqlParameter("@Site_Logoimg", model.Site_Logoimg),
					new SqlParameter("@Site_Name", model.Site_Name),
					new SqlParameter("@Site_Phone", model.Site_Phone),
					new SqlParameter("@Site_QQ", model.Site_QQ),
					new SqlParameter("@Site_Title", model.Site_Title),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Time_Reg", model.Time_Reg),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@User_id", model.User_id),
					new SqlParameter("@UserLevel_id", model.UserLevel_id),
					new SqlParameter("@UserName", model.UserName)};
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
		public Lebi_DT GetModel(int id, int seconds=0)
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
		           return obj as Lebi_DT;
		   }
		   Lebi_DT model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_DT",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_DT GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_DT;
		   }
		   Lebi_DT model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_DT",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_DT GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_DT;
		   }
		   Lebi_DT model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_DT",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_DT> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_DT>;
		   }
		   List<Lebi_DT> list = new List<Lebi_DT>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_DT", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_DT> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_DT>;
		   }
		   List<Lebi_DT> list = new List<Lebi_DT>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_DT", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_DT> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_DT>;
		   }
		   List<Lebi_DT> list = new List<Lebi_DT>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_DT", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_DT> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_DT>;
		   }
		   List<Lebi_DT> list = new List<Lebi_DT>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_DT", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_DT BindForm(Lebi_DT model)
		{
			if (HttpContext.Current.Request["Address"] != null)
				model.Address=LB.Tools.RequestTool.RequestString("Address");
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["CommissionLevel"] != null)
				model.CommissionLevel=LB.Tools.RequestTool.RequestInt("CommissionLevel",0);
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Domain"] != null)
				model.Domain=LB.Tools.RequestTool.RequestString("Domain");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["Group_id"] != null)
				model.Group_id=LB.Tools.RequestTool.RequestInt("Group_id",0);
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestString("IP_This");
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestString("Language");
			if (HttpContext.Current.Request["Logo"] != null)
				model.Logo=LB.Tools.RequestTool.RequestString("Logo");
			if (HttpContext.Current.Request["MobilePhone"] != null)
				model.MobilePhone=LB.Tools.RequestTool.RequestString("MobilePhone");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Msn"] != null)
				model.Msn=LB.Tools.RequestTool.RequestString("Msn");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestString("Phone");
			if (HttpContext.Current.Request["Postalcode"] != null)
				model.Postalcode=LB.Tools.RequestTool.RequestString("Postalcode");
			if (HttpContext.Current.Request["Product_ids"] != null)
				model.Product_ids=LB.Tools.RequestTool.RequestString("Product_ids");
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestString("QQ");
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestString("RealName");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["Site_Copyright"] != null)
				model.Site_Copyright=LB.Tools.RequestTool.RequestString("Site_Copyright");
			if (HttpContext.Current.Request["Site_Description"] != null)
				model.Site_Description=LB.Tools.RequestTool.RequestString("Site_Description");
			if (HttpContext.Current.Request["Site_Email"] != null)
				model.Site_Email=LB.Tools.RequestTool.RequestString("Site_Email");
			if (HttpContext.Current.Request["Site_Keywords"] != null)
				model.Site_Keywords=LB.Tools.RequestTool.RequestString("Site_Keywords");
			if (HttpContext.Current.Request["Site_Logoimg"] != null)
				model.Site_Logoimg=LB.Tools.RequestTool.RequestString("Site_Logoimg");
			if (HttpContext.Current.Request["Site_Name"] != null)
				model.Site_Name=LB.Tools.RequestTool.RequestString("Site_Name");
			if (HttpContext.Current.Request["Site_Phone"] != null)
				model.Site_Phone=LB.Tools.RequestTool.RequestString("Site_Phone");
			if (HttpContext.Current.Request["Site_QQ"] != null)
				model.Site_QQ=LB.Tools.RequestTool.RequestString("Site_QQ");
			if (HttpContext.Current.Request["Site_Title"] != null)
				model.Site_Title=LB.Tools.RequestTool.RequestString("Site_Title");
			if (HttpContext.Current.Request["Status"] != null)
				model.Status=LB.Tools.RequestTool.RequestInt("Status",0);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Reg"] != null)
				model.Time_Reg=LB.Tools.RequestTool.RequestTime("Time_Reg", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["UserLevel_id"] != null)
				model.UserLevel_id=LB.Tools.RequestTool.RequestInt("UserLevel_id",0);
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestString("UserName");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_DT SafeBindForm(Lebi_DT model)
		{
			if (HttpContext.Current.Request["Address"] != null)
				model.Address=LB.Tools.RequestTool.RequestSafeString("Address");
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["CommissionLevel"] != null)
				model.CommissionLevel=LB.Tools.RequestTool.RequestInt("CommissionLevel",0);
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Domain"] != null)
				model.Domain=LB.Tools.RequestTool.RequestSafeString("Domain");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["Group_id"] != null)
				model.Group_id=LB.Tools.RequestTool.RequestInt("Group_id",0);
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestSafeString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestSafeString("IP_This");
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestSafeString("Language");
			if (HttpContext.Current.Request["Logo"] != null)
				model.Logo=LB.Tools.RequestTool.RequestSafeString("Logo");
			if (HttpContext.Current.Request["MobilePhone"] != null)
				model.MobilePhone=LB.Tools.RequestTool.RequestSafeString("MobilePhone");
			if (HttpContext.Current.Request["Money"] != null)
				model.Money=LB.Tools.RequestTool.RequestDecimal("Money",0);
			if (HttpContext.Current.Request["Msn"] != null)
				model.Msn=LB.Tools.RequestTool.RequestSafeString("Msn");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestSafeString("Phone");
			if (HttpContext.Current.Request["Postalcode"] != null)
				model.Postalcode=LB.Tools.RequestTool.RequestSafeString("Postalcode");
			if (HttpContext.Current.Request["Product_ids"] != null)
				model.Product_ids=LB.Tools.RequestTool.RequestSafeString("Product_ids");
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestSafeString("QQ");
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestSafeString("RealName");
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["Site_Copyright"] != null)
				model.Site_Copyright=LB.Tools.RequestTool.RequestSafeString("Site_Copyright");
			if (HttpContext.Current.Request["Site_Description"] != null)
				model.Site_Description=LB.Tools.RequestTool.RequestSafeString("Site_Description");
			if (HttpContext.Current.Request["Site_Email"] != null)
				model.Site_Email=LB.Tools.RequestTool.RequestSafeString("Site_Email");
			if (HttpContext.Current.Request["Site_Keywords"] != null)
				model.Site_Keywords=LB.Tools.RequestTool.RequestSafeString("Site_Keywords");
			if (HttpContext.Current.Request["Site_Logoimg"] != null)
				model.Site_Logoimg=LB.Tools.RequestTool.RequestSafeString("Site_Logoimg");
			if (HttpContext.Current.Request["Site_Name"] != null)
				model.Site_Name=LB.Tools.RequestTool.RequestSafeString("Site_Name");
			if (HttpContext.Current.Request["Site_Phone"] != null)
				model.Site_Phone=LB.Tools.RequestTool.RequestSafeString("Site_Phone");
			if (HttpContext.Current.Request["Site_QQ"] != null)
				model.Site_QQ=LB.Tools.RequestTool.RequestSafeString("Site_QQ");
			if (HttpContext.Current.Request["Site_Title"] != null)
				model.Site_Title=LB.Tools.RequestTool.RequestSafeString("Site_Title");
			if (HttpContext.Current.Request["Status"] != null)
				model.Status=LB.Tools.RequestTool.RequestInt("Status",0);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Reg"] != null)
				model.Time_Reg=LB.Tools.RequestTool.RequestTime("Time_Reg", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["User_id"] != null)
				model.User_id=LB.Tools.RequestTool.RequestInt("User_id",0);
			if (HttpContext.Current.Request["UserLevel_id"] != null)
				model.UserLevel_id=LB.Tools.RequestTool.RequestInt("UserLevel_id",0);
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestSafeString("UserName");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_DT ReaderBind(IDataReader dataReader)
		{
			Lebi_DT model=new Lebi_DT();
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
			ojb = dataReader["CommissionLevel"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CommissionLevel= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Count_Login"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Count_Login= Convert.ToInt32(ojb);
			}
			model.Domain=dataReader["Domain"].ToString();
			model.Email=dataReader["Email"].ToString();
			ojb = dataReader["Group_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Group_id= Convert.ToInt32(ojb);
			}
			model.IP_Last=dataReader["IP_Last"].ToString();
			model.IP_This=dataReader["IP_This"].ToString();
			model.Language=dataReader["Language"].ToString();
			model.Logo=dataReader["Logo"].ToString();
			model.MobilePhone=dataReader["MobilePhone"].ToString();
			ojb = dataReader["Money"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Money=(decimal)ojb;
			}
			model.Msn=dataReader["Msn"].ToString();
			model.Phone=dataReader["Phone"].ToString();
			model.Postalcode=dataReader["Postalcode"].ToString();
			model.Product_ids=dataReader["Product_ids"].ToString();
			model.QQ=dataReader["QQ"].ToString();
			model.RealName=dataReader["RealName"].ToString();
			model.Remark=dataReader["Remark"].ToString();
			model.Site_Copyright=dataReader["Site_Copyright"].ToString();
			model.Site_Description=dataReader["Site_Description"].ToString();
			model.Site_Email=dataReader["Site_Email"].ToString();
			model.Site_Keywords=dataReader["Site_Keywords"].ToString();
			model.Site_Logoimg=dataReader["Site_Logoimg"].ToString();
			model.Site_Name=dataReader["Site_Name"].ToString();
			model.Site_Phone=dataReader["Site_Phone"].ToString();
			model.Site_QQ=dataReader["Site_QQ"].ToString();
			model.Site_Title=dataReader["Site_Title"].ToString();
			ojb = dataReader["Status"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Status= Convert.ToInt32(ojb);
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
			ojb = dataReader["User_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["UserLevel_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserLevel_id= Convert.ToInt32(ojb);
			}
			model.UserName=dataReader["UserName"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

