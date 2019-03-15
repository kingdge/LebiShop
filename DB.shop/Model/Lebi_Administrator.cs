using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Administrator
	{
		#region Model
		private int _id = 0;
		private string _username = "";
		private string _password = "";
		private string _ip_last = "";
		private string _ip_this = "";
		private DateTime _time_add = DateTime.Now;
		private DateTime _time_this = DateTime.Now;
		private DateTime _time_last = DateTime.Now;
		private int _count_login = 0;
		private int _type_id_adminstatus = 0;
		private int _admin_group_id = 0;
		private string _realname = "";
		private string _modilephone = "";
		private string _phone = "";
		private string _email = "";
		private string _sex = "";
		private string _admintype = "";
		private string _site_ids = "";
		private string _pro_type_ids = "";
		private int _randnum = 0;
		private string _avatar = "";
		private string _project_ids = "";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		public string IP_Last
		{
			set{ _ip_last=value;}
			get{return _ip_last;}
		}
		public string IP_This
		{
			set{ _ip_this=value;}
			get{return _ip_this;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_This
		{
			set{ _time_this=value;}
			get{return _time_this;}
		}
		public DateTime Time_Last
		{
			set{ _time_last=value;}
			get{return _time_last;}
		}
		public int Count_Login
		{
			set{ _count_login=value;}
			get{return _count_login;}
		}
		public int Type_id_AdminStatus
		{
			set{ _type_id_adminstatus=value;}
			get{return _type_id_adminstatus;}
		}
		public int Admin_Group_id
		{
			set{ _admin_group_id=value;}
			get{return _admin_group_id;}
		}
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		public string ModilePhone
		{
			set{ _modilephone=value;}
			get{return _modilephone;}
		}
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		public string AdminType
		{
			set{ _admintype=value;}
			get{return _admintype;}
		}
		public string Site_ids
		{
			set{ _site_ids=value;}
			get{return _site_ids;}
		}
		public string Pro_Type_ids
		{
			set{ _pro_type_ids=value;}
			get{return _pro_type_ids;}
		}
		public int RandNum
		{
			set{ _randnum=value;}
			get{return _randnum;}
		}
		public string Avatar
		{
			set{ _avatar=value;}
			get{return _avatar;}
		}
		public string Project_ids
		{
			set{ _project_ids=value;}
			get{return _project_ids;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

