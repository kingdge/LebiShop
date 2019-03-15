using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Verified_Log
	{
		#region Model
		private int _id=0;
		private string _imageurl="";
		private int _type_id_supplierverifiedstatus=0;
		private DateTime _time_add=DateTime.Now;
		private int _verified_id=0;
		private int _supplier_id=0;
		private Lebi_Supplier_Verified_Log _model;
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
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 9020未审核,9021已审核,9022未通过
		/// </summary>
		public int Type_id_SupplierVerifiedStatus
		{
			set{ _type_id_supplierverifiedstatus=value;}
			get{return _type_id_supplierverifiedstatus;}
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
		public int Verified_id
		{
			set{ _verified_id=value;}
			get{return _verified_id;}
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

