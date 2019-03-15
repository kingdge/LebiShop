using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Node
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private string _code="";
		private int _parentid=0;
		private string _seo_title="";
		private string _seo_keywords="";
		private string _seo_description="";
		private string _target="";
		private string _url="";
		private int _haveson=0;
		private string _adminpage_index="";
		private string _adminpage="";
		private int _typeflag=0;
		private int _type_id_publishtype=0;
		private string _language="";
		private string _language_ids="";
		private string _showmode="";
		private int _islanguages=0;
		private int _supplier_id=0;
		private Lebi_Node _model;
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
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		public int haveson
		{
			set{ _haveson=value;}
			get{return _haveson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminPage_Index
		{
			set{ _adminpage_index=value;}
			get{return _adminpage_index;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminPage
		{
			set{ _adminpage=value;}
			get{return _adminpage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeFlag
		{
			set{ _typeflag=value;}
			get{return _typeflag;}
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
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
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
		public string ShowMode
		{
			set{ _showmode=value;}
			get{return _showmode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsLanguages
		{
			set{ _islanguages=value;}
			get{return _islanguages;}
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

