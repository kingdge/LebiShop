using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_BillType
	{
		#region Model
		private int _id=0;
		private string _content="";
		private string _description="";
		private string _name="";
		private int _sort=0;
		private decimal _taxrate=0;
		private int _type_id_billtype=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
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
		public decimal TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
		}
		public int Type_id_BillType
		{
			set{ _type_id_billtype=value;}
			get{return _type_id_billtype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

