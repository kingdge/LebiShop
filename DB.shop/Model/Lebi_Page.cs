using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Page
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _author="";
		private string _content="";
		private int _count_comment=0;
		private int _count_views=0;
		private string _description="";
		private string _editor="";
		private string _email="";
		private string _imagebig="";
		private string _imagemedium="";
		private string _imageoriginal="";
		private string _imagesmall="";
		private string _language="";
		private string _language_ids="";
		private string _name="";
		private string _namecolor="";
		private int _node_id=0;
		private string _seo_description="";
		private string _seo_keywords="";
		private string _seo_title="";
		private int _sort=0;
		private string _source="";
		private string _sourceurl="";
		private string _subname="";
		private int _supplier_id=0;
		private string _target="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private string _url="";
		private int _user_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int Count_Comment
		{
			set{ _count_comment=value;}
			get{return _count_comment;}
		}
		public int Count_Views
		{
			set{ _count_views=value;}
			get{return _count_views;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string Editor
		{
			set{ _editor=value;}
			get{return _editor;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string ImageBig
		{
			set{ _imagebig=value;}
			get{return _imagebig;}
		}
		public string ImageMedium
		{
			set{ _imagemedium=value;}
			get{return _imagemedium;}
		}
		public string ImageOriginal
		{
			set{ _imageoriginal=value;}
			get{return _imageoriginal;}
		}
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string NameColor
		{
			set{ _namecolor=value;}
			get{return _namecolor;}
		}
		public int Node_id
		{
			set{ _node_id=value;}
			get{return _node_id;}
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
		public string source
		{
			set{ _source=value;}
			get{return _source;}
		}
		public string sourceurl
		{
			set{ _sourceurl=value;}
			get{return _sourceurl;}
		}
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string target
		{
			set{ _target=value;}
			get{return _target;}
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
		public string url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public int user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

