using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Administrator
	{
		#region Model
		private int _id=0;
		private string _username="";
		private string _password="";
		private string _ip_last="";
		private string _ip_this="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_this=DateTime.Now;
		private DateTime _time_last=DateTime.Now;
		private int _count_login=0;
		private int _type_id_adminstatus=0;
		private int _admin_group_id=0;
		private string _realname="";
		private string _modilephone="";
		private string _phone="";
		private string _email="";
		private string _sex="";
		private string _admintype="";
		private string _site_ids="";
		private string _pro_type_ids="";
		private int _randnum=0;
		private string _avatar="";
		private Lebi_Administrator _model;
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
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP_Last
		{
			set{ _ip_last=value;}
			get{return _ip_last;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP_This
		{
			set{ _ip_this=value;}
			get{return _ip_this;}
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
		public DateTime Time_This
		{
			set{ _time_this=value;}
			get{return _time_this;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Last
		{
			set{ _time_last=value;}
			get{return _time_last;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Login
		{
			set{ _count_login=value;}
			get{return _count_login;}
		}
		/// <summary>
		/// 230正常,231冻结
		/// </summary>
		public int Type_id_AdminStatus
		{
			set{ _type_id_adminstatus=value;}
			get{return _type_id_adminstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Admin_Group_id
		{
			set{ _admin_group_id=value;}
			get{return _admin_group_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModilePhone
		{
			set{ _modilephone=value;}
			get{return _modilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminType
		{
			set{ _admintype=value;}
			get{return _admintype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_ids
		{
			set{ _site_ids=value;}
			get{return _site_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pro_Type_ids
		{
			set{ _pro_type_ids=value;}
			get{return _pro_type_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RandNum
		{
			set{ _randnum=value;}
			get{return _randnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Avatar
		{
			set{ _avatar=value;}
			get{return _avatar;}
		}
		#endregion

	}
}

