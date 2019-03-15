using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Product_Stock_Log
	{
		#region Model
		private int _id=0;
		private int _product_id=0;
		private decimal _count=0;
		private int _type_id_stock=0;
		private DateTime _time_add=DateTime.Now;
		private string _order_code="";
		private int _order_id=0;
		private string _remark="";
		private Lebi_Product_Stock_Log _model;
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
		public decimal Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_Stock
		{
			set{ _type_id_stock=value;}
			get{return _type_id_stock;}
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
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion

	}
}

