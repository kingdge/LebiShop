using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Product_Combo
	{
		#region Model
		private int _id=0;
		private int _count=0;
		private int _product_id=0;
		private int _product_id_son=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		public int Product_id_son
		{
			set{ _product_id_son=value;}
			get{return _product_id_son;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

