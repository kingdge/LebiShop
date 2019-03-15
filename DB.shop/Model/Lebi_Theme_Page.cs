using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Theme_Page
	{
		#region Model
		private int _id=0;
		private string _code="";
		private int _isallowhtml=0;
		private int _iscustom=0;
		private string _name="";
		private string _pagename="";
		private string _pageparameter="";
		private string _seo_description="";
		private string _seo_keywords="";
		private string _seo_title="";
		private int _sort=0;
		private string _staticpagename="";
		private string _staticpath="";
		private int _type_id_publishtype=0;
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
		public int IsAllowHtml
		{
			set{ _isallowhtml=value;}
			get{return _isallowhtml;}
		}
		public int IsCustom
		{
			set{ _iscustom=value;}
			get{return _iscustom;}
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
		public string StaticPath
		{
			set{ _staticpath=value;}
			get{return _staticpath;}
		}
		public int Type_id_PublishType
		{
			set{ _type_id_publishtype=value;}
			get{return _type_id_publishtype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

