using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Config
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _value="";
		private int _supplier_id=0;
		private Lebi_Supplier_Config _model;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		#endregion

	}
}

