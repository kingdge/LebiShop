using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_BillType
	{
		#region Model
		private int _id=0;
		private decimal _taxrate=0;
		private int _type_id_billtype=0;
		private int _supplier_id=0;
		private Lebi_Supplier_BillType _model;
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
		public decimal TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
		}
		/// <summary>
		/// 150不开发票 ,151普通发票,152增值税发票
		/// </summary>
		public int Type_id_BillType
		{
			set{ _type_id_billtype=value;}
			get{return _type_id_billtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		#endregion

	}
}

