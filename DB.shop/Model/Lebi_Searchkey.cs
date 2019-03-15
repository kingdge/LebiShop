using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Searchkey
	{
		#region Model
		private int _id=0;
		private string _language_code="";
		private int _language_id=0;
		private string _name="";
		private int _sort=0;
		private int _type=0;
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Language_Code
		{
			set{ _language_code=value;}
			get{return _language_code;}
		}
		public int Language_id
		{
			set{ _language_id=value;}
			get{return _language_id;}
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
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		public string URL
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

