using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_FriendLink
	{
		#region Model
		private int _id=0;
		private int _ispic=0;
		private int _isshow=0;
		private string _language="";
		private string _language_ids="";
		private string _logo="";
		private string _name="";
		private int _sort=0;
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int IsPic
		{
			set{ _ispic=value;}
			get{return _ispic;}
		}
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

