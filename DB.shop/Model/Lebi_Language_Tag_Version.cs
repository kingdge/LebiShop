using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Language_Tag_Version
	{
		#region Model
		private int _id=0;
		private string _content="";
		private string _language_code="";
		private DateTime _time_update=DateTime.Now;
		private int _version=0;
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
		public string Language_Code
		{
			set{ _language_code=value;}
			get{return _language_code;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Version
		{
			set{ _version=value;}
			get{return _version;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

