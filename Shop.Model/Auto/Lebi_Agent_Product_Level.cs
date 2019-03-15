using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Agent_Product_Level
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private decimal _price=0;
		private int _count_product=0;
		private int _count_product_change=0;
		private int _years=0;
		private string _content="";
		private decimal _commission=0;
		private int _cardorder_id=0;
		private Lebi_Agent_Product_Level _model;
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
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Product
		{
			set{ _count_product=value;}
			get{return _count_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_product_change
		{
			set{ _count_product_change=value;}
			get{return _count_product_change;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Years
		{
			set{ _years=value;}
			get{return _years;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Commission
		{
			set{ _commission=value;}
			get{return _commission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CardOrder_id
		{
			set{ _cardorder_id=value;}
			get{return _cardorder_id;}
		}
		#endregion

	}
}

