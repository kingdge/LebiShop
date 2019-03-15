using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_EmailTask
	{
		#region Model
		private int _id=0;
		private string _emailtitle="";
		private string _user_ids="";
		private string _userlevel_ids="";
		private int _admin_id=0;
		private string _admin_username="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_edit=DateTime.Now;
		private DateTime _time_task=DateTime.Now;
		private int _issubmit=0;
		private int _count_send=0;
		private int _count=0;
		private string _emailcontent="";
		private Lebi_EmailTask _model;
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
		public string EmailTitle
		{
			set{ _emailtitle=value;}
			get{return _emailtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_ids
		{
			set{ _user_ids=value;}
			get{return _user_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevel_ids
		{
			set{ _userlevel_ids=value;}
			get{return _userlevel_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_task
		{
			set{ _time_task=value;}
			get{return _time_task;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSubmit
		{
			set{ _issubmit=value;}
			get{return _issubmit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_send
		{
			set{ _count_send=value;}
			get{return _count_send;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmailContent
		{
			set{ _emailcontent=value;}
			get{return _emailcontent;}
		}
		#endregion

	}
}

