using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Product_Price
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private decimal _price=0;
		private int _product_id=0;
		private Lebi_Product_Price _model;
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
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		#endregion

	}
}

