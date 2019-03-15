using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Log
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _adminname="";
		private string _content="";
		private string _description="";
		private string _ip_add="";
		private string _keyid="";
		private string _refererurl="";
		private int _supplier_id=0;
		private string _supplier_subname="";
		private string _tablename="";
		private DateTime _time_add=DateTime.Now;
		private string _url="";
		private int _user_id=0;
		private string _username="";
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
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
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
		public string IP_Add
		{
			set{ _ip_add=value;}
			get{return _ip_add;}
		}
		public string Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		public string RefererURL
		{
			set{ _refererurl=value;}
			get{return _refererurl;}
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
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

