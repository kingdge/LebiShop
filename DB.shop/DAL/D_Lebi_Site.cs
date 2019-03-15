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
	/// 数据访问类D_Lebi_Site。
	/// </summary>
	public partial class D_Lebi_Site
	{
		static D_Lebi_Site _Instance;
		public static D_Lebi_Site Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Site("Lebi_Site");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Site";
		public D_Lebi_Site(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Site", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Site", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Site" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Site", 0 , cachestr,seconds);
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
		public int Add(Lebi_Site model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Copyright")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Domain")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Fax")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("FootHtml")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsMobile")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Keywords")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Logoimg")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Path")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_custemtoken")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_image_qrcode")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_number")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_secret")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_subscribe_automsg")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ServiceP")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("SubName")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Edit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+")");
			strSql.Append(" values (");
			strSql.Append("@Copyright,@Description,@Domain,@Email,@Fax,@FootHtml,@IsMobile,@Keywords,@Logoimg,@Name,@Path,@Phone,@platform_weixin_custemtoken,@platform_weixin_id,@platform_weixin_image_qrcode,@platform_weixin_number,@platform_weixin_secret,@platform_weixin_subscribe_automsg,@QQ,@ServiceP,@Sort,@SubName,@Time_add,@Time_Edit,@Title);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Copyright", model.Copyright),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Domain", model.Domain),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Fax", model.Fax),
					new SqlParameter("@FootHtml", model.FootHtml),
					new SqlParameter("@IsMobile", model.IsMobile),
					new SqlParameter("@Keywords", model.Keywords),
					new SqlParameter("@Logoimg", model.Logoimg),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Path", model.Path),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@platform_weixin_custemtoken", model.platform_weixin_custemtoken),
					new SqlParameter("@platform_weixin_id", model.platform_weixin_id),
					new SqlParameter("@platform_weixin_image_qrcode", model.platform_weixin_image_qrcode),
					new SqlParameter("@platform_weixin_number", model.platform_weixin_number),
					new SqlParameter("@platform_weixin_secret", model.platform_weixin_secret),
					new SqlParameter("@platform_weixin_subscribe_automsg", model.platform_weixin_subscribe_automsg),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@ServiceP", model.ServiceP),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@SubName", model.SubName),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Time_Edit", model.Time_Edit),
					new SqlParameter("@Title", model.Title)};

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
		public void Update(Lebi_Site model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Copyright,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Copyright")+"= @Copyright");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",Domain,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Domain")+"= @Domain");
			if((","+model.UpdateCols+",").IndexOf(",Email,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Email")+"= @Email");
			if((","+model.UpdateCols+",").IndexOf(",Fax,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Fax")+"= @Fax");
			if((","+model.UpdateCols+",").IndexOf(",FootHtml,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("FootHtml")+"= @FootHtml");
			if((","+model.UpdateCols+",").IndexOf(",IsMobile,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsMobile")+"= @IsMobile");
			if((","+model.UpdateCols+",").IndexOf(",Keywords,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Keywords")+"= @Keywords");
			if((","+model.UpdateCols+",").IndexOf(",Logoimg,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Logoimg")+"= @Logoimg");
			if((","+model.UpdateCols+",").IndexOf(",Name,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Name")+"= @Name");
			if((","+model.UpdateCols+",").IndexOf(",Path,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Path")+"= @Path");
			if((","+model.UpdateCols+",").IndexOf(",Phone,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Phone")+"= @Phone");
			if((","+model.UpdateCols+",").IndexOf(",platform_weixin_custemtoken,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_custemtoken")+"= @platform_weixin_custemtoken");
			if((","+model.UpdateCols+",").IndexOf(",platform_weixin_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_id")+"= @platform_weixin_id");
			if((","+model.UpdateCols+",").IndexOf(",platform_weixin_image_qrcode,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_image_qrcode")+"= @platform_weixin_image_qrcode");
			if((","+model.UpdateCols+",").IndexOf(",platform_weixin_number,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_number")+"= @platform_weixin_number");
			if((","+model.UpdateCols+",").IndexOf(",platform_weixin_secret,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_secret")+"= @platform_weixin_secret");
			if((","+model.UpdateCols+",").IndexOf(",platform_weixin_subscribe_automsg,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("platform_weixin_subscribe_automsg")+"= @platform_weixin_subscribe_automsg");
			if((","+model.UpdateCols+",").IndexOf(",QQ,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("QQ")+"= @QQ");
			if((","+model.UpdateCols+",").IndexOf(",ServiceP,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ServiceP")+"= @ServiceP");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",SubName,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("SubName")+"= @SubName");
			if((","+model.UpdateCols+",").IndexOf(",Time_add,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_add")+"= @Time_add");
			if((","+model.UpdateCols+",").IndexOf(",Time_Edit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Time_Edit")+"= @Time_Edit");
			if((","+model.UpdateCols+",").IndexOf(",Title,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Title")+"= @Title");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Copyright", model.Copyright),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Domain", model.Domain),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Fax", model.Fax),
					new SqlParameter("@FootHtml", model.FootHtml),
					new SqlParameter("@IsMobile", model.IsMobile),
					new SqlParameter("@Keywords", model.Keywords),
					new SqlParameter("@Logoimg", model.Logoimg),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Path", model.Path),
					new SqlParameter("@Phone", model.Phone),
					new SqlParameter("@platform_weixin_custemtoken", model.platform_weixin_custemtoken),
					new SqlParameter("@platform_weixin_id", model.platform_weixin_id),
					new SqlParameter("@platform_weixin_image_qrcode", model.platform_weixin_image_qrcode),
					new SqlParameter("@platform_weixin_number", model.platform_weixin_number),
					new SqlParameter("@platform_weixin_secret", model.platform_weixin_secret),
					new SqlParameter("@platform_weixin_subscribe_automsg", model.platform_weixin_subscribe_automsg),
					new SqlParameter("@QQ", model.QQ),
					new SqlParameter("@ServiceP", model.ServiceP),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@SubName", model.SubName),
					new SqlParameter("@Time_add", model.Time_add),
					new SqlParameter("@Time_Edit", model.Time_Edit),
					new SqlParameter("@Title", model.Title)};
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
		public Lebi_Site GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Site;
		   }
		   Lebi_Site model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Site",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Site GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Site;
		   }
		   Lebi_Site model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Site",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Site GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Site;
		   }
		   Lebi_Site model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Site",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Site> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Site>;
		   }
		   List<Lebi_Site> list = new List<Lebi_Site>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Site", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Site> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Site>;
		   }
		   List<Lebi_Site> list = new List<Lebi_Site>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Site", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Site> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Site>;
		   }
		   List<Lebi_Site> list = new List<Lebi_Site>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Site", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Site> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Site>;
		   }
		   List<Lebi_Site> list = new List<Lebi_Site>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Site", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Site BindForm(Lebi_Site model)
		{
			if (HttpContext.Current.Request["Copyright"] != null)
				model.Copyright=LB.Tools.RequestTool.RequestString("Copyright");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["Domain"] != null)
				model.Domain=LB.Tools.RequestTool.RequestString("Domain");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestString("Email");
			if (HttpContext.Current.Request["Fax"] != null)
				model.Fax=LB.Tools.RequestTool.RequestString("Fax");
			if (HttpContext.Current.Request["FootHtml"] != null)
				model.FootHtml=LB.Tools.RequestTool.RequestString("FootHtml");
			if (HttpContext.Current.Request["IsMobile"] != null)
				model.IsMobile=LB.Tools.RequestTool.RequestInt("IsMobile",0);
			if (HttpContext.Current.Request["Keywords"] != null)
				model.Keywords=LB.Tools.RequestTool.RequestString("Keywords");
			if (HttpContext.Current.Request["Logoimg"] != null)
				model.Logoimg=LB.Tools.RequestTool.RequestString("Logoimg");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestString("Name");
			if (HttpContext.Current.Request["Path"] != null)
				model.Path=LB.Tools.RequestTool.RequestString("Path");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestString("Phone");
			if (HttpContext.Current.Request["platform_weixin_custemtoken"] != null)
				model.platform_weixin_custemtoken=LB.Tools.RequestTool.RequestString("platform_weixin_custemtoken");
			if (HttpContext.Current.Request["platform_weixin_id"] != null)
				model.platform_weixin_id=LB.Tools.RequestTool.RequestString("platform_weixin_id");
			if (HttpContext.Current.Request["platform_weixin_image_qrcode"] != null)
				model.platform_weixin_image_qrcode=LB.Tools.RequestTool.RequestString("platform_weixin_image_qrcode");
			if (HttpContext.Current.Request["platform_weixin_number"] != null)
				model.platform_weixin_number=LB.Tools.RequestTool.RequestString("platform_weixin_number");
			if (HttpContext.Current.Request["platform_weixin_secret"] != null)
				model.platform_weixin_secret=LB.Tools.RequestTool.RequestString("platform_weixin_secret");
			if (HttpContext.Current.Request["platform_weixin_subscribe_automsg"] != null)
				model.platform_weixin_subscribe_automsg=LB.Tools.RequestTool.RequestString("platform_weixin_subscribe_automsg");
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestString("QQ");
			if (HttpContext.Current.Request["ServiceP"] != null)
				model.ServiceP=LB.Tools.RequestTool.RequestString("ServiceP");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["SubName"] != null)
				model.SubName=LB.Tools.RequestTool.RequestString("SubName");
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Edit"] != null)
				model.Time_Edit=LB.Tools.RequestTool.RequestTime("Time_Edit", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestString("Title");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Site SafeBindForm(Lebi_Site model)
		{
			if (HttpContext.Current.Request["Copyright"] != null)
				model.Copyright=LB.Tools.RequestTool.RequestSafeString("Copyright");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["Domain"] != null)
				model.Domain=LB.Tools.RequestTool.RequestSafeString("Domain");
			if (HttpContext.Current.Request["Email"] != null)
				model.Email=LB.Tools.RequestTool.RequestSafeString("Email");
			if (HttpContext.Current.Request["Fax"] != null)
				model.Fax=LB.Tools.RequestTool.RequestSafeString("Fax");
			if (HttpContext.Current.Request["FootHtml"] != null)
				model.FootHtml=LB.Tools.RequestTool.RequestSafeString("FootHtml");
			if (HttpContext.Current.Request["IsMobile"] != null)
				model.IsMobile=LB.Tools.RequestTool.RequestInt("IsMobile",0);
			if (HttpContext.Current.Request["Keywords"] != null)
				model.Keywords=LB.Tools.RequestTool.RequestSafeString("Keywords");
			if (HttpContext.Current.Request["Logoimg"] != null)
				model.Logoimg=LB.Tools.RequestTool.RequestSafeString("Logoimg");
			if (HttpContext.Current.Request["Name"] != null)
				model.Name=LB.Tools.RequestTool.RequestSafeString("Name");
			if (HttpContext.Current.Request["Path"] != null)
				model.Path=LB.Tools.RequestTool.RequestSafeString("Path");
			if (HttpContext.Current.Request["Phone"] != null)
				model.Phone=LB.Tools.RequestTool.RequestSafeString("Phone");
			if (HttpContext.Current.Request["platform_weixin_custemtoken"] != null)
				model.platform_weixin_custemtoken=LB.Tools.RequestTool.RequestSafeString("platform_weixin_custemtoken");
			if (HttpContext.Current.Request["platform_weixin_id"] != null)
				model.platform_weixin_id=LB.Tools.RequestTool.RequestSafeString("platform_weixin_id");
			if (HttpContext.Current.Request["platform_weixin_image_qrcode"] != null)
				model.platform_weixin_image_qrcode=LB.Tools.RequestTool.RequestSafeString("platform_weixin_image_qrcode");
			if (HttpContext.Current.Request["platform_weixin_number"] != null)
				model.platform_weixin_number=LB.Tools.RequestTool.RequestSafeString("platform_weixin_number");
			if (HttpContext.Current.Request["platform_weixin_secret"] != null)
				model.platform_weixin_secret=LB.Tools.RequestTool.RequestSafeString("platform_weixin_secret");
			if (HttpContext.Current.Request["platform_weixin_subscribe_automsg"] != null)
				model.platform_weixin_subscribe_automsg=LB.Tools.RequestTool.RequestSafeString("platform_weixin_subscribe_automsg");
			if (HttpContext.Current.Request["QQ"] != null)
				model.QQ=LB.Tools.RequestTool.RequestSafeString("QQ");
			if (HttpContext.Current.Request["ServiceP"] != null)
				model.ServiceP=LB.Tools.RequestTool.RequestSafeString("ServiceP");
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["SubName"] != null)
				model.SubName=LB.Tools.RequestTool.RequestSafeString("SubName");
			if (HttpContext.Current.Request["Time_add"] != null)
				model.Time_add=LB.Tools.RequestTool.RequestTime("Time_add", System.DateTime.Now);
			if (HttpContext.Current.Request["Time_Edit"] != null)
				model.Time_Edit=LB.Tools.RequestTool.RequestTime("Time_Edit", System.DateTime.Now);
			if (HttpContext.Current.Request["Title"] != null)
				model.Title=LB.Tools.RequestTool.RequestSafeString("Title");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Site ReaderBind(IDataReader dataReader)
		{
			Lebi_Site model=new Lebi_Site();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.Copyright=dataReader["Copyright"].ToString();
			model.Description=dataReader["Description"].ToString();
			model.Domain=dataReader["Domain"].ToString();
			model.Email=dataReader["Email"].ToString();
			model.Fax=dataReader["Fax"].ToString();
			model.FootHtml=dataReader["FootHtml"].ToString();
			ojb = dataReader["IsMobile"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsMobile= Convert.ToInt32(ojb);
			}
			model.Keywords=dataReader["Keywords"].ToString();
			model.Logoimg=dataReader["Logoimg"].ToString();
			model.Name=dataReader["Name"].ToString();
			model.Path=dataReader["Path"].ToString();
			model.Phone=dataReader["Phone"].ToString();
			model.platform_weixin_custemtoken=dataReader["platform_weixin_custemtoken"].ToString();
			model.platform_weixin_id=dataReader["platform_weixin_id"].ToString();
			model.platform_weixin_image_qrcode=dataReader["platform_weixin_image_qrcode"].ToString();
			model.platform_weixin_number=dataReader["platform_weixin_number"].ToString();
			model.platform_weixin_secret=dataReader["platform_weixin_secret"].ToString();
			model.platform_weixin_subscribe_automsg=dataReader["platform_weixin_subscribe_automsg"].ToString();
			model.QQ=dataReader["QQ"].ToString();
			model.ServiceP=dataReader["ServiceP"].ToString();
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			model.SubName=dataReader["SubName"].ToString();
			ojb = dataReader["Time_add"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_add=(DateTime)ojb;
			}
			ojb = dataReader["Time_Edit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Time_Edit=(DateTime)ojb;
			}
			model.Title=dataReader["Title"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

