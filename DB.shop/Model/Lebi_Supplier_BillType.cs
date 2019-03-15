using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_BillType
	{
		#region Model
		private int _id=0;
		private int _supplier_id=0;
		private decimal _taxrate=0;
		private int _type_id_billtype=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public decimal TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
		}
		public int Type_id_BillType
		{
			set{ _type_id_billtype=value;}
			get{return _type_id_billtype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

