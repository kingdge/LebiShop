using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Power
	{
		#region Model
		private int _id=0;
		private int _supplier_group_id=0;
		private string _supplier_limit_code="";
		private int _supplier_limit_id=0;
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Supplier_Group_id
		{
			set{ _supplier_group_id=value;}
			get{return _supplier_group_id;}
		}
		public string Supplier_Limit_Code
		{
			set{ _supplier_limit_code=value;}
			get{return _supplier_limit_code;}
		}
		public int Supplier_Limit_id
		{
			set{ _supplier_limit_id=value;}
			get{return _supplier_limit_id;}
		}
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

