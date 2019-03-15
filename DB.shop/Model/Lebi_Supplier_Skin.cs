using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Skin
	{
		#region Model
		private int _id=0;
		private string _head="";
		private string _image="";
		private int _ispublic=0;
		private int _isshow=0;
		private string _longbar="";
		private string _name="";
		private string _path="";
		private string _shortbar="";
		private int _sort=0;
		private string _type="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string head
		{
			set{ _head=value;}
			get{return _head;}
		}
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		public int IsPublic
		{
			set{ _ispublic=value;}
			get{return _ispublic;}
		}
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		public string longbar
		{
			set{ _longbar=value;}
			get{return _longbar;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		public string shortbar
		{
			set{ _shortbar=value;}
			get{return _shortbar;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

