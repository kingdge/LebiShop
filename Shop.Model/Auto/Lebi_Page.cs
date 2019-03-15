using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Page
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _subname="";
		private string _namecolor="";
		private string _content="";
		private string _description="";
		private int _node_id=0;
		private string _seo_title="";
		private string _seo_keywords="";
		private string _seo_description="";
		private string _target="";
		private string _url="";
		private string _language_ids="";
		private string _language="";
		private string _sourceurl="";
		private string _source="";
		private string _author="";
		private string _editor="";
		private string _email="";
		private int _sort=0;
		private int _user_id=0;
		private int _admin_id=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _count_views=0;
		private int _count_comment=0;
		private string _imagesmall="";
		private string _imagemedium="";
		private string _imagebig="";
		private string _imageoriginal="";
		private int _supplier_id=0;
		private Lebi_Page _model;
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
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NameColor
		{
			set{ _namecolor=value;}
			get{return _namecolor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Node_id
		{
			set{ _node_id=value;}
			get{return _node_id;}
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
		public string target
		{
			set{ _target=value;}
			get{return _target;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sourceurl
		{
			set{ _sourceurl=value;}
			get{return _sourceurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Editor
		{
			set{ _editor=value;}
			get{return _editor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
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
		public int user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
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
		public int Count_Views
		{
			set{ _count_views=value;}
			get{return _count_views;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Comment
		{
			set{ _count_comment=value;}
			get{return _count_comment;}
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
		public string ImageMedium
		{
			set{ _imagemedium=value;}
			get{return _imagemedium;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageBig
		{
			set{ _imagebig=value;}
			get{return _imagebig;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageOriginal
		{
			set{ _imageoriginal=value;}
			get{return _imageoriginal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		#endregion

	}
}

