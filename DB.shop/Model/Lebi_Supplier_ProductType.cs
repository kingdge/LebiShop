using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_ProductType
	{
		#region Model
		private int _id=0;
		private string _imageurl="";
		private string _name="";
		private int _parentid=0;
		private int _sort=0;
		private int _supplier_id=0;
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
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

