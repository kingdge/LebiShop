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
	/// 数据访问类D_Lebi_Promotion。
	/// </summary>
	public partial class D_Lebi_Promotion
	{
		static D_Lebi_Promotion _Instance;
		public static D_Lebi_Promotion Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Promotion("Lebi_Promotion");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Promotion";
		public D_Lebi_Promotion(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Promotion", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Promotion", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Promotion" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Promotion", 0 , cachestr,seconds);
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
		public int Add(Lebi_Promotion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_UserName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Case801")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Case802")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Case803")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Case804")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Case805")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Case806")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase801")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase802")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase803")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase804")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase805")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase806")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule901")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule902")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule903")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule904")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule905")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule906")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule907")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule908")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule909")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule910")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule911")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule912")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSetCase")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSetRule")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Promotion_Type_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule901")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule902")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule903")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule904")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule905")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule906")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule907")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule908")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule909")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule910")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule911")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule912")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Start")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_PromotionStatus")+")");
			strSql.Append(" values (");
			strSql.Append("@Admin_id,@Admin_UserName,@Case801,@Case802,@Case803,@Case804,@Case805,@Case806,@IsCase801,@IsCase802,@IsCase803,@IsCase804,@IsCase805,@IsCase806,@IsRule901,@IsRule902,@IsRule903,@IsRule904,@IsRule905,@IsRule906,@IsRule907,@IsRule908,@IsRule909,@IsRule910,@IsRule911,@IsRule912,@IsSetCase,@IsSetRule,@Promotion_Type_id,@Remark,@Rule901,@Rule902,@Rule903,@Rule904,@Rule905,@Rule906,@Rule907,@Rule908,@Rule909,@Rule910,@Rule911,@Rule912,@Sort,@Time_Add,@Time_End,@Time_Start,@Time_Update,@Type_id_PromotionStatus);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@Admin_UserName", model.Admin_UserName),
					new SqlParameter("@Case801", model.Case801),
					new SqlParameter("@Case802", model.Case802),
					new SqlParameter("@Case803", model.Case803),
					new SqlParameter("@Case804", model.Case804),
					new SqlParameter("@Case805", model.Case805),
					new SqlParameter("@Case806", model.Case806),
					new SqlParameter("@IsCase801", model.IsCase801),
					new SqlParameter("@IsCase802", model.IsCase802),
					new SqlParameter("@IsCase803", model.IsCase803),
					new SqlParameter("@IsCase804", model.IsCase804),
					new SqlParameter("@IsCase805", model.IsCase805),
					new SqlParameter("@IsCase806", model.IsCase806),
					new SqlParameter("@IsRule901", model.IsRule901),
					new SqlParameter("@IsRule902", model.IsRule902),
					new SqlParameter("@IsRule903", model.IsRule903),
					new SqlParameter("@IsRule904", model.IsRule904),
					new SqlParameter("@IsRule905", model.IsRule905),
					new SqlParameter("@IsRule906", model.IsRule906),
					new SqlParameter("@IsRule907", model.IsRule907),
					new SqlParameter("@IsRule908", model.IsRule908),
					new SqlParameter("@IsRule909", model.IsRule909),
					new SqlParameter("@IsRule910", model.IsRule910),
					new SqlParameter("@IsRule911", model.IsRule911),
					new SqlParameter("@IsRule912", model.IsRule912),
					new SqlParameter("@IsSetCase", model.IsSetCase),
					new SqlParameter("@IsSetRule", model.IsSetRule),
					new SqlParameter("@Promotion_Type_id", model.Promotion_Type_id),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Rule901", model.Rule901),
					new SqlParameter("@Rule902", model.Rule902),
					new SqlParameter("@Rule903", model.Rule903),
					new SqlParameter("@Rule904", model.Rule904),
					new SqlParameter("@Rule905", model.Rule905),
					new SqlParameter("@Rule906", model.Rule906),
					new SqlParameter("@Rule907", model.Rule907),
					new SqlParameter("@Rule908", model.Rule908),
					new SqlParameter("@Rule909", model.Rule909),
					new SqlParameter("@Rule910", model.Rule910),
					new SqlParameter("@Rule911", model.Rule911),
					new SqlParameter("@Rule912", model.Rule912),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Start", model.Time_Start),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Type_id_PromotionStatus", model.Type_id_PromotionStatus)};

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
		public void Update(Lebi_Promotion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Admin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_id")+"= @Admin_id");
			if((","+model.UpdateCols+",").IndexOf(",Admin_UserName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Admin_UserName")+"= @Admin_UserName");
			if((","+model.UpdateCols+",").IndexOf(",Case801,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Case801")+"= @Case801");
			if((","+model.UpdateCols+",").IndexOf(",Case802,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Case802")+"= @Case802");
			if((","+model.UpdateCols+",").IndexOf(",Case803,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Case803")+"= @Case803");
			if((","+model.UpdateCols+",").IndexOf(",Case804,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Case804")+"= @Case804");
			if((","+model.UpdateCols+",").IndexOf(",Case805,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Case805")+"= @Case805");
			if((","+model.UpdateCols+",").IndexOf(",Case806,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Case806")+"= @Case806");
			if((","+model.UpdateCols+",").IndexOf(",IsCase801,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase801")+"= @IsCase801");
			if((","+model.UpdateCols+",").IndexOf(",IsCase802,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase802")+"= @IsCase802");
			if((","+model.UpdateCols+",").IndexOf(",IsCase803,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase803")+"= @IsCase803");
			if((","+model.UpdateCols+",").IndexOf(",IsCase804,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase804")+"= @IsCase804");
			if((","+model.UpdateCols+",").IndexOf(",IsCase805,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase805")+"= @IsCase805");
			if((","+model.UpdateCols+",").IndexOf(",IsCase806,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCase806")+"= @IsCase806");
			if((","+model.UpdateCols+",").IndexOf(",IsRule901,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule901")+"= @IsRule901");
			if((","+model.UpdateCols+",").IndexOf(",IsRule902,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule902")+"= @IsRule902");
			if((","+model.UpdateCols+",").IndexOf(",IsRule903,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule903")+"= @IsRule903");
			if((","+model.UpdateCols+",").IndexOf(",IsRule904,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule904")+"= @IsRule904");
			if((","+model.UpdateCols+",").IndexOf(",IsRule905,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule905")+"= @IsRule905");
			if((","+model.UpdateCols+",").IndexOf(",IsRule906,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule906")+"= @IsRule906");
			if((","+model.UpdateCols+",").IndexOf(",IsRule907,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule907")+"= @IsRule907");
			if((","+model.UpdateCols+",").IndexOf(",IsRule908,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule908")+"= @IsRule908");
			if((","+model.UpdateCols+",").IndexOf(",IsRule909,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule909")+"= @IsRule909");
			if((","+model.UpdateCols+",").IndexOf(",IsRule910,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule910")+"= @IsRule910");
			if((","+model.UpdateCols+",").IndexOf(",IsRule911,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule911")+"= @IsRule911");
			if((","+model.UpdateCols+",").IndexOf(",IsRule912,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRule912")+"= @IsRule912");
			if((","+model.UpdateCols+",").IndexOf(",IsSetCase,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSetCase")+"= @IsSetCase");
			if((","+model.UpdateCols+",").IndexOf(",IsSetRule,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSetRule")+"= @IsSetRule");
			if((","+model.UpdateCols+",").IndexOf(",Promotion_Type_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Promotion_Type_id")+"= @Promotion_Type_id");
			if((","+model.UpdateCols+",").IndexOf(",Remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Remark")+"= @Remark");
			if((","+model.UpdateCols+",").IndexOf(",Rule901,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule901")+"= @Rule901");
			if((","+model.UpdateCols+",").IndexOf(",Rule902,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule902")+"= @Rule902");
			if((","+model.UpdateCols+",").IndexOf(",Rule903,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule903")+"= @Rule903");
			if((","+model.UpdateCols+",").IndexOf(",Rule904,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule904")+"= @Rule904");
			if((","+model.UpdateCols+",").IndexOf(",Rule905,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule905")+"= @Rule905");
			if((","+model.UpdateCols+",").IndexOf(",Rule906,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule906")+"= @Rule906");
			if((","+model.UpdateCols+",").IndexOf(",Rule907,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule907")+"= @Rule907");
			if((","+model.UpdateCols+",").IndexOf(",Rule908,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule908")+"= @Rule908");
			if((","+model.UpdateCols+",").IndexOf(",Rule909,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule909")+"= @Rule909");
			if((","+model.UpdateCols+",").IndexOf(",Rule910,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule910")+"= @Rule910");
			if((","+model.UpdateCols+",").IndexOf(",Rule911,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule911")+"= @Rule911");
			if((","+model.UpdateCols+",").IndexOf(",Rule912,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Rule912")+"= @Rule912");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Time_End,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_End")+"= @Time_End");
			if((","+model.UpdateCols+",").IndexOf(",Time_Start,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Start")+"= @Time_Start");
			if((","+model.UpdateCols+",").IndexOf(",Time_Update,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Update")+"= @Time_Update");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_PromotionStatus,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_PromotionStatus")+"= @Type_id_PromotionStatus");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Admin_id", model.Admin_id),
					new SqlParameter("@Admin_UserName", model.Admin_UserName),
					new SqlParameter("@Case801", model.Case801),
					new SqlParameter("@Case802", model.Case802),
					new SqlParameter("@Case803", model.Case803),
					new SqlParameter("@Case804", model.Case804),
					new SqlParameter("@Case805", model.Case805),
					new SqlParameter("@Case806", model.Case806),
					new SqlParameter("@IsCase801", model.IsCase801),
					new SqlParameter("@IsCase802", model.IsCase802),
					new SqlParameter("@IsCase803", model.IsCase803),
					new SqlParameter("@IsCase804", model.IsCase804),
					new SqlParameter("@IsCase805", model.IsCase805),
					new SqlParameter("@IsCase806", model.IsCase806),
					new SqlParameter("@IsRule901", model.IsRule901),
					new SqlParameter("@IsRule902", model.IsRule902),
					new SqlParameter("@IsRule903", model.IsRule903),
					new SqlParameter("@IsRule904", model.IsRule904),
					new SqlParameter("@IsRule905", model.IsRule905),
					new SqlParameter("@IsRule906", model.IsRule906),
					new SqlParameter("@IsRule907", model.IsRule907),
					new SqlParameter("@IsRule908", model.IsRule908),
					new SqlParameter("@IsRule909", model.IsRule909),
					new SqlParameter("@IsRule910", model.IsRule910),
					new SqlParameter("@IsRule911", model.IsRule911),
					new SqlParameter("@IsRule912", model.IsRule912),
					new SqlParameter("@IsSetCase", model.IsSetCase),
					new SqlParameter("@IsSetRule", model.IsSetRule),
					new SqlParameter("@Promotion_Type_id", model.Promotion_Type_id),
					new SqlParameter("@Remark", model.Remark),
					new SqlParameter("@Rule901", model.Rule901),
					new SqlParameter("@Rule902", model.Rule902),
					new SqlParameter("@Rule903", model.Rule903),
					new SqlParameter("@Rule904", model.Rule904),
					new SqlParameter("@Rule905", model.Rule905),
					new SqlParameter("@Rule906", model.Rule906),
					new SqlParameter("@Rule907", model.Rule907),
					new SqlParameter("@Rule908", model.Rule908),
					new SqlParameter("@Rule909", model.Rule909),
					new SqlParameter("@Rule910", model.Rule910),
					new SqlParameter("@Rule911", model.Rule911),
					new SqlParameter("@Rule912", model.Rule912),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Time_End", model.Time_End),
					new SqlParameter("@Time_Start", model.Time_Start),
					new SqlParameter("@Time_Update", model.Time_Update),
					new SqlParameter("@Type_id_PromotionStatus", model.Type_id_PromotionStatus)};
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
		public Lebi_Promotion GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Promotion;
		   }
		   Lebi_Promotion model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Promotion",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Promotion GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Promotion;
		   }
		   Lebi_Promotion model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Promotion",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Promotion GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Promotion;
		   }
		   Lebi_Promotion model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Promotion",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Promotion> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Promotion>;
		   }
		   List<Lebi_Promotion> list = new List<Lebi_Promotion>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Promotion", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Promotion> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Promotion>;
		   }
		   List<Lebi_Promotion> list = new List<Lebi_Promotion>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Promotion", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Promotion> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Promotion>;
		   }
		   List<Lebi_Promotion> list = new List<Lebi_Promotion>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Promotion", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Promotion> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Promotion>;
		   }
		   List<Lebi_Promotion> list = new List<Lebi_Promotion>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Promotion", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Promotion BindForm(Lebi_Promotion model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["Admin_UserName"] != null)
				model.Admin_UserName=LB.Tools.RequestTool.RequestString("Admin_UserName");
			if (HttpContext.Current.Request["Case801"] != null)
				model.Case801=LB.Tools.RequestTool.RequestInt("Case801",0);
			if (HttpContext.Current.Request["Case802"] != null)
				model.Case802=LB.Tools.RequestTool.RequestInt("Case802",0);
			if (HttpContext.Current.Request["Case803"] != null)
				model.Case803=LB.Tools.RequestTool.RequestInt("Case803",0);
			if (HttpContext.Current.Request["Case804"] != null)
				model.Case804=LB.Tools.RequestTool.RequestString("Case804");
			if (HttpContext.Current.Request["Case805"] != null)
				model.Case805=LB.Tools.RequestTool.RequestString("Case805");
			if (HttpContext.Current.Request["Case806"] != null)
				model.Case806=LB.Tools.RequestTool.RequestInt("Case806",0);
			if (HttpContext.Current.Request["IsCase801"] != null)
				model.IsCase801=LB.Tools.RequestTool.RequestInt("IsCase801",0);
			if (HttpContext.Current.Request["IsCase802"] != null)
				model.IsCase802=LB.Tools.RequestTool.RequestInt("IsCase802",0);
			if (HttpContext.Current.Request["IsCase803"] != null)
				model.IsCase803=LB.Tools.RequestTool.RequestInt("IsCase803",0);
			if (HttpContext.Current.Request["IsCase804"] != null)
				model.IsCase804=LB.Tools.RequestTool.RequestInt("IsCase804",0);
			if (HttpContext.Current.Request["IsCase805"] != null)
				model.IsCase805=LB.Tools.RequestTool.RequestInt("IsCase805",0);
			if (HttpContext.Current.Request["IsCase806"] != null)
				model.IsCase806=LB.Tools.RequestTool.RequestInt("IsCase806",0);
			if (HttpContext.Current.Request["IsRule901"] != null)
				model.IsRule901=LB.Tools.RequestTool.RequestInt("IsRule901",0);
			if (HttpContext.Current.Request["IsRule902"] != null)
				model.IsRule902=LB.Tools.RequestTool.RequestInt("IsRule902",0);
			if (HttpContext.Current.Request["IsRule903"] != null)
				model.IsRule903=LB.Tools.RequestTool.RequestInt("IsRule903",0);
			if (HttpContext.Current.Request["IsRule904"] != null)
				model.IsRule904=LB.Tools.RequestTool.RequestInt("IsRule904",0);
			if (HttpContext.Current.Request["IsRule905"] != null)
				model.IsRule905=LB.Tools.RequestTool.RequestInt("IsRule905",0);
			if (HttpContext.Current.Request["IsRule906"] != null)
				model.IsRule906=LB.Tools.RequestTool.RequestInt("IsRule906",0);
			if (HttpContext.Current.Request["IsRule907"] != null)
				model.IsRule907=LB.Tools.RequestTool.RequestInt("IsRule907",0);
			if (HttpContext.Current.Request["IsRule908"] != null)
				model.IsRule908=LB.Tools.RequestTool.RequestInt("IsRule908",0);
			if (HttpContext.Current.Request["IsRule909"] != null)
				model.IsRule909=LB.Tools.RequestTool.RequestInt("IsRule909",0);
			if (HttpContext.Current.Request["IsRule910"] != null)
				model.IsRule910=LB.Tools.RequestTool.RequestInt("IsRule910",0);
			if (HttpContext.Current.Request["IsRule911"] != null)
				model.IsRule911=LB.Tools.RequestTool.RequestInt("IsRule911",0);
			if (HttpContext.Current.Request["IsRule912"] != null)
				model.IsRule912=LB.Tools.RequestTool.RequestInt("IsRule912",0);
			if (HttpContext.Current.Request["IsSetCase"] != null)
				model.IsSetCase=LB.Tools.RequestTool.RequestInt("IsSetCase",0);
			if (HttpContext.Current.Request["IsSetRule"] != null)
				model.IsSetRule=LB.Tools.RequestTool.RequestInt("IsSetRule",0);
			if (HttpContext.Current.Request["Promotion_Type_id"] != null)
				model.Promotion_Type_id=LB.Tools.RequestTool.RequestInt("Promotion_Type_id",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestString("Remark");
			if (HttpContext.Current.Request["Rule901"] != null)
				model.Rule901=LB.Tools.RequestTool.RequestInt("Rule901",0);
			if (HttpContext.Current.Request["Rule902"] != null)
				model.Rule902=LB.Tools.RequestTool.RequestInt("Rule902",0);
			if (HttpContext.Current.Request["Rule903"] != null)
				model.Rule903=LB.Tools.RequestTool.RequestInt("Rule903",0);
			if (HttpContext.Current.Request["Rule904"] != null)
				model.Rule904=LB.Tools.RequestTool.RequestInt("Rule904",0);
			if (HttpContext.Current.Request["Rule905"] != null)
				model.Rule905=LB.Tools.RequestTool.RequestInt("Rule905",0);
			if (HttpContext.Current.Request["Rule906"] != null)
				model.Rule906=LB.Tools.RequestTool.RequestInt("Rule906",0);
			if (HttpContext.Current.Request["Rule907"] != null)
				model.Rule907=LB.Tools.RequestTool.RequestInt("Rule907",0);
			if (HttpContext.Current.Request["Rule908"] != null)
				model.Rule908=LB.Tools.RequestTool.RequestInt("Rule908",0);
			if (HttpContext.Current.Request["Rule909"] != null)
				model.Rule909=LB.Tools.RequestTool.RequestString("Rule909");
			if (HttpContext.Current.Request["Rule910"] != null)
				model.Rule910=LB.Tools.RequestTool.RequestInt("Rule910",0);
			if (HttpContext.Current.Request["Rule911"] != null)
				model.Rule911=LB.Tools.RequestTool.RequestString("Rule911");
			if (HttpContext.Current.Request["Rule912"] != null)
				model.Rule912=LB.Tools.RequestTool.RequestInt("Rule912",0);
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Start"] != null)
				model.Time_Start=LB.Tools.RequestTool.RequestTime("Time_Start", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_PromotionStatus"] != null)
				model.Type_id_PromotionStatus=LB.Tools.RequestTool.RequestInt("Type_id_PromotionStatus",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Promotion SafeBindForm(Lebi_Promotion model)
		{
			if (HttpContext.Current.Request["Admin_id"] != null)
				model.Admin_id=LB.Tools.RequestTool.RequestInt("Admin_id",0);
			if (HttpContext.Current.Request["Admin_UserName"] != null)
				model.Admin_UserName=LB.Tools.RequestTool.RequestSafeString("Admin_UserName");
			if (HttpContext.Current.Request["Case801"] != null)
				model.Case801=LB.Tools.RequestTool.RequestInt("Case801",0);
			if (HttpContext.Current.Request["Case802"] != null)
				model.Case802=LB.Tools.RequestTool.RequestInt("Case802",0);
			if (HttpContext.Current.Request["Case803"] != null)
				model.Case803=LB.Tools.RequestTool.RequestInt("Case803",0);
			if (HttpContext.Current.Request["Case804"] != null)
				model.Case804=LB.Tools.RequestTool.RequestSafeString("Case804");
			if (HttpContext.Current.Request["Case805"] != null)
				model.Case805=LB.Tools.RequestTool.RequestSafeString("Case805");
			if (HttpContext.Current.Request["Case806"] != null)
				model.Case806=LB.Tools.RequestTool.RequestInt("Case806",0);
			if (HttpContext.Current.Request["IsCase801"] != null)
				model.IsCase801=LB.Tools.RequestTool.RequestInt("IsCase801",0);
			if (HttpContext.Current.Request["IsCase802"] != null)
				model.IsCase802=LB.Tools.RequestTool.RequestInt("IsCase802",0);
			if (HttpContext.Current.Request["IsCase803"] != null)
				model.IsCase803=LB.Tools.RequestTool.RequestInt("IsCase803",0);
			if (HttpContext.Current.Request["IsCase804"] != null)
				model.IsCase804=LB.Tools.RequestTool.RequestInt("IsCase804",0);
			if (HttpContext.Current.Request["IsCase805"] != null)
				model.IsCase805=LB.Tools.RequestTool.RequestInt("IsCase805",0);
			if (HttpContext.Current.Request["IsCase806"] != null)
				model.IsCase806=LB.Tools.RequestTool.RequestInt("IsCase806",0);
			if (HttpContext.Current.Request["IsRule901"] != null)
				model.IsRule901=LB.Tools.RequestTool.RequestInt("IsRule901",0);
			if (HttpContext.Current.Request["IsRule902"] != null)
				model.IsRule902=LB.Tools.RequestTool.RequestInt("IsRule902",0);
			if (HttpContext.Current.Request["IsRule903"] != null)
				model.IsRule903=LB.Tools.RequestTool.RequestInt("IsRule903",0);
			if (HttpContext.Current.Request["IsRule904"] != null)
				model.IsRule904=LB.Tools.RequestTool.RequestInt("IsRule904",0);
			if (HttpContext.Current.Request["IsRule905"] != null)
				model.IsRule905=LB.Tools.RequestTool.RequestInt("IsRule905",0);
			if (HttpContext.Current.Request["IsRule906"] != null)
				model.IsRule906=LB.Tools.RequestTool.RequestInt("IsRule906",0);
			if (HttpContext.Current.Request["IsRule907"] != null)
				model.IsRule907=LB.Tools.RequestTool.RequestInt("IsRule907",0);
			if (HttpContext.Current.Request["IsRule908"] != null)
				model.IsRule908=LB.Tools.RequestTool.RequestInt("IsRule908",0);
			if (HttpContext.Current.Request["IsRule909"] != null)
				model.IsRule909=LB.Tools.RequestTool.RequestInt("IsRule909",0);
			if (HttpContext.Current.Request["IsRule910"] != null)
				model.IsRule910=LB.Tools.RequestTool.RequestInt("IsRule910",0);
			if (HttpContext.Current.Request["IsRule911"] != null)
				model.IsRule911=LB.Tools.RequestTool.RequestInt("IsRule911",0);
			if (HttpContext.Current.Request["IsRule912"] != null)
				model.IsRule912=LB.Tools.RequestTool.RequestInt("IsRule912",0);
			if (HttpContext.Current.Request["IsSetCase"] != null)
				model.IsSetCase=LB.Tools.RequestTool.RequestInt("IsSetCase",0);
			if (HttpContext.Current.Request["IsSetRule"] != null)
				model.IsSetRule=LB.Tools.RequestTool.RequestInt("IsSetRule",0);
			if (HttpContext.Current.Request["Promotion_Type_id"] != null)
				model.Promotion_Type_id=LB.Tools.RequestTool.RequestInt("Promotion_Type_id",0);
			if (HttpContext.Current.Request["Remark"] != null)
				model.Remark=LB.Tools.RequestTool.RequestSafeString("Remark");
			if (HttpContext.Current.Request["Rule901"] != null)
				model.Rule901=LB.Tools.RequestTool.RequestInt("Rule901",0);
			if (HttpContext.Current.Request["Rule902"] != null)
				model.Rule902=LB.Tools.RequestTool.RequestInt("Rule902",0);
			if (HttpContext.Current.Request["Rule903"] != null)
				model.Rule903=LB.Tools.RequestTool.RequestInt("Rule903",0);
			if (HttpContext.Current.Request["Rule904"] != null)
				model.Rule904=LB.Tools.RequestTool.RequestInt("Rule904",0);
			if (HttpContext.Current.Request["Rule905"] != null)
				model.Rule905=LB.Tools.RequestTool.RequestInt("Rule905",0);
			if (HttpContext.Current.Request["Rule906"] != null)
				model.Rule906=LB.Tools.RequestTool.RequestInt("Rule906",0);
			if (HttpContext.Current.Request["Rule907"] != null)
				model.Rule907=LB.Tools.RequestTool.RequestInt("Rule907",0);
			if (HttpContext.Current.Request["Rule908"] != null)
				model.Rule908=LB.Tools.RequestTool.RequestInt("Rule908",0);
			if (HttpContext.Current.Request["Rule909"] != null)
				model.Rule909=LB.Tools.RequestTool.RequestSafeString("Rule909");
			if (HttpContext.Current.Request["Rule910"] != null)
				model.Rule910=LB.Tools.RequestTool.RequestInt("Rule910",0);
			if (HttpContext.Current.Request["Rule911"] != null)
				model.Rule911=LB.Tools.RequestTool.RequestSafeString("Rule911");
			if (HttpContext.Current.Request["Rule912"] != null)
				model.Rule912=LB.Tools.RequestTool.RequestInt("Rule912",0);
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_End"] != null)
				model.Time_End=LB.Tools.RequestTool.RequestTime("Time_End", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Start"] != null)
				model.Time_Start=LB.Tools.RequestTool.RequestTime("Time_Start", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Update"] != null)
				model.Time_Update=LB.Tools.RequestTool.RequestTime("Time_Update", System.DateTime.Now);
			if (HttpContext.Current.Request["Type_id_PromotionStatus"] != null)
				model.Type_id_PromotionStatus=LB.Tools.RequestTool.RequestInt("Type_id_PromotionStatus",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Promotion ReaderBind(IDataReader dataReader)
		{
			Lebi_Promotion model=new Lebi_Promotion();
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
			ojb = dataReader["Case801"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Case801= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Case802"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Case802= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Case803"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Case803= Convert.ToInt32(ojb);
			}
			model.Case804=dataReader["Case804"].ToString();
			model.Case805=dataReader["Case805"].ToString();
			ojb = dataReader["Case806"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Case806= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCase801"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCase801= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCase802"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCase802= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCase803"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCase803= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCase804"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCase804= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCase805"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCase805= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCase806"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCase806= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule901"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule901= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule902"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule902= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule903"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule903= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule904"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule904= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule905"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule905= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule906"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule906= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule907"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule907= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule908"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule908= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule909"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule909= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule910"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule910= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule911"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule911= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsRule912"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRule912= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSetCase"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSetCase= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSetRule"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSetRule= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Promotion_Type_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Promotion_Type_id= Convert.ToInt32(ojb);
			}
			model.Remark=dataReader["Remark"].ToString();
			ojb = dataReader["Rule901"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule901= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule902"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule902= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule903"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule903= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule904"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule904= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule905"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule905= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule906"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule906= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule907"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule907= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Rule908"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule908= Convert.ToInt32(ojb);
			}
			model.Rule909=dataReader["Rule909"].ToString();
			ojb = dataReader["Rule910"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule910= Convert.ToInt32(ojb);
			}
			model.Rule911=dataReader["Rule911"].ToString();
			ojb = dataReader["Rule912"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Rule912= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Time_Add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Add=(DateTime)ojb;
			}
			ojb = dataReader["Time_End"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_End=(DateTime)ojb;
			}
			ojb = dataReader["Time_Start"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Start=(DateTime)ojb;
			}
			ojb = dataReader["Time_Update"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Update=(DateTime)ojb;
			}
			ojb = dataReader["Type_id_PromotionStatus"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Type_id_PromotionStatus= Convert.ToInt32(ojb);
			}
			return model;
		}

		#endregion  成员方法
	}
}

