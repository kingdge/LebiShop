using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Verified
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private int _supplier_group_id=0;
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
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Supplier_Group_id
		{
			set{ _supplier_group_id=value;}
			get{return _supplier_group_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

