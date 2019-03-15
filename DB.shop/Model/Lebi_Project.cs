using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Project
	{
		#region Model
		private int _id = 0;
		private string _name = "";
		private string _supplier_ids = "";
		private int _sort = 0;
		private string _site_ids = "";
		private string _pro_type_ids = "";
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
		public string Supplier_ids
		{
			set{ _supplier_ids=value;}
			get{return _supplier_ids;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string Site_ids
		{
			set{ _site_ids=value;}
			get{return _site_ids;}
		}
		public string Pro_Type_ids
		{
			set{ _pro_type_ids=value;}
			get{return _pro_type_ids;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

