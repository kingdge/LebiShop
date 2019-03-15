using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_ServicePanel_Type
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _face="";
		private int _isonline=0;
		private string _name="";
		private int _sort=0;
		private string _text="";
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Face
		{
			set{ _face=value;}
			get{return _face;}
		}
		public int IsOnline
		{
			set{ _isonline=value;}
			get{return _isonline;}
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
		public string Text
		{
			set{ _text=value;}
			get{return _text;}
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

