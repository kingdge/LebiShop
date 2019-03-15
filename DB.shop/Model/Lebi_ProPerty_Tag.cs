using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_ProPerty_Tag
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private int _supplier_id=0;
		private int _type_id_propertytype=0;
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public int Type_id_ProPertyType
		{
			set{ _type_id_propertytype=value;}
			get{return _type_id_propertytype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

