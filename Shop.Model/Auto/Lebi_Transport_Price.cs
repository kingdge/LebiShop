using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Transport_Price
	{
		#region Model
		private int _id=0;
		private int _transport_id=0;
		private decimal _price=0;
		private decimal _weight_start=0;
		private decimal _weight_step=0;
		private decimal _price_step=0;
		private int _iscanofflinepay=0;
		private string _description="";
		private int _isoneprice=0;
		private int _area_id=0;
		private decimal _ordermoney=0;
		private string _container="";
		private decimal _price_ordermoneyok=0;
		private int _supplier_id=0;
		private int _sort=0;
		private Lebi_Transport_Price _model;
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
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
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
		public decimal Weight_Start
		{
			set{ _weight_start=value;}
			get{return _weight_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Weight_Step
		{
			set{ _weight_step=value;}
			get{return _weight_step;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_Step
		{
			set{ _price_step=value;}
			get{return _price_step;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCanofflinePay
		{
			set{ _iscanofflinepay=value;}
			get{return _iscanofflinepay;}
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
		public int IsOnePrice
		{
			set{ _isoneprice=value;}
			get{return _isoneprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal OrderMoney
		{
			set{ _ordermoney=value;}
			get{return _ordermoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Container
		{
			set{ _container=value;}
			get{return _container;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_OrderMoneyOK
		{
			set{ _price_ordermoneyok=value;}
			get{return _price_ordermoneyok;}
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
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion

	}
}

