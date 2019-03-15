using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Shop.Model;
using Shop.SQLDataAccess;
namespace Shop.Bussiness
{
	/// <summary>
	/// 业务逻辑类B_Lebi_Type 的摘要说明。
	/// </summary>
	public partial class B_Lebi_Type
	{
		public B_Lebi_Type()
		{}
		#region  成员方法

		/// <summary>
		/// 返回单个字符串
		/// </summary>
		public static string GetValue(string col,string where)
		{
			return D_Lebi_Type.Instance.GetValue(col,where);
		}


		/// <summary>
		/// 返回记录条数
		/// </summary>
		public static int Counts(string where)
		{
			return D_Lebi_Type.Instance.Counts(where);
		}
		public static int Counts(SQLPara para)
		{
			return D_Lebi_Type.Instance.Counts(para);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Lebi_Type model)
		{
			return D_Lebi_Type.Instance.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static void Update(Lebi_Type model)
		{
			D_Lebi_Type.Instance.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static void Delete(int id)
		{
			
			D_Lebi_Type.Instance.Delete(id);
		}
		/// <summary>
		/// 删除多条数据  by where条件
		/// </summary>
		public static void Delete(string where)
		{
			
			D_Lebi_Type.Instance.Delete(where);
		}
		/// <summary>
		/// 删除多条数据
		/// </summary>
		public static void Delete(SQLPara para)
		{
			
			D_Lebi_Type.Instance.Delete(para);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Lebi_Type GetModel(int id)
		{
			
			return D_Lebi_Type.Instance.GetModel(id);
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public static Lebi_Type GetModel(string where)
		{
			
			return D_Lebi_Type.Instance.GetModel(where);
		}
		public static Lebi_Type GetModel(SQLPara para)
		{
			
			return D_Lebi_Type.Instance.GetModel(para);
		}


		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return D_Lebi_Type.Instance.GetMaxID("");
		}
		public static int GetMaxId(SQLPara para)
		{
			return D_Lebi_Type.Instance.GetMaxID(para);
		}
		public static int GetMaxId(string strWhere)
		{
			return D_Lebi_Type.Instance.GetMaxID(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Lebi_Type> GetList(string strWhere,string strFieldOrder)
		{
			return D_Lebi_Type.Instance.GetList(strWhere,strFieldOrder);
		}
		public static List<Lebi_Type> GetList(SQLPara para)
		{
			return D_Lebi_Type.Instance.GetList(para);
		}
		public static List<Lebi_Type> GetList(string strWhere, string strFieldOrder, int PageSize, int page)
		{
			return D_Lebi_Type.Instance.GetList(strWhere,strFieldOrder,PageSize,page);
		}
		public static List<Lebi_Type> GetList(SQLPara para, int PageSize, int page)
		{
			return D_Lebi_Type.Instance.GetList(para,PageSize,page);
		}

		/// <summary>
		/// 绑定表单数据
		/// </summary>
		public static Lebi_Type BindForm(Lebi_Type model)
		{
			
			return D_Lebi_Type.Instance.BindForm(model);
		}
		/// <summary>
		/// 安全方式绑定表单数据
		/// </summary>
		public static Lebi_Type SafeBindForm(Lebi_Type model)
		{
			
			return D_Lebi_Type.Instance.SafeBindForm(model);
		}

		#endregion  成员方法
	}
}

