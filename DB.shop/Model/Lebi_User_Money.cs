using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Money
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private string _description="";
		private int _dt_id=0;
		private decimal _money=0;
		private decimal _money_after=0;
		private decimal _money_fanxian_after=0;
		private string _order_code="";
		private int _order_id=0;
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _type_id_moneystatus=0;
		private int _type_id_moneytype=0;
		private int _user_id=0;
		private string _user_realname="";
		private string _user_username="";
		private string _order_payno="";
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public decimal Money_after
		{
			set{ _money_after=value;}
			get{return _money_after;}
		}
		public decimal Money_fanxian_after
		{
			set{ _money_fanxian_after=value;}
			get{return _money_fanxian_after;}
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
		public int Type_id_MoneyStatus
		{
			set{ _type_id_moneystatus=value;}
			get{return _type_id_moneystatus;}
		}
		public int Type_id_MoneyType
		{
			set{ _type_id_moneytype=value;}
			get{return _type_id_moneytype;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

