using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Config
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _supplier_id=0;
		private string _value="";
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

