using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Cash
	{
		#region Model
		private int _id=0;
		private string _accountcode="";
		private string _accountname="";
		private int _admin_id=0;
		private string _admin_username="";
		private string _bank="";
		private int _dt_id=0;
		private decimal _fee=0;
		private decimal _money=0;
		private string _paytype="";
		private string _remark="";
		private int _supplier_id=0;
		private string _supplier_subname="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _type_id_cashstatus=0;
		private int _user_id=0;
		private string _user_username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string AccountCode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		public string AccountName
		{
			set{ _accountname=value;}
			get{return _accountname;}
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
		public string Bank
		{
			set{ _bank=value;}
			get{return _bank;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public decimal Fee
		{
			set{ _fee=value;}
			get{return _fee;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public string PayType
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Type_id_CashStatus
		{
			set{ _type_id_cashstatus=value;}
			get{return _type_id_cashstatus;}
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

