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
	/// 数据访问类D_Lebi_Transport_Price。
	/// </summary>
	public partial class D_Lebi_Transport_Price
	{
		static D_Lebi_Transport_Price _Instance;
		public static D_Lebi_Transport_Price Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Transport_Price("Lebi_Transport_Price");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Transport_Price";
		public D_Lebi_Transport_Price(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Price", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Price", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Price" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Transport_Price", 0 , cachestr,seconds);
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
		public int Add(Lebi_Transport_Price model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Container")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCanofflinePay")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsOnePrice")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("OrderMoney")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_OrderMoneyOK")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Step")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_id")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight_Start")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight_Step")+")");
			strSql.Append(" values (");
			strSql.Append("@Area_id,@Container,@Description,@IsCanofflinePay,@IsOnePrice,@OrderMoney,@Price,@Price_OrderMoneyOK,@Price_Step,@Sort,@Supplier_id,@Transport_id,@Weight_Start,@Weight_Step);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@Container", model.Container),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@IsCanofflinePay", model.IsCanofflinePay),
					new SqlParameter("@IsOnePrice", model.IsOnePrice),
					new SqlParameter("@OrderMoney", model.OrderMoney),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@Price_OrderMoneyOK", model.Price_OrderMoneyOK),
					new SqlParameter("@Price_Step", model.Price_Step),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Transport_id", model.Transport_id),
					new SqlParameter("@Weight_Start", model.Weight_Start),
					new SqlParameter("@Weight_Step", model.Weight_Step)};

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
		public void Update(Lebi_Transport_Price model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",Area_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Area_id")+"= @Area_id");
			if((","+model.UpdateCols+",").IndexOf(",Container,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Container")+"= @Container");
			if((","+model.UpdateCols+",").IndexOf(",Description,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Description")+"= @Description");
			if((","+model.UpdateCols+",").IndexOf(",IsCanofflinePay,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCanofflinePay")+"= @IsCanofflinePay");
			if((","+model.UpdateCols+",").IndexOf(",IsOnePrice,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsOnePrice")+"= @IsOnePrice");
			if((","+model.UpdateCols+",").IndexOf(",OrderMoney,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("OrderMoney")+"= @OrderMoney");
			if((","+model.UpdateCols+",").IndexOf(",Price,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price")+"= @Price");
			if((","+model.UpdateCols+",").IndexOf(",Price_OrderMoneyOK,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_OrderMoneyOK")+"= @Price_OrderMoneyOK");
			if((","+model.UpdateCols+",").IndexOf(",Price_Step,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Price_Step")+"= @Price_Step");
			if((","+model.UpdateCols+",").IndexOf(",Sort,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Sort")+"= @Sort");
			if((","+model.UpdateCols+",").IndexOf(",Supplier_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Supplier_id")+"= @Supplier_id");
			if((","+model.UpdateCols+",").IndexOf(",Transport_id,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Transport_id")+"= @Transport_id");
			if((","+model.UpdateCols+",").IndexOf(",Weight_Start,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight_Start")+"= @Weight_Start");
			if((","+model.UpdateCols+",").IndexOf(",Weight_Step,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Weight_Step")+"= @Weight_Step");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@Area_id", model.Area_id),
					new SqlParameter("@Container", model.Container),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@IsCanofflinePay", model.IsCanofflinePay),
					new SqlParameter("@IsOnePrice", model.IsOnePrice),
					new SqlParameter("@OrderMoney", model.OrderMoney),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@Price_OrderMoneyOK", model.Price_OrderMoneyOK),
					new SqlParameter("@Price_Step", model.Price_Step),
					new SqlParameter("@Sort", model.Sort),
					new SqlParameter("@Supplier_id", model.Supplier_id),
					new SqlParameter("@Transport_id", model.Transport_id),
					new SqlParameter("@Weight_Start", model.Weight_Start),
					new SqlParameter("@Weight_Step", model.Weight_Step)};
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
		public Lebi_Transport_Price GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Transport_Price;
		   }
		   Lebi_Transport_Price model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Transport_Price",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Transport_Price GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Transport_Price;
		   }
		   Lebi_Transport_Price model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Transport_Price",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Transport_Price GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Transport_Price;
		   }
		   Lebi_Transport_Price model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Transport_Price",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Transport_Price> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Transport_Price>;
		   }
		   List<Lebi_Transport_Price> list = new List<Lebi_Transport_Price>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Price", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Transport_Price> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Transport_Price>;
		   }
		   List<Lebi_Transport_Price> list = new List<Lebi_Transport_Price>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Price", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Transport_Price> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Transport_Price>;
		   }
		   List<Lebi_Transport_Price> list = new List<Lebi_Transport_Price>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Price", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Transport_Price> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Transport_Price>;
		   }
		   List<Lebi_Transport_Price> list = new List<Lebi_Transport_Price>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Transport_Price", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Transport_Price BindForm(Lebi_Transport_Price model)
		{
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["Container"] != null)
				model.Container=LB.Tools.RequestTool.RequestString("Container");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestString("Description");
			if (HttpContext.Current.Request["IsCanofflinePay"] != null)
				model.IsCanofflinePay=LB.Tools.RequestTool.RequestInt("IsCanofflinePay",0);
			if (HttpContext.Current.Request["IsOnePrice"] != null)
				model.IsOnePrice=LB.Tools.RequestTool.RequestInt("IsOnePrice",0);
			if (HttpContext.Current.Request["OrderMoney"] != null)
				model.OrderMoney=LB.Tools.RequestTool.RequestDecimal("OrderMoney",0);
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["Price_OrderMoneyOK"] != null)
				model.Price_OrderMoneyOK=LB.Tools.RequestTool.RequestDecimal("Price_OrderMoneyOK",0);
			if (HttpContext.Current.Request["Price_Step"] != null)
				model.Price_Step=LB.Tools.RequestTool.RequestDecimal("Price_Step",0);
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Transport_id"] != null)
				model.Transport_id=LB.Tools.RequestTool.RequestInt("Transport_id",0);
			if (HttpContext.Current.Request["Weight_Start"] != null)
				model.Weight_Start=LB.Tools.RequestTool.RequestDecimal("Weight_Start",0);
			if (HttpContext.Current.Request["Weight_Step"] != null)
				model.Weight_Step=LB.Tools.RequestTool.RequestDecimal("Weight_Step",0);
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Transport_Price SafeBindForm(Lebi_Transport_Price model)
		{
			if (HttpContext.Current.Request["Area_id"] != null)
				model.Area_id=LB.Tools.RequestTool.RequestInt("Area_id",0);
			if (HttpContext.Current.Request["Container"] != null)
				model.Container=LB.Tools.RequestTool.RequestSafeString("Container");
			if (HttpContext.Current.Request["Description"] != null)
				model.Description=LB.Tools.RequestTool.RequestSafeString("Description");
			if (HttpContext.Current.Request["IsCanofflinePay"] != null)
				model.IsCanofflinePay=LB.Tools.RequestTool.RequestInt("IsCanofflinePay",0);
			if (HttpContext.Current.Request["IsOnePrice"] != null)
				model.IsOnePrice=LB.Tools.RequestTool.RequestInt("IsOnePrice",0);
			if (HttpContext.Current.Request["OrderMoney"] != null)
				model.OrderMoney=LB.Tools.RequestTool.RequestDecimal("OrderMoney",0);
			if (HttpContext.Current.Request["Price"] != null)
				model.Price=LB.Tools.RequestTool.RequestDecimal("Price",0);
			if (HttpContext.Current.Request["Price_OrderMoneyOK"] != null)
				model.Price_OrderMoneyOK=LB.Tools.RequestTool.RequestDecimal("Price_OrderMoneyOK",0);
			if (HttpContext.Current.Request["Price_Step"] != null)
				model.Price_Step=LB.Tools.RequestTool.RequestDecimal("Price_Step",0);
			if (HttpContext.Current.Request["Sort"] != null)
				model.Sort=LB.Tools.RequestTool.RequestInt("Sort",0);
			if (HttpContext.Current.Request["Supplier_id"] != null)
				model.Supplier_id=LB.Tools.RequestTool.RequestInt("Supplier_id",0);
			if (HttpContext.Current.Request["Transport_id"] != null)
				model.Transport_id=LB.Tools.RequestTool.RequestInt("Transport_id",0);
			if (HttpContext.Current.Request["Weight_Start"] != null)
				model.Weight_Start=LB.Tools.RequestTool.RequestDecimal("Weight_Start",0);
			if (HttpContext.Current.Request["Weight_Step"] != null)
				model.Weight_Step=LB.Tools.RequestTool.RequestDecimal("Weight_Step",0);
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Transport_Price ReaderBind(IDataReader dataReader)
		{
			Lebi_Transport_Price model=new Lebi_Transport_Price();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Area_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Area_id= Convert.ToInt32(ojb);
			}
			model.Container=dataReader["Container"].ToString();
			model.Description=dataReader["Description"].ToString();
			ojb = dataReader["IsCanofflinePay"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCanofflinePay= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsOnePrice"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsOnePrice= Convert.ToInt32(ojb);
			}
			ojb = dataReader["OrderMoney"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OrderMoney=(decimal)ojb;
			}
			ojb = dataReader["Price"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price=(decimal)ojb;
			}
			ojb = dataReader["Price_OrderMoneyOK"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_OrderMoneyOK=(decimal)ojb;
			}
			ojb = dataReader["Price_Step"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Price_Step=(decimal)ojb;
			}
			ojb = dataReader["Sort"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Sort= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Supplier_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Supplier_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Transport_id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Transport_id= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Weight_Start"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Weight_Start=(decimal)ojb;
			}
			ojb = dataReader["Weight_Step"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Weight_Step=(decimal)ojb;
			}
			return model;
		}

		#endregion  成员方法
	}
}

