using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_DT_Product
	{
		#region Model
		private int _id=0;
		private int _count_sales=0;
		private int _count_views=0;
		private int _dt_id=0;
		private int _parentid=0;
		private decimal _price=0;
		private decimal _price_market=0;
		private int _product_id=0;
		private int _user_id=0;
		private string _username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Count_Sales
		{
			set{ _count_sales=value;}
			get{return _count_sales;}
		}
		public int Count_Views
		{
			set{ _count_views=value;}
			get{return _count_views;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		public decimal Price_Market
		{
			set{ _price_market=value;}
			get{return _price_market;}
		}
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

