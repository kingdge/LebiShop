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
	/// 数据访问类D_Lebi_Language_Tag。
	/// </summary>
	public partial class D_Lebi_Language_Tag
	{
		static D_Lebi_Language_Tag _Instance;
		public static D_Lebi_Language_Tag Instance
		{
		   get
		   {
		        if (_Instance == null)
		        {
		            _Instance = new D_Lebi_Language_Tag("Lebi_Language_Tag");
		        }
		        return _Instance;
		    }
		    set
		    {
		        _Instance = value;
		    }
		}
		string TableName = "Lebi_Language_Tag";
		public D_Lebi_Language_Tag(string tablename)
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
		           LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Language_Tag", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Language_Tag", 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Language_Tag" , 0 , cachestr , seconds);
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
		       LB.DataAccess.DB.Instance.SetMemchche(cachekey, val, "Lebi_Language_Tag", 0 , cachestr,seconds);
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
		public int Add(Lebi_Language_Tag model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into "+ TableName + " (");
			strSql.Append(LB.DataAccess.DB.BaseUtilsInstance.ColName("ar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ca")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("CN")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("cs")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("cy")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("da")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("de")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("el")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("EN")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("es")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("eu")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("fa")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("fi")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("fr")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("gl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("he")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("hr")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("hu")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("id_")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isar")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isca")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCN")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Iscs")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Iscy")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isda")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isde")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isel")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("IsEN")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ises")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Iseu")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isfa")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isfi")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isfr")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isgl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ishe")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ishr")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ishu")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isid_")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isit")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isja")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Iska")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isko")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Islt")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ismk")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isms")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isnl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isno")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ispl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ispt")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isro")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isru")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Issv")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Ista")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Istcn")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isth")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Istr")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isuk")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("Isvi")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("it")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ja")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ka")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ko")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("lt")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("mk")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ms")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("nl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("no")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("pl")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("pt")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ro")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ru")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("sv")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("ta")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("tag")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("tcn")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("th")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("tr")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("tw")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("uk")+","+LB.DataAccess.DB.BaseUtilsInstance.ColName("vi")+")");
			strSql.Append(" values (");
			strSql.Append("@ar,@ca,@CN,@cs,@cy,@da,@de,@el,@EN,@es,@eu,@fa,@fi,@fr,@gl,@he,@hr,@hu,@id_,@Isar,@Isca,@IsCN,@Iscs,@Iscy,@Isda,@Isde,@Isel,@IsEN,@Ises,@Iseu,@Isfa,@Isfi,@Isfr,@Isgl,@Ishe,@Ishr,@Ishu,@Isid_,@Isit,@Isja,@Iska,@Isko,@Islt,@Ismk,@Isms,@Isnl,@Isno,@Ispl,@Ispt,@Isro,@Isru,@Issv,@Ista,@Istcn,@Isth,@Istr,@Isuk,@Isvi,@it,@ja,@ka,@ko,@lt,@mk,@ms,@nl,@no,@pl,@pt,@ro,@ru,@sv,@ta,@tag,@tcn,@th,@tr,@tw,@uk,@vi);select @@identity;");
			SqlParameter[] parameters = {
					new SqlParameter("@ar", model.ar),
					new SqlParameter("@ca", model.ca),
					new SqlParameter("@CN", model.CN),
					new SqlParameter("@cs", model.cs),
					new SqlParameter("@cy", model.cy),
					new SqlParameter("@da", model.da),
					new SqlParameter("@de", model.de),
					new SqlParameter("@el", model.el),
					new SqlParameter("@EN", model.EN),
					new SqlParameter("@es", model.es),
					new SqlParameter("@eu", model.eu),
					new SqlParameter("@fa", model.fa),
					new SqlParameter("@fi", model.fi),
					new SqlParameter("@fr", model.fr),
					new SqlParameter("@gl", model.gl),
					new SqlParameter("@he", model.he),
					new SqlParameter("@hr", model.hr),
					new SqlParameter("@hu", model.hu),
					new SqlParameter("@id_", model.id_),
					new SqlParameter("@Isar", model.Isar),
					new SqlParameter("@Isca", model.Isca),
					new SqlParameter("@IsCN", model.IsCN),
					new SqlParameter("@Iscs", model.Iscs),
					new SqlParameter("@Iscy", model.Iscy),
					new SqlParameter("@Isda", model.Isda),
					new SqlParameter("@Isde", model.Isde),
					new SqlParameter("@Isel", model.Isel),
					new SqlParameter("@IsEN", model.IsEN),
					new SqlParameter("@Ises", model.Ises),
					new SqlParameter("@Iseu", model.Iseu),
					new SqlParameter("@Isfa", model.Isfa),
					new SqlParameter("@Isfi", model.Isfi),
					new SqlParameter("@Isfr", model.Isfr),
					new SqlParameter("@Isgl", model.Isgl),
					new SqlParameter("@Ishe", model.Ishe),
					new SqlParameter("@Ishr", model.Ishr),
					new SqlParameter("@Ishu", model.Ishu),
					new SqlParameter("@Isid_", model.Isid_),
					new SqlParameter("@Isit", model.Isit),
					new SqlParameter("@Isja", model.Isja),
					new SqlParameter("@Iska", model.Iska),
					new SqlParameter("@Isko", model.Isko),
					new SqlParameter("@Islt", model.Islt),
					new SqlParameter("@Ismk", model.Ismk),
					new SqlParameter("@Isms", model.Isms),
					new SqlParameter("@Isnl", model.Isnl),
					new SqlParameter("@Isno", model.Isno),
					new SqlParameter("@Ispl", model.Ispl),
					new SqlParameter("@Ispt", model.Ispt),
					new SqlParameter("@Isro", model.Isro),
					new SqlParameter("@Isru", model.Isru),
					new SqlParameter("@Issv", model.Issv),
					new SqlParameter("@Ista", model.Ista),
					new SqlParameter("@Istcn", model.Istcn),
					new SqlParameter("@Isth", model.Isth),
					new SqlParameter("@Istr", model.Istr),
					new SqlParameter("@Isuk", model.Isuk),
					new SqlParameter("@Isvi", model.Isvi),
					new SqlParameter("@it", model.it),
					new SqlParameter("@ja", model.ja),
					new SqlParameter("@ka", model.ka),
					new SqlParameter("@ko", model.ko),
					new SqlParameter("@lt", model.lt),
					new SqlParameter("@mk", model.mk),
					new SqlParameter("@ms", model.ms),
					new SqlParameter("@nl", model.nl),
					new SqlParameter("@no", model.no),
					new SqlParameter("@pl", model.pl),
					new SqlParameter("@pt", model.pt),
					new SqlParameter("@ro", model.ro),
					new SqlParameter("@ru", model.ru),
					new SqlParameter("@sv", model.sv),
					new SqlParameter("@ta", model.ta),
					new SqlParameter("@tag", model.tag),
					new SqlParameter("@tcn", model.tcn),
					new SqlParameter("@th", model.th),
					new SqlParameter("@tr", model.tr),
					new SqlParameter("@tw", model.tw),
					new SqlParameter("@uk", model.uk),
					new SqlParameter("@vi", model.vi)};

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
		public void Update(Lebi_Language_Tag model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update "+ TableName + " set ");
			List<string> cols = new List<string>();
			if((","+model.UpdateCols+",").IndexOf(",ar,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ar")+"= @ar");
			if((","+model.UpdateCols+",").IndexOf(",ca,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ca")+"= @ca");
			if((","+model.UpdateCols+",").IndexOf(",CN,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("CN")+"= @CN");
			if((","+model.UpdateCols+",").IndexOf(",cs,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("cs")+"= @cs");
			if((","+model.UpdateCols+",").IndexOf(",cy,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("cy")+"= @cy");
			if((","+model.UpdateCols+",").IndexOf(",da,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("da")+"= @da");
			if((","+model.UpdateCols+",").IndexOf(",de,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("de")+"= @de");
			if((","+model.UpdateCols+",").IndexOf(",el,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("el")+"= @el");
			if((","+model.UpdateCols+",").IndexOf(",EN,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("EN")+"= @EN");
			if((","+model.UpdateCols+",").IndexOf(",es,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("es")+"= @es");
			if((","+model.UpdateCols+",").IndexOf(",eu,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("eu")+"= @eu");
			if((","+model.UpdateCols+",").IndexOf(",fa,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("fa")+"= @fa");
			if((","+model.UpdateCols+",").IndexOf(",fi,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("fi")+"= @fi");
			if((","+model.UpdateCols+",").IndexOf(",fr,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("fr")+"= @fr");
			if((","+model.UpdateCols+",").IndexOf(",gl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("gl")+"= @gl");
			if((","+model.UpdateCols+",").IndexOf(",he,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("he")+"= @he");
			if((","+model.UpdateCols+",").IndexOf(",hr,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("hr")+"= @hr");
			if((","+model.UpdateCols+",").IndexOf(",hu,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("hu")+"= @hu");
			if((","+model.UpdateCols+",").IndexOf(",id_,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("id_")+"= @id_");
			if((","+model.UpdateCols+",").IndexOf(",Isar,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isar")+"= @Isar");
			if((","+model.UpdateCols+",").IndexOf(",Isca,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isca")+"= @Isca");
			if((","+model.UpdateCols+",").IndexOf(",IsCN,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsCN")+"= @IsCN");
			if((","+model.UpdateCols+",").IndexOf(",Iscs,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Iscs")+"= @Iscs");
			if((","+model.UpdateCols+",").IndexOf(",Iscy,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Iscy")+"= @Iscy");
			if((","+model.UpdateCols+",").IndexOf(",Isda,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isda")+"= @Isda");
			if((","+model.UpdateCols+",").IndexOf(",Isde,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isde")+"= @Isde");
			if((","+model.UpdateCols+",").IndexOf(",Isel,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isel")+"= @Isel");
			if((","+model.UpdateCols+",").IndexOf(",IsEN,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("IsEN")+"= @IsEN");
			if((","+model.UpdateCols+",").IndexOf(",Ises,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ises")+"= @Ises");
			if((","+model.UpdateCols+",").IndexOf(",Iseu,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Iseu")+"= @Iseu");
			if((","+model.UpdateCols+",").IndexOf(",Isfa,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isfa")+"= @Isfa");
			if((","+model.UpdateCols+",").IndexOf(",Isfi,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isfi")+"= @Isfi");
			if((","+model.UpdateCols+",").IndexOf(",Isfr,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isfr")+"= @Isfr");
			if((","+model.UpdateCols+",").IndexOf(",Isgl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isgl")+"= @Isgl");
			if((","+model.UpdateCols+",").IndexOf(",Ishe,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ishe")+"= @Ishe");
			if((","+model.UpdateCols+",").IndexOf(",Ishr,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ishr")+"= @Ishr");
			if((","+model.UpdateCols+",").IndexOf(",Ishu,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ishu")+"= @Ishu");
			if((","+model.UpdateCols+",").IndexOf(",Isid_,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isid_")+"= @Isid_");
			if((","+model.UpdateCols+",").IndexOf(",Isit,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isit")+"= @Isit");
			if((","+model.UpdateCols+",").IndexOf(",Isja,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isja")+"= @Isja");
			if((","+model.UpdateCols+",").IndexOf(",Iska,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Iska")+"= @Iska");
			if((","+model.UpdateCols+",").IndexOf(",Isko,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isko")+"= @Isko");
			if((","+model.UpdateCols+",").IndexOf(",Islt,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Islt")+"= @Islt");
			if((","+model.UpdateCols+",").IndexOf(",Ismk,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ismk")+"= @Ismk");
			if((","+model.UpdateCols+",").IndexOf(",Isms,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isms")+"= @Isms");
			if((","+model.UpdateCols+",").IndexOf(",Isnl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isnl")+"= @Isnl");
			if((","+model.UpdateCols+",").IndexOf(",Isno,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isno")+"= @Isno");
			if((","+model.UpdateCols+",").IndexOf(",Ispl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ispl")+"= @Ispl");
			if((","+model.UpdateCols+",").IndexOf(",Ispt,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ispt")+"= @Ispt");
			if((","+model.UpdateCols+",").IndexOf(",Isro,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isro")+"= @Isro");
			if((","+model.UpdateCols+",").IndexOf(",Isru,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isru")+"= @Isru");
			if((","+model.UpdateCols+",").IndexOf(",Issv,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Issv")+"= @Issv");
			if((","+model.UpdateCols+",").IndexOf(",Ista,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Ista")+"= @Ista");
			if((","+model.UpdateCols+",").IndexOf(",Istcn,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Istcn")+"= @Istcn");
			if((","+model.UpdateCols+",").IndexOf(",Isth,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isth")+"= @Isth");
			if((","+model.UpdateCols+",").IndexOf(",Istr,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Istr")+"= @Istr");
			if((","+model.UpdateCols+",").IndexOf(",Isuk,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isuk")+"= @Isuk");
			if((","+model.UpdateCols+",").IndexOf(",Isvi,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("Isvi")+"= @Isvi");
			if((","+model.UpdateCols+",").IndexOf(",it,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("it")+"= @it");
			if((","+model.UpdateCols+",").IndexOf(",ja,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ja")+"= @ja");
			if((","+model.UpdateCols+",").IndexOf(",ka,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ka")+"= @ka");
			if((","+model.UpdateCols+",").IndexOf(",ko,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ko")+"= @ko");
			if((","+model.UpdateCols+",").IndexOf(",lt,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("lt")+"= @lt");
			if((","+model.UpdateCols+",").IndexOf(",mk,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("mk")+"= @mk");
			if((","+model.UpdateCols+",").IndexOf(",ms,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ms")+"= @ms");
			if((","+model.UpdateCols+",").IndexOf(",nl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("nl")+"= @nl");
			if((","+model.UpdateCols+",").IndexOf(",no,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("no")+"= @no");
			if((","+model.UpdateCols+",").IndexOf(",pl,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("pl")+"= @pl");
			if((","+model.UpdateCols+",").IndexOf(",pt,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("pt")+"= @pt");
			if((","+model.UpdateCols+",").IndexOf(",ro,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ro")+"= @ro");
			if((","+model.UpdateCols+",").IndexOf(",ru,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ru")+"= @ru");
			if((","+model.UpdateCols+",").IndexOf(",sv,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("sv")+"= @sv");
			if((","+model.UpdateCols+",").IndexOf(",ta,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("ta")+"= @ta");
			if((","+model.UpdateCols+",").IndexOf(",tag,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("tag")+"= @tag");
			if((","+model.UpdateCols+",").IndexOf(",tcn,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("tcn")+"= @tcn");
			if((","+model.UpdateCols+",").IndexOf(",th,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("th")+"= @th");
			if((","+model.UpdateCols+",").IndexOf(",tr,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("tr")+"= @tr");
			if((","+model.UpdateCols+",").IndexOf(",tw,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("tw")+"= @tw");
			if((","+model.UpdateCols+",").IndexOf(",uk,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("uk")+"= @uk");
			if((","+model.UpdateCols+",").IndexOf(",vi,")>-1 || model.UpdateCols=="")
			   cols.Add(LB.DataAccess.DB.BaseUtilsInstance.ColName("vi")+"= @vi");
			strSql.Append(string.Join(",", cols));
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", model.id),
					new SqlParameter("@ar", model.ar),
					new SqlParameter("@ca", model.ca),
					new SqlParameter("@CN", model.CN),
					new SqlParameter("@cs", model.cs),
					new SqlParameter("@cy", model.cy),
					new SqlParameter("@da", model.da),
					new SqlParameter("@de", model.de),
					new SqlParameter("@el", model.el),
					new SqlParameter("@EN", model.EN),
					new SqlParameter("@es", model.es),
					new SqlParameter("@eu", model.eu),
					new SqlParameter("@fa", model.fa),
					new SqlParameter("@fi", model.fi),
					new SqlParameter("@fr", model.fr),
					new SqlParameter("@gl", model.gl),
					new SqlParameter("@he", model.he),
					new SqlParameter("@hr", model.hr),
					new SqlParameter("@hu", model.hu),
					new SqlParameter("@id_", model.id_),
					new SqlParameter("@Isar", model.Isar),
					new SqlParameter("@Isca", model.Isca),
					new SqlParameter("@IsCN", model.IsCN),
					new SqlParameter("@Iscs", model.Iscs),
					new SqlParameter("@Iscy", model.Iscy),
					new SqlParameter("@Isda", model.Isda),
					new SqlParameter("@Isde", model.Isde),
					new SqlParameter("@Isel", model.Isel),
					new SqlParameter("@IsEN", model.IsEN),
					new SqlParameter("@Ises", model.Ises),
					new SqlParameter("@Iseu", model.Iseu),
					new SqlParameter("@Isfa", model.Isfa),
					new SqlParameter("@Isfi", model.Isfi),
					new SqlParameter("@Isfr", model.Isfr),
					new SqlParameter("@Isgl", model.Isgl),
					new SqlParameter("@Ishe", model.Ishe),
					new SqlParameter("@Ishr", model.Ishr),
					new SqlParameter("@Ishu", model.Ishu),
					new SqlParameter("@Isid_", model.Isid_),
					new SqlParameter("@Isit", model.Isit),
					new SqlParameter("@Isja", model.Isja),
					new SqlParameter("@Iska", model.Iska),
					new SqlParameter("@Isko", model.Isko),
					new SqlParameter("@Islt", model.Islt),
					new SqlParameter("@Ismk", model.Ismk),
					new SqlParameter("@Isms", model.Isms),
					new SqlParameter("@Isnl", model.Isnl),
					new SqlParameter("@Isno", model.Isno),
					new SqlParameter("@Ispl", model.Ispl),
					new SqlParameter("@Ispt", model.Ispt),
					new SqlParameter("@Isro", model.Isro),
					new SqlParameter("@Isru", model.Isru),
					new SqlParameter("@Issv", model.Issv),
					new SqlParameter("@Ista", model.Ista),
					new SqlParameter("@Istcn", model.Istcn),
					new SqlParameter("@Isth", model.Isth),
					new SqlParameter("@Istr", model.Istr),
					new SqlParameter("@Isuk", model.Isuk),
					new SqlParameter("@Isvi", model.Isvi),
					new SqlParameter("@it", model.it),
					new SqlParameter("@ja", model.ja),
					new SqlParameter("@ka", model.ka),
					new SqlParameter("@ko", model.ko),
					new SqlParameter("@lt", model.lt),
					new SqlParameter("@mk", model.mk),
					new SqlParameter("@ms", model.ms),
					new SqlParameter("@nl", model.nl),
					new SqlParameter("@no", model.no),
					new SqlParameter("@pl", model.pl),
					new SqlParameter("@pt", model.pt),
					new SqlParameter("@ro", model.ro),
					new SqlParameter("@ru", model.ru),
					new SqlParameter("@sv", model.sv),
					new SqlParameter("@ta", model.ta),
					new SqlParameter("@tag", model.tag),
					new SqlParameter("@tcn", model.tcn),
					new SqlParameter("@th", model.th),
					new SqlParameter("@tr", model.tr),
					new SqlParameter("@tw", model.tw),
					new SqlParameter("@uk", model.uk),
					new SqlParameter("@vi", model.vi)};
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
		public Lebi_Language_Tag GetModel(int id, int seconds=0)
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
		           return obj as Lebi_Language_Tag;
		   }
		   Lebi_Language_Tag model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow,  strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Language_Tag",id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public Lebi_Language_Tag GetModel(string strWhere, int seconds=0)
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
		           return obj as Lebi_Language_Tag;
		   }
		   Lebi_Language_Tag model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, null))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Language_Tag",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}
		/// <summary>
		/// 得到一个对象实体 by SQLpara
		/// </summary>
		public Lebi_Language_Tag GetModel(SQLPara para, int seconds=0)
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
		           return obj as Lebi_Language_Tag;
		   }
		   Lebi_Language_Tag model = null;
		   using (IDataReader dataReader = LB.DataAccess.DB.Instance.TextExecuteReaderOne(strTableName, strFieldShow, strWhere, para.Para))
		   {
		       if(dataReader==null)
		           return null;
		       while (dataReader.Read())
		       {
		           model = ReaderBind(dataReader);
		           if (cachekey != "")
		               LB.DataAccess.DB.Instance.SetMemchche(cachekey, model, "Lebi_Language_Tag",model.id , cachestr , seconds);
		           return model;
		       }
		   }
		   return null;
		}

		/// <summary>
		/// 获得数据列表-带分页
		/// </summary>
		public List<Lebi_Language_Tag> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Language_Tag>;
		   }
		   List<Lebi_Language_Tag> list = new List<Lebi_Language_Tag>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Language_Tag", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Language_Tag> GetList(SQLPara para, int PageSize, int page, int seconds=0)
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
		           return obj as List<Lebi_Language_Tag>;
		   }
		   List<Lebi_Language_Tag> list = new List<Lebi_Language_Tag>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Language_Tag", 0 , cachestr , seconds);
		   return list;
		}

		/// <summary>
		/// 获得数据列表-不带分页
		/// </summary>
		public List<Lebi_Language_Tag> GetList(string strWhere,string strFieldOrder, int seconds=0)
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
		           return obj as List<Lebi_Language_Tag>;
		   }
		   List<Lebi_Language_Tag> list = new List<Lebi_Language_Tag>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Language_Tag", 0 , cachestr , seconds);
		   return list;
		}
		public List<Lebi_Language_Tag> GetList(SQLPara para, int seconds=0)
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
		           return obj as List<Lebi_Language_Tag>;
		   }
		   List<Lebi_Language_Tag> list = new List<Lebi_Language_Tag>();
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
		         LB.DataAccess.DB.Instance.SetMemchche(cachekey, list, "Lebi_Language_Tag", 0 , cachestr , seconds);
		   return list;
		}


		/// <summary>
		/// 绑定对象表单
		/// </summary>
		public Lebi_Language_Tag BindForm(Lebi_Language_Tag model)
		{
			if (HttpContext.Current.Request["ar"] != null)
				model.ar=LB.Tools.RequestTool.RequestString("ar");
			if (HttpContext.Current.Request["ca"] != null)
				model.ca=LB.Tools.RequestTool.RequestString("ca");
			if (HttpContext.Current.Request["CN"] != null)
				model.CN=LB.Tools.RequestTool.RequestString("CN");
			if (HttpContext.Current.Request["cs"] != null)
				model.cs=LB.Tools.RequestTool.RequestString("cs");
			if (HttpContext.Current.Request["cy"] != null)
				model.cy=LB.Tools.RequestTool.RequestString("cy");
			if (HttpContext.Current.Request["da"] != null)
				model.da=LB.Tools.RequestTool.RequestString("da");
			if (HttpContext.Current.Request["de"] != null)
				model.de=LB.Tools.RequestTool.RequestString("de");
			if (HttpContext.Current.Request["el"] != null)
				model.el=LB.Tools.RequestTool.RequestString("el");
			if (HttpContext.Current.Request["EN"] != null)
				model.EN=LB.Tools.RequestTool.RequestString("EN");
			if (HttpContext.Current.Request["es"] != null)
				model.es=LB.Tools.RequestTool.RequestString("es");
			if (HttpContext.Current.Request["eu"] != null)
				model.eu=LB.Tools.RequestTool.RequestString("eu");
			if (HttpContext.Current.Request["fa"] != null)
				model.fa=LB.Tools.RequestTool.RequestString("fa");
			if (HttpContext.Current.Request["fi"] != null)
				model.fi=LB.Tools.RequestTool.RequestString("fi");
			if (HttpContext.Current.Request["fr"] != null)
				model.fr=LB.Tools.RequestTool.RequestString("fr");
			if (HttpContext.Current.Request["gl"] != null)
				model.gl=LB.Tools.RequestTool.RequestString("gl");
			if (HttpContext.Current.Request["he"] != null)
				model.he=LB.Tools.RequestTool.RequestString("he");
			if (HttpContext.Current.Request["hr"] != null)
				model.hr=LB.Tools.RequestTool.RequestString("hr");
			if (HttpContext.Current.Request["hu"] != null)
				model.hu=LB.Tools.RequestTool.RequestString("hu");
			if (HttpContext.Current.Request["id_"] != null)
				model.id_=LB.Tools.RequestTool.RequestString("id_");
			if (HttpContext.Current.Request["Isar"] != null)
				model.Isar=LB.Tools.RequestTool.RequestInt("Isar",0);
			if (HttpContext.Current.Request["Isca"] != null)
				model.Isca=LB.Tools.RequestTool.RequestInt("Isca",0);
			if (HttpContext.Current.Request["IsCN"] != null)
				model.IsCN=LB.Tools.RequestTool.RequestInt("IsCN",0);
			if (HttpContext.Current.Request["Iscs"] != null)
				model.Iscs=LB.Tools.RequestTool.RequestInt("Iscs",0);
			if (HttpContext.Current.Request["Iscy"] != null)
				model.Iscy=LB.Tools.RequestTool.RequestInt("Iscy",0);
			if (HttpContext.Current.Request["Isda"] != null)
				model.Isda=LB.Tools.RequestTool.RequestInt("Isda",0);
			if (HttpContext.Current.Request["Isde"] != null)
				model.Isde=LB.Tools.RequestTool.RequestInt("Isde",0);
			if (HttpContext.Current.Request["Isel"] != null)
				model.Isel=LB.Tools.RequestTool.RequestInt("Isel",0);
			if (HttpContext.Current.Request["IsEN"] != null)
				model.IsEN=LB.Tools.RequestTool.RequestInt("IsEN",0);
			if (HttpContext.Current.Request["Ises"] != null)
				model.Ises=LB.Tools.RequestTool.RequestInt("Ises",0);
			if (HttpContext.Current.Request["Iseu"] != null)
				model.Iseu=LB.Tools.RequestTool.RequestInt("Iseu",0);
			if (HttpContext.Current.Request["Isfa"] != null)
				model.Isfa=LB.Tools.RequestTool.RequestInt("Isfa",0);
			if (HttpContext.Current.Request["Isfi"] != null)
				model.Isfi=LB.Tools.RequestTool.RequestInt("Isfi",0);
			if (HttpContext.Current.Request["Isfr"] != null)
				model.Isfr=LB.Tools.RequestTool.RequestInt("Isfr",0);
			if (HttpContext.Current.Request["Isgl"] != null)
				model.Isgl=LB.Tools.RequestTool.RequestInt("Isgl",0);
			if (HttpContext.Current.Request["Ishe"] != null)
				model.Ishe=LB.Tools.RequestTool.RequestInt("Ishe",0);
			if (HttpContext.Current.Request["Ishr"] != null)
				model.Ishr=LB.Tools.RequestTool.RequestInt("Ishr",0);
			if (HttpContext.Current.Request["Ishu"] != null)
				model.Ishu=LB.Tools.RequestTool.RequestInt("Ishu",0);
			if (HttpContext.Current.Request["Isid_"] != null)
				model.Isid_=LB.Tools.RequestTool.RequestInt("Isid_",0);
			if (HttpContext.Current.Request["Isit"] != null)
				model.Isit=LB.Tools.RequestTool.RequestInt("Isit",0);
			if (HttpContext.Current.Request["Isja"] != null)
				model.Isja=LB.Tools.RequestTool.RequestInt("Isja",0);
			if (HttpContext.Current.Request["Iska"] != null)
				model.Iska=LB.Tools.RequestTool.RequestInt("Iska",0);
			if (HttpContext.Current.Request["Isko"] != null)
				model.Isko=LB.Tools.RequestTool.RequestInt("Isko",0);
			if (HttpContext.Current.Request["Islt"] != null)
				model.Islt=LB.Tools.RequestTool.RequestInt("Islt",0);
			if (HttpContext.Current.Request["Ismk"] != null)
				model.Ismk=LB.Tools.RequestTool.RequestInt("Ismk",0);
			if (HttpContext.Current.Request["Isms"] != null)
				model.Isms=LB.Tools.RequestTool.RequestInt("Isms",0);
			if (HttpContext.Current.Request["Isnl"] != null)
				model.Isnl=LB.Tools.RequestTool.RequestInt("Isnl",0);
			if (HttpContext.Current.Request["Isno"] != null)
				model.Isno=LB.Tools.RequestTool.RequestInt("Isno",0);
			if (HttpContext.Current.Request["Ispl"] != null)
				model.Ispl=LB.Tools.RequestTool.RequestInt("Ispl",0);
			if (HttpContext.Current.Request["Ispt"] != null)
				model.Ispt=LB.Tools.RequestTool.RequestInt("Ispt",0);
			if (HttpContext.Current.Request["Isro"] != null)
				model.Isro=LB.Tools.RequestTool.RequestInt("Isro",0);
			if (HttpContext.Current.Request["Isru"] != null)
				model.Isru=LB.Tools.RequestTool.RequestInt("Isru",0);
			if (HttpContext.Current.Request["Issv"] != null)
				model.Issv=LB.Tools.RequestTool.RequestInt("Issv",0);
			if (HttpContext.Current.Request["Ista"] != null)
				model.Ista=LB.Tools.RequestTool.RequestInt("Ista",0);
			if (HttpContext.Current.Request["Istcn"] != null)
				model.Istcn=LB.Tools.RequestTool.RequestInt("Istcn",0);
			if (HttpContext.Current.Request["Isth"] != null)
				model.Isth=LB.Tools.RequestTool.RequestInt("Isth",0);
			if (HttpContext.Current.Request["Istr"] != null)
				model.Istr=LB.Tools.RequestTool.RequestInt("Istr",0);
			if (HttpContext.Current.Request["Isuk"] != null)
				model.Isuk=LB.Tools.RequestTool.RequestInt("Isuk",0);
			if (HttpContext.Current.Request["Isvi"] != null)
				model.Isvi=LB.Tools.RequestTool.RequestInt("Isvi",0);
			if (HttpContext.Current.Request["it"] != null)
				model.it=LB.Tools.RequestTool.RequestString("it");
			if (HttpContext.Current.Request["ja"] != null)
				model.ja=LB.Tools.RequestTool.RequestString("ja");
			if (HttpContext.Current.Request["ka"] != null)
				model.ka=LB.Tools.RequestTool.RequestString("ka");
			if (HttpContext.Current.Request["ko"] != null)
				model.ko=LB.Tools.RequestTool.RequestString("ko");
			if (HttpContext.Current.Request["lt"] != null)
				model.lt=LB.Tools.RequestTool.RequestString("lt");
			if (HttpContext.Current.Request["mk"] != null)
				model.mk=LB.Tools.RequestTool.RequestString("mk");
			if (HttpContext.Current.Request["ms"] != null)
				model.ms=LB.Tools.RequestTool.RequestString("ms");
			if (HttpContext.Current.Request["nl"] != null)
				model.nl=LB.Tools.RequestTool.RequestString("nl");
			if (HttpContext.Current.Request["no"] != null)
				model.no=LB.Tools.RequestTool.RequestString("no");
			if (HttpContext.Current.Request["pl"] != null)
				model.pl=LB.Tools.RequestTool.RequestString("pl");
			if (HttpContext.Current.Request["pt"] != null)
				model.pt=LB.Tools.RequestTool.RequestString("pt");
			if (HttpContext.Current.Request["ro"] != null)
				model.ro=LB.Tools.RequestTool.RequestString("ro");
			if (HttpContext.Current.Request["ru"] != null)
				model.ru=LB.Tools.RequestTool.RequestString("ru");
			if (HttpContext.Current.Request["sv"] != null)
				model.sv=LB.Tools.RequestTool.RequestString("sv");
			if (HttpContext.Current.Request["ta"] != null)
				model.ta=LB.Tools.RequestTool.RequestString("ta");
			if (HttpContext.Current.Request["tag"] != null)
				model.tag=LB.Tools.RequestTool.RequestString("tag");
			if (HttpContext.Current.Request["tcn"] != null)
				model.tcn=LB.Tools.RequestTool.RequestString("tcn");
			if (HttpContext.Current.Request["th"] != null)
				model.th=LB.Tools.RequestTool.RequestString("th");
			if (HttpContext.Current.Request["tr"] != null)
				model.tr=LB.Tools.RequestTool.RequestString("tr");
			if (HttpContext.Current.Request["tw"] != null)
				model.tw=LB.Tools.RequestTool.RequestString("tw");
			if (HttpContext.Current.Request["uk"] != null)
				model.uk=LB.Tools.RequestTool.RequestString("uk");
			if (HttpContext.Current.Request["vi"] != null)
				model.vi=LB.Tools.RequestTool.RequestString("vi");
				return model;
		}
		/// <summary>
		/// 安全方式绑定对象表单
		/// </summary>
		public Lebi_Language_Tag SafeBindForm(Lebi_Language_Tag model)
		{
			if (HttpContext.Current.Request["ar"] != null)
				model.ar=LB.Tools.RequestTool.RequestSafeString("ar");
			if (HttpContext.Current.Request["ca"] != null)
				model.ca=LB.Tools.RequestTool.RequestSafeString("ca");
			if (HttpContext.Current.Request["CN"] != null)
				model.CN=LB.Tools.RequestTool.RequestSafeString("CN");
			if (HttpContext.Current.Request["cs"] != null)
				model.cs=LB.Tools.RequestTool.RequestSafeString("cs");
			if (HttpContext.Current.Request["cy"] != null)
				model.cy=LB.Tools.RequestTool.RequestSafeString("cy");
			if (HttpContext.Current.Request["da"] != null)
				model.da=LB.Tools.RequestTool.RequestSafeString("da");
			if (HttpContext.Current.Request["de"] != null)
				model.de=LB.Tools.RequestTool.RequestSafeString("de");
			if (HttpContext.Current.Request["el"] != null)
				model.el=LB.Tools.RequestTool.RequestSafeString("el");
			if (HttpContext.Current.Request["EN"] != null)
				model.EN=LB.Tools.RequestTool.RequestSafeString("EN");
			if (HttpContext.Current.Request["es"] != null)
				model.es=LB.Tools.RequestTool.RequestSafeString("es");
			if (HttpContext.Current.Request["eu"] != null)
				model.eu=LB.Tools.RequestTool.RequestSafeString("eu");
			if (HttpContext.Current.Request["fa"] != null)
				model.fa=LB.Tools.RequestTool.RequestSafeString("fa");
			if (HttpContext.Current.Request["fi"] != null)
				model.fi=LB.Tools.RequestTool.RequestSafeString("fi");
			if (HttpContext.Current.Request["fr"] != null)
				model.fr=LB.Tools.RequestTool.RequestSafeString("fr");
			if (HttpContext.Current.Request["gl"] != null)
				model.gl=LB.Tools.RequestTool.RequestSafeString("gl");
			if (HttpContext.Current.Request["he"] != null)
				model.he=LB.Tools.RequestTool.RequestSafeString("he");
			if (HttpContext.Current.Request["hr"] != null)
				model.hr=LB.Tools.RequestTool.RequestSafeString("hr");
			if (HttpContext.Current.Request["hu"] != null)
				model.hu=LB.Tools.RequestTool.RequestSafeString("hu");
			if (HttpContext.Current.Request["id_"] != null)
				model.id_=LB.Tools.RequestTool.RequestSafeString("id_");
			if (HttpContext.Current.Request["Isar"] != null)
				model.Isar=LB.Tools.RequestTool.RequestInt("Isar",0);
			if (HttpContext.Current.Request["Isca"] != null)
				model.Isca=LB.Tools.RequestTool.RequestInt("Isca",0);
			if (HttpContext.Current.Request["IsCN"] != null)
				model.IsCN=LB.Tools.RequestTool.RequestInt("IsCN",0);
			if (HttpContext.Current.Request["Iscs"] != null)
				model.Iscs=LB.Tools.RequestTool.RequestInt("Iscs",0);
			if (HttpContext.Current.Request["Iscy"] != null)
				model.Iscy=LB.Tools.RequestTool.RequestInt("Iscy",0);
			if (HttpContext.Current.Request["Isda"] != null)
				model.Isda=LB.Tools.RequestTool.RequestInt("Isda",0);
			if (HttpContext.Current.Request["Isde"] != null)
				model.Isde=LB.Tools.RequestTool.RequestInt("Isde",0);
			if (HttpContext.Current.Request["Isel"] != null)
				model.Isel=LB.Tools.RequestTool.RequestInt("Isel",0);
			if (HttpContext.Current.Request["IsEN"] != null)
				model.IsEN=LB.Tools.RequestTool.RequestInt("IsEN",0);
			if (HttpContext.Current.Request["Ises"] != null)
				model.Ises=LB.Tools.RequestTool.RequestInt("Ises",0);
			if (HttpContext.Current.Request["Iseu"] != null)
				model.Iseu=LB.Tools.RequestTool.RequestInt("Iseu",0);
			if (HttpContext.Current.Request["Isfa"] != null)
				model.Isfa=LB.Tools.RequestTool.RequestInt("Isfa",0);
			if (HttpContext.Current.Request["Isfi"] != null)
				model.Isfi=LB.Tools.RequestTool.RequestInt("Isfi",0);
			if (HttpContext.Current.Request["Isfr"] != null)
				model.Isfr=LB.Tools.RequestTool.RequestInt("Isfr",0);
			if (HttpContext.Current.Request["Isgl"] != null)
				model.Isgl=LB.Tools.RequestTool.RequestInt("Isgl",0);
			if (HttpContext.Current.Request["Ishe"] != null)
				model.Ishe=LB.Tools.RequestTool.RequestInt("Ishe",0);
			if (HttpContext.Current.Request["Ishr"] != null)
				model.Ishr=LB.Tools.RequestTool.RequestInt("Ishr",0);
			if (HttpContext.Current.Request["Ishu"] != null)
				model.Ishu=LB.Tools.RequestTool.RequestInt("Ishu",0);
			if (HttpContext.Current.Request["Isid_"] != null)
				model.Isid_=LB.Tools.RequestTool.RequestInt("Isid_",0);
			if (HttpContext.Current.Request["Isit"] != null)
				model.Isit=LB.Tools.RequestTool.RequestInt("Isit",0);
			if (HttpContext.Current.Request["Isja"] != null)
				model.Isja=LB.Tools.RequestTool.RequestInt("Isja",0);
			if (HttpContext.Current.Request["Iska"] != null)
				model.Iska=LB.Tools.RequestTool.RequestInt("Iska",0);
			if (HttpContext.Current.Request["Isko"] != null)
				model.Isko=LB.Tools.RequestTool.RequestInt("Isko",0);
			if (HttpContext.Current.Request["Islt"] != null)
				model.Islt=LB.Tools.RequestTool.RequestInt("Islt",0);
			if (HttpContext.Current.Request["Ismk"] != null)
				model.Ismk=LB.Tools.RequestTool.RequestInt("Ismk",0);
			if (HttpContext.Current.Request["Isms"] != null)
				model.Isms=LB.Tools.RequestTool.RequestInt("Isms",0);
			if (HttpContext.Current.Request["Isnl"] != null)
				model.Isnl=LB.Tools.RequestTool.RequestInt("Isnl",0);
			if (HttpContext.Current.Request["Isno"] != null)
				model.Isno=LB.Tools.RequestTool.RequestInt("Isno",0);
			if (HttpContext.Current.Request["Ispl"] != null)
				model.Ispl=LB.Tools.RequestTool.RequestInt("Ispl",0);
			if (HttpContext.Current.Request["Ispt"] != null)
				model.Ispt=LB.Tools.RequestTool.RequestInt("Ispt",0);
			if (HttpContext.Current.Request["Isro"] != null)
				model.Isro=LB.Tools.RequestTool.RequestInt("Isro",0);
			if (HttpContext.Current.Request["Isru"] != null)
				model.Isru=LB.Tools.RequestTool.RequestInt("Isru",0);
			if (HttpContext.Current.Request["Issv"] != null)
				model.Issv=LB.Tools.RequestTool.RequestInt("Issv",0);
			if (HttpContext.Current.Request["Ista"] != null)
				model.Ista=LB.Tools.RequestTool.RequestInt("Ista",0);
			if (HttpContext.Current.Request["Istcn"] != null)
				model.Istcn=LB.Tools.RequestTool.RequestInt("Istcn",0);
			if (HttpContext.Current.Request["Isth"] != null)
				model.Isth=LB.Tools.RequestTool.RequestInt("Isth",0);
			if (HttpContext.Current.Request["Istr"] != null)
				model.Istr=LB.Tools.RequestTool.RequestInt("Istr",0);
			if (HttpContext.Current.Request["Isuk"] != null)
				model.Isuk=LB.Tools.RequestTool.RequestInt("Isuk",0);
			if (HttpContext.Current.Request["Isvi"] != null)
				model.Isvi=LB.Tools.RequestTool.RequestInt("Isvi",0);
			if (HttpContext.Current.Request["it"] != null)
				model.it=LB.Tools.RequestTool.RequestSafeString("it");
			if (HttpContext.Current.Request["ja"] != null)
				model.ja=LB.Tools.RequestTool.RequestSafeString("ja");
			if (HttpContext.Current.Request["ka"] != null)
				model.ka=LB.Tools.RequestTool.RequestSafeString("ka");
			if (HttpContext.Current.Request["ko"] != null)
				model.ko=LB.Tools.RequestTool.RequestSafeString("ko");
			if (HttpContext.Current.Request["lt"] != null)
				model.lt=LB.Tools.RequestTool.RequestSafeString("lt");
			if (HttpContext.Current.Request["mk"] != null)
				model.mk=LB.Tools.RequestTool.RequestSafeString("mk");
			if (HttpContext.Current.Request["ms"] != null)
				model.ms=LB.Tools.RequestTool.RequestSafeString("ms");
			if (HttpContext.Current.Request["nl"] != null)
				model.nl=LB.Tools.RequestTool.RequestSafeString("nl");
			if (HttpContext.Current.Request["no"] != null)
				model.no=LB.Tools.RequestTool.RequestSafeString("no");
			if (HttpContext.Current.Request["pl"] != null)
				model.pl=LB.Tools.RequestTool.RequestSafeString("pl");
			if (HttpContext.Current.Request["pt"] != null)
				model.pt=LB.Tools.RequestTool.RequestSafeString("pt");
			if (HttpContext.Current.Request["ro"] != null)
				model.ro=LB.Tools.RequestTool.RequestSafeString("ro");
			if (HttpContext.Current.Request["ru"] != null)
				model.ru=LB.Tools.RequestTool.RequestSafeString("ru");
			if (HttpContext.Current.Request["sv"] != null)
				model.sv=LB.Tools.RequestTool.RequestSafeString("sv");
			if (HttpContext.Current.Request["ta"] != null)
				model.ta=LB.Tools.RequestTool.RequestSafeString("ta");
			if (HttpContext.Current.Request["tag"] != null)
				model.tag=LB.Tools.RequestTool.RequestSafeString("tag");
			if (HttpContext.Current.Request["tcn"] != null)
				model.tcn=LB.Tools.RequestTool.RequestSafeString("tcn");
			if (HttpContext.Current.Request["th"] != null)
				model.th=LB.Tools.RequestTool.RequestSafeString("th");
			if (HttpContext.Current.Request["tr"] != null)
				model.tr=LB.Tools.RequestTool.RequestSafeString("tr");
			if (HttpContext.Current.Request["tw"] != null)
				model.tw=LB.Tools.RequestTool.RequestSafeString("tw");
			if (HttpContext.Current.Request["uk"] != null)
				model.uk=LB.Tools.RequestTool.RequestSafeString("uk");
			if (HttpContext.Current.Request["vi"] != null)
				model.vi=LB.Tools.RequestTool.RequestSafeString("vi");
				return model;
		}


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public Lebi_Language_Tag ReaderBind(IDataReader dataReader)
		{
			Lebi_Language_Tag model=new Lebi_Language_Tag();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id= Convert.ToInt32(ojb);
			}
			model.ar=dataReader["ar"].ToString();
			model.ca=dataReader["ca"].ToString();
			model.CN=dataReader["CN"].ToString();
			model.cs=dataReader["cs"].ToString();
			model.cy=dataReader["cy"].ToString();
			model.da=dataReader["da"].ToString();
			model.de=dataReader["de"].ToString();
			model.el=dataReader["el"].ToString();
			model.EN=dataReader["EN"].ToString();
			model.es=dataReader["es"].ToString();
			model.eu=dataReader["eu"].ToString();
			model.fa=dataReader["fa"].ToString();
			model.fi=dataReader["fi"].ToString();
			model.fr=dataReader["fr"].ToString();
			model.gl=dataReader["gl"].ToString();
			model.he=dataReader["he"].ToString();
			model.hr=dataReader["hr"].ToString();
			model.hu=dataReader["hu"].ToString();
			model.id_=dataReader["id_"].ToString();
			ojb = dataReader["Isar"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isar= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isca"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isca= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsCN"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsCN= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Iscs"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Iscs= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Iscy"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Iscy= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isda"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isda= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isde"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isde= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isel"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isel= Convert.ToInt32(ojb);
			}
			ojb = dataReader["IsEN"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsEN= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ises"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ises= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Iseu"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Iseu= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isfa"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isfa= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isfi"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isfi= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isfr"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isfr= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isgl"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isgl= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ishe"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ishe= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ishr"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ishr= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ishu"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ishu= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isid_"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isid_= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isit"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isit= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isja"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isja= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Iska"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Iska= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isko"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isko= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Islt"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Islt= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ismk"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ismk= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isms"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isms= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isnl"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isnl= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isno"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isno= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ispl"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ispl= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ispt"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ispt= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isro"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isro= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isru"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isru= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Issv"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Issv= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Ista"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ista= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Istcn"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Istcn= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isth"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isth= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Istr"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Istr= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isuk"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isuk= Convert.ToInt32(ojb);
			}
			ojb = dataReader["Isvi"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Isvi= Convert.ToInt32(ojb);
			}
			model.it=dataReader["it"].ToString();
			model.ja=dataReader["ja"].ToString();
			model.ka=dataReader["ka"].ToString();
			model.ko=dataReader["ko"].ToString();
			model.lt=dataReader["lt"].ToString();
			model.mk=dataReader["mk"].ToString();
			model.ms=dataReader["ms"].ToString();
			model.nl=dataReader["nl"].ToString();
			model.no=dataReader["no"].ToString();
			model.pl=dataReader["pl"].ToString();
			model.pt=dataReader["pt"].ToString();
			model.ro=dataReader["ro"].ToString();
			model.ru=dataReader["ru"].ToString();
			model.sv=dataReader["sv"].ToString();
			model.ta=dataReader["ta"].ToString();
			model.tag=dataReader["tag"].ToString();
			model.tcn=dataReader["tcn"].ToString();
			model.th=dataReader["th"].ToString();
			model.tr=dataReader["tr"].ToString();
			model.tw=dataReader["tw"].ToString();
			model.uk=dataReader["uk"].ToString();
			model.vi=dataReader["vi"].ToString();
			return model;
		}

		#endregion  成员方法
	}
}

