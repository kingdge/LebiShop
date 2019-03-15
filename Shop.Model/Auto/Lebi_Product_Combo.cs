using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Product_Combo
	{
		#region Model
		private int _id=0;
		private int _product_id=0;
		private int _product_id_son=0;
		private int _count=0;
		private Lebi_Product_Combo _model;
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
		public int Product_id_son
		{
			set{ _product_id_son=value;}
			get{return _product_id_son;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		#endregion

	}
}

