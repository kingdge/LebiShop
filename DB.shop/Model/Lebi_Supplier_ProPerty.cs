using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_ProPerty
	{
		#region Model
		private int _id=0;
		private int _pro_type_id=0;
		private string _property="";
		private int _supplier_id=0;
		private int _type_id_propertytype=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
		}
		public string ProPerty
		{
			set{ _property=value;}
			get{return _property;}
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

