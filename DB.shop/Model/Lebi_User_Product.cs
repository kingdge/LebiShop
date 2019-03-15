using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Product
	{
		#region Model
		private int _id=0;
		private int _count=0;
		private int _discount=0;
		private string _imagebig="";
		private string _imagemedium="";
		private string _imageoriginal="";
		private string _imagesmall="";
		private int _pointagain=0;
		private int _pro_type_id=0;
		private int _product_id=0;
		private string _product_number="";
		private decimal _product_point=0;
		private decimal _product_price=0;
		private decimal _property_price=0;
		private string _property134="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_addemail=DateTime.Now;
		private int _type_id_userproducttype=0;
		private int _user_id=0;
		private int _warndays=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int count
		{
			set{ _count=value;}
			get{return _count;}
		}
		public int Discount
		{
			set{ _discount=value;}
			get{return _discount;}
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
		public int Pointagain
		{
			set{ _pointagain=value;}
			get{return _pointagain;}
		}
		public int Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
		}
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		public string Product_Number
		{
			set{ _product_number=value;}
			get{return _product_number;}
		}
		public decimal Product_Point
		{
			set{ _product_point=value;}
			get{return _product_point;}
		}
		public decimal Product_Price
		{
			set{ _product_price=value;}
			get{return _product_price;}
		}
		public decimal ProPerty_Price
		{
			set{ _property_price=value;}
			get{return _property_price;}
		}
		public string ProPerty134
		{
			set{ _property134=value;}
			get{return _property134;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_addemail
		{
			set{ _time_addemail=value;}
			get{return _time_addemail;}
		}
		public int Type_id_UserProductType
		{
			set{ _type_id_userproducttype=value;}
			get{return _type_id_userproducttype;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public int WarnDays
		{
			set{ _warndays=value;}
			get{return _warndays;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

