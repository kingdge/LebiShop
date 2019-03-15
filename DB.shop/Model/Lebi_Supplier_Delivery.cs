using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Delivery
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _remark="";
		private int _sort=0;
		private int _supplier_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

