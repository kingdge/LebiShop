using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Transport_Price
	{
		#region Model
		private int _id=0;
		private int _area_id=0;
		private string _container="";
		private string _description="";
		private int _iscanofflinepay=0;
		private int _isoneprice=0;
		private decimal _ordermoney=0;
		private decimal _price=0;
		private decimal _price_ordermoneyok=0;
		private decimal _price_step=0;
		private int _sort=0;
		private int _supplier_id=0;
		private int _transport_id=0;
		private decimal _weight_start=0;
		private decimal _weight_step=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		public string Container
		{
			set{ _container=value;}
			get{return _container;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int IsCanofflinePay
		{
			set{ _iscanofflinepay=value;}
			get{return _iscanofflinepay;}
		}
		public int IsOnePrice
		{
			set{ _isoneprice=value;}
			get{return _isoneprice;}
		}
		public decimal OrderMoney
		{
			set{ _ordermoney=value;}
			get{return _ordermoney;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		public decimal Price_OrderMoneyOK
		{
			set{ _price_ordermoneyok=value;}
			get{return _price_ordermoneyok;}
		}
		public decimal Price_Step
		{
			set{ _price_step=value;}
			get{return _price_step;}
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
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		public decimal Weight_Start
		{
			set{ _weight_start=value;}
			get{return _weight_start;}
		}
		public decimal Weight_Step
		{
			set{ _weight_step=value;}
			get{return _weight_step;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

