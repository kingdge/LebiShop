using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Inquiry
	{
		#region Model
		private int _id=0;
		private string _content="";
		private string _email="";
		private string _language="";
		private string _phone="";
		private string _subject="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _type_id_inquirystatus=0;
		private string _username="";
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
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		public string Subject
		{
			set{ _subject=value;}
			get{return _subject;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Type_id_InquiryStatus
		{
			set{ _type_id_inquirystatus=value;}
			get{return _type_id_inquirystatus;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

