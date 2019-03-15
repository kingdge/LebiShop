using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LB.DataAccess;
namespace DB.LebiShop
{
	/// <summary>
	/// 业务逻辑类B_Lebi_Product 的摘要说明。
	/// </summary>
	public partial class B_Lebi_Product
	{
		public B_Lebi_Product()
		{}
		#region  成员方法

		/// <summary>
		/// 返回单个字符串
		/// </summary>
		public static string GetValue(string col,string where, int seconds=0)
		{
			return D_Lebi_Product.Instance.GetValue(col,where,seconds);
		}


		/// <summary>
		/// 返回记录条数
		/// </summary>
		public static int Counts(string where, int seconds=0)
		{
			return D_Lebi_Product.Instance.Counts(where,seconds);
		}
		public static int Counts(SQLPara para, int seconds=0)
		{
			return D_Lebi_Product.Instance.Counts(para,seconds);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Lebi_Product model)
		{
			return D_Lebi_Product.Instance.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static void Update(Lebi_Product model)
		{
			D_Lebi_Product.Instance.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static void Delete(int id)
		{
			
			D_Lebi_Product.Instance.Delete(id);
		}
		/// <summary>
		/// 删除多条数据  by where条件
		/// </summary>
		public static void Delete(string where)
		{
			
			D_Lebi_Product.Instance.Delete(where);
		}
		/// <summary>
		/// 删除多条数据
		/// </summary>
		public static void Delete(SQLPara para)
		{
			
			D_Lebi_Product.Instance.Delete(para);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Lebi_Product GetModel(int id, int seconds=0)
		{
			
			return D_Lebi_Product.Instance.GetModel(id,seconds);
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public static Lebi_Product GetModel(string where, int seconds=0)
		{
			
			return D_Lebi_Product.Instance.GetModel(where,seconds);
		}
		public static Lebi_Product GetModel(SQLPara para, int seconds=0)
		{
			
			return D_Lebi_Product.Instance.GetModel(para,seconds);
		}


		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return D_Lebi_Product.Instance.GetMaxID("");
		}
		public static int GetMaxId(SQLPara para)
		{
			return D_Lebi_Product.Instance.GetMaxID(para);
		}
		public static int GetMaxId(string strWhere)
		{
			return D_Lebi_Product.Instance.GetMaxID(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Lebi_Product> GetList(string strWhere,string strFieldOrder, int seconds=0)
		{
			return D_Lebi_Product.Instance.GetList(strWhere,strFieldOrder,seconds);
		}
		public static List<Lebi_Product> GetList(SQLPara para, int seconds=0)
		{
			return D_Lebi_Product.Instance.GetList(para,seconds);
		}
		public static List<Lebi_Product> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
		{
			return D_Lebi_Product.Instance.GetList(strWhere,strFieldOrder,PageSize,page,seconds);
		}
		public static List<Lebi_Product> GetList(SQLPara para, int PageSize, int page, int seconds=0)
		{
			return D_Lebi_Product.Instance.GetList(para,PageSize,page,seconds);
		}

		/// <summary>
		/// 绑定表单数据
		/// </summary>
		public static Lebi_Product BindForm(Lebi_Product model)
		{
			
			return D_Lebi_Product.Instance.BindForm(model);
		}
		/// <summary>
		/// 安全方式绑定表单数据
		/// </summary>
		public static Lebi_Product SafeBindForm(Lebi_Product model)
		{
			
			return D_Lebi_Product.Instance.SafeBindForm(model);
		}

		#endregion  成员方法
	}
}

