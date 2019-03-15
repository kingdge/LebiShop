using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Point
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private decimal _point=0;
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _type_id_pointstatus=0;
		private int _user_id=0;
		private string _user_realname="";
		private string _user_username="";
		private string _order_payno="";
		private string _order_code="";
		private int _order_id=0;
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
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Type_id_PointStatus
		{
			set{ _type_id_pointstatus=value;}
			get{return _type_id_pointstatus;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string User_RealName
		{
			set{ _user_realname=value;}
			get{return _user_realname;}
		}
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		public string Order_PayNo
		{
			set{ _order_payno=value;}
			get{return _order_payno;}
		}
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

