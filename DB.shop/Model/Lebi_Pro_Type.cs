using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Pro_Type
	{
		#region Model
		private int _id=0;
		private string _imagesmall="";
		private string _imageurl="";
		private int _isdel=0;
		private int _isindexshow=0;
		private int _isshow=0;
		private string _isurlrewrite="";
		private int _level=0;
		private string _name="";
		private int _parentid=0;
		private string _path="";
		private string _property131="";
		private string _property132="";
		private string _property133="";
		private string _property134="";
		private string _seo_description="";
		private string _seo_keywords="";
		private string _seo_title="";
		private string _site_ids="";
		private int _sort=0;
		private string _taobaoid="";
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		public int IsIndexShow
		{
			set{ _isindexshow=value;}
			get{return _isindexshow;}
		}
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		public string IsUrlrewrite
		{
			set{ _isurlrewrite=value;}
			get{return _isurlrewrite;}
		}
		public int Level
		{
			set{ _level=value;}
			get{return _level;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		public string ProPerty131
		{
			set{ _property131=value;}
			get{return _property131;}
		}
		public string ProPerty132
		{
			set{ _property132=value;}
			get{return _property132;}
		}
		public string ProPerty133
		{
			set{ _property133=value;}
			get{return _property133;}
		}
		public string ProPerty134
		{
			set{ _property134=value;}
			get{return _property134;}
		}
		public string SEO_Description
		{
			set{ _seo_description=value;}
			get{return _seo_description;}
		}
		public string SEO_Keywords
		{
			set{ _seo_keywords=value;}
			get{return _seo_keywords;}
		}
		public string SEO_Title
		{
			set{ _seo_title=value;}
			get{return _seo_title;}
		}
		public string Site_ids
		{
			set{ _site_ids=value;}
			get{return _site_ids;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string taobaoid
		{
			set{ _taobaoid=value;}
			get{return _taobaoid;}
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

