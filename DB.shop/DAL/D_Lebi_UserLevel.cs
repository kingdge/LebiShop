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
	/// 数据访问类D_Lebi_UserLevel。
	/// </summary>
	public partial class D_Lebi_UserLevel
	{
		static D_Lebi_UserLevel _Instance;
		public static D_Lebi_UserLevel Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_UserLevel("Lebi_UserLevel");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_UserLevel";
		public D_Lebi_UserLevel(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_UserLevel", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_UserLevel", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_UserLevel" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_UserLevel", 0 , cachestr,seconds);
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
		public int Add(Lebi_UserLevel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("BuyRight")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Comment")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Grade")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageUrl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsHidePrice")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUsedAgent")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("LisDefault")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("LoginPointAdd")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("LoginPointCut")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Lpoint")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("MoneyToPoint")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OrderSubmit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OrderSubmitCount")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PointToMoney")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("PriceName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("RegisterType")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("remark")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_PriceType")+")");
			strSql.Append(" values (");
			strSql.Append("@BuyRight,@Comment,@Grade,@ImageUrl,@IsHidePrice,@IsUsedAgent,@LisDefault,@LoginPointAdd,@LoginPointCut,@Lpoint,@MoneyToPoint,@Name,@OrderSubmit,@OrderSubmitCount,@PointToMoney,@Price,@PriceName,@RegisterType,@remark,@Type_id_PriceType);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@BuyRight", model.BuyRight),
					new SqlParameter("@Comment", model.Comment),
					new SqlParameter("@Grade", model.Grade),
					new SqlParameter("@ImageUrl", model.ImageUrl),
					new SqlParameter("@IsHidePrice", model.IsHidePrice),
					new SqlParameter("@IsUsedAgent", model.IsUsedAgent),
					new SqlParameter("@LisDefault", model.LisDefault),
					new SqlParameter("@LoginPointAdd", model.LoginPointAdd),
					new SqlParameter("@LoginPointCut", model.LoginPointCut),
					new SqlParameter("@Lpoint", model.Lpoint),
					new SqlParameter("@MoneyToPoint", model.MoneyToPoint),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@OrderSubmit", model.OrderSubmit),
					new SqlParameter("@OrderSubmitCount", model.OrderSubmitCount),
					new SqlParameter("@PointToMoney", model.PointToMoney),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@PriceName", model.PriceName),
					new SqlParameter("@RegisterType", model.RegisterType),
					new SqlParameter("@remark", model.remark),
					new SqlParameter("@Type_id_PriceType", model.Type_id_PriceType)};

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
		public void Update(Lebi_UserLevel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",BuyRight,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("BuyRight")+"= @BuyRight");
			if((","+model.UpdateCols+",").IndexOf(",Comment,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Comment")+"= @Comment");
			if((","+model.UpdateCols+",").IndexOf(",Grade,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Grade")+"= @Grade");
			if((","+model.UpdateCols+",").IndexOf(",ImageUrl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ImageUrl")+"= @ImageUrl");
			if((","+model.UpdateCols+",").IndexOf(",IsHidePrice,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsHidePrice")+"= @IsHidePrice");
			if((","+model.UpdateCols+",").IndexOf(",IsUsedAgent,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsUsedAgent")+"= @IsUsedAgent");
			if((","+model.UpdateCols+",").IndexOf(",LisDefault,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("LisDefault")+"= @LisDefault");
			if((","+model.UpdateCols+",").IndexOf(",LoginPointAdd,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("LoginPointAdd")+"= @LoginPointAdd");
			if((","+model.UpdateCols+",").IndexOf(",LoginPointCut,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("LoginPointCut")+"= @LoginPointCut");
			if((","+model.UpdateCols+",").IndexOf(",Lpoint,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Lpoint")+"= @Lpoint");
			if((","+model.UpdateCols+",").IndexOf(",MoneyToPoint,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("MoneyToPoint")+"= @MoneyToPoint");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",OrderSubmit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OrderSubmit")+"= @OrderSubmit");
			if((","+model.UpdateCols+",").IndexOf(",OrderSubmitCount,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OrderSubmitCount")+"= @OrderSubmitCount");
			if((","+model.UpdateCols+",").IndexOf(",PointToMoney,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PointToMoney")+"= @PointToMoney");
			if((","+model.UpdateCols+",").IndexOf(",Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+"= @Price");
			if((","+model.UpdateCols+",").IndexOf(",PriceName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("PriceName")+"= @PriceName");
			if((","+model.UpdateCols+",").IndexOf(",RegisterType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("RegisterType")+"= @RegisterType");
			if((","+model.UpdateCols+",").IndexOf(",remark,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("remark")+"= @remark");
			if((","+model.UpdateCols+",").IndexOf(",Type_id_PriceType,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Type_id_PriceType")+"= @Type_id_PriceType");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@BuyRight", model.BuyRight),
					new SqlParameter("@Comment", model.Comment),
					new SqlParameter("@Grade", model.Grade),
					new SqlParameter("@ImageUrl", model.ImageUrl),
					new SqlParameter("@IsHidePrice", model.IsHidePrice),
					new SqlParameter("@IsUsedAgent", model.IsUsedAgent),
					new SqlParameter("@LisDefault", model.LisDefault),
					new SqlParameter("@LoginPointAdd", model.LoginPointAdd),
					new SqlParameter("@LoginPointCut", model.LoginPointCut),
					new SqlParameter("@Lpoint", model.Lpoint),
					new SqlParameter("@MoneyToPoint", model.MoneyToPoint),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@OrderSubmit", model.OrderSubmit),
					new SqlParameter("@OrderSubmitCount", model.OrderSubmitCount),
					new SqlParameter("@PointToMoney", model.PointToMoney),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@PriceName", model.PriceName),
					new SqlParameter("@RegisterType", model.RegisterType),
					new SqlParameter("@remark", model.remark),
					new SqlParameter("@Type_id_PriceType", model.Type_id_PriceType)};
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
		public Lebi_UserLevel GetModel(int id, int seconds=0)
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
		           return obj as Lebi_UserLevel;
		   }
		   Lebi_UserLevel model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_UserLevel",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_UserLevel GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_UserLevel;
		   }
		   Lebi_UserLevel model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_UserLevel",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_UserLevel GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_UserLevel;
		   }
		   Lebi_UserLevel model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_UserLevel",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_UserLevel> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_UserLevel>;
		   }
		   List<Lebi_UserLevel> list = new List<Lebi_UserLevel>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_UserLevel", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_UserLevel> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_UserLevel>;
		   }
		   List<Lebi_UserLevel> list = new List<Lebi_UserLevel>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_UserLevel", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_UserLevel> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_UserLevel>;
		   }
		   List<Lebi_UserLevel> list = new List<Lebi_UserLevel>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_UserLevel", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_UserLevel> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_UserLevel>;
		   }
		   List<Lebi_UserLevel> list = new List<Lebi_UserLevel>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_UserLevel", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_UserLevel BindForm(Lebi_UserLevel model)
		{
			if (HttpContext.Current.Request["BuyRight"] != null)
				model.BuyRight=LB.Tools.RequestTool.RequestInt("BuyRight",0);
			if (HttpContext.Current.Request["Comment"] != null)
				model.Comment=LB.Tools.RequestTool.RequestInt("Comment",0);
			if (HttpContext.Current.Request["Grade"] != null)
				model.Grade=LB.Tools.RequestTool.RequestInt("Grade",0);
			if (HttpContext.Current.Request["ImageUrl"] != null)
				model.ImageUrl=LB.Tools.RequestTool.RequestString("ImageUrl");
			if (HttpContext.Current.Request["IsHidePrice"] != null)
				model.IsHidePrice=LB.Tools.RequestTool.RequestInt("IsHidePrice",0);
			if (HttpContext.Current.Request["IsUsedAgent"] != null)
				model.IsUsedAgent=LB.Tools.RequestTool.RequestInt("IsUsedAgent",0);
			if (HttpContext.Current.Request["LisDefault"] != null)
				model.LisDefault=LB.Tools.RequestTool.RequestInt("LisDefault",0);
			if (HttpContext.Current.Request["LoginPointAdd"] != null)
				model.LoginPointAdd=LB.Tools.RequestTool.RequestInt("LoginPointAdd",0);
			if (HttpContext.Current.Request["LoginPointCut"] != null)
				model.LoginPointCut=LB.Tools.RequestTool.RequestInt("LoginPointCut",0);
			if (HttpContext.Current.Request["Lpoint"] != null)
				model.Lpoint=LB.Tools.RequestTool.RequestInt("Lpoint",0);
			if (HttpContext.Current.Request["MoneyToPoint"] != null)
				model.MoneyToPoint=LB.Tools.RequestTool.RequestDecimal("MoneyToPoint",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["OrderSubmit"] != null)
				model.OrderSubmit=LB.Tools.RequestTool.RequestDecimal("OrderSubmit",0);
			if (HttpContext.Current.Request["OrderSubmitCount"] != null)
				model.OrderSubmitCount=LB.Tools.RequestTool.RequestInt("OrderSubmitCount",0);
			if (HttpContext.Current.Request["PointToMoney"] != null)
				model.PointToMoney=LB.Tools.RequestTool.RequestString("PointToMoney");
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["PriceName"] != null)
				model.PriceName=LB.Tools.RequestTool.RequestString("PriceName");
			if (HttpContext.Current.Request["RegisterType"] != null)
				model.RegisterType=LB.Tools.RequestTool.RequestInt("RegisterType",0);
			if (HttpContext.Current.Request["remark"] != null)
				model.remark=LB.Tools.RequestTool.RequestString("remark");
			if (HttpContext.Current.Request["Type_id_PriceType"] != null)
				model.Type_id_PriceType=LB.Tools.RequestTool.RequestString("Type_id_PriceType");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_UserLevel SafeBindForm(Lebi_UserLevel model)
		{
			if (HttpContext.Current.Request["BuyRight"] != null)
				model.BuyRight=LB.Tools.RequestTool.RequestInt("BuyRight",0);
			if (HttpContext.Current.Request["Comment"] != null)
				model.Comment=LB.Tools.RequestTool.RequestInt("Comment",0);
			if (HttpContext.Current.Request["Grade"] != null)
				model.Grade=LB.Tools.RequestTool.RequestInt("Grade",0);
			if (HttpContext.Current.Request["ImageUrl"] != null)
				model.ImageUrl=LB.Tools.RequestTool.RequestSafeString("ImageUrl");
			if (HttpContext.Current.Request["IsHidePrice"] != null)
				model.IsHidePrice=LB.Tools.RequestTool.RequestInt("IsHidePrice",0);
			if (HttpContext.Current.Request["IsUsedAgent"] != null)
				model.IsUsedAgent=LB.Tools.RequestTool.RequestInt("IsUsedAgent",0);
			if (HttpContext.Current.Request["LisDefault"] != null)
				model.LisDefault=LB.Tools.RequestTool.RequestInt("LisDefault",0);
			if (HttpContext.Current.Request["LoginPointAdd"] != null)
				model.LoginPointAdd=LB.Tools.RequestTool.RequestInt("LoginPointAdd",0);
			if (HttpContext.Current.Request["LoginPointCut"] != null)
				model.LoginPointCut=LB.Tools.RequestTool.RequestInt("LoginPointCut",0);
			if (HttpContext.Current.Request["Lpoint"] != null)
				model.Lpoint=LB.Tools.RequestTool.RequestInt("Lpoint",0);
			if (HttpContext.Current.Request["MoneyToPoint"] != null)
				model.MoneyToPoint=LB.Tools.RequestTool.RequestDecimal("MoneyToPoint",0);
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["OrderSubmit"] != null)
				model.OrderSubmit=LB.Tools.RequestTool.RequestDecimal("OrderSubmit",0);
			if (HttpContext.Current.Request["OrderSubmitCount"] != null)
				model.OrderSubmitCount=LB.Tools.RequestTool.RequestInt("OrderSubmitCount",0);
			if (HttpContext.Current.Request["PointToMoney"] != null)
				model.PointToMoney=LB.Tools.RequestTool.RequestSafeString("PointToMoney");
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["PriceName"] != null)
				model.PriceName=LB.Tools.RequestTool.RequestSafeString("PriceName");
			if (HttpContext.Current.Request["RegisterType"] != null)
				model.RegisterType=LB.Tools.RequestTool.RequestInt("RegisterType",0);
			if (HttpContext.Current.Request["remark"] != null)
				model.remark=LB.Tools.RequestTool.RequestSafeString("remark");
			if (HttpContext.Current.Request["Type_id_PriceType"] != null)
				model.Type_id_PriceType=LB.Tools.RequestTool.RequestSafeString("Type_id_PriceType");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_UserLevel ReaderBind(IDataReader dataReader)
		{
			Lebi_UserLevel model=new Lebi_UserLevel();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["BuyRight"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BuyRight= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Comment"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Comment= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Grade"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Grade= Convert.ToInt32(ojb);
			}
			model.ImageUrl=dataReader["ImageUrl"].ToString();
			ojb = dataReader["IsHidePrice"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsHidePrice= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsUsedAgent"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsUsedAgent= Convert.ToInt32(ojb);
			}
			ojb = dataReader["LisDefault"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.LisDefault= Convert.ToInt32(ojb);
			}
			ojb = dataReader["LoginPointAdd"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.LoginPointAdd= Convert.ToInt32(ojb);
			}
			ojb = dataReader["LoginPointCut"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.LoginPointCut= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Lpoint"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Lpoint= Convert.ToInt32(ojb);
			}
			ojb = dataReader["MoneyToPoint"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.MoneyToPoint=(decimal)ojb;
			}
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["OrderSubmit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OrderSubmit=(decimal)ojb;
			}
			ojb = dataReader["OrderSubmitCount"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OrderSubmitCount= Convert.ToInt32(ojb);
			}
			model.PointToMoney=dataReader["PointToMoney"].ToString();
			ojb = dataReader["Price"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price=(decimal)ojb;
			}
			model.PriceName=dataReader["PriceName"].ToString();
			ojb = dataReader["RegisterType"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.RegisterType= Convert.ToInt32(ojb);
			}
			model.remark=dataReader["remark"].ToString();
			model.Type_id_PriceType=dataReader["Type_id_PriceType"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

