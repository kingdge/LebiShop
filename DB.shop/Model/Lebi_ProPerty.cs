using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_ProPerty
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _imageurl="";
		private string _name="";
		private int _parentid=0;
		private int _parentsort=0;
		private decimal _price=0;
		private string _remark="";
		private int _sort=0;
		private int _supplier_id=0;
		private string _tag="";
		private string _taobaoid="";
		private int _type_id_propertytype=0;
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
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public int parentSort
		{
			set{ _parentsort=value;}
			get{return _parentsort;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		public string Tag
		{
			set{ _tag=value;}
			get{return _tag;}
		}
		public string taobaoid
		{
			set{ _taobaoid=value;}
			get{return _taobaoid;}
		}
		public int Type_id_ProPertyType
		{
			set{ _type_id_propertytype=value;}
			get{return _type_id_propertytype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

