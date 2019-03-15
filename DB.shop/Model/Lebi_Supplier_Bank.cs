using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Bank
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _name="";
		private int _supplier_id=0;
		private string _username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
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
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

