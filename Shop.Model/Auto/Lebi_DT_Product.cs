using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_DT_Product
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private string _username="";
		private int _dt_id=0;
		private int _product_id=0;
		private decimal _price=0;
		private int _parentid=0;
		private int _count_sales=0;
		private int _count_views=0;
		private decimal _price_market=0;
		private Lebi_DT_Product _model;
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
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
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
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		public int Count_Views
		{
			set{ _count_views=value;}
			get{return _count_views;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price_Market
		{
			set{ _price_market=value;}
			get{return _price_market;}
		}
		#endregion

	}
}

