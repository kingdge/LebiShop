using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_ServicePanel_Group
	{
		#region Model
		private int _id=0;
		private string _language="";
		private string _language_ids="";
		private string _name="";
		private int _sort=0;
		private int _supplier_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

