using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Theme_Skin
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _name="";
		private int _ispage=0;
		private string _path_skin="";
		private string _pagename="";
		private string _pageparameter="";
		private string _staticpagename="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _theme_id=0;
		private int _sort=0;
		private int _pagesize=0;
		private Lebi_Theme_Skin _model;
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
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPage
		{
			set{ _ispage=value;}
			get{return _ispage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Path_Skin
		{
			set{ _path_skin=value;}
			get{return _path_skin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PageName
		{
			set{ _pagename=value;}
			get{return _pagename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PageParameter
		{
			set{ _pageparameter=value;}
			get{return _pageparameter;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StaticPageName
		{
			set{ _staticpagename=value;}
			get{return _staticpagename;}
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
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PageSize
		{
			set{ _pagesize=value;}
			get{return _pagesize;}
		}
		#endregion

	}
}

