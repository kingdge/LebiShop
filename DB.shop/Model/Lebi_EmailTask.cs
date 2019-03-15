using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_EmailTask
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private int _count=0;
		private int _count_send=0;
		private string _emailcontent="";
		private string _emailtitle="";
		private int _issubmit=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_edit=DateTime.Now;
		private DateTime _time_task=DateTime.Now;
		private string _user_ids="";
		private string _userlevel_ids="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		public int Count_send
		{
			set{ _count_send=value;}
			get{return _count_send;}
		}
		public string EmailContent
		{
			set{ _emailcontent=value;}
			get{return _emailcontent;}
		}
		public string EmailTitle
		{
			set{ _emailtitle=value;}
			get{return _emailtitle;}
		}
		public int IsSubmit
		{
			set{ _issubmit=value;}
			get{return _issubmit;}
		}
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
		}
		public DateTime Time_task
		{
			set{ _time_task=value;}
			get{return _time_task;}
		}
		public string User_ids
		{
			set{ _user_ids=value;}
			get{return _user_ids;}
		}
		public string UserLevel_ids
		{
			set{ _userlevel_ids=value;}
			get{return _userlevel_ids;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

