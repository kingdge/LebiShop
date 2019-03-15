using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Brand
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _imageurl="";
		private int _sort=0;
		private int _isrecommend=0;
		private string _firstletter="";
		private string _pro_type_id="";
		private int _count=0;
		private string _description="";
		private string _seo_title="";
		private string _seo_keywords="";
		private string _seo_description="";
		private int _supplier_id=0;
		private int _type_id_brandstatus=0;
		private int _isvat=0;
		private Lebi_Brand _model;
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
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
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
		public int IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FirstLetter
		{
			set{ _firstletter=value;}
			get{return _firstletter;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 451未审核,452已审核,453已停用
		/// </summary>
		public int Type_id_BrandStatus
		{
			set{ _type_id_brandstatus=value;}
			get{return _type_id_brandstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsVAT
		{
			set{ _isvat=value;}
			get{return _isvat;}
		}
		#endregion

	}
}

