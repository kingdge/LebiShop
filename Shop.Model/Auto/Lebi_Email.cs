using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Email
	{
		#region Model
		private int _id=0;
		private string _email="";
		private int _user_id=0;
		private string _user_name="";
		private string _title="";
		private string _content="";
		private int _count_send=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_task=DateTime.Now;
		private DateTime _time_send=DateTime.Now;
		private int _type_id_emailstatus=0;
		private int _emailtask_id=0;
		private string _tablename="";
		private int _keyid=0;
		private Lebi_Email _model;
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
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
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
		public string User_Name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Task
		{
			set{ _time_task=value;}
			get{return _time_task;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Send
		{
			set{ _time_send=value;}
			get{return _time_send;}
		}
		/// <summary>
		/// 270排队中,271成功,272失败
		/// </summary>
		public int Type_id_EmailStatus
		{
			set{ _type_id_emailstatus=value;}
			get{return _type_id_emailstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int EmailTask_id
		{
			set{ _emailtask_id=value;}
			get{return _emailtask_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		#endregion

	}
}

