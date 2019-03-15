using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Shop.Model;
using Shop.SQLDataAccess;
namespace Shop.Bussiness
{
	/// <summary>
	/// 业务逻辑类B_Lebi_User 的摘要说明。
	/// </summary>
	public partial class B_Lebi_User
	{
		public B_Lebi_User()
		{}
		#region  成员方法

		/// <summary>
		/// 返回单个字符串
		/// </summary>
		public static string GetValue(string col,string where)
		{
			return D_Lebi_User.Instance.GetValue(col,where);
		}


		/// <summary>
		/// 返回记录条数
		/// </summary>
		public static int Counts(string where)
		{
			return D_Lebi_User.Instance.Counts(where);
		}
		public static int Counts(SQLPara para)
		{
			return D_Lebi_User.Instance.Counts(para);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Lebi_User model)
		{
			return D_Lebi_User.Instance.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static void Update(Lebi_User model)
		{
			D_Lebi_User.Instance.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static void Delete(int id)
		{
			
			D_Lebi_User.Instance.Delete(id);
		}
		/// <summary>
		/// 删除多条数据  by where条件
		/// </summary>
		public static void Delete(string where)
		{
			
			D_Lebi_User.Instance.Delete(where);
		}
		/// <summary>
		/// 删除多条数据
		/// </summary>
		public static void Delete(SQLPara para)
		{
			
			D_Lebi_User.Instance.Delete(para);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Lebi_User GetModel(int id)
		{
			
			return D_Lebi_User.Instance.GetModel(id);
		}
		/// <summary>
		/// 得到一个对象实体 by where条件
		/// </summary>
		public static Lebi_User GetModel(string where)
		{
			
			return D_Lebi_User.Instance.GetModel(where);
		}
		public static Lebi_User GetModel(SQLPara para)
		{
			
			return D_Lebi_User.Instance.GetModel(para);
		}


		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return D_Lebi_User.Instance.GetMaxID("");
		}
		public static int GetMaxId(SQLPara para)
		{
			return D_Lebi_User.Instance.GetMaxID(para);
		}
		public static int GetMaxId(string strWhere)
		{
			return D_Lebi_User.Instance.GetMaxID(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Lebi_User> GetList(string strWhere,string strFieldOrder)
		{
			return D_Lebi_User.Instance.GetList(strWhere,strFieldOrder);
		}
		public static List<Lebi_User> GetList(SQLPara para)
		{
			return D_Lebi_User.Instance.GetList(para);
		}
		public static List<Lebi_User> GetList(string strWhere, string strFieldOrder, int PageSize, int page)
		{
			return D_Lebi_User.Instance.GetList(strWhere,strFieldOrder,PageSize,page);
		}
		public static List<Lebi_User> GetList(SQLPara para, int PageSize, int page)
		{
			return D_Lebi_User.Instance.GetList(para,PageSize,page);
		}

		/// <summary>
		/// 绑定表单数据
		/// </summary>
		public static Lebi_User BindForm(Lebi_User model)
		{
			
			return D_Lebi_User.Instance.BindForm(model);
		}
		/// <summary>
		/// 安全方式绑定表单数据
		/// </summary>
		public static Lebi_User SafeBindForm(Lebi_User model)
		{
			
			return D_Lebi_User.Instance.SafeBindForm(model);
		}

		#endregion  成员方法
	}
}

