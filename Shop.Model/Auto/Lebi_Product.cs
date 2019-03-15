using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Product
	{
		#region Model
		private int _id=0;
		private int _pro_type_id=0;
		private int _brand_id=0;
		private string _name="";
		private int _sort=0;
		private string _number="";
		private string _code="";
		private int _units_id=0;
		private decimal _weight=0;
		private string _introduction="";
		private string _description="";
		private string _mobiledescription="";
		private int _type_id_productstatus=0;
		private int _count_like=0;
		private int _count_sales_show=0;
		private int _count_sales=0;
		private int _count_views_show=0;
		private int _count_views=0;
		private int _count_stock=0;
		private int _count_stockcaution=0;
		private string _seo_title="";
		private string _seo_description="";
		private string _seo_keywords="";
		private decimal _price_market=0;
		private decimal _price_cost=0;
		private decimal _price=0;
		private string _images="";
		private string _imagesmall="";
		private string _imagemedium="";
		private string _imagebig="";
		private string _imageoriginal="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_onsale=DateTime.Now;
		private DateTime _time_edit=DateTime.Now;
		private string _tags="";
		private string _pro_tag_id="";
		private string _property132="";
		private string _property131="";
		private string _property133="";
		private string _service="";
		private string _remarks="";
		private int _count_viewsfalse=0;
		private int _count_salesfalse=0;
		private int _product_id=0;
		private string _packing="";
		private string _specification="";
		private decimal _star_comment=0;
		private int _count_comment=0;
		private int _count_freeze=0;
		private int _propertymain=0;
		private string _taobaoid="";
		private string _taobaoid_type="";
		private int _issuppliertransport=0;
		private decimal _volumel=0;
		private decimal _volumew=0;
		private decimal _volumeh=0;
		private int _packagerate=0;
		private DateTime _time_expired=DateTime.Now;
		private int _count_limit=0;
		private decimal _price_sale=0;
		private int _type_id_producttype=0;
		private decimal _netweight=0;
		private string _pro_type_id_other="";
		private string _supplier_producttype_ids="";
		private string _site_ids="";
		private DateTime _time_start=DateTime.Now;
		private string _property134="";
		private int _supplier_id=0;
		private string _freezeremark="";
		private string _stepprice="";
		private string _userlevelprice="";
		private string _userlevel_ids_show="";
		private string _userlevel_ids_priceshow="";
		private string _userlevel_ids_buy="";
		private int _isnullstocksale=0;
		private decimal _price_reserve=0;
		private decimal _price_reserve_per=0;
		private int _reserve_days=0;
		private int _iscombo=0;
		private string _userlevelcount="";
		private int _isdel=0;
		private string _location="";
		private Lebi_Product _model;
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
		public int Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Brand_id
		{
			set{ _brand_id=value;}
			get{return _brand_id;}
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
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
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
		public int Units_id
		{
			set{ _units_id=value;}
			get{return _units_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
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
		public string MobileDescription
		{
			set{ _mobiledescription=value;}
			get{return _mobiledescription;}
		}
		/// <summary>
		/// 100下架,101上架,102未审核,103冻结
		/// </summary>
		public int Type_id_ProductStatus
		{
			set{ _type_id_productstatus=value;}
			get{return _type_id_productstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Like
		{
			set{ _count_like=value;}
			get{return _count_like;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Sales_Show
		{
			set{ _count_sales_show=value;}
			get{return _count_sales_show;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Sales
		{
			set{ _count_sales=value;}
			get{return _count_sales;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Views_Show
		{
			set{ _count_views_show=value;}
			get{return _count_views_show;}
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
		public int Count_Stock
		{
			set{ _count_stock=value;}
			get{return _count_stock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_StockCaution
		{
			set{ _count_stockcaution=value;}
			get{return _count_stockcaution;}
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
		public string SEO_Description
		{
			set{ _seo_description=value;}
			get{return _seo_description;}
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
		public decimal Price_Market
		{
			set{ _price_market=value;}
			get{return _price_market;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_Cost
		{
			set{ _price_cost=value;}
			get{return _price_cost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Images
		{
			set{ _images=value;}
			get{return _images;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_OnSale
		{
			set{ _time_onsale=value;}
			get{return _time_onsale;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pro_Tag_id
		{
			set{ _pro_tag_id=value;}
			get{return _pro_tag_id;}
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
		public string Service
		{
			set{ _service=value;}
			get{return _service;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_ViewsFalse
		{
			set{ _count_viewsfalse=value;}
			get{return _count_viewsfalse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_SalesFalse
		{
			set{ _count_salesfalse=value;}
			get{return _count_salesfalse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Packing
		{
			set{ _packing=value;}
			get{return _packing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Specification
		{
			set{ _specification=value;}
			get{return _specification;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Star_Comment
		{
			set{ _star_comment=value;}
			get{return _star_comment;}
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
		public int Count_Freeze
		{
			set{ _count_freeze=value;}
			get{return _count_freeze;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ProPertyMain
		{
			set{ _propertymain=value;}
			get{return _propertymain;}
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
		public string taobaoid_type
		{
			set{ _taobaoid_type=value;}
			get{return _taobaoid_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSupplierTransport
		{
			set{ _issuppliertransport=value;}
			get{return _issuppliertransport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal VolumeL
		{
			set{ _volumel=value;}
			get{return _volumel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal VolumeW
		{
			set{ _volumew=value;}
			get{return _volumew;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal VolumeH
		{
			set{ _volumeh=value;}
			get{return _volumeh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PackageRate
		{
			set{ _packagerate=value;}
			get{return _packagerate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Expired
		{
			set{ _time_expired=value;}
			get{return _time_expired;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Limit
		{
			set{ _count_limit=value;}
			get{return _count_limit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_Sale
		{
			set{ _price_sale=value;}
			get{return _price_sale;}
		}
		/// <summary>
		/// 320一般商品,321限时抢购,322团购,323积分换购,324预定商品
		/// </summary>
		public int Type_id_ProductType
		{
			set{ _type_id_producttype=value;}
			get{return _type_id_producttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal NetWeight
		{
			set{ _netweight=value;}
			get{return _netweight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pro_Type_id_other
		{
			set{ _pro_type_id_other=value;}
			get{return _pro_type_id_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_ProductType_ids
		{
			set{ _supplier_producttype_ids=value;}
			get{return _supplier_producttype_ids;}
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
		public DateTime Time_Start
		{
			set{ _time_start=value;}
			get{return _time_start;}
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FreezeRemark
		{
			set{ _freezeremark=value;}
			get{return _freezeremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StepPrice
		{
			set{ _stepprice=value;}
			get{return _stepprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevelPrice
		{
			set{ _userlevelprice=value;}
			get{return _userlevelprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevel_ids_show
		{
			set{ _userlevel_ids_show=value;}
			get{return _userlevel_ids_show;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevel_ids_priceshow
		{
			set{ _userlevel_ids_priceshow=value;}
			get{return _userlevel_ids_priceshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevel_ids_buy
		{
			set{ _userlevel_ids_buy=value;}
			get{return _userlevel_ids_buy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsNullStockSale
		{
			set{ _isnullstocksale=value;}
			get{return _isnullstocksale;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_reserve
		{
			set{ _price_reserve=value;}
			get{return _price_reserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_reserve_per
		{
			set{ _price_reserve_per=value;}
			get{return _price_reserve_per;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Reserve_days
		{
			set{ _reserve_days=value;}
			get{return _reserve_days;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCombo
		{
			set{ _iscombo=value;}
			get{return _iscombo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevelCount
		{
			set{ _userlevelcount=value;}
			get{return _userlevelcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Location
		{
			set{ _location=value;}
			get{return _location;}
		}
		#endregion

	}
}

