using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Menu
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _image="";
		private int _isshow=0;
		private int _issys=0;
		private string _name="";
		private int _parentid=0;
		private int _sort=0;
		private string _url="";
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
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		public int Isshow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		public int IsSYS
		{
			set{ _issys=value;}
			get{return _issys;}
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
		public string URL
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

