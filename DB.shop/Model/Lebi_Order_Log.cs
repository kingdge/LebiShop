using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Order_Log
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_name="";
		private string _content="";
		private int _order_id=0;
		private DateTime _time_add=DateTime.Now;
		private int _user_id=0;
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
		public string Admin_Name
		{
			set{ _admin_name=value;}
			get{return _admin_name;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

