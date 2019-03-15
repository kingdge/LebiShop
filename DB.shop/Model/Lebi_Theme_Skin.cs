using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Theme_Skin
	{
		#region Model
		private int _id=0;
		private string _code="";
		private int _ispage=0;
		private string _name="";
		private string _pagename="";
		private string _pageparameter="";
		private int _pagesize=0;
		private string _path_skin="";
		private int _sort=0;
		private string _staticpagename="";
		private int _theme_id=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
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
		public int IsPage
		{
			set{ _ispage=value;}
			get{return _ispage;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string PageName
		{
			set{ _pagename=value;}
			get{return _pagename;}
		}
		public string PageParameter
		{
			set{ _pageparameter=value;}
			get{return _pageparameter;}
		}
		public int PageSize
		{
			set{ _pagesize=value;}
			get{return _pagesize;}
		}
		public string Path_Skin
		{
			set{ _path_skin=value;}
			get{return _path_skin;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string StaticPageName
		{
			set{ _staticpagename=value;}
			get{return _staticpagename;}
		}
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

