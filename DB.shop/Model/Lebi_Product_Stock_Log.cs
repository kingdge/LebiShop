using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Product_Stock_Log
	{
		#region Model
		private int _id=0;
		private decimal _count=0;
		private string _order_code="";
		private int _order_id=0;
		private int _product_id=0;
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private int _type_id_stock=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public decimal Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int Type_id_Stock
		{
			set{ _type_id_stock=value;}
			get{return _type_id_stock;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

