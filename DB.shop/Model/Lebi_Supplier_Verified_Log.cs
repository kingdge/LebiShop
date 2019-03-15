using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Verified_Log
	{
		#region Model
		private int _id=0;
		private string _imageurl="";
		private int _supplier_id=0;
		private DateTime _time_add=DateTime.Now;
		private int _type_id_supplierverifiedstatus=0;
		private int _verified_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int Type_id_SupplierVerifiedStatus
		{
			set{ _type_id_supplierverifiedstatus=value;}
			get{return _type_id_supplierverifiedstatus;}
		}
		public int Verified_id
		{
			set{ _verified_id=value;}
			get{return _verified_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

