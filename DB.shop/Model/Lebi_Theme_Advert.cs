using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Theme_Advert
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _content="";
		private string _description="";
		private int _height=0;
		private int _sort=0;
		private int _theme_id=0;
		private int _width=0;
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
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		public int Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

