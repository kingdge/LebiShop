using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Order_Product
	{
		#region Model
		private int _id=0;
		private int _product_id=0;
		private int _order_id=0;
		private string _order_code="";
		private string _product_name="";
		private string _product_number="";
		private int _count=0;
		private int _count_shipped=0;
		private int _count_received=0;
		private int _count_return=0;
		private decimal _price=0;
		private decimal _money=0;
		private string _remark="";
		private decimal _weight=0;
		private string _imagesmall="";
		private string _imagemedium="";
		private string _imagebig="";
		private string _imageoriginal="";
		private int _type_id_orderproducttype=0;
		private int _discount=0;
		private decimal _point=0;
		private int _iscommented=0;
		private int _user_id=0;
		private DateTime _time_add=DateTime.Now;
		private decimal _price_cost=0;
		private int _supplier_id=0;
		private int _issuppliertransport=0;
		private decimal _volume=0;
		private decimal _netweight=0;
		private int _packagerate=0;
		private int _units_id=0;
		private decimal _money_card312_one=0;
		private decimal _point_give_one=0;
		private decimal _money_give_one=0;
		private decimal _point_product=0;
		private int _pointagain=0;
		private string _property134="";
		private decimal _point_buy_one=0;
		private decimal _property_price=0;
		private int _ispaid=0;
		private int _isstockok=0;
		private DateTime _time_paid=DateTime.Now;
		private DateTime _time_stockok=DateTime.Now;
		private decimal _price_reserve=0;
		private int _ispaidreserve=0;
		private int _isreserve=0;
		private int _isdel=0;
		private Lebi_Order_Product _model;
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
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Product_Name
		{
			set{ _product_name=value;}
			get{return _product_name;}
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
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Shipped
		{
			set{ _count_shipped=value;}
			get{return _count_shipped;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Received
		{
			set{ _count_received=value;}
			get{return _count_received;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Return
		{
			set{ _count_return=value;}
			get{return _count_return;}
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
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		public int Type_id_OrderProductType
		{
			set{ _type_id_orderproducttype=value;}
			get{return _type_id_orderproducttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCommented
		{
			set{ _iscommented=value;}
			get{return _iscommented;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
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
		public decimal Volume
		{
			set{ _volume=value;}
			get{return _volume;}
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
		public int PackageRate
		{
			set{ _packagerate=value;}
			get{return _packagerate;}
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
		public decimal Money_Card312_one
		{
			set{ _money_card312_one=value;}
			get{return _money_card312_one;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point_Give_one
		{
			set{ _point_give_one=value;}
			get{return _point_give_one;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Give_one
		{
			set{ _money_give_one=value;}
			get{return _money_give_one;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point_Product
		{
			set{ _point_product=value;}
			get{return _point_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Pointagain
		{
			set{ _pointagain=value;}
			get{return _pointagain;}
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
		public decimal Point_Buy_one
		{
			set{ _point_buy_one=value;}
			get{return _point_buy_one;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal ProPerty_Price
		{
			set{ _property_price=value;}
			get{return _property_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPaid
		{
			set{ _ispaid=value;}
			get{return _ispaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsStockOK
		{
			set{ _isstockok=value;}
			get{return _isstockok;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Paid
		{
			set{ _time_paid=value;}
			get{return _time_paid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_StockOK
		{
			set{ _time_stockok=value;}
			get{return _time_stockok;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_Reserve
		{
			set{ _price_reserve=value;}
			get{return _price_reserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPaidReserve
		{
			set{ _ispaidreserve=value;}
			get{return _ispaidreserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsReserve
		{
			set{ _isreserve=value;}
			get{return _isreserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion

	}
}

