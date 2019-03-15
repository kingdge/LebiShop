using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_BillType
	{
		#region Model
		private int _id=0;
		private decimal _taxrate=0;
		private string _name="";
		private string _description="";
		private int _type_id_billtype=0;
		private int _sort=0;
		private string _content="";
		private Lebi_BillType _model;
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
		public decimal TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_BillType
		{
			set{ _type_id_billtype=value;}
			get{return _type_id_billtype;}
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
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion

	}
}

