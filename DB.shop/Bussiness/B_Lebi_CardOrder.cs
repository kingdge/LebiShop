using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LB.DataAccess;
namespace DB.LebiShop
{
	/// <summary>
	/// 业务逻辑类B_Lebi_CardOrder 的摘要说明。
	/// </summary>
	public partial class B_Lebi_CardOrder
	{
		public B_Lebi_CardOrder()
		{}
		#region  成员方法

		/// <summary>
		/// 返回单个字符串
		/// </summary>
		public static string GetValue(string col,string where, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.GetValue(col,where,seconds);
		}


		/// <summary>
		/// 返回记录条数
		/// </summary>
		public static int Counts(string where, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.Counts(where,seconds);
		}
		public static int Counts(SQLPara para, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.Counts(para,seconds);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Lebi_CardOrder model)
		{
			return D_Lebi_CardOrder.Instance.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static void Update(Lebi_CardOrder model)
		{
			D_Lebi_CardOrder.Instance.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static void Delete(int id)
		{
			
			D_Lebi_CardOrder.Instance.Delete(id);
		}
		/// <summary>
		/// 删除多条数据  by where条件
		/// </summary>
		public static void Delete(string where)
		{
			
			D_Lebi_CardOrder.Instance.Delete(where);
		}
		/// <summary>
		/// 删除多条数据
		/// </summary>
		public static void Delete(SQLPara para)
		{
			
			D_Lebi_CardOrder.Instance.Delete(para);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Lebi_CardOrder GetModel(int id, int seconds=0)
		{
			
			return D_Lebi_CardOrder.Instance.GetModel(id,seconds);
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public static Lebi_CardOrder GetModel(string where, int seconds=0)
		{
			
			return D_Lebi_CardOrder.Instance.GetModel(where,seconds);
		}
		public static Lebi_CardOrder GetModel(SQLPara para, int seconds=0)
		{
			
			return D_Lebi_CardOrder.Instance.GetModel(para,seconds);
		}


		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return D_Lebi_CardOrder.Instance.GetMaxID("");
		}
		public static int GetMaxId(SQLPara para)
		{
			return D_Lebi_CardOrder.Instance.GetMaxID(para);
		}
		public static int GetMaxId(string strWhere)
		{
			return D_Lebi_CardOrder.Instance.GetMaxID(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Lebi_CardOrder> GetList(string strWhere,string strFieldOrder, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.GetList(strWhere,strFieldOrder,seconds);
		}
		public static List<Lebi_CardOrder> GetList(SQLPara para, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.GetList(para,seconds);
		}
		public static List<Lebi_CardOrder> GetList(string strWhere, string strFieldOrder, int PageSize, int page, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.GetList(strWhere,strFieldOrder,PageSize,page,seconds);
		}
		public static List<Lebi_CardOrder> GetList(SQLPara para, int PageSize, int page, int seconds=0)
		{
			return D_Lebi_CardOrder.Instance.GetList(para,PageSize,page,seconds);
		}

		/// <summary>
		/// 绑定表单数据
		/// </summary>
		public static Lebi_CardOrder BindForm(Lebi_CardOrder model)
		{
			
			return D_Lebi_CardOrder.Instance.BindForm(model);
		}
		/// <summary>
		/// 安全方式绑定表单数据
		/// </summary>
		public static Lebi_CardOrder SafeBindForm(Lebi_CardOrder model)
		{
			
			return D_Lebi_CardOrder.Instance.SafeBindForm(model);
		}

		#endregion  成员方法
	}
}

