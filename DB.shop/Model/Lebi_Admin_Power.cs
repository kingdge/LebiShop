using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Admin_Power
	{
		#region Model
		private int _id=0;
		private int _admin_group_id=0;
		private string _admin_limit_code="";
		private int _admin_limit_id=0;
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Admin_Group_id
		{
			set{ _admin_group_id=value;}
			get{return _admin_group_id;}
		}
		public string Admin_Limit_Code
		{
			set{ _admin_limit_code=value;}
			get{return _admin_limit_code;}
		}
		public int Admin_Limit_id
		{
			set{ _admin_limit_id=value;}
			get{return _admin_limit_id;}
		}
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

