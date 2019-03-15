using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Pro_Type
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _parentid=0;
		private string _path="";
		private int _isindexshow=0;
		private int _sort=0;
		private int _level=0;
		private string _seo_title="";
		private string _seo_keywords="";
		private string _seo_description="";
		private int _isshow=0;
		private string _property132="";
		private string _property131="";
		private string _property133="";
		private string _imageurl="";
		private string _imagesmall="";
		private string _taobaoid="";
		private string _url="";
		private string _site_ids="";
		private string _property134="";
		private string _isurlrewrite="";
		private int _isdel=0;
		private Lebi_Pro_Type _model;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsIndexShow
		{
			set{ _isindexshow=value;}
			get{return _isindexshow;}
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
		public int Level
		{
			set{ _level=value;}
			get{return _level;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEO_Title
		{
			set{ _seo_title=value;}
			get{return _seo_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEO_Keywords
		{
			set{ _seo_keywords=value;}
			get{return _seo_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEO_Description
		{
			set{ _seo_description=value;}
			get{return _seo_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProPerty132
		{
			set{ _property132=value;}
			get{return _property132;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProPerty131
		{
			set{ _property131=value;}
			get{return _property131;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProPerty133
		{
			set{ _property133=value;}
			get{return _property133;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string taobaoid
		{
			set{ _taobaoid=value;}
			get{return _taobaoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_ids
		{
			set{ _site_ids=value;}
			get{return _site_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProPerty134
		{
			set{ _property134=value;}
			get{return _property134;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsUrlrewrite
		{
			set{ _isurlrewrite=value;}
			get{return _isurlrewrite;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion

	}
}

