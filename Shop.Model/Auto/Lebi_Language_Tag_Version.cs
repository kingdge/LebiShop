using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Language_Tag_Version
	{
		#region Model
		private int _id=0;
		private int _version=0;
		private string _language_code="";
		private DateTime _time_update=DateTime.Now;
		private string _content="";
		private Lebi_Language_Tag_Version _model;
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
		public int Version
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_Code
		{
			set{ _language_code=value;}
			get{return _language_code;}
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
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion

	}
}

