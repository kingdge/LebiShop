using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Power
	{
		#region Model
		private int _id=0;
		private int _supplier_group_id=0;
		private int _supplier_limit_id=0;
		private string _supplier_limit_code="";
		private string _url="";
		private Lebi_Supplier_Power _model;
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
		public int Supplier_Group_id
		{
			set{ _supplier_group_id=value;}
			get{return _supplier_group_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_Limit_id
		{
			set{ _supplier_limit_id=value;}
			get{return _supplier_limit_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_Limit_Code
		{
			set{ _supplier_limit_code=value;}
			get{return _supplier_limit_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		#endregion

	}
}

