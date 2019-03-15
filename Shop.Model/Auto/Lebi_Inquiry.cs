using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Inquiry
	{
		#region Model
		private int _id=0;
		private string _username="";
		private string _email="";
		private string _subject="";
		private string _content="";
		private int _type_id_inquirystatus=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private string _language="";
		private string _phone="";
		private Lebi_Inquiry _model;
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
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Subject
		{
			set{ _subject=value;}
			get{return _subject;}
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
		public int Type_id_InquiryStatus
		{
			set{ _type_id_inquirystatus=value;}
			get{return _type_id_inquirystatus;}
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
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		#endregion

	}
}

