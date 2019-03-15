using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Comment
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private string _content="";
		private string _images="";
		private string _imagessmall="";
		private int _isread=0;
		private int _keyid=0;
		private string _language_code="";
		private int _parentid=0;
		private int _product_id=0;
		private int _star=0;
		private int _status=0;
		private int _supplier_id=0;
		private string _supplier_subname="";
		private string _tablename="";
		private DateTime _time_add=DateTime.Now;
		private int _user_id=0;
		private string _user_username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public string Images
		{
			set{ _images=value;}
			get{return _images;}
		}
		public string ImagesSmall
		{
			set{ _imagessmall=value;}
			get{return _imagessmall;}
		}
		public int IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		public int Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		public string Language_Code
		{
			set{ _language_code=value;}
			get{return _language_code;}
		}
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		public int Star
		{
			set{ _star=value;}
			get{return _star;}
		}
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string Supplier_SubName
		{
			set{ _supplier_subname=value;}
			get{return _supplier_subname;}
		}
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

