using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Brand
	{
		#region Model
		private int _id=0;
		private int _count=0;
		private string _description="";
		private string _firstletter="";
		private string _imageurl="";
		private int _isrecommend=0;
		private int _isvat=0;
		private string _name="";
		private string _pro_type_id="";
		private string _seo_description="";
		private string _seo_keywords="";
		private string _seo_title="";
		private int _sort=0;
		private int _supplier_id=0;
		private int _type_id_brandstatus=0;
		private int _type_id_supplierstatus=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string FirstLetter
		{
			set{ _firstletter=value;}
			get{return _firstletter;}
		}
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public int IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		public int IsVAT
		{
			set{ _isvat=value;}
			get{return _isvat;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public int Type_id_BrandStatus
		{
			set{ _type_id_brandstatus=value;}
			get{return _type_id_brandstatus;}
		}
		public int Type_id_SupplierStatus
		{
			set{ _type_id_supplierstatus=value;}
			get{return _type_id_supplierstatus;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

