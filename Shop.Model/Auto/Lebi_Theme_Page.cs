using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Theme_Page
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private string _code="";
		private string _pagename="";
		private string _pageparameter="";
		private string _staticpagename="";
		private string _staticpath="";
		private int _type_id_publishtype=0;
		private string _seo_title="";
		private string _seo_keywords="";
		private string _seo_description="";
		private int _isallowhtml=0;
		private int _iscustom=0;
		private Lebi_Theme_Page _model;
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
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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
		public string StaticPath
		{
			set{ _staticpath=value;}
			get{return _staticpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_PublishType
		{
			set{ _type_id_publishtype=value;}
			get{return _type_id_publishtype;}
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
		public int IsAllowHtml
		{
			set{ _isallowhtml=value;}
			get{return _isallowhtml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCustom
		{
			set{ _iscustom=value;}
			get{return _iscustom;}
		}
		#endregion

	}
}

