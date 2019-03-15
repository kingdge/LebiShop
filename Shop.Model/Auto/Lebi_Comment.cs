using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Comment
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private int _admin_id=0;
		private string _tablename="";
		private int _keyid=0;
		private string _content="";
		private DateTime _time_add=DateTime.Now;
		private int _star=0;
		private int _status=0;
		private string _user_username="";
		private string _admin_username="";
		private string _language_code="";
		private int _parentid=0;
		private string _imagessmall="";
		private string _images="";
		private int _supplier_id=0;
		private string _supplier_subname="";
		private int _product_id=0;
		private int _isread=0;
		private Lebi_Comment _model;
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
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Star
		{
			set{ _star=value;}
			get{return _star;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_Code
		{
			set{ _language_code=value;}
			get{return _language_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImagesSmall
		{
			set{ _imagessmall=value;}
			get{return _imagessmall;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Images
		{
			set{ _images=value;}
			get{return _images;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_SubName
		{
			set{ _supplier_subname=value;}
			get{return _supplier_subname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		#endregion

	}
}

