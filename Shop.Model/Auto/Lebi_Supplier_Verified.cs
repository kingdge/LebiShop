using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Verified
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private int _supplier_group_id=0;
		private Lebi_Supplier_Verified _model;
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
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_Group_id
		{
			set{ _supplier_group_id=value;}
			get{return _supplier_group_id;}
		}
		#endregion

	}
}

