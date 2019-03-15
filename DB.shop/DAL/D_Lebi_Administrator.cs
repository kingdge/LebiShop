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
	/// 数据访问类D_Lebi_Administrator。
	/// </summary>
	public partial class D_Lebi_Administrator
	{
		static D_Lebi_Administrator _Instance;
		public static D_Lebi_Administrator Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Administrator("Lebi_Administrator");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Administrator";
		public D_Lebi_Administrator(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Administrator", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Administrator", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Administrator" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Administrator", 0 , cachestr,seconds);
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
		public int Add(Lebi_Administrator model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Password")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_AdminStatus")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_Group_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ModilePhone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sex")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("AdminType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_ids")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RandNum")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Avatar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Project_ids")+")");
			strSql.Append(" values (");
			strSql.Append("@UserName,@Password,@IP_Last,@IP_This,@Time_Add,@Time_This,@Time_Last,@Count_Login,@Type_id_AdminStatus,@Admin_Group_id,@RealName,@ModilePhone,@Phone,@Email,@Sex,@AdminType,@Site_ids,@Pro_Type_ids,@RandNum,@Avatar,@Project_ids);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Type_id_AdminStatus", model.Type_id_AdminStatus),
					new SqlParameter("@Admin_Group_id", model.Admin_Group_id),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@ModilePhone", model.ModilePhone),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Sex", model.Sex),
					new SqlParameter("@AdminType", model.AdminType),
					new SqlParameter("@Site_ids", model.Site_ids),
					new SqlParameter("@Pro_Type_ids", model.Pro_Type_ids),
					new SqlParameter("@RandNum", model.RandNum),
					new SqlParameter("@Avatar", model.Avatar),
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
		public void Update(Lebi_Administrator model)
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
			if(upcols.Contains("username") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("UserName")+"= @UserName");
			if(upcols.Contains("password") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Password")+"= @Password");
			if(upcols.Contains("ip_last") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_Last")+"= @IP_Last");
			if(upcols.Contains("ip_this") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP_This")+"= @IP_This");
			if(upcols.Contains("time_add") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if(upcols.Contains("time_this") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_This")+"= @Time_This");
			if(upcols.Contains("time_last") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Last")+"= @Time_Last");
			if(upcols.Contains("count_login") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Count_Login")+"= @Count_Login");
			if(upcols.Contains("type_id_adminstatus") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_AdminStatus")+"= @Type_id_AdminStatus");
			if(upcols.Contains("admin_group_id") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_Group_id")+"= @Admin_Group_id");
			if(upcols.Contains("realname") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RealName")+"= @RealName");
			if(upcols.Contains("modilephone") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ModilePhone")+"= @ModilePhone");
			if(upcols.Contains("phone") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+"= @Phone");
			if(upcols.Contains("email") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if(upcols.Contains("sex") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sex")+"= @Sex");
			if(upcols.Contains("admintype") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("AdminType")+"= @AdminType");
			if(upcols.Contains("site_ids") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Site_ids")+"= @Site_ids");
			if(upcols.Contains("pro_type_ids") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Pro_Type_ids")+"= @Pro_Type_ids");
			if(upcols.Contains("randnum") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RandNum")+"= @RandNum");
			if(upcols.Contains("avatar") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Avatar")+"= @Avatar");
			if(upcols.Contains("project_ids") || model.UpdateCols == "")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Project_ids")+"= @Project_ids");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@Password", model.Password),
					new SqlParameter("@IP_Last", model.IP_Last),
					new SqlParameter("@IP_This", model.IP_This),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_This", model.Time_This),
					new SqlParameter("@Time_Last", model.Time_Last),
					new SqlParameter("@Count_Login", model.Count_Login),
					new SqlParameter("@Type_id_AdminStatus", model.Type_id_AdminStatus),
					new SqlParameter("@Admin_Group_id", model.Admin_Group_id),
					new SqlParameter("@RealName", model.RealName),
					new SqlParameter("@ModilePhone", model.ModilePhone),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Sex", model.Sex),
					new SqlParameter("@AdminType", model.AdminType),
					new SqlParameter("@Site_ids", model.Site_ids),
					new SqlParameter("@Pro_Type_ids", model.Pro_Type_ids),
					new SqlParameter("@RandNum", model.RandNum),
					new SqlParameter("@Avatar", model.Avatar),
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
		public Lebi_Administrator GetModel(int id, string strFieldShow, int seconds=0)
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
		           return obj as Lebi_Administrator;
		   }
		   Lebi_Administrator model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader,strFieldShow);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Administrator",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Administrator GetModel(string strWhere, string strFieldShow, int seconds=0)
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
		           return obj as Lebi_Administrator;
		   }
		   Lebi_Administrator model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader,strFieldShow);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Administrator",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Administrator GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Administrator;
		   }
		   Lebi_Administrator model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader,strFieldShow);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Administrator",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Administrator> GetList(string strWhere, string strFieldShow, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Administrator>;
		   }
		   List<Lebi_Administrator> list = new List<Lebi_Administrator>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Administrator", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Administrator> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Administrator>;
		   }
		   List<Lebi_Administrator> list = new List<Lebi_Administrator>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Administrator", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Administrator> GetList(string strWhere, string strFieldShow, string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Administrator>;
		   }
		   List<Lebi_Administrator> list = new List<Lebi_Administrator>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Administrator", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Administrator> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Administrator>;
		   }
		   List<Lebi_Administrator> list = new List<Lebi_Administrator>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Administrator", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Administrator BindForm(Lebi_Administrator model)
		{
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestString("UserName");
			if (HttpContext.Current.Request["Password"] != null)
				model.Password=LB.Tools.RequestTool.RequestString("Password");
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestString("IP_This");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Type_id_AdminStatus"] != null)
				model.Type_id_AdminStatus=LB.Tools.RequestTool.RequestInt("Type_id_AdminStatus",0);
			if (HttpContext.Current.Request["Admin_Group_id"] != null)
				model.Admin_Group_id=LB.Tools.RequestTool.RequestInt("Admin_Group_id",0);
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestString("RealName");
			if (HttpContext.Current.Request["ModilePhone"] != null)
				model.ModilePhone=LB.Tools.RequestTool.RequestString("ModilePhone");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestString("Phone");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["Sex"] != null)
				model.Sex=LB.Tools.RequestTool.RequestString("Sex");
			if (HttpContext.Current.Request["AdminType"] != null)
				model.AdminType=LB.Tools.RequestTool.RequestString("AdminType");
			if (HttpContext.Current.Request["Site_ids"] != null)
				model.Site_ids=LB.Tools.RequestTool.RequestString("Site_ids");
			if (HttpContext.Current.Request["Pro_Type_ids"] != null)
				model.Pro_Type_ids=LB.Tools.RequestTool.RequestString("Pro_Type_ids");
			if (HttpContext.Current.Request["RandNum"] != null)
				model.RandNum=LB.Tools.RequestTool.RequestInt("RandNum",0);
			if (HttpContext.Current.Request["Avatar"] != null)
				model.Avatar=LB.Tools.RequestTool.RequestString("Avatar");
			if (HttpContext.Current.Request["Project_ids"] != null)
				model.Project_ids=LB.Tools.RequestTool.RequestString("Project_ids");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Administrator SafeBindForm(Lebi_Administrator model)
		{
			if (HttpContext.Current.Request["UserName"] != null)
				model.UserName=LB.Tools.RequestTool.RequestSafeString("UserName");
			if (HttpContext.Current.Request["Password"] != null)
				model.Password=LB.Tools.RequestTool.RequestSafeString("Password");
			if (HttpContext.Current.Request["IP_Last"] != null)
				model.IP_Last=LB.Tools.RequestTool.RequestSafeString("IP_Last");
			if (HttpContext.Current.Request["IP_This"] != null)
				model.IP_This=LB.Tools.RequestTool.RequestSafeString("IP_This");
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_This"] != null)
				model.Time_This=LB.Tools.RequestTool.RequestTime("Time_This", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Last"] != null)
				model.Time_Last=LB.Tools.RequestTool.RequestTime("Time_Last", System.DateTime.Now);
			if (HttpContext.Current.Request["Count_Login"] != null)
				model.Count_Login=LB.Tools.RequestTool.RequestInt("Count_Login",0);
			if (HttpContext.Current.Request["Type_id_AdminStatus"] != null)
				model.Type_id_AdminStatus=LB.Tools.RequestTool.RequestInt("Type_id_AdminStatus",0);
			if (HttpContext.Current.Request["Admin_Group_id"] != null)
				model.Admin_Group_id=LB.Tools.RequestTool.RequestInt("Admin_Group_id",0);
			if (HttpContext.Current.Request["RealName"] != null)
				model.RealName=LB.Tools.RequestTool.RequestSafeString("RealName");
			if (HttpContext.Current.Request["ModilePhone"] != null)
				model.ModilePhone=LB.Tools.RequestTool.RequestSafeString("ModilePhone");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestSafeString("Phone");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["Sex"] != null)
				model.Sex=LB.Tools.RequestTool.RequestSafeString("Sex");
			if (HttpContext.Current.Request["AdminType"] != null)
				model.AdminType=LB.Tools.RequestTool.RequestSafeString("AdminType");
			if (HttpContext.Current.Request["Site_ids"] != null)
				model.Site_ids=LB.Tools.RequestTool.RequestSafeString("Site_ids");
			if (HttpContext.Current.Request["Pro_Type_ids"] != null)
				model.Pro_Type_ids=LB.Tools.RequestTool.RequestSafeString("Pro_Type_ids");
			if (HttpContext.Current.Request["RandNum"] != null)
				model.RandNum=LB.Tools.RequestTool.RequestInt("RandNum",0);
			if (HttpContext.Current.Request["Avatar"] != null)
				model.Avatar=LB.Tools.RequestTool.RequestSafeString("Avatar");
			if (HttpContext.Current.Request["Project_ids"] != null)
				model.Project_ids=LB.Tools.RequestTool.RequestSafeString("Project_ids");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Administrator ReaderBind(IDataReader dataReader, string strFieldShow)
		{
			Lebi_Administrator model=new Lebi_Administrator();
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
			if(isall || upcols.Contains("username"))
			{
				ojb = dataReader["UserName"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.UserName=dataReader["UserName"].ToString();
				}
			}
			if(isall || upcols.Contains("password"))
			{
				ojb = dataReader["Password"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Password=dataReader["Password"].ToString();
				}
			}
			if(isall || upcols.Contains("ip_last"))
			{
				ojb = dataReader["IP_Last"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IP_Last=dataReader["IP_Last"].ToString();
				}
			}
			if(isall || upcols.Contains("ip_this"))
			{
				ojb = dataReader["IP_This"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.IP_This=dataReader["IP_This"].ToString();
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
			if(isall || upcols.Contains("time_this"))
			{
				ojb = dataReader["Time_This"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_This=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("time_last"))
			{
				ojb = dataReader["Time_Last"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Time_Last=(DateTime)ojb;
				}
			}
			if(isall || upcols.Contains("count_login"))
			{
				ojb = dataReader["Count_Login"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Count_Login= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("type_id_adminstatus"))
			{
				ojb = dataReader["Type_id_AdminStatus"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Type_id_AdminStatus= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("admin_group_id"))
			{
				ojb = dataReader["Admin_Group_id"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Admin_Group_id= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("realname"))
			{
				ojb = dataReader["RealName"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.RealName=dataReader["RealName"].ToString();
				}
			}
			if(isall || upcols.Contains("modilephone"))
			{
				ojb = dataReader["ModilePhone"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.ModilePhone=dataReader["ModilePhone"].ToString();
				}
			}
			if(isall || upcols.Contains("phone"))
			{
				ojb = dataReader["Phone"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Phone=dataReader["Phone"].ToString();
				}
			}
			if(isall || upcols.Contains("email"))
			{
				ojb = dataReader["Email"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Email=dataReader["Email"].ToString();
				}
			}
			if(isall || upcols.Contains("sex"))
			{
				ojb = dataReader["Sex"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Sex=dataReader["Sex"].ToString();
				}
			}
			if(isall || upcols.Contains("admintype"))
			{
				ojb = dataReader["AdminType"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.AdminType=dataReader["AdminType"].ToString();
				}
			}
			if(isall || upcols.Contains("site_ids"))
			{
				ojb = dataReader["Site_ids"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Site_ids=dataReader["Site_ids"].ToString();
				}
			}
			if(isall || upcols.Contains("pro_type_ids"))
			{
				ojb = dataReader["Pro_Type_ids"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Pro_Type_ids=dataReader["Pro_Type_ids"].ToString();
				}
			}
			if(isall || upcols.Contains("randnum"))
			{
				ojb = dataReader["RandNum"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.RandNum= Convert.ToInt32(ojb);
				}
			}
			if(isall || upcols.Contains("avatar"))
			{
				ojb = dataReader["Avatar"];
				if(ojb != null && ojb != DBNull.Value)
				{
				  model.Avatar=dataReader["Avatar"].ToString();
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

