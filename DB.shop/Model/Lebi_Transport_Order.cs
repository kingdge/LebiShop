using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Transport_Order
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _adminname="";
		private string _code="";
		private string _htmllog="";
		private int _ishavenewlog=0;
		private string _log="";
		private decimal _money=0;
		private int _order_id=0;
		private string _product="";
		private string _ramark="";
		private int _supplier_id=0;
		private string _supplier_subname="";
		private string _t_address="";
		private string _t_email="";
		private string _t_mobilephone="";
		private string _t_name="";
		private string _t_phone="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_received=DateTime.Now;
		private string _transport_code="";
		private int _transport_id=0;
		private string _transport_name="";
		private int _type_id_transportorderstatus=0;
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
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string HtmlLog
		{
			set{ _htmllog=value;}
			get{return _htmllog;}
		}
		public int IsHaveNewLog
		{
			set{ _ishavenewlog=value;}
			get{return _ishavenewlog;}
		}
		public string Log
		{
			set{ _log=value;}
			get{return _log;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public string Product
		{
			set{ _product=value;}
			get{return _product;}
		}
		public string Ramark
		{
			set{ _ramark=value;}
			get{return _ramark;}
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
		public string T_Address
		{
			set{ _t_address=value;}
			get{return _t_address;}
		}
		public string T_Email
		{
			set{ _t_email=value;}
			get{return _t_email;}
		}
		public string T_MobilePhone
		{
			set{ _t_mobilephone=value;}
			get{return _t_mobilephone;}
		}
		public string T_Name
		{
			set{ _t_name=value;}
			get{return _t_name;}
		}
		public string T_Phone
		{
			set{ _t_phone=value;}
			get{return _t_phone;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Received
		{
			set{ _time_received=value;}
			get{return _time_received;}
		}
		public string Transport_Code
		{
			set{ _transport_code=value;}
			get{return _transport_code;}
		}
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		public string Transport_Name
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
		}
		public int Type_id_TransportOrderStatus
		{
			set{ _type_id_transportorderstatus=value;}
			get{return _type_id_transportorderstatus;}
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

