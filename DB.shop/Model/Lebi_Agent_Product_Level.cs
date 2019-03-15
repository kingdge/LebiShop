using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Agent_Product_Level
	{
		#region Model
		private int _id=0;
		private int _cardorder_id=0;
		private decimal _commission=0;
		private string _content="";
		private int _count_product=0;
		private int _count_product_change=0;
		private string _name="";
		private decimal _price=0;
		private int _sort=0;
		private int _years=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int CardOrder_id
		{
			set{ _cardorder_id=value;}
			get{return _cardorder_id;}
		}
		public decimal Commission
		{
			set{ _commission=value;}
			get{return _commission;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int Count_Product
		{
			set{ _count_product=value;}
			get{return _count_product;}
		}
		public int Count_product_change
		{
			set{ _count_product_change=value;}
			get{return _count_product_change;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Years
		{
			set{ _years=value;}
			get{return _years;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

