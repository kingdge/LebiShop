using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Product
	{
		#region Model
		private int _id=0;
		private int _brand_id=0;
		private string _code="";
		private int _count_comment=0;
		private int _count_freeze=0;
		private int _count_like=0;
		private int _count_limit=0;
		private int _count_sales=0;
		private int _count_sales_show=0;
		private int _count_salesfalse=0;
		private int _count_stock=0;
		private int _count_stockcaution=0;
		private int _count_views=0;
		private int _count_views_show=0;
		private int _count_viewsfalse=0;
		private string _description="";
		private string _freezeremark="";
		private string _imagebig="";
		private string _imagemedium="";
		private string _imageoriginal="";
		private string _images="";
		private string _imagesmall="";
		private string _introduction="";
		private int _iscombo=0;
		private int _isdel=0;
		private int _isnullstocksale=0;
		private int _issuppliertransport=0;
		private string _mobiledescription="";
		private string _name="";
		private decimal _netweight=0;
		private string _number="";
		private int _packagerate=0;
		private string _packing="";
		private decimal _price=0;
		private decimal _price_cost=0;
		private decimal _price_market=0;
		private decimal _price_reserve=0;
		private decimal _price_reserve_per=0;
		private decimal _price_sale=0;
		private string _pro_tag_id="";
		private int _pro_type_id=0;
		private string _pro_type_id_other="";
		private int _product_id=0;
		private string _property131="";
		private string _property132="";
		private string _property133="";
		private string _property134="";
		private int _propertymain=0;
		private string _remarks="";
		private int _reserve_days=0;
		private string _seo_description="";
		private string _seo_keywords="";
		private string _seo_title="";
		private string _service="";
		private string _site_ids="";
		private int _sort=0;
		private string _specification="";
		private decimal _star_comment=0;
		private string _stepprice="";
		private int _supplier_id=0;
		private string _supplier_producttype_ids="";
		private string _tags="";
		private string _taobaoid="";
		private string _taobaoid_type="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_edit=DateTime.Now;
		private DateTime _time_expired=DateTime.Now;
		private DateTime _time_onsale=DateTime.Now;
		private DateTime _time_start=DateTime.Now;
		private int _type_id_productstatus=0;
		private int _type_id_producttype=0;
		private int _units_id=0;
		private string _userlevel_ids_buy="";
		private string _userlevel_ids_priceshow="";
		private string _userlevel_ids_show="";
		private string _userlevelcount="";
		private string _userlevelprice="";
		private decimal _volumeh=0;
		private decimal _volumel=0;
		private decimal _volumew=0;
		private decimal _weight=0;
		private string _location="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Brand_id
		{
			set{ _brand_id=value;}
			get{return _brand_id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public int Count_Comment
		{
			set{ _count_comment=value;}
			get{return _count_comment;}
		}
		public int Count_Freeze
		{
			set{ _count_freeze=value;}
			get{return _count_freeze;}
		}
		public int Count_Like
		{
			set{ _count_like=value;}
			get{return _count_like;}
		}
		public int Count_Limit
		{
			set{ _count_limit=value;}
			get{return _count_limit;}
		}
		public int Count_Sales
		{
			set{ _count_sales=value;}
			get{return _count_sales;}
		}
		public int Count_Sales_Show
		{
			set{ _count_sales_show=value;}
			get{return _count_sales_show;}
		}
		public int Count_SalesFalse
		{
			set{ _count_salesfalse=value;}
			get{return _count_salesfalse;}
		}
		public int Count_Stock
		{
			set{ _count_stock=value;}
			get{return _count_stock;}
		}
		public int Count_StockCaution
		{
			set{ _count_stockcaution=value;}
			get{return _count_stockcaution;}
		}
		public int Count_Views
		{
			set{ _count_views=value;}
			get{return _count_views;}
		}
		public int Count_Views_Show
		{
			set{ _count_views_show=value;}
			get{return _count_views_show;}
		}
		public int Count_ViewsFalse
		{
			set{ _count_viewsfalse=value;}
			get{return _count_viewsfalse;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string FreezeRemark
		{
			set{ _freezeremark=value;}
			get{return _freezeremark;}
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
		public string Images
		{
			set{ _images=value;}
			get{return _images;}
		}
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		public int IsCombo
		{
			set{ _iscombo=value;}
			get{return _iscombo;}
		}
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		public int IsNullStockSale
		{
			set{ _isnullstocksale=value;}
			get{return _isnullstocksale;}
		}
		public int IsSupplierTransport
		{
			set{ _issuppliertransport=value;}
			get{return _issuppliertransport;}
		}
		public string MobileDescription
		{
			set{ _mobiledescription=value;}
			get{return _mobiledescription;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public decimal NetWeight
		{
			set{ _netweight=value;}
			get{return _netweight;}
		}
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		public int PackageRate
		{
			set{ _packagerate=value;}
			get{return _packagerate;}
		}
		public string Packing
		{
			set{ _packing=value;}
			get{return _packing;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		public decimal Price_Cost
		{
			set{ _price_cost=value;}
			get{return _price_cost;}
		}
		public decimal Price_Market
		{
			set{ _price_market=value;}
			get{return _price_market;}
		}
		public decimal Price_reserve
		{
			set{ _price_reserve=value;}
			get{return _price_reserve;}
		}
		public decimal Price_reserve_per
		{
			set{ _price_reserve_per=value;}
			get{return _price_reserve_per;}
		}
		public decimal Price_Sale
		{
			set{ _price_sale=value;}
			get{return _price_sale;}
		}
		public string Pro_Tag_id
		{
			set{ _pro_tag_id=value;}
			get{return _pro_tag_id;}
		}
		public int Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
		}
		public string Pro_Type_id_other
		{
			set{ _pro_type_id_other=value;}
			get{return _pro_type_id_other;}
		}
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
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
		public int ProPertyMain
		{
			set{ _propertymain=value;}
			get{return _propertymain;}
		}
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		public int Reserve_days
		{
			set{ _reserve_days=value;}
			get{return _reserve_days;}
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
		public string Service
		{
			set{ _service=value;}
			get{return _service;}
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
		public string Specification
		{
			set{ _specification=value;}
			get{return _specification;}
		}
		public decimal Star_Comment
		{
			set{ _star_comment=value;}
			get{return _star_comment;}
		}
		public string StepPrice
		{
			set{ _stepprice=value;}
			get{return _stepprice;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string Supplier_ProductType_ids
		{
			set{ _supplier_producttype_ids=value;}
			get{return _supplier_producttype_ids;}
		}
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		public string taobaoid
		{
			set{ _taobaoid=value;}
			get{return _taobaoid;}
		}
		public string taobaoid_type
		{
			set{ _taobaoid_type=value;}
			get{return _taobaoid_type;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
		}
		public DateTime Time_Expired
		{
			set{ _time_expired=value;}
			get{return _time_expired;}
		}
		public DateTime Time_OnSale
		{
			set{ _time_onsale=value;}
			get{return _time_onsale;}
		}
		public DateTime Time_Start
		{
			set{ _time_start=value;}
			get{return _time_start;}
		}
		public int Type_id_ProductStatus
		{
			set{ _type_id_productstatus=value;}
			get{return _type_id_productstatus;}
		}
		public int Type_id_ProductType
		{
			set{ _type_id_producttype=value;}
			get{return _type_id_producttype;}
		}
		public int Units_id
		{
			set{ _units_id=value;}
			get{return _units_id;}
		}
		public string UserLevel_ids_buy
		{
			set{ _userlevel_ids_buy=value;}
			get{return _userlevel_ids_buy;}
		}
		public string UserLevel_ids_priceshow
		{
			set{ _userlevel_ids_priceshow=value;}
			get{return _userlevel_ids_priceshow;}
		}
		public string UserLevel_ids_show
		{
			set{ _userlevel_ids_show=value;}
			get{return _userlevel_ids_show;}
		}
		public string UserLevelCount
		{
			set{ _userlevelcount=value;}
			get{return _userlevelcount;}
		}
		public string UserLevelPrice
		{
			set{ _userlevelprice=value;}
			get{return _userlevelprice;}
		}
		public decimal VolumeH
		{
			set{ _volumeh=value;}
			get{return _volumeh;}
		}
		public decimal VolumeL
		{
			set{ _volumel=value;}
			get{return _volumel;}
		}
		public decimal VolumeW
		{
			set{ _volumew=value;}
			get{return _volumew;}
		}
		public decimal Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		public string Location
		{
			set{ _location=value;}
			get{return _location;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

