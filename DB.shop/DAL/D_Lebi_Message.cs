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
	/// 数据访问类D_Lebi_Message。
	/// </summary>
	public partial class D_Lebi_Message
	{
		static D_Lebi_Message _Instance;
		public static D_Lebi_Message Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Message("Lebi_Message");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Message";
		public D_Lebi_Message(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Message", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Message", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Message" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Message", 0 , cachestr,seconds);
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
		public int Add(Lebi_Message model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IP")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRead")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSystem")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Message_Type_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Parentid")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id_From")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id_To")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_Name_From")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("User_Name_To")+")");
			strSql.Append(" values (");
			strSql.Append("@Content,@DT_id,@IP,@IsRead,@IsSystem,@Language,@Message_Type_id,@Parentid,@Supplier_id,@Time_Add,@Title,@User_id_From,@User_id_To,@User_Name_From,@User_Name_To);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@IP", model.IP),
					new SqlParameter("@IsRead", model.IsRead),
					new SqlParameter("@IsSystem", model.IsSystem),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Message_Type_id", model.Message_Type_id),
					new SqlParameter("@Parentid", model.Parentid),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@User_id_From", model.User_id_From),
					new SqlParameter("@User_id_To", model.User_id_To),
					new SqlParameter("@User_Name_From", model.User_Name_From),
					new SqlParameter("@User_Name_To", model.User_Name_To)};

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
		public void Update(Lebi_Message model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Content,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Content")+"= @Content");
			if((","+model.UpdateCols+",").IndexOf(",DT_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("DT_id")+"= @DT_id");
			if((","+model.UpdateCols+",").IndexOf(",IP,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IP")+"= @IP");
			if((","+model.UpdateCols+",").IndexOf(",IsRead,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsRead")+"= @IsRead");
			if((","+model.UpdateCols+",").IndexOf(",IsSystem,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsSystem")+"= @IsSystem");
			if((","+model.UpdateCols+",").IndexOf(",Language,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Language")+"= @Language");
			if((","+model.UpdateCols+",").IndexOf(",Message_Type_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Message_Type_id")+"= @Message_Type_id");
			if((","+model.UpdateCols+",").IndexOf(",Parentid,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Parentid")+"= @Parentid");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",Time_Add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Add")+"= @Time_Add");
			if((","+model.UpdateCols+",").IndexOf(",Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+"= @Title");
			if((","+model.UpdateCols+",").IndexOf(",User_id_From,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id_From")+"= @User_id_From");
			if((","+model.UpdateCols+",").IndexOf(",User_id_To,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_id_To")+"= @User_id_To");
			if((","+model.UpdateCols+",").IndexOf(",User_Name_From,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_Name_From")+"= @User_Name_From");
			if((","+model.UpdateCols+",").IndexOf(",User_Name_To,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("User_Name_To")+"= @User_Name_To");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Content", model.Content),
					new SqlParameter("@DT_id", model.DT_id),
					new SqlParameter("@IP", model.IP),
					new SqlParameter("@IsRead", model.IsRead),
					new SqlParameter("@IsSystem", model.IsSystem),
					new SqlParameter("@Language", model.Language),
					new SqlParameter("@Message_Type_id", model.Message_Type_id),
					new SqlParameter("@Parentid", model.Parentid),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Time_Add", model.Time_Add),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@User_id_From", model.User_id_From),
					new SqlParameter("@User_id_To", model.User_id_To),
					new SqlParameter("@User_Name_From", model.User_Name_From),
					new SqlParameter("@User_Name_To", model.User_Name_To)};
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
		public Lebi_Message GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Message;
		   }
		   Lebi_Message model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Message",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Message GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Message;
		   }
		   Lebi_Message model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Message",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Message GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Message;
		   }
		   Lebi_Message model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Message",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Message> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Message>;
		   }
		   List<Lebi_Message> list = new List<Lebi_Message>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Message", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Message> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Message>;
		   }
		   List<Lebi_Message> list = new List<Lebi_Message>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Message", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Message> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Message>;
		   }
		   List<Lebi_Message> list = new List<Lebi_Message>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Message", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Message> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Message>;
		   }
		   List<Lebi_Message> list = new List<Lebi_Message>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Message", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Message BindForm(Lebi_Message model)
		{
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestString("Content");
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["IP"] != null)
				model.IP=LB.Tools.RequestTool.RequestString("IP");
			if (HttpContext.Current.Request["IsRead"] != null)
				model.IsRead=LB.Tools.RequestTool.RequestInt("IsRead",0);
			if (HttpContext.Current.Request["IsSystem"] != null)
				model.IsSystem=LB.Tools.RequestTool.RequestInt("IsSystem",0);
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestString("Language");
			if (HttpContext.Current.Request["Message_Type_id"] != null)
				model.Message_Type_id=LB.Tools.RequestTool.RequestInt("Message_Type_id",0);
			if (HttpContext.Current.Request["Parentid"] != null)
				model.Parentid=LB.Tools.RequestTool.RequestInt("Parentid",0);
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestString("Title");
			if (HttpContext.Current.Request["User_id_From"] != null)
				model.User_id_From=LB.Tools.RequestTool.RequestInt("User_id_From",0);
			if (HttpContext.Current.Request["User_id_To"] != null)
				model.User_id_To=LB.Tools.RequestTool.RequestInt("User_id_To",0);
			if (HttpContext.Current.Request["User_Name_From"] != null)
				model.User_Name_From=LB.Tools.RequestTool.RequestString("User_Name_From");
			if (HttpContext.Current.Request["User_Name_To"] != null)
				model.User_Name_To=LB.Tools.RequestTool.RequestString("User_Name_To");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Message SafeBindForm(Lebi_Message model)
		{
			if (HttpContext.Current.Request["Content"] != null)
				model.Content=LB.Tools.RequestTool.RequestSafeString("Content");
			if (HttpContext.Current.Request["DT_id"] != null)
				model.DT_id=LB.Tools.RequestTool.RequestInt("DT_id",0);
			if (HttpContext.Current.Request["IP"] != null)
				model.IP=LB.Tools.RequestTool.RequestSafeString("IP");
			if (HttpContext.Current.Request["IsRead"] != null)
				model.IsRead=LB.Tools.RequestTool.RequestInt("IsRead",0);
			if (HttpContext.Current.Request["IsSystem"] != null)
				model.IsSystem=LB.Tools.RequestTool.RequestInt("IsSystem",0);
			if (HttpContext.Current.Request["Language"] != null)
				model.Language=LB.Tools.RequestTool.RequestSafeString("Language");
			if (HttpContext.Current.Request["Message_Type_id"] != null)
				model.Message_Type_id=LB.Tools.RequestTool.RequestInt("Message_Type_id",0);
			if (HttpContext.Current.Request["Parentid"] != null)
				model.Parentid=LB.Tools.RequestTool.RequestInt("Parentid",0);
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Time_Add"] != null)
				model.Time_Add=LB.Tools.RequestTool.RequestTime("Time_Add", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestSafeString("Title");
			if (HttpContext.Current.Request["User_id_From"] != null)
				model.User_id_From=LB.Tools.RequestTool.RequestInt("User_id_From",0);
			if (HttpContext.Current.Request["User_id_To"] != null)
				model.User_id_To=LB.Tools.RequestTool.RequestInt("User_id_To",0);
			if (HttpContext.Current.Request["User_Name_From"] != null)
				model.User_Name_From=LB.Tools.RequestTool.RequestSafeString("User_Name_From");
			if (HttpContext.Current.Request["User_Name_To"] != null)
				model.User_Name_To=LB.Tools.RequestTool.RequestSafeString("User_Name_To");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Message ReaderBind(IDataReader dataReader)
		{
			Lebi_Message model=new Lebi_Message();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Content=dataReader["Content"].ToString();
			ojb = dataReader["DT_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.DT_id= Convert.ToInt32(ojb);
			}
			model.IP=dataReader["IP"].ToString();
			ojb = dataReader["IsRead"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsRead= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsSystem"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsSystem= Convert.ToInt32(ojb);
			}
			model.Language=dataReader["Language"].ToString();
			ojb = dataReader["Message_Type_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Message_Type_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Parentid"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Parentid= Convert.ToInt32(ojb);
			}
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
			model.Title=dataReader["Title"].ToString();
			ojb = dataReader["User_id_From"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id_From= Convert.ToInt32(ojb);
			}
			ojb = dataReader["User_id_To"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.User_id_To= Convert.ToInt32(ojb);
			}
			model.User_Name_From=dataReader["User_Name_From"].ToString();
			model.User_Name_To=dataReader["User_Name_To"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

