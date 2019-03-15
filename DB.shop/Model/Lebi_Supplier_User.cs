using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_User
	{
		#region Model
		private int _id=0;
		private string _remark="";
		private string _remarkname="";
		private int _supplier_id=0;
		private int _supplier_usergroup_id=0;
		private DateTime _time_add=DateTime.Now;
		private int _type_id_supplieruserstatus=0;
		private int _user_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public string RemarkName
		{
			set{ _remarkname=value;}
			get{return _remarkname;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public int Supplier_UserGroup_id
		{
			set{ _supplier_usergroup_id=value;}
			get{return _supplier_usergroup_id;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int Type_id_SupplierUserStatus
		{
			set{ _type_id_supplieruserstatus=value;}
			get{return _type_id_supplieruserstatus;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

