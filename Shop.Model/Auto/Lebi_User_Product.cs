using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_User_Product
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private int _product_id=0;
		private DateTime _time_add=DateTime.Now;
		private int _type_id_userproducttype=0;
		private int _count=0;
		private string _imageoriginal="";
		private string _imagebig="";
		private string _imagemedium="";
		private string _imagesmall="";
		private string _product_number="";
		private int _pro_type_id=0;
		private int _discount=0;
		private int _pointagain=0;
		private decimal _product_point=0;
		private decimal _product_price=0;
		private string _property134="";
		private int _warndays=0;
		private DateTime _time_addemail=DateTime.Now;
		private decimal _property_price=0;
		private Lebi_User_Product _model;
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
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 141收藏夹,142购物车,143浏览记录,144常购清单
		/// </summary>
		public int Type_id_UserProductType
		{
			set{ _type_id_userproducttype=value;}
			get{return _type_id_userproducttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int count
		{
			set{ _count=value;}
			get{return _count;}
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
		public string ImageBig
		{
			set{ _imagebig=value;}
			get{return _imagebig;}
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
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Product_Number
		{
			set{ _product_number=value;}
			get{return _product_number;}
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
		/// 商品折扣
		/// </summary>
		public int Discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		/// <summary>
		/// 积分倍数
		/// </summary>
		public int Pointagain
		{
			set{ _pointagain=value;}
			get{return _pointagain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Product_Point
		{
			set{ _product_point=value;}
			get{return _product_point;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Product_Price
		{
			set{ _product_price=value;}
			get{return _product_price;}
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
		public int WarnDays
		{
			set{ _warndays=value;}
			get{return _warndays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_addemail
		{
			set{ _time_addemail=value;}
			get{return _time_addemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal ProPerty_Price
		{
			set{ _property_price=value;}
			get{return _property_price;}
		}
		#endregion

	}
}

