using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Transport_Order
	{
		#region Model
		private int _id=0;
		private int _transport_id=0;
		private string _transport_name="";
		private string _code="";
		private string _transport_code="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_received=DateTime.Now;
		private string _t_name="";
		private string _t_address="";
		private string _t_email="";
		private string _t_mobilephone="";
		private string _t_phone="";
		private int _order_id=0;
		private int _user_id=0;
		private int _type_id_transportorderstatus=0;
		private int _ishavenewlog=0;
		private decimal _money=0;
		private int _admin_id=0;
		private string _adminname="";
		private int _supplier_id=0;
		private string _supplier_subname="";
		private string _product="";
		private string _log="";
		private string _htmllog="";
		private Lebi_Transport_Order _model;
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
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Name
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Code
		{
			set{ _transport_code=value;}
			get{return _transport_code;}
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
		public DateTime Time_Received
		{
			set{ _time_received=value;}
			get{return _time_received;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Name
		{
			set{ _t_name=value;}
			get{return _t_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Address
		{
			set{ _t_address=value;}
			get{return _t_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Email
		{
			set{ _t_email=value;}
			get{return _t_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_MobilePhone
		{
			set{ _t_mobilephone=value;}
			get{return _t_mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Phone
		{
			set{ _t_phone=value;}
			get{return _t_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
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
		public int Type_id_TransportOrderStatus
		{
			set{ _type_id_transportorderstatus=value;}
			get{return _type_id_transportorderstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsHaveNewLog
		{
			set{ _ishavenewlog=value;}
			get{return _ishavenewlog;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
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
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
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
		public string Product
		{
			set{ _product=value;}
			get{return _product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Log
		{
			set{ _log=value;}
			get{return _log;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HtmlLog
		{
			set{ _htmllog=value;}
			get{return _htmllog;}
		}
		#endregion

	}
}

