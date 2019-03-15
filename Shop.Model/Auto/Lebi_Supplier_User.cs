using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_User
	{
		#region Model
		private int _id=0;
		private int _supplier_id=0;
		private int _user_id=0;
		private DateTime _time_add=DateTime.Now;
		private string _remark="";
		private string _remarkname="";
		private int _supplier_usergroup_id=0;
		private int _type_id_supplieruserstatus=0;
		private Lebi_Supplier_User _model;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RemarkName
		{
			set{ _remarkname=value;}
			get{return _remarkname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_UserGroup_id
		{
			set{ _supplier_usergroup_id=value;}
			get{return _supplier_usergroup_id;}
		}
		/// <summary>
		/// 9010未审核,9011已审核,9012已停用
		/// </summary>
		public int Type_id_SupplierUserStatus
		{
			set{ _type_id_supplieruserstatus=value;}
			get{return _type_id_supplieruserstatus;}
		}
		#endregion

	}
}

