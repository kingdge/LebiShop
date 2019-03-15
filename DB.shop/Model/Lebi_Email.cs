using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Email
	{
		#region Model
		private int _id=0;
		private string _content="";
		private int _count_send=0;
		private string _email="";
		private int _emailtask_id=0;
		private int _keyid=0;
		private string _tablename="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_send=DateTime.Now;
		private DateTime _time_task=DateTime.Now;
		private string _title="";
		private int _type_id_emailstatus=0;
		private int _user_id=0;
		private string _user_name="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int Count_send
		{
			set{ _count_send=value;}
			get{return _count_send;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public int EmailTask_id
		{
			set{ _emailtask_id=value;}
			get{return _emailtask_id;}
		}
		public int Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Send
		{
			set{ _time_send=value;}
			get{return _time_send;}
		}
		public DateTime Time_Task
		{
			set{ _time_task=value;}
			get{return _time_task;}
		}
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		public int Type_id_EmailStatus
		{
			set{ _type_id_emailstatus=value;}
			get{return _type_id_emailstatus;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string User_Name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

